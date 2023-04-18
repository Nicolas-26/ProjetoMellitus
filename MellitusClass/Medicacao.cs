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
        public void Inserir()
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "insert medicacoes (alarme, instrucao, remedio, id_user) values (@alarme, @instrucao, @remedio, @user)";
            cmd.Parameters.Add("@alarme", MySqlDbType.DateTime).Value = Alarme;
            cmd.Parameters.Add("@instrucao", MySqlDbType.Text).Value = Instrucao;
            cmd.Parameters.Add("@remedio", MySqlDbType.VarChar).Value = Remedio;
            cmd.Parameters.Add("@user", MySqlDbType.Int32).Value = Usuario.Id;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select @@identity";
            Id = Convert.ToInt32(cmd.ExecuteScalar());
            Banco.Fechar(cmd);
        }

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

        public void Excluir(int id)
        {
            var cmd = Banco.Abrir();
            cmd.CommandText = "delete medicacoes where id = " + id;
            cmd.ExecuteNonQuery();
            Banco.Fechar(cmd);
        }
    }
}
