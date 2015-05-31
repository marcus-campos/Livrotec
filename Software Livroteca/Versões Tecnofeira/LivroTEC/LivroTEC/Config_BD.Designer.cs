namespace WindowsFormsApplication1
{
    partial class Config_BD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config_BD));
            this.gb_config = new System.Windows.Forms.GroupBox();
            this.btnPadrao = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.ckb_mostra_senha = new System.Windows.Forms.CheckBox();
            this.tb_porta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_senha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_usuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_database = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_servidor = new System.Windows.Forms.TextBox();
            this.gb_config.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_config
            // 
            this.gb_config.BackColor = System.Drawing.Color.Transparent;
            this.gb_config.Controls.Add(this.btnPadrao);
            this.gb_config.Controls.Add(this.btnCancelar);
            this.gb_config.Controls.Add(this.btnConfirmar);
            this.gb_config.Controls.Add(this.ckb_mostra_senha);
            this.gb_config.Controls.Add(this.tb_porta);
            this.gb_config.Controls.Add(this.label5);
            this.gb_config.Controls.Add(this.tb_senha);
            this.gb_config.Controls.Add(this.label4);
            this.gb_config.Controls.Add(this.tb_usuario);
            this.gb_config.Controls.Add(this.label3);
            this.gb_config.Controls.Add(this.tb_database);
            this.gb_config.Controls.Add(this.label6);
            this.gb_config.Controls.Add(this.label7);
            this.gb_config.Controls.Add(this.tb_servidor);
            this.gb_config.Location = new System.Drawing.Point(12, 12);
            this.gb_config.Name = "gb_config";
            this.gb_config.Size = new System.Drawing.Size(380, 289);
            this.gb_config.TabIndex = 9;
            this.gb_config.TabStop = false;
            this.gb_config.Text = "Configurações";
            // 
            // btnPadrao
            // 
            this.btnPadrao.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPadrao.Location = new System.Drawing.Point(150, 248);
            this.btnPadrao.Name = "btnPadrao";
            this.btnPadrao.Size = new System.Drawing.Size(75, 23);
            this.btnPadrao.TabIndex = 31;
            this.btnPadrao.Text = "Padrao";
            this.btnPadrao.UseVisualStyleBackColor = true;
            this.btnPadrao.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(231, 248);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 30;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(69, 248);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 29;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckb_mostra_senha
            // 
            this.ckb_mostra_senha.AutoSize = true;
            this.ckb_mostra_senha.Location = new System.Drawing.Point(69, 225);
            this.ckb_mostra_senha.Name = "ckb_mostra_senha";
            this.ckb_mostra_senha.Size = new System.Drawing.Size(93, 17);
            this.ckb_mostra_senha.TabIndex = 28;
            this.ckb_mostra_senha.Text = "Mostrar senha";
            this.ckb_mostra_senha.UseVisualStyleBackColor = true;
            this.ckb_mostra_senha.CheckStateChanged += new System.EventHandler(this.ckb_mostra_senha_CheckStateChanged);
            // 
            // tb_porta
            // 
            this.tb_porta.Location = new System.Drawing.Point(69, 80);
            this.tb_porta.Name = "tb_porta";
            this.tb_porta.Size = new System.Drawing.Size(237, 20);
            this.tb_porta.TabIndex = 20;
            this.tb_porta.Text = "3306";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Porta";
            // 
            // tb_senha
            // 
            this.tb_senha.Location = new System.Drawing.Point(69, 199);
            this.tb_senha.Name = "tb_senha";
            this.tb_senha.Size = new System.Drawing.Size(237, 20);
            this.tb_senha.TabIndex = 24;
            this.tb_senha.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Senha";
            // 
            // tb_usuario
            // 
            this.tb_usuario.Location = new System.Drawing.Point(69, 160);
            this.tb_usuario.Name = "tb_usuario";
            this.tb_usuario.Size = new System.Drawing.Size(237, 20);
            this.tb_usuario.TabIndex = 23;
            this.tb_usuario.Text = "root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Usuário";
            // 
            // tb_database
            // 
            this.tb_database.Location = new System.Drawing.Point(69, 122);
            this.tb_database.Name = "tb_database";
            this.tb_database.Size = new System.Drawing.Size(237, 20);
            this.tb_database.TabIndex = 21;
            this.tb_database.Text = "livrotec";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Database";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Servidor (IP)";
            // 
            // tb_servidor
            // 
            this.tb_servidor.Location = new System.Drawing.Point(69, 41);
            this.tb_servidor.Name = "tb_servidor";
            this.tb_servidor.Size = new System.Drawing.Size(237, 20);
            this.tb_servidor.TabIndex = 18;
            this.tb_servidor.Text = "localhost";
            // 
            // Config_BD
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnPadrao;
            this.ClientSize = new System.Drawing.Size(404, 312);
            this.Controls.Add(this.gb_config);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 350);
            this.Name = "Config_BD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurar conexão";
            this.Load += new System.EventHandler(this.Config_BD_Load);
            this.gb_config.ResumeLayout(false);
            this.gb_config.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_config;
        private System.Windows.Forms.Button btnPadrao;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.CheckBox ckb_mostra_senha;
        private System.Windows.Forms.TextBox tb_porta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_senha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_usuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_database;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_servidor;
    }
}