using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cAtividadeInsercao
    {
        public int TipoPesquisa { get; set; }
        public Int32 IdAatividadeInsercao { get; set; }
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

        public DataTable buscaAtividadeinsercao()
        {
            // Validação básica dos parâmetros
            if (TipoPesquisa < 0)
                return null;

            if (TipoPesquisa == 1 && IdAatividadeInsercao < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaatividadeinsercao", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdAtividadeInsercao", IdAatividadeInsercao);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", Sigla ?? string.Empty);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome ?? string.Empty);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca da Atividade de Inserção: {ex.Message}");
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

        public bool IncluiAtividaDeInsercao()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluiatividadeinsercao", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir a Atividade de Inserção: {ex.Message}");
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

        public bool atualizaAtividaDeInsercao()
        {
            // Validação de entrada
            if (IdAatividadeInsercao <= 0 || string.IsNullOrEmpty(Sigla) || string.IsNullOrEmpty(Nome))
            {
                MessageBox.Show("ID, sigla e nome são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!conectaBanco())
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_atualizaatividadeinsercao";

                    command.Parameters.AddWithValue("pIdAtividadeInsercao", IdAatividadeInsercao);
                    command.Parameters.AddWithValue("pSigla", Sigla);
                    command.Parameters.AddWithValue("pNome", Nome);
                    command.Parameters.AddWithValue("pTexto", Texto);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    conexao.Close();
                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao?.Close();
                return false;
            }
        }
    }
}
