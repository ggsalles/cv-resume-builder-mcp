using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WEDLC.Banco
{
    public class cResultadoAtividadeInsercao
    {
        public Int32 IdPaciente { get; set; }
        public Int32 IdResultado { get; set; }
        public Int32 IdAtividadeInsercao { get; set; }
        public Int32 IdResultadoAtividadeInsercao { get; set; }
        public string Texto { get; set; }

        // Construtor
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

        public DataTable buscaResultadoAtividadeInsercao()
        {
            // Validação básica dos parâmetros

            if (IdResultado <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadoatividadeinsercao", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdResultado", IdResultado);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadoatividadeinsercao: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado na pr_buscaresultadoatividadeinsercao: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
