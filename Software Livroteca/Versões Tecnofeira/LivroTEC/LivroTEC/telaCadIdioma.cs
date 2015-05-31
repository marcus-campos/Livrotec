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
    public partial class telaCadIdioma : Form
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public telaCadIdioma()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int _existe = 0;
        public void Verificar(string idioma)
        {

            try
            {
                objconexao.Open();
            }
            catch
            {
                MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                string _SQL = "";

                _SQL = "select * from tbl_idioma where idioma = '" + idioma + "';";

                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Usuarios");

                if (objdatasert_usu.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Este idioma já foi cadastrado!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _existe = 1;
                }
                else
                {
                    _existe = 0;
                }

            }
            objconexao.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Verificar(tb_idioma.Text);
            if (tb_idioma.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_existe == 0)
                {
                    EX_CMD inserir_novo_idioma = new EX_CMD();
                    inserir_novo_idioma.ExecutarSQL("insert into tbl_idioma values('','" + tb_idioma.Text + "');", "Idioma cadastrado com sucesso!");
                    this.Close();
                }
                else
                {

                }
            }
        }

        private void telaCadIdioma_Load(object sender, EventArgs e)
        {

        }
    }
}
