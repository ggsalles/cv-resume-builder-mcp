using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cEspecializacao
    {
        private int _idespecializacao;
        private string _nome;
        private string _sigla;

        public int IdEspecializacao   // property
        {
            get { return _idespecializacao; }   // get method
            set { _idespecializacao = value; }  // set method
        }

        public string Nome   // property
        {
            get { return _nome; }   // get method
            set { _nome = value; }  // set method
        }

        public string Sigla   // property
        {
            get { return _sigla; }   // get method
            set { _sigla = value; }  // set method
        }

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

        public DataTable buscaEspecializacao(int pTipopesquisa, int pIdEspecializacao, string pSigla, string pNome)

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
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", pTipopesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdEspecializacao", pIdEspecializacao);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", pSigla);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
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
            if (string.IsNullOrEmpty(_nome) || string.IsNullOrEmpty(_sigla))
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

                    command.Parameters.AddWithValue("pNome", _nome);
                    command.Parameters.AddWithValue("pSigla", _sigla);

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
            if (_idespecializacao <= 0 || string.IsNullOrEmpty(_sigla) || string.IsNullOrEmpty(_nome))
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

                    command.Parameters.AddWithValue("pIdEspecializacao", _idespecializacao);
                    command.Parameters.AddWithValue("pSigla", _sigla);
                    command.Parameters.AddWithValue("pNome", _nome);

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
            if (_idespecializacao <= 0)
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

                    command.Parameters.AddWithValue("pIdEspecializacao", _idespecializacao);

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
