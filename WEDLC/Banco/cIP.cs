using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEDLC.Banco
{
    public class cIp
    {
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
    }
}
