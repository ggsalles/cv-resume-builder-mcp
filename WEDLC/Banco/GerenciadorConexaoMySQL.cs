using MySql.Data.MySqlClient;
using System.Configuration;

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

        public GerenciadorConexaoMySQL(Ambiente ambiente = Ambiente.LOCAL)
        {
            _ambiente = ambiente;
        }

        /// <summary>
        /// Obtém a string de conexão configurada para o ambiente atual
        /// </summary>
        private string ObterStringConexao()
        {
            var config = ConfigurationManager.ConnectionStrings;

            if (_ambiente == Ambiente.LOCAL)
            {
                if (config["L_WEDLC"] == null)
                    throw new ConfigurationErrorsException("String de conexão LOCAL não configurada");

                return config["L_WEDLC"].ConnectionString;
            }

            if (config["R_WEDLC"] == null)
                throw new ConfigurationErrorsException("String de conexão REMOTA não configurada");

            return config["R_WEDLC"].ConnectionString;
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