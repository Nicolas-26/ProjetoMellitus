using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class Exercicio
    {
        //propriedades
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Tempo { get; set; }
        public TipoExercicio TipoExercicio { get; set; }

        //Métodos Construtores
        public Exercicio() { }
        public Exercicio(int id, string titulo, string descricao, DateTime tempo, TipoExercicio tipoExercicio)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Tempo = tempo;
            TipoExercicio = tipoExercicio;
        }
        public Exercicio(string titulo, string descricao, DateTime tempo, TipoExercicio tipoExercicio)
        {
            Titulo = titulo;
            Descricao = descricao;
            Tempo = tempo;
            TipoExercicio = tipoExercicio;
        }


        //Métodos De Acesso

        /// <summary>
        /// 
        /// </summary>
        public void InserirExercicios()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insere_exercicio";
            cmd.Parameters.AddWithValue("_id", 0).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("_titulo", Titulo).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_descricao", Descricao).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_tempo", Tempo).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_id_tipo", TipoExercicio.Id).Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
            Id = Convert.ToInt32(cmd.Parameters["_id"].Value);
            Banco.Fechar(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Exercicio ObterPorId(int id)
        {
            Exercicio exer = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from exercicios where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                exer = new Exercicio(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2),
                    dr.GetDateTime(3),
                    TipoExercicio.ObterPorId(dr.GetInt32(4))
                    );
            }
            Banco.Fechar(cmd);
            return exer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static List<Exercicio> ObterPorTipo(int id_tipo)
        {
            List<Exercicio> lista = new List<Exercicio>();
            Exercicio td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from exercicios where id_tipo = " + id_tipo;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                td = new Exercicio();
                td.Id = dr.GetInt32(0);
                td.Titulo = dr.GetString(1);
                td.Descricao = dr.GetString(2);
                td.Tempo = dr.GetDateTime(3);
                td.TipoExercicio = TipoExercicio.ObterPorId(dr.GetInt32(4));
                lista.Add(td);
            }
            Banco.Fechar(cmd);
            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public static List<Exercicio> ListarExercicios(string titulo = "")
        {
            List<Exercicio> list = new List<Exercicio>();
            Exercicio exer = null;
            var cmd = Banco.Abrir();
            if(titulo!=string.Empty)
            {
                cmd.CommandText = "select * from exercicios where titulo like '%" + titulo + "%'";
            }
            else
            {
                cmd.CommandText = "select * from exercicios";
            }
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                exer = new Exercicio();
                exer.Id = dr.GetInt32(0);
                exer.Titulo = dr.GetString(1);
                exer.Descricao = dr.GetString(2);
                exer.Tempo = dr.GetDateTime(3);
                exer.TipoExercicio = TipoExercicio.ObterPorId(dr.GetInt32(4));
                list.Add(exer);
            }
            Banco.Fechar(cmd);
            return list;
        }

        public void Atualizar(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "update exercicios set titulo = @titulo, descricao = @descricao, tempo = @tempo where id = " + id;
            cmd.Parameters.Add("@titulo", MySqlDbType.VarChar).Value = Titulo;
            cmd.Parameters.Add("@descricao",MySqlDbType.VarChar).Value = Descricao;
            cmd.Parameters.Add("@tempo", MySqlDbType.DateTime).Value = Tempo;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete exercicios where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
