using ADIFLogEntry.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADIFLogEntry.Engine
{
    internal class DataStore
    {
        MySqlConnection m_Connection;
        RegistrySettings.Settings m_Settings = new RegistrySettings.Settings();

        public DataStore()
        {
            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
            m_Settings["Hello"] = "World"; // Just so we can find the SID in RegEdit :-)
            csb.Server = m_Settings["Server"];
            csb.UserID = m_Settings["Username"];
            csb.Password = m_Settings["Password"];
            csb.Database = m_Settings["Database"];

            m_Connection = new MySqlConnection(csb.ConnectionString);
            m_Connection.Open();
        }

        public LogsModel LoadLogs(int userID)
        {
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM logs WHERE userID = ?userID ORDER BY created DESC;";
                cmd.Parameters.AddWithValue("?userID", userID);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    LogsModel logs = new LogsModel();
                    logs.Logs = new List<LogModel>();
                    while (reader.Read())
                    {
                        logs.Logs.Add(LoadLog(reader));
                    }
                    return logs;
                }
            }
        }

        private LogModel LoadLog(MySqlDataReader reader)
        {
            LogModel log = new LogModel();
            log.ID = reader.GetInt32("id");
            log.Name = reader.GetString("name");
            log.UserID = reader.GetInt32("userId");
            log.Locator = reader.GetString("locator");
            log.WabSquare = reader.GetString("wabSquare");
            return log;
        }

        public void CreateLog(LogModel model)
        {
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO logs (name, userId, locator, wabSquare) VALUES (?name, ?userId, ?locator, ?wabSquare);";
                cmd.Parameters.AddWithValue("?name", model.Name);
                cmd.Parameters.AddWithValue("?userId", model.UserID);
                cmd.Parameters.AddWithValue("?locator", model.Locator);
                cmd.Parameters.AddWithValue("?wabSquare", model.WabSquare);
                cmd.ExecuteNonQuery();
                model.ID = (int)cmd.LastInsertedId;
            }
        }

        public void LogQso(QsoModel qso)
        {
            using (MySqlCommand cmd = m_Connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO qsos (session, date, band, mode, callsign, rsttx, rstrx) VALUES (?session, ?date, ?band, ?mode, ?callsign, ?rsttx, ?rstrx);";
                cmd.Parameters.AddWithValue("?session", 0);
                cmd.Parameters.AddWithValue("?date", DateTime.Parse(qso.Date));
                cmd.Parameters.AddWithValue("?band", qso.Band);
                cmd.Parameters.AddWithValue("?mode", qso.Mode);
                cmd.Parameters.AddWithValue("?callsign", qso.Callsign);
                cmd.Parameters.AddWithValue("?rsttx", qso.RstTx);
                cmd.Parameters.AddWithValue("?rstrx", qso.RstRx);
                cmd.ExecuteNonQuery();
            }
        }
    }
}