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
            Local = 1,
            Remoto = 2
        }

        public string conexao;

        public string buscaStringConexao(Ambiente eAmbiente)
        {

            if (eAmbiente == Ambiente.Local) 
            {
                conexao = System.Configuration.ConfigurationManager.ConnectionStrings["L_WEDLC"].ConnectionString; // Desenvolvimento
            }
            else if (eAmbiente == Ambiente.Remoto)
            {
                conexao = System.Configuration.ConfigurationManager.ConnectionStrings["R_WEDLC"].ConnectionString; // Remoto
            }

            return conexao;
        }

    }
}
