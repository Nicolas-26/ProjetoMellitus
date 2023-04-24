using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class TipoExercicio
    {
        //atributos
        private int id;
        private string tipo;


        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public string Tipo { get {  return tipo; } set {  tipo = value; } }


        //Métodos Construtores
        public TipoExercicio() { }
        public TipoExercicio(int id, string tipo)
        {
            Id = id;
            Tipo = tipo;
        }
        public TipoExercicio(string tipo)
        {
            Tipo = tipo;
        }


        //Métodos De Acesso

        /// <summary>
        /// Método para inserir campos (id e o tipo de exercicio) na tabela tipo_exercicio do banco.
        /// </summary>
        public void InserirTipoExercicio()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert tipo_exercicio (tipo) values (@tipo)";
            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = Tipo;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método que traz todos os campos da tabela tipo_exercicio onde o id é especifícado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TipoExercicio ObterPorId(int id)
        {
            TipoExercicio tipo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from tipo_exercicio where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                tipo = new TipoExercicio(
                    dr.GetInt32(0),
                    dr.GetString(1)
                    );
            }
            Banco.Fechar(cmd);
            return tipo;
        }


        /// <summary>
        /// Método que lista todos elementos da tabela tipo_exercicio e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<TipoExercicio> Listar()
        {
            List<TipoExercicio> list = new List<TipoExercicio>();
            TipoExercicio td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from tipo_pedido";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                td = new TipoExercicio();
                td.Id = dr.GetInt32(0);
                td.Tipo = dr.GetString(1);
                list.Add(td);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Método que exclui um campo inteiro na tabela tipo_exercicio ao especificar o id certo.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from tipo_exercicio where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
