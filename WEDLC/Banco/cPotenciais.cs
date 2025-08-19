using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cPotenciais
    {
        public int TipoPesquisa { get; set; }
        public Int32 IdPotenciaisUnidade { get; set; }
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

        public DataTable BuscaPotenciaisUnidade()
        {
            // Validação básica dos parâmetros
            if (TipoPesquisa < 0)
                return null;

            if (TipoPesquisa == 1 && IdPotenciaisUnidade <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscapotenciaisunidade", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPotenciaisUnidade", IdPotenciaisUnidade);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", Sigla ?? string.Empty);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome ?? string.Empty);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca da Unidade Potencial: {ex.Message}");
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

        public bool IncluiPotenciaisUnidade()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluipotenciaisunidade", conexao))
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
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir Potencial Unidade: {ex.Message}");
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

        public bool AtualizaPotenciaisUnidademotora()
        {
            // Validação de entrada
            if (IdPotenciaisUnidade <= 0 || string.IsNullOrEmpty(Sigla) || string.IsNullOrEmpty(Nome))
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
                    command.CommandText = "pr_atualizapotenciaisunidademotora";

                    command.Parameters.AddWithValue("pIdPotenciaisUnidade", IdPotenciaisUnidade);
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
