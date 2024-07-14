namespace GerEsportes_BackEnd.Infra.Exceptions
{
    public static class ExtensaoLista
    {
        public static List<T> gNovaLista<T>(this T obj)
        {
            return new List<T> { obj };
        }

        public static bool gVazio<T>(this IEnumerable<T> obj, Func<T, bool> exp)
        {
            return !obj.gAny(exp);
        }

        public static bool gVazio<T>(this IEnumerable<T> obj)
        {
            return obj.gVazio((T p) => true);
        }

        public static bool gAny<T>(this IEnumerable<T> obj, Func<T, bool> exp)
        {
            return obj?.Any(exp) ?? false;
        }

        public static bool gAny<T>(this IEnumerable<T> obj)
        {
            return obj.gAny((T p) => true);
        }

        public static string gConcatena<T>(this IEnumerable<T> objs, string delimitador)
        {
            string text = objs.Aggregate(null, (string current, T obj) => current + delimitador + obj.ToString());
            if (!objs.Any())
            {
                return null;
            }

            if (!text.StartsWith(delimitador))
            {
                return text;
            }

            return text.Substring(delimitador.Length);
        }

        public static List<List<T>> gRepartidaListaPorNumeroMaxDeItens<T>(this List<T> lista, int numeroMaxItens)
        {
            List<List<T>> list = new List<List<T>>();
            if (numeroMaxItens <= 1)
            {
                list.Add(lista);
                return list;
            }

            double num = Math.Ceiling((double)lista.Count / (double)numeroMaxItens);
            if (num <= 0.0)
            {
                list.Add(lista);
                return list;
            }

            int num2 = (int)Math.Ceiling((double)lista.Count / num);
            for (int i = 0; (double)i < num; i++)
            {
                list.Add(lista.Skip(num2 * i).Take(num2).ToList());
            }

            return list;
        }
    }
}
