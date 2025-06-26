using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WEDLC.Banco
{

    public class clLogin
    {

        public GerenciadorConexaoMySQL objCconexao;

        private byte[] sal = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x5, 0x4, 0x3, 0x2, 0x1, 0x0 };
        private byte[] textoCifrado;

        public int Idusuario { get; set; } // field
        public string Nome { get; set; } // field
        public string Senha { get; set; } // field

        //public string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["L_WEDLC"].ConnectionString; // Producao
        //public string stringConexao = System.Configuration.ConfigurationManager.ConnectionStrings["R_WEDLC"].ConnectionString; // Remoto

        GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();
        MySqlConnection conexao = new MySqlConnection();

        public bool conectaBanco()
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

        public MySqlConnection MySqlConection() // Determina a instância que será usada para a conexão: 1 - local; 2 - Remoto
        {
            GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();
            var conexao = new MySqlConnection(objcConexao.CriarConexao().ConnectionString);
            return conexao;
        }

        public DataTable buscaUsuarioLogin(string pNome)

        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null; // Fix: Return null instead of a boolean to match the DataTable return type  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Fix: Return null instead of a boolean to match the DataTable return type  
            }

            try
            {
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_login", conexao);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                conexao.Close();

                return dt;
            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return null;
            }
        }

        public string critptografiaSenha(string pSenha, string pNome, out byte[] pCifrado)
        {
            string senha = pSenha;
            string mensagem = pNome;

            Rfc2898DeriveBytes chave = new Rfc2898DeriveBytes(senha, sal);
            // criptografa os dados
            var algoritmo = new RijndaelManaged();

            algoritmo.Key = chave.GetBytes(16);
            algoritmo.IV = chave.GetBytes(16);

            byte[] fonteBytes = new System.Text.UnicodeEncoding().GetBytes(mensagem);

            using (var StreamFonte = new MemoryStream(fonteBytes))
            {
                using (MemoryStream StreamDestino = new MemoryStream())
                {
                    using (CryptoStream crypto = new CryptoStream(StreamFonte, algoritmo.CreateEncryptor(), CryptoStreamMode.Read))
                    {
                        moveBytes(crypto, StreamDestino);
                        textoCifrado = StreamDestino.ToArray();
                        pCifrado = textoCifrado;
                    }
                }
            }

            //senha = senha + Convert.ToBase64String(textoCifrado) + "    ::  senha => " + pSenha + Environment.NewLine;

            senha = Convert.ToBase64String(textoCifrado);

            return senha;
        }

        public string descritptografiaSenha(string pSenha, byte[] pCifrado)
        {

            {
                if ((pCifrado == null))
                {
                    MessageBox.Show("Os dados não estão criptografados!");
                    return "";
                }

                string msgDescriptografada = "";

                Rfc2898DeriveBytes chave = new Rfc2898DeriveBytes(pSenha, sal);
                var algoritmo = new RijndaelManaged();

                algoritmo.Key = chave.GetBytes(16);
                algoritmo.IV = chave.GetBytes(16);
                using (var StreamFonte = new MemoryStream(pCifrado))
                {
                    using (MemoryStream StreamDestino = new MemoryStream())
                    {
                        using (CryptoStream crypto = new CryptoStream(StreamFonte, algoritmo.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            moveBytes(crypto, StreamDestino);
                            byte[] bytesDescriptografados = StreamDestino.ToArray();
                            var mensagemDescriptografada = new UnicodeEncoding().GetString(bytesDescriptografados);
                            msgDescriptografada = mensagemDescriptografada;
                        }
                    }
                }
                return msgDescriptografada;
            }

        }

        private void moveBytes(Stream fonte, Stream destino)
        {
            byte[] bytes = new byte[2049];
            var contador = fonte.Read(bytes, 0, bytes.Length - 1);
            while ((0 != contador))
            {
                destino.Write(bytes, 0, contador);
                contador = fonte.Read(bytes, 0, bytes.Length - 1);
            }
        }

        public bool incluiLogin()
        {
            var conexao = MySqlConection();

            conexao.Open();

            MySqlParameter[] pParam = new MySqlParameter[3];
            MySqlCommand command = new MySqlCommand();

            pParam[0] = new MySqlParameter("pNome", MySqlDbType.VarChar);
            pParam[0].Value = Nome;

            pParam[1] = new MySqlParameter("pPassword", MySqlDbType.VarChar);
            pParam[1].Value = Senha;

            pParam[2] = new MySqlParameter("pIdUsuario", MySqlDbType.Int16);
            pParam[2].Value = Idusuario;

            command.Connection = conexao;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pr_incluilogin";
            command.Parameters.AddRange(pParam);

            if (command.ExecuteNonQuery() == 1)

            {
                conexao.Close();
                return true;
            }
            else
            {
                conexao.Close();
                return false;
            }
        }
    }
}