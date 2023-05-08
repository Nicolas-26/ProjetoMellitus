using MellitusClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoMellitus
{
    public partial class FrmReceitas : Form
    {
        public FrmReceitas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Receita rc = new Receita(
                txtTitulo.Text, txtDescricao.Text, txtTempo.Text,
                TipoReceita.ObterPorId(Convert.ToInt32(txtIdTipo.Text))
                );  
            rc.InserirReceitas();
            txtId.Text = rc.ID.ToString();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TipoReceita tr = new TipoReceita(
                txtTipo.Text
                );
            tr.Inserir();
            txtIdTipoReceita.Text = tr.Id.ToString();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            VideoReceita vd = new VideoReceita(
                Receita.ObterPorId(Convert.ToInt32(txtIdReceita.Text)), txtVideos.Text
                );
            vd.Inserir();
            txtIdVideoR.Text = vd.Id.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ComentarioReceita cr = new ComentarioReceita(
                Usuario.ObterPorId(Convert.ToInt32(txtIdUserComentario.Text)),
                Receita.ObterPorId(Convert.ToInt32(txtIdRecComentario.Text)),
                txtComentarios.Text
                );
            cr.InserirComentariosReceita();
            txtIdComentario.Text = cr.Id.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LikeReceita lc = new LikeReceita(
                Usuario.ObterPorId(Convert.ToInt32(txtIdUserLike.Text)),
                Receita.ObterPorId(Convert.ToInt32(txtIdRecLike.Text)),
                Convert.ToInt32(txtQuant.Text)
                );
            lc.InserirLikeReceita();
            txtIdLike.Text = lc.Id.ToString();  
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ImagemReceita ir = new ImagemReceita(
                 Receita.ObterPorId(Convert.ToInt32(txtIdRecImagem.Text)),
                 txtImagens.Text
                );
            ir.InseririmgReceitas();
            txtIdImagem.Text = ir.Id.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Receita rc = new Receita(txtTitulo.Text, txtDescricao.Text, txtTempo.Text);
            rc.Atualizar(Convert.ToInt32(txtId.Text));
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Receita rec = Receita.ObterPorId(Convert.ToInt32(txtId.Text));
            txtIdTipo.Text = rec.TipoReceita.Id.ToString();
            txtTitulo.Text = rec.Titulo;
            txtDescricao.Text = rec.Descricao;
            txtTempo.Text = rec.Tempo;
        }
    }
}
