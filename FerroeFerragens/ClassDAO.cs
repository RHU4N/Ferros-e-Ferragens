using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FerroeFerragens
{
    public class Campos //Inner Class (Classe Interna)
    {
        public int id_prod;
        public string nome_prod;
        public string descr_prod;
        public decimal valor_unit;
        public int qtde_estoque;
        public string fornecedor;
    }

    class ClassDao
    {
        public Campos campos = new Campos();

        public MySqlConnection minhaConexao;

        public string usuarioBD = "root";
        public string senhaBD = "root";
        public string servidor = "localhost";
        string bancoDados;
        string tabela;

        public void Conecte(string BancoDados, string Tabela)
        {
            bancoDados = BancoDados;
            tabela = Tabela;
            minhaConexao = new MySqlConnection("server=" + servidor +
                "; database=" + bancoDados + "; uid=" + usuarioBD +
                "; password=" + senhaBD);
        }

        void Abrir()
        {
            minhaConexao.Open();
        }

        void Fechar()
        {
            minhaConexao.Close();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * From " + tabela, minhaConexao);

            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView, string busca)
        {
            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * From " + tabela + " where nome_prod like " + "'" + busca + "%';", minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void Insere(string campoNome_prod, string campoDescr_prod, decimal campoValUnit,int campoEstoque,string campoForn)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("insert into " + tabela
                + "(nome_prod,descr_prod,valor_unit,qtde_estoque,fornecedor) " +
                "values(@nome_prod,@descr_prod,@valor_unit,@qtde_estoque,@fornecedor)", minhaConexao);
            comando.Parameters.AddWithValue("@nome_prod", campoNome_prod);
            comando.Parameters.AddWithValue("@descr_prod", campoDescr_prod);
            comando.Parameters.AddWithValue("@valor_unit", campoValUnit);
            comando.Parameters.AddWithValue("@qtde_estoque", campoEstoque);
            comando.Parameters.AddWithValue("@fornecedor", campoForn);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Atualiza(string campoNome_prod, string campoDescr_prod, decimal campoValUnit, int campoEstoque, string campoForn, int id_prod)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("update " + tabela
                                                    + " set nome_prod=@nome_prod, descr_prod=@descr_prod , "
                                                    + "valor_unit=@valor_unit,qtde_estoque=@qtde_estoque,fornecedor=@fornecedor where id_prod=@id_prod", minhaConexao);

            comando.Parameters.AddWithValue("id_prod", id_prod);
            comando.Parameters.AddWithValue("@nome_prod", campoNome_prod);
            comando.Parameters.AddWithValue("@descr_prod", campoDescr_prod);
            comando.Parameters.AddWithValue("@valor_unit", campoValUnit);
            comando.Parameters.AddWithValue("@qtde_estoque", campoEstoque);
            comando.Parameters.AddWithValue("@fornecedor", campoForn);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Consulta(string campoNome)
        {
            //consulta por nome
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela
                                                    + " where nome = '" + campoNome + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id_prod = int.Parse(dtReader["id_prod"].ToString());
                campos.nome_prod = dtReader["nome_prod"].ToString();
                campos.descr_prod = dtReader["descr_prod"].ToString();
                campos.valor_unit = decimal.Parse(dtReader["valor_unit"].ToString());
                campos.qtde_estoque = int.Parse(dtReader["qtde_estoque"].ToString());
                campos.fornecedor = dtReader["fornecedor"].ToString();
            }

            Fechar();
        }

        public void Consulta(int id_prod)
        {
            //consulta por id - sobrecarga de metodo (dois metodos podem ser usados ao mesmo tempo)
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela
                                                    + " where id_prod = '" + id_prod + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id_prod = int.Parse(dtReader["id_prod"].ToString());
                campos.nome_prod = dtReader["nome_prod"].ToString();
                campos.descr_prod = dtReader["descr_prod"].ToString();
                campos.valor_unit = decimal.Parse(dtReader["valor_unit"].ToString());
                campos.qtde_estoque = int.Parse(dtReader["qtde_estoque"].ToString());
                campos.fornecedor = dtReader["fornecedor"].ToString();
            }

            Fechar();
        }


        public void Deletar(int id)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("delete from " + tabela + " where id_prod= '" + id + "'", minhaConexao);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public int NumRegistro()
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("SELECT MAX(id_prod) FROM "
                                                        + tabela, minhaConexao);
            string n = comando.ExecuteScalar().ToString();
            int num = int.Parse(n) + 1;
            Fechar();

            return num;
        }


    }
}

