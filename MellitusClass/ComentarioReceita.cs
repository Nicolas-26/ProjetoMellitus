using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class ComentarioReceita
    {
        //atributos
        private int id;
        private string comentario;


        //propriedades
        public int Id { get { return id; } set { id = value;  } }
        public Usuario Usuarios { get; set; }
        public Receita Receitas { get; set; }
        public string Comentario { get {  return comentario; } set {  comentario = value; } }


        //Métodos Construtores
        public ComentarioReceita() { }
        public ComentarioReceita(int id, Usuario user, Receita rec, string comentario)
        {
            Id = id;
            Usuarios = user;
            Receitas = rec;
            Comentario = comentario;
        }
        public ComentarioReceita(Usuario user, Receita rec, string comentario)
        {
            Usuarios = user;
            Receitas = rec;
            Comentario = comentario;
        }


        //Métodos De Acesso

        /// <summary>
        /// Inserir Campos(id, um usuário, uma receita e o comentario feito pelo usuário) na tabela comentariosrec 
        /// No Banco de Dados
        /// </summary>
        public void InserirComentariosReceita()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert comentariosrec (comentarios, id_user, id_rec) values (@comentarios, @user, @rec)";
            cmd.Parameters.Add("@comentarios", MySqlDbType.VarChar).Value = Comentario;
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = Usuarios.Id;
            cmd.Parameters.Add("@rec", MySqlDbType.Int32).Value = Receitas.ID;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);  
        }


        /// <summary>
        /// Método para consultar um comentário na receita obtendo o id para consultar. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ComentarioReceita ObterPorId(int id)
        {
            ComentarioReceita rct = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from comentariosrec where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                rct = new ComentarioReceita(
                    dr.GetInt32(0),
                    Usuario.ObterPorId(Convert.ToInt32(dr.GetInt32(2))),
                    Receita.ObterPorId(Convert.ToInt32(dr.GetInt32(3))),
                    dr.GetString(1)
                    );
            }
            Banco.Fechar(cmd);
            return rct;
        }


        /// <summary>
        /// Método que retorna os comentários da tabela comentariosrec do banco, ao inserir o id do usuario e o id da receita
        /// </summary>
        /// <param name="id_user"></param>
        /// <param name="id_rec"></param>
        /// <returns></returns>
        public static List<ComentarioReceita> ObterComentariosPorUserEReceita(int id_user, int id_rec)
        {
            List<ComentarioReceita> cr = new List<ComentarioReceita>();
            ComentarioReceita crt = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select comentarios from comentariosrec where id_user = @user and id_rec = @rec";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = id_user;
            cmd.Parameters.Add("@rec", MySqlDbType.Int32).Value = id_rec;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                crt = new ComentarioReceita();
                crt.Id = dr.GetInt32(0);
                crt.Usuarios = Usuario.ObterPorId(dr.GetInt32(2));
                crt.Receitas = Receita.ObterPorId(dr.GetInt32(3));
                crt.Comentario = dr.GetString(1);
                cr.Add(crt);
            }
            Banco.Fechar(cmd);
            return cr;
        }


        /// <summary>
        /// Métodos que retorna os comentários feito pelo usuário dentro da receita, ao informar qual id da receita.
        /// </summary>
        /// <param name="id_rec"></param>
        /// <returns></returns>
        public static List<ComentarioReceita> ListarPorReceitas(int id_rec)
        {
            List<ComentarioReceita> list = new List<ComentarioReceita>();
            ComentarioReceita com = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select comentarios from comentariosrec where id_rec = " + id_rec;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                com = new ComentarioReceita();
                com.Id = dr.GetInt32(0);
                com.Usuarios = Usuario.ObterPorId(dr.GetInt32(2));
                com.Receitas = Receita.ObterPorId(dr.GetInt32(3));
                com.Comentario = dr.GetString(1);
                list.Add(com);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Métodos que retorna os comentários feito pelo usuário ao informar o id do usuário (para saber qual usuário comentou).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<ComentarioReceita> ListarPorUsuarios(int user)
        {
            List<ComentarioReceita> lista = new List<ComentarioReceita>();
            ComentarioReceita us = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select comentarios from comentariosrec where id_user = " + user;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                us = new ComentarioReceita();
                us.Id = dr.GetInt32(0);
                us.Usuarios = Usuario.ObterPorId(dr.GetInt32(2));
                us.Receitas = Receita.ObterPorId(dr.GetInt32(3));
                us.Comentario = dr.GetString(1);
                lista.Add(us);
            }
            Banco.Fechar(cmd);
            return lista;
        }


        /// <summary>
        /// Método que lista todos os elementos que estão na tabela comentariosrec(do banco de dados) e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<ComentarioReceita> ListarComentariosRec()
        {
            List<ComentarioReceita> lcr = new List<ComentarioReceita>();
            ComentarioReceita rec = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from comentariosrec";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                rec = new ComentarioReceita();
                rec.Id = dr.GetInt32(0);
                rec.Usuarios = Usuario.ObterPorId(dr.GetInt32(2));
                rec.Receitas = Receita.ObterPorId(dr.GetInt32(3));
                rec.Comentario = dr.GetString(1);
                lcr.Add(rec);
            }
            Banco.Fechar(cmd);
            return lcr;
        }


        /// <summary>
        /// Método para excluir um comentário na receita não apropriado feito pelo usuário
        /// </summary>
        /// <param name="id"></param>
        public void ExcluirComentariosReceita(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from comentariosrec where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
