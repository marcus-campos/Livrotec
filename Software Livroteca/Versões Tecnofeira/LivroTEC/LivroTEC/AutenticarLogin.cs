using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class AutenticarLogin
    {
        public static int cod_usu = 0;
        public static int tipo = 0;
        public static string _nome_usuario = "";
        public static string _cod_seguranca = "";
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public bool Autenticar(string Login, string Senha)
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
                string SQL = "SELECT * FROM tbl_usuario WHERE LOGIN = '" + Login + "'AND SENHA = '" + Senha + "' LIMIT 1;";
                MySqlDataAdapter objadapter = new MySqlDataAdapter(SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "usuarios");
                
                if (objdataset.Tables[0].Rows.Count > 0)
                {
                   tipo = int.Parse(objdataset.Tables[0].Rows[0]["tipo"].ToString());
                   _nome_usuario = objdataset.Tables[0].Rows[0]["nome"].ToString();
                   _cod_seguranca = objdataset.Tables[0].Rows[0]["cod_seguranca"].ToString();
                   retn = true;
                }
                else
                {
                    MessageBox.Show("Senha ou usuario incorretos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    retn = false;
                }
                objconexao.Close();
            }
            return retn;


        }
    }
}
