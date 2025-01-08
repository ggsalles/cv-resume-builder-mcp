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

        private byte[] sal = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x5, 0x4, 0x3, 0x2, 0x1, 0x0 };
        private string mensagem = "";
        private byte[] textoCifrado;

        public DataTable buscaUsuarioLogin(string pNome)

        {
            var strConexao = System.Configuration.ConfigurationManager.ConnectionStrings["P_WEDLC"].ConnectionString;
            var conexao = new MySqlConnection(strConexao);
            conexao.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_login", conexao);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            conexao.Close();

            return dt;
        }

        public string critptografiaSenha(string pSenha)
        {
            string senha = pSenha;

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
                    }
                }
            }

            senha = senha + Convert.ToBase64String(textoCifrado) + "    ::  senha => " + pSenha + Environment.NewLine;

            return senha;
        }

        public string descritptografiaSenha(string pSenha)
        {

            string senha = pSenha;

            Rfc2898DeriveBytes chave = new Rfc2898DeriveBytes(senha, sal);
            var algoritmo = new RijndaelManaged();

            algoritmo.Key = chave.GetBytes(16);
            algoritmo.IV = chave.GetBytes(16);
            using (var StreamFonte = new MemoryStream(textoCifrado))
            {
                using (MemoryStream StreamDestino = new MemoryStream())
                {
                    using (CryptoStream crypto = new CryptoStream(StreamFonte, algoritmo.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        moveBytes(crypto, StreamDestino);
                        byte[] bytesDescriptografados = StreamDestino.ToArray();
                        var mensagemDescriptografada = new UnicodeEncoding().GetString(bytesDescriptografados);
                        senha = mensagemDescriptografada;
                    }
                }
            }

            return senha;
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
    }
}