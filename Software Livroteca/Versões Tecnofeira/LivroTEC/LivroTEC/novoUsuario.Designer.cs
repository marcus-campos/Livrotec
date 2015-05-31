namespace WindowsFormsApplication1
{
    partial class novoUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(novoUsuario));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtb_comum = new System.Windows.Forms.RadioButton();
            this.rtb_admin = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_confirma_senha = new System.Windows.Forms.TextBox();
            this.tb_senha = new System.Windows.Forms.TextBox();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.tb_nome = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCadastrarUsuario = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rtb_comum);
            this.groupBox1.Controls.Add(this.rtb_admin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_confirma_senha);
            this.groupBox1.Controls.Add(this.tb_senha);
            this.groupBox1.Controls.Add(this.tb_login);
            this.groupBox1.Controls.Add(this.tb_nome);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 226);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastro de novo usuario";
            // 
            // rtb_comum
            // 
            this.rtb_comum.AutoSize = true;
            this.rtb_comum.Location = new System.Drawing.Point(167, 190);
            this.rtb_comum.Name = "rtb_comum";
            this.rtb_comum.Size = new System.Drawing.Size(60, 17);
            this.rtb_comum.TabIndex = 15;
            this.rtb_comum.TabStop = true;
            this.rtb_comum.Text = "Comum";
            this.rtb_comum.UseVisualStyleBackColor = true;
            // 
            // rtb_admin
            // 
            this.rtb_admin.AutoSize = true;
            this.rtb_admin.Location = new System.Drawing.Point(73, 190);
            this.rtb_admin.Name = "rtb_admin";
            this.rtb_admin.Size = new System.Drawing.Size(88, 17);
            this.rtb_admin.TabIndex = 14;
            this.rtb_admin.TabStop = true;
            this.rtb_admin.Text = "Administrador";
            this.rtb_admin.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nome Completo*:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Login*:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Senha*:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Confirmar Senha*:";
            // 
            // tb_confirma_senha
            // 
            this.tb_confirma_senha.Location = new System.Drawing.Point(73, 164);
            this.tb_confirma_senha.Name = "tb_confirma_senha";
            this.tb_confirma_senha.Size = new System.Drawing.Size(197, 20);
            this.tb_confirma_senha.TabIndex = 10;
            this.tb_confirma_senha.UseSystemPasswordChar = true;
            // 
            // tb_senha
            // 
            this.tb_senha.Location = new System.Drawing.Point(73, 125);
            this.tb_senha.Name = "tb_senha";
            this.tb_senha.Size = new System.Drawing.Size(197, 20);
            this.tb_senha.TabIndex = 9;
            this.tb_senha.UseSystemPasswordChar = true;
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(73, 86);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(197, 20);
            this.tb_login.TabIndex = 8;
            // 
            // tb_nome
            // 
            this.tb_nome.Location = new System.Drawing.Point(73, 47);
            this.tb_nome.Name = "tb_nome";
            this.tb_nome.Size = new System.Drawing.Size(197, 20);
            this.tb_nome.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(193, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Campos com * são obrigatórios";
            // 
            // btnCadastrarUsuario
            // 
            this.btnCadastrarUsuario.Location = new System.Drawing.Point(99, 256);
            this.btnCadastrarUsuario.Name = "btnCadastrarUsuario";
            this.btnCadastrarUsuario.Size = new System.Drawing.Size(120, 25);
            this.btnCadastrarUsuario.TabIndex = 10;
            this.btnCadastrarUsuario.Text = "Cadastrar";
            this.btnCadastrarUsuario.UseVisualStyleBackColor = true;
            this.btnCadastrarUsuario.Click += new System.EventHandler(this.btnCadastrarUsuario_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(225, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 25);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // novoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(357, 297);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCadastrarUsuario);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "novoUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novo usuario";
            this.Load += new System.EventHandler(this.novoUsuario_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_confirma_senha;
        private System.Windows.Forms.TextBox tb_senha;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.TextBox tb_nome;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCadastrarUsuario;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rtb_comum;
        private System.Windows.Forms.RadioButton rtb_admin;
    }
}