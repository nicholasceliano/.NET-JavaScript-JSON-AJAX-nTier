using System;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Runtime.Serialization;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
	[DataContract()]
	public abstract class BusinessBase<T> : Csla.BusinessBase<T>, IBusinessObject where T : BusinessBase<T>
	{
		#region Protected Methods

		#region Validation Methods

		protected virtual bool ValidateRequiredFields()
		{
			foreach (PropertyInfo propertyInfo in this.GetType().GetProperties().Where(p => !p.PropertyType.IsEnum))
				foreach (DataFieldAttribute attribute in Attribute.GetCustomAttributes(propertyInfo, true).Where(p => p is DataFieldAttribute && ((DataFieldAttribute)p).Required))
					if (propertyInfo.GetValue(this, null) == null) throw new Csla.Validation.ValidationException(string.Format("{0} is required.", attribute.DisplayName));
			return true;
		}

		protected virtual bool ValidateBoundaries()
		{
			return true;
		}

		protected virtual bool ValidateBusinessRules()
		{
			return true;
		}

		#endregion

		#region Data Access Methods

		protected object ValueOfNullable<C>(Nullable<C> obj) where C : struct
		{
			if (obj.HasValue) return obj.Value; else return null;
		}

		protected Nullable<C> ConvertToNullable<C>(object obj) where C : struct
		{
			if (obj == null) return null; else return (Nullable<C>)obj;
		}

		protected string GetSQLValue(int? value)
		{
			return !value.HasValue ? "null" : Convert.ToString(value.Value);
		}

		protected string GetSQLValue(decimal? value)
		{
			return !value.HasValue ? "null" : Convert.ToString(value.Value);
		}

		protected string GetSQLValue(double? value)
		{
			return !value.HasValue ? "null" : Convert.ToString(value.Value);
		}

		protected string GetSQLValue(string value)
		{
			return string.IsNullOrEmpty(value) ? "null" : string.Format("'{0}'", value.Replace("'", "''"));
		}

		protected string GetSQLValue(DateTime? value, string format)
		{
			return !value.HasValue ? "null" : string.Format("TO_DATE('{0}','{1}')", value.Value,format);
		}

		protected string GetSQLValue<C>(Nullable<C> value) where C : struct
		{
			return !value.HasValue ? "null" : Convert.ToString(value.Value);
		}

		protected void AddInsertSQL(string fieldName, object fieldValue, ref string fields, ref string values)
		{
			AddInsertSQL(fieldName, fieldValue, ref fields, ref values, false);
		}

		protected void AddInsertSQL(string fieldName, object fieldValue, ref string fields, ref string values, bool forceInsert)
		{
			if (fieldValue == null && !forceInsert) return;
			if (!string.IsNullOrEmpty(fields)) fields += ","; fields += fieldName;
			if (!string.IsNullOrEmpty(values)) values += ",";
			if (fieldValue is DateTime)
				values += this.GetSQLValue(this.ConvertToNullable<DateTime>(fieldValue), Common.Utils.ORACLE_DATETIME_FORMAT);
			else if (fieldValue is int || fieldValue is double || fieldValue is decimal)
				values += fieldValue.ToString();
			else
				values += this.GetSQLValue(fieldValue.ToString());
		}

		protected void AddUpdateSQL(string fieldName, object fieldValue, ref string fields)
		{
			this.AddUpdateSQL(fieldName, fieldValue, ref fields, true);
		}

		protected void AddUpdateSQL(string fieldName, object fieldValue, ref string fields, bool IsDBNull)
		{
			if (!string.IsNullOrEmpty(fields)) fields += ",";
			fields += string.Format("{0}=", fieldName);
			if (fieldValue == null && IsDBNull)
				fields += "NULL";
			else if (fieldValue is System.DateTime)
				fields += this.GetSQLValue(this.ConvertToNullable<DateTime>(fieldValue), Common.Utils.ORACLE_DATETIME_FORMAT);
			else if (fieldValue is int || fieldValue is double || fieldValue is decimal)
				fields += fieldValue.ToString();
			else
				fields += this.GetSQLValue(fieldValue.ToString());
		}

		protected void Insert(DataAccessBase dataAccess, string tableName, string fields, string values)
		{
			this.Insert(dataAccess, tableName, fields, values, false);
		}

		protected void Insert(DataAccessBase dataAccess, string tableName, string fields, string values, bool transaction)
		{
			dataAccess.ExecuteNonQuery(string.Format("{0} insert into {1} ({2}) values ({3}) {4}", (transaction ? "begin tran" : string.Empty), tableName, fields, values, (transaction ? "commit tran" : string.Empty)));
		}

		protected void Update(DataAccessBase dataAccess, string tableName, string fields, string whereClause)
		{
			this.Update(dataAccess, tableName, fields, whereClause, false);
		}

		protected void Update(DataAccessBase dataAccess, string tableName, string fields, string whereClause, bool transaction)
		{
			dataAccess.ExecuteNonQuery(string.Format("{0} update {1} set {2} where {3} {4}", (transaction ? "begin tran" : string.Empty), tableName, fields, whereClause, (transaction ? "commit tran" : string.Empty)));
		}

		protected void Delete(DataAccessBase dataAccess, string tableName, string whereClause)
		{
			this.Delete(dataAccess, tableName, whereClause, false);
		}

		protected void Delete(DataAccessBase dataAccess, string tableName, string whereClause, bool transaction)
		{
			dataAccess.ExecuteNonQuery(string.Format("{0} delete {1} where {2} {3}", (transaction ? "begin tran" : string.Empty), tableName, whereClause, (transaction ? "commit tran" : string.Empty)));
		}

		protected void AddUpdateSQLGeneric(ref string fields)
		{
			// iterate through each savable field and call update
			var SavableProp = from prop in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
							  where ((DataFieldAttribute)prop.GetCustomAttributes(typeof(DataFieldAttribute), true)[0]).Savable
							  select prop;
			foreach (PropertyInfo propertyInfo in SavableProp)
			{
				DataFieldAttribute dataAttribute = (DataFieldAttribute)propertyInfo.GetCustomAttributes(typeof(DataFieldAttribute), true)[0];
				AddUpdateSQL(dataAttribute.FieldName, propertyInfo.GetValue(this, null), ref fields);
			}
		}

	   
		#endregion

		#endregion

		#region Methods Overrides

		private Object GetBusinessObjectIdValue()
		{
			return this.GetIdValue();
		}
		Object IBusinessObject.GetIdValue()
		{
			return GetBusinessObjectIdValue();
		}

		protected override object GetIdValue()
		{
			string key = string.Empty;
			this.GetType().GetProperties().Where(p => !p.PropertyType.IsEnum && p.GetCustomAttributes(typeof(DataFieldAttribute), true).FirstOrDefault(
				a => ((DataFieldAttribute)a).Indexed && ((DataFieldAttribute)a).IsPrimary) != null).ToList().ForEach(p => key += p.GetValue(this, null).ToString());
			if (string.IsNullOrEmpty(key)) return Guid.NewGuid().ToString(); else return key;
		}

		public override bool IsValid
		{
			get { return this.ValidateRequiredFields() && this.ValidateBoundaries() && this.ValidateBusinessRules(); }
		}

		public override T Save()
		{
			// Always save even if dirty, let data access methods control the updates
			if (this.IsChild || this.EditLevel > 0 || !this.IsValid || this.IsDirty) return base.Save();
			return (T)Csla.DataPortal.Update(this);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Local cache of property values populated upon initializing of the business object
		/// </summary>
		protected Dictionary<string, object> StoredValues = new Dictionary<string, object>();

		public virtual void SetValue(PropertyInfo property, object value) { this.SetValue(property, value, false); }        

		public virtual void SetValue(PropertyInfo property, object value, bool stored)
		{
			if (property == null || !property.CanWrite) return;
			if (value != null && value.GetType() == typeof(string))
				value = ((string)value).Trim();
			else if (property.PropertyType.IsSubclassOfGeneric(typeof(Nullable<>)))
				value = Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType));
			if (value != null) property.SetValue(this, value, null);
			if (stored) this.StoredValues[property.Name] = value;
		}

		public void MarkOldAndClean()
		{
			this.MarkClean(); base.MarkOld();
		}

		public void CopyFromData(object o)
		{
			throw new NotImplementedException();
		}
		object IBusinessObject.Save()
		{
			return SaveBusinessObject();
		}

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
		#endregion
	}
}
