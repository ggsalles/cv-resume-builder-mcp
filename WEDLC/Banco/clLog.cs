using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class clLog
    {
        private int _idlog; // field
        private int _idlogdescricao; // field  
        private string _datalog;
        private int _idusuario;
        private string _descerrovs;

        public int IdLog   // property
        {
            get { return _idlog; }   // get method
            set { _idlog = value; }  // set method
        }

        public int Idlogdescricao   // property
        {
            get { return _idlogdescricao; }   // get method
            set { _idlogdescricao = value; }  // set method
        }

        public string Datalog   // property
        {
            get { return _datalog; }   // get method
            set { _datalog = value; }  // set method
        }

        public int Idusuario   // property
        {
            get { return _idusuario; }   // get method
            set { _idusuario = value; }  // set method
        }

        public string Descerrovs   // property
        {
            get { return _descerrovs; }   // get method
            set { _descerrovs = value; }  // set method
        }

        cConexao objcConexao = new cConexao();
        MySqlConnection conexao = new MySqlConnection();

        public bool conectaBanco()
        {
            conexao = objcConexao.MySqlConection();
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
                MySqlParameter[] pParam = new MySqlParameter[3];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pIdLogDescricao", MySqlDbType.Int16);
                pParam[0].Value = _idlogdescricao;

                pParam[1] = new MySqlParameter("pIdUsuario", MySqlDbType.Int16);
                pParam[1].Value = _idusuario;

                pParam[2] = new MySqlParameter("pDescerrovs", MySqlDbType.VarChar);
                pParam[2].Value = _descerrovs;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_log";
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
                return false;
            }

        }
    }
}
