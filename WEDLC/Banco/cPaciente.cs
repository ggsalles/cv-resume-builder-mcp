using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cPaciente
    {
        public bool Apaga { get; set; }
        public int TipoPesquisa { get; set; }
        public Int32 IdPaciente { get; set; }
        public Int32 IdPacienteFolha { get; set; }
        public Int32 IdExame { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Telefone { get; set; }
        public int IdSexo { get; set; }
        public string DataNascimento { get; set; }
        public Int32 IdConvenio { get; set; }
        public Int32 IdIndicacao1 { get; set; }
        public Int32 IdIndicacao2 { get; set; }
        public Int32 IdMedico { get; set; }
        public int IdSimNao { get; set; } // 0 - Não, 1 - Sim Benefcio
        public string DataCadastro { get; set; }
        public Int32 IdFolha { get; set; } // Folha de Pagamento
        public string Observacao { get; set; }

        // Propriedades de relacionamento
        public cConvenio Convenio { get; set; }
        public cIndicacao Indicacao { get; set; }
        public cMedico Medico { get; set; }
        public cSimNao SimNao { get; set; }
        public cFolha Folha { get; set; }
        public cSexo Sexo { get; set; }

        // Construtor padrão
        public cPaciente()
        {
            Convenio = new cConvenio();
            Indicacao = new cIndicacao();
            Medico = new cMedico();
            SimNao = new cSimNao();
            Folha = new cFolha();
            Sexo = new cSexo();
        }

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

        public DataTable buscaPaciente()
        {
            // Validação básica dos parâmetros
            if (TipoPesquisa < 0)
                return null;

            if (TipoPesquisa == 1 && IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscapaciente", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pTipoPesquisa", TipoPesquisa);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);
                    sqlDa.SelectCommand.Parameters.AddWithValue("pNome", Nome ?? string.Empty);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca do paciente: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool incluiPaciente(out Int32 ultimoId)
        {
            ultimoId = -1;

            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluipaciente", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pNome", MySqlDbType.VarChar) { Value = Nome},
                new MySqlParameter("pCep", MySqlDbType.VarChar) { Value = Cep ?? string.Empty },
                new MySqlParameter("pLogradouro", MySqlDbType.VarChar) { Value = Logradouro ?? string.Empty },
                new MySqlParameter("pComplemento", MySqlDbType.VarChar) { Value = Complemento ?? string.Empty },
                new MySqlParameter("pBairro", MySqlDbType.VarChar) { Value = Bairro ?? string.Empty },
                new MySqlParameter("pLocalidade", MySqlDbType.VarChar) { Value = Localidade ?? string.Empty },
                new MySqlParameter("pUf", MySqlDbType.VarChar) { Value = Uf ?? string.Empty },
                new MySqlParameter("pTelefone", MySqlDbType.VarChar) { Value = Telefone ?? string.Empty },
                new MySqlParameter("pIdSexo", MySqlDbType.Int32) { Value = IdSexo },
                new MySqlParameter("pNascimento", MySqlDbType.Date) { Value =  DateTime.Parse(DataNascimento).ToString("yyyy/MM/dd") },
                new MySqlParameter("pIdConvenio", MySqlDbType.Int32) { Value = (IdConvenio == 0) ? DBNull.Value : (object)IdConvenio},
                new MySqlParameter("pIdIndicacao1", MySqlDbType.Int32) { Value = (IdIndicacao1 == 0) ? DBNull.Value : (object)IdIndicacao1},
                new MySqlParameter("pIdIndicacao2", MySqlDbType.Int32) { Value = (IdIndicacao2 == 0) ? DBNull.Value : (object)IdIndicacao2},
                new MySqlParameter("pIdMedico", MySqlDbType.Int32) { Value = (IdMedico == 0) ? DBNull.Value : (object)IdMedico},
                new MySqlParameter("pIdSimNao", MySqlDbType.Int32) { Value = (IdSimNao == 0) ? DBNull.Value : (object)IdSimNao},
                new MySqlParameter("pObservacao", MySqlDbType.VarChar) { Value = Observacao ?? string.Empty }

                    });

                    // Parâmetro de saída
                    MySqlParameter outputParam = new MySqlParameter("@p_sequence", MySqlDbType.Int32);
                    outputParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParam);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    ultimoId = sucesso ? Convert.ToInt32(outputParam.Value) : -1;

                    return sucesso;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir paciente: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para outros tipos de erro
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
        public bool incluiPacienteFolha()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluipacientefolha", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdPaciente", MySqlDbType.Int32) { Value = IdPaciente },
                new MySqlParameter("pIdFolha", MySqlDbType.Int32) { Value = Folha.IdFolha }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Qualquer número positivo indica sucesso
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir paciente folha: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para outros tipos de erro
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool atualizaPaciente()
        {
            // Validação de entrada
            if (IdPaciente <= 0 || IdFolha <= 0)
            {
                MessageBox.Show("ID´s obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!conectaBanco())
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_atualizapaciente"; // Atiualiza paciente e paciente folha

                    command.Parameters.AddWithValue("pApaga", Apaga);
                    command.Parameters.AddWithValue("pIdFolha", IdFolha);
                    command.Parameters.AddWithValue("pIdPaciente", IdPaciente);
                    command.Parameters.AddWithValue("pNome", Nome ?? string.Empty);
                    command.Parameters.AddWithValue("pCep", Cep ?? string.Empty);
                    command.Parameters.AddWithValue("pLogradouro", Logradouro ?? string.Empty);
                    command.Parameters.AddWithValue("pComplemento", Complemento ?? string.Empty);
                    command.Parameters.AddWithValue("pBairro", Bairro ?? string.Empty);
                    command.Parameters.AddWithValue("pLocalidade", Localidade ?? string.Empty);
                    command.Parameters.AddWithValue("pUf", Uf ?? string.Empty);
                    command.Parameters.AddWithValue("pTelefone", Telefone ?? string.Empty);
                    command.Parameters.AddWithValue("pIdSexo", IdSexo);
                    command.Parameters.AddWithValue("pNascimento", DateTime.Parse(DataNascimento).ToString("yyyy/MM/dd"));
                    command.Parameters.AddWithValue("pIdConvenio", (IdConvenio == 0) ? DBNull.Value : (object)IdConvenio);
                    command.Parameters.AddWithValue("pIdIndicacao1", (IdIndicacao1 == 0) ? DBNull.Value : (object)IdIndicacao1);
                    command.Parameters.AddWithValue("pIdIndicacao2", (IdIndicacao2 == 0) ? DBNull.Value : (object)IdIndicacao2);
                    command.Parameters.AddWithValue("pIdMedico", (IdMedico == 0) ? DBNull.Value : (object)IdMedico);
                    command.Parameters.AddWithValue("pIdSimNao", (IdSimNao == 0) ? DBNull.Value : (object)IdSimNao);
                    command.Parameters.AddWithValue("pObservacao", Observacao ?? string.Empty);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    conexao.Close();
                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao?.Close();
                return false;
            }
        }
        public DataTable buscaPacienteFolha()
        {
            // Validação básica dos parâmetros
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscapacientefolha", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);
                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca do paciente: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public DataTable buscaPacienteExame()
        {
            // Validação básica dos parâmetros
            if (IdPaciente < 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscapacienteexame", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdPaciente", IdPaciente);
                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na busca do paciente: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }

        public bool incluiPacienteExame()
        {
            if (!conectaBanco())
                return false;

            try
            {
                using (var command = new MySqlCommand("pr_incluipacienteexame", conexao))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(new MySqlParameter[]
                    {
                new MySqlParameter("pIdPaciente", MySqlDbType.Int32) { Value = IdPaciente },
                new MySqlParameter("pIdExame", MySqlDbType.Int32) { Value = IdExame }
                    });

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Qualquer número positivo indica sucesso
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir paciente exame: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log para outros tipos de erro
                System.Diagnostics.Debug.WriteLine($"Erro inesperado: {ex.Message}");
                return false;
            }
            finally
            {
                conexao?.Close();
            }
        }
        public bool atualizaPacienteExame()
        {
            // Validação de entrada
            if (IdPaciente <= 0 || IdExame <= 0)
            {
                MessageBox.Show("ID´s obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!conectaBanco())
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_atualizapacienteexame"; // Atiualiza paciente exame

                    command.Parameters.AddWithValue("pApaga", Apaga);
                    command.Parameters.AddWithValue("pIdPaciente", IdPaciente);
                    command.Parameters.AddWithValue("pIdExame", IdExame);

                    bool sucesso = command.ExecuteNonQuery() > 0;
                    conexao.Close();
                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexao?.Close();
                return false;
            }
        }

    }
}
