using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    // Filtro para detectar atividade global (mouse, teclado, cliques)
    public class FiltroAtividade : IMessageFilter
    {
        private readonly Action _onActivity;
        private readonly int[] mensagensAtividade = {
            0x0200, // WM_MOUSEMOVE
            0x0201, // WM_LBUTTONDOWN
            0x0204, // WM_RBUTTONDOWN
            0x0202, // WM_LBUTTONUP
            0x0205, // WM_RBUTTONUP
            0x0100, // WM_KEYDOWN
            0x0101  // WM_KEYUP
        };

        public FiltroAtividade(Action onActivity)
        {
            _onActivity = onActivity;
        }

        public bool PreFilterMessage(ref Message m)
        {
            foreach (int msg in mensagensAtividade)
            {
                if (m.Msg == msg)
                {
                    try
                    {
                        _onActivity();
                    }
                    catch
                    {
                        // não deixar o filtro quebrar o loop de mensagens
                    }
                    break;
                }
            }
            return false; // não bloqueia a mensagem
        }
    }
}
