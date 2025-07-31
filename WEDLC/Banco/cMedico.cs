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
        public bool Apaga { get; set; }
        public int TipoPesquisa { get; set; }
        public Int32 IdMedico { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Pais { get; set; }
        public string Telefone { get; set; }
        public string NomeConsultorio { get; set; }
        public string CepConsultorio { get; set; }
        public string LogradouroConsultorio { get; set; }
        public string ComplementoConsultorio { get; set; }
        public string BairroConsultorio { get; set; }
        public string LocalidadeConsultorio { get; set; }
        public string UfConsultorio { get; set; }
        public string TelefoneConsultorio { get; set; }
        public int IdClasseConsultorio { get; set; }
        public decimal MediaConsultorio { get; set; }
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

        public DataTable carregaComboClasse()

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
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_carregacomboclasse", conexao)
)
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable buscaEspecializacaoMedico(Int32 idMedico)

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
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_buscaespecializacaomedico", conexao)
)
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdMedico", idMedico);

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

        public DataTable carregaComboEspecialidadeMedico()

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
                using (MySqlDataAdapter sqlDa = new MySqlDataAdapter("pr_carregacomboespecialidademedico", conexao)
)
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;

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
        public bool atualizaMedico()
        {
            // Validação básica dos dados
            if (IdMedico <= 0)
            {
                // Id inválido
                return false;
            }

            if (!conectaBanco())
            {
                return false;
            }

            try
            {
                using (var command = new MySqlCommand("pr_atualizamedico", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pApaga", MySqlDbType.Byte) { Value = Apaga },
                new MySqlParameter("pIdMedico", MySqlDbType.Int32) { Value = IdMedico},
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome },
                new MySqlParameter("pCep", MySqlDbType.VarChar) { Value = Cep },
                new MySqlParameter("pLogradouro", MySqlDbType.VarChar) { Value = Logradouro },
                new MySqlParameter("pComplemento", MySqlDbType.VarChar) { Value = Complemento },
                new MySqlParameter("pBairro", MySqlDbType.VarChar) { Value = Bairro },
                new MySqlParameter("pLocalidade", MySqlDbType.VarChar) { Value = Localidade },
                new MySqlParameter("pUf", MySqlDbType.VarChar) { Value = Uf },
                new MySqlParameter("pPais", MySqlDbType.VarChar) { Value = Pais },
                new MySqlParameter("pTelefone", MySqlDbType.VarChar) { Value = Telefone },
                new MySqlParameter("pNomeConsultorio", MySqlDbType.VarChar) { Value = NomeConsultorio },
                new MySqlParameter("pCepConsultorio", MySqlDbType.VarChar) { Value = CepConsultorio },
                new MySqlParameter("pLogradouroConsultorio", MySqlDbType.VarChar) { Value = LogradouroConsultorio },
                new MySqlParameter("pComplementoConsultorio", MySqlDbType.VarChar) { Value = ComplementoConsultorio },
                new MySqlParameter("pBairroConsultorio", MySqlDbType.VarChar) { Value = BairroConsultorio },
                new MySqlParameter("pLocalidadeConsultorio", MySqlDbType.VarChar) { Value = LocalidadeConsultorio },
                new MySqlParameter("pUfConsultorio", MySqlDbType.VarChar) { Value = UfConsultorio },
                new MySqlParameter("pTelefoneConsultorio", MySqlDbType.VarChar) { Value = TelefoneConsultorio },
                new MySqlParameter("pIdClasseConsultorio", MySqlDbType.Int32) { Value = IdClasseConsultorio },
                new MySqlParameter("pMediaConsultorio", MySqlDbType.Decimal) { Value = MediaConsultorio },
                new MySqlParameter("pIdEspecializacao", MySqlDbType.Int32) { Value = IdEspecializacao }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Considera sucesso se qualquer linha foi afetada
                }
            }
            catch (MySqlException ex)
            {
                // Logar o erro (ex.Message, ex.StackTrace) para diagnóstico
                MessageBox.Show($"Erro ao atualizar médico: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conexao?.State == ConnectionState.Open)
                    conexao.Close();
            }
        }

        public bool incluiMedico(cMedico pMedico, out Int32 ultimoId)

        {
            ultimoId = -1;

            if (!conectaBanco())
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                using (MySqlCommand command = new MySqlCommand("pr_incluimedico", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parâmetros de entrada
                    command.Parameters.AddWithValue("pNome", pMedico.Nome);
                    command.Parameters.AddWithValue("pCep", pMedico.Cep);
                    command.Parameters.AddWithValue("pLogradouro", pMedico.Logradouro);
                    command.Parameters.AddWithValue("pComplemento", pMedico.Complemento);
                    command.Parameters.AddWithValue("pBairro", pMedico.Bairro);
                    command.Parameters.AddWithValue("pLocalidade", pMedico.Localidade);
                    command.Parameters.AddWithValue("pUf", pMedico.Uf);
                    command.Parameters.AddWithValue("pPais", pMedico.Pais);
                    command.Parameters.AddWithValue("pTelefone", pMedico.Telefone);
                    command.Parameters.AddWithValue("pNomeconsultorio", pMedico.NomeConsultorio);
                    command.Parameters.AddWithValue("pCepconsultorio", pMedico.CepConsultorio);
                    command.Parameters.AddWithValue("pLogradouroconsultorio", pMedico.LogradouroConsultorio);
                    command.Parameters.AddWithValue("pComplementoconsultorio", pMedico.ComplementoConsultorio);
                    command.Parameters.AddWithValue("pBairroconsultorio", pMedico.BairroConsultorio);
                    command.Parameters.AddWithValue("pLocalidadeconsultorio", pMedico.LocalidadeConsultorio);
                    command.Parameters.AddWithValue("pUfconsultorio", pMedico.UfConsultorio);
                    command.Parameters.AddWithValue("pTelefoneconsultorio", pMedico.TelefoneConsultorio);
                    command.Parameters.AddWithValue("pIdClasseConsultorio", pMedico.IdClasseConsultorio);
                    command.Parameters.AddWithValue("pMediaConsultorio", pMedico.MediaConsultorio);
                    command.Parameters.AddWithValue("pIdEspecializacao", pMedico.IdEspecializacao);

                    // Parâmetro de saída
                    MySqlParameter outputParam = new MySqlParameter("@p_sequence", MySqlDbType.Int32);
                    outputParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParam);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    ultimoId = sucesso ? Convert.ToInt32(outputParam.Value) : -1;

                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao incluir medico: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool incluiEspecialidadeMedico(cMedico pMedico)

        {
            if (!conectaBanco())
            {
                MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                using (MySqlCommand command = new MySqlCommand("pr_incluiespecialidademedico", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parâmetros de entrada
                    command.Parameters.AddWithValue("pIdMedico", pMedico.IdMedico);
                    command.Parameters.AddWithValue("pIdEspecializacao", pMedico.IdEspecializacao);
                    
                    bool sucesso = command.ExecuteNonQuery() > 0;

                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao incluir especialidade: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
