using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MellitusClass;

namespace ProjetoMellitus
{
    public partial class FrmExercicio : Form
    {
        public FrmExercicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Exercicio exer = new Exercicio(
                0, txtTitulo.Text, txtDescricao.Text, Convert.ToDateTime(dtTempo.Text),
                TipoExercicio.ObterPorId(Convert.ToInt32(comboBox1.Text))
                );
            exer.InserirExercicios();
            textBox1.Text = exer.Id.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
