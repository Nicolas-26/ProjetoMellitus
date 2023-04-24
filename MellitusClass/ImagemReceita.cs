using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MellitusClass
{
    public class ImagemReceita
    {
        //atributos
        private int id;
        private string imagens;


        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public Receita Receita { get; set; }
        public string Imagens { get { return imagens; } set { imagens = value; } }


        //Métodos Construtores
        public ImagemReceita() { }
        public ImagemReceita(int id, Receita receita, string imagens)
        {
            Id = id;
            Receita = receita;
            Imagens = imagens;
        }
        public ImagemReceita(Receita receita, string imagens)
        {
            Receita = receita;
            Imagens = imagens;
        }


        //Métodos de Acesso

        /// <summary>
        /// Método para inserir campos (id, o id da receita e as imagens das receitas) na tabela imagemrec do banco.
        /// </summary>
        public void InseririmgReceitas()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert imagemrec (id_rec, imagens) values (@rec, @img)";
            cmd.Parameters.Add("@rec", MySqlDbType.Int32).Value = Receita.ID;
            cmd.Parameters.Add("@img", MySqlDbType.VarChar).Value = Imagens;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para consultar todos campos da tabela imagemrec do banco, ao especificar o id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ImagemReceita ObterPorId(int id)
        {
            ImagemReceita imgrec = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from imagemrec where id = " + id;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                imgrec = new ImagemReceita(
                    dr.GetInt32(0),
                    Receita.ObterPorId(dr.GetInt32(1)),
                    dr.GetString(2)
                    );
            }
            Banco.Fechar(cmd);
            return imgrec;
        }


        /// <summary>
        /// Método para trazer todos os campos da tabela imagemrec do banco, ao especificar o id da receita(para saber qual imagem
        /// pertence a receita).
        /// </summary>
        /// <param name="id_rec"></param>
        /// <returns></returns>
        public static List<ImagemReceita> ListarImgReceitas(int id_rec)
        {
            List<ImagemReceita> lista = new List<ImagemReceita>();
            ImagemReceita td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from imagemrec where id_rec = " + id_rec;
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                td = new ImagemReceita();
                td.Id = dr.GetInt32(0);
                td.Receita = Receita.ObterPorId(dr.GetInt32(1));
                td.Imagens = dr.GetString(2);
                lista.Add(td);
            }
            Banco.Fechar(cmd);
            return lista;
        }


        /// <summary>
        /// Método que lista todos elementos da tabela imagemrec do banco e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<ImagemReceita> Listar()
        {
            List<ImagemReceita> lt = new List<ImagemReceita>();
            ImagemReceita tdimg = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from imagemrec";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tdimg = new ImagemReceita();
                tdimg.Id = dr.GetInt32(0);
                tdimg.Receita = Receita.ObterPorId(dr.GetInt32(1));
                tdimg.Imagens = dr.GetString(2);
                lt.Add(tdimg);
            }
            Banco.Fechar(cmd);
            return lt;
        }

        /// <summary>
        /// Método para excluir um campo inteiro da tabela imagemrec do banco.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from imagemrec where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
