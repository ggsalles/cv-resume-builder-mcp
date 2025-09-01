using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cPotenciaisPEV
    {
        [DisplayName("ID Folha")]
        public Int32 IdFolha { get; set; }

        [DisplayName("ID Resultado")]
        public Int32 IdResultado { get; set; }

        [DisplayName("ID Paciente")]
        public Int32 IdPaciente { get; set; }

        [DisplayName("ID Resultado PEV")]
        public Int32 IdResultadoPev { get; set; }

        [DisplayName("N75 Olho Direito")]
        public string N75OlhoDireito { get; set; }

        [DisplayName("N75 Olho Esquerdo")]
        public string N75OlhoEsquerdo { get; set; }

        [DisplayName("P100 Olho Direito")]
        public string P100OlhoDireito { get; set; }

        [DisplayName("P100 Olho Esquerdo")]
        public string P100OlhoEsquerdo { get; set; }

        [DisplayName("P100 Diferença")]
        public string P100Diferenca { get; set; }

        [DisplayName("N145 Olho Direito")]
        public string N145OlhoDireito { get; set; }

        [DisplayName("N145 Olho Esquerdo")]
        public string N145OlhoEsquerdo { get; set; }

        [DisplayName("Amplitude Olho Direito")]
        public string AmplitudeOlhoDireito { get; set; }

        [DisplayName("Amplitude Olho Esquerdo")]
        public string AmplitudeOlhoEsquerdo { get; set; }

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

        public DataTable BuscaResultadoPev()
        {
            if (IdResultado < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopev", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdResultado", IdResultado);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na BuscaResultadoPev: {ex.Message}");
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

        public DataTable BuscaResultadoComentarioPev()
        {
            if (IdResultado < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadocomentariopev", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadocomentariopev: {ex.Message}");
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
