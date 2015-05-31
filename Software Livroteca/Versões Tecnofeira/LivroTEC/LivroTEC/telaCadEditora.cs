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
    public partial class telaCadEditora : Form
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public telaCadEditora()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int _existe = 0;
        public void Verificar(string cnpj)
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

                _SQL = "select * from tbl_editora where cnpj = '" + cnpj + "';";

                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Usuarios");

                if (objdatasert_usu.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Este CNPJ já foi cadastrado!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int numTryParse = 0;
            Verificar(msk_cnpj.Text);
            if (tb_editora.Text == "" || tb_nomeFantasia.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_existe == 0)
                {
                    string sql = "insert into tbl_editora values('','" + tb_editora.Text + "','" + tb_nomeFantasia.Text + "','" + msk_cnpj.Text + "','" + tb_site.Text + "','" + tb_contato.Text + "','" + tb_proficional.Text + "','" + tb_telefonePrincipal.Text + "','" + tb_fax.Text + "','" + tb_cep.Text + "','" + tb_logradouro.Text + "','" + tb_cidade.Text + "','" + tb_bairro.Text + "','" + int.TryParse(tb_numero.Text, out numTryParse) + "','" + tb_complemento.Text + "','" + cb_uf.Text + "');";
                    EX_CMD inserir = new EX_CMD();
                    inserir.ExecutarSQL(sql, "Editora cadastrada com sucesso!");
                    this.Close();
                }
                else
                {

                }
            }
        }
    }
}
