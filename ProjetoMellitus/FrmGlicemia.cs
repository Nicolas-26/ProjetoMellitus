using MellitusClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoMellitus
{
    public partial class FrmGlicemia : Form
    {
        public FrmGlicemia()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Glicemia gli = new Glicemia(
                Convert.ToInt32(txtValor.Text), txtData.Text,
                Usuario.ObterPorId(Convert.ToInt32(txtUserId.Text))
                );
            gli.Inserir();
            txtId.Text = gli.ID.ToString();
            CarregaGrid();
        }

        private void FrmGlicemia_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            List<Glicemia> gli = Glicemia.Listar();
            int c = 0;
            dataGridView1.Rows.Clear();
            foreach (var item in gli)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[c].Cells[0].Value = item.ID.ToString();
                dataGridView1.Rows[c].Cells[1].Value = item.Valor;
                dataGridView1.Rows[c].Cells[2].Value = item.Data;
                dataGridView1.Rows[c].Cells[3].Value = item.Usuario.Id.ToString();
                c++;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Glicemia gl = Glicemia.ObterPorId(Convert.ToInt32(txtId.Text));
            txtValor.Text = gl.Valor.ToString();
            txtData.Text = gl.Data.ToString();
            txtUserId.Text = gl.Usuario.Id.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Glicemia gcl = new Glicemia(Convert.ToInt32(txtValor.Text), txtData.Text);
            gcl.Atualizar(Convert.ToInt32(txtId.Text));
            CarregaGrid();
        }
    }
}
