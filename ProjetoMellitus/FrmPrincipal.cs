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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void exerciciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExercicio frmExercicio = new FrmExercicio();
            //frmExercicio.MdiParent = this;
            frmExercicio.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuarios frmUsuarios = new FrmUsuarios();
            //frmUsuarios.MdiParent = this;
            frmUsuarios.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void glicemiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGlicemia frmGlicemia = new FrmGlicemia();
            frmGlicemia.Show();
        }
    }
}
