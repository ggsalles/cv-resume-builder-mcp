using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cPotenciaisPESS
    {
        [DisplayName("ID Paciente")]
        public Int32 IdPaciente { get; set; }

        [DisplayName("idresultado")]
        public int? IdResultado { get; set; }

        [DisplayName("idfolha")]
        public int? IdFolha { get; set; }

        [DisplayName("idresultadopess")]
        public int? IdResultadoPess { get; set; }

        [DisplayName("idestudopotenevocado")]
        public int? IdEstudopotenevocado { get; set; }

        [DisplayName("p1n1ladodireitorespobt")]
        public string P1N1LadoDireitoRespObt { get; set; }

        [DisplayName("p1n1ladodireitorespoesp")]
        public string P1N1LadoDireitoRespOEsp { get; set; }

        [DisplayName("p1n1ladoesquerdorespobt")]
        public string P1N1LadoEsquerdoRespObt { get; set; }

        [DisplayName("p1n1ladoesquerdorespoesp")]
        public string P1N1LadoEsquerdoRespOEsp { get; set; }

        [DisplayName("diferencaobitida")]
        public string DiferencaObitida { get; set; }

        [DisplayName("diferencaesperada")]
        public string    DiferencaEsperada { get; set; }

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

        public DataTable BuscaResultadoPess()
        {
            if (IdPaciente < 0 && IdFolha < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopess", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);

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
        public bool AtualizarResultadoPESS()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoPess < 0)
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadopess", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    // Adiciona os parâmetros com tratamento de DBNull
                    cmd.Parameters.AddWithValue("p_idresultadopess", IdResultadoPess);
                    cmd.Parameters.AddWithValue("p_idresultado", IdResultado);
                    cmd.Parameters.AddWithValue("p_idfolha", IdFolha);
                    cmd.Parameters.AddWithValue("p_idestudopotenevocado", IdEstudopotenevocado);
                    cmd.Parameters.AddWithValue("p_p1n1ladodireitorespobt", P1N1LadoDireitoRespObt);
                    cmd.Parameters.AddWithValue("p_p1n1ladodireitorespoesp", P1N1LadoDireitoRespOEsp);
                    cmd.Parameters.AddWithValue("p_p1n1ladoesquerdorespobt", P1N1LadoEsquerdoRespObt);
                    cmd.Parameters.AddWithValue("p_p1n1ladoesquerdorespoesp", P1N1LadoEsquerdoRespOEsp);
                    cmd.Parameters.AddWithValue("p_diferencaobitida", DiferencaObitida);
                    cmd.Parameters.AddWithValue("p_diferencaesperada", DiferencaEsperada);
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
