using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Numerics;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cResultadoPotenciaisUnidadeMotora
    {
        public Int32 IdPaciente { get; set; }
        public Int32 IdResultado { get; set; }
        public Int32 IdPotenciaisUnidade { get; set; }
        public Int32 IdResultadoPotenciaisUnidade{ get; set; }
        public string Texto { get; set; }

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

        public DataTable buscaResultadoUnidadePotencial()
        {
            // Validação básica dos parâmetros

            if (IdResultado <= 0)
                return null;

            if (!conectaBanco())
                return null;

            DataTable dt = new DataTable();

            try
            {
                using (var sqlDa = new MySqlDataAdapter("pr_buscaresultadopotenciaisunidade", conexao))
                {
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("pIdResultado", IdResultado);

                    sqlDa.Fill(dt);
                    return dt;
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro na pr_buscaresultadoavaliacaomuscular: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Log para outros erros
                System.Diagnostics.Debug.WriteLine($"Erro inesperado na pr_buscaresultadoatividadeinsercao: {ex.Message}");
                return null;
            }
            finally
            {
                conexao?.Close();
            }
        }
    }
}
