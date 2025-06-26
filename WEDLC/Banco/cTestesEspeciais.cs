using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cTestesEspeciais
    {
        public Int32 IdTestesEspeciais { get; set; }
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

        public bool atualizaTestesEspeciais()
        {
            // Validação de entrada
            if (IdFolha <= 0)
            {
                MessageBox.Show("ID folha obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!conectaBanco())
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_atualizatestesespeciais";

                    command.Parameters.AddWithValue("pIdFolha", IdFolha);
                    command.Parameters.AddWithValue("pBlinkReflex", blinkreflex);
                    command.Parameters.AddWithValue("pRbc", rbc);
                    command.Parameters.AddWithValue("pReflexOh", reflexoh);
                    command.Parameters.AddWithValue("pNspd", nspd);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    conexao.Close();
                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao?.Close();
                return false;
            }
        }

        public DataTable carregaTestesEspeciais(Int32 pIdFolha)

        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null; // Fix: Return null instead of a boolean to match the DataTable return type  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Fix: Return null instead of a boolean to match the DataTable return type  
            }

            try
            {
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscatestesespeciais", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", IdFolha);

                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);

                    //Fecha a conexão
                    conexao.Close();

                    // Retorna o DataTable
                    return dt;

                }

            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return null;
            }
        }
    }
}