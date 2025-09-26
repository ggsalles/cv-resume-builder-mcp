using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace WinFormsZoom
{
    public class FormZoomHelper
    {
        private readonly Form form;
        private readonly string baseTitle;

        public float ZoomFactor { get; private set; } = 1.0f;
        public float MinZoom { get; set; } = 0.5f;
        public float MaxZoom { get; set; } = 2.0f;

        private float targetZoom;
        private const float animationStep = 0.03f;
        private readonly Timer animationTimer;

        private readonly Size baseFormSize;
        private readonly Dictionary<Control, Rectangle> originalBounds = new Dictionary<Control, Rectangle>();
        private readonly Dictionary<Control, float> originalFontSizes = new Dictionary<Control, float>();

        private readonly string zoomFilePath;
        private bool zoomLoaded = false;
        private bool initializing = false;

        public FormZoomHelper(Form targetForm)
        {
            form = targetForm ?? throw new ArgumentNullException(nameof(targetForm));
            form.KeyPreview = true;

            baseTitle = form.Text;
            baseFormSize = form.ClientSize;

            StoreOriginalLayout(form);
            AttachMouseWheelHandlers(form);
            form.KeyDown += Form_KeyDown;

            animationTimer = new Timer { Interval = 5 };
            animationTimer.Tick += AnimationTimer_Tick;

            zoomFilePath = Path.Combine(Application.UserAppDataPath, "zoom.json");

            initializing = true;
            LoadSavedZoom();

            if (Math.Abs(ZoomFactor - 1.0f) > 0.001f)
            {
                ApplyZoom(); // aplica sem salvar
            }
            initializing = false;

            UpdateTitle();

            // Salva posição/tamanho no fechamento do form
            form.FormClosing += (s, e) => SavePositionAndSize();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (Math.Abs(ZoomFactor - targetZoom) < 0.001f)
            {
                ZoomFactor = targetZoom;
                ApplyZoom();
                animationTimer.Stop();
            }
            else
            {
                ZoomFactor = ZoomFactor < targetZoom
                    ? Math.Min(ZoomFactor + animationStep, targetZoom)
                    : Math.Max(ZoomFactor - animationStep, targetZoom);

                ApplyZoom();
            }
        }

        private void StoreOriginalLayout(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (!originalBounds.ContainsKey(ctrl))
                {
                    originalBounds[ctrl] = new Rectangle(ctrl.Left, ctrl.Top, ctrl.Width, ctrl.Height);
                    originalFontSizes[ctrl] = ctrl.Font.Size;
                }

                if (ctrl.HasChildren)
                    StoreOriginalLayout(ctrl);
            }
        }

        private void AttachMouseWheelHandlers(Control parent)
        {
            parent.MouseWheel += Form_MouseWheel;
            foreach (Control c in parent.Controls)
                AttachMouseWheelHandlers(c);
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0) ZoomIn();
                else if (e.Delta < 0) ZoomOut();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Shift && !e.Alt)
            {
                if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add) ZoomIn();
                else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract) ZoomOut();
                else if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0) ResetZoom();
            }
        }

        public void ZoomIn() => SetTargetZoom(ZoomFactor + 0.05f);
        public void ZoomOut() => SetTargetZoom(ZoomFactor - 0.05f);
        public void ResetZoom() => SetTargetZoom(1.0f);

        private void SetTargetZoom(float newZoom)
        {
            if (!zoomLoaded) return;

            targetZoom = Math.Max(MinZoom, Math.Min(MaxZoom, newZoom));
            animationTimer.Start();
        }

        private void ApplyZoom()
        {
            if (!zoomLoaded) return;

            form.SuspendLayout();

            form.ClientSize = new Size(
                (int)(baseFormSize.Width * ZoomFactor),
                (int)(baseFormSize.Height * ZoomFactor)
            );

            foreach (var kv in originalBounds)
            {
                var ctrl = kv.Key;
                var orig = kv.Value;

                ctrl.SetBounds(
                    (int)(orig.X * ZoomFactor),
                    (int)(orig.Y * ZoomFactor),
                    (int)(orig.Width * ZoomFactor),
                    (int)(orig.Height * ZoomFactor)
                );

                ctrl.Font = new Font(ctrl.Font.FontFamily, originalFontSizes[ctrl] * ZoomFactor, ctrl.Font.Style);
            }

            UpdateTitle();

            // 🔹 salva zoom apenas quando alterado pelo usuário
            if (!initializing)
                SaveZoom();

            form.ResumeLayout();
        }

        private void UpdateTitle()
        {
            if (!zoomLoaded) return;
            int percent = (int)(ZoomFactor * 100);
            form.Text = $"{baseTitle} - {percent}%";
        }

        private void LoadSavedZoom()
        {
            try
            {
                if (File.Exists(zoomFilePath))
                {
                    var json = File.ReadAllText(zoomFilePath);
                    var dict = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

                    if (dict != null)
                    {
                        if (dict.TryGetValue(form.Name + "_Zoom", out int percent))
                            ZoomFactor = targetZoom = percent / 100f;

                        if (dict.TryGetValue(form.Name + "_Left", out int left) &&
                            dict.TryGetValue(form.Name + "_Top", out int top))
                        {
                            form.StartPosition = FormStartPosition.Manual;
                            form.Location = new Point(left, top);
                        }

                        if (dict.TryGetValue(form.Name + "_Width", out int w) &&
                            dict.TryGetValue(form.Name + "_Height", out int h))
                        {
                            form.Size = new Size(w, h);
                        }
                    }
                }
                zoomLoaded = true;
            }
            catch
            {
                zoomLoaded = true;
            }
        }

        private void SaveZoom()
        {
            if (!zoomLoaded) return;

            try
            {
                Dictionary<string, int> dict;
                if (File.Exists(zoomFilePath))
                {
                    var json = File.ReadAllText(zoomFilePath);
                    dict = JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
                }
                else dict = new Dictionary<string, int>();

                dict[form.Name + "_Zoom"] = (int)(ZoomFactor * 100);

                var jsonOut = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(zoomFilePath));
                File.WriteAllText(zoomFilePath, jsonOut);
            }
            catch { }
        }

        private void SavePositionAndSize()
        {
            if (!zoomLoaded) return;

            try
            {
                Dictionary<string, int> dict;
                if (File.Exists(zoomFilePath))
                {
                    var json = File.ReadAllText(zoomFilePath);
                    dict = JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
                }
                else dict = new Dictionary<string, int>();

                dict[form.Name + "_Left"] = form.Left;
                dict[form.Name + "_Top"] = form.Top;
                dict[form.Name + "_Width"] = form.Width;
                dict[form.Name + "_Height"] = form.Height;

                var jsonOut = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(zoomFilePath));
                File.WriteAllText(zoomFilePath, jsonOut);
            }
            catch { }
        }

        public void Dispose()
        {
            animationTimer?.Stop();
            animationTimer?.Dispose();
        }
    }
}
