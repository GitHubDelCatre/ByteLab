namespace lib_bytelab.entidades
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }

        public abstract List<string> Validar();

        public bool FaltaInformacion()
        {
            return Validar().Count > 0;
        }
    }
}
