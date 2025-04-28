using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace WEDLC.Banco
{
    public class cEspecializacao
    {
        private int _idespecializacao;
        private string _nome;

        public int IdEspecializacao   // property
        {
            get { return _idespecializacao; }   // get method
            set { _idespecializacao = value; }  // set method
        }

        public string Nome   // property
        {
            get { return _nome; }   // get method
            set { _nome = value; }  // set method
        }

        public DataTable buscaEspecializacao(int pTipopesquisa, int pIdEspecializacao, string pSigla, string pNome)

        {
            cConexao objcConexao = new cConexao();
            var conexao = objcConexao.MySqlConection();
            conexao.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscaespecializacao", conexao);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", pTipopesquisa);
            sqlDa.SelectCommand.Parameters.AddWithValue("pIdEspecializacao", pIdEspecializacao);
            sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", pSigla);
            sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
            //sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            conexao.Close();

            return dt;
        }

        public bool incluiEspecialidade()
        {

            cConexao objcConexao = new cConexao();
            var conexao = objcConexao.MySqlConection();
            conexao.Open();

            MySqlParameter[] pParam = new MySqlParameter[1];
            MySqlCommand command = new MySqlCommand();

            pParam[0] = new MySqlParameter("pNome", MySqlDbType.VarChar);
            pParam[0].Value = _nome;

            command.Connection = conexao;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pr_incluiespecializacao";
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
        public bool atualizaEspecializacao()
        {

            cConexao objcConexao = new cConexao();
            var conexao = objcConexao.MySqlConection();
            conexao.Open();

            MySqlParameter[] pParam = new MySqlParameter[2];
            MySqlCommand command = new MySqlCommand();

            pParam[0] = new MySqlParameter("pIdEpescializacao", MySqlDbType.Int32);
            pParam[0].Value = _idespecializacao;

            pParam[1] = new MySqlParameter("pNome", MySqlDbType.VarChar);
            pParam[1].Value = _nome;


            command.Connection = conexao;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pr_atualizaespecializacao";
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

        public bool excluiEspecializacao()
        {

            cConexao objcConexao = new cConexao();
            var conexao = objcConexao.MySqlConection();
            conexao.Open();

            MySqlParameter[] pParam = new MySqlParameter[1];
            MySqlCommand command = new MySqlCommand();

            pParam[0] = new MySqlParameter("pIdEpescializacao", MySqlDbType.Int32);
            pParam[0].Value = _idespecializacao;

            command.Connection = conexao;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "pr_excluiespecializacao";
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
    }
}
