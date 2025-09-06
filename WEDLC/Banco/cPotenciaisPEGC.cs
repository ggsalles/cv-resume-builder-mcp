using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cPotenciaisPEGC
    {
        [DisplayName("ID Paciente")]
        [Browsable(false)] // Oculta no DataGridView
        public Int32 IdPaciente { get; set; }

        [DisplayName("ID Resultado")]
        [Browsable(false)] // Oculta no DataGridView
        public Int32 IdResultado { get; set; }

        [DisplayName("ID Resultado PEGC")]
        [Browsable(false)] // Oculta no DataGridView
        public Int32 IdResultadoPegc { get; set; }

        [DisplayName("P1 Início Obtido")]
        [Category("Potenciais")]
        public string P1InicioValObtido { get; set; } = string.Empty;

        [DisplayName("P1 Pico Obtido")]
        [Category("Potenciais")]
        public string P1PicoValObtido { get; set; } = string.Empty;

        [DisplayName("N1 Pico Obtido")]
        [Category("Potenciais")]
        public string N1PicoValObtido { get; set; } = string.Empty;

        [DisplayName("P2 Pico Obtido")]
        [Category("Potenciais")]
        public string P2PicoValObtido { get; set; } = string.Empty;

        [DisplayName("N2 Pico Obtido")]
        [Category("Potenciais")]
        public string N2PicoValObtido { get; set; } = string.Empty;

        [DisplayName("P3 Pico Obtido")]
        [Category("Potenciais")]
        public string P3PicoValObtido { get; set; } = string.Empty;

        [DisplayName("N3 Pico Obtido")]
        [Category("Potenciais")]

        public string N3PicoValObtido { get; set; } = string.Empty;
        [DisplayName("Resposta Espinhal")]
        [Category("Resultados")]
        public string RespostaEspinhal { get; set; } = string.Empty;

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

        public DataTable BuscaResultadoPegc()
        {
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopegc", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na BuscaResultadoPea: {ex.Message}");
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

        public bool AtualizarResultadoPegc()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoPegc < 0 || IdResultado < 0)
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadopegc", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionar parâmetros
                    cmd.Parameters.AddWithValue("p_idresultadopegc", IdResultadoPegc);
                    cmd.Parameters.AddWithValue("p_idresultado", IdResultado);
                    cmd.Parameters.AddWithValue("p_p1iniciovalobtido",
                        string.IsNullOrEmpty(P1InicioValObtido) ? DBNull.Value : (object)P1InicioValObtido);
                    cmd.Parameters.AddWithValue("p_p1picovalobtido",
                        string.IsNullOrEmpty(P1PicoValObtido) ? DBNull.Value : (object)P1PicoValObtido);
                    cmd.Parameters.AddWithValue("p_n1picovalobtido",
                        string.IsNullOrEmpty(N1PicoValObtido) ? DBNull.Value : (object)N1PicoValObtido);
                    cmd.Parameters.AddWithValue("p_p2picovalobtido",
                        string.IsNullOrEmpty(P2PicoValObtido) ? DBNull.Value : (object)P2PicoValObtido);
                    cmd.Parameters.AddWithValue("p_n2picovalobtido",
                        string.IsNullOrEmpty(N2PicoValObtido) ? DBNull.Value : (object)N2PicoValObtido);
                    cmd.Parameters.AddWithValue("p_p3picovalobtido",
                        string.IsNullOrEmpty(P3PicoValObtido) ? DBNull.Value : (object)P3PicoValObtido);
                    cmd.Parameters.AddWithValue("p_n3picovalobtido",
                        string.IsNullOrEmpty(N3PicoValObtido) ? DBNull.Value : (object)N3PicoValObtido);
                    cmd.Parameters.AddWithValue("p_respostaespinhal",
                        string.IsNullOrEmpty(RespostaEspinhal) ? DBNull.Value : (object)RespostaEspinhal);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pr_atualizaresultadopegc: {ex.Message}");
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
