using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cNervo
    {
        private int _idnervo;
        private string _nome;
        private string _sigla;
        private string _normlmd;
        private string _normncm;
        private string _normncs;

        public int IdNervo   // property
        {
            get { return _idnervo; }   // get method
            set { _idnervo = value; }  // set method
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

        public string NormLmd   // property
        {
            get { return _normlmd; }   // get method
            set { _normlmd = value; }  // set method
        }

        public string NormNcm   // property
        {
            get { return _normncm; }   // get method
            set { _normncm = value; }  // set method
        }

        public string NormNcs   // property
        {
            get { return _normncs; }   // get method
            set { _normncs = value; }  // set method
        }

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

        public DataTable buscaNervo(int pTipopesquisa, int pIdNervo, string pSigla, string pNome)
        {
            // Validação básica dos parâmetros
            if (pTipopesquisa < 0)
                return null;

            if (pTipopesquisa == 1 && pIdNervo <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscanervo", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", pTipopesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdNervo", pIdNervo);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", pSigla ?? string.Empty);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome ?? string.Empty);

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
            if (string.IsNullOrWhiteSpace(_sigla) || string.IsNullOrWhiteSpace(_nome))
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
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = _sigla ?? string.Empty },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = _nome ?? string.Empty },
                new MySqlParameter("pNormLmd", MySqlDbType.VarChar) { Value = _normlmd ?? string.Empty },
                new MySqlParameter("pNormNcs", MySqlDbType.VarChar) { Value = _normncs ?? string.Empty },
                new MySqlParameter("pNormNcm", MySqlDbType.VarChar) { Value = _normncm ?? string.Empty }
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
            if (_idnervo <= 0 || string.IsNullOrWhiteSpace(_sigla) || string.IsNullOrWhiteSpace(_nome))
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
                new MySqlParameter("pIdNervo", MySqlDbType.Int32) { Value = _idnervo },
                new MySqlParameter("pSigla", MySqlDbType.VarChar) { Value = _sigla ?? string.Empty },
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = _nome ?? string.Empty },
                new MySqlParameter("pNormLmd", MySqlDbType.VarChar) { Value = _normlmd ?? string.Empty },
                new MySqlParameter("pNormNcm", MySqlDbType.VarChar) { Value = _normncm ?? string.Empty },
                new MySqlParameter("pNormNcs", MySqlDbType.VarChar) { Value = _normncs ?? string.Empty }
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
            if (_idnervo <= 0)
                return false;

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_excluinervo", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("pIdNervo", _idnervo);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451) // Código para FK violation
            {
                // Log específico para registro com relacionamentos
                Debug.WriteLine($"Não foi possível excluir: nervo possui relacionamentos. ID: {_idnervo}");
                return false;
            }
            catch (MySqlException ex)
            {
                // Log para outros erros MySQL
                Debug.WriteLine($"Erro MySQL ao excluir nervo ID {_idnervo}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para erros inesperados
                Debug.WriteLine($"Erro inesperado ao excluir nervo ID {_idnervo}: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
