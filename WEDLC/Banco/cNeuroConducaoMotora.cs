using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cNeuroConducaoMotora
    {
        public Int32 IdFolha { get; set; }
        public Int32 IdNeuroCondMotora { get; set; }

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

        public bool IncluiNeuroConducaoMotora()
        {
            // Validação básica dos dados
            if (IdFolha == 0 || IdNervo == 0)
            {
                Debug.WriteLine("IDs inválidos para inclusão de neurocondução motora");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluineuroconducaomotora", conexao))
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
                Debug.WriteLine($"Erro MySQL ao incluir neurocondução motora: {ex.Message}");
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
        public bool ExcluiNeuroConducaoMotora()
        {
            // Validação básica do ID
            if (IdNeuroCondMotora <= 0)
            {
                Debug.WriteLine("ID inválido para exclusão de neurocondução motora");
                return false;
            }

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_excluineuroconducaomotora", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("pIdNeuroCondMotora", IdNeuroCondMotora);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451) // Código para FK violation
            {
                Debug.WriteLine($"Não foi possível excluir: registro possui relacionamentos. ID: {IdNeuroCondMotora}");
                return false;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"Erro MySQL ao excluir neurocondução motora ID {IdNeuroCondMotora}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro inesperado ao excluir neurocondução motora ID {IdNeuroCondMotora}: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
