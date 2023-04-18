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

        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert usuarios (nome, sobrenome, idade, senha, email, perfil)" +
                " values (@nome, @sobrenome, @idade, @senha, @email, default)";
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = Nome;
            cmd.Parameters.Add("@sobrenome", MySqlDbType.VarChar).Value = SobreNome;
            cmd.Parameters.Add("@idade", MySqlDbType.Int32).Value = Idade;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = Senha;
            cmd.Parameters.Add("email", MySqlDbType.VarChar).Value = Email;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

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

        public static List<Usuario> Listar(string texto = "")
        {
            List<Usuario> list = new List<Usuario>();
            Usuario user = null;
            var cmd = Banco.Abrir();
            if(texto!=string.Empty)
            {
                cmd.CommandText = "select * from usuarios where nome like '%" + texto + "%'";
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

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from usuarios where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
