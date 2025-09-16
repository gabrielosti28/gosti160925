using System.Windows.Forms;
using gosti2;

namespace gosti
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Text = "Bem-vindo ao Sistema de Livros";
        }

        private void btnAvancar_Click_1(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSair_Click_1(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do sistema?", "Confirmação de Saída",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnInformacoes_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            using (var infoForm = new infoTela())
            {
                infoForm.ShowDialog();
            }
            this.Show();
        }

        private void panelConteudo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}