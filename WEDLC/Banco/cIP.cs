using MySql.Data.MySqlClient;
using Nest;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEDLC.Banco
{
    public class cIp
    {
        public string Endereco { get; set; }
        public cIp() { }

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

        private static readonly string[] IpServices =
  {
        "https://api.ipify.org",
        "https://icanhazip.com",
        "https://ipinfo.io/ip",
        "https://checkip.amazonaws.com"
    };

        // Método assíncrono para obter o IP público
        public static async Task<string> GetPublicIPAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // Tenta cada serviço até obter uma resposta válida
                foreach (var service in IpServices)
                {
                    try
                    {
                        // Faz a requisição e retorna o IP (removendo espaços extras)
                        string ip = await httpClient.GetStringAsync(service);
                        return ip.Trim();
                    }
                    catch
                    {
                        // Se falhar, tenta o próximo serviço
                        continue;
                    }
                }
            }

            throw new Exception("Não foi possível obter o IP público.");
        }

        public bool buscaEndereco()
        {
            if (!conectaBanco())
                return false;

            try
            {
                cIp retorno = new cIp();

                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conexao;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pr_rede";

                    command.Parameters.AddWithValue("pEndereco", Endereco);
                    MySqlParameter pTotalParam = new MySqlParameter("pTotal", MySqlDbType.Int32);
                    pTotalParam.Direction = ParameterDirection.Output;

                    command.Parameters.Add(pTotalParam);
                    command.ExecuteNonQuery(); // Continua sendo ExecuteNonQuery
                    if (Convert.ToInt32(pTotalParam.Value) == 0)
                    {
                        conexao.Close();
                        return false;
                    }
                    else
                    {
                        conexao.Close();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log específico para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Erro ao incluir indicacao: {ex.Message}");
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

        // storedBlock exemplo: "10.15.123"
        public bool RetornaIpPrefixo(string ipAddress, out string ipPrefix)
        {
            ipPrefix = null;

            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;

            ipAddress = ipAddress.Trim();
            var parts = ipAddress.Split('.');

            // IP precisa ter exatamente 4 blocos
            if (parts.Length != 4)
                return false;

            // Valida cada octeto
            for (int i = 0; i < 4; i++)
            {
                if (!int.TryParse(parts[i], out int octet))
                    return false;

                if (octet < 0 || octet > 255)
                    return false;
            }

            // Monta apenas os 3 primeiros
            ipPrefix = $"{parts[0]}.{parts[1]}.{parts[2]}";
            return true;
        }
    }
}
