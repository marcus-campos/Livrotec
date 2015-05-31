using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections;


namespace WindowsFormsApplication1
{
    public partial class telaPrincipal : Form
    {
        MySqlConnection objconexao = new MySqlConnection(Program.ConectionString);
        public int emprestimo = 0, acervo = 0, leitores = 0, tipoacervo = 0, editor = 0, idioma = 0, autor = 0, genero = 0, usuario = 0;
        public int _codigo_usuario = 0;
        public int PesquisaTodos = 0;
        public telaPrincipal()
        {
            InitializeComponent();
        }

        private void telaPrincipal_Load(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_emprestimos);
            tabControlPrincipal.TabPages.Remove(this.tab_acervo);
            tabControlPrincipal.TabPages.Remove(this.tab_leitores);
            tabControlPrincipal.TabPages.Remove(this.tab_tipo_acervo);
            tabControlPrincipal.TabPages.Remove(this.tab_editora);
            tabControlPrincipal.TabPages.Remove(this.tab_idioma);
            tabControlPrincipal.TabPages.Remove(this.tab_autores);
            tabControlPrincipal.TabPages.Remove(this.tab_generos);
            tabControlPrincipal.TabPages.Remove(this.tab_usuarios);
            tabControlEmprestimo.TabPages.Remove(this.tab_emprestimos_clientes);

            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
            else
            {

            }

            TV_emprestimo_clientes.ImageList = img_lst;

            if (AutenticarLogin.tipo == 1)
            {
                btnUsuario.Enabled = true;
                TSL_TIPO.Text = "Admistrador";
            }
            else
            {
                btnUsuario.Enabled = false;
                TSL_TIPO.Text = "Comum";
            }

            this.Text = "Livro TEC";
            TSL_VERSAO.Text = "1.0.N";
            if (AutenticarLogin._nome_usuario.Length >= 15)
            {
                TSL_UsuarioLogado.Text = AutenticarLogin._nome_usuario.Substring(0, 14) + "...";
            }
            else
                if (AutenticarLogin._nome_usuario.Length < 15)
                {
                    TSL_UsuarioLogado.Text = AutenticarLogin._nome_usuario;
                }
            TSL_InicioSessao.Text = DateTime.Now.ToString();


            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                TSL_CPSLOCK.Text = "LIGADO";
                TSL_CPSLOCK.BackColor = Color.Green;
            }
            else
            {
                TSL_CPSLOCK.Text = "DESLIGADO";
                TSL_CPSLOCK.BackColor = Color.Red;
            }
            
            
            

            
        }
        
        //----------------Emprestimo-----------------------

        private void btnEmprestimo_Click(object sender, EventArgs e)
        {
            if (emprestimo == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_emprestimos);
                tabControlPrincipal.SelectTab(tab_emprestimos);
                emprestimo = 1;
                PesquisaTodos = 1;
                PesquisaEmprestimo();
                PesquisaTodos = 0;
                PesquisaHistorico();
                CarregaCboxAcervos();
                CarregaCboxLeitores();
                PreencherTreeView();
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_emprestimos);
            }
        }
        public ArrayList tabelaArray;
        private void GetBancoDeDados()
        {

            try
            {
                //abre a conexão
                try
                {
                    objconexao.Open();
                }
                catch
                {

                }
                MySqlDataAdapter objdatadapter = new MySqlDataAdapter("select cliente from tbl_cliente ORDER BY cliente asc;", objconexao);
                DataTable objdatable = new DataTable();
                objdatadapter.Fill(objdatable);
                //obtem informações do esquema (nome das tabelas do banco de dados)

                tabelaArray = new ArrayList();
                //preenche o array com  o nome das tabelas
                foreach (DataRow datRow in objdatable.Rows)
                {
                    tabelaArray.Add(datRow[0].ToString());
                }
                objconexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void PreencherTreeView()
        {
            TV_emprestimo_clientes.Nodes.Clear();
            // define o no raiz para o TreeView.
            TV_emprestimo_clientes.Nodes.Add("Clientes");
            TV_emprestimo_clientes.Nodes[0].ImageIndex = 0;
            TV_emprestimo_clientes.Nodes[0].SelectedImageIndex = 0;


            GetBancoDeDados();


            for (int i = 0; i < tabelaArray.Count; i++)
            {
                TV_emprestimo_clientes.Nodes[0].Nodes.Add(tabelaArray[i].ToString());                
                TV_emprestimo_clientes.Nodes[0].Nodes[i].ImageIndex = 99;
                TV_emprestimo_clientes.Nodes[0].Nodes[i].SelectedImageIndex = 99;
                
            }

        }
        
        public class Cliente
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }

        public void CarregaCboxLeitores()
        {           
            ListaLeitores();
            cb_emprestimo_leitor.DisplayMember = "Nome";
            cb_emprestimo_leitor.ValueMember = "Id";
            cb_emprestimo_leitor.DataSource = ListaLeitores();            
        }

        private List<Cliente> ListaLeitores()
        {         
            List<Cliente> lista = new List<Cliente>();

            string query = "SELECT cod_leitor,cliente FROM tbl_cliente;";

            try
            {
                objconexao.Open();
            }
            catch
            {
                
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Cliente c = new Cliente();
                        c.Id = Convert.ToInt32(leitor["cod_leitor"]);
                        c.Nome = leitor["cliente"].ToString();
                        lista.Add(c);
                    }
                }               
               
            }
            objconexao.Close();
            return lista;
        }
        public int _codigo_acervo = 0;
        public int _emprestimo = 0;
        public int _tipo_Acervo = 0;
        private void button22_Click(object sender, EventArgs e)
        {
            if (tb_emprestimos_cod_bar.Text == "")
            {
                MessageBox.Show("Insira um codigo de barras!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (cb_emprestimo_leitor.Text == "")
            {
                MessageBox.Show("Selecione um leitor valido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string status = "SELECT status_acervo FROM tbl_acervo WHERE codigobarra = '" + tb_emprestimos_cod_bar.Text + "' ;";
            MySqlDataAdapter statusadapter = new MySqlDataAdapter(status, objconexao);
            DataSet statusdataset = new DataSet();
            statusadapter.Fill(statusdataset);
            string statusAcervo = statusdataset.Tables[0].Rows[0]["status_acervo"].ToString();
            if (statusAcervo == "Locado")
            {
                MessageBox.Show("Este livro já foi locado por outra pessoa!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
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
                string sql = "Select cod_acervo,tbl_tipo_acervo_cod_tipo_acervo from tbl_acervo where codigobarra = '" + tb_emprestimos_cod_bar.Text + "';";                
                MySqlDataAdapter objadapter = new MySqlDataAdapter(sql,objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset);


                if (objdataset.Tables[0].Rows.Count > 0)
                {
                    _codigo_acervo = int.Parse(objdataset.Tables[0].Rows[0]["cod_acervo"].ToString());
                    _tipo_Acervo = int.Parse(objdataset.Tables[0].Rows[0]["tbl_tipo_acervo_cod_tipo_acervo"].ToString());
                }

                string sql1 = "Select emprestimo from tbl_tipo_acervo where cod_tipo_acervo = '" + _tipo_Acervo + "';";                
                MySqlDataAdapter objadapter1 = new MySqlDataAdapter(sql1,objconexao);
                DataSet objdataset1 = new DataSet();
                objadapter1.Fill(objdataset1);

                if (objdataset1.Tables[0].Rows.Count > 0)
                {
                    _emprestimo = int.Parse(objdataset1.Tables[0].Rows[0]["emprestimo"].ToString());
                }
            }
            objconexao.Close();

            
            string Insert_SQL = "Insert into tbl_emprestimo values('','"+_codigo_acervo+"','"+cb_emprestimo_leitor.SelectedValue+"','"+DateTime.Now.ToString("yyyy-MM-dd")+"','"+DateTime.Now.AddDays(emprestimo).ToString("yyyy-MM-dd")+"');";
            string Update = "Update tbl_acervo set status_acervo = 'Locado' where cod_acervo = '"+_codigo_acervo+"';";
            EX_CMD inserir = new EX_CMD();
            EX_CMD.ExibirMenssagem = 1;
            inserir.ExecutarSQL(Update, "");
            EX_CMD.ExibirMenssagem = 0;
            inserir.ExecutarSQL(Insert_SQL, "Emprestimo cadastrado com sucesso!");
            PesquisaEmprestimo();
            tabControlEmprestimo.SelectTab(tabPagePendenteEmprestimo);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (cb_emprestimo_leitor.Text == "")
            {
                MessageBox.Show("Selecione um leitor valido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string status = "SELECT status_acervo FROM tbl_acervo WHERE codigobarra = '" + tb_emprestimos_cod_bar.Text + "' ;";
            MySqlDataAdapter statusadapter = new MySqlDataAdapter(status, objconexao);
            DataSet statusdataset = new DataSet();
            statusadapter.Fill(statusdataset);
            string statusAcervo = statusdataset.Tables[0].Rows[0]["status_acervo"].ToString();
            if (statusAcervo == "Locado")
            {
                MessageBox.Show("Este livro já foi locado por outra pessoa!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {

            }

            if (cb_emprestimo_leitor.Text == "")
            {
                MessageBox.Show("Selecione um leitor valido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_emprestimos_nomeLivro.Text == "")
            {
                MessageBox.Show("Selecione um Acervo!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
                string sql = "Select cod_acervo,tbl_tipo_acervo_cod_tipo_acervo from tbl_acervo where titulo like '" + cb_emprestimo_leitor.Text + "%';";
                MySqlDataAdapter objadapter = new MySqlDataAdapter(sql, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset);


                if (objdataset.Tables[0].Rows.Count > 0)
                {
                    _codigo_acervo = int.Parse(objdataset.Tables[0].Rows[0]["cod_acervo"].ToString());
                    _tipo_Acervo = int.Parse(objdataset.Tables[0].Rows[0]["tbl_tipo_acervo_cod_tipo_acervo"].ToString());
                }

                string sql1 = "Select emprestimo from tbl_tipo_acervo where cod_tipo_acervo = '" + _tipo_Acervo + "';";
                MySqlDataAdapter objadapter1 = new MySqlDataAdapter(sql1, objconexao);
                DataSet objdataset1 = new DataSet();
                objadapter1.Fill(objdataset1);

                if (objdataset1.Tables[0].Rows.Count > 0)
                {
                    _emprestimo = int.Parse(objdataset1.Tables[0].Rows[0]["emprestimo"].ToString());
                }
            }
            objconexao.Close();


            string Insert_SQL = "Insert into tbl_emprestimo values('','" + _codigo_acervo + "','" + cb_emprestimo_leitor.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.AddDays(emprestimo).ToString("yyyy-MM-dd") + "');";
            string Update = "Update tbl_acervo set status_acervo = 'Locado' where cod_acervo = '" + _codigo_acervo + "';";
            EX_CMD inserir = new EX_CMD();
            EX_CMD.ExibirMenssagem = 1;
            inserir.ExecutarSQL(Update,"");
            EX_CMD.ExibirMenssagem = 0;
            inserir.ExecutarSQL(Insert_SQL, "Emprestimo cadastrado com sucesso!");
            PesquisaEmprestimo();
            tabControlEmprestimo.SelectTab(tabPagePendenteEmprestimo);
        }

        private void btnFechaEmprestimo_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_emprestimos);
            emprestimo = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        //----------------Acervo-----------------------


        private void btnAcervo_Click(object sender, EventArgs e)
        {
            if (acervo == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_acervo);
                tabControlPrincipal.SelectTab(tab_acervo);
                PesquisaTodos = 1;
                PesquisaAcervo();
                PesquisaTodos = 0;
                CarregaCBautores();
                CarregaCBeditora();
                CarregaCBgenero();
                CarregaCBidioma();
                CarregaCBtipoDeAcervos();
                tb_acervo_exemplar.Enabled = false;
                acervo = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_acervo);
            }
        }

        private void tb_acervo_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaAcervo();
        }

        private void btnPesquisaAcervo_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaAcervo();
            PesquisaTodos = 0;
        }

        public void PesquisaAcervo()
        {

            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                    //MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {
                string _SQL = "";
                if (PesquisaTodos == 0)
                {
                    _SQL = "select * from tbl_acervo where titulo like '" + tb_acervo_pesquisa.Text + "%';";                    
                }
                else
                {
                    _SQL = "Select * from tbl_acervo;";
                }

                try
                {
                    MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                    DataSet objdataset = new DataSet();
                    objadapter.Fill(objdataset, "Acervo");

                    dg_acervo_pesquisa.DataSource = objdataset;
                    dg_acervo_pesquisa.DataMember = "Acervo";

                    dg_acervo_pesquisa.Columns[0].HeaderText = "Código do Acervo";
                    dg_acervo_pesquisa.Columns[1].HeaderText = "Código do Autor";
                    dg_acervo_pesquisa.Columns[2].HeaderText = "Código do Genero";
                    dg_acervo_pesquisa.Columns[3].HeaderText = "Código do Tipo do Acervo";
                    dg_acervo_pesquisa.Columns[4].HeaderText = "Código do Idioma";
                    dg_acervo_pesquisa.Columns[5].HeaderText = "Código da Editora";
                    dg_acervo_pesquisa.Columns[6].HeaderText = "Titulo";
                    dg_acervo_pesquisa.Columns[7].HeaderText = "Sub-Titulo";
                    dg_acervo_pesquisa.Columns[8].HeaderText = "Data da Aquisicao";
                    dg_acervo_pesquisa.Columns[9].HeaderText = "Exemplar";
                    dg_acervo_pesquisa.Columns[10].HeaderText = "Volume";
                    dg_acervo_pesquisa.Columns[11].HeaderText = "Edição";
                    dg_acervo_pesquisa.Columns[12].HeaderText = "Data da Edição";
                    dg_acervo_pesquisa.Columns[13].HeaderText = "Observações";
                    dg_acervo_pesquisa.Columns[14].HeaderText = "Número de Paginas";
                    dg_acervo_pesquisa.Columns[15].HeaderText = "Preço";
                    dg_acervo_pesquisa.Columns[16].HeaderText = "CDD";
                    dg_acervo_pesquisa.Columns[17].HeaderText = "CDU";
                    dg_acervo_pesquisa.Columns[18].HeaderText = "Cutter";
                    dg_acervo_pesquisa.Columns[19].HeaderText = "Isbn";
                    dg_acervo_pesquisa.Columns[20].HeaderText = "Status do Acervo";
                    dg_acervo_pesquisa.Columns[21].HeaderText = "Referêrencia Blibliografica";
                    dg_acervo_pesquisa.Columns[22].HeaderText = "Resenha";
                    dg_acervo_pesquisa.Columns[23].HeaderText = "Periodicidade";
                    dg_acervo_pesquisa.Columns[24].HeaderText = "Código de Barra";

                    _ErroPesquisa = 0;
                }
                catch
                {

                }
            }
            objconexao.Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string statusAcervo = "";
            if (rb_acervo_locado.Checked == true)
            {
                statusAcervo = "Locado";
            }
            if (rb_acervo_disponivel.Checked == true)
            {
                statusAcervo = "Disponivel";
            }
            string sql = "UPDATE tbl_acervo SET tbl_autor_cod_autor = '" + cb_acervo_autor.Text + "', tbl_genero_cod_genero = '" + cb_acervo_genero.Text + "', tbl_tipo_acervo_cod_tipo_acervo = '" + cb_acervo_tipoDeAcervo.Text + "', tbl_idioma_cod_idioma = '" + cb_acervo_idioma.Text + "', tbl_editora_cod_editora = '" + cb_acervo_editora.Text + "', titulo = '" + tb_acervo_titulo.Text + "', sub_titulo = '" + tb_acervo_subTitulo.Text + "', data_aquisicao = '" + dtp_acervo_dataAquisicao.Value.ToString("yyyy-MM-dd") + "', exemplar = '" + tb_acervo_exemplar.Text + "', volume = '" + tb_acervo_volume.Text + "', edicao = '" + tb_acervo_edicao.Text + "', data_edicao = '" + dtp_acervo_dataEdicao.Value.ToString("yyyy-MM-dd") + "', observacoes = '" + tb_acervo_observacoes.Text + "', numero_paginas = '" + tb_acervo_numPaginas.Text + "', preco = '" + tb_acervo_preco.Text + "', cdd = '" + tb_acervo_cdd.Text + "', cdu = '" + tb_acervo_cdu.Text + "', cutter = '" + tb_acervo_cutter.Text + "' isbn = '" + tb_acervo_isbn.Text + "', status_acervo = '" + statusAcervo + "', referencia_bibliografica = '" + tb_acervo_refBibliografica.Text + "', resenha = '" + tb_acervo_resenha.Text + "', periodicidade = '" + cb_acervo_periodicidade.Text + "', codigobarra = '" + tb_acervo_codBarra.Text + "' WHERE cod_acervo = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[0].Value.ToString() + "';";
            EX_CMD update = new EX_CMD();
            update.ExecutarSQL(sql, "Acervo atualizado com sucesso!");
            PesquisaTodos = 1;
            PesquisaAcervo();
            PesquisaTodos = 0;
            tb_acervo_exemplar.Enabled = true;
            tb_acervo_cdd.Text = "";
            tb_acervo_cdu.Text = "";
            tb_acervo_codBarra.Text = "";
            tb_acervo_cutter.Text = "";
            tb_acervo_edicao.Text = "";
            tb_acervo_exemplar.Text = "";
            tb_acervo_isbn.Text = "";
            tb_acervo_numPaginas.Text = "";
            tb_acervo_observacoes.Text = "";
            tb_acervo_preco.Text = "";
            tb_acervo_refBibliografica.Text = "";
            tb_acervo_resenha.Text = "";
            tb_acervo_subTitulo.Text = "";
            tb_acervo_titulo.Text = "";
            tb_acervo_volume.Text = "";
            cb_acervo_autor.Text = "";
            cb_acervo_editora.Text = "";
            cb_acervo_genero.Text = "";
            cb_acervo_idioma.Text = "";
            cb_acervo_periodicidade.Text = "";
            cb_acervo_tipoDeAcervo.Text = "";
            dtp_acervo_dataAquisicao.Text = "";
            dtp_acervo_dataEdicao.Text = "";
            _codigo_acervo = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM tbl_acervo WHERE cod_acervo = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[0].Value.ToString() + "';";
            EX_CMD delete = new EX_CMD();
            delete.ExecutarSQL(sql, "Acervo deletado com sucesso!");
            PesquisaTodos = 1;
            PesquisaAcervo();
            PesquisaTodos = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
                EX_CMD INSERIR_ACERVO = new EX_CMD();
                string statusAcervo = "";
                if (rb_acervo_locado.Checked == true)
                {
                    statusAcervo = "Locado";
                }
                if (rb_acervo_disponivel.Checked == true)
                {
                    statusAcervo = "Disponivel";
                }
                else
                    if (rb_acervo_locado.Checked == false && rb_acervo_locado.Checked == false)
                {
                    MessageBox.Show("Selecione o estado do acervo!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (nUD_acervo_quantidadeDeExemplares.Value < 2)
                {
                    string exemplar = "Exemplar 1";
                    string sql1 = "Insert into tbl_acervo values ('','" + cb_acervo_autor.SelectedValue + "','" + cb_acervo_genero.SelectedValue + "','" + cb_acervo_tipoDeAcervo.SelectedValue + "','" + cb_acervo_idioma.SelectedValue + "','" + cb_acervo_editora.SelectedValue + "','" + tb_acervo_titulo.Text + "','" + tb_acervo_subTitulo.Text + "','" + dtp_acervo_dataAquisicao.Value.ToString("yyyy-MM-dd") + "','" + exemplar + "','" + tb_acervo_volume.Text + "','" + tb_acervo_edicao.Text + "','" + dtp_acervo_dataEdicao.Value.ToString("yyyy-MM-dd") + "','" + tb_acervo_observacoes.Text + "','" + tb_acervo_numPaginas.Text + "','" + tb_acervo_preco.Text + "','" + tb_acervo_cdd.Text + "','" + tb_acervo_cdu.Text + "','" + tb_acervo_cutter.Text + "','" + tb_acervo_isbn.Text + "','" + statusAcervo + "','" + tb_acervo_refBibliografica.Text + "','" + tb_acervo_resenha.Text + "','" + cb_acervo_periodicidade.Text + "','" + tb_acervo_codBarra.Text + "');";
                    EX_CMD inserir1 = new EX_CMD();
                    inserir1.ExecutarSQL(sql1, "Acervo inserido com sucesso!");
                }
                else
                {
                    for (int x = 0; x < nUD_acervo_quantidadeDeExemplares.Value; x++)
                    {
                        string exemplar = "Exemplar " + (x + 1);
                        string sql = "Insert into tbl_acervo values ('','" + cb_acervo_autor.SelectedValue + "','" + cb_acervo_genero.SelectedValue + "','" + cb_acervo_tipoDeAcervo.SelectedValue + "','" + cb_acervo_idioma.SelectedValue.ToString() + "','" + cb_acervo_editora.SelectedValue.ToString() + "','" + tb_acervo_titulo.Text + "','" + tb_acervo_subTitulo.Text + "','" + dtp_acervo_dataAquisicao.Value.ToString("yyyy-MM-dd") + "','" + exemplar + "','" + tb_acervo_volume.Text + "','" + tb_acervo_edicao.Text + "','" + dtp_acervo_dataEdicao.Value.ToString("yyyy-MM-dd") + "','" + tb_acervo_observacoes.Text + "','" + tb_acervo_numPaginas.Text + "','" + tb_acervo_preco.Text + "','" + tb_acervo_cdd.Text + "','" + tb_acervo_cdu.Text + "','" + tb_acervo_cutter.Text + "','" + tb_acervo_isbn.Text + "','" + statusAcervo + "','" + tb_acervo_refBibliografica.Text + "','" + tb_acervo_resenha.Text + "','" + cb_acervo_periodicidade.Text + "','" + tb_acervo_codBarra.Text + "');";
                        EX_CMD inserir = new EX_CMD();
                        EX_CMD.ExibirMenssagem = 1;
                        inserir.ExecutarSQL(sql, "Acervo inserido com sucesso!");
                    }
                    EX_CMD.ExibirMenssagem = 0;
                    MessageBox.Show("Acervo cadastrado com sucesso!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaCboxAcervos();
                }
                PesquisaTodos = 1;
                PesquisaAcervo();
                PesquisaTodos = 0;
                tb_acervo_cdd.Text = "";
                tb_acervo_cdu.Text = "";
                tb_acervo_codBarra.Text = "";
                tb_acervo_cutter.Text = "";
                tb_acervo_edicao.Text = "";
                tb_acervo_exemplar.Text = "";
                tb_acervo_isbn.Text = "";
                tb_acervo_numPaginas.Text = "";
                tb_acervo_observacoes.Text = "";
                tb_acervo_preco.Text = "";
                tb_acervo_refBibliografica.Text = "";
                tb_acervo_resenha.Text = "";
                tb_acervo_subTitulo.Text = "";
                tb_acervo_titulo.Text = "";
                tb_acervo_volume.Text = "";
                cb_acervo_autor.Text = "";
                cb_acervo_editora.Text = "";
                cb_acervo_genero.Text = "";
                cb_acervo_idioma.Text = "";
                cb_acervo_periodicidade.Text = "";
                cb_acervo_tipoDeAcervo.Text = "";
                dtp_acervo_dataAquisicao.Text = "";
                dtp_acervo_dataEdicao.Text = "";
           
        }

        public class Autor
        {
            public int Cod_atores { get; set; }
            public string Autores { get; set; }
        }

        public void CarregaCBautores()
        {
            ListaAutores();
            cb_acervo_autor.DisplayMember = "Autores";
            cb_acervo_autor.ValueMember = "Cod_atores";
            cb_acervo_autor.DataSource = ListaAutores();
        }

        private List<Autor> ListaAutores()
        {
            List<Autor> lista = new List<Autor>();

            string query = "SELECT cod_autor,autor from tbl_autor;";

            try
            {
                objconexao.Open();
            }
            catch
            {
                //MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Autor c = new Autor();
                        c.Cod_atores = Convert.ToInt32(leitor["cod_autor"]);
                        c.Autores = leitor["autor"].ToString();
                        lista.Add(c);
                    }
                }

            }
            objconexao.Close();
            return lista;
        }

        public class Editora
        {
            public int Cod_editoras { get; set; }
            public string Editoras { get; set; }
        }

        public void CarregaCBeditora()
        {
            ListaEditora();
            cb_acervo_editora.DisplayMember = "Editoras";
            cb_acervo_editora.ValueMember = "Cod_editoras";
            cb_acervo_editora.DataSource = ListaEditora();
        }

        private List<Editora> ListaEditora()
        {
            List<Editora> lista = new List<Editora>();

            string query = "SELECT cod_editora,razao_social from tbl_editora;";

            try
            {
                objconexao.Open();
            }
            catch
            {
               // MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Editora c = new Editora();
                        c.Cod_editoras = Convert.ToInt32(leitor["cod_editora"]);
                        c.Editoras = leitor["razao_social"].ToString();
                        lista.Add(c);
                    }
                }

            }
            objconexao.Close();
            return lista;
        }

        public class TipoDeAcervo
        {
            public int Cod_TipoDeAcervos { get; set; }
            public string TipoDeAcervos { get; set; }
        }

        public void CarregaCBtipoDeAcervos()
        {
            ListaTipoDeAcervo();
            cb_acervo_tipoDeAcervo.DisplayMember = "TipoDeAcervos";
            cb_acervo_tipoDeAcervo.ValueMember = "Cod_TipoDeAcervos";
            cb_acervo_tipoDeAcervo.DataSource = ListaTipoDeAcervo();
        }

        private List<TipoDeAcervo> ListaTipoDeAcervo()
        {
            List<TipoDeAcervo> lista = new List<TipoDeAcervo>();

            string query = "SELECT cod_tipo_acervo,tipo_acervo from tbl_tipo_acervo;";

            try
            {
                objconexao.Open();
            }
            catch
            {
               // MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        TipoDeAcervo c = new TipoDeAcervo();
                        c.Cod_TipoDeAcervos = Convert.ToInt32(leitor["cod_tipo_acervo"]);
                        c.TipoDeAcervos = leitor["tipo_acervo"].ToString();
                        lista.Add(c);
                    }
                }

            }
            objconexao.Close();
            return lista;
        }

        public class Idioma
        {
            public int Cod_idiomas { get; set; }
            public string Idiomas { get; set; }
        }

        public void CarregaCBidioma()
        {
            ListaIdioma();
            cb_acervo_idioma.DisplayMember = "Idiomas";
            cb_acervo_idioma.ValueMember = "Cod_idiomas";
            cb_acervo_idioma.DataSource = ListaIdioma();
        }

        private List<Idioma> ListaIdioma()
        {
            List<Idioma> lista = new List<Idioma>();

            string query = "SELECT cod_idioma,idioma from tbl_idioma;";

            try
            {
                objconexao.Open();
            }
            catch
            {
               // MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Idioma c = new Idioma();
                        c.Cod_idiomas = Convert.ToInt32(leitor["cod_idioma"]);
                        c.Idiomas = leitor["idioma"].ToString();
                        lista.Add(c);
                    }
                }

            }
            objconexao.Close();
            return lista;
        }

        public class Genero
        {
            public int Cod_generos { get; set; }
            public string Generos { get; set; }
        }

        public void CarregaCBgenero()
        {
            ListaGenero();
            cb_acervo_genero.DisplayMember = "Generos";
            cb_acervo_genero.ValueMember = "Cod_generos";
            cb_acervo_genero.DataSource = ListaGenero();            
        }

        private List<Genero> ListaGenero()
        {
            List<Genero> lista = new List<Genero>();

            string query = "SELECT cod_genero,genero from tbl_genero;";

            try
            {
                objconexao.Open();
            }
            catch
            {
               // MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Genero c = new Genero();
                        c.Cod_generos = Convert.ToInt32(leitor["cod_genero"]);
                        c.Generos = leitor["genero"].ToString();
                        lista.Add(c);
                    }
                }

            }
            objconexao.Close();
            return lista;
        }

        private void btnFechaAcervo_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_acervo);
            acervo = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        //----------------Leitores-----------------------

        private void btnLeitores_Click(object sender, EventArgs e)
        {
            if (leitores == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_leitores);
                tabControlPrincipal.SelectTab(tab_leitores);
                PesquisaTodos = 1;
                PesquisaLeitores();
                PesquisaTodos = 0;
                leitores = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_leitores);
            }
        }

        private void tb_leitores_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaLeitores();
        }

        private void btnFechaLeitor_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_leitores);
            leitores = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        public int _existe = 0;
        public void Verificar(string cpf)
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

                _SQL = "select * from tbl_cliente where cpj = '" + cpf + "';";

                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Usuarios");

                if (objdatasert_usu.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Este CPF de acervo já foi cadastrado!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _existe = 1;
                }
                else
                {
                    _existe = 0;
                }

            }
            objconexao.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            string rg = "";
            string cpf = "";
            rg = msk_leitores_rg.Text;
            cpf = msk_leitores_cpf.Text;
            rg.Replace(',', '.');
            cpf.Replace(',', '.');
            string sexo = "";
            Verificar(msk_leitores_cpf.Text);
            if (rb_leitores_masculino.Checked == true)
            {
                sexo = "M";
            }
            if (rb_leitores_feminino.Checked == true)
            {
                sexo = "F";
            }
            
            if ( tb_leitores_nomeLeitor.Text =="" || dt_leitores_dataCadastro.Text == "" || sexo =="")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_existe == 0)
                {
                    string sql = "INSERT into tbl_cliente values('','" + tb_leitores_nomeLeitor.Text + "','" + rg + "','" + cpf + "','" + dt_leitores_dataNascimento.Value.ToString("yyyy-MM-dd") + "','" + dt_leitores_dataCadastro.Value.ToString("yyyy-MM-dd") + "','" + sexo + "','" + tb_leitores_responsavel.Text + "','" + tb_leitores_observacoes.Text + "','" + tb_leitores_matricula.Text + "','" + tb_leitores_turma.Text + "','" + tb_leitores_periodo.Text + "','" + tb_leitores_multa.Text + "','" + tb_leitores_email.Text + "','" + tb_leitores_cep.Text + "','" + tb_leitores_logradouro.Text + "','" + tb_leitores_cidade.Text + "','" + tb_leitores_bairro.Text + "','" + tb_leitores_numero.Text + "','" + tb_leitores_complemento.Text + "','" + cb_leitores_uf.Text + "','" + tb_leitores_residencial.Text + "','" + tb_leitores_celular.Text + "');";
                    EX_CMD inserir = new EX_CMD();
                    inserir.ExecutarSQL(sql, "Leitor inserido com sucesso!");
                    PreencherTreeView();
                    PesquisaTodos = 1;
                    PesquisaLeitores();
                    PesquisaTodos = 0;
                    tb_leitores_multa.Text = "";
                    tb_leitores_matricula.Text = "";
                    tb_leitores_logradouro.Text = "";
                    tb_leitores_email.Text = "";
                    tb_leitores_complemento.Text = "";
                    tb_leitores_cidade.Text = "";
                    tb_leitores_cep.Text = "";
                    tb_leitores_celular.Text = "";
                    tb_leitores_bairro.Text = "";
                    tb_leitores_nomeLeitor.Text = "";
                    tb_leitores_numero.Text = "";
                    tb_leitores_observacoes.Text = "";
                    tb_leitores_periodo.Text = "";
                    tb_leitores_residencial.Text = "";
                    tb_leitores_responsavel.Text = "";
                    tb_leitores_turma.Text = "";
                    msk_leitores_cpf.Text = "";
                    msk_leitores_rg.Text = "";
                    dt_leitores_dataCadastro.Text = "";
                    dt_leitores_dataNascimento.Text = "";
                    cb_leitores_uf.Text = "";
                }
            }
           
        }

        private void btnPesquisaLeitor_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaLeitores();
            PesquisaTodos = 0;
        }

        public void PesquisaLeitores()
        {
            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                    //MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {

                string _SQL = "";

                if (PesquisaTodos == 0)
                {
                    _SQL = "select * from tbl_cliente where cliente like '" + tb_leitores_pesquisa.Text + "%';";                   
                }
                else
                {
                    _SQL = "select * from tbl_cliente;";
                }

                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "Leitores");

                dg_leitores_pesquisa.DataSource = objdataset;
                dg_leitores_pesquisa.DataMember = "Leitores";

                dg_leitores_pesquisa.Columns[0].HeaderText = "Código do Leitor";
                dg_leitores_pesquisa.Columns[1].HeaderText = "Nome do Leitor";
                dg_leitores_pesquisa.Columns[2].HeaderText = "RG";
                dg_leitores_pesquisa.Columns[3].HeaderText = "CPF";
                dg_leitores_pesquisa.Columns[4].HeaderText = "Data de Nascimento";
                dg_leitores_pesquisa.Columns[5].HeaderText = "Data de Cadastro";
                dg_leitores_pesquisa.Columns[6].HeaderText = "Sexo";
                dg_leitores_pesquisa.Columns[7].HeaderText = "Responsavel";
                dg_leitores_pesquisa.Columns[8].HeaderText = "Observação";
                dg_leitores_pesquisa.Columns[9].HeaderText = "Matricula";
                dg_leitores_pesquisa.Columns[10].HeaderText = "Turma";
                dg_leitores_pesquisa.Columns[11].HeaderText = "Periodo";
                dg_leitores_pesquisa.Columns[12].HeaderText = "Multa";
                dg_leitores_pesquisa.Columns[13].HeaderText = "E-mail";
                dg_leitores_pesquisa.Columns[14].HeaderText = "CEP";
                dg_leitores_pesquisa.Columns[15].HeaderText = "Logradouro";
                dg_leitores_pesquisa.Columns[16].HeaderText = "Cidade";
                dg_leitores_pesquisa.Columns[17].HeaderText = "Bairro";
                dg_leitores_pesquisa.Columns[18].HeaderText = "Número";
                dg_leitores_pesquisa.Columns[19].HeaderText = "Complemento";
                dg_leitores_pesquisa.Columns[20].HeaderText = "UF";
                dg_leitores_pesquisa.Columns[21].HeaderText = "Telefone";
                dg_leitores_pesquisa.Columns[22].HeaderText = "Celular";


                _ErroPesquisa = 0;


            }
            objconexao.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            string sexo = "";
            if (rb_leitores_masculino.Checked == true)
            {
                sexo = "M";
            }
            if (rb_leitores_feminino.Checked == true)
            {
                sexo = "F";
            }
            if (tb_leitores_nomeLeitor.Text == "" || dt_leitores_dataCadastro.Text == "" || sexo == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "UPDATE tbl_cliente set cliente = '" + tb_leitores_nomeLeitor.Text + "', rg = '" + msk_leitores_rg.Text + "', cpf = '" + msk_leitores_cpf.Text + "', dt_nascimento = '" + dt_leitores_dataNascimento.Value.ToString("yyyy-MM-dd") + "', dt_cadastro = '" + dt_leitores_dataCadastro.Value.ToString("yyyy-MM-dd") + "', sexo = '" + sexo + "', responsavel = '" + tb_leitores_responsavel.Text + "', observacao = '" + tb_leitores_observacoes.Text + "', matricula = '" + tb_leitores_matricula.Text + "', turma = '" + tb_leitores_turma.Text + "', periodo = '" + tb_leitores_periodo.Text + "', multa = '" + tb_leitores_multa.Text + "', email = '" + tb_leitores_email.Text + "', cep = '" + tb_leitores_cep.Text + "', logradouro = '" + tb_leitores_logradouro.Text + "', cidade = '" + tb_leitores_cidade.Text + "', bairro = '" + tb_leitores_bairro.Text + "', numero = '" + tb_leitores_numero.Text + "', complemento = '" + tb_leitores_complemento.Text + "', uf = '" + cb_leitores_uf.Text + "', telefone = '" + tb_leitores_residencial.Text + "', celular = '" + tb_leitores_celular.Text + "' WHERE cod_leitor = '" + _codigo_leitor + "';";
                EX_CMD update = new EX_CMD();
                update.ExecutarSQL(sql, "Leitor atualizado com sucesso!");
                PesquisaTodos = 1;
                PesquisaLeitores();
                PesquisaTodos = 0;                
            }
            tb_leitores_multa.Text = "";
            tb_leitores_matricula.Text = "";
            tb_leitores_logradouro.Text = "";
            tb_leitores_email.Text = "";
            tb_leitores_complemento.Text = "";
            tb_leitores_cidade.Text = "";
            tb_leitores_cep.Text = "";
            tb_leitores_celular.Text = "";
            tb_leitores_bairro.Text = "";
            tb_leitores_nomeLeitor.Text = "";
            tb_leitores_numero.Text = "";
            tb_leitores_observacoes.Text = "";
            tb_leitores_periodo.Text = "";
            tb_leitores_residencial.Text = "";
            tb_leitores_responsavel.Text = "";
            tb_leitores_turma.Text = "";
            msk_leitores_cpf.Text = "";
            msk_leitores_rg.Text = "";
            dt_leitores_dataCadastro.Text = "";
            dt_leitores_dataNascimento.Text = "";
            cb_leitores_uf.Text = "";
            _codigo_leitor = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM tbl_cliente WHERE cod_leitor = '" + _codigo_leitor + "';";
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                EX_CMD delete = new EX_CMD();
                delete.ExecutarSQL(sql, "Leitor deletado com sucesso!");
                PesquisaTodos = 1;
                PesquisaLeitores();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        public int _codigo_leitor = 0;

        private void dg_leitores_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            tb_leitores_multa.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[12].Value.ToString();
            tb_leitores_matricula.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[9].Value.ToString();
            tb_leitores_logradouro.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[15].Value.ToString();
            tb_leitores_email.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[13].Value.ToString();
            tb_leitores_complemento.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[19].Value.ToString();
            tb_leitores_cidade.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[16].Value.ToString();
            tb_leitores_cep.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[14].Value.ToString();
            tb_leitores_celular.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[22].Value.ToString();
            tb_leitores_bairro.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[17].Value.ToString();
            tb_leitores_nomeLeitor.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
            tb_leitores_numero.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[18].Value.ToString();
            tb_leitores_observacoes.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[8].Value.ToString();
            tb_leitores_periodo.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[11].Value.ToString();
            tb_leitores_residencial.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[21].Value.ToString();
            tb_leitores_responsavel.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[7].Value.ToString();
            tb_leitores_turma.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[10].Value.ToString();
            msk_leitores_cpf.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[3].Value.ToString();
            msk_leitores_rg.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[2].Value.ToString();
            dt_leitores_dataCadastro.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[5].Value.ToString();
            dt_leitores_dataNascimento.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[4].Value.ToString();
            cb_leitores_uf.Text = dg_leitores_pesquisa.SelectedRows[0].Cells[20].Value.ToString();
            _codigo_leitor = int.Parse(dg_leitores_pesquisa.SelectedRows[0].Cells[0].Value.ToString());

            if (dg_leitores_pesquisa.SelectedRows[0].Cells[6].Value.ToString() == "M")
            {
                rb_leitores_masculino.Checked = true;
            }
            if (dg_leitores_pesquisa.SelectedRows[0].Cells[6].Value.ToString() == "F")
            {
                rb_leitores_feminino.Checked = true;
            }
            tabControlLeitores.SelectTab(tabPageManutencaoLeitores);
            

        }

        //----------------Tipo de Acervo-----------------------
        public static int _cod_tipo_acervo = 0;

       
        private void btnTipoAcervo_Click(object sender, EventArgs e)
        {
            if (tipoacervo == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_tipo_acervo);
                tabControlPrincipal.SelectTab(tab_tipo_acervo);
                PesquisaTodos = 1;
                PesquisaTipoDeAcervo();
                PesquisaTodos = 0;
                tipoacervo = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_tipo_acervo);
            }
        }

        private void btn_tipo_acervo_confirmar_Click(object sender, EventArgs e)
        {
            if (tb_nome_tipo_acervo.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "Update tbl_tipo_acervo SET tipo_acervo = '" + tb_nome_tipo_acervo.Text + "', emprestimo = " + nUD_dias_emprestimo.Value + " WHERE COD_TIPO_ACERVO = " + _cod_tipo_acervo + ";";
                EX_CMD executa_update = new EX_CMD();
                executa_update.ExecutarSQL(sql, "Tipo de acervo alterado com sucesso!");
                PesquisaTodos = 1;
                PesquisaTipoDeAcervo();
                PesquisaTodos = 0;
            }
            
        }

        private void button32_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaTipoDeAcervo();
            PesquisaTodos = 0;
        }

        private void btnPesquisaTipoAcervo_Click(object sender, EventArgs e)
        {
            PesquisaTipoDeAcervo();
        }

        private void tb_tipoDeAcervo_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaTipoDeAcervo();
        }

        public int _ErroPesquisa = 0;
        public void PesquisaTipoDeAcervo()
        {
            
            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                    //MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {
                string _SQL = "";
                /*
                 Nome do tipo do acervo
                 Dias de emprestimo
                */
                if (PesquisaTodos == 0)
                {
                    _SQL = "Select * from tbl_tipo_acervo where tipo_acervo like '" + tb_usuario_pesquisa.Text + "%';";
                }
                else
                {
                    _SQL = "Select * from tbl_tipo_acervo";
                }                
                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset,"TipoDeAcervos");

                dg_tipo_de_acervo.DataSource = objdataset;
                dg_tipo_de_acervo.DataMember = "TipoDeAcervos";

                dg_tipo_de_acervo.AutoResizeColumns();
                dg_tipo_de_acervo.Columns[0].HeaderText = "Codigo do Tipo de acervo";
                dg_tipo_de_acervo.Columns[1].HeaderText = "Tipo de acervo";
                

                _ErroPesquisa = 0;
                

            }
            objconexao.Close();
        }

        private void btnFechaTipoAcervo_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_tipo_acervo);
            tipoacervo = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        private void dg_tipo_de_acervo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tb_nome_tipo_acervo.Text = dg_tipo_de_acervo.SelectedRows[0].Cells[1].Value.ToString();
                _cod_tipo_acervo = int.Parse(dg_tipo_de_acervo.SelectedRows[0].Cells[0].Value.ToString());
                nUD_dias_emprestimo.Value = int.Parse(dg_tipo_de_acervo.SelectedRows[0].Cells[2].Value.ToString());
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string SQL = "Delete from tbl_tipo_acervo where cod_tipo_acervo = " + dg_tipo_de_acervo.SelectedRows[0].Cells[0].Value.ToString() + ";";
            EX_CMD executar_delete = new EX_CMD();
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                executar_delete.ExecutarSQL(SQL, "Tipo de acervo deletado com sucesso!");
                PesquisaTodos = 1;
                PesquisaTipoDeAcervo();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                tb_nome_tipo_acervo.Text = dg_tipo_de_acervo.SelectedRows[0].Cells[1].Value.ToString();               
                _cod_tipo_acervo = int.Parse(dg_tipo_de_acervo.SelectedRows[0].Cells[0].Value.ToString());
                nUD_dias_emprestimo.Value = int.Parse(dg_tipo_de_acervo.SelectedRows[0].Cells[2].Value.ToString());                
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            telaCadTipoAcervo tl_cad_tipo_acervo = new telaCadTipoAcervo();
            tl_cad_tipo_acervo.ShowDialog();
            PesquisaTodos = 1;
            PesquisaTipoDeAcervo();
            PesquisaTodos = 0;
        }

        //----------------Editora-----------------------

        private void btnEditora_Click(object sender, EventArgs e)
        {
            if (editor == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_editora);
                tabControlPrincipal.SelectTab(tab_editora);
                PesquisaTodos = 1;
                PesquisaEditora();
                PesquisaTodos = 0;
                editor = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_editora);
            }
        }

        public int _existeEditora = 0;
        public void VerificarEditora(string cnpj)
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
                    _existeEditora = 1;
                }
                else
                {
                    _existeEditora = 0;
                }

            }
            objconexao.Close();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            int numTryParse = 0;
            VerificarEditora(msk_editora_cnpj.Text);
            if (tb_editora_nomeFantasia.Text == "" || tb_editora_razaoSocial.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_existeEditora == 0)
                {
                    string sql = "insert into tbl_editora values('','" + tb_editora_razaoSocial.Text + "','" + tb_editora_nomeFantasia.Text + "','" + msk_editora_cnpj.Text + "','" + tb_editora_site.Text + "','" + tb_editora_contato.Text + "','" + tb_editora_email.Text + "','" + tb_editora_telefonePrincipal.Text + "','" + tb_editora_FAX.Text + "','" + tb_editora_cep.Text + "','" + tb_editora_logradouro.Text + "','" + tb_editora_cidade.Text + "','" + tb_editora_bairro.Text + "','" + int.TryParse(tb_editora_numero.Text, out numTryParse) + "','" + tb_editora_complemento.Text + "','" + cb_editora_UF.Text + "');";
                    EX_CMD inserir = new EX_CMD();
                    inserir.ExecutarSQL(sql, "Editora cadastrada com sucesso!");
                    PesquisaTodos = 1;
                    PesquisaEditora();
                    PesquisaTodos = 0;
                    tb_editora_razaoSocial.Text = "";
                    tb_editora_nomeFantasia.Text = "";
                    msk_editora_cnpj.Text = "";
                    tb_editora_site.Text = "";
                    tb_editora_contato.Text = "";
                    tb_editora_email.Text = "";
                    tb_editora_logradouro.Text = "";
                    tb_editora_numero.Text = "";
                    cb_editora_UF.Text = "";
                    tb_editora_cep.Text = "";
                    tb_editora_bairro.Text = "";
                    tb_editora_cidade.Text = "";
                    tb_editora_complemento.Text = "";
                    tb_editora_telefonePrincipal.Text = "";
                    tb_editora_FAX.Text = "";
                }
                else
                {

                }
                
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (tb_editora_nomeFantasia.Text == "" || tb_editora_razaoSocial.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "UPDATE tbl_editora set razao_social = '" + tb_editora_razaoSocial.Text + "', nome_fantasia = '" + tb_editora_nomeFantasia.Text + "', cnpj = '" + msk_editora_cnpj.Text + "', site = '" + tb_editora_site.Text + "', contato = '" + tb_editora_contato.Text + "', email = '" + tb_editora_email.Text + "', telefone = '" + tb_editora_telefonePrincipal.Text + "', fax = '" + tb_editora_FAX.Text + "', cep = '" + tb_editora_cep.Text + "', logradouro = '" + tb_editora_logradouro.Text + "', cidade = '" + tb_editora_cidade.Text + "', bairro = '" + tb_editora_bairro.Text + "', numero = '" + tb_editora_numero.Text + "', complemento = '" + tb_editora_complemento.Text + "', uf = '" + cb_editora_UF.Text + "' WHERE cod_editora = '" + dg_editora_pesquisa.SelectedRows[0].Cells[0].Value.ToString() + "';";
                EX_CMD update = new EX_CMD();
                update.ExecutarSQL(sql, "Editora alterada com sucesso!");
                PesquisaTodos = 1;
                PesquisaEditora();
                PesquisaTodos = 0;
                tb_editora_razaoSocial.Text = "";
                tb_editora_nomeFantasia.Text = "";
                msk_editora_cnpj.Text = "";
                tb_editora_site.Text = "";
                tb_editora_contato.Text = "";
                tb_editora_email.Text = "";
                tb_editora_logradouro.Text = "";
                tb_editora_numero.Text = "";
                cb_editora_UF.Text = "";
                tb_editora_cep.Text = "";
                tb_editora_bairro.Text = "";
                tb_editora_cidade.Text = "";
                tb_editora_complemento.Text = "";
                tb_editora_telefonePrincipal.Text = "";
                tb_editora_FAX.Text = "";            
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM tbl_editora WHERE cod_editora = '" + dg_editora_pesquisa.SelectedRows[0].Cells[0].Value.ToString() + "';";
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                EX_CMD delete = new EX_CMD();
                delete.ExecutarSQL(sql, "Editora deletada com sucesso!");
                PesquisaTodos = 1;
                PesquisaEditora();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaEditora();
            PesquisaTodos = 0;
        }

        public void PesquisaEditora()
        {

            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                   // MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {
                string _SQL = "";
                if (PesquisaTodos == 0)
                {
                    _SQL = "select * from tbl_editora where nome_fantasia like '" + tb_editora_pesquisa.Text + "%';";
                }
                else
                {
                    _SQL = "Select * from tbl_editora;";
                }


                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "Editora");

                dg_editora_pesquisa.DataSource = objdataset;
                dg_editora_pesquisa.DataMember = "Editora";
                _ErroPesquisa = 0;
            }
            objconexao.Close();
        }

        private void tb_editora_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaTodos = 0;
            PesquisaEditora();            
        }


        private void dg_editora_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            tb_editora_razaoSocial.Text = dg_editora_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
            tb_editora_nomeFantasia.Text = dg_editora_pesquisa.SelectedRows[0].Cells[2].Value.ToString();
            msk_editora_cnpj.Text = dg_editora_pesquisa.SelectedRows[0].Cells[3].Value.ToString();
            tb_editora_site.Text = dg_editora_pesquisa.SelectedRows[0].Cells[4].Value.ToString();
            tb_editora_contato.Text = dg_editora_pesquisa.SelectedRows[0].Cells[5].Value.ToString();
            tb_editora_email.Text = dg_editora_pesquisa.SelectedRows[0].Cells[6].Value.ToString();
            tb_editora_logradouro.Text = dg_editora_pesquisa.SelectedRows[0].Cells[10].Value.ToString();
            tb_editora_numero.Text = dg_editora_pesquisa.SelectedRows[0].Cells[13].Value.ToString();
            cb_editora_UF.Text = dg_editora_pesquisa.SelectedRows[0].Cells[15].Value.ToString();
            tb_editora_cep.Text = dg_editora_pesquisa.SelectedRows[0].Cells[9].Value.ToString();
            tb_editora_bairro.Text = dg_editora_pesquisa.SelectedRows[0].Cells[12].Value.ToString();
            tb_editora_cidade.Text = dg_editora_pesquisa.SelectedRows[0].Cells[11].Value.ToString();
            tb_editora_complemento.Text = dg_editora_pesquisa.SelectedRows[0].Cells[14].Value.ToString();
            tb_editora_telefonePrincipal.Text = dg_editora_pesquisa.SelectedRows[0].Cells[7].Value.ToString();
            tb_editora_FAX.Text = dg_editora_pesquisa.SelectedRows[0].Cells[8].Value.ToString();
            tabControl1.SelectTab(tab_editora_manutencao);
        }

        private void btnFechaEditora_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_editora);
            editor = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        //----------------Idioma-----------------------
        public int _codigo_idioma = 0;
        private void btnIdioma_Click(object sender, EventArgs e)
        {
            if (idioma == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_idioma);
                tabControlPrincipal.SelectTab(tab_idioma);
                PesquisaTodos = 1;
                PesquisaIdioma();
                PesquisaTodos = 0;
                idioma = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_idioma);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (tb_idioma.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "Update tbl_idioma SET IDIOMA = '" + tb_idioma.Text + "' WHERE COD_IDIOMA = " + _codigo_idioma + ";";
                EX_CMD executa_update = new EX_CMD();
                executa_update.ExecutarSQL(sql, "Idioma alterado com sucesso!");
                PesquisaTodos = 1;
                PesquisaIdioma();
                PesquisaTodos = 0;
            }
            
        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaIdioma();
            PesquisaTodos = 0;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                tb_idioma.Text = dg_idioma_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
                _codigo_idioma = int.Parse(dg_idioma_pesquisa.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch
            {

            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string SQL = "Delete from tbl_idioma where cod_idioma = " + dg_idioma_pesquisa.SelectedRows[0].Cells[0].Value.ToString() + ";";
                EX_CMD executar_delete = new EX_CMD();
                executar_delete.ExecutarSQL(SQL, "Idioma deletado com sucesso!");
                PesquisaTodos = 1;
                PesquisaIdioma();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        private void dg_idioma_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tb_idioma.Text = dg_idioma_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
                _codigo_idioma = int.Parse(dg_idioma_pesquisa.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch
            {

            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            telaCadIdioma tl_cad_idioma = new telaCadIdioma();
            tl_cad_idioma.ShowDialog();
            PesquisaTodos = 1;
            PesquisaIdioma();
            PesquisaTodos = 0;
        }

        private void tb_idioma_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaIdioma();
        }
        
        public void PesquisaIdioma()
        {

            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                    MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {
                /*
                 Codigo do idioma
                 Idioma
                 */
                string  _SQL = "";
                if (PesquisaTodos == 0)
                {
                    _SQL = "Select * from tbl_idioma where idioma like '" + tb_idioma_pesquisa.Text + "%';";
                }
                else
                {
                    _SQL = "Select * from tbl_idioma;";
                }
                                            


                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "Idioma");

                dg_idioma_pesquisa.DataSource = objdataset;
                dg_idioma_pesquisa.DataMember = "Idioma";

                dg_idioma_pesquisa.AutoResizeColumns();

                dg_idioma_pesquisa.Columns[0].HeaderText = "Codigo do idioma";
                dg_idioma_pesquisa.Columns[1].HeaderText = "Idioma";

                _ErroPesquisa = 0;


            }
            objconexao.Close();
        }

        private void button31_Click(object sender, EventArgs e)
        {

        }
       
        private void btnFechaIdioma_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_idioma);
            idioma = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        //----------------Autor-----------------------

        private void btnAutor_Click(object sender, EventArgs e)
        {
            if (autor == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_autores);
                tabControlPrincipal.SelectTab(tab_autores);
                PesquisaTodos = 1;
                PesquisaAutor();
                PesquisaTodos = 0;
                autor = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_autores);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            telaCadAutor tl_cad_autor = new telaCadAutor();
            tl_cad_autor.ShowDialog();
            PesquisaTodos = 1;
            PesquisaAutor();
            PesquisaTodos = 0;
        }

        public int _cod_autor = 0;
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                tb_autor.Text = dg_autor_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
                _cod_autor = int.Parse(dg_autor_pesquisa.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch
            {

            }
        }

        private void dg_autor_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tb_autor.Text = dg_autor_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
                _cod_autor = int.Parse(dg_autor_pesquisa.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch
            {

            }
        }
        
        public void PesquisaAutor()
        {

            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                    MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {
                /*
                 Codigo do autor
                 Autor
                 */
                string _SQL = "";
                if (PesquisaTodos == 0)
                {
                    _SQL = "Select * from tbl_autor where autor like '" + tb_autores_pesquisa.Text + "%';";
                }
                else
                {
                    _SQL = "Select * from tbl_autor;";
                }
                
                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "Autor");

                dg_autor_pesquisa.DataSource = objdataset;
                dg_autor_pesquisa.DataMember = "Autor";

                dg_autor_pesquisa.AutoResizeColumns();

                dg_autor_pesquisa.Columns[0].HeaderText = "Codigo do autor";
                dg_autor_pesquisa.Columns[1].HeaderText = "Autor";

                _ErroPesquisa = 0;


            }
            objconexao.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = "Delete from tbl_autor where cod_autor = '" + dg_autor_pesquisa.SelectedRows[0].Cells[0].Value.ToString() + "';";
                EX_CMD deletar = new EX_CMD();
                deletar.ExecutarSQL(sql, "Autor deletado com sucesso!");
                PesquisaTodos = 1;
                PesquisaAutor();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (tb_autor.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "Update tbl_autor set autor = '" + tb_autor.Text + "' where cod_autor = '" + _cod_autor + "';";
                EX_CMD update = new EX_CMD();
                update.ExecutarSQL(sql, "Autor atualizado com sucesso!");
                PesquisaTodos = 1;
                PesquisaAutor();
                PesquisaTodos = 0;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaAutor();
            PesquisaTodos = 0;
        }

        private void tb_autores_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaAutor();
        }

        private void btnFechaAutor_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_autores);
            autor = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        //----------------Genero-----------------------

        private void btnGenero_Click(object sender, EventArgs e)
        {
            if (genero == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_generos);
                tabControlPrincipal.SelectTab(tab_generos);
                PesquisaTodos = 1;
                PesquisaGenero();
                PesquisaTodos = 0;
                genero = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_generos);
            }
        }

        private void textBox22_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaTodos = 0;
            PesquisaGenero();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            telaCadGenero tl_cad_genero = new telaCadGenero();
            tl_cad_genero.ShowDialog();
            PesquisaTodos = 1;
            PesquisaGenero();
            PesquisaTodos = 0;
        }

        public int _codigo_genero = 0;
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                tb_genero.Text = dg_genero_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
                _codigo_genero = int.Parse(dg_genero_pesquisa.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch
            {

            }
        }

        private void dg_genero_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tb_genero.Text = dg_genero_pesquisa.SelectedRows[0].Cells[1].Value.ToString();
                _codigo_genero = int.Parse(dg_genero_pesquisa.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch
            {

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = "Delete from tbl_genero where cod_genero = '" + int.Parse(dg_genero_pesquisa.SelectedRows[0].Cells[0].Value.ToString()) + "';";
                EX_CMD deletar = new EX_CMD();
                deletar.ExecutarSQL(sql, "Genero deletado com sucesso!");
                PesquisaTodos = 1;
                PesquisaGenero();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (tb_genero.Text == "")
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "Update tbl_genero set genero ='" + tb_genero.Text + "' where cod_genero = '" + _codigo_genero + "';";
                EX_CMD update = new EX_CMD();
                update.ExecutarSQL(sql, "Genero atualizado com sucesso!");
                PesquisaTodos = 1;
                PesquisaGenero();
                PesquisaTodos = 0;
            }
        }

        public void PesquisaGenero()
        {

            try
            {
                objconexao.Open();
            }
            catch
            {
                if (_ErroPesquisa == 0)
                {
                    MessageBox.Show("Não foi possivel abrir uma conexão com o banco de dados.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                }
            }
            if (objconexao.State == ConnectionState.Open)
            {
                /*
                 Codigo do genero
                 Genero
                 */
                string _SQL = "";

                if (PesquisaTodos == 0)
                {
                    _SQL = "Select * from tbl_genero where genero like '" + tb_genero_pesquisa.Text + "%';";
                }
                else
                {
                    _SQL = "Select * from tbl_genero;";
                }


                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdataset = new DataSet();
                objadapter.Fill(objdataset, "Genero");

                dg_genero_pesquisa.DataSource = objdataset;
                dg_genero_pesquisa.DataMember = "Genero";

                dg_genero_pesquisa.AutoResizeColumns();

                dg_genero_pesquisa.Columns[0].HeaderText = "Codigo do genero";
                dg_genero_pesquisa.Columns[1].HeaderText = "Genero";               

                _ErroPesquisa = 0;


            }
            objconexao.Close();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaGenero();
            PesquisaTodos = 0;
        }

        private void btnFechaGenero_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_generos);
            genero = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        //----------------Usuario-----------------------

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            if (usuario == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_usuarios);
                tabControlPrincipal.SelectTab(tab_usuarios);
                PesquisaUsuario();
                usuario = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_usuarios);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaUsuario();
            PesquisaTodos = 0;
        }

        private void dg_usuarios_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tb_nome.Text = dg_usuarios.SelectedRows[0].Cells[1].Value.ToString();
                tb_login.Text = dg_usuarios.SelectedRows[0].Cells[2].Value.ToString();
                tb_cod_seguranca.Text = dg_usuarios.SelectedRows[0].Cells[5].Value.ToString();
                _codigo_usuario = int.Parse(dg_usuarios.SelectedRows[0].Cells[0].Value.ToString());

                if (int.Parse(dg_usuarios.SelectedRows[0].Cells[4].Value.ToString()) == 1)
                {
                    rtb_admin.Checked = true;
                }
                else
                {
                    rtb_comum.Checked = true;
                }
            }
            catch
            {

            }
        }

        private void btn_alterar_usu_Click(object sender, EventArgs e)
        {
            int tipo = 0;
            if (rtb_admin.Checked == true)
            {
                tipo = 1;
            }
            if (tb_nome.Text == "" || tb_login.Text == "" || tb_cod_seguranca.Text == "" || rtb_admin.Checked == false && rtb_comum.Checked == false)
            {
                MessageBox.Show("Preencha os campos obrigatórios", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "Update tbl_usuario SET NOME = '" + tb_nome.Text + "', LOGIN ='" + tb_login.Text + "',TIPO = '" + tipo + "',COD_SEGURANCA = '" + tb_cod_seguranca.Text + "' WHERE COD_USUARIO = " + _codigo_usuario + ";";
                EX_CMD executa_update = new EX_CMD();
                executa_update.ExecutarSQL(sql, "Usuário alterado com sucesso!");
                PesquisaTodos = 1;
                PesquisaUsuario();
                PesquisaTodos = 0;
            }

        }

        private void btn_alterar_usuario_Click(object sender, EventArgs e)
        {
            try
            {
                tb_nome.Text = dg_usuarios.SelectedRows[0].Cells[1].Value.ToString();
                tb_login.Text = dg_usuarios.SelectedRows[0].Cells[2].Value.ToString();
                tb_cod_seguranca.Text = dg_usuarios.SelectedRows[0].Cells[5].Value.ToString();
                _codigo_usuario = int.Parse(dg_usuarios.SelectedRows[0].Cells[0].Value.ToString());

                if (int.Parse(dg_usuarios.SelectedRows[0].Cells[4].Value.ToString()) == 1)
                {
                    rtb_admin.Checked = true;
                    rtb_comum.Checked = false;
                }
                else
                {
                    rtb_comum.Checked = true;
                    rtb_admin.Checked = false;
                }
            }
            catch
            {

            }
        }

        private void btn_novo_usuario_Click(object sender, EventArgs e)
        {
            novoUsuario nv_usu = new novoUsuario();
            nv_usu.ShowDialog();
            PesquisaTodos = 1;
            PesquisaUsuario();
            PesquisaTodos = 0;
        }

        private void btn_deletar_usuario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string SQL = "Delete from tbl_usuario where cod_usuario = " + dg_usuarios.SelectedRows[0].Cells[0].Value.ToString() + ";";
                EX_CMD executar_delete = new EX_CMD();
                executar_delete.ExecutarSQL(SQL, "Usuário deletado com sucesso!");
                PesquisaTodos = 1;
                PesquisaUsuario();
                PesquisaTodos = 0;
            }
            else
            {

            }
        }

        private void btnFechaUsuario_Click(object sender, EventArgs e)
        {
            tabControlPrincipal.TabPages.Remove(this.tab_usuarios);
            usuario = 0;
            if (tabControlPrincipal.TabPages.Count == 0)
            {
                tabControlPrincipal.Visible = false;
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaUsuario();            
        }

        public void PesquisaUsuario()
        {
            try
            {
                objconexao.Open();
            }
            catch
            {
                MessageBox.Show("Não foi possivel abrir uma conxao com o banco de dados.","Erro!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (objconexao.State == ConnectionState.Open)
            {
                string _SQL = "";
                if (PesquisaTodos == 0)
                {
                     _SQL = "select * from tbl_usuario where login like '" + tb_usuario_pesquisa.Text + "%'" + ";";
                }
                else
                {
                    _SQL = "Select * from tbl_usuario;";
                }
                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL,objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Usuarios");
                dg_usuarios.DataSource = objdatasert_usu;
                dg_usuarios.DataMember = "Usuarios";

                dg_usuarios.AutoResizeColumns();

                dg_usuarios.Columns[0].HeaderText = "Codigo do usuario";
                dg_usuarios.Columns[1].HeaderText = "Nome";
                dg_usuarios.Columns[2].HeaderText = "Login";
                dg_usuarios.Columns[3].Visible = false;
                dg_usuarios.Columns[4].HeaderText = "Tipo";
                dg_usuarios.Columns[5].HeaderText = "Codigo de segurança";
            }
            objconexao.Close();
        }
        

        //----------------Telas de Cadastros-----------------------

        private void btnAddAutor_Click(object sender, EventArgs e)
        {
            telaCadAutor Autor = new telaCadAutor();
            Autor.ShowDialog();
            CarregaCBautores();
        }

        private void btnAddEditora_Click(object sender, EventArgs e)
        {
            telaCadEditora Editora = new telaCadEditora();
            Editora.ShowDialog();
            CarregaCBeditora();
        }

        private void btnAddTipoAcervo_Click(object sender, EventArgs e)
        {
            telaCadTipoAcervo TipoAcervo = new telaCadTipoAcervo();
            TipoAcervo.ShowDialog();
            CarregaCBtipoDeAcervos();
        }

        private void btnAddIdioma_Click(object sender, EventArgs e)
        {
            telaCadIdioma Idioma = new telaCadIdioma();
            Idioma.ShowDialog();
            CarregaCBidioma();
        }

        private void btnAddGenero_Click(object sender, EventArgs e)
        {
            telaCadGenero Genero = new telaCadGenero();
            Genero.ShowDialog();
            CarregaCBgenero();
        }
        public int CPS_ONoff = 0;

        //----------------Atalhos-----------------------

        private void telaPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnEmprestimo_Click(sender, e);
                    break;
                case Keys.F2:
                    btnAcervo_Click(sender, e);
                    break;
                case Keys.F3:
                    btnLeitores_Click(sender, e);
                    break;
                case Keys.F4:
                    btnTipoAcervo_Click(sender, e);
                    break;
                case Keys.F5:
                    btnEditora_Click(sender, e);
                    break;
                case Keys.F6:
                    btnIdioma_Click(sender, e);
                    break;
                case Keys.F7:
                    btnAutor_Click(sender, e);
                    break;
                case Keys.F8:
                    btnGenero_Click(sender, e);
                    break;
                case Keys.F9:
                    if (AutenticarLogin.tipo == 1)
                    {
                        btnUsuario_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Você não tem acesso a este recursso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case Keys.F11:
                    Process.Start("calc.exe");
                    break;
                case Keys.F12:
                    Sobre.BACKUP opn_backup = new Sobre.BACKUP();
                    opn_backup.ShowDialog();
                    break;
                case Keys.CapsLock:
                    if (Control.IsKeyLocked(Keys.CapsLock))
                    {
                        TSL_CPSLOCK.Text = "LIGADO";
                        TSL_CPSLOCK.BackColor = Color.Green;
                    }
                    else
                    {
                        TSL_CPSLOCK.Text = "DESLIGADO";
                        TSL_CPSLOCK.BackColor = Color.Red;
                    }
                    break;
            
            }                            
                        
            /*
             * Ainda não achamos uma solução adequada para o que precisamos
            if (tab_emprestimos.Visible==true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaEmprestimo_Click(sender, e);
                        break;
                }

            }
            if (tab_acervo.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaAcervo_Click(sender, e);
                        break;
                }

            }
            if (tab_leitores.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaLeitor_Click(sender, e);
                        break;
                }

            }
            if (tab_tipo_acervo.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaTipoAcervo_Click(sender, e);
                        break;
                }

            }
            if (tab_editora.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaEditora_Click(sender, e);
                        break;
                }

            }
            if (tab_idioma.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaIdioma_Click(sender, e);
                        break;
                }

            }
            if (tab_autores.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaAutor_Click(sender, e);
                        break;
                }

            }
            if (tab_generos.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaGenero_Click(sender, e);
                        break;
                }

            }
            if (tab_usuarios.Visible == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnFechaUsuario_Click(sender, e);
                        break;
                }

             
            }
              */

        }
            
        //----------------Menu Strip-----------------------

        private void emprestimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (emprestimo == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_emprestimos);
                tabControlPrincipal.SelectTab(tab_emprestimos);
                CarregaCboxAcervos();
                emprestimo = 1;
                PesquisaTodos = 1;
                PesquisaEmprestimo();
                PesquisaTodos = 0;
                PesquisaHistorico();
                CarregaCboxAcervos();
                CarregaCboxLeitores();
                PreencherTreeView();
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_emprestimos);
            }
        }

        private void acervoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (acervo == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_acervo);
                tabControlPrincipal.SelectTab(tab_acervo);
                PesquisaTodos = 1;
                PesquisaAcervo();
                PesquisaTodos = 0;
                CarregaCBautores();
                CarregaCBeditora();
                CarregaCBgenero();
                CarregaCBidioma();
                CarregaCBtipoDeAcervos();
                tb_acervo_exemplar.Enabled = false;
                acervo = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_acervo);
            }
        }

        private void leitoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (leitores == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_leitores);
                tabControlPrincipal.SelectTab(tab_leitores);
                PesquisaTodos = 1;
                PesquisaLeitores();
                PesquisaTodos = 0;
                leitores = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_leitores);
            }
        }

        private void tipoDeAcervoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tipoacervo == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_tipo_acervo);
                tabControlPrincipal.SelectTab(tab_tipo_acervo);
                PesquisaTodos = 1;
                PesquisaTipoDeAcervo();
                PesquisaTodos = 0;
                tipoacervo = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_tipo_acervo);
            }
        }

        private void editoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editor == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_editora);
                tabControlPrincipal.SelectTab(tab_editora);
                PesquisaTodos = 1;
                PesquisaEditora();
                PesquisaTodos = 0;
                editor = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_editora);
            }
        }

        private void idiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idioma == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_idioma);
                tabControlPrincipal.SelectTab(tab_idioma);
                PesquisaTodos = 1;
                PesquisaIdioma();
                PesquisaTodos = 0;
                idioma = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_idioma);
            }
        }

        private void autorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (autor == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_autores);
                tabControlPrincipal.SelectTab(tab_autores);
                PesquisaTodos = 1;
                PesquisaAutor();
                PesquisaTodos = 0;
                autor = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_autores);
            }
        }

        private void generoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (genero == 0)
            {
                tabControlPrincipal.Visible = true;
                tabControlPrincipal.TabPages.Add(this.tab_generos);
                tabControlPrincipal.SelectTab(tab_generos);
                PesquisaTodos = 1;
                PesquisaGenero();
                PesquisaTodos = 0;
                genero = 1;
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_generos);
            }
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usuario == 0)
            {
                if (AutenticarLogin.tipo == 1)
                {
                    tabControlPrincipal.Visible = true;
                    tabControlPrincipal.TabPages.Add(this.tab_usuarios);
                    tabControlPrincipal.SelectTab(tab_usuarios);
                    PesquisaTodos = 1;
                    PesquisaUsuario();
                    PesquisaTodos = 0;
                    usuario = 1;
                }
                else
                {
                    MessageBox.Show("Você não tem acesso a este recursso.","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                tabControlPrincipal.SelectTab(tab_usuarios);
            }
        }

        private void tabPagePesquisaAcervo_Click(object sender, EventArgs e)
        {

        }

        private void tabPagePesquisaUsuarios_Click(object sender, EventArgs e)
        {

        }
        public int logoff = 0;
        private void telaPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logoff == 0)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void cb_autor_campos_pesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Livrotecnotify_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Livrotecnotify.BalloonTipTitle = "Livro Tec";
                Livrotecnotify.BalloonTipText = "Livro Tec";
                Livrotecnotify.Text = "Livro Tec";
                this.Show();
                this.WindowState = FormWindowState.Normal;                
            }
        }

        private void telaPrincipal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Livrotecnotify.BalloonTipTitle = "Livro Tec | Clique 2x para abrir";
                Livrotecnotify.BalloonTipText = "Livro Tec | Clique 2x para abrir";
                Livrotecnotify.Text = "Livro Tec | Clique 2x para abrir";
                Hide();
            }
        }

       

        private void sairLogOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logoff = 1;
            this.Close();
            telaLogin tl_login = new telaLogin();
            tl_login.Show();
            logoff = 0;
        }

        private void reverCodigoDeSegurancaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REVER_COD_SEGURANCA rever_cod = new REVER_COD_SEGURANCA();
            rever_cod.ShowDialog();
        }

        private void cb_leitores_uf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPesquisaEmprestimo_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaEmprestimo();
            PesquisaTodos = 0;
        }

        private void dg_emprestimos_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            string SQL = "Delete from tbl_emprestimo where cod_emprestimo = " + dg_emprestimos_pendente.SelectedRows[0].Cells[0].Value.ToString() + ";";
            EX_CMD executar_delete = new EX_CMD();
            executar_delete.ExecutarSQL(SQL, "Emprestimo deletado com sucesso!");
        }

        public void PesquisaEmprestimo()
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
                if (PesquisaTodos == 0)
                {
                    string sql = "SELECT * FROM tbl_emprestimo WHERE cod_emprestimo LIKE '%" + tb_emprestimos_pendente.Text + "%' AND estado = 'Pendente';";
                }
                else
                {
                    _SQL = "Select * from tbl_emprestimo where estado = 'Pendente';";
                }
                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Emprestimo");
                dg_emprestimos_pendente.DataSource = objdatasert_usu;
                dg_emprestimos_pendente.DataMember = "Emprestimo";

                dg_emprestimos_pendente.AutoResizeColumns();

                dg_emprestimos_pendente.Columns[0].HeaderText = "Código do Emprestimo";
                dg_emprestimos_pendente.Columns[1].HeaderText = "Código do Acervo";
                dg_emprestimos_pendente.Columns[2].HeaderText = "Código do Leitor";
                dg_emprestimos_pendente.Columns[3].HeaderText = "Data do Aluguel";
                dg_emprestimos_pendente.Columns[4].HeaderText = "Data da Devolução";
                dg_emprestimos_pendente.Columns[5].HeaderText = "Estado";

                
            }
            objconexao.Close();
        }

        public class Acervos
        {
            public int Codigo_acervo { get; set; }
            public string Titulo { get; set; }
        }

        public void CarregaCboxAcervos()
        {
            ListaAcervo();
            cb_emprestimos_nomeLivro.DisplayMember = "Titulo";
            cb_emprestimos_nomeLivro.ValueMember = "Codigo_acervo";
            cb_emprestimos_nomeLivro.DataSource = ListaAcervo();
        }

        private List<Acervos> ListaAcervo()
        {
            List<Acervos> lista = new List<Acervos>();

            string query = "SELECT cod_acervo,titulo,exemplar FROM tbl_acervo where status_acervo = 'Disponivel';";

            try
            {
                objconexao.Open();
            }
            catch
            {
                
            }
            if (objconexao.State == ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand(query, objconexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Acervos c = new Acervos();
                        c.Codigo_acervo = Convert.ToInt32(leitor["cod_acervo"]);
                        c.Titulo = leitor["titulo"].ToString() + " - " + leitor["exemplar"];
                        lista.Add(c);
                    }
                }

            }
            objconexao.Close();
            return lista;
        }

        private void dg_emprestimos_pesquisa_DoubleClick_1(object sender, EventArgs e)
        {
            if (dg_emprestimos_pendente.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Você realmente deseja deletar o item selecionado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string sql = "UPDATE tbl_emprestimo SET estado = 'Devolvido' WHERE cod_emprestimo = '" + dg_emprestimos_pendente.SelectedRows[0].Cells[0].Value.ToString() + "' ;";
                    string sql2 = "UPDATE tbl_acervo SET status_acervo = 'Disponivel' WHERE cod_acervo = '" + dg_emprestimos_pendente.SelectedRows[0].Cells[1].Value.ToString() + "' ; ";
                    EX_CMD deletar = new EX_CMD();
                    EX_CMD.ExibirMenssagem = 1;
                    deletar.ExecutarSQL(sql2, "");
                    EX_CMD.ExibirMenssagem = 0;

                    deletar.ExecutarSQL(sql, "Emprestimo devolvido com sucesso!");
                    PesquisaEmprestimo();
                }
                else
                {

                }
            }
            else
            {

            }
        }

        private void dg_acervo_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            string autor = "SELECT * FROM tbl_autor WHERE cod_autor = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[1].Value.ToString() + "' limit 1;";
            MySqlDataAdapter Autoradapter = new MySqlDataAdapter(autor, objconexao);
            DataSet Autordataset = new DataSet();
            Autoradapter.Fill(Autordataset);
            string nomeAutor = Autordataset.Tables[0].Rows[0]["autor"].ToString();


            string editora = "SELECT nome_fantasia FROM tbl_editora  WHERE cod_editora = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[5].Value.ToString() + "' ;";
            MySqlDataAdapter Editoraadapter = new MySqlDataAdapter(editora, objconexao);
            DataSet Editoradataset = new DataSet();
            Editoraadapter.Fill(Editoradataset);
            string nomeEditora = Editoradataset.Tables[0].Rows[0]["nome_fantasia"].ToString();


            string genero = "SELECT genero FROM tbl_genero WHERE cod_genero = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[2].Value.ToString() + "' ;";
            MySqlDataAdapter Generoadapter = new MySqlDataAdapter(genero, objconexao);
            DataSet Generodataset = new DataSet();
            Generoadapter.Fill(Generodataset);
            string nomeGenero = Generodataset.Tables[0].Rows[0]["genero"].ToString();


            string tipoAcervo = "SELECT tipo_acervo FROM tbl_tipo_acervo WHERE cod_tipo_acervo = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[3].Value.ToString() + "' ;";
            MySqlDataAdapter Acervoadapter = new MySqlDataAdapter(tipoAcervo, objconexao);
            DataSet Acervodataset = new DataSet();
            Acervoadapter.Fill(Acervodataset);
            string nomeAcervo = Acervodataset.Tables[0].Rows[0]["tipo_acervo"].ToString();


            string idioma = "SELECT idioma FROM tbl_idioma WHERE cod_idioma = '" + dg_acervo_pesquisa.SelectedRows[0].Cells[4].Value.ToString() + "' ;";
            MySqlDataAdapter Idiomaadapter = new MySqlDataAdapter(idioma, objconexao);
            DataSet Idiomadataset = new DataSet();
            Idiomaadapter.Fill(Idiomadataset);
            string nomeIdioma = Idiomadataset.Tables[0].Rows[0]["idioma"].ToString();

            dg_acervo_pesquisa.AutoResizeColumns();
            tb_acervo_cdd.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[16].Value.ToString();
            tb_acervo_cdu.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[17].Value.ToString();
            tb_acervo_codBarra.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[24].Value.ToString();
            tb_acervo_cutter.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[18].Value.ToString();
            tb_acervo_edicao.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[11].Value.ToString();
            tb_acervo_exemplar.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[9].Value.ToString();
            tb_acervo_isbn.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[19].Value.ToString();
            tb_acervo_numPaginas.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[14].Value.ToString();
            tb_acervo_observacoes.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[13].Value.ToString();
            tb_acervo_preco.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[15].Value.ToString();
            tb_acervo_refBibliografica.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[21].Value.ToString();
            tb_acervo_resenha.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[22].Value.ToString();
            tb_acervo_subTitulo.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[7].Value.ToString();
            tb_acervo_titulo.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[6].Value.ToString();
            tb_acervo_volume.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[10].Value.ToString();
            cb_acervo_autor.Text = nomeAutor;
            cb_acervo_editora.Text = nomeEditora;
            cb_acervo_genero.Text = nomeGenero;
            cb_acervo_idioma.Text = nomeIdioma;
            cb_acervo_periodicidade.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[23].Value.ToString();
            cb_acervo_tipoDeAcervo.Text = nomeAcervo;
            dtp_acervo_dataAquisicao.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[8].Value.ToString();
            dtp_acervo_dataEdicao.Text = dg_acervo_pesquisa.SelectedRows[0].Cells[12].Value.ToString();
            btn_acervo_cadastrar.Enabled = false;

            
            if (dg_acervo_pesquisa.SelectedRows[0].Cells[20].Value.ToString() == "Locado")
            {
                rb_acervo_locado.Checked = true;
            }
            if (dg_acervo_pesquisa.SelectedRows[0].Cells[20].Value.ToString() == "Disponivel")
            {
                rb_acervo_disponivel.Checked = true;
            }
            tb_acervo_exemplar.Enabled = true;
            tabControlAcervo.SelectTab(tabPageManutencaoAcervo);
        }

        private void groupBox18_Enter(object sender, EventArgs e)
        {
            
        }

        private void tb_emprestimos_pesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaEmprestimo();
        }

        private void cb_emprestimo_leitor_SelectedValueChanged(object sender, EventArgs e)
        {
            /* try
            {
                objconexao.Open();
            }
            catch
            {
               
            }
             if (objconexao.State == ConnectionState.Open)
             {
                
                 string _SQL = "Select * from tbl_emprestimo where tbl_cliente_cod_leitor = '"+cb_emprestimo_leitor.SelectedValue+"';";
                 
                 MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                 DataSet objdataset = new DataSet();
                 objadapter.Fill(objdataset, "Pendencias");
                 dg_emprestimos_pesquisa.DataSource = objdataset;
                 dg_emprestimos_pesquisa.DataMember = "Pendencias";

                 dg_emprestimos_pesquisa.AutoResizeColumns();

                 if (objdataset.Tables[0].Rows.Count > 0)
                 {
                     lb_emprestimo_pendencias.Text = objdataset.Tables[0].Rows.Count.ToString();
                 }
             }
             */ 
        }

        private void button39_Click(object sender, EventArgs e)
        {
            tb_editora_razaoSocial.Text = "";
            tb_editora_nomeFantasia.Text = "";
            msk_editora_cnpj.Text = "";
            tb_editora_site.Text = "";
            tb_editora_contato.Text = "";
            tb_editora_email.Text = "";
            tb_editora_logradouro.Text = "";
            tb_editora_numero.Text = "";
            cb_editora_UF.Text = "";
            tb_editora_cep.Text = "";
            tb_editora_bairro.Text = "";
            tb_editora_cidade.Text = "";
            tb_editora_complemento.Text = "";
            tb_editora_telefonePrincipal.Text = "";
            tb_editora_FAX.Text = "";
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            tb_leitores_multa.Text = "";
            tb_leitores_matricula.Text = "";
            tb_leitores_logradouro.Text = "";
            tb_leitores_email.Text = "";
            tb_leitores_complemento.Text = "";
            tb_leitores_cidade.Text = "";
            tb_leitores_cep.Text = "";
            tb_leitores_celular.Text = "";
            tb_leitores_bairro.Text = "";
            tb_leitores_nomeLeitor.Text = "";
            tb_leitores_numero.Text = "";
            tb_leitores_observacoes.Text = "";
            tb_leitores_periodo.Text = "";
            tb_leitores_residencial.Text = "";
            tb_leitores_responsavel.Text = "";
            tb_leitores_turma.Text = "";
            msk_leitores_cpf.Text = "";
            msk_leitores_rg.Text = "";
            dt_leitores_dataCadastro.Text = "";
            dt_leitores_dataNascimento.Text = "";
            cb_leitores_uf.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            tb_acervo_cdd.Text = "";
            tb_acervo_cdu.Text = "";
            tb_acervo_codBarra.Text = ""; 
            tb_acervo_cutter.Text = "";
            tb_acervo_edicao.Text = "";
            tb_acervo_exemplar.Text = "";
            tb_acervo_isbn.Text = "";
            tb_acervo_numPaginas.Text = "";
            tb_acervo_observacoes.Text = "";
            tb_acervo_preco.Text = "";
            tb_acervo_refBibliografica.Text = "";
            tb_acervo_resenha.Text = "";
            tb_acervo_subTitulo.Text = "";
            tb_acervo_titulo.Text = "";
            tb_acervo_volume.Text = "";
            cb_acervo_autor.Text = "";
            cb_acervo_editora.Text = "";
            cb_acervo_genero.Text = "";
            cb_acervo_idioma.Text = "";
            cb_acervo_periodicidade.Text = "";
            cb_acervo_tipoDeAcervo.Text = "";
            dtp_acervo_dataAquisicao.Text = "";
            dtp_acervo_dataEdicao.Text = "";
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sobre.Sobre sobre = new Sobre.Sobre();
            sobre.ShowDialog();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sobre.BACKUP opn_backup = new Sobre.BACKUP();
            opn_backup.ShowDialog();
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void TV_emprestimo_clientes_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }

        private void TV_emprestimo_clientes_DoubleClick(object sender, EventArgs e)
        {

        }

        private void TV_emprestimo_clientes_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CarregaLivrosLV();
        }

        public void CarregaLivrosLV()
        {
           
            try
            {
                objconexao.Open();
            }
            catch
            {

            }

            LV_empretimo_historicoCliente.Clear();
            string _sql = "select tbl_acervo.titulo,tbl_emprestimo.estado FROM tbl_acervo JOIN tbl_emprestimo ON tbl_emprestimo.tbl_acervo_cod_acervo = tbl_acervo.cod_acervo;";
            MySqlCommand cmd = new MySqlCommand(_sql, objconexao);
            MySqlDataAdapter objadapter = new MySqlDataAdapter(_sql, objconexao);
            MySqlDataReader objdatareader = cmd.ExecuteReader();


            // Preenche o ListView
            while (objdatareader.Read())
            {
                ListViewItem objListItem = new ListViewItem(objdatareader.GetValue(0).ToString());
                for (int c = 1; c < objdatareader.FieldCount; c++)
                {
                    objListItem.SubItems.Add(objdatareader.GetValue(c).ToString());
                }
                objListItem.ImageIndex = 1;
                LV_empretimo_historicoCliente.Items.Add(objListItem);
            }
            //fecha o datareader e a conexa
            objdatareader.Close();
            objconexao.Close();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você realmente deseja sair do Livrotec?","Atenção!",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Livrotecnotify_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CMS_NOTIFY.Show(MousePosition.X, MousePosition.Y);
            }           
        }

        private void Livrotecnotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void creditosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sobre.Sobre sobre = new Sobre.Sobre();
            sobre.ShowDialog();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            PesquisaTodos = 1;
            PesquisaHistorico();
            PesquisaTodos = 0;
        }

        public void PesquisaHistorico()
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
                string _SQL = "SELECT * FROM tbl_emprestimo;";
                
                MySqlDataAdapter objadapter = new MySqlDataAdapter(_SQL, objconexao);
                DataSet objdatasert_usu = new DataSet();
                objadapter.Fill(objdatasert_usu, "Emprestimo");
                dg_emprestimos_historico.DataSource = objdatasert_usu;
                dg_emprestimos_historico.DataMember = "Emprestimo";

                dg_emprestimos_historico.AutoResizeColumns();


                dg_emprestimos_historico.Columns[0].HeaderText = "Código do Emprestimo";
                dg_emprestimos_historico.Columns[1].HeaderText = "Código do Acervo";
                dg_emprestimos_historico.Columns[2].HeaderText = "Código do Leitor";
                dg_emprestimos_historico.Columns[3].HeaderText = "Data do Aluguel";
                dg_emprestimos_historico.Columns[4].HeaderText = "Data da Devolução";
                dg_emprestimos_historico.Columns[4].HeaderText = "Estado";
                 


            }
            objconexao.Close();
        }

        private void tb_editora_telefonePrincipal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        
        

        

        

        

        

        

        

       

        

        

        

        

        

        

        


       

       

        

        

       

        

        

        

       

        

        

       

        

        

        

       

        

        

        

        

        




        








    }
}
