using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class ENCRIPTOGRAFAR_SENHAS
    {
        public static string EncriptografarSenhas(string input)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] Bytes_de_entrada = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(Bytes_de_entrada);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {

                    sb.Append(hash[i].ToString("X2"));

                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return MessageBox.Show("" + ex.Message).ToString();
            }

        }
    }
}
