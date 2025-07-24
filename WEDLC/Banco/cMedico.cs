using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cMedico
    {
        public int TipoPesquisa { get; set; }
        public Int32 IdMedico { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Pais { get; set; }
        public string Telefone { get; set; }
        public string Consultorio { get; set; }
        public string CepConsultorio { get; set; }
        public string LogradouroConsultorio { get; set; }
        public string ComplementoConsultorio { get; set; }
        public string BairroConsultorio { get; set; }
        public string LocalidadeConsultorio { get; set; }
        public string UfConsultorio { get; set; }
        public string TelefoneConsultorio { get; set; }
        public int Classe { get; set; }
        public double Media { get; set; }
        public int IdEspecializacao { get; set; }


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

        public DataTable buscaMedico()

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
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscamedico", conexao)
)
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdMedico", IdMedico);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome);

                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);

                    //Fecha a conexão
                    conexao.Close();

                    // Retorna o DataTable
                    return dt;
                }

            }
            catch (System.Exception)
            {
                // Fecha a conexão  
                conexao.Close();
                return null;
            }
        }

    }
}
