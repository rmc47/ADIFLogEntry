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