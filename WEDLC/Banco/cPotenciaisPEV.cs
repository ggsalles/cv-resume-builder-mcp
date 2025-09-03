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

        public bool AtualizarResultadoPEV()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoPev < 0 || IdResultado < 0)
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadopev", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    cmd.Parameters.AddWithValue("pidresultadopev", IdResultadoPev);
                    cmd.Parameters.AddWithValue("pidresultado", IdResultado);
                    cmd.Parameters.AddWithValue("pn75olhodireito", N75OlhoDireito ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pn75olhoesquerdo", N75OlhoEsquerdo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pp100olhodireito", P100OlhoDireito ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pp100olhoesquerdo", P100OlhoEsquerdo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pp100diferenca", P100Diferenca ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pn145olhodireito", N145OlhoDireito ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pn145olhoesquerdo", N145OlhoEsquerdo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pamplitudeolhodireito", AmplitudeOlhoDireito ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pamplitudeolhoesquerdo", AmplitudeOlhoEsquerdo ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pr_atualizaresultadopev: {ex.Message}");
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
