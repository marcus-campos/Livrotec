using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class RecuperarSenha
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public bool Rec_Senha(string Login, string cod_seguranca)
        {
            bool retn = false;

            try
            {
                objconexao.Open();
            }
            catch
            {
                MessageBox.Show("Não foi possível conectar-se ao banco de dados. Favor conferir se o banco de dados está corretamente configurado, e online.", "Erro!");
                retn = false;
            }

            if (objconexao.State == System.Data.ConnectionState.Open)
            {
                string SQL = "SELECT * FROM tbl_usuario WHERE COD_SEGURANCA = '" + cod_seguranca + "'AND LOGIN = '" + Login + "' LIMIT 1;";
                MySqlDataAdapter objadapter = new MySqlDataAdapter(SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "RecuperaSenha");

                if (objdataset.Tables[0].Rows.Count > 0)
                {
                    retn = true;
                }
                else
                {
                    MessageBox.Show("Login ou Codigo de segurança inválidos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    retn = false;
                }
                objconexao.Close();
            }
            return retn;


        }
    }
}
