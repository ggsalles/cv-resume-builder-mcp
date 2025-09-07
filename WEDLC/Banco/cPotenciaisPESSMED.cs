using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cPotenciaisPESSMED
    {
        [DisplayName("ID Paciente")]
        [Browsable(false)] // Oculta no DataGridView
        public Int32 IdPaciente { get; set; }

        [DisplayName("ID Resultado")]
        [Browsable(false)] // Oculta no DataGridView
        public Int32 IdResultado { get; set; }

        [DisplayName("ID Resultado PessMed")]
        [Browsable(false)]
        public Int32 IdResultadoPessMed { get; set; }

        [DisplayName("ERBS Direito")]
        public string ErbsDireito { get; set; }

        [DisplayName("ERBS Esquerdo")]
        public string ErbsEsquerdo { get; set; }

        [DisplayName("N11N13 Direito")]
        public string N11N13Direito { get; set; }

        [DisplayName("N11N13 Esquerdo")]
        public string N11N13Esquerdo { get; set; }

        [DisplayName("N19 Direito")]
        public string N19Direito { get; set; }

        [DisplayName("N19 Esquerdo")]
        public string N19Esquerdo { get; set; }

        [DisplayName("P22 Direito")]
        public string P22Direito { get; set; }

        [DisplayName("P22 Esquerdo")]
        public string P22Esquerdo { get; set; }

        [DisplayName("EP N11N13 Direito")]
        public string EpN11N13Direito { get; set; }

        [DisplayName("EP N11N13 Esquerdo")]
        public string EpN11N13Esquerdo { get; set; }

        [DisplayName("EP N19 Direito")]
        public string EpN19Direito { get; set; }

        [DisplayName("EP N19 Esquerdo")]
        public string EpN19Esquerdo { get; set; }

        [DisplayName("EP P22 Direito")]
        public string EpP22Direito { get; set; }

        [DisplayName("EP P22 Esquerdo")]
        public string EpP22Esquerdo { get; set; }

        [DisplayName("N11N13N19 Direito")]
        public string N11N13N19Direito { get; set; }

        [DisplayName("N11N13N19 Esquerdo")]
        public string N11N13N19Esquerdo { get; set; }

        public cComentario objComentario { get; set; } = new cComentario();

        public cPotenciaisEvocadosTecnica objTecnica { get; set; } = new cPotenciaisEvocadosTecnica();

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

            try
            {
                objTecnica.IdPaciente = this.IdPaciente;
                DataTable dt = objTecnica.BuscaResultadoPotEvocadoTecnica();
                return dt;
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

        public DataTable BuscaResultadoPessMed()
        {
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopessmed", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadopessmed: {ex.Message}");
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

        public bool AtualizarResultadoPessMed()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoPessMed < 0 || IdResultado < 0)
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadopessmed", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionar parâmetros
                    cmd.Parameters.AddWithValue("p_idresultadopessmed", IdResultadoPessMed);
                    cmd.Parameters.AddWithValue("p_idresultado", IdResultado);
                    cmd.Parameters.AddWithValue("p_erbsdireito", ErbsDireito ?? "");
                    cmd.Parameters.AddWithValue("p_erbsesquerdo", ErbsEsquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_n11n13direito", N11N13Direito ?? "");
                    cmd.Parameters.AddWithValue("p_n11n13esquerdo", N11N13Esquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_n19direito", N19Direito ?? "");
                    cmd.Parameters.AddWithValue("p_n19esquerdo", N19Esquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_p22direito", P22Direito ?? "");
                    cmd.Parameters.AddWithValue("p_p22esquerdo", P22Esquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_epn11n13direito", EpN11N13Direito ?? "");
                    cmd.Parameters.AddWithValue("p_epn11n13esquerdo", EpN11N13Esquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_epn19direito", EpN19Direito ?? "");
                    cmd.Parameters.AddWithValue("p_epn19esquerdo", EpN19Esquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_epp22direito", EpP22Direito ?? "");
                    cmd.Parameters.AddWithValue("p_epp22esquerdo", EpP22Esquerdo ?? "");
                    cmd.Parameters.AddWithValue("p_n11n13n19direito", N11N13N19Direito ?? "");
                    cmd.Parameters.AddWithValue("p_n11n13n19esquerdo", N11N13N19Esquerdo ?? "");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pr_atualizaresultadopessmed: {ex.Message}");
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
