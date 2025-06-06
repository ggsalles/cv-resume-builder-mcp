using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cSequence
    {
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

        public long GetNextSequenceValue(string sequenceName)
        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1; // Fix: Return null instead of a boolean to match the DataTable return type  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Fix: Return null instead of a boolean to match the DataTable return type  
            }

            long nextValue = 0;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("pr_nextval", conexao))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parâmetro de entrada
                    cmd.Parameters.AddWithValue("p_sequence_name", sequenceName);

                    // Parâmetro de saída
                    MySqlParameter outParam = new MySqlParameter("p_next_val", MySqlDbType.Int64)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    cmd.ExecuteNonQuery();

                    nextValue = Convert.ToInt64(outParam.Value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter próximo valor da sequência: {ex.Message}");
                throw; // Ou trate de outra forma apropriada
            }

            return nextValue;
        }
    }
}
