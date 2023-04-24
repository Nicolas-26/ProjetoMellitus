using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class Usuario
    {
        //atributos
        private int id;
        private string nome;
        private string sobreNome;
        private int idade;
        private string senha;
        private string email;


        //propriedades
        public int Id { get { return id; } set {  id = value; } }
        public string Nome { get {  return nome; } set {  nome = value; } }
        public string SobreNome { get {  return sobreNome; } set {  sobreNome = value; } }
        public int Idade { get {  return idade; } set { idade = value;  } }
        public string Senha { get { return senha; } set { senha = value; } }
        public string Email { get { return email; } set {  email = value; } }


        //Métodos construtores
        public Usuario() { }
        public Usuario(int id, string nome, string sobreNome, int idade, string senha, string email)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            Idade = idade;
            Senha = senha;
            Email = email;
        }
        public Usuario(string nome, string sobreNome, int idade, string senha, string email)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Idade = idade;
            Senha = senha;
            Email = email;
        }


        //Métodos De Acesso

        /// <summary>
        /// Método para cadastrar com email e senha.
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Usuario EfetuarLogin(string senha, string email)
        {
            Usuario user = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select nome, sobrenome from usuarios where senha = @senha and email = @email";
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                user = new Usuario(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2),
                    dr.GetInt32(3),
                    dr.GetString(4),
                    dr.GetString(5)
                    );
            }
            Banco.Fechar(cmd);
            return user;
        }


        /// <summary>
        /// Método para inserir campos (id, nome, sobrenome, idade, senha e email tudo do usuário) na tabela usuarios do banco
        /// </summary>
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insere_usuario";
            cmd.Parameters.AddWithValue("_id", 0).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("_nome", Nome).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_sobrenome", SobreNome).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_idade", Idade).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_senha", Senha).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_email", Email).Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
            Id = Convert.ToInt32(cmd.Parameters["_id"].Value);
            Banco.Fechar(cmd);
        }
       

        /// <summary>
        /// Método para trazer todos campos da tabela usuarios do banco onde o id ser especifícado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Usuario ObterPorId(int id)
        {
            Usuario user = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from usuarios where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                user = new Usuario(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2),
                    dr.GetInt32(3),
                    dr.GetString(4),
                    dr.GetString(5)
                    );
            }
            Banco.Fechar(cmd);
            return user;
        }


        /// <summary>
        /// Métodos para listar todos os usuários se o adm não iniciar com nenhuma letra na barra de pesquisa, caso o adm iniciar
        /// com qualquer letra haverá uma consulta trazendo apenas os usuários cujo as primeira letra da barra de pesquisa
        /// ser idêntica
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static List<Usuario> Listar(string texto = "")
        {
            List<Usuario> list = new List<Usuario>();
            Usuario user = null;
            var cmd = Banco.Abrir();
            if(texto!=string.Empty)
            {
                cmd.CommandText = "select * from usuarios where nome like '" + texto + "%'";
            }
            else
            {
                cmd.CommandText = "select * from usuarios";
            }
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                user = new Usuario();
                user.Id = dr.GetInt32(0);
                user.Nome = dr.GetString(1);
                user.SobreNome = dr.GetString(2);
                user.Idade = dr.GetInt32(3);
                user.Senha = dr.GetString(4);
                user.Email = dr.GetString(5);
                list.Add(user);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Método para atualizar os campos (nome, sobrenome, idade, senha) da tabela usuarios onde o id ser especifícado.
        /// </summary>
        /// <param name="id"></param>
        public void Atualizar(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "update usuarios set nome = @nome, sobrenome = @sobrenome, idade = @idade, senha = @senha where id = " + id;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = Nome;
            cmd.Parameters.Add("@sobrenome", MySqlDbType.VarChar).Value = SobreNome;
            cmd.Parameters.Add("@idade", MySqlDbType.Int32).Value = Idade;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = Senha;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para excluir um campo inteiro da tabela usuarios do banco.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from usuarios where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
