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
                Convert.ToDateTime(txtAlarme.Text), txtInstrucao.Text, txtRemedio.Text, Usuario.ObterPorId(Convert.ToInt32(txtIdUser.Text))
                );
            md.Inserir();
            txtId.Text = md.Id.ToString();  
        }
    }
}
