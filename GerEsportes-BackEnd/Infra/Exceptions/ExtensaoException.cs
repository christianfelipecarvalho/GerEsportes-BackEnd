namespace GerEsportes_BackEnd.Infra.Exceptions
{
    public static class ExtensaoException
    {
        public static T gExceptionSeNull<T>(this T obj, string msg) where T : class
        {
            if (obj == null)
            {
                throw new Exception(msg);
            }

            return obj;
        }

        public static T gExceptionSeNull<T>(this T obj, string msg, params object[] args) where T : class
        {
            if (obj == null)
            {
                throw new Exception(string.Format(msg, args));
            }

            return obj;
        }

        public static string gTratar(this Exception exp)
        {
            string text = exp.Message;
            for (Exception innerException = exp.InnerException; innerException != null; innerException = innerException.InnerException)
            {
                text = text + "\nMais detalhes: " + innerException.Message;
            }

            return text;
        }

        public static string gTratarUltimo(this Exception exp)
        {
            string message = exp.Message;
            for (Exception innerException = exp.InnerException; innerException != null; innerException = innerException.InnerException)
            {
                message = innerException.Message;
            }

            return message;
        }

        public static Exception gRecuperarUltimaExcessao(this Exception exp)
        {
            while (exp.InnerException != null)
            {
                exp = exp.InnerException;
            }

            return exp;
        }

        public static IEnumerable<T> gExceptionSeVazio<T>(this IEnumerable<T> obj, string msg, params object[] args) where T : class
        {
            if (obj.gVazio())
            {
                throw new Exception(string.Format(msg, args));
            }

            return obj;
        }

        public static string gMsgCompleta(this Exception e)
        {
            if (e == null)
            {
                return null;
            }

            string text = e.Message;
            if (e.InnerException != null)
            {
                text = $"{text}\n{e.InnerException.gMsgCompleta()}";
            }

            return text;
        }

        public static T gExceptionSeTrue<T>(this T obj, Func<T, bool> cond, string msg, params object[] args)
        {
            if (!cond(obj))
            {
                return obj;
            }

            throw new Exception(string.Format(msg, args));
        }

        public static T gExceptionSeFalse<T>(this T obj, Func<T, bool> cond, string msg, params object[] args)
        {
            if (cond(obj))
            {
                return obj;
            }

            throw new Exception(string.Format(msg, args));
        }

        public static void gExceptionSeTrue(this bool obj, string msg, params object[] args)
        {
            if (!obj)
            {
                return;
            }

            throw new Exception(string.Format(msg, args));
        }

        public static void gExceptionSeFalse(this bool obj, string msg, params object[] args)
        {
            if (obj)
            {
                return;
            }

            throw new Exception(string.Format(msg, args));
        }
    }
}
