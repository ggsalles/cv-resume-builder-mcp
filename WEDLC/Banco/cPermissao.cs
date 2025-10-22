using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static WEDLC.Forms.frmLogin;

namespace WEDLC.Banco
{
    public class cPermissao
    {

        [DisplayName("ID Permissao")]
        public Int32 IdPermissao { get; set; }

        [DisplayName("ID Modulo")]
        public Int32 IdModulo { get; set; }

        [DisplayName("ID Usuario")]
        public Int32 IdUsuario { get; set; }

        [DisplayName("Nome")]
        public String Nome { get; set; }

        [DisplayName("ID Nivel")]
        public Int32 IdNivel { get; set; }

        // Construtor
        GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();
        MySqlConnection conexao = new MySqlConnection();

        // Permissões globais (nível do usuário)
        public static bool PodeAcessar => Sessao.Nivel != NivelAcesso.NIVEL4_SEMACESSO;
        public static bool PodeAdministrar => Sessao.Nivel == NivelAcesso.NIVEL1_ADM;
        public static bool PodeGravar => Sessao.Nivel == NivelAcesso.NIVEL1_ADM || Sessao.Nivel == NivelAcesso.NIVEL2_USUCOMPLETO;
        public static bool PodeLer => Sessao.Nivel != NivelAcesso.NIVEL4_SEMACESSO;

        // Permissões específicas por módulo (opcional)
        public static bool PodeGravarModulo(int idModulo)
        {
            var perm = Sessao.Permissoes.FirstOrDefault(p => p.IdModulo == idModulo);
            return perm != null && (perm.Nivel == NivelAcesso.NIVEL1_ADM || perm.Nivel == NivelAcesso.NIVEL2_USUCOMPLETO);
        }

        public static bool PodeAcessarModulo(int idModulo)
        {
            var perm = Sessao.Permissoes.FirstOrDefault(p => p.IdModulo == idModulo);
            return perm != null && (perm.Nivel != NivelAcesso.NIVEL4_SEMACESSO);
        }

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

        public DataTable BuscaPermissao()
        {
            if (IdUsuario <= 0 || IdModulo <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscapermissao", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdUsuario", IdUsuario);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdModulo", IdModulo);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na BuscaPermissao: {ex.Message}");
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

        public DataTable BuscaUsuario()
        {
            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscausuario", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdUsuario", IdUsuario);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscausuario: {ex.Message}");
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

        public DataTable BuscaUsuarioPermissao()
        {
            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscausuariopermissão", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdUsuario", IdUsuario);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscausuariopermissão: {ex.Message}");
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

        public DataTable BuscaNivelPermissao()
        {
            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscanivelpermissao", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscanivelpermissao: {ex.Message}");
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

        public bool atualizaPermissao()
        {
            if (!conectaBanco())
            {
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_atualizapermissao", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdUsuario", MySqlDbType.Int32) { Value = IdUsuario },
                new MySqlParameter("pIdModulo", MySqlDbType.Int32) { Value = IdModulo },
                new MySqlParameter("pIdNivel", MySqlDbType.Int16) { Value = IdNivel },

                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro (ex.Message, ex.StackTrace) para diagnóstico
                MessageBox.Show($"Erro ao atualizar pr_atualizapermissao: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexao?.State == ConnectionState.Open)
                    conexao.Close();
            }
        }
    }
}
