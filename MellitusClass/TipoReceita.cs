using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class TipoReceita
    {
        //atributos
        private int id;
        private string tipo;

        //propriedades
        public int Id { get { return id; } set { id = value; } }
        public string Tipo { get { return tipo;} set { tipo = value; } }

        //Métodos Construtores
        public TipoReceita() { }
        public TipoReceita(int id, string tipo)
        {
            Id = id;
            Tipo = tipo;
        }
        public TipoReceita(string tipo)
        {
            Tipo = tipo;
        }


        //Métodos De Acesso
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert tipo_receita (tipo) values (@tipo)";
            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = Tipo;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

        public static TipoReceita ObterPorId(int id)
        {
            TipoReceita tipo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from tipo_receita where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                tipo = new TipoReceita(
                    dr.GetInt32(0),
                    dr.GetString(1)
                    );
            }
            Banco.Fechar(cmd);
            return tipo;
        }

        public static TipoReceita ListarTipos()
        {
            TipoReceita list = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select tipo from tipo_receita";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                list = new TipoReceita(
                    dr.GetString(1)
                    );
            }
            Banco.Fechar(cmd);
            return list;
        }

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete tipo_receita where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
