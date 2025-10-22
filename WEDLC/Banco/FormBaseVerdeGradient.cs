using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeuProjeto
{
    public class FormBaseVerdeGradient : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private Timer fadeTimer;

        public FormBaseVerdeGradient()
        {
            InicializarLayout();
            InicializarFade();
        }

        private void InicializarLayout()
        {
            // Configurações básicas
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            Opacity = 0;
            Size = new Size(800, 600);

            // Define uma cor de fundo segura
            BackColor = Color.FromArgb(150, 200, 150);

            // Bordas arredondadas
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            // Permite arrastar clicando em qualquer ponto
            MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Capture = false;
                    Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                    WndProc(ref m);
                }
            };

            // Redesenha corretamente ao redimensionar
            Resize += (s, e) =>
            {
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
                Invalidate();
            };
        }

        private void InicializarFade()
        {
            fadeTimer = new Timer { Interval = 30 };
            fadeTimer.Tick += (s, e) =>
            {
                if (Opacity < 1)
                    Opacity += 0.08;
                else
                    fadeTimer.Stop();
            };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            fadeTimer.Start();
        }

        public async Task FadeOutAsync()
        {
            for (double i = Opacity; i > 0; i -= 0.08)
            {
                Opacity = i;
                await Task.Delay(20);
            }
        }

        // Gradiente de fundo verde oliva → verde claro
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(110, 130, 80),      // verde oliva escuro
                Color.FromArgb(200, 240, 200),     // verde claro suave
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }

            // Borda verde oliva sutil
            using (Pen pen = new Pen(Color.FromArgb(80, 100, 60), 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
            }
        }
    }
}
