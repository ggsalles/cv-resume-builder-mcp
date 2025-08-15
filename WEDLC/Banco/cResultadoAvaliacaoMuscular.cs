using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cResultadoAvaliacaoMuscular
    {
        public Int32 IdFolha { get; set; }
        public Int32 IdResultado { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public string Lado { get; set; }

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

        public DataTable buscaResultadoAvaliacaoMuscular()
        {
            // Validação básica dos parâmetros

            if (IdFolha <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadoavaliacaomuscular", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca do convenio: {ex.Message}");
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

        public DataTable buscaResultadoNeuroCondMotora()
        {
            // Validação básica dos parâmetros

            if (IdFolha <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadoneurocondmotora", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca do convenio: {ex.Message}");
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
}
