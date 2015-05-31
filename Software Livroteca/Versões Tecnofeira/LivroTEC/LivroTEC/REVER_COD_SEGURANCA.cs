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
    public partial class REVER_COD_SEGURANCA : Form
    {
        public REVER_COD_SEGURANCA()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (tb_login.Text == "" || tb_senha.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AutenticarLogin cls = new AutenticarLogin();
                string _senha = ENCRIPTOGRAFAR_SENHAS.EncriptografarSenhas(tb_senha.Text);
                if (cls.Autenticar(tb_login.Text, _senha) == true)
                {
                    tb_cod_seguranca.Text = AutenticarLogin._cod_seguranca;
                }
                else
                {

                }
            }
        }
    }
}
