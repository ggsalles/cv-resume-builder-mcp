using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cResultadoReflexoH
    {
        public Int32 IdResultado { get; set; }
        public Int32 idReflexoh { get; set; }
        public string idade { get; set; }
        public string comprimentoperna { get; set; }
        public string latenciadireita { get; set; }
        public string latenciaesquerda { get; set; }
        public string latenciaesperada { get; set; }
        public string limiteinferior { get; set; }
        public string limitesuperior { get; set; }
        public string diferencalados { get; set; }

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

        public DataTable buscaResultadoReflexoH()
        {
            // Validação básica dos parâmetros

            if (IdResultado < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadoreflexoh", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdResultado", IdResultado);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadoreflexoh: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado na pr_buscaresultadoreflexoh: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool gravaResultadoReflexoH()
        {
            if (!conectaBanco())
            {
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_incluiresultadoreflexoh", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdResultado", MySqlDbType.Int32) { Value = IdResultado},
                new MySqlParameter("pIdade", MySqlDbType.VarChar) { Value = idade ?? string.Empty },
                new MySqlParameter("pCompPerna", MySqlDbType.VarChar) { Value = comprimentoperna ?? string.Empty },
                new MySqlParameter("pLatDireita", MySqlDbType.VarChar) { Value = latenciadireita ?? string.Empty },
                new MySqlParameter("pLatEsquerda", MySqlDbType.VarChar) { Value = latenciaesquerda ?? string.Empty },
                new MySqlParameter("pLatEsperada", MySqlDbType.VarChar) { Value = latenciaesperada ?? string.Empty },
                new MySqlParameter("pLimInfeior", MySqlDbType.VarChar) { Value = limiteinferior ?? string.Empty },
                new MySqlParameter("pLimSuperior", MySqlDbType.VarChar) { Value = limitesuperior ?? string.Empty },
                new MySqlParameter("pDiferencaLados", MySqlDbType.VarChar) { Value = diferencalados ?? string.Empty },
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected >= 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro (ex.Message, ex.StackTrace) para diagnóstico
                MessageBox.Show($"Erro ao incluir/atualizar pr_incluiresultadoreflexoh: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
