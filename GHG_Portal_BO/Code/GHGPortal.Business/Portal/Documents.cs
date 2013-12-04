using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hess.Corporate.GHGPortal.Data;
using Hess.Corporate.GHGPortal.Configuration;

namespace  Hess.Corporate.GHGPortal.Business
{
    public class Documents:BusinessObjects<Documents, Document>
    {
        public Decimal MoveNumber { get; private set; }

        public Documents()
        {

        }

        public static Dictionary<String, int> GetDocumentCountForMoves(int[] moves, string type)
        {
            Dictionary<String, int> counts = new Dictionary<string, int>();
            StringBuilder queryBuilder = new StringBuilder("select cth.tempestdealid as moveNumber, count(docType.Description) as count from dbo.ConfirmTrackingDetail ctd, dbo.ConfirmTrackingHeader cth, dbo.PickLists docType where ctd.HeaderId = cth.Id and docType.Code = ctd.DocType and (ctd.Obsolete is null OR ctd.Obsolete <>1) and docType.DocGroup = 'BLADES Docs' and cth.TempestDealId in(");
            //queryBuilder.Append(String.Join(",", moves));
            foreach (int move in moves)
            {
                queryBuilder.AppendFormat("'{0}',", move);
            }
            //remove last comma
            queryBuilder.Remove(queryBuilder.Length - 1, 1);
            queryBuilder.Append(")");
            queryBuilder.Append(" and docType like '%" + type + "%'");
            queryBuilder.Append("group by cth.tempestdealid;");
            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccess(Configuration.SystemType.DocManager).ExecuteQuery(queryBuilder.ToString())))
            {
                while (dataReader.Read())
                {
                    counts.Add(dataReader.GetString("moveNumber"), dataReader.GetInt32("count"));
                }
            }

            return counts;
        }

        public static Documents GetDocumentsForMove(Decimal move)
        {
            return DataPortal.Fetch<Documents>(move);
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            Decimal moveNumber = (Decimal)criteria;
            StringBuilder queryBuilder = new StringBuilder("select ");
            queryBuilder.Append(" ctd.Id as Id,");
            queryBuilder.Append(" ctd.HeaderId as HeaderId,");
            queryBuilder.Append(" DocType,");
            queryBuilder.Append(" Source,");
            queryBuilder.Append(" Status,");
            queryBuilder.Append(" Reason,");
            queryBuilder.Append(" Notes,");
            queryBuilder.Append(" ctd.CreatedOn as CreatedOn,");
            queryBuilder.Append(" ctd.CreatedBy as CreatedBy,");
            queryBuilder.Append(" ConfirmFileName,");
            queryBuilder.Append(" LastUploadAttempt,");
            queryBuilder.Append(" Obsolete,");
            queryBuilder.Append(" AgreementDate");
            queryBuilder.Append(" from dbo.ConfirmTrackingDetail ctd, dbo.ConfirmTrackingHeader cth, dbo.PickLists docType where ctd.HeaderId = cth.Id and docType.Code = ctd.DocType and (ctd.Obsolete is null OR ctd.Obsolete <>1) and docType.DocGroup = 'BLADES Docs' and cth.TempestDealId  =");
            queryBuilder.AppendFormat("'{0}'", moveNumber);

            using (Csla.Data.SafeDataReader reader = new Csla.Data.SafeDataReader(new DataAccess(SystemType.DocManager).ExecuteQuery(queryBuilder.ToString())))
            {
                while (reader.Read())
                {
                    this.Add(new Document(reader));
                }
            }

            this.MoveNumber = moveNumber;
        }
    }
}