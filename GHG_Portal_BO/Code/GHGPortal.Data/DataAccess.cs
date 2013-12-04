using System.Data;
using System.Data.Common;
using Hess.Corporate.GHGPortal.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Hess.Corporate.GHGPortal.Data
{

    public class DataAccess
    {
        #region Private members

        protected Database _Database;
        protected int _ExecutionTimeout;
        protected static object _Locker = new object();

        protected Configuration.SystemType SystemType { get; set; }

        #endregion

        #region Constructors

        /// <summary>This is the default constructor and retrieves a connection string from the configuration file.</summary>
        public DataAccess():this(SystemType.GHGPortal)
        {
           
        }

        public DataAccess(SystemType type)
        {
            SiteConfigurationElement connectionString = AppConfiguration.Current.GetConnectionString(type);
            if (connectionString == null) throw new System.Exception(string.Format("No connection string found for {0}", this.SystemType));

            this._Database = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString.Value);
            //this._Database = DatabaseFactory.CreateDatabase(connectionString.Value);
            this._ExecutionTimeout = connectionString.ExecutionTimeout;
            this.SystemType = type;
        }

        #endregion


        #region Public Properties

        public static object Locker
        {
            get { return DataAccess._Locker; }
        }

        #endregion

        #region Data Access Methods

        public virtual IDataReader ExecuteQuery(string query)
        {
            lock (DataAccess._Locker)
            {
                DbCommand dbCommand = this._Database.GetSqlStringCommand(query);

                if (this._ExecutionTimeout != 0)
                    dbCommand.CommandTimeout = this._ExecutionTimeout;
                return this._Database.ExecuteReader(dbCommand);
            }
        }

        public virtual IDataReader ExecuteStoredProcedure(string query, params object[] parameters)
        {
            lock (DataAccess._Locker)
            {
                DbCommand dbCommand;
                if (parameters != null && parameters.Length > 0)
                {
                    dbCommand = this._Database.GetStoredProcCommand(query, parameters);
                }
                else
                    dbCommand = this._Database.GetStoredProcCommand(query);

                if (this._ExecutionTimeout != 0)
                    dbCommand.CommandTimeout = this._ExecutionTimeout;
                IDataReader datReader = this._Database.ExecuteReader(dbCommand);
                return datReader;
            }
        }

        public virtual void ExecuteNonQuery(string query)
        {
            lock (DataAccess._Locker)
            {
                DbCommand dbCommand = this._Database.GetSqlStringCommand(query);
                if (this._ExecutionTimeout != 0)
                    dbCommand.CommandTimeout = this._ExecutionTimeout;
                this._Database.ExecuteNonQuery(dbCommand);
            }
        }

        #endregion
    }
}
