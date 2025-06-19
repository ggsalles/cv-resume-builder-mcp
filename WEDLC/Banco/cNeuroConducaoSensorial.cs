using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace WEDLC.Banco
{
    public class cNeuroConducaoSensorial
    {
        public Int32 IdFolha { get; set; }
        public Int32 IdNeuroCondSensorial { get; set; }

        public Int32 IdNervo { get; set; }

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

        public bool IncluiNeuroConducaoSensorial()
        {
            // Validação básica dos dados
            if (IdFolha == 0 || IdNervo == 0)
            {
                Debug.WriteLine("IDs inválidos para inclusão de neurocondução sensorial");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluineuroconducaosensorial", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                // Considerar usar tipo correto (Int32 se forem números)
                new MySqlParameter("pIdFolha", MySqlDbType.Int32) { Value = IdFolha },
                new MySqlParameter("pIdNervo", MySqlDbType.Int32) { Value = IdNervo }
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
                Debug.WriteLine($"Erro MySQL ao incluir neurocondução sensorial: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro inesperado ao incluir neurocondução motora: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
        public bool ExcluiNeuroConducaoSensorial()
        {
            // Validação básica do ID
            if (IdNeuroCondSensorial <= 0)
            {
                Debug.WriteLine("ID inválido para exclusão de neurocondução sensorial");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_excluineuroconducaosensorial", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("pIdNeuroCondSensorial", IdNeuroCondSensorial);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451) // Código para FK violation
            {
                Debug.WriteLine($"Não foi possível excluir: registro possui relacionamentos. ID: {IdNeuroCondSensorial}");
                return false;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erro MySQL ao excluir neurocondução sensorial ID {IdNeuroCondSensorial}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro inesperado ao excluir neurocondução sensorial ID {IdNeuroCondSensorial}: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
