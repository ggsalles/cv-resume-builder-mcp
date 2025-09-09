using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;

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

        [DisplayName("ID Nivel")]
        public Int32 IdNivel { get; set; }



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
      
        public DataTable BuscaPermissao()
        {
            if (IdUsuario <= 0 || IdModulo <=0)
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
    }
}
