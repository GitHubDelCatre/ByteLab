namespace pruebas_bytelab.Nucleo
{
    public static class Datos
    {
        public static string Unico(int largo)
        {
            var valor = Guid.NewGuid().ToString("N");
            return valor.Substring(0, Math.Min(largo, valor.Length));
        }

        public static string Texto(int largo)
        {
            var valor = "Prueba";
            return valor.Substring(0, Math.Min(largo, valor.Length));
        }
    }
}
