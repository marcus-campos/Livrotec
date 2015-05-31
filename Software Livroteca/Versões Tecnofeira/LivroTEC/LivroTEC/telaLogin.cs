using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class telaLogin : Form
    {
        
        public telaLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AutenticarLogin cls = new AutenticarLogin();
            string _senha = ENCRIPTOGRAFAR_SENHAS.EncriptografarSenhas(tb_senha.Text);
            if (cls.Autenticar(tb_login.Text, _senha) == true)
            {
                telaPrincipal Principal = new telaPrincipal();
                Principal.Show();
                this.Close();
            }
            else
            {

            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void telaLogin_Load(object sender, EventArgs e)
        {
           
        }

        private void telaLogin_MaximumSizeChanged(object sender, EventArgs e)
        {
           
        }

        private void telaLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void telaLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void lblEsqueceuSenha_Click(object sender, EventArgs e)
        {
            Recuperar_Senha rec_senha_form = new Recuperar_Senha();
            rec_senha_form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Config_BD config_bd = new Config_BD();
            config_bd.ShowDialog(); 
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tb_login_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tb_senha_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
