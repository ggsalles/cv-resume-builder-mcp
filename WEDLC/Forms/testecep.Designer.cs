namespace WEDLC.Forms
{
    partial class testecep
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.txtcep = new System.Windows.Forms.TextBox();
            this.txtlogradouro = new System.Windows.Forms.TextBox();
            this.txtcidade = new System.Windows.Forms.TextBox();
            this.txtbairro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtuf = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(313, 72);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisar.TabIndex = 0;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtcep
            // 
            this.txtcep.Location = new System.Drawing.Point(12, 27);
            this.txtcep.Name = "txtcep";
            this.txtcep.Size = new System.Drawing.Size(100, 20);
            this.txtcep.TabIndex = 1;
            // 
            // txtlogradouro
            // 
            this.txtlogradouro.Location = new System.Drawing.Point(118, 27);
            this.txtlogradouro.Name = "txtlogradouro";
            this.txtlogradouro.Size = new System.Drawing.Size(270, 20);
            this.txtlogradouro.TabIndex = 2;
            // 
            // txtcidade
            // 
            this.txtcidade.Location = new System.Drawing.Point(12, 74);
            this.txtcidade.Name = "txtcidade";
            this.txtcidade.Size = new System.Drawing.Size(206, 20);
            this.txtcidade.TabIndex = 3;
            // 
            // txtbairro
            // 
            this.txtbairro.Location = new System.Drawing.Point(394, 27);
            this.txtbairro.Name = "txtbairro";
            this.txtbairro.Size = new System.Drawing.Size(277, 20);
            this.txtbairro.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "CEP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Logradouro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Bairro";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cidade";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(224, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "UF";
            // 
            // txtuf
            // 
            this.txtuf.Location = new System.Drawing.Point(224, 74);
            this.txtuf.Name = "txtuf";
            this.txtuf.Size = new System.Drawing.Size(64, 20);
            this.txtuf.TabIndex = 9;
            // 
            // testecep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 111);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtuf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbairro);
            this.Controls.Add(this.txtcidade);
            this.Controls.Add(this.txtlogradouro);
            this.Controls.Add(this.txtcep);
            this.Controls.Add(this.btnPesquisar);
            this.Name = "testecep";
            this.Text = "testecep";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.TextBox txtcep;
        private System.Windows.Forms.TextBox txtlogradouro;
        private System.Windows.Forms.TextBox txtcidade;
        private System.Windows.Forms.TextBox txtbairro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtuf;
    }
}