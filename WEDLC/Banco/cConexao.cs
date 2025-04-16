using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEDLC.Banco
{
    public class cConexao
    {
        public enum Ambiente
        {
            LOCAL = 1,
            REMOTO = 2
        }

        public string conexao;
        
        //Determina o ambiente de conexao atual
        public Ambiente cAmbiente = Ambiente.LOCAL;

        public string buscaStringConexao()
        {

            if (cAmbiente == Ambiente.LOCAL) 
            {
                conexao = System.Configuration.ConfigurationManager.ConnectionStrings["L_WEDLC"].ConnectionString; // Desenvolvimento
            }
            else if (cAmbiente == Ambiente.REMOTO)
            {
                conexao = System.Configuration.ConfigurationManager.ConnectionStrings["R_WEDLC"].ConnectionString; // Remoto
            }

            return conexao;
        }

        public MySqlConnection MySqlConection() // Determina a instância que será usada para a conexão: 1 - local; 2 - Remoto
        {
            var conexao = new MySqlConnection(Conexao());
            return conexao;
        }

        public string Conexao()   // property
        {
            var stringConexao = this.buscaStringConexao();
            return stringConexao;

        }
    }
}
