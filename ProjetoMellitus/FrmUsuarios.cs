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
    public partial class FrmUsuarios : FrmPrincipal
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario(
                txtNome.Text, txtSobrenome.Text, Convert.ToInt32(txtIdade.Text), txtSenha.Text, txtEmail.Text, null
                );
            user.Inserir();
            txtId.Text = user.Id.ToString();
            CarregaGrid();
        }

        private void txtPerfil_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            List<Usuario> user = Usuario.Listar();
            int l = 0;
            dataGridView1.Rows.Clear();
            foreach (Usuario u in user)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[l].Cells[0].Value = u.Id.ToString();
                dataGridView1.Rows[l].Cells[1].Value = u.Nome;
                dataGridView1.Rows[l].Cells[2].Value = u.SobreNome;
                dataGridView1.Rows[l].Cells[3].Value = u.Idade;
                dataGridView1.Rows[l].Cells[4].Value = u.Senha;
                dataGridView1.Rows[l].Cells[5].Value = u.Email;
                l++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuario user = Usuario.ObterPorId(Convert.ToInt32(txtId.Text));
            txtNome.Text = user.Nome;
            txtSobrenome.Text = user.SobreNome;
            txtIdade.Text = user.Idade.ToString();
            txtSenha.Text = user.Senha;
            txtEmail.Text = user.Email;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario(txtNome.Text, txtSobrenome.Text, Convert.ToInt32(txtIdade.Text), txtSenha.Text);
            user.Atualizar(Convert.ToInt32(txtId.Text));
            CarregaGrid();
        }
    }
}
