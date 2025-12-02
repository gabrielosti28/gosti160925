using System;
using System.Windows.Forms;

namespace gosti2
{
    public partial class infoTela : Form
    {
        public infoTela()
        {
            InitializeComponent();
            this.Text = "Informações do Sistema - BookConnect";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void infoTela_Load(object sender, EventArgs e)
        {
            // Texto detalhado sobre como usar o sistema
            labelComoUsar.Text = "📖 COMO USAR O SISTEMA:\n\n" +
                                "1. 🔐 Faça login ou crie uma conta\n" +
                               
                                "2. 📚 Adicione seus livros favoritos\n" +
                               
                                "3. 💬 Converse com outros leitores\n" +
                               
                                "4. 👥 Compartilhe suas descobertas\n\n" +
                                 
                                "5. Faça seu perfil ser a sua cara\n\n" +
                                "💡 Dica: Use o menu principal para navegar entre as funcionalidades!";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Mantido para compatibilidade
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Mantido para compatibilidade
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void labelCopyright_Click(object sender, EventArgs e)
        {

        }
    }
}