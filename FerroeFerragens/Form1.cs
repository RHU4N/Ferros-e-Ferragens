using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FerroeFerragens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ClassDao dao = new ClassDao();
        void LimparCampos()
        {
            txtNome.Clear();
            txtDesc.Clear();
            txtValUn.Clear();
            txtEstoque.Clear();
            txtForn.Clear();
            lblCod_prod.Text = dao.NumRegistro().ToString();
            BtnAlterar.Enabled = false;
            BtnDeletar.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dao.Conecte("ferroeferragens", "tabela_prod");
            dao.PreencheTabela(dataGridView1);
            LimparCampos();
        }

        void ExibirDados()
        {
            lblCod_prod.Text = dao.campos.id_prod.ToString();
            txtNome.Text = dao.campos.nome_prod;
            txtDesc.Text = dao.campos.descr_prod;
            txtValUn.Text = dao.campos.valor_unit.ToString();
            txtEstoque.Text = dao.campos.qtde_estoque.ToString();
            txtForn.Text = dao.campos.fornecedor;

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            // insere
            if (txtNome.Text == "" || txtDesc.Text == "" || txtValUn.Text == "" || txtEstoque.Text == "" || txtForn.Text == "")
            {
                MessageBox.Show("Campos em branco", "AVISO!");
            }
            else
            {
                dao.Insere(txtNome.Text, txtDesc.Text, decimal.Parse(txtValUn.Text), int.Parse(txtEstoque.Text), txtForn.Text);
                MessageBox.Show("Registro gravado com sucesso!", "Informação do Sistema");
                LimparCampos();
                dao.PreencheTabela(dataGridView1);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numLinha = e.RowIndex; //retorna o numero da linha selecionada

            if (numLinha >= 0)
            {
                int idCliente = int.Parse(dataGridView1.Rows[numLinha].Cells[0].Value.ToString());
                dao.Consulta(idCliente);
                ExibirDados();
                BtnAlterar.Enabled = true;
                BtnDeletar.Enabled = true;
            }
        }

        private void Btnpesquisar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "")
            {
                dao.PreencheTabela(dataGridView1, txtNome.Text);
                txtDesc.Clear();
                txtValUn.Clear();
                txtEstoque.Clear();
                txtForn.Clear();
            }
            else
            {
                dao.PreencheTabela(dataGridView1);
                ExibirDados();
                LimparCampos();
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            dao.Atualiza(txtNome.Text, txtDesc.Text, decimal.Parse(txtValUn.Text), int.Parse(txtEstoque.Text), txtForn.Text, int.Parse(lblCod_prod.Text));
            ExibirDados();
            MessageBox.Show("Alterado com sucesso");
            dao.PreencheTabela(dataGridView1);
            LimparCampos();
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja mesmo excluir esse registro?", "AVISO DE EXCLUSÃO",
                                MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
            dao.Deletar(int.Parse(lblCod_prod.Text));
                MessageBox.Show("Registro excluido");
            ExibirDados();
            dao.PreencheTabela(dataGridView1);
            LimparCampos();
                BtnAlterar.Enabled = false;
                BtnDeletar.Enabled = false;
            }
            else
            {
                MessageBox.Show("Registro Mantido");
            }
            
        }
    }

}
