using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MellitusClass
{
    public class Medicacao
    {
        //atributos
        private int id;
        private DateTime alarme;
        private string instrucao;
        private string remedio;


        //propriedades
        public int Id { get { return id; } set {  id = value; } }
        public DateTime Alarme { get {  return alarme; } set {  alarme = value; } }
        public string Instrucao { get {  return instrucao; } set {  instrucao = value; } }
        public string Remedio { get {  return remedio; } set {  remedio = value; } }
        public Usuario Usuario { get; set; }


        //Métodos Construtores
        public Medicacao() { }
        public Medicacao(int id, DateTime alarme, string instrucao, string remedio, Usuario usuario)
        {
            Id = id;
            Alarme = alarme;
            Instrucao = instrucao;
            Remedio = remedio;
            Usuario = usuario;
        }
        public Medicacao(DateTime alarme, string instrucao, string remedio, Usuario usuario)
        {
            Alarme = alarme;
            Instrucao = instrucao;
            Remedio = remedio;
            Usuario = usuario;
        }


        //Métodos De Acesso

        /// <summary>
        /// Método Para inserir campos (id, alarme da medicação, instrução da medicação, remedio para o usuario tomar,
        /// e o id do usuário) na tabela medicacoes do banco.
        /// </summary>
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_insere_medicacoes";
            cmd.Parameters.AddWithValue("_id", 0).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("_alarme", Alarme).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_instrucao", Instrucao).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_remedio", Remedio).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("_id_user", Usuario.Id).Direction = ParameterDirection.Input;
            cmd.ExecuteNonQuery();
            Id = Convert.ToInt32(cmd.Parameters["_id"].Value);
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para trazer todos os campos da tabela medicacoes do banco ao inserir o id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Medicacao ObterPorId(int id)
        {
            Medicacao med = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from medicacoes where id = " + id;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                med = new Medicacao(
                    dr.GetInt32(0),
                    dr.GetDateTime(1),
                    dr.GetString(2),
                    dr.GetString(3),
                    Usuario.ObterPorId(dr.GetInt32(4))
                    );
            }
            Banco.Fechar(cmd);
            return med;
        }


        /// <summary>
        /// Método para listar todos campos da tabela medicacoes onde o id do usuário for especifícado(para saber qual as medicações
        /// certas do usuário especifícado).
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public static List<Medicacao> ListarMedicacaoPorUser(int id_user)
        {
            List<Medicacao> med = new List<Medicacao>();
            Medicacao td = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from medicacoes where id_user = " + id_user;
            var dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                td = new Medicacao();
                td.Id = dr.GetInt32(0);
                td.Alarme = dr.GetDateTime(1);
                td.Instrucao = dr.GetString(2);
                td.Remedio = dr.GetString(3);
                td.Usuario = Usuario.ObterPorId(dr.GetInt32(4));
                med.Add(td);
            }
            Banco.Fechar(cmd);
            return med;
        }


        /// <summary>
        /// Método que lista todos elementos da tabela medicacoes do banco e retorna todos para o adm.
        /// </summary>
        /// <returns></returns>
        public static List<Medicacao> Listar()
        {
            List<Medicacao> medicacao = new List<Medicacao>();
            Medicacao tudo = null;
            var cmd = Banco.Abrir();
            cmd.CommandText = "select * from medicacoes";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tudo = new Medicacao();
                tudo.Id = dr.GetInt32(0);
                tudo.Alarme = dr.GetDateTime(1);
                tudo.Instrucao = dr.GetString(2);
                tudo.Remedio = dr.GetString(3);
                tudo.Usuario = Usuario.ObterPorId(dr.GetInt32(4));
                medicacao.Add(tudo);
            }
            Banco.Fechar(cmd);
            return medicacao;
        }


        /// <summary>
        /// Método para atualizar campos (alarme, instrução, remédio) da tabela medicacoes do banco ao inserir o id certo.
        /// </summary>
        /// <param name="id"></param>
        public void Alterar(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "update medicacoes set alarme = @alarme, instrucao = @instrucao, remedio = @remedio where id = " + id;
            cmd.Parameters.Add("@alarme", MySqlDbType.DateTime).Value = Alarme;
            cmd.Parameters.Add("@instrucao", MySqlDbType.Text).Value = Instrucao;
            cmd.Parameters.Add("remedio", MySqlDbType.VarChar).Value = Remedio;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }


        /// <summary>
        /// Método para excluir um campo inteiro da tabela medicacoes do banco.
        /// </summary>
        /// <param name="id"></param>
        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete from medicacoes where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
