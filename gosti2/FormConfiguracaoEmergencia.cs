using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using gosti2.Data;

namespace gosti2
{
    public partial class FormConfiguracaoEmergencia : Form
    {
        private ComboBox comboBoxServidores;
        private Button btnTestarConexao;
        private Button btnCriarBanco;
        private Button btnCancelar;
        private Label labelTitulo;
        private Label labelServidor;

        public FormConfiguracaoEmergencia()
        {
            // ✅ INICIALIZAÇÃO RÁPIDA - SEM COMPONENTES COMPLEXOS
            InitializeComponentCustom();

            // ✅ CARREGA CONFIGURAÇÕES EM SEGUNDO PLANO
            System.Threading.Tasks.Task.Run(() => CarregarConfiguracoesBackground());
        }

        private void InitializeComponentCustom()
        {
            // ✅ CONFIGURAÇÃO BÁSICA E RÁPIDA
            this.Text = "Configuração de Banco";
            this.Size = new Size(450, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // ✅ CONTROLES ESSENCIAIS APENAS
            labelTitulo = new Label
            {
                Text = "🔧 Configuração de Banco",
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            comboBoxServidores = new ComboBox
            {
                Location = new Point(20, 60),
                Size = new Size(300, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnTestarConexao = new Button
            {
                Text = "Testar Conexão",
                Location = new Point(330, 58),
                Size = new Size(90, 25)
            };

            btnCriarBanco = new Button
            {
                Text = "Criar Banco",
                Location = new Point(20, 100),
                Size = new Size(100, 30)
            };

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(130, 100),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            // ✅ EVENTOS
            btnTestarConexao.Click += btnTestarConexao_Click;
            btnCriarBanco.Click += btnCriarBanco_Click;
            btnCancelar.Click += btnCancelar_Click;

            // ✅ ADICIONA CONTROLES
            this.Controls.AddRange(new Control[] {
        labelTitulo, comboBoxServidores, btnTestarConexao,
        btnCriarBanco, btnCancelar
    });

            this.CancelButton = btnCancelar;
        }

        private void CarregarConfiguracoesBackground()
        {
            // ✅ CARREGA SERVIDORES BÁSICOS
            var servidores = new[] { "(localdb)\\MSSQLLocalDB", ".\\SQLEXPRESS", "(local)", "localhost" };

            // ✅ ATUALIZA UI NA THREAD PRINCIPAL
            this.Invoke(new Action(() =>
            {
                comboBoxServidores.Items.AddRange(servidores);
                comboBoxServidores.SelectedIndex = 0;
            }));
        }

        private void CarregarConfiguracoes()
        {
            comboBoxServidores.Items.AddRange(new[] {
                "(localdb)\\MSSQLLocalDB",
                ".\\SQLEXPRESS",
                "(local)",
                "localhost"
            });
            comboBoxServidores.SelectedIndex = 0;
        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            string servidor = comboBoxServidores.Text;

            if (string.IsNullOrWhiteSpace(servidor))
            {
                MessageBox.Show("❌ Por favor, selecione um servidor.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string stringConexao = $"Server={servidor};Trusted_Connection=true;TrustServerCertificate=true;";

            try
            {
                using (var conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    MessageBox.Show($"✅ Conexão com SQL Server bem-sucedida!\n\nServidor: {servidor}",
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Falha na conexão:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCriarBanco_Click(object sender, EventArgs e)
        {
            try
            {
                string servidor = comboBoxServidores.Text;

                if (string.IsNullOrWhiteSpace(servidor))
                {
                    MessageBox.Show("❌ Por favor, selecione um servidor.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ TENTA CRIAR BANCO
                DatabaseManager.GarantirBancoCriado();

                MessageBox.Show("✅ Banco criado/configurado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao criar banco:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}