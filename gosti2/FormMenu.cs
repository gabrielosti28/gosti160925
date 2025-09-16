using System.Windows.Forms;
using gosti2;

namespace gosti
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            this.Text = "Menu Principal - BookConnect";
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            using (var formLogin = new FormLogin())
            {
                if (formLogin.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            this.Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            using (var formCadastro = new FormCadastro())
            {
                if (formCadastro.ShowDialog() == DialogResult.OK)
                {
                    // Cadastro bem-sucedido, pode fazer login agora
                    MessageBox.Show("Cadastro realizado com sucesso! Faça login para continuar.",
                                  "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.Show();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair do sistema?", "Confirmação de Saída",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void panelBotoes_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}