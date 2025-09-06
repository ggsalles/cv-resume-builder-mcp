using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WEDLC.Banco
{
    public class cPotenciaisEvocadosTecnica
    {
        public Int32 IdPaciente { get; set; }
        public Int32 IdResultadoPotEvocTecnica { get; set; }
        public Int32 IdResultado { get; set; }
        public string Captacao { get; set; }
        public string Sensibilidade { get; set; }
        public int? UvDivTempo { get; set; }
        public string Filtros { get; set; }
        public string Estimulacao { get; set; }
        public int? FreqEstimada { get; set; }
        public int? NEstimulo { get; set; }
        public string Intensidade { get; set; }

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
        public DataTable BuscaResultadoPotEvocadoTecnica()
        {
            if (IdResultado < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopotevocadotecninca", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadopotevocadotecninca: {ex.Message}");
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

        public bool AtualizaResultadoPotEvocadoTecnica()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoPotEvocTecnica <= 0 )
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadopotevocadotecninca", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros
                    cmd.Parameters.AddWithValue("pIdresultadopotevoctecnica", IdResultadoPotEvocTecnica);
                    cmd.Parameters.AddWithValue("pIdresultado", IdResultado);
                    cmd.Parameters.AddWithValue("pCaptacao", Captacao ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pSensibilidade", Sensibilidade ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pUvdivtempo", UvDivTempo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pFiltros", Filtros ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pEstimulacao", Estimulacao ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pFreqestimada", FreqEstimada ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pNestimulo", NEstimulo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pIntensidade", Intensidade ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pr_atualizaresultadopotevocadotecnica: {ex.Message}");
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
    }
}
