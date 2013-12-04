using System;
using System.Diagnostics;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message, EventLogEntryType messageType)
        {
            this.Message = message;
            this.MessageType = messageType;
            this.EventTime = DateTime.Now;
        }

        public MessageEventArgs(Exception exception) : this(exception.Message, EventLogEntryType.Error)
        {
            this.InnerException = exception;
        }

        public string Message { get; private set; }
        public DateTime EventTime { get; private set; }
        public EventLogEntryType MessageType { get; private set; }
        public Exception InnerException { get; private set; }
    }
}
