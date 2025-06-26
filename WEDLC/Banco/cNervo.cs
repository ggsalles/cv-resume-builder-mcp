using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cNervo
    {
        public int TipoPesquisa { get; set; }
        public int IdNervo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string NormLmd { get; set; }
        public string NormNcm { get; set; }
        public string NormNcs { get; set; }

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

        public DataTable buscaNervo()
        {
            // Validação básica dos parâmetros
            if (TipoPesquisa < 0)
                return null;

            if (TipoPesquisa == 1 && IdNervo <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscanervo", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdNervo", IdNervo);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", Sigla ?? string.Empty);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome ?? string.Empty);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca de nervo: {ex.Message}");
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

        public bool incluiNervo()
        {
            // Validação básica dos dados
            if (string.IsNullOrWhiteSpace(Sigla) || string.IsNullOrWhiteSpace(Nome))
                return false;

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluinervo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = Sigla ?? string.Empty },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome ?? string.Empty },
                new MySqlParameter("pNormLmd", MySqlDbType.VarChar) { Value = NormLmd?? string.Empty },
                new MySqlParameter("pNormNcs", MySqlDbType.VarChar) { Value = NormNcs ?? string.Empty },
                new MySqlParameter("pNormNcm", MySqlDbType.VarChar) { Value = NormNcm ?? string.Empty }
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
        public bool AtualizaNervo()
        {
            // Validação básica dos dados
            if (IdNervo <= 0 || string.IsNullOrWhiteSpace(Sigla) || string.IsNullOrWhiteSpace(Nome))
                return false;

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_atualizanervo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdNervo", MySqlDbType.Int32) { Value = IdNervo },
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = Sigla ?? string.Empty },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome ?? string.Empty },
                new MySqlParameter("pNormLmd", MySqlDbType.VarChar) { Value = NormLmd ?? string.Empty },
                new MySqlParameter("pNormNcm", MySqlDbType.VarChar) { Value = NormNcm ?? string.Empty },
                new MySqlParameter("pNormNcs", MySqlDbType.VarChar) { Value = NormNcs ?? string.Empty }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro MySQL ao atualizar nervo: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado ao atualizar nervo: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool ExcluiNervo()
        {
            // Validação básica
            if (IdNervo <= 0)
                return false;

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_excluinervo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("pIdNervo", IdNervo);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451) // Código para FK violation
            {
                // Log específico para registro com relacionamentos
                Debug.WriteLine($"Não foi possível excluir: nervo possui relacionamentos. ID: {IdNervo}");
                return false;
            }
            catch (MySqlException ex)
            {
                // Log para outros erros MySQL
                Debug.WriteLine($"Erro MySQL ao excluir nervo ID {IdNervo}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para erros inesperados
                Debug.WriteLine($"Erro inesperado ao excluir nervo ID {IdNervo}: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
