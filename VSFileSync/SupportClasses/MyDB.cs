using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Windows;
using System.IO;

namespace VSFileSync.SupportClasses
{
    class MyDB
    {
        private SqlCeConnection con;
        private SqlCeCommand cmd;
        //private SqlCeResultSet rs;
        //private StreamWriter swTrans;
        //private string sTransDir = System.Environment.CurrentDirectory + "\\transactions";
        //private string sTransFile;
        private string sAddinDir = System.Reflection.Assembly.GetExecutingAssembly().Location;

        #region Constructors

        public MyDB()
        {
            // leaves the trailing \ on the path..
            sAddinDir = Path.GetDirectoryName(sAddinDir);

            try
            {
                con = new SqlCeConnection(@"Data Source=" + sAddinDir + @"\SolutionFileSyncAddinDataStore.sdf");
                //#if DEBUG
                //                con = new SqlCeConnection(@"Data Source=E:\GitHub\VSFileSync\VSFileSyncAddin\SolutionFileSyncAddinDataStore.sdf");
                //#else
                //                con = new SqlCeConnection(@"Data Source=" + sAddinDir + @"\SolutionFileSyncAddinDataStore.sdf");
                //#endif
                con.Open();

                cmd = new SqlCeCommand();
                cmd.Connection = con;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect to the database, can't continue..: " + ex.Message);
            }

        }

        #endregion

        #region Wrapper Functions

        internal void getTrackingState(string SolutionName, string ProjectName, ref string LocalStoredPath, ref string RemoteStoredPath, ref bool bOverride)
        {
            try
            {
                SqlCeResultSet rsResult = null;

                cmd.CommandText = "select LocalStoredPath, RemoteStoredPath, ManualNotTracked from [Projects] where [SolutionName] = " +
                    SolutionName + " and [ProjectName] = " + ProjectName;

                rsResult = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                if (rsResult == null)
                {
                    MessageBox.Show("Failed to execute command to get tracking info?");
                    return;
                }

                if(rsResult.RecordsAffected != 1)
                {
                    MessageBox.Show("Seems we have too many rows in our tracker for this Solution..");
                    return;
                }

                rsResult.ReadFirst();

                LocalStoredPath = (string)rsResult.GetString(0);
                RemoteStoredPath = (string)rsResult.GetString(1);
                bOverride = (bool)rsResult.GetBoolean(2);


            }
            catch (Exception ex)
            {

            }

        }

        #endregion

        public SqlCeResultSet ResultSet(string sCommand)
        {
            cmd.CommandText = sCommand;
            return cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
        }

        public bool ProjectTrackedForThisSolution(string sSolutionName, string sProjectName)
        {
            cmd.CommandText = "select count(*) from Projects where SolutionName = " + sSolutionName + " and ProjectName = " + sProjectName;
            return ((int)cmd.ExecuteScalar() > 0);
        }

        public int iNumValReturn(string sCommand)
        {
            cmd.CommandText = sCommand;
            return (int)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Executes the command passed in, be it insert, update, delete, etc.
        /// </summary>
        /// <param name="sCommand">Command to execute on the database</param>
        /// <param name="bDumpToTrans">Optional parameter telling us if we should create a transaction log entry
        /// for this command.. we do NOT want to log things like inserts to the baseline tables during refresh, or
        /// updates when we shift parsing to baseline.</param>
        public void Insert(string sCommand)
        {
            cmd.CommandText = sCommand;
            cmd.ExecuteNonQuery();

        }


        public void CloseConnections()
        {
            if (con != null)
                con.Close();
            //if ( swTrans != null )
            //    swTrans.Close();
        }

    }

}
