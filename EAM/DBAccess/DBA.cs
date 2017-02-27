using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using EAManager.Models;
using Common;
using MySql.Data.MySqlClient;

namespace EAManager.DBAccess
{
    public class DBA
    {
        static DBA()
        {
            MySqlHelper.connectionStringManager = ConfigurationManager.AppSettings["SqlConnectionString"];
        }

        public static readonly DateTime InitDateTime = new DateTime(2000, 1, 1);

        public static List<string> GetAllSymbol()
        {
            List<string> symbols = new List<string>();
            string query = string.Format("select id from tb_info where type = 3 order by responseTime desc");
            MySqlDataReader reader = null;
            try
            {
                reader = MySqlHelper.ExecuteReader(MySqlHelper.ConnectionStringManager, CommandType.Text, query);
            }
            catch (Exception ex) { }

            if (reader != null)
            {
                while (reader.Read())
                {
                    symbols.Add(reader.GetString(0));
                }
                reader.Close();
            }
            return symbols;
        }

        public static bool InsertSignal(SignalItem signal)
        {
            bool success = true;
            string query = string.Format("insert into tb_signal values(null,'{0}',{1},'{2}',{3},{4},'{5}',{6},{7},{8},{9},{10},'{11}',{12},'{13}')",
                signal.Symbol, signal.SignalType, signal.Source, signal.State, signal.TradeType, signal.Expires.ToGeneralTimeString(),
                signal.ExpectPrice, signal.TpPoints, signal.SlPoints, signal.DistancePoints, signal.WinningRate,
                signal.OutbreakTime.ToGeneralTimeString(), signal.Timeout, signal.CreateTime.ToGeneralTimeString());
            try
            {
                MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionStringManager, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        public static EAInfo GetEAInfo(string id)
        {
            EAInfo info = null;
            string query = string.Format("select id,type,requestState,requestExpires,responseValue,responseTime from tb_info where id = '{0}'", id);
            MySqlDataReader reader = null;
            try
            {
                reader = MySqlHelper.ExecuteReader(MySqlHelper.ConnectionStringManager, CommandType.Text, query);
            }
            catch (Exception ex) { }

            if (reader != null)
            {
                info = new EAInfo();
                if (reader.Read())
                {
                    info.Id = reader.GetString(0);
                    info.Type = reader.GetInt32(1);
                    info.RequestState = reader.GetInt32(2);
                    info.RequestExpires = reader.GetDateTime(3);
                    info.ResponseValue = reader.GetString(4);
                    info.ResponseTime = reader.GetDateTime(5);
                }
                reader.Close();
            }
            return info;
        }

        public static void RequestEAInfo(string id, DateTime requestExpires)
        {
            string query = string.Format("update tb_info set requestState = 1,requestExpires = '{0}' where id = '{1}'", requestExpires.ToGeneralTimeString(), id);
            try
            {
                MySqlHelper.ExecuteNonQuery(MySqlHelper.ConnectionStringManager, CommandType.Text, query);
            }
            catch (Exception ex) { }
        }
    }
}