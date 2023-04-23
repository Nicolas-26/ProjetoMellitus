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
    public partial class Form1 : Form
    {
        public Form1()
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
                0, textBox2.Text, textBox3.Text, Convert.ToDateTime(dateTimePicker1.Text),
                TipoExercicio.ObterPorId(Convert.ToInt32(textBox5.Text))
                );
            exer.Inserir();
            textBox1.Text = exer.Id.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
