using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Exceptions
{
    public class GeneralException : Exception
    {
        public string FullMessage
        {
            get { return this.GetMessage(this); }
        }

        public string FullStack
        {
            get { return this.GetStack(this); }
        }

        public GeneralException(string message): base(message) { }
        public GeneralException(string message, Exception innerException) : base(message, innerException) { }

        private string GetMessage(Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
                message += "\r\n\r\n" + this.GetMessage(ex.InnerException);
            return message;
        }

        private string GetStack(Exception ex)
        {
            string message = ex.StackTrace;
            if (ex.InnerException != null)
                message += "\r\n\r\n" + this.GetStack(ex.InnerException);
            return message;
        }
    }
}
