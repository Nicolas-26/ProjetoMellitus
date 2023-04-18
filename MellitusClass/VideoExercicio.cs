using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class VideoExercicio
    {
        //atributos
        private int id;
        private string video;

        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Exercicio Exercicio { get; set; }
        public string Video { get { return video; } set {  video = value; } }

        //Métodos Construtores
        public VideoExercicio() { }
        public VideoExercicio(int id, Exercicio exercicio, string video)
        {
            Id = id;
            Exercicio = exercicio;
            Video = video;
        }
        public VideoExercicio(Exercicio exercicio, string video)
        {
            Exercicio = exercicio;
            Video = video;
        }

        //Métodos De Acesso
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert videoexer (id_exer, video) values (@exer, @video)";
            cmd.Parameters.Add("@exer", MySqlDbType.Int32).Value = Exercicio.Id;
            cmd.Parameters.Add("@video", MySqlDbType.VarChar).Value = Video;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

        public static VideoExercicio ObterPorId(int id)
        {
            VideoExercicio video = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from videoexer where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                video = new VideoExercicio(
                    dr.GetInt32(0),
                    Exercicio.ObterPorId(dr.GetInt32(1)),
                    dr.GetString(2)
                    );
            }
            Banco.Fechar(cmd);
            return video;
        }

        public List<VideoExercicio> ListarVideosDosExercicios(int id_exer)
        {
            List<VideoExercicio> list = new List<VideoExercicio>();
            VideoExercicio td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from videoexer set video where id_exer = " + id_exer;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                td = new VideoExercicio();
                td.Id = dr.GetInt32(0);
                td.Exercicio = Exercicio.ObterPorId(dr.GetInt32(1));
                td.Video = dr.GetString(2);
                list.Add(td);
            }
            Banco.Fechar(cmd);
            return list;
        }

        public List<VideoExercicio> Listar()
        {
            List<VideoExercicio> list = new List<VideoExercicio>();
            VideoExercicio td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from videoexer";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                td = new VideoExercicio();
                td.Id = dr.GetInt32(0);
                td.Exercicio = Exercicio.ObterPorId(dr.GetInt32(1));
                td.Video = dr.GetString(2);
                list.Add(td);
            }
            Banco.Fechar(cmd);
            return list;
        }

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete imagemexer where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }

}
