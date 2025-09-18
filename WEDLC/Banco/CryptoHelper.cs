using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WEDLC.Banco
{
    public static class CryptoHelper
    {
        private static GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();
        private static MySqlConnection conexao = new MySqlConnection();

        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CryptoConfig.json");
        private static byte[] Key;
        private static byte[] IV;

        // Dados originais
        public static int Id { get; set; }
        public static string IpServidor { get; private set; }
        public static string ShareServidor { get; private set; }
        public static string Usuario { get; private set; }
        public static string Senha { get; private set; }

        static CryptoHelper()
        {
            LoadOrCreateKeyIV();
        }

        private static void LoadOrCreateKeyIV()
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                var cfg = JsonSerializer.Deserialize<CryptoConfig>(json);
                Key = Convert.FromBase64String(cfg.Key);
                IV = Convert.FromBase64String(cfg.IV);
            }
            else
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    Key = new byte[32]; // AES-256
                    IV = new byte[16];  // AES IV
                    rng.GetBytes(Key);
                    rng.GetBytes(IV);

                    var cfg = new CryptoConfig
                    {
                        Key = Convert.ToBase64String(Key),
                        IV = Convert.ToBase64String(IV)
                    };

                    File.WriteAllText(ConfigPath, JsonSerializer.Serialize(cfg, new JsonSerializerOptions { WriteIndented = true }));
                }
            }
        }

        private class CryptoConfig
        {
            public string Key { get; set; }
            public string IV { get; set; }
        }

        public static bool conectaBanco()
        {
            conexao = objcConexao.CriarConexao();
            conexao.Open();
            return conexao.State == ConnectionState.Open;
        }

        // --- MÉTODO PARA SETAR CREDENCIAIS
        public static void SetCredenciais(string ip, string share, string user, string pass)
        {
            IpServidor = ip;
            ShareServidor = share;
            Usuario = user;
            Senha = pass;
        }

        // --- ENCRYPT / DECRYPT
        public static byte[] Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                    return encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                }
            }
        }

        public static string Decrypt(byte[] cipherBytes)
        {
            if (cipherBytes == null || cipherBytes.Length == 0)
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        // --- INSERIR NO BANCO
        public static bool IncluiCriptografia()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("sp_inserir_credenciais", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                        new MySqlParameter("p_ip_servidor", MySqlDbType.VarBinary) { Value = Encrypt(IpServidor) },
                        new MySqlParameter("p_share_servidor", MySqlDbType.VarBinary) { Value = Encrypt(ShareServidor)},
                        new MySqlParameter("p_usuario", MySqlDbType.VarBinary) { Value = Encrypt(Usuario)},
                        new MySqlParameter("p_senha", MySqlDbType.VarBinary) { Value = Encrypt(Senha)},
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected >= 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }

        // --- BUSCAR DO BANCO
        public static DataTable BuscaCriptografia()
        {
            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("sp_obter_credenciais", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pId", Id);

                    sqlDa.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[dt.Rows.Count - 1];

                        // Descriptografa os valores
                        string ip = Decrypt((byte[])row["ip_servidor"]);
                        string share = Decrypt((byte[])row["share_servidor"]);
                        string user = Decrypt((byte[])row["usuario"]);
                        string pass = Decrypt((byte[])row["senha"]);

                        // Cria novas colunas para armazenar os valores como string (se ainda não existirem)
                        if (!dt.Columns.Contains("ip_descriptografado"))
                            dt.Columns.Add("ip_descriptografado", typeof(string));
                        if (!dt.Columns.Contains("share_descriptografado"))
                            dt.Columns.Add("share_descriptografado", typeof(string));
                        if (!dt.Columns.Contains("user_descriptografado"))
                            dt.Columns.Add("user_descriptografado", typeof(string));
                        if (!dt.Columns.Contains("pass_descriptografado"))
                            dt.Columns.Add("pass_descriptografado", typeof(string));

                        // Atribui os valores descriptografados nas novas colunas
                        row["ip_descriptografado"] = ip;
                        row["share_descriptografado"] = share;
                        row["user_descriptografado"] = user;
                        row["pass_descriptografado"] = pass;
                    }

                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar do banco: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }

    }
}
