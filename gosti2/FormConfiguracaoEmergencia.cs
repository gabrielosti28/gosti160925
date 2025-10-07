using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using gosti2.Data;

namespace gosti2
{
    public partial class FormConfiguracaoEmergencia : Form
    {
        public FormConfiguracaoEmergencia()
        {
            InitializeComponent();
            CarregarConfiguracoes();
        }

        private void CarregarConfiguracoes()
        {
            // ✅ CORREÇÃO: Use o nome correto do ComboBox do Designer
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
            // ✅ CORREÇÃO: Use comboBoxServidores em vez de cmbServidor
            string servidor = comboBoxServidores.Text;

            if (string.IsNullOrWhiteSpace(servidor))
            {
                MessageBox.Show("❌ Por favor, selecione ou digite um servidor.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string stringConexao = $"Server={servidor};Trusted_Connection=true;TrustServerCertificate=true;";

            try
            {
                using (var conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();

                    // ✅ VERIFICAÇÃO ADICIONAL: Tenta acessar o banco mestre
                    using (var command = new SqlCommand("SELECT @@VERSION", conexao))
                    {
                        var version = command.ExecuteScalar();
                        MessageBox.Show($"✅ Conexão com SQL Server bem-sucedida!\n\nServidor: {servidor}\nVersão: {version.ToString().Substring(0, 50)}...",
                            "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Falha na conexão com o servidor '{servidor}':\n\n{ex.Message}",
                    "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCriarBanco_Click(object sender, EventArgs e)
        {
            try
            {
                string servidor = comboBoxServidores.Text;

                if (string.IsNullOrWhiteSpace(servidor))
                {
                    MessageBox.Show("❌ Por favor, selecione um servidor primeiro.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ CORREÇÃO: Use o método estático corretamente
                DatabaseManager.GarantirBancoCriado(); // ✅ SEM 'new DatabaseManager()'

                MessageBox.Show("✅ Banco criado/configurado com sucesso!\n\n" +
                              "O banco 'CJ3027333PR2' foi criado no servidor:\n" +
                              servidor,
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao criar/configurar o banco:\n\n{ex.Message}\n\n" +
                              "Verifique:\n" +
                              "• Se o SQL Server está executando\n" +
                              "• Se você tem permissões adequadas\n" +
                              "• Se o nome do servidor está correto",
                    "Erro na Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODO ADICIONAL: Testar conexão com banco específico
        private void btnTestarBanco_Click(object sender, EventArgs e)
        {
            string servidor = comboBoxServidores.Text;

            if (string.IsNullOrWhiteSpace(servidor))
            {
                MessageBox.Show("❌ Por favor, selecione um servidor primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string stringConexao = $"Server={servidor};Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;";

            try
            {
                using (var conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();

                    // Verifica se o banco existe e tem tabelas
                    using (var command = new SqlCommand(
                        "SELECT COUNT(*) FROM sys.tables WHERE name IN ('Usuarios', 'Livros')", conexao))
                    {
                        var tableCount = (int)command.ExecuteScalar();

                        if (tableCount >= 2)
                        {
                            MessageBox.Show($"✅ Conexão com o banco CJ3027333PR2 bem-sucedida!\n\n" +
                                          $"Servidor: {servidor}\n" +
                                          $"Tabelas encontradas: {tableCount}",
                                "Banco Validado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"⚠️ Banco encontrado mas tabelas estão incompletas.\n\n" +
                                          $"Tabelas encontradas: {tableCount}\n" +
                                          $"Execute a criação do banco.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 4060) // Banco não existe
            {
                MessageBox.Show($"📋 O banco 'CJ3027333PR2' não existe no servidor '{servidor}'.\n\n" +
                              "Clique em 'Criar Banco' para criá-lo automaticamente.",
                    "Banco Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Falha na conexão com o banco:\n\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODO PARA ADICIONAR SERVIDOR MANUALMENTE
        private void btnAdicionarServidor_Click(object sender, EventArgs e)
        {
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "Adicionar Servidor";
                inputDialog.Size = new System.Drawing.Size(350, 150);
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;

                var textBox = new TextBox { Location = new System.Drawing.Point(20, 20), Width = 300 };
                var btnOk = new Button { Text = "OK", Location = new System.Drawing.Point(120, 60), DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancelar", Location = new System.Drawing.Point(200, 60), DialogResult = DialogResult.Cancel };

                inputDialog.Controls.AddRange(new Control[] { textBox, btnOk, btnCancel });
                inputDialog.AcceptButton = btnOk;
                inputDialog.CancelButton = btnCancel;

                if (inputDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (!comboBoxServidores.Items.Contains(textBox.Text))
                    {
                        comboBoxServidores.Items.Add(textBox.Text);
                    }
                    comboBoxServidores.Text = textBox.Text;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}