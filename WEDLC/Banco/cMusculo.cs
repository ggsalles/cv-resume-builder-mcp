using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace WEDLC.Banco
{
    public class cMusculo
    {
        public int TipoPesquisa { get; set; }
        public int IdMusculo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Raizes { get; set; }
        public string Inervacao { get; set; }

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

        public DataTable buscaMusculo()
        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null; // Fix: Return null instead of a boolean to match the DataTable return type  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Fix: Return null instead of a boolean to match the DataTable return type  
            }

            try
            {
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscamusculo", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdMusculo", IdMusculo);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", Sigla);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pRaizes", Raizes);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pInervacao", Inervacao);
                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);

                    // Fecha a conexão  
                    conexao.Close();

                    // Retorna o DataTable  
                    return dt;
                }
            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return null;
            }

        }

        public bool incluiMusculo()
        {
            if (!conectaBanco())
            {
                // Melhor lançar exceção ou retornar false e tratar a mensagem em outra camada
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_incluimusculo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = Sigla },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome },
                new MySqlParameter("pRaizes", MySqlDbType.VarChar) { Value = Raizes },
                new MySqlParameter("pInervacao", MySqlDbType.VarChar) { Value = Inervacao }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro ex.Message para diagnóstico
                MessageBox.Show($"Erro ao incluir músculo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexao != null && conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
            }
        }

        public bool atualizamusculo()
        {
            // Validação básica dos dados
            if (IdMusculo <= 0)
            {
                // Id inválido
                return false;
            }

            if (!conectaBanco())
            {
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_atualizamusculo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdMusculo", MySqlDbType.Int32) { Value = IdMusculo },
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = Sigla ?? string.Empty },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome ?? string.Empty },
                new MySqlParameter("pRaizes", MySqlDbType.VarChar) { Value = Raizes ?? string.Empty },
                new MySqlParameter("pInervacao", MySqlDbType.VarChar) { Value = Inervacao ?? string.Empty }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro (ex.Message, ex.StackTrace) para diagnóstico
                MessageBox.Show($"Erro ao atualizar músculo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexao?.State == ConnectionState.Open)
                    conexao.Close();
            }
        }

        public bool excluiMusculo()
        {
            // Validação inicial
            if (IdMusculo <= 0)
                return false;

            // Conexão
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_excluimusculo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("pIdMusculo", IdMusculo);

                    // Qualquer número positivo indica sucesso
                    bool sucesso = command.ExecuteNonQuery() > 0;
                    return sucesso;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para erros de banco
                Console.Error.WriteLine($"Erro ao excluir músculo: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para outros tipos de erro
                Console.Error.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
