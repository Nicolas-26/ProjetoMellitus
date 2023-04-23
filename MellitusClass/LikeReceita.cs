using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MellitusClass
{
    public class LikeReceita
    {
        //atributos
        private int id;

        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Usuario Usuario { get; set; }
        public Receita Receita { get; set; }
        public int Quantidade { get; set; }

        //Métodos Construtores
        public LikeReceita() { }
        public LikeReceita(int id, Usuario usuario, Receita receita, int quantidade)
        {
            Id = id;
            Usuario = usuario;
            Receita = receita;
            Quantidade = quantidade;
        }
        public LikeReceita(Usuario usuario, Receita receita, int quantidade)
        {
            Usuario = usuario;
            Receita = receita;
            Quantidade = quantidade;
        }

        //Métodos De Acesso

        /// <summary>
        /// 
        /// </summary>
        public void InserirLikeReceita()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert likerec (id_user, id_rec, quant) values (@user, @rec, default)";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = Usuario.Id;
            cmd.Parameters.Add("@rec", MySqlDbType.Int32).Value = Receita.ID;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LikeReceita ObterPorId(int id)
        {
            LikeReceita like = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likerec where id = " + id;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                like = new LikeReceita(
                    dr.GetInt32(0),
                    Usuario.ObterPorId(dr.GetInt32(1)),
                    Receita.ObterPorId(dr.GetInt32(2)),
                    dr.GetInt32(3)
                    );
            }
            Banco.Fechar(cmd);
            return like;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_user"></param>
        /// <param name="id_rec"></param>
        /// <returns></returns>
        public static List<LikeReceita> ObterPorUsuariosEReceitas(int id_user, int id_rec)
        {
            List<LikeReceita> lkr = new List<LikeReceita>();
            LikeReceita lr = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likerec set quant where id_user = @user, id_rec = @rec";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = id_user;
            cmd.Parameters.Add("@rec", MySqlDbType.Int32).Value = id_rec;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lr = new LikeReceita();
                lr.Id = dr.GetInt32(0);
                lr.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                lr.Receita = Receita.ObterPorId(dr.GetInt32(2));
                lr.Quantidade = dr.GetInt32(3);
                lkr.Add(lr);
            }
            Banco.Fechar(cmd);
            return lkr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_rec"></param>
        /// <returns></returns>
        public static List<LikeReceita> ListarPorReceitas(int id_rec)
        {
            List<LikeReceita> list = new List<LikeReceita>();
            LikeReceita like = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likerec where id_rec = " + id_rec;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                like = new LikeReceita();
                like.Id = dr.GetInt32(0);
                like.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                like.Receita = Receita.ObterPorId(dr.GetInt32(2));
                like.Quantidade = dr.GetInt32(3);
                list.Add(like);
            }
            Banco.Fechar(cmd);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public static List<LikeReceita> ListarPorUsuarios(int id_user)
        {
            List<LikeReceita> lista = new List<LikeReceita>();
            LikeReceita lir = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likerec where id_user = " + id_user;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lir = new LikeReceita();
                lir.Id = dr.GetInt32(0);
                lir.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                lir.Receita = Receita.ObterPorId(dr.GetInt32(2));
                lir.Quantidade = dr.GetInt32(3);
                lista.Add(lir);
            }
            Banco.Fechar(cmd);
            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<LikeReceita> Listar()
        {
            List<LikeReceita> lista = new List<LikeReceita>();
            LikeReceita tudo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likerec";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tudo = new LikeReceita();
                tudo.Id = dr.GetInt32(0);
                tudo.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                tudo.Receita = Receita.ObterPorId(dr.GetInt32(2));
                tudo.Quantidade = dr.GetInt32(3);
                lista.Add(tudo);
            }
            Banco.Fechar(cmd);
            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete likerec where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
