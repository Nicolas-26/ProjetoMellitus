using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class ComentarioExercicio
    {
        //atributos
        private int id;
        private string comentarios;


        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Usuario Usuario { get; set; }
        public Exercicio Exercicio { get; set; }
        public string Comentarios { get {  return comentarios; } set {  comentarios = value; } }


        //Métodos Construtores
        public ComentarioExercicio() { }
        public ComentarioExercicio(int id, Usuario usuario, Exercicio exercicio, string comentarios)
        {
            Id = id;
            Usuario = usuario;
            Exercicio = exercicio;
            Comentarios = comentarios;
        }
        public ComentarioExercicio(Usuario usuario, Exercicio exercicio, string comentarios)
        {
            Usuario = usuario;
            Exercicio = exercicio;
            Comentarios = comentarios;
        }


        //Métodos De Acesso

        /// <summary>
        /// Método para Inserir Campos(id, um usuário, um exercício e o comentario feito pelo usuário) na tabela comentariosexer 
        /// No Banco de Dados
        /// </summary>
        public void InserirComentariosExercicios()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert comentarioexer (id_user, id_exer, comentarios)" +
                " values (@user, @exer, @comentarios)";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = Usuario.Id;
            cmd.Parameters.Add("@exer",MySqlDbType.Int32).Value = Exercicio.Id;
            cmd.Parameters.Add("@comentarios",MySqlDbType.VarChar).Value = Comentarios;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para consultar um comentário no exercício, ao especificar o id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ComentarioExercicio ObterPorId(int id)
        {
            ComentarioExercicio com = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from comentariosexer where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                com = new ComentarioExercicio(
                    dr.GetInt32(0),
                    Usuario.ObterPorId(dr.GetInt32(1)),
                    Exercicio.ObterPorId(dr.GetInt32(2)),
                    dr.GetString(3)
                    );
            }
            Banco.Fechar(cmd);
            return com;
        }


        /// <summary>
        /// Método que retorna os comentários da tabela comentariosexer do banco, ao inserir o id do usuario e o id do exercicio
        /// </summary>
        /// <param name="id_user"></param>
        /// <param name="id_exer"></param>
        /// <returns></returns>
        public static List<ComentarioExercicio> ObterPorUsuarioEexercicios(int id_user, int id_exer)
        {
            List<ComentarioExercicio> com = new List<ComentarioExercicio>();
            ComentarioExercicio ce = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select comentarios from comentariosexer where id_user = @user and id_exer = @exer";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = id_user;
            cmd.Parameters.Add("@exer", MySqlDbType.Int32).Value = id_exer;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                ce = new ComentarioExercicio();
                ce.Id = dr.GetInt32(0);
                ce.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                ce.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                ce.Comentarios = dr.GetString(3);
                com.Add(ce);
            }
            Banco.Fechar(cmd);
            return com;
        }


        /// <summary>
        /// Métodos que retorna os campo da tabela comentariosexer ao saber em qual exercício foi comentado.
        /// </summary>
        /// <param name="id_exer"></param>
        /// <returns></returns>
        public static List<ComentarioExercicio> ListarPorExercicios(int id_exer)
        {
            List<ComentarioExercicio> list = new List<ComentarioExercicio>();
            ComentarioExercicio com = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from comentariosexer where id_exer = " + id_exer;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                com = new ComentarioExercicio();
                com.Id = dr.GetInt32(0);
                com.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                com.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                com.Comentarios = dr.GetString(3);
                list.Add(com);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Métodos que retorna os campo da tabela comentariosexer ao saber em qual exercício o usuário comentou.
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public static List<ComentarioExercicio> ListarPorUsuarios(int id_user)
        {
            List<ComentarioExercicio> list = new List<ComentarioExercicio>();
            ComentarioExercicio com = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from comentariosexer where id_user = " + id_user;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                com = new ComentarioExercicio();
                com.Id = dr.GetInt32(0);
                com.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                com.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                com.Comentarios = dr.GetString(3);
                list.Add(com);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Método que lista todos os elementos que estão na tabela comentariosexer(do banco de dados) e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<ComentarioExercicio> Listar()
        {
            List<ComentarioExercicio> lista = new List<ComentarioExercicio>();
            ComentarioExercicio tudo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from comentariosexer";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                tudo = new ComentarioExercicio();
                tudo.Id = dr.GetInt32(0);
                tudo.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                tudo.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                tudo.Comentarios = dr.GetString(3);
                lista.Add(tudo);
            }
            Banco.Fechar(cmd);
            return lista;
        }


        /// <summary>
        /// Método para excluir um comentário no exercicio não apropriado feito pelo usuário
        /// </summary>
        /// <param name="id"></param>
        public void ExcluirComentariosExer(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from comentariosexer where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
