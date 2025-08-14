using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace WEDLC.Banco
{
    public class cEstudoPotencialEvocado
    {
        public Int32 IdFolha { get; set; }
        public Int32 IdEstudoPotenEvocado { get; set; }
        public string Descricao{ get; set; }

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

        public bool IncluiEstudoPotencialEvocado()
        {
            // Validação básica dos dados
            if (IdFolha == 0)
            {
                Debug.WriteLine("ID inválido para inclusão do Estudo Potencial Evocado");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluiestudopotenevocado", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                // Considerar usar tipo correto (Int32 se forem números)
                new MySqlParameter("pIdFolha", MySqlDbType.Int32) { Value = IdFolha },
                 new MySqlParameter("pDescricao", MySqlDbType.VarChar) { Value = Descricao }
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
                Debug.WriteLine($"Erro MySQL ao incluir estudo potencial evocado: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro inesperado ao incluir estudo potencial evocado: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
        public bool ExcluiEstudoPotencialEvocado()
        {
            // Validação básica do ID
            if (IdEstudoPotenEvocado <= 0)
            {
                Debug.WriteLine("ID inválido para exclusão de estudo potencial evocado");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_excluiestudopotenevocado", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("pIdEstudoPotenEvocado", IdEstudoPotenEvocado);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451) // Código para FK violation
            {
                Debug.WriteLine($"Não foi possível excluir: registro possui relacionamentos. ID: {IdEstudoPotenEvocado}");
                return false;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erro MySQL ao excluir  estudo potencial evocado {IdEstudoPotenEvocado}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro inesperado ao excluir  estudo potencial evocado ID {IdEstudoPotenEvocado}: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
