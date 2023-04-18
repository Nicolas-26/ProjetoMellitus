using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class LikeExercicio
    {
        //atributos
        private int id;

        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Usuario Usuario { get; set; }
        public Exercicio Exercicio { get; set; }
        public int Quantidade { get; set; }

        //Métodos Construtores
        public LikeExercicio() { }
        public LikeExercicio(int id, Usuario usuario, Exercicio exercicio, int quantidade)
        {
            Id = id;
            Usuario = usuario;
            Exercicio = exercicio;
            Quantidade = quantidade;
        }
        public LikeExercicio(Usuario usuario, Exercicio exercicio, int quantidade)
        {
            Usuario = usuario;
            Exercicio = exercicio;
            Quantidade = quantidade;
        }

        //Métodos De Acesso
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert likeexer (id_user, id_exer, quant) values (@user, @exer, default)";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = Usuario.Id;
            cmd.Parameters.Add("@exer", MySqlDbType.Int32).Value = Exercicio.Id;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

        public static LikeExercicio ObterPorId(int id)
        {
            LikeExercicio like = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likeexer where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                like = new LikeExercicio(
                    dr.GetInt32(0),
                    Usuario.ObterPorId(dr.GetInt32(1)),
                    Exercicio.ObterPorId(dr.GetInt32(2)),
                    dr.GetInt32(3)
                    );
            }
            Banco.Fechar(cmd);
            return like;
        }

        public static List<LikeExercicio> ObterPorUsuarioEexercicios(int id_user, int id_exer)
        {
            List<LikeExercicio> com = new List<LikeExercicio>();
            LikeExercicio ce = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likeexer set quant where id_user = @user, id_exer = @exer";
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = id_user;
            cmd.Parameters.Add("@exer", MySqlDbType.Int32).Value = id_exer;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ce = new LikeExercicio();
                ce.Id = dr.GetInt32(0);
                ce.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                ce.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                ce.Quantidade = dr.GetInt32(3);
                com.Add(ce);
            }
            Banco.Fechar(cmd);
            return com;
        }

        public static List<LikeExercicio> ListarPorExercicios(int id_exer)
        {
            List<LikeExercicio> list = new List<LikeExercicio>();
            LikeExercicio com = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likeexer where id_exer = " + id_exer;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                com = new LikeExercicio();
                com.Id = dr.GetInt32(0);
                com.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                com.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                com.Quantidade = dr.GetInt32(3);
                list.Add(com);
            }
            Banco.Fechar(cmd);
            return list;
        }

        public static List<LikeExercicio> ListarPorUsuarios(int id_user)
        {
            List<LikeExercicio> list = new List<LikeExercicio>();
            LikeExercicio com = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likeexer where id_user = " + id_user;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                com = new LikeExercicio();
                com.Id = dr.GetInt32(0);
                com.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                com.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                com.Quantidade = dr.GetInt32(3);
                list.Add(com);
            }
            Banco.Fechar(cmd);
            return list;
        }

        public static List<LikeExercicio> Listar()
        {
            List<LikeExercicio> lista = new List<LikeExercicio>();
            LikeExercicio tudo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from likeexer";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tudo = new LikeExercicio();
                tudo.Id = dr.GetInt32(0);
                tudo.Usuario = Usuario.ObterPorId(dr.GetInt32(1));
                tudo.Exercicio = Exercicio.ObterPorId(dr.GetInt32(2));
                tudo.Quantidade = dr.GetInt32(3);
                lista.Add(tudo);
            }
            Banco.Fechar(cmd);
            return lista;
        }

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete likeexer where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
