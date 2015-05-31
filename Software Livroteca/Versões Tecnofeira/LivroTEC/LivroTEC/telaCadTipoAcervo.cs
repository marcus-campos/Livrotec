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
    public partial class telaCadTipoAcervo : Form
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public telaCadTipoAcervo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int _existe = 0;
        public void Verificar(string tipoAcervo)
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

                _SQL = "select * from tbl_tipo_acervo where tipo_acervo = '" + tipoAcervo + "';";

                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Usuarios");

                if (objdatasert_usu.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Este Tipode de acervo já foi cadastrado!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (tb_nome_tipo_de_acervo.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EX_CMD inserir_novo_tipo_de_acervo = new EX_CMD();
                inserir_novo_tipo_de_acervo.ExecutarSQL("insert into tbl_tipo_acervo values('','" + tb_nome_tipo_de_acervo.Text + "','" + NuD_dias_de_emprestimo.Value + "');", "Acervo cadastrado com sucesso!");
                this.Close();
            }
        }

        private void telaCadTipoAcervo_Load(object sender, EventArgs e)
        {

        }
    }
}
