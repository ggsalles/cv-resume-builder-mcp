using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace WEDLC.Banco
{
    public class cTestesEspeciais
    {
        public Int32 IdFolha { get; set; }
        public int blinkreflex { get; set; }
        public int rbc { get; set; }
        public int reflexoh { get; set; }
        public int nspd { get; set; }

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

        public bool IncluiTestesEspeciais()
        {
            // Validação básica dos dados
            if (IdFolha == 0)
            {
                Debug.WriteLine("ID inválido para inclusão de testes especiais");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluitestesespeciais", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                // Considerar usar tipo correto (Int32 se forem números)
                new MySqlParameter("pIdFolha", MySqlDbType.Int32) { Value = IdFolha },
                new MySqlParameter("pValorBlink", MySqlDbType.Int16) { Value = blinkreflex },
                new MySqlParameter("pValorRbc", MySqlDbType.Int16) { Value = rbc },
                new MySqlParameter("pValorReflexo", MySqlDbType.Int16) { Value = reflexoh },
                new MySqlParameter("pValorNspd", MySqlDbType.Int16) { Value = nspd }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062) // Código para duplicata
            {
                Debug.WriteLine($"Tentativa de inserção duplicada: {ex.Message}");
                return false;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erro MySQL ao incluir testes especiais: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro inesperado ao incluir testes especiais: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }

    }
}