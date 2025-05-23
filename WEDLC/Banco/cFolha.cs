using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cFolha
    {
        public int IdTipoFolha { get; set; }
        public string Descricao { get; set; }

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

        public DataTable buscaTipoFolha()

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
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscatipofolha", conexao);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable buscaGrupoFolha(int pTipo)

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
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscagrupofolha", conexao);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("pTipo", pTipo);

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

        public DataTable buscaFolha(int pTipopesquisa, Int32 pIdFolha, string pSigla, string pNome)

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
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscafolha", conexao);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", pTipopesquisa);
                sqlDa.SelectCommand.Parameters.AddWithValue("pIdFolha", pIdFolha);
                sqlDa.SelectCommand.Parameters.AddWithValue("pSigla", pSigla);
                sqlDa.SelectCommand.Parameters.AddWithValue("pNome", pNome);

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
    }
}
