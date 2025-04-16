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
        private int _codigo;
        private string _nome;

        public int Codigo   // property
        {
            get { return _codigo; }   // get method
            set { _codigo = value; }  // set method
        }

        public string Nome   // property
        {
            get { return _nome; }   // get method
            set { _nome = value; }  // set method
        }

        public DataTable buscaEspecializacao(int pCodigo, string pNome)

        {
            cConexao objcConexao = new cConexao();
            var conexao = objcConexao.MySqlConection();
            conexao.Open();
            MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscaespecializacao", conexao);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("pCodigo", pCodigo);
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

            pParam[0] = new MySqlParameter("pCodigo", MySqlDbType.Int32);
            pParam[0].Value = _codigo;

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

            pParam[0] = new MySqlParameter("pCodigo", MySqlDbType.Int32);
            pParam[0].Value = _codigo;

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
