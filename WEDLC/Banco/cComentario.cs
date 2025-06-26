using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WEDLC.Banco
{
    public class cComentario
    {
        public int TipoPesquisa { get; set; }
        public Int32 IdComentario { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
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

        public DataTable buscaComentario()
        {
            // Validação básica dos parâmetros
            if (TipoPesquisa < 0)
                return null;

            if (TipoPesquisa == 1 && IdComentario <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscacomentario", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdComentario", IdComentario);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", Sigla ?? string.Empty);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome ?? string.Empty);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca do comentário: {ex.Message}");
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

        public bool incluiComentario()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluicomentario", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = Sigla ?? string.Empty },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome ?? string.Empty },
                new MySqlParameter("pTexto", MySqlDbType.VarChar) { Value = Texto?? string.Empty },
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Qualquer número positivo indica sucesso
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir nervo: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para outros tipos de erro
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
