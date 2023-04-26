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
    public partial class FrmGlicemia : Form
    {
        public FrmGlicemia()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Glicemia gli = new Glicemia(
                Convert.ToInt32(txtValor.Text), Convert.ToDateTime(txtData.Text), Usuario.ObterPorId(Convert.ToInt32(txtUserId.Text))
                );
            gli.Inserir();
            txtId.Text = gli.ID.ToString(); 
        }
    }
}
