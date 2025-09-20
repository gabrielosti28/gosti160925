using System;
using System.Windows.Forms;
using gosti;
using gosti2.Data;
using gosti2.Models;
using System.Data.SqlClient;
using gosti2.Tools;
using Microsoft.Win32;

namespace gosti2
{
    namespace gosti2
    {
        static class Program
        {
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                try
                {
                    // ✅ VERIFICAR E ATUALIZAR BANCO PARA VERSÃO MAIS RECENTE
                    DatabaseEvolutionManager.VerificarEAtualizarBanco();

                    // ✅ VALIDAR ESQUEMA
                    if (!DatabaseSchemaValidator.ValidarEsquema())
                    {
                        Application.Exit();
                        return;
                    }

                    // ✅ INICIALIZAR APLICAÇÃO
                    using (var formMain = new FormMain())
                    {
                        if (formMain.ShowDialog() == DialogResult.OK)
                        {
                            ExecutarAplicacao();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro na inicialização: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //gosti2.Tools.ReferenceVerifier.VerificarTodasReferencias();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // ✅ PRIMEIRO: Verificar todas as referências
                ReferenceVerifier.VerificarTodasReferencias();

                // ✅ DEPOIS: Continuar com a inicialização normal
                if (!DatabaseSchemaValidator.ValidarEsquema())
                {
                    MessageBox.Show("O banco de dados precisa ser corrigido para continuar.",
                        "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // ✅ PRIMEIRO: Validar o esquema do banco
                    if (!DatabaseSchemaValidator.ValidarEsquema())
                    {
                        MessageBox.Show("O banco de dados precisa ser corrigido para continuar.",
                            "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ✅ DEPOIS: Garantir que o banco existe e está atualizado
                    DatabaseManager.GarantirBancoCriado();

                    // ✅ SÓ ENTÃO: Testar conexão
                    if (!DatabaseManager.TestarConexao())
                    {
                        using (var formConfig = new FormConfiguracaoBanco())
                        {
                            if (formConfig.ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }

                    // ✅ TELA DE BOAS-VINDAS
                    using (var formMain = new FormMain())
                    {
                        if (formMain.ShowDialog() == DialogResult.OK)
                        {
                            ExecutarAplicacao();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro inicial: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private static void ExecutarAplicacao()
            {
                // ... implementação existente ...
            }
        }
    }
}
//USE[master]
//GO

//-- Criar banco se não existir (mantendo o nome requerido)
//IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CJ3027333PR2')
//CREATE DATABASE [CJ3027333PR2]
//GO

//USE[CJ3027333PR2]
//GO

//-- =============================================
//-- TABELA DE CONTROLE DE VERSÃO DO SCHEMA
//-- =============================================
//CREATE TABLE SchemaVersion (
//    VersionId INT IDENTITY(1,1) PRIMARY KEY,
//    VersionNumber VARCHAR(20) NOT NULL,
//    Description NVARCHAR(500) NOT NULL,
//    AppliedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
//    ScriptName NVARCHAR(255) NOT NULL
//)
//GO

//-- =============================================
//-- TABELAS PRINCIPAIS (VERSÃO 1.0)
//-- =============================================

//-- USUARIOS com bio básica (podemos expandir depois)
//CREATE TABLE Usuarios (
//    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
//    Nome NVARCHAR(100) NOT NULL,
//    Email NVARCHAR(100) NOT NULL UNIQUE,
//    Senha NVARCHAR(255) NOT NULL,
//    DataNascimento DATE NOT NULL,
//    FotoPerfil VARBINARY(MAX) NULL,
//    Bio NVARCHAR(500) NULL, -- ✅ Bio básica para começar
//    DataCadastro DATETIME2 NOT NULL DEFAULT GETDATE(),
//    UltimoLogin DATETIME2 NULL,
//    Ativo BIT NOT NULL DEFAULT 1,
    
//    -- Campos para futuras expansões
//    Website NVARCHAR(255) NULL,
//    Localizacao NVARCHAR(100) NULL
//)
//GO

//-- LIVROS
//CREATE TABLE Livros (
//    LivroId INT IDENTITY(1,1) PRIMARY KEY,
//    Titulo NVARCHAR(200) NOT NULL,
//    Autor NVARCHAR(100) NOT NULL,
//    Genero NVARCHAR(50) NOT NULL,
//    Descricao NVARCHAR(1000) NULL,
//    Capa VARBINARY(MAX) NULL,
//    DataAdicao DATETIME2 NOT NULL DEFAULT GETDATE(),
//    Favorito BIT NOT NULL DEFAULT 0,
//    Lido BIT NOT NULL DEFAULT 0,
//    UsuarioId INT NOT NULL,
    
//    -- Novos campos para evolução futura
//    ISBN NVARCHAR(20) NULL,
//    AnoPublicacao INT NULL,
//    Editora NVARCHAR(100) NULL,
//    Paginas INT NULL,

//    CONSTRAINT FK_Livros_Usuarios FOREIGN KEY (UsuarioId) 
//        REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE
//)
//GO

//-- COMENTARIOS
//CREATE TABLE Comentarios (
//    ComentarioId INT IDENTITY(1,1) PRIMARY KEY,
//    Texto NVARCHAR(2000) NOT NULL,
//    DataComentario DATETIME2 NOT NULL DEFAULT GETDATE(),
//    Likes INT NOT NULL DEFAULT 0,
//    Dislikes INT NOT NULL DEFAULT 0,
//    LivroId INT NOT NULL,
//    UsuarioId INT NOT NULL,
    
//    -- Campo para futuras funcionalidades
//    Editado BIT NOT NULL DEFAULT 0,
//    DataEdicao DATETIME2 NULL,

//    CONSTRAINT FK_Comentarios_Livros FOREIGN KEY (LivroId) 
//        REFERENCES Livros(LivroId) ON DELETE CASCADE,
//    CONSTRAINT FK_Comentarios_Usuarios FOREIGN KEY (UsuarioId) 
//        REFERENCES Usuarios(UsuarioId)
//)
//GO

//-- LIKES/DISLIKES
//CREATE TABLE LikesDislikes (
//    LikeDislikeId INT IDENTITY(1,1) PRIMARY KEY,
//    LivroId INT NOT NULL,
//    UsuarioId INT NOT NULL,
//    IsLike BIT NOT NULL,
//    DataAcao DATETIME2 NOT NULL DEFAULT GETDATE(),

//    CONSTRAINT FK_LikesDislikes_Livros FOREIGN KEY (LivroId) 
//        REFERENCES Livros(LivroId) ON DELETE CASCADE,
//    CONSTRAINT FK_LikesDislikes_Usuarios FOREIGN KEY (UsuarioId) 
//        REFERENCES Usuarios(UsuarioId),

//    CONSTRAINT UK_LivroUsuario UNIQUE (LivroId, UsuarioId)
//)
//GO

//-- =============================================
//-- ÍNDICES E PERFORMANCE
//-- =============================================
//CREATE UNIQUE INDEX IX_Usuarios_Email ON Usuarios(Email);
//CREATE INDEX IX_Livros_Titulo ON Livros(Titulo);
//CREATE INDEX IX_Livros_Autor ON Livros(Autor);
//CREATE INDEX IX_Comentarios_LivroId ON Comentarios(LivroId);
//GO

//-- =============================================
//--REGISTRAR VERSÃO INICIAL
//-- =============================================
//INSERT INTO SchemaVersion (VersionNumber, Description, ScriptName)
//VALUES ('1.0.0', 'Schema inicial com estrutura básica para evolucao futura', 'CJ3027333PR2_Schema_Evolutivo.sql')
//GO

//PRINT '✅ Banco CJ3027333PR2 criado com estrutura evolutiva!';
//PRINT '✅ Preparado para futuras atualizações';
//PRINT '✅ Versionamento implementado';
//GO