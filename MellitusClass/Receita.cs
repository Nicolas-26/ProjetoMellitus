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
        private DateTime tempo;

        //propriedades
        public int ID { get { return id; } set { id = value; } }
        public TipoReceita TipoReceita { get; set; }
        public string Titulo { get { return titulo; } set { titulo = value; } }
        public string Descricao { get { return descricao; } set { descricao = value; } }
        public DateTime Tempo { get { return tempo; } set { tempo = value; } }


        //Métodos Construtores

        public Receita() { }    

        public Receita(int id, string titulo, string descricao, DateTime tempo, TipoReceita tipoReceita)
        {
            ID = id;
            TipoReceita = tipoReceita;
            Titulo = titulo;
            Descricao = descricao;
            Tempo = tempo;
        }
        public Receita(string titulo, string descricao, DateTime tempo, TipoReceita tipoReceita)
        {
            TipoReceita = tipoReceita;
            Titulo = titulo;
            Descricao = descricao;
            Tempo = tempo;
        }
        public Receita(string titulo)
        {
            Titulo = titulo;
        }

        //Métodos De Acesso

        /// <summary>
        /// 
        /// </summary>
        public void InserirReceitas()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert receitas (id_tipo, titulo, descricao, tempo)" +
                " values (@tipo, @titulo, @descricao, @tempo)";
            cmd.Parameters.Add("@tipo", MySqlDbType.Int32).Value = TipoReceita.Id;
            cmd.Parameters.Add("@titulo", MySqlDbType.VarChar).Value = Titulo;
            cmd.Parameters.Add("@descricao",MySqlDbType.VarChar).Value = Descricao;
            cmd.Parameters.Add("@tempo",MySqlDbType.DateTime).Value = Tempo;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            ID = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

        public static Receita ObterPorId(int id)
        {
            Receita receita = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from receitas where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                receita = new Receita();
                receita.ID = dr.GetInt32(0);
                receita.TipoReceita = TipoReceita.ObterPorId(dr.GetInt32(1));
                receita.Titulo = dr.GetString(2);
                receita.Descricao = dr.GetString(3);
                receita.Tempo = dr.GetDateTime(4);
            }
            Banco.Fechar(cmd);
            return receita;
        }

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
                RC.Tempo = dr.GetDateTime(4);
                list.Add(RC);
            }
            Banco.Fechar(cmd);
            return list;
        }

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
                rec.Tempo = dr.GetDateTime(4);
                receitas.Add(rec);
            }
            Banco.Fechar(cmd);
            return receitas;
        }

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

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete receitas where id " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
