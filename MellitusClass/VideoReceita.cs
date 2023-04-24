using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class VideoReceita
    {
        //atributos
        private int id;
        private string videos;


        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Receita Receita { get; set; }
        public string Video { get { return videos; } set { videos = value; } }


        //Métodos Construtores
        public VideoReceita() { }
        public VideoReceita(int id, Receita receita, string videos)
        {
            Id = id;
            Receita = receita;
            Video = videos;
        }
        public VideoReceita(Receita receita, string videos)
        {
            Receita = receita;
            Video = videos;
        }



        //Métodos De Acesso

        /// <summary>
        ///  Método para inserir campos (id, id da receita e o video das receitas) na tabela videorec do banco.
        /// </summary>
        public void Inserir()
        {
            var vd = Banco.Abrir();
            vd.CommandText = "insert videorec (id_rec, videos) values (@rec, @videos)";
            vd.Parameters.Add("@rec", MySqlDbType.Int32).Value = Receita.ID;
            vd.Parameters.Add("@videos", MySqlDbType.VarChar).Value = Video;
            vd.ExecuteNonQuery();
            vd.CommandText = "select @@identity";
            Id = Convert.ToInt32(vd.ExecuteScalar());
            Banco.Fechar(vd);
        }


        /// <summary>
        /// Método que traz todos campos da tabela videorec onde o id ser especifícado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static VideoReceita ObterPorId(int id)
        {
            VideoReceita vdr = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from videorec where id = " + id;
            var vd = cmd.ExecuteReader();
            while(vd.Read())
            {
                vdr = new VideoReceita(
                    vd.GetInt32(0),
                    Receita.ObterPorId(Convert.ToInt32(vd.GetInt32(1))),
                    vd.GetString(2)
                    );
            }
            Banco.Fechar(cmd);
            return vdr;
        }


        /// <summary>
        /// Método para trazer todos videos das receitas com o id da receita(para saber que video pertence a receita).
        /// </summary>
        /// <param name="id_rec"></param>
        /// <returns></returns>
        public static List<VideoReceita> ObterVideosPorReceitas(int id_rec)
        {
            List<VideoReceita> list = new List<VideoReceita>();
            VideoReceita video = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select videos from videorec where id_rec = " + id_rec;
            var rec = cmd.ExecuteReader();
            while(rec.Read())
            {
                video = new VideoReceita();
                video.Id = rec.GetInt32(0);
                video.Receita = Receita.ObterPorId(rec.GetInt32(1));
                video.Video = rec.GetString(2);
                list.Add(video);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Método para listar todos elementos da tabela videorec do banco e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<VideoReceita> Listar()
        {
            List<VideoReceita> lista = new List<VideoReceita>();
            VideoReceita vd = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from videorec";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                vd = new VideoReceita();
                vd.Id = dr.GetInt32(0);
                vd.Receita = Receita.ObterPorId(dr.GetInt32(1));
                vd.Video = dr.GetString(2);
                lista.Add(vd);
            }
            Banco.Fechar(cmd);
            return lista;
        }


        /// <summary>
        /// Método Para excluir um campo inteiro da tabela videorec do banco ao ser inserido o id.
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from videorec where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
