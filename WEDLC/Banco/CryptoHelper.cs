using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using WEDLC.Banco;

public static class CryptoHelper
{
    // Chave secreta (32 bytes para AES-256) - você pode guardar em config seguro
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("MinhaChaveSuperSecreta123456789012");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("ChaveInicial12345"); // 16 bytes

    // Construtor
    static GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();
    static MySqlConnection conexao = new MySqlConnection();

    // Dados originais
    public static int id { get; set; }
    public static string ipServidor { get; set; }
    public static string shareServidor { get; set; }
    public static string usuario { get; set; }
    public static string senha { get; set; }

    // Criptografa
    private static byte[] ipEnc = CryptoHelper.Encrypt(ipServidor);
    private static byte[] shareEnc = CryptoHelper.Encrypt(shareServidor);
    private static byte[] userEnc = CryptoHelper.Encrypt(usuario);
    private static byte[] passEnc = CryptoHelper.Encrypt(senha);

    public static bool conectaBanco()
    {
        conexao = objcConexao.CriarConexao();
        conexao.Open();
        if (conexao.State == ConnectionState.Open)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static byte[] Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            return encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        }
    }

    public static string Decrypt(byte[] cipherBytes)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    public static bool IncluiCriptografia()
    {
        if (!conectaBanco())
            return false;

        try
        {
            using (var cmd = new MySqlCommand("sp_inserir_credenciais", conexao))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new MySqlParameter[]
                {
                cmd.Parameters.AddWithValue("@ip", ipEnc),
                cmd.Parameters.AddWithValue("@share", shareEnc),
                cmd.Parameters.AddWithValue("@user", userEnc),
                cmd.Parameters.AddWithValue("@pass", passEnc),
            });

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Qualquer número positivo indica sucesso
            }
        }
        catch (MySqlException ex)
        {
            // Log específico para diagnóstico
            System.Diagnostics.Debug.WriteLine($"Erro ao incluir sp_inserir_credenciais: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Log para outros tipos de erro
            System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
            return false;
        }
        finally
        {
            conexao?.Close();
        }
    }

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
                sqlDa.SelectCommand.Parameters.AddWithValue("pId", id);

                sqlDa.Fill(dt);

                dt.Rows[dt.Rows.Count - 1]["ip"] = Decrypt((byte[])dt.Rows[dt.Rows.Count - 1]["ip"]);
                dt.Rows[dt.Rows.Count - 1]["share"] = Decrypt((byte[])dt.Rows[dt.Rows.Count - 1]["share"]);
                dt.Rows[dt.Rows.Count - 1]["user"] = Decrypt((byte[])dt.Rows[dt.Rows.Count - 1]["user"]);
                dt.Rows[dt.Rows.Count - 1]["pass"] = Decrypt(((byte[])dt.Rows[dt.Rows.Count - 1]["pass"]));

                return dt;
            }
        }
        catch (MySqlException ex)
        {
            // Log específico para diagnóstico
            System.Diagnostics.Debug.WriteLine($"Erro na sp_obter_credenciais: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Log para outros erros
            System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
            return null;
        }
        finally
        {
            conexao?.Close();
        }
    }
}
