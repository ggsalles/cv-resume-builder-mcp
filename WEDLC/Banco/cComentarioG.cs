using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

namespace WEDLC.Banco
{
    public class cComentarioG
    {

        [DisplayName("ID Resultado")]
        public Int32 IdResultado { get; set; }

        [DisplayName("ID Paciente")]
        public Int32 IdPaciente { get; set; }

        [DisplayName("ID Folha")]
        public Int32 IdFolha { get; set; }

        [DisplayName("ID Resultado G")]
        public Int32 IdResultadoG { get; set; }
        public Int32 IdComentario { get; set; }
        public Int32 IdResultadoComentarioG { get; set; }
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
        public DataTable BuscaResultadoG()
        {
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadog", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na BuscaResultadoComentarioG: {ex.Message}");
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
        public DataTable BuscaResultadoComentarioG()
        {
            if (IdResultado < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadocomentariog", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadocomentariog: {ex.Message}");
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
        public bool AtualizarResultadoComentarioG()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadocomentariog", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    cmd.Parameters.AddWithValue("pIdresultadocomentariog", IdResultadoComentarioG);
                    cmd.Parameters.AddWithValue("pIdresultado", IdResultado);
                    cmd.Parameters.AddWithValue("pIdcomentario", (IdComentario == 0) ? DBNull.Value : (object)IdComentario);
                    cmd.Parameters.AddWithValue("pTexto", Texto ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar resultado comentário G: {ex.Message}");
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

