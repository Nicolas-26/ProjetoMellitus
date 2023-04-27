using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class Glicemia
    {
        //atributos
        private int id;
        private int valor;
        private DateTime data;


        //propriedades
        public int ID { get { return id; } set {  id = value; } }
        public int Valor { get {  return valor; } set {  valor = value; } }
        public DateTime Data { get { return data; } set { data = value; } }
        public Usuario Usuario { get; set; }


        //Métodos Construtores
        public Glicemia() { }
        public Glicemia(int id, int valor, DateTime data, Usuario usuario)
        {
            ID = id;
            Valor = valor;
            Data = data;
            Usuario = usuario;
        }
        public Glicemia(int valor, DateTime data, Usuario usuario)
        {
            Valor = valor;
            Data = data;
            Usuario = usuario;
        }


        //Métodos De Acesso

        /// <summary>
        /// Inserir Campos(id, valor da glicemia, data da glicemia e o id do usuário para saber qual usuário está com glicemia)
        /// na tabela glicemia do banco.
        /// </summary>
        public void Inserir()
        {
            var cmd = Banco.Abrir();    
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insere_glicemia";
            cmd.Parameters.AddWithValue("_id", 0).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("_valor", Valor).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_data", Data).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_id_user", Usuario.Id).Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
            ID = Convert.ToInt32(cmd.Parameters["_id"].Value);
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para obter por id a glicemia.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Glicemia ObterPorId(int id)
        {
            Glicemia gli = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from glicemia where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                gli = new Glicemia(
                    dr.GetInt32(0),
                    dr.GetInt32(1),
                    dr.GetDateTime(2),
                    Usuario.ObterPorId(dr.GetInt32(3))
                    );
            }
            Banco.Fechar(cmd);
            return gli;
        }


        /// <summary>
        /// Método para retornar data e o valor da glicemia ao inserir o id do usuario
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public static List<Glicemia> ObterGlicemiaPorUsuario(int id_user)
        {
            List<Glicemia> list = new List<Glicemia>();
            Glicemia glicemia = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select data, valor from glicemia where id_user = " + id_user;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                glicemia = new Glicemia();
                glicemia.ID = dr.GetInt32(0);
                glicemia.Valor = dr.GetInt32(1);
                glicemia.Data = dr.GetDateTime(2);
                glicemia.Usuario = Usuario.ObterPorId(dr.GetInt32(3));
                list.Add(glicemia);
            }
            Banco.Fechar(cmd);
            return list;
        }


        /// <summary>
        /// Método para listar todos elementos da tabela glicemia do banco e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<Glicemia> Listar()
        {
            List<Glicemia> gli = new List<Glicemia>();
            Glicemia td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from glicemia";
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                td = new Glicemia();
                td.ID = dr.GetInt32(0);
                td.Valor = dr.GetInt32(1);
                td.Data = dr.GetDateTime(2);
                td.Usuario = Usuario.ObterPorId(dr.GetInt32(3));
                gli.Add(td);
            }
            Banco.Fechar(cmd);
            return gli;
        }


        /// <summary>
        /// Método para atualizar campos (valor e data) da tabela glicemia do banco onde o id é especifícado.
        /// </summary>
        /// <param name="id"></param>
        public void Atualizar(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "update glicemia set valor = @valor, data = @data where id = " + id;
            cmd.Parameters.Add("@valor", MySqlDbType.Int32).Value = Valor;
            cmd.Parameters.Add("@data",MySqlDbType.Date).Value = Data;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }

        /// <summary>
        /// Método Para excluir um campo inteiro da tabela glicemia do banco ao ser inserido o id.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from glicemia where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
