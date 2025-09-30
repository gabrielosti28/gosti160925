# Script para criar Gists no GitHub de todos os arquivos desta pasta
# É necessário já estar logado no GitHub CLI (gh auth login)

# Lista de arquivos da pasta atual
$arquivos = @(
    "App.config",
    "Application Configuration File.config",
    "ApplicationDbContext.cs",
    "Avaliacao.cs",
    "CategoriaTier.cs",
    "Comentario.cs",
    "DatabaseEvolutionManager.cs",
    "DatabaseExporter.cs",
    "DatabaseHelper.cs",
    "DatabaseInitializer.cs",
    "DatabaseManager.cs",
    "DatabaseSchemaValidator.cs",
    "FormAdicionarLivro.cs",
    "FormAdicionarLivro.Designer.cs",
    "FormAdicionarLivro.resx",
    "FormCadastro.cs",
    "FormCadastro.Designer.cs",
    "FormCadastro.resx",
    "FormConfiguracaoBanco.cs",
    "FormConfiguracaoBanco.Designer.cs",
    "FormConfiguracaoBanco.resx",
    "FormLivroAberto.cs",
    "FormLivroAberto.Designer.cs",
    "FormLivroAberto.resx",
    "FormLogin.cs",
    "FormLogin.Designer.cs",
    "FormLogin.resx",
    "FormMain.cs",
    "FormMain.Designer.cs",
    "FormMain.resx",
    "FormMenu.cs",
    "FormMenu.Designer.cs",
    "FormMenu.resx",
    "FormMeusLivros.cs",
    "FormMeusLivros.Designer.cs",
    "FormMeusLivros.resx",
    "FormPrincipal.cs",
    "FormPrincipal.Designer.cs",
    "FormPrincipal.resx",
    "infoTela.cs",
    "infoTela.Designer.cs",
    "infoTela.resx",
    "LikeDislike.cs",
    "Livro.cs",
    "Mensagem.cs",
    "packages.config",
    "Program.cs",
    "Usuario.cs",
    "UsuarioManager.cs",
    "VerifyReferences.cs"
)

# Loop que cria cada Gist
foreach ($arquivo in $arquivos) {
    Write-Host "Criando Gist para $arquivo..."
    gh gist create $arquivo --desc $arquivo
}