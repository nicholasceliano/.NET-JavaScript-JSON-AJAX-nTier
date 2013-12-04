using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Csla;
using System.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public abstract class BusinessObject<T> : Csla.BusinessBase<T>, IBusinessObject where T : BusinessObject<T>
    {

        public const string DateFormatString = "MM/dd/yyyy";
        public const string VolumeFormatString = "#,##0.00 ;(#,##0.00)";
        public const string VolumeFormatWithoutDecimalString = "#,##0 ;(#,##0)";

        #region Interface Methods

        private object SaveBusinessObject()
        {
            T businessObject = null;
            try
            {
                businessObject = this.Save();
            }
            catch (Exception ex)
            {
                Exception businessException = ex;
                while (businessException is Csla.DataPortalException)
                {
                    businessException = ((Csla.DataPortalException)businessException).BusinessException;
                }
                string msg = businessException.Message;
                if (msg.Contains("unique constraint") && msg.Contains("violated"))
                {
                    throw new Exception("Record with specified parameters already exists");
                }
                throw businessException;
            }
            return businessObject;
        }

        public virtual void CopyFromData(object data)
        {
            try
            {
                foreach (PropertyInfo originatingProperty in data.GetType().GetProperties())
                {
                    PropertyInfo destinationProperty = typeof(T).GetProperty(originatingProperty.Name);
                    if (destinationProperty == null)
                        continue;
                    if (destinationProperty.CanWrite)
                    {
                        destinationProperty.SetValue(this, originatingProperty.GetValue(data, null), null);
                    }
                    else if (originatingProperty.GetIndexParameters().Length == 0 && originatingProperty.GetValue(data, null) is IEnumerable && destinationProperty.GetValue(this, null) is IBusinessObjects)
                    {
                        foreach (object originatingItem in (IEnumerable)originatingProperty.GetValue(data, null))
                        {
                            ((IBusinessObject)((IBusinessObjects)destinationProperty.GetValue(this, null)).AddNew(true)).CopyFromData(originatingItem);
                        }
                    }
                    else if (destinationProperty.GetValue(this, null) is IBusinessObject && (originatingProperty.GetValue(data, null) != null))
                    {
                        ((IBusinessObject)destinationProperty.GetValue(this, null)).CopyFromData(originatingProperty.GetValue(data, null));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object GetBusinessObjectIdValue()
        {
            return this.GetIdValue();
        }

        #endregion

        public override T Save()
        {
            object oLocker = new Object();

            lock (oLocker)
            {
                //Always save even if dirty, let data access methods control the updates
                if (this.IsChild || EditLevel > 0 || !IsValid || IsDirty)
                    return base.Save();
                return (T)Csla.DataPortal.Update(this);
            }
        }

        #region Interface Methods

        object IBusinessObject.GetIdValue()
        {
            return GetBusinessObjectIdValue();
        }

        object IBusinessObject.Save()
        {
            return SaveBusinessObject();
        }

        protected virtual void FetchData(Csla.Data.SafeDataReader datareader)
        {
            this.MarkClean();
            this.MarkOld();
        }

        protected void AddUpdateSQL(string fieldName, object fieldValue, ref string fields)
        {
            this.AddUpdateSQL(fieldName, fieldValue, ref fields, true);
        }

        protected void AddUpdateSQL(string fieldName, object fieldValue, ref string fields, bool IsDBNull)
        {
            this.AddUpdateSQL(fieldName, fieldValue, ref fields, IsDBNull, "");
        }

        protected void AddUpdateSQL(string fieldName, object fieldValue, ref string fields, bool IsDBNull, string go)
        {
            if (string.IsNullOrEmpty(fields))
                fields = string.Empty;
            else
                fields += ",";
            fields += string.Format("{0}=", fieldName);
            if (fieldValue == null && IsDBNull)
            {
                fields += "NULL";
            }
            else if ((fieldValue is System.DateTime || fieldValue is DateTime))
            {
                fields += string.Format("'{0}'", Convert.ToDateTime(fieldValue).ToString());
            }
            else if (fieldValue is int || fieldValue is double || fieldValue is decimal)
            {
                fields += fieldValue.ToString();
            }
            else
            {
                fields += string.Format("'{0}'", fieldValue.ToString().Replace("'", "''"));
            }
        }
    
        protected void Update(Data.DataAccess dataAccess, string storedProcName, params object[] parameters)
        {
            IDataReader reader = dataAccess.ExecuteStoredProcedure(storedProcName, parameters);
            //not closing connection causes major issues when doing a lot of queries
            reader.Close();
        }

        protected void Update(Data.DataAccess dataAccess, string tableName, string fieldList, string whereClause)
        {
            this.Update(dataAccess, tableName, fieldList, whereClause, false);
        }

        protected void Update(Data.DataAccess dataAccess, string tableName, string fieldList, string whereClause, bool transaction)
        {
            dataAccess.ExecuteNonQuery(string.Format("{0} update {1} set {2} where {3} {4}", (transaction ? "begin tran" : string.Empty), tableName, fieldList, whereClause, (transaction ? "commit tran" : string.Empty)));
        }
        #endregion
    }

}