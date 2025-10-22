using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WEDLC.Forms
{
    public partial class frmConfirmacaoSessao : Form
    {
        private int segundosRestantes;
        private Timer timerContagem;
        private Timer timerFade;
        private Label lblMensagem;
        private Label lblTempo;
        private Button btnContinuar;

        public bool ContinuarSessao { get; private set; } = false;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public frmConfirmacaoSessao(int tempoConfirmacaoSegundos = 15)
        {
            segundosRestantes = tempoConfirmacaoSegundos;
            InicializarLayout();
            IniciarTimers();
        }

        private void InicializarLayout()
        {
            // Detecta tema atual do Windows
            bool temaEscuro = DetectarTemaEscuro();

            Color bgColor = temaEscuro ? Color.FromArgb(40, 40, 45) : Color.FromArgb(255, 255, 230);
            Color fgColor = temaEscuro ? Color.WhiteSmoke : Color.FromArgb(40, 40, 40);
            Color btnColor = temaEscuro ? Color.FromArgb(90, 90, 150) : Color.FromArgb(255, 220, 100);

            // Configurações básicas do form
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = bgColor;
            ForeColor = fgColor;
            Opacity = 0;
            Size = new Size(400, 180);
            TopMost = true;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

            // Label principal
            lblMensagem = new Label
            {
                Text = "Sessão inativa detectada.",
                Dock = DockStyle.Top,
                Height = 50,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = fgColor
            };
            Controls.Add(lblMensagem);

            // Label do tempo
            lblTempo = new Label
            {
                Text = "",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = fgColor
            };
            Controls.Add(lblTempo);

            // Botão continuar
            btnContinuar = new Button
            {
                Text = "Continuar Sessão",
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = btnColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = temaEscuro ? Color.White : Color.Black
            };
            btnContinuar.FlatAppearance.BorderSize = 0;
            Controls.Add(btnContinuar);

            btnContinuar.Click += (s, e) =>
            {
                ContinuarSessao = true;
                FecharComFadeOut();
            };
        }

        private void IniciarTimers()
        {
            // Timer do contador regressivo
            timerContagem = new Timer { Interval = 1000 };
            timerContagem.Tick += (s, e) =>
            {
                segundosRestantes--;
                lblTempo.Text = $"Fechando automaticamente em {segundosRestantes} segundos...";

                if (segundosRestantes <= 0)
                {
                    timerContagem.Stop();
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            };

            // Timer de fade-in
            timerFade = new Timer { Interval = 30 };
            timerFade.Tick += (s, e) =>
            {
                if (Opacity < 1)
                    Opacity += 0.08;
                else
                    timerFade.Stop();
            };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            lblTempo.Text = $"Fechando automaticamente em {segundosRestantes} segundos...";
            timerContagem.Start();
            timerFade.Start();

            // 🔔 Emite alerta sonoro sutil
            SystemSounds.Exclamation.Play();
        }

        private async void FecharComFadeOut()
        {
            for (double i = Opacity; i > 0; i -= 0.08)
            {
                Opacity = i;
                await System.Threading.Tasks.Task.Delay(20);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // Detecta se o Windows está em tema escuro
        private bool DetectarTemaEscuro()
        {
            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (key != null)
                {
                    var value = key.GetValue("AppsUseLightTheme");
                    return value != null && (int)value == 0; // 0 = dark mode
                }
            }
            catch { }
            return false;
        }
    }
}
