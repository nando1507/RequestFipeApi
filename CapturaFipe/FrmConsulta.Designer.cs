namespace CapturaFipe
{
    partial class FrmConsulta
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
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.lblLinhas = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMarca = new System.Windows.Forms.Button();
            this.btnModelo = new System.Windows.Forms.Button();
            this.btnPeriodo = new System.Windows.Forms.Button();
            this.btnValores = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDados
            // 
            this.dgvDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(12, 115);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.Size = new System.Drawing.Size(776, 323);
            this.dgvDados.TabIndex = 0;
            // 
            // lblLinhas
            // 
            this.lblLinhas.Location = new System.Drawing.Point(119, 89);
            this.lblLinhas.Name = "lblLinhas";
            this.lblLinhas.Size = new System.Drawing.Size(100, 23);
            this.lblLinhas.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Linhas Baixadas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMarca
            // 
            this.btnMarca.Location = new System.Drawing.Point(12, 27);
            this.btnMarca.Name = "btnMarca";
            this.btnMarca.Size = new System.Drawing.Size(75, 23);
            this.btnMarca.TabIndex = 3;
            this.btnMarca.Text = "Marca";
            this.btnMarca.UseVisualStyleBackColor = true;
            this.btnMarca.Click += new System.EventHandler(this.btnMarca_Click);
            // 
            // btnModelo
            // 
            this.btnModelo.Location = new System.Drawing.Point(93, 27);
            this.btnModelo.Name = "btnModelo";
            this.btnModelo.Size = new System.Drawing.Size(75, 23);
            this.btnModelo.TabIndex = 4;
            this.btnModelo.Text = "Modelo";
            this.btnModelo.UseVisualStyleBackColor = true;
            this.btnModelo.Click += new System.EventHandler(this.btnModelo_Click);
            // 
            // btnPeriodo
            // 
            this.btnPeriodo.Location = new System.Drawing.Point(174, 27);
            this.btnPeriodo.Name = "btnPeriodo";
            this.btnPeriodo.Size = new System.Drawing.Size(75, 23);
            this.btnPeriodo.TabIndex = 5;
            this.btnPeriodo.Text = "Periodo";
            this.btnPeriodo.UseVisualStyleBackColor = true;
            this.btnPeriodo.Click += new System.EventHandler(this.btnPeriodo_Click);
            // 
            // btnValores
            // 
            this.btnValores.Location = new System.Drawing.Point(255, 27);
            this.btnValores.Name = "btnValores";
            this.btnValores.Size = new System.Drawing.Size(75, 23);
            this.btnValores.TabIndex = 6;
            this.btnValores.Text = "Valores";
            this.btnValores.UseVisualStyleBackColor = true;
            this.btnValores.Click += new System.EventHandler(this.btnValores_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem,
            this.processarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // processarToolStripMenuItem
            // 
            this.processarToolStripMenuItem.Name = "processarToolStripMenuItem";
            this.processarToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.processarToolStripMenuItem.Text = "Processar";
            this.processarToolStripMenuItem.Click += new System.EventHandler(this.processarToolStripMenuItem_Click);
            // 
            // FrmConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnValores);
            this.Controls.Add(this.btnPeriodo);
            this.Controls.Add(this.btnModelo);
            this.Controls.Add(this.btnMarca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLinhas);
            this.Controls.Add(this.dgvDados);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmConsulta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.Label lblLinhas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMarca;
        private System.Windows.Forms.Button btnModelo;
        private System.Windows.Forms.Button btnPeriodo;
        private System.Windows.Forms.Button btnValores;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processarToolStripMenuItem;
    }
}

