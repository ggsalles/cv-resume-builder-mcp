using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cResultadoComentario
    {
        public Int32 IdPaciente { get; set; }
        public Int32 IdResultado { get; set; }
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

        public DataTable buscaResultadoComentario()
        {
            // Validação básica dos parâmetros

            if (IdResultado <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadocomentario", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadocomentario: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado na pr_buscaresultadocomentario: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool gravaResultadoComentario()
        {
            if (!conectaBanco())
            {
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_incluiresultadocomentario", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdComentario", MySqlDbType.Int32) { Value = IdComentario },
                new MySqlParameter("pIdResultado", MySqlDbType.Int32) { Value = IdResultado},
                new MySqlParameter("pTexto", MySqlDbType.VarChar) { Value = Texto ?? string.Empty },
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected >= 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro (ex.Message, ex.StackTrace) para diagnóstico
                MessageBox.Show($"Erro ao incluir/atualizar pr_incluiresultadocomentario: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexao?.State == ConnectionState.Open)
                    conexao.Close();
            }
        }

        public bool AtualizarResultadoComentarioPEV()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var cmd = new MySqlCommand("pr_atualizaresultadocomentariopev", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adicionando os parâmetros exatamente como na procedure
                    cmd.Parameters.AddWithValue("pIdresultadocomentariopev", IdResultadoComentario);
                    cmd.Parameters.AddWithValue("pIdresultado", IdResultado);
                    cmd.Parameters.AddWithValue("pIdcomentario", (IdComentario == 0) ? DBNull.Value : (object)IdComentario);
                    cmd.Parameters.AddWithValue("pTexto", Texto ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar resultado comentário PEV: {ex.Message}");
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
