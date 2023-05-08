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
    public partial class FrmMedicacoes : Form
    {
        public FrmMedicacoes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Medicacao md = new Medicacao(
                 txtAlarme.Text, txtInstrucao.Text, txtRemedio.Text, Usuario.ObterPorId(Convert.ToInt32(txtIdUser.Text))
                );
            md.Inserir();
            txtId.Text = md.Id.ToString();
            CarregaGrid();
        }

        private void txtInstrucao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmMedicacoes_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            List<Medicacao> med = Medicacao.Listar();
            int c = 0;
            dgvMedicacao.Rows.Clear();
            foreach (var item in med)
            {
                dgvMedicacao.Rows.Add();
                dgvMedicacao.Rows[c].Cells[0].Value = item.Id.ToString();
                dgvMedicacao.Rows[c].Cells[1].Value = item.Alarme;
                dgvMedicacao.Rows[c].Cells[2].Value = item.Instrucao;
                dgvMedicacao.Rows[c].Cells[3].Value = item.Remedio;
                dgvMedicacao.Rows[c].Cells[4].Value = item.Usuario.Id.ToString();
                c++;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Medicacao md = Medicacao.ObterPorId(Convert.ToInt32(txtId.Text));
            txtAlarme.Text = md.Alarme;
            txtInstrucao.Text = md.Instrucao;
            txtRemedio.Text = md.Remedio;
            txtIdUser.Text = md.Usuario.Id.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Medicacao mc = new Medicacao(txtAlarme.Text, txtInstrucao.Text, txtRemedio.Text);
            mc.Alterar(Convert.ToInt32(txtId.Text));
            CarregaGrid();
        }
    }
}
