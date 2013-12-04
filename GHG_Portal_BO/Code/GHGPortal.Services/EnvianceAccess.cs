using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Web;

using Hess.Corporate.GHGPortal.Configuration;
using Hess.Corporate.GHGPortal.Business;
using Hess.Corporate.GHGPortal.Services.EnvianceAuthenticationService;
using Hess.Corporate.GHGPortal.Services.EnvianceDataSubmissionService;
using Hess.Corporate.GHGPortal.Services.EnvianceTreeService;
using System.Globalization;

namespace Hess.Corporate.GHGPortal.Services
{
    public class EnvianceAccess
    {
        #region Private Fields

        private Guid sessionId;
        private const string NOT_AUTHENTICATED_ERROR_FORMAT = "An error has occurred while trying to authenticate with Enviance. <br /> The current username is {0},  <br />current password is {1},  <br />The exception message is: {2} <br />The Stack trace is:{3}";
        private const string NO_AUTH_SERVICE_ERROR = "Unable to instantiate Authentication Service";
        private const string NOT_AUTHENTICATED_CALL_ERROR_FORMAT = "You must be authenticated before trying to call {0}";
        private const string RETRY_TIMEOUT_ERROR_FORMAT = "{0} records were submitted to Enviance but there was a timeout while checking for batch status";
        private TreeService treeService = null;
        private AuthService authService = null;
        private EnvianceDataSubmissionService.DataService dataService = null;
        AppConfiguration config = null;

        private const int INVALID_TAG_ERROR_KEY = -1;
        private const int INVALID_BATCH_ERROR_KEY = -2;
        private static int PROCESS_COMPLETE_KEY = 0;
        private static int PROCESS_TECHNICAL_ERROR_KEY = -3;
        //private const int 
        


        private Guid currentBatchId;
        #endregion

        #region Constructor

        public EnvianceAccess()
        {
            try
            {
                config = AppConfiguration.Current;

                authService = new AuthService();
                authService.Url = config.EnvianceAuthenticationServiceURL;

                dataService = new EnvianceDataSubmissionService.DataService();
                dataService.Url = config.EnvianceDataServiceURL;

                treeService = new TreeService();
                treeService.Url = config.EnvianceTreeServiceURL;

                Authenticate();

      
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
        #endregion

        #region Public Properties

        public bool IsAuthenticated { get { return !(sessionId == Guid.Empty); } }

        public static int ProcessSuccessfulCode
        {
            get { return PROCESS_COMPLETE_KEY; }
        }
        
        public static int ProcessTechnicalErrorCode
        {
            get { return PROCESS_TECHNICAL_ERROR_KEY; }
        }
        #endregion

        #region Web Service Methods

        public EnvianceInterfaceRecords SubmitRecords(EnvianceInterfaceRecords records)
        {

            if (!IsAuthenticated)
            {
                Authenticate();
            }
            try
            {
                List<String> tags = records.Select(t => t.TagName).ToList<String>();
                List<String> invalidTags = new List<String>(this.InvalidTags(tags));
                List<EnvianceInterfaceRecord> validRecords = records.Where(t => !invalidTags.Contains(t.TagName.Trim())).ToList();
                List<EnvianceInterfaceRecord> invalidRecords = records.Where(t => invalidTags.Contains(t.TagName.Trim())).ToList();
                Errors e = config.ErrorCodes;
                foreach (EnvianceInterfaceRecord record in invalidRecords)
                {
                    // string errorText = e.EnvianceSubmissionErrors[INVALID_TAG_ERROR_KEY].Value;
                    record.Status = MessageForError(Convert.ToString(INVALID_TAG_ERROR_KEY));
                    record.ErrorCode = INVALID_TAG_ERROR_KEY;
                    record.Save();
                }
                if (validRecords.Count > 0)
                {
                    SubmitBatch(validRecords);

                    BatchStatus currentStatus = null;
                    int totalExecutionTime = 0;

                    do
                    {
                        Thread.Sleep(config.EnvianceExecutionTimeout);
                        currentStatus = GetBatchStatus();
                        totalExecutionTime += config.EnvianceExecutionTimeout;

                    } while (totalExecutionTime < config.EnvianceRetryTimeout && currentStatus != null && (currentStatus.StatusCode == CommandStatusCode.Processing || currentStatus.StatusCode == CommandStatusCode.Waiting));

                    //there was a timeout notify the admin
                    if (currentStatus.StatusCode == CommandStatusCode.Waiting || currentStatus.StatusCode == CommandStatusCode.Processing)
                    {
                        throw new Exception(String.Format(RETRY_TIMEOUT_ERROR_FORMAT, records.Count));
                    }

                    if (currentStatus.StatusCode == CommandStatusCode.Failed)
                    {
                        throw new Exception(String.Format("Batch Submission Failed. {0} ", currentStatus.ErrorMessage));
                    }

                    InvalidBatchData[] badData = InvalidRows();
                    foreach (InvalidBatchData dataRecord in badData)
                    {
                        NumericDataPointValues submittedPoint = (NumericDataPointValues)dataRecord.DataDetails;
                        EnvianceInterfaceRecord badRecord = records.Where(t => submittedPoint.Tag == t.TagName).First<EnvianceInterfaceRecord>();

                        badRecord.Status = dataRecord.Error;
                        badRecord.ErrorCode = INVALID_BATCH_ERROR_KEY;
                        // badRecord.Save();
                        validRecords.Remove(badRecord);
                        invalidRecords.Add(badRecord);
                    }
                    //EmailOwners(invalidRecords);

                    foreach (EnvianceInterfaceRecord record in validRecords)
                    {
                        record.ErrorCode = ProcessSuccessfulCode;
                    }
                }
                
                
                CloseSession();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "<br />" + e.StackTrace);
            }


            return records;
        }

        private Guid Authenticate()
        {
            if (authService != null)
            {
                try
                {
                    sessionId = authService.Authenticate(config.EnvianceUsername, config.EnviancePassword, SessionType.WebServices);
                    //every time session id changes we need to update the authentication header
                    GHGPortal.Services.EnvianceAuthenticationService.SessionHeader authHeader = new Services.EnvianceAuthenticationService.SessionHeader();
                    authHeader.SessionID = sessionId;
                    authService.SessionHeaderValue = authHeader;

                    GHGPortal.Services.EnvianceDataSubmissionService.SessionHeader header = new Services.EnvianceDataSubmissionService.SessionHeader();
                    header.SessionID = sessionId;
                    dataService.SessionHeaderValue = header;

                    GHGPortal.Services.EnvianceTreeService.SessionHeader treeHeader = new Services.EnvianceTreeService.SessionHeader();
                    treeHeader.SessionID = sessionId;
                    treeService.SessionHeaderValue = treeHeader;
                }
                catch (Exception e)
                {
                    throw new Exception(String.Format(NOT_AUTHENTICATED_ERROR_FORMAT, config.EnvianceUsername, config.EnviancePassword, e.Message, e.StackTrace));
                }

            }
            else
            {
                throw new Exception(NO_AUTH_SERVICE_ERROR);
            }
            //authClient.
            return sessionId;
        }

        private string[] InvalidTags(List<string> tagsToCheck)
        {
            string[] invalidTags = null;
            if (IsAuthenticated)
            {
                invalidTags = treeService.CheckTagsExist(tagsToCheck.ToArray());
            }
            else
            {
                throw new Exception(String.Format(NOT_AUTHENTICATED_CALL_ERROR_FORMAT, " InvalidTags"));
            }
            return invalidTags;
        }

        private void SubmitBatch(IEnumerable<EnvianceInterfaceRecord> records)
        {
            if (IsAuthenticated)
            {
                if (records.Count() > 0)
                {
                    List<DataPoint> points = new List<DataPoint>();
                    foreach (EnvianceInterfaceRecord record in records)
                    {
                        DataPoint dataPoint = new DataPoint();
                        TagID tag = new TagID();
                        tag.Tag = record.TagName;
                        dataPoint.Collector = record.ConnectorName;
                        dataPoint.RequirementId = tag;
                        dataPoint.Value = record.EmissionVolume;
                        dataPoint.Complete = record.EventDate;
                        points.Add(dataPoint);

                    }

                    currentBatchId = dataService.SubmitNumericDataBatch(points.ToArray(), true, false);
                }

            }
            else
            {
                throw new Exception(String.Format(NOT_AUTHENTICATED_CALL_ERROR_FORMAT, " SubmitBatch"));
            }

        }

        private BatchStatus GetBatchStatus()
        {
            BatchStatus status = null;
            if (IsAuthenticated)
            {
                status = dataService.GetBatchStatus(currentBatchId);
            }
            else
            {
                throw new Exception(String.Format(NOT_AUTHENTICATED_CALL_ERROR_FORMAT, " Get Batch Status"));
            }
            return status;
        }

        private InvalidBatchData[] InvalidRows()
        {
            InvalidBatchData[] badData = null;

            if (IsAuthenticated)
            {
                badData = dataService.GetInvalidBatchData(currentBatchId);
            }
            else
            {
                throw new Exception(String.Format(NOT_AUTHENTICATED_CALL_ERROR_FORMAT, " InvalidRows"));
            }
            return badData;
        }

        private void CloseSession()
        {
            if (IsAuthenticated)
            {
                authService.CloseSession();
                sessionId = Guid.Empty;
                currentBatchId = Guid.Empty;
            }
            else
            {
                throw new Exception(String.Format(NOT_AUTHENTICATED_CALL_ERROR_FORMAT, " Close Session"));
            }
        }
        #endregion

        #region Business Methods

        private string MessageForError(string errorCode)
        {
            HashConfigurationElements errors = config.ErrorCodes.EnvianceSubmissionErrors;
            string msg = "";
            foreach (HashConfigurationElement error in errors)
            {
                if (error.Key == errorCode)
                {
                    msg = error.Value;
                    break;
                }
            }

            return msg;
        }
        #endregion
    }



}
