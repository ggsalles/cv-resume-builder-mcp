using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class clLog
    {
        public int IdLog { get; set; }
        public int IdLogDescricao { get; set; }
        public string DataLog { get; set; }
        public int IdUsuario { get; set; }
        public string DescErro { get; set; }

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

        public bool incluiLogin()
        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Fix: Return null instead of a boolean to match the DataTable return type  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Fix: Return null instead of a boolean to match the DataTable return type  
            }

            try
            {
                using (var command = new MySqlCommand("pr_log", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new[]
                    {
                        new MySqlParameter("pIdLogDescricao", MySqlDbType.Int32) { Value = IdLogDescricao },
                        new MySqlParameter("pIdUsuario", MySqlDbType.Int32) { Value = IdUsuario },
                        new MySqlParameter("pDescErro", MySqlDbType.VarChar) { Value = DescErro },
                        new MySqlParameter("pDataLog", MySqlDbType.DateTime) { Value = DataLog }
                    });

                    int affectedRows = command.ExecuteNonQuery();
                    return affectedRows > 0;
                }
            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return false;
            }

        }
    }
}
