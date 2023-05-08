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
            dtTempo.Text = ex.Tempo;
            comboBox1.Text = ex.TipoExercicio.Id.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void btnComentarioExer_Click(object sender, EventArgs e)
        {
            ComentarioExercicio ce = new ComentarioExercicio(
                Usuario.ObterPorId(Convert.ToInt32(txtIdUserComentario.Text)),
                Exercicio.ObterPorId(Convert.ToInt32(txtIdExerComentario.Text)),
                txtComentario.Text
                );
            ce.InserirComentariosExercicios();
            txtIdComentarioExer.Text = ce.Id.ToString();
        }

        private void btnLikeExer_Click(object sender, EventArgs e)
        {
            LikeExercicio le = new LikeExercicio(
                Usuario.ObterPorId(Convert.ToInt32(txtIdUserLike.Text)),
                Exercicio.ObterPorId(Convert.ToInt32(txtIdExerLike.Text)),
                Convert.ToInt32(txtQuantidade.Text)
                );
            le.Inserir();
            txtIdLikeExer.Text = le.Id.ToString();
        }

        private void btnInserirImagem_Click(object sender, EventArgs e)
        {
            ImagemExercicio ix = new ImagemExercicio(
                Exercicio.ObterPorId(Convert.ToInt32(textBox3.Text)),
                textBox4.Text
                );
            ix.InserirImgReceitas();
            textBox2.Text = ix.Id.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Exercicio exe = new Exercicio(
                txtTitulo.Text, txtDescricao.Text, dtTempo.Text
                );
            exe.Atualizar(Convert.ToInt32(textBox1.Text));
        }
    }
}
