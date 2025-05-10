using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace WEDLC.Banco
{
    public class cMusculo
    {
        private int _idmusculo;
        private string _nome;
        private string _sigla;
        private string _raizes;
        private string _inervacao;

        public int IdMusculo   // property
        {
            get { return _idmusculo; }   // get method
            set { _idmusculo = value; }  // set method
        }

        public string Sigla   // property
        {
            get { return _sigla; }   // get method
            set { _sigla = value; }  // set method
        }

        public string Nome   // property
        {
            get { return _nome; }   // get method
            set { _nome = value; }  // set method
        }

        public string Raizes   // property
        {
            get { return _raizes; }   // get method
            set { _raizes = value; }  // set method
        }

        public string Inervacao   // property
        {
            get { return _inervacao; }   // get method
            set { _inervacao = value; }  // set method
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

        public DataTable buscaMusculo(int pTipopesquisa, int pIdMusculo, string pSigla, string pNome)
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
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscamusculo", conexao);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", pTipopesquisa);
                sqlDa.SelectCommand.Parameters.AddWithValue("pIdMusculo", pIdMusculo);
                sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", pSigla);
                sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
                sqlDa.SelectCommand.Parameters.AddWithValue("pRaizes", pNome);
                sqlDa.SelectCommand.Parameters.AddWithValue("pInervacao", pNome);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);

                // Fecha a conexão  
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

        public bool incluiMusculo()

        {
            try

            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                MySqlParameter[] pParam = new MySqlParameter[4];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pSigla", MySqlDbType.VarChar);
                pParam[0].Value = _sigla;

                pParam[1] = new MySqlParameter("pNome", MySqlDbType.VarChar);
                pParam[1].Value = _nome;

                pParam[2] = new MySqlParameter("pRaizes", MySqlDbType.VarChar);
                pParam[2].Value = _raizes;

                pParam[3] = new MySqlParameter("pInervacao", MySqlDbType.VarChar);
                pParam[3].Value = _inervacao;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_incluimusculo";
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
                conexao.Close();
                return false;
            }

        }

        public bool atualizamusculo()
        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try

            {
                MySqlParameter[] pParam = new MySqlParameter[5];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pIdMusculo", MySqlDbType.Int32);
                pParam[0].Value = _idmusculo;

                pParam[1] = new MySqlParameter("pSigla", MySqlDbType.VarChar);
                pParam[1].Value = _sigla;

                pParam[2] = new MySqlParameter("pNome", MySqlDbType.VarChar);
                pParam[2].Value = _nome;

                pParam[3] = new MySqlParameter("pRaizes", MySqlDbType.VarChar);
                pParam[3].Value = _raizes;

                pParam[4] = new MySqlParameter("pInervacao", MySqlDbType.VarChar);
                pParam[4].Value = _inervacao;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_atualizamusculo";
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
                conexao.Close();
                return false;
            }

        }

        public bool excluiNervo()
        {
            try
            {
                if (conectaBanco() == false)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                MySqlParameter[] pParam = new MySqlParameter[1];
                MySqlCommand command = new MySqlCommand();

                pParam[0] = new MySqlParameter("pIdMusculo", MySqlDbType.Int32);
                pParam[0].Value = _idmusculo;

                command.Connection = conexao;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pr_excluimusculo";
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
                return false;
            }
        }
    }
}
