using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cResultadoNeuroCondMotora
    {
        public Int32 IdResultadoVelocNeuroCondMotora { get; set; }
        public Int32 IdPaciente { get; set; }
        public Int32 IdFolha { get; set; }
        public Int32 IdResultado { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public string VelocidadeDireito { get; set; }
        public string VelocidadeEsquerdo { get; set; }
        public string LatenciaDireito { get; set; }
        public string LatenciaEsquerdo { get; set; }

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

        public DataTable buscaResultadoNeuroCondMotora()
        {
            // Validação básica dos parâmetros

            if (IdFolha <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadoneurocondmotora", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca da pr_buscaresultadoneurocondmotora: {ex.Message}");
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

        public bool gravaResultadoNeuroConducaoMotora()
        {
            if (!conectaBanco())
            {
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_atualizaresultadoneuroconducaomotora", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdResultadoNeuroCondMotora", MySqlDbType.Int32) { Value = IdResultadoVelocNeuroCondMotora },
                new MySqlParameter("pVelocidadeDireito", MySqlDbType.VarChar) { Value = VelocidadeDireito ?? string.Empty },
                new MySqlParameter("pVelocidadeEsquerdo", MySqlDbType.VarChar) { Value = VelocidadeEsquerdo ?? string.Empty },
                new MySqlParameter("pLatenciaDireito", MySqlDbType.VarChar) { Value = LatenciaDireito ?? string.Empty },
                new MySqlParameter("pLatenciaEsquerdo", MySqlDbType.VarChar) { Value = LatenciaEsquerdo ?? string.Empty },
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro (ex.Message, ex.StackTrace) para diagnóstico
                MessageBox.Show($"Erro ao atualizar pr_atualizaresultadoneuroconducaomotora: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexao?.State == ConnectionState.Open)
                    conexao.Close();
            }
        }

        public DataTable buscaRelNeuroCondMotoraLatencia()
        {
            // Validação básica dos parâmetros

            if (IdFolha <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_relneuroconducaomotoralatencia", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdResultado", IdResultado);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca da pr_buscaresultadoneurocondmotora: {ex.Message}");
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
