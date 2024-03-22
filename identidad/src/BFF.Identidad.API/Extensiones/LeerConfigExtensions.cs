namespace BFF.Identidad.API.Extensiones
{
    public static class LeerConfigExtensions
    {
        public static IConfigurationRoot CargarConfig(string nombreArchivo)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(nombreArchivo, optional: false, reloadOnChange: true)
                .Build();
        }

        public static T CargarSeccion<T>(this ConfigurationManager configurationManager) where T : class
        {
            return configurationManager
                .GetSection(typeof(T).Name)
                .Get<T>();
        }

    }
}
