using System;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Tools;
using System.Data.SqlClient;
using System.Configuration;

namespace gosti2.Data
{
    public static class UsuarioManager
    {
        public static Usuario UsuarioLogado { get; private set; }

        public static bool CadastrarUsuario(Usuario novoUsuario)
        {
            DiagnosticContext.FormularioAtual = "UsuarioManager";
            DiagnosticContext.MetodoAtual = "CadastrarUsuario";

            try
            {
                DiagnosticContext.LogarInfo($"Tentando cadastrar usuário: {novoUsuario.Email}");

                using (var context = new ApplicationDbContext())
                {
                    // Verifica se email já existe
                    if (context.Usuarios.Any(u => u.Email == novoUsuario.Email))
                    {
                        DiagnosticContext.LogarAviso($"Tentativa de cadastro com email já existente: {novoUsuario.Email}");
                        MessageBox.Show("Este e-mail já está cadastrado!", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Verifica se nome de usuário já existe
                    if (context.Usuarios.Any(u => u.NomeUsuario == novoUsuario.NomeUsuario))
                    {
                        DiagnosticContext.LogarAviso($"Tentativa de cadastro com nome de usuário já existente: {novoUsuario.NomeUsuario}");
                        MessageBox.Show("Este nome de usuário já está em uso!", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    context.Usuarios.Add(novoUsuario);
                    context.SaveChanges();

                    DiagnosticContext.LogarInfo($"Usuário cadastrado com sucesso: {novoUsuario.NomeUsuario} (ID: {novoUsuario.UsuarioId})");
                    return true;
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro no cadastro do usuário {novoUsuario.Email}", ex);
                MessageBox.Show($"Erro no cadastro: {ex.Message}\n\nDetalhes técnicos foram registrados no sistema.",
                    "Erro de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool Login(string email, string senha)
        {
            DiagnosticContext.FormularioAtual = "UsuarioManager";
            DiagnosticContext.MetodoAtual = "Login";

            try
            {
                DiagnosticContext.LogarInfo($"Tentativa de login: {email}");

                using (var context = new ApplicationDbContext())
                {
                    var usuario = context.Usuarios
                        .FirstOrDefault(u => u.Email == email && u.Senha == senha && u.Ativo);

                    if (usuario != null)
                    {
                        UsuarioLogado = usuario;

                        // Atualizar último login
                        usuario.UltimoLogin = DateTime.Now;
                        context.SaveChanges();

                        DiagnosticContext.UsuarioId = usuario.UsuarioId;
                        DiagnosticContext.LogarInfo($"Login bem-sucedido: {usuario.NomeUsuario} (ID: {usuario.UsuarioId})");
                        return true;
                    }
                    else
                    {
                        DiagnosticContext.LogarAviso($"Tentativa de login falhou para: {email}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro durante tentativa de login: {email}", ex);
                MessageBox.Show($"Erro no login: {ex.Message}\n\nTente novamente ou contate o suporte.",
                    "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void Logout()
        {
            if (UsuarioLogado != null)
            {
                DiagnosticContext.LogarInfo($"Usuário fez logout: {UsuarioLogado.NomeUsuario} (ID: {UsuarioLogado.UsuarioId})");
                DiagnosticContext.UsuarioId = null;
            }
            else
            {
                DiagnosticContext.LogarInfo("Logout executado (nenhum usuário logado)");
            }

            UsuarioLogado = null;
        }

        public static bool VerificarEmailExistente(string email)
        {
            DiagnosticContext.FormularioAtual = "UsuarioManager";
            DiagnosticContext.MetodoAtual = "VerificarEmailExistente";

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    bool existe = context.Usuarios.Any(u => u.Email == email);
                    DiagnosticContext.LogarInfo($"Verificação de email '{email}': {(existe ? "EXISTE" : "NÃO EXISTE")}");
                    return existe;
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao verificar existência do email: {email}", ex);
                MessageBox.Show($"Erro ao verificar email: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Retorna false em caso de erro (considera que não existe)
            }
        }

        public static bool VerificarNomeUsuarioExistente(string nomeUsuario)
        {
            DiagnosticContext.FormularioAtual = "UsuarioManager";
            DiagnosticContext.MetodoAtual = "VerificarNomeUsuarioExistente";

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    bool existe = context.Usuarios.Any(u => u.NomeUsuario == nomeUsuario);
                    DiagnosticContext.LogarInfo($"Verificação de nome de usuário '{nomeUsuario}': {(existe ? "EXISTE" : "NÃO EXISTE")}");
                    return existe;
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao verificar existência do nome de usuário: {nomeUsuario}", ex);
                return false;
            }
        }

        // ✅ NOVO MÉTODO: Atualizar dados do usuário
        public static bool AtualizarUsuario(Usuario usuarioAtualizado)
        {
            DiagnosticContext.FormularioAtual = "UsuarioManager";
            DiagnosticContext.MetodoAtual = "AtualizarUsuario";

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuario = context.Usuarios.Find(usuarioAtualizado.UsuarioId);
                    if (usuario != null)
                    {
                        // Atualiza apenas campos permitidos
                        usuario.NomeUsuario = usuarioAtualizado.NomeUsuario;
                        usuario.Bio = usuarioAtualizado.Bio;
                        usuario.Website = usuarioAtualizado.Website;
                        usuario.Localizacao = usuarioAtualizado.Localizacao;
                        usuario.FotoPerfil = usuarioAtualizado.FotoPerfil;

                        context.SaveChanges();

                        // Atualiza usuário logado se for o mesmo
                        if (UsuarioLogado?.UsuarioId == usuario.UsuarioId)
                        {
                            UsuarioLogado = usuario;
                        }

                        DiagnosticContext.LogarInfo($"Usuário atualizado com sucesso: {usuario.NomeUsuario} (ID: {usuario.UsuarioId})");
                        return true;
                    }
                    else
                    {
                        DiagnosticContext.LogarErro($"Tentativa de atualizar usuário não encontrado: {usuarioAtualizado.UsuarioId}",
                            new Exception("Usuário não encontrado no banco"));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao atualizar usuário ID: {usuarioAtualizado.UsuarioId}", ex);
                MessageBox.Show($"Erro ao atualizar perfil: {ex.Message}",
                    "Erro de Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ✅ NOVO MÉTODO: Buscar usuário por ID
        public static Usuario BuscarUsuarioPorId(int usuarioId)
        {
            DiagnosticContext.FormularioAtual = "UsuarioManager";
            DiagnosticContext.MetodoAtual = "BuscarUsuarioPorId";

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuario = context.Usuarios.Find(usuarioId);
                    if (usuario != null)
                    {
                        DiagnosticContext.LogarInfo($"Usuário encontrado: {usuario.NomeUsuario} (ID: {usuarioId})");
                    }
                    else
                    {
                        DiagnosticContext.LogarAviso($"Usuário não encontrado: ID {usuarioId}");
                    }
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao buscar usuário por ID: {usuarioId}", ex);
                return null;
            }
        }

        // ✅ NOVO MÉTODO: Verificar se usuário está logado
        public static bool EstaLogado()
        {
            bool logado = UsuarioLogado != null;
            if (logado)
            {
                DiagnosticContext.LogarInfo($"Verificação de login: USUÁRIO LOGADO - {UsuarioLogado.NomeUsuario}");
            }
            else
            {
                DiagnosticContext.LogarInfo("Verificação de login: NENHUM USUÁRIO LOGADO");
            }
            return logado;
        }

        // ✅ NOVO MÉTODO: Obter ID do usuário logado (seguro)
        public static int? ObterUsuarioIdLogado()
        {
            var id = UsuarioLogado?.UsuarioId;
            DiagnosticContext.LogarInfo($"ID do usuário logado obtido: {id ?? -1}");
            return id;
        }
    }
}