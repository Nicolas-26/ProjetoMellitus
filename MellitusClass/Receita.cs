        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class Receita
    {
        //atributos
        private int id;
        private string titulo;
        private string descricao;
        private int tempo;


        //propriedades
        public int ID { get { return id; } set { id = value; } }
        public TipoReceita TipoReceita { get; set; }
        public string Titulo { get { return titulo; } set { titulo = value; } }
        public string Descricao { get { return descricao; } set { descricao = value; } }
        public int Tempo { get { return tempo; } set { tempo = value; } }


        //Métodos Construtores

        public Receita() { }    

        public Receita(int id, string titulo, string descricao, int tempo, TipoReceita tipoReceita)
        {
            ID = id;
            Titulo = titulo;
            Descricao = descricao;
            Tempo = tempo;
            TipoReceita = tipoReceita;
        }
        public Receita(string titulo, string descricao, int tempo, TipoReceita tipoReceita)
        {
            Titulo = titulo;
            Descricao = descricao;
            Tempo = tempo;
            TipoReceita = tipoReceita;
        }
        public Receita(string titulo)
        {
            Titulo = titulo;
        }


        //Métodos De Acesso

        /// <summary>
        /// Método Para inserir campos (id, o título da receita, a descrição da receita, o tempo que a receita tem e o id do tipo
        /// da receita) na tabela receitas no banco.
        /// </summary>
        public void InserirReceitas()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insere_receita";
            cmd.Parameters.AddWithValue("_id", 0).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("_titulo", Titulo).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_descricao", Descricao).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_tempo", Tempo).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_id_tipo", TipoReceita.Id).Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
            ID = Convert.ToInt32(cmd.Parameters["_id"].Value);
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para trazer todos os campos da tabela receitas do banco onde o id é especifícado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Receita ObterPorId(int id)
        {
            Receita receita = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from receitas where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                receita = new Receita(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetInt32(3), TipoReceita.ObterPorId((4)));
            }
            Banco.Fechar(cmd);
            return receita;
        }


        /// <summary>
        /// Método que traz todos os campos da tabela receitas ao informar o id do tipo da receita.
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static List<Receita> ListarTiposPorReceitas(int id_tipo)
        {
            List<Receita> list = new List<Receita>();
            Receita RC = new Receita();
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from receitas where id_tipo = " + id_tipo;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                RC.ID = dr.GetInt32(0);
                RC.TipoReceita = TipoReceita.ObterPorId(dr.GetInt32(1));
                RC.Titulo = dr.GetString(2);
                RC.Descricao = dr.GetString(3);
                RC.Tempo = dr.GetInt32(4);
                list.Add(RC);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Método que traz o título da tabela receita ao inserir o id do tipo da receita(para saber os titulos dos tipos de receita).
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static Receita ObterTituloPorIdDoTipo(int id_tipo)
        {
            Receita rec = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select titulo from receitas where id_tipo = " + id_tipo;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                rec = new Receita(dr.GetString(2));
            }
            Banco.Fechar(cmd);
            return rec;
        }


        /// <summary>
        /// Método que lista todos elementos da tabela receitas do banco e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<Receita> Listar()
        {
            List<Receita> receitas = new List<Receita>();
            Receita rec = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from receitas";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                rec = new Receita();
                rec.ID = dr.GetInt32(0);
                rec.TipoReceita = TipoReceita.ObterPorId(dr.GetInt32(1));
                rec.Titulo = dr.GetString(2);
                rec.Descricao = dr.GetString(3);
                rec.Tempo = dr.GetInt32(4);
                receitas.Add(rec);
            }
            Banco.Fechar(cmd);
            return receitas;
        }


        /// <summary>
        /// Método para atualizar campos (título, Descrição e tempo) da tabela receitas onde o id ser especifícado.
        /// </summary>
        /// <param name="id"></param>
        public void Atualizar(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "update receitas set titulo = @titulo, descricao = @desc, tempo = @tempo where id = " + id;
            cmd.Parameters.Add("@titulo", MySqlDbType.VarChar).Value = Titulo;
            cmd.Parameters.Add("@desc",MySqlDbType.VarChar).Value = Descricao;
            cmd.Parameters.Add("@tempo", MySqlDbType.DateTime).Value = Tempo;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para excluir um campo inteiro da tabela receitas do banco.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from receitas where id " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
