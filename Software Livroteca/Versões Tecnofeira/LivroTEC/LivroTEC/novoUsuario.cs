using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;


namespace WindowsFormsApplication1
{
    public partial class novoUsuario : Form
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public novoUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Verificar(string Login)
        {
            
            try
            {
                objconexao.Open();
            }
            catch
            {
                MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                string _SQL = "";

                _SQL = "select * from tbl_usuario where login = '" + Login + "';";
                              
                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Usuarios");

                if (objdatasert_usu.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Este login já esta sendo usado por outro usuário!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _existe = 1;
                }
                else
                {
                    _existe = 0;
                }
               
            }
            objconexao.Close();
        }
        public int _existe = 0;
        private void btnCadastrarUsuario_Click(object sender, EventArgs e)
        {
            
            int _tipo = 0;
            if (rtb_admin.Checked == true)
            {
                _tipo = 1;
            }
            else
            {
                _tipo = 0;
            }

            if (tb_senha.Text == tb_confirma_senha.Text)
            {

            }
            else
            {
                MessageBox.Show("As senhas não coincidem!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Verificar(tb_login.Text);
            if (_existe == 0)
            {
                GeradorCodSeguranca();

                string _senha = ENCRIPTOGRAFAR_SENHAS.EncriptografarSenhas(tb_senha.Text);
                if (tb_login.Text == "" || tb_nome.Text == "" || tb_senha.Text == "" || tb_confirma_senha.Text == "" || rtb_admin.Checked == false && rtb_comum.Checked == false)
                {
                    MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    EX_CMD NOVO_USER = new EX_CMD();
                    NOVO_USER.ExecutarSQL("insert into tbl_usuario values(null,'" + tb_nome.Text + "','" + tb_login.Text + "','" + _senha + "','" + _tipo + "','" + _cod_seguranca + "');", "Usuário cadastrado com sucesso!\n\nPor favor anote seu codigo de seguranca: " + _cod_seguranca);
                    this.Close();
                }
            }
            else
            {

            }
        }
        public string _cod_seguranca = "";
        public void GeradorCodSeguranca()
        {
            string carac = "abcdefhijkmnopqrstuvxwyz123456789";

            char[] letras = carac.ToCharArray();
            Embaralhar(ref letras, 16);
            for (int x = 0; x <= 1; x++)
            {
                string cod_seguranca = new String(letras).Substring(0, 3);
                _cod_seguranca += cod_seguranca;
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


        private void novoUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
