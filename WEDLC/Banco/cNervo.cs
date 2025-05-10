using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cNervo
    {
        private int _idnervo;
        private string _nome;
        private string _sigla;
        private string _normlmd;
        private string _normncm;
        private string _normncs;

        public int IdNervo   // property
        {
            get { return _idnervo; }   // get method
            set { _idnervo = value; }  // set method
        }

        public string Nome   // property
        {
            get { return _nome; }   // get method
            set { _nome = value; }  // set method
        }

        public string Sigla   // property
        {
            get { return _sigla; }   // get method
            set { _sigla = value; }  // set method
        }

        public string NormLmd   // property
        {
            get { return _normlmd; }   // get method
            set { _normlmd = value; }  // set method
        }

        public string NormNcm   // property
        {
            get { return _normncm; }   // get method
            set { _normncm = value; }  // set method
        }

        public string NormNcs   // property
        {
            get { return _normncs; }   // get method
            set { _normncs = value; }  // set method
        }

        // Construtor
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

        public DataTable buscaNervo(int pTipopesquisa, int pIdNervo, string pSigla, string pNome)

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
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscanervo", conexao);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", pTipopesquisa);
                sqlDa.SelectCommand.Parameters.AddWithValue("pIdNervo", pIdNervo);
                sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", pSigla);
                sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);

                //sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);

                //Fecha a conexão
                conexao.Close();

                // Retorna o DataTable
                return dt;
            }
            catch (Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return null;
            }
        }

        public bool incluiNervo()
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
                MySqlParameter[] pParam = new MySqlParameter[5];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pSigla", MySqlDbType.VarChar);
                pParam[0].Value = _sigla;

                pParam[1] = new MySqlParameter("pNome", MySqlDbType.VarChar);
                pParam[1].Value = _nome;

                pParam[2] = new MySqlParameter("pNormLmd", MySqlDbType.VarChar);
                pParam[2].Value = _normlmd;

                pParam[3] = new MySqlParameter("pNormNcs", MySqlDbType.VarChar);
                pParam[3].Value = _normncs;

                pParam[4] = new MySqlParameter("pNormNcm", MySqlDbType.VarChar);
                pParam[4].Value = _normncm;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_incluinervo";
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
        public bool atualizanervo()
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
                MySqlParameter[] pParam = new MySqlParameter[6];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pIdNervo", MySqlDbType.Int32);
                pParam[0].Value = _idnervo;

                pParam[1] = new MySqlParameter("pSigla", MySqlDbType.VarChar);
                pParam[1].Value = _sigla;

                pParam[2] = new MySqlParameter("pNome", MySqlDbType.VarChar);
                pParam[2].Value = _nome;

                pParam[3] = new MySqlParameter("pNormLmd", MySqlDbType.VarChar);
                pParam[3].Value = _normlmd;

                pParam[4] = new MySqlParameter("pNormNcm", MySqlDbType.VarChar);
                pParam[4].Value = _normncm;

                pParam[5] = new MySqlParameter("pNormNcs", MySqlDbType.VarChar);
                pParam[5].Value = _normncs;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_atualizanervo";
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
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Fix: Return null instead of a boolean to match the DataTable return type  ;
            }
        }

        public bool excluiNervo()
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

                pParam[0] = new MySqlParameter("pIdNervo", MySqlDbType.Int32);
                pParam[0].Value = _idnervo;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_excluinervo";
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
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Fix: Return null instead of a boolean to match the DataTable return type  ;
            }
        }
    }
}
