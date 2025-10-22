using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WEDLC.Forms.frmLogin;

namespace WEDLC.Banco
{
    public static class Sessao
    {
        public static int IdUsuario { get; set; }
        public static string NomeUsuario { get; set; }
        public static NivelAcesso Nivel { get; set; }

        // Lista de permissões detalhadas por módulo (opcional)
        public static List<PermissaoModulo> Permissoes { get; set; } = new List<PermissaoModulo>();
    }

    public class PermissaoModulo
    {
        public int IdModulo { get; set; }
        public string NomeModulo { get; set; }
        public NivelAcesso Nivel { get; set; }
    }
}
