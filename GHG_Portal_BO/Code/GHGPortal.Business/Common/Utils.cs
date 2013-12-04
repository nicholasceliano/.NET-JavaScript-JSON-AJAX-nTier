using System;
using System.Data;
using Hess.Corporate.GHGPortal.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;

namespace Hess.Corporate.GHGPortal.Business.Common
{
    public class Utils
    {
        #region Public Constants

        public const string DATE_SQL_FORMAT = "yyyyMMdd";
        public const string ORACLE_DATETIME_FORMAT = "MM/dd/yyyy hh:mm:ss tt";

        #endregion

        #region Public Methods
              
        #endregion


    }

    public static class ExtensionMethods_Helper
    {
        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source);
        }

    }



    public class MoveListColumnOrders : List<MoveListColumnOrder>
    {
        public static void SerializeToXML(MoveListColumnOrders columnOrders, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MoveListColumnOrders));
            TextWriter textWriter = new StreamWriter(filePath);
            serializer.Serialize(textWriter, columnOrders);
            textWriter.Close();
        }

        public static MoveListColumnOrders DeserializeXML(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MoveListColumnOrders));
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            var xmlObject =  (MoveListColumnOrders)serializer.Deserialize(fileStream);
            fileStream.Close();
            return xmlObject;
        }
    }

    public class MoveListColumnOrder
    {
       
        protected int _Order = 999;
        protected string _FieldName = string.Empty;

        [XmlAttribute("FieldName")]
        public string FieldName
        {
            get { return this._FieldName; }
            set { this._FieldName = value; }
        }

        [XmlAttribute("Order")]
        public int Order
        {
            get { return this._Order; }
            set { this._Order = value; }
        }
    }

    public enum HtmlTag
    {
        Label,
        EditableLabel,
        DateLabel,
        EditableDateLabel,
        Other
    }
}
