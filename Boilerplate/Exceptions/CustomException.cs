using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebGrease.Css.Extensions;

namespace Boilerplate.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        public ExceptionCode ExceptionCode { get; set; }
        public List<string> MessageParamters = new List<string>();

        public CustomException()
        {
        }

        public CustomException(ExceptionCode exceptionCode, params string[] paramters)
            : base($"{(int)exceptionCode}-{exceptionCode.ToString()}")
        {
            this.ExceptionCode = exceptionCode;

            paramters.ForEach(x => MessageParamters.Add(x));
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CustomException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}