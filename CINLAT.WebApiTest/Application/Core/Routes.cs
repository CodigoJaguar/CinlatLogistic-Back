namespace CINLAT.WebApiTest.Application.Core
{
    public static class Routes
    {
        private static Dictionary<string, string[]>? secciones = new Dictionary<string, string[]>()
            {
                { "/expedientes", new string[] { "CLIENTES", "EMPLEADOS", "LISTAS" } },
                {"/home", new string[] { "CLIENTES", "EMPLEADOS", "LISTAS", "HEROES"} }
            };

        public static List<string> GetRoutes(List<string> Claims)
        {
            List<string> routes = new List<string>();

            if (secciones == null) return routes;

            foreach (string Claim in Claims)
            {
                foreach (var kvp in secciones)
                {
                    string clave = kvp.Key;
                    string[] valores = kvp.Value;

                    foreach (string valor in valores)
                    {
                        if (string.IsNullOrEmpty(valor)) continue;

                        if (routes.Contains(clave)) continue;

                        if (valor == Claim) routes.Add(clave);
                    }
                }
            }

            return routes;
        }
    }

}
