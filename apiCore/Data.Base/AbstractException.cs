using System;

namespace apiCore.Data.Base
{
    [Serializable]
    public abstract class AbstractException : Exception
    {


        public static readonly string ERROR_INTERNO = "Error interno del sistema";


        public int ExceptionId { get; set; }
        public string MessageValue { get; set; }


        protected AbstractException()
        {
        }

        protected AbstractException(int exceptionId)
        {
            ExceptionId = exceptionId;
        }

        protected AbstractException(string messageValue, Exception innerException)
            : base(messageValue, innerException)
        {
            MessageValue = messageValue;
        }

        protected AbstractException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
    }
}
