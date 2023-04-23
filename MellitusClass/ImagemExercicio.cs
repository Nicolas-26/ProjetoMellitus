using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class ImagemExercicio
    {
        //atributos
        private int id;
        private string imagens;

        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Exercicio Exercicio { get; set; }
        public string Imagens { get { return imagens; } set {  imagens = value; } }

        //Métodos Construtores
        public ImagemExercicio() { }
        public ImagemExercicio(int id, Exercicio exercicio, string imagens)
        {
            Id = id;
            Exercicio = exercicio;
            Imagens = imagens;
        }
        public ImagemExercicio(Exercicio exercicio, string imagens)
        {
            Exercicio = exercicio;
            Imagens = imagens;
        }

        //Métodos de Acesso

        /// <summary>
        /// 
        /// </summary>
        public void InserirImgReceitas()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert imagemexer (id_exer, imagens) values (@exer, @img)";
            cmd.Parameters.Add("@exer", MySqlDbType.Int32).Value = Exercicio.Id;
            cmd.Parameters.Add("@img", MySqlDbType.VarChar).Value = Imagens;
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
        public static ImagemExercicio ObterPorId(int id)
        {
            ImagemExercicio img = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from imagemexer where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                img = new ImagemExercicio(
                    dr.GetInt32(0),
                    Exercicio.ObterPorId(dr.GetInt32(1)),
                    dr.GetString(2)
                    );
            }
            Banco.Fechar(cmd);
            return img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_exer"></param>
        /// <returns></returns>
        public static List<ImagemExercicio> ListarImgPorExercicios(int id_exer)
        {
            List<ImagemExercicio> list = new List<ImagemExercicio>();
            ImagemExercicio tudo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from imagemexer where id_exer = " + id_exer;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                tudo = new ImagemExercicio();
                tudo.Id = dr.GetInt32(0);
                tudo.Exercicio = Exercicio.ObterPorId(dr.GetInt32(1));
                tudo.Imagens = dr.GetString(2);
                list.Add(tudo);
            }
            Banco.Fechar(cmd);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ImagemExercicio> ListarTudoDeImgExercicios()
        {
            List<ImagemExercicio> lista = new List<ImagemExercicio>();
            ImagemExercicio td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from imagemexer";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                td = new ImagemExercicio();
                td.Id = dr.GetInt32(0);
                td.Exercicio = Exercicio.ObterPorId(dr.GetInt32(1));
                td.Imagens = dr.GetString(2);
                lista.Add(td);
            }
            Banco.Fechar(cmd);
            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void ExcluirImgExercicio(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete imagemexer where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
