using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities.Properties;
using TSP.Exceptions;

namespace TSP.Exceptions
{
    public class TspException : Exception
    {
        private int m_errorCode;

        /// <summary>
        /// Contains the error code which indicates fro which part of the application the exception is comming from
        /// </summary>
        public int ErrorCode
        {
            get { return m_errorCode; }
            private set { m_errorCode = value; }
        }

        /// <summary>
        /// Create a new empty <see cref="TspException"/>.
        /// </summary>
        public TspException() : base(Resources.ExDefaultMessage)
        {
            ErrorCode = DiagnosticEvents.Base;
        }

        /// <summary>
        /// Create a new <see cref="TspException"/> with an event code.
        /// </summary>
        /// <param name="eCode"></param>
        public TspException(int eCode) : base(Resources.ExDefaultMessage)
        {
            ErrorCode = eCode;
        }
        /// <summary>
        /// Create a new <see cref="TspException"/> with an event code and a message
        /// </summary>
        /// <param name="eCode"></param>
        /// <param name="message"></param>
        public TspException(int eCode, string message) : base(message)
        {
            ErrorCode = eCode;
        }
        /// <summary>
        /// Create a new <see cref="TspException"/> with an event code, a message and an inner exception
        /// </summary>
        /// <param name="eCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TspException(int eCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = eCode;
        }

    }
}
