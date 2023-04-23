using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MellitusClass
{
    public class Banco
    {
        private static string strConn;

        public static MySqlCommand Abrir()
        {
            strConn = @"server=localhost;database=dbmellitus;port=3306;user id=root;password=senacitaquera";
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                MySqlConnection cn = new MySqlConnection(strConn);
                if(cn.State!=ConnectionState.Open)
                {
                    cn.Open();
                }
                cmd.Connection = cn;
            }
            catch(Exception)
            {
                throw;
            }
            return cmd;
        }

        public static void Fechar(MySqlCommand cmd)
        {
            if(cmd.Connection.State==ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
        }
    }
}
