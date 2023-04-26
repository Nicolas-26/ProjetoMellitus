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
                txtNome.Text, txtSobrenome.Text, Convert.ToInt32(txtIdade.Text), txtSenha.Text, txtEmail.Text
                );
            user.Inserir();
            txtId.Text = user.Id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TipoExercicio tp = new TipoExercicio(
                 textBox2.Text
                );
            tp.InserirTipoExercicio();
            textBox1.Text = tp.Id.ToString();
        }

        private void txtPerfil_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
