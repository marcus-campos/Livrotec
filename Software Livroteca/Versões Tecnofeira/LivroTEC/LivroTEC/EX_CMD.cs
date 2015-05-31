using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApplication1
{
    class EX_CMD
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public static int ExibirMenssagem = 0;
        public void ExecutarSQL(string _SQL, string _MENSSAGEM)
        {
            try
            {
                objconexao.Open();
            }
            catch
            {
                MessageBox.Show("Não foi possivel estabelecer uma conexão com o banco de dados!","Erro!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand SQL_CMD = new MySqlCommand(_SQL, objconexao);
                SQL_CMD.ExecuteNonQuery();
                if (ExibirMenssagem == 0)
                {
                    MessageBox.Show("" + _MENSSAGEM, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                }
            }
            objconexao.Close();
        }
    }
}
