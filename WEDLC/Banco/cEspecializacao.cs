using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cEspecializacao
    {
        public int TipoPesquisa { get; set; }
        public int IdEspecializacao { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

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

        public DataTable buscaEspecializacao()

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
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscaespecializacao", conexao)
)
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdEspecializacao", IdEspecializacao);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", Sigla);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome);
                    //sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);

                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);

                    //Fecha a conexão
                    conexao.Close();

                    // Retorna o DataTable
                    return dt;
                }

            }
            catch (System.Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return null;
            }
        }

        public bool incluiEspecialidade()
        {
            // Validação de entrada
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Sigla))
            {
                MessageBox.Show("Nome e sigla são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!conectaBanco())
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (var command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_incluiespecializacao";

                    command.Parameters.AddWithValue("pNome", Nome);
                    command.Parameters.AddWithValue("pSigla", Sigla);

                    int rowsAffected = command.ExecuteNonQuery();
                    conexao.Close();

                    return rowsAffected > 0; // Retorna true se pelo menos uma linha foi afetada
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao?.Close(); // Fecha a conexão se existir
                return false;
            }
        }
        public bool atualizaEspecializacao()
        {
            // Validação de entrada
            if (IdEspecializacao <= 0 || string.IsNullOrEmpty(Sigla) || string.IsNullOrEmpty(Nome))
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
                    command.CommandText = "pr_atualizaespecializacao";

                    command.Parameters.AddWithValue("pIdEspecializacao", IdEspecializacao);
                    command.Parameters.AddWithValue("pSigla", Sigla);
                    command.Parameters.AddWithValue("pNome", Nome);

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

        public bool excluiEspecializacao()
        {
            // Validação de entrada
            if (IdEspecializacao <= 0)
            {
                MessageBox.Show("ID de especialização inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!conectaBanco())
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (var command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_excluiespecializacao";

                    command.Parameters.AddWithValue("pIdEspecializacao", IdEspecializacao);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    conexao.Close();
                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao?.Close();
                return false;
            }
        }
    }
}
