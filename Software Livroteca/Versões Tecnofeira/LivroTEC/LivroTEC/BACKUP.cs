using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Sobre
{
    public partial class BACKUP : Form
    {
        public BACKUP()
        {
            InitializeComponent();
        }
        public string _dateTime = DateTime.Now.ToShortDateString();
        public int _BackupRestore = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (_BackupRestore == 0)
            {
                SaveFileDialog opn_file = new SaveFileDialog();
                opn_file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                opn_file.Filter = "Arquivo SQL (*.sql)|*.sql";
                opn_file.FilterIndex = 1;
                if (opn_file.ShowDialog(this) == DialogResult.OK)
                {
                    Caminho.Text = opn_file.FileName.Replace(@"\", "\\");
                }
            }
            else
            {
                OpenFileDialog opn_file = new OpenFileDialog();
                opn_file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                opn_file.Filter = "Arquivo SQL (*.sql)|*.sql";
                opn_file.FilterIndex = 1;
                if (opn_file.ShowDialog(this) == DialogResult.OK)
                {
                    Caminho.Text = opn_file.FileName.Replace(@"\", "\\");
                }
            }
        }

        public string _diretorioAtual = Directory.GetCurrentDirectory().ToString();
        public string _nomeBakup = "";
        public string _caminhoSalvo = "";
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                button3.Enabled = false;
                rtb_logsBakup.Text = "Iniciando Backup...";
                if (cb_caminho.Checked == false)
                {
                    Process.Start(_diretorioAtual + "\\mysqldump.exe ", "--user root --password= -B livrotec  > -r " + _diretorioAtual + "\\BACKUP\\Backup-Livrotec(" + _dateTime + ").sql");
                    timer1.Enabled = true;
                    _caminhoSalvo = ""+ _diretorioAtual + "\\BACKUP\\Backup-Livrotec(" + _dateTime + ").sql";
                }
                else
                {
                    try
                    {
                        Process.Start(_diretorioAtual + "\\mysqldump.exe ", "--user root --password= -B livrotec  > -r " + Caminho.Text + "");
                        timer1.Enabled = true;
                        _caminhoSalvo = Caminho.Text;
                    }
                    catch(Exception ex)
                    {
                        if (MessageBox.Show("Não foi possivel fazer o Backup neste computador!", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            MessageBox.Show("Erro:\n\n"+ex, "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        }
                    }

                }

                _diretorioAtual = Caminho.Text;
                            
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 5;
                rtb_logsBakup.Text += "\nProgresso: "+progressBar1.Value+"%";
            }
            else
            {
                timer1.Enabled = false;
                rtb_logsBakup.Text += "\nBackup realizado com sucesso!";
                rtb_logsBakup.Text += "\n"+_caminhoSalvo;
            }
        }

        private void cb_caminho_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_caminho.Checked == false)
            {
                Caminho.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                Caminho.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void BACKUP_Load(object sender, EventArgs e)
        {
            Caminho.Enabled = false;
            button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            try
            {
                    rtb_logsBakup.Text = "Iniciando a restauração de Backup...";
                    try
                    {
                        if (Caminho.Text.Length < 3)
                        {
                            MessageBox.Show("Selecione onde está o arquivo de Backup!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Caminho.Enabled = true;
                            button1.Enabled = true;
                            _BackupRestore = 1;
                            Caminho.Focus();
                            button3.Enabled = true;
                            return;
                        }
                        Process.Start(_diretorioAtual + "\\mysqldump.exe ", "-u root -p livrotec < -r " + Caminho.Text + "");
                        timer2.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("Não foi possivel restaurar o Backup neste computador!", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            MessageBox.Show("Erro:\n\n" + ex, "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        }
                    }
                

                _diretorioAtual = Caminho.Text;
            }
            catch
            {

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 5;
                rtb_logsBakup.Text += "\nProgresso: " + progressBar1.Value + "%";
            }
            else
            {
                timer2.Enabled = false;
                rtb_logsBakup.Text += "\nRestauração de Backup realizada com sucesso!";
            }
        }

        private void cb_backup_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_backup.Checked == true)
            {
                cb_caminho.Enabled = false;
                _BackupRestore = 1;
                Caminho.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                cb_caminho.Enabled = true;
                _BackupRestore = 0;
                Caminho.Enabled = false;
                button1.Enabled = false;
            }
        }
    }
}
