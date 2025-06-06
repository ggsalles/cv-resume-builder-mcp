using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cAvaliacaoMuscular
    {
        public Int32 IdFolha { get; set; }
        public Int32 IdMusculo { get; set; }
        public Int32 IdAvaliacaoMuscular { get; set; }

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

        public bool incluiAvaliacaoMuscular()
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
                MySqlParameter[] pParam = new MySqlParameter[2];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pIdFolha", MySqlDbType.VarChar);
                pParam[0].Value = IdFolha;

                pParam[1] = new MySqlParameter("pIdMusculo", MySqlDbType.VarChar);
                pParam[1].Value = IdMusculo;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_incluiavaliacaomuscular";
                command.Parameters.AddRange(pParam);

                if (command.ExecuteNonQuery() == 1)

                {
                    conexao.Close();
                    return true;
                }
                else
                {
                    conexao.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return false; ;
            }
        }

        public bool excluiAvaliacaoMuscular()
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
                MySqlParameter[] pParam = new MySqlParameter[1];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pIdAvaliacaoMuscular", MySqlDbType.Int32);
                pParam[0].Value = IdAvaliacaoMuscular;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_excluiavaliacaomuscular";
                command.Parameters.AddRange(pParam);

                if (command.ExecuteNonQuery() == 1)

                {
                    conexao.Close();
                    return true;
                }
                else
                {
                    conexao.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return false; ;
            }
        }

    }
}
