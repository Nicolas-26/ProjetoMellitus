﻿using System;
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
                0, txtTitulo.Text, txtDescricao.Text, dtTempo.Text,
                TipoExercicio.ObterPorId(Convert.ToInt32(comboBox1.Text))
                );
            exer.InserirExercicios();
            textBox1.Text = exer.Id.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TipoExercicio te = new TipoExercicio(
                txtTipo.Text
                );
            te.InserirTipoExercicio();
            txtId_TipoE.Text = te.Id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VideoExercicio ve = new VideoExercicio(
                Exercicio.ObterPorId(Convert.ToInt32(txtIdVideoExercicio.Text)), txtVideo.Text
                );
            ve.Inserir();
            txtIdVideo.Text = ve.Id.ToString();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Exercicio ex = Exercicio.ObterPorId(Convert.ToInt32(textBox1.Text));
            txtTitulo.Text = ex.Titulo;
            txtDescricao.Text = ex.Descricao;
            dtTempo.Text = ex.Tempo.ToString();
            comboBox1.Text = ex.TipoExercicio.Id.ToString();
        }
    }
}
