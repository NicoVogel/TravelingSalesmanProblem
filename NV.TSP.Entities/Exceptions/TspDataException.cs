using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Exceptions
{
    public class TspDataException : TspException
    {

        /// <summary>
        /// Create a new <see cref="TspDataException"/> with an event code and a message
        /// </summary>
        /// <param name="eCode"></param>
        /// <param name="message"></param>
        public TspDataException(int eCode, string message) : base(eCode, message) { }

        /// <summary>
        /// Create a new <see cref="TspDataException"/> with an event code, a message and an inner exception
        /// </summary>
        /// <param name="eCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TspDataException(int eCode, string message, Exception innerException) : base(eCode, message, innerException) { }
    }
}
