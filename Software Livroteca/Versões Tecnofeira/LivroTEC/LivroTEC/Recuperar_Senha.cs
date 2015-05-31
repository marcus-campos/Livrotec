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
    public partial class Recuperar_Senha : Form
    {
        public Recuperar_Senha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_cod_seguranca.Text == "" || tb_login.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                RecuperarSenha rec_senha = new RecuperarSenha();
                if (rec_senha.Rec_Senha(tb_login.Text, tb_cod_seguranca.Text) == true)
                {
                    GeradorDeSenha();

                    string SQL = "Update tbl_usuario set SENHA = '" + ENCRIPTOGRAFAR_SENHAS.EncriptografarSenhas(_senhaGerada) + "' WHERE LOGIN = '" + tb_login.Text + "';";
                    EX_CMD nova_senha = new EX_CMD();
                    nova_senha.ExecutarSQL(SQL, "Senha recuperada com sucesso!\n\nSua nova senha é: " + _senhaGerada + "");
                    this.Close();
                }
                else
                {

                }
            }
        }
        public string _senhaGerada = "";
        public void GeradorDeSenha()
        {
            string carac = "abcdefhijkmnopqrstuvxwyz123456789";

            char[] letras = carac.ToCharArray();
            Embaralhar(ref letras, 16);
            for (int x = 0; x <= 1; x++)
            {
                string senha = new String(letras).Substring(0, 4);
                _senhaGerada += senha;
            }

        }

        public static void Embaralhar(ref char[] array, int vezes)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 1; i <= vezes; i++)
            {
                for (int x = 1; x <= array.Length; x++)
                {
                    Trocar(ref array[rand.Next(0, array.Length)],
                      ref array[rand.Next(0, array.Length)]);
                }
            }
        }

        public static void Trocar(ref char arg1, ref char arg2)
        {
            char strTemp = arg1;
            arg1 = arg2;
            arg2 = strTemp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
