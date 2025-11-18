using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cAVJ
    {

        [DisplayName("ID Resultado")]
        public Int32 IdResultado { get; set; }

        [DisplayName("ID Paciente")]
        public Int32 IdPaciente { get; set; }

        [DisplayName("ID Folha")]
        public Int32 IdFolha { get; set; }

        [DisplayName("ID Resultado AVJ")]
        public Int32 IdResultadoAVJ { get; set; }

        [DisplayName("Estimulacao")]
        public string Estimulacao { get; set; }

        [DisplayName("Nervo")]
        public string Nervo { get; set; }

        [DisplayName("Musculo")]
        public string Musculo { get; set; }

        [DisplayName("Antes do Exercicio")]
        public string AntesExercicio { get; set; }

        [DisplayName("Logo Apos Exercicio")]
        public string LogoAposExercicio { get; set; }

        [DisplayName("Tres Minutos Apos Exercicio")]
        public string TresMinutosApos { get; set; }
        public Int32 IdComentario { get; set; }
        public Int32 IdResultadoComentario { get; set; }
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

        public DataTable BuscaResultadoAVJ()
        {
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadoavj", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na BuscaResultadoAVJ: {ex.Message}");
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

        public bool AtualizarResultadoAVJ()
        {
            if (!conectaBanco())
                return false;

            if (IdResultadoAVJ < 0 || IdResultado < 0)
            {
                return false;
            }
            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadoavj", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    cmd.Parameters.AddWithValue("pidresultadoavj", IdResultadoAVJ);
                    cmd.Parameters.AddWithValue("pidresultado", IdResultado);
                    cmd.Parameters.AddWithValue("pestimulacao", Estimulacao ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pnervo", Nervo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pmusculo", Musculo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("pantesdoexercicio", AntesExercicio ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("plogoaposexercicio", LogoAposExercicio ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ptresminutosapos", TresMinutosApos ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pr_atualizaresultadoavj: {ex.Message}");
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

        public DataTable BuscaResultadoComentarioAVJ()
        {
            if (IdResultado < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadocomentarioavj", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadocomentarioavj: {ex.Message}");
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

        public bool AtualizarResultadoComentarioAVJ()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadocomentarioavj", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    cmd.Parameters.AddWithValue("pIdresultadocomentarioavj", IdResultadoComentario);
                    cmd.Parameters.AddWithValue("pIdresultado", IdResultado);
                    cmd.Parameters.AddWithValue("pIdcomentario", (IdComentario == 0) ? DBNull.Value : (object)IdComentario);
                    cmd.Parameters.AddWithValue("pTexto", Texto ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar resultado comentário AVJ: {ex.Message}");
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

