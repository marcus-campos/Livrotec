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
    public partial class Config_BD : Form
    {
          
        public Config_BD()
        {
            InitializeComponent();
        }

        private void Config_BD_Load(object sender, EventArgs e)
        {
            
        }

        public void ConectString(string Servidor, string porta, string database, string usuario, string senha)
        {
            Program.ConectionString = "Persist Security Info=False;server=" + Servidor + ";Port=" + porta + ";database=" + database + ";uid=" + usuario + ";pwd=" + senha + "";                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConectString(tb_servidor.Text, tb_porta.Text, tb_database.Text, tb_usuario.Text, tb_senha.Text);
            VERIFICA_USUARIOS verifica_usu = new VERIFICA_USUARIOS();
            verifica_usu.VerificaSeTemUsuarios();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tb_servidor.Text = "localhost";
            tb_senha.Text = "";
            tb_usuario.Text = "root";
            tb_database.Text = "livrotec";
            tb_porta.Text = "3306";
        }

        private void ckb_mostra_senha_CheckStateChanged(object sender, EventArgs e)
        {
            if (ckb_mostra_senha.Checked == true)
            {
                tb_senha.UseSystemPasswordChar = false;
            }
            else
            {
                tb_senha.UseSystemPasswordChar = true;
            }
            
        }
    }
}
