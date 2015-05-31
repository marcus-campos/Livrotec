using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Splash : Form
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            VERIFICA_USUARIOS verificar_usu = new VERIFICA_USUARIOS();
            verificar_usu.VerificaSeTemUsuarios();
        }        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 10;
            }
            else

                if (progressBar1.Value >= 100)
                {
                    this.Hide();
                    telaLogin tl_log = new telaLogin();
                    tl_log.Show();
                    timer1.Enabled = false;
                }
                else
                {

                }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)
            {
                this.Opacity += 0.2;
            }
            else
            {
                timer2.Enabled = false;
                timer1.Enabled = true;
            }
        }
    }
}
