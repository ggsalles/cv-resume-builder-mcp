using MySql.Data.MySqlClient;
using System.Configuration;
using static WEDLC.Banco.CryptoHelper;
using System;

namespace WEDLC.Banco
{
    /// <summary>
    /// Gerenciador de conexões MySQL para ambientes local e remoto
    /// </summary>
    public class GerenciadorConexaoMySQL
    {
        public enum Ambiente
        {
            LOCAL = 1,
            REMOTO = 2
        }

        private string _stringConexao;
        public readonly Ambiente _ambiente;

        public GerenciadorConexaoMySQL(Ambiente ambiente = Ambiente.REMOTO)
        {
            _ambiente = ambiente;
        }

        /// <summary>
        /// Obtém a string de conexão configurada para o ambiente atual 
        /// Aqui também é feita a criptografia automática da string de conexão
        /// </summary>
        private string ObterStringConexao()
        {
            string nomeConexao = (_ambiente == Ambiente.LOCAL) ? "L_WEDLC" : "R_WEDLC";

            var config = ConfigurationManager.ConnectionStrings[nomeConexao];

            if (config == null)
                throw new ConfigurationErrorsException($"String de conexão '{nomeConexao}' não configurada");

            string csValue = config.ConnectionString;

            if (string.IsNullOrEmpty(csValue))
                throw new ConfigurationErrorsException($"String de conexão '{nomeConexao}' vazia");

            try
            {
                // Tenta interpretar como Base64 (assumindo que já está criptografada)
                byte[] encryptedBytes = Convert.FromBase64String(csValue);
                // Se der certo, descriptografa
                return CryptoHelper.Decrypt(encryptedBytes);
            }
            catch (FormatException)
            {
                // Se não for Base64, significa que ainda está em texto puro
                string decrypted = csValue; // já é o texto original

                // Criptografa e salva de volta no App.config
                try
                {
                    string encrypted = Convert.ToBase64String(CryptoHelper.Encrypt(decrypted));
                    Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configFile.ConnectionStrings.ConnectionStrings[nomeConexao].ConnectionString = encrypted;
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("connectionStrings");

                    Console.WriteLine($"✅ Connection string '{nomeConexao}' criptografada automaticamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Falha ao criptografar connection string '{nomeConexao}': {ex.Message}");
                }

                // Retorna o valor original para uso imediato
                return decrypted;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException($"Erro ao processar a string de conexão '{nomeConexao}': {ex.Message}");
            }
        }

        /// <summary>
        /// Cria e retorna uma nova conexão MySQL
        /// </summary>
        public MySqlConnection CriarConexao()
        {
            _stringConexao = ObterStringConexao();
            return new MySqlConnection(_stringConexao);
        }
    }
}