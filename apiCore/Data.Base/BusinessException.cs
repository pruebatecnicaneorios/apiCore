using System;

namespace apiCore.Data.Base
{
    public class BusinessException : AbstractException
    {
        //Profile messages
        public static readonly string EL_CLIENTE_NO_EXISTE = "El cliente no existe";

        public static readonly string LA_CUENTA_YA_EXISTE = "La cuenta ya existe";

        public static readonly string SALDO_NO_NEGATIVO = "El saldo no puede ser negativo";

        public static readonly string LA_CUENTA_NO_EXISTE = "La cuenta NO existe";

        public static readonly string LA_CUENTA_NO_PUEDE_CAMBIAR_DE_CLIENTE = "La cuenta no puede cambiar de cliente";

        public static readonly string EL_MOVIMIENTO_NO_EXISTE = "El movimiento NO existe";

        public static readonly string EL_MOVIMIENTO_YA_EXISTE = "El movimiento YA existe";

        public static readonly string SALDO_NO_DISONIBLE = "Saldo no disponible";

        public static readonly string EL_MOVIMIENTO_NO_PUEDE_CAMBIAR_DE_CUENTA = "El movimiento no puede cambiar de Cuenta";

        public static readonly string EL_MOVIMIENTO_NO_PUEDE_CAMBIAR_DE_CLIENTE = "El movimiento no puede cambiar de Cliente";

        public BusinessException()
        {
        }

        public BusinessException(Exception innerException) : base(innerException)
        {
        }

        public BusinessException(int exceptionId) : base(exceptionId)
        {
        }

        public BusinessException(string messageValue, Exception innerException) : base(messageValue, innerException)
        {
        }
    }
}
