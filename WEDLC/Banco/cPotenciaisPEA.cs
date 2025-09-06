using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cPotenciaisPEA
    {
        [DisplayName("ID Paciente")]
        public Int32 IdPaciente { get; set; }

        [DisplayName("idresultado")]
        public Int32 IdResultado { get; set; }

        [DisplayName("idresultadopea")]
        public Int32 IdResultadoPea { get; set; }

        [DisplayName("onda1ouvidodireito")]
        public string Onda1OuvidoDireito { get; set; }

        [DisplayName("onda1ouvidoesquerdo")]
        public string Onda1OuvidoEsquerdo { get; set; }

        [DisplayName("onda2ouvidodireito")]
        public string Onda2OuvidoDireito { get; set; }

        [DisplayName("onda2ouvidoesquerdo")]
        public string Onda2OuvidoEsquerdo { get; set; }

        [DisplayName("onda3ouvidodireito")]
        public string Onda3OuvidoDireito { get; set; }

        [DisplayName("onda3ouvidoesquerdo")]
        public string Onda3OuvidoEsquerdo { get; set; }

        [DisplayName("onda4vouvidodireito")]
        public string Onda4OuvidoDireito { get; set; }

        [DisplayName("onda4ouvidoesquerdo")]
        public string Onda4OuvidoEsquerdo { get; set; }

        [DisplayName("onda5ouvidodireito")]
        public string Onda5OuvidoDireito { get; set; }

        [DisplayName("onda5ouvidoesquerdo")]
        public string Onda5OuvidoEsquerdo { get; set; }

        [DisplayName("1a3direito")]
        public string Intervalo1a3Direito { get; set; }

        [DisplayName("1a3esquerdo")]
        public string Intervalo1a3Esquerdo { get; set; }

        [DisplayName("3a5direito")]
        public string Intervalo3a5Direito { get; set; }

        [DisplayName("3a5esquerdo")]
        public string Intervalo3a5Esquerdo { get; set; }

        [DisplayName("1a4direito")]
        public string Intervalo1a4Direito { get; set; }

        [DisplayName("1a4esquerdo")]
        public string Intervalo1a4Esquerdo { get; set; }
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

        public DataTable BuscaResultadoPea()
        {
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopea", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //sqlDa.SelectCommand.Parameters.AddWithValue("pIdResultado", IdResultado);
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

        public bool AtualizarResultadoPEA()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoPea < 0 || IdResultado < 0)
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadopea", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    // Adiciona os parâmetros com tratamento de DBNull
                    cmd.Parameters.AddWithValue("p_idresultadopea", IdResultadoPea);
                    cmd.Parameters.AddWithValue("p_idresultado", IdResultado);
                    cmd.Parameters.AddWithValue("p_onda1ouvidodireito", !string.IsNullOrEmpty(Onda1OuvidoDireito) ? Onda1OuvidoDireito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda1ouvidoesquerdo", !string.IsNullOrEmpty(Onda1OuvidoEsquerdo) ? Onda1OuvidoEsquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda2ouvidodireito", !string.IsNullOrEmpty(Onda2OuvidoDireito) ? Onda2OuvidoDireito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda2ouvidoesquerdo", !string.IsNullOrEmpty(Onda2OuvidoEsquerdo) ? Onda2OuvidoEsquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda3ouvidodireito", !string.IsNullOrEmpty(Onda3OuvidoDireito) ? Onda3OuvidoDireito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda3ouvidoesquerdo", !string.IsNullOrEmpty(Onda3OuvidoEsquerdo) ? Onda3OuvidoEsquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda4vouvidodireito", !string.IsNullOrEmpty(Onda4OuvidoDireito) ? Onda4OuvidoDireito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda4ouvidoesquerdo", !string.IsNullOrEmpty(Onda4OuvidoEsquerdo) ? Onda4OuvidoEsquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda5ouvidodireito", !string.IsNullOrEmpty(Onda5OuvidoDireito) ? Onda5OuvidoDireito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_onda5ouvidoesquerdo", !string.IsNullOrEmpty(Onda5OuvidoEsquerdo) ? Onda5OuvidoEsquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_1a3direito", !string.IsNullOrEmpty(Intervalo1a3Direito) ? Intervalo1a3Direito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_1a3esquerdo", !string.IsNullOrEmpty(Intervalo1a3Esquerdo) ? Intervalo1a3Esquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_3a5direito", !string.IsNullOrEmpty(Intervalo3a5Direito) ? Intervalo3a5Direito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_3a5esquerdo", !string.IsNullOrEmpty(Intervalo3a5Esquerdo) ? Intervalo3a5Esquerdo : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_1a4direito", !string.IsNullOrEmpty(Intervalo1a4Direito) ? Intervalo1a4Direito : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_1a4esquerdo", !string.IsNullOrEmpty(Intervalo1a4Esquerdo) ? Intervalo1a4Esquerdo : (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pr_atualizaresultadopea: {ex.Message}");
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
