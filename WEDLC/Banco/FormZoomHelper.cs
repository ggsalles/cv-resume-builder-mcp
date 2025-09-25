using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinFormsZoom
{
    public class FormZoomHelper
    {
        private readonly Form form;
        private float zoomFactor = 1.0f;
        private const float zoomStep = 0.1f;
        private const float minZoom = 0.5f;
        private const float maxZoom = 2.0f;

        private Size baseFormSize;
        private Dictionary<Control, Rectangle> originalBounds = new Dictionary<Control, Rectangle>();
        private Dictionary<Control, float> originalFontSizes = new Dictionary<Control, float>();

        public FormZoomHelper(Form targetForm)
        {
            form = targetForm ?? throw new ArgumentNullException(nameof(targetForm));
            form.KeyPreview = true;
            form.MouseWheel += Form_MouseWheel;

            baseFormSize = form.ClientSize;
            StoreOriginalLayout(form);
        }

        private void StoreOriginalLayout(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (!originalBounds.ContainsKey(ctrl))
                {
                    originalBounds[ctrl] = ctrl.Bounds;
                    originalFontSizes[ctrl] = ctrl.Font.Size;
                }

                if (ctrl.HasChildren)
                    StoreOriginalLayout(ctrl);
            }
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0 && zoomFactor < maxZoom)
                    zoomFactor += zoomStep;
                else if (e.Delta < 0 && zoomFactor > minZoom)
                    zoomFactor -= zoomStep;

                ApplyZoom();
            }
        }

        private void ApplyZoom()
        {
            form.SuspendLayout();

            form.ClientSize = new Size(
                (int)(baseFormSize.Width * zoomFactor),
                (int)(baseFormSize.Height * zoomFactor)
            );

            foreach (Control ctrl in form.Controls)
            {
                ApplyZoomToControl(ctrl);
            }

            form.ResumeLayout();
        }

        private void ApplyZoomToControl(Control ctrl)
        {
            if (originalBounds.ContainsKey(ctrl))
            {
                Rectangle orig = originalBounds[ctrl];
                ctrl.SetBounds(
                    (int)(orig.X * zoomFactor),
                    (int)(orig.Y * zoomFactor),
                    (int)(orig.Width * zoomFactor),
                    (int)(orig.Height * zoomFactor)
                );

                float origFontSize = originalFontSizes[ctrl];
                ctrl.Font = new Font(ctrl.Font.FontFamily,
                                     origFontSize * zoomFactor,
                                     ctrl.Font.Style);
            }

            foreach (Control child in ctrl.Controls)
            {
                ApplyZoomToControl(child);
            }
        }
    }
}
