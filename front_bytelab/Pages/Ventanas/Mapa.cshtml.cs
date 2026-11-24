using System.Text.Json;
using front_bytelab.nucleo;
using lib_bytelab.entidades;
using lib_bytelab.nucleo;
using presentaciones_bytelab.interfaces;

namespace front_bytelab.Pages.Ventanas
{
    public class MapaModel : PaginaSegura
    {
        private ISucursalesPresentacion ISucursalesPresentacion;

        public MapaModel(ISucursalesPresentacion ISucursalesPresentacion)
        {
            this.ISucursalesPresentacion = ISucursalesPresentacion;
        }

        public override string Permiso => "Mapa";
        public List<Sucursales> Sucursales { get; set; } = new List<Sucursales>();
        public string PuntosJson { get; set; } = "[]";

        public void OnGet()
        {
            try
            {
                Sucursales = this.ISucursalesPresentacion.Consultar();

                var puntos = Sucursales
                    .Where(x => x.Latitud != null && x.Longitud != null)
                    .Select(x => new
                    {
                        nombre = x.NombreSucursal,
                        direccion = x.Direccion + ", " + x.Ciudad,
                        lat = x.Latitud,
                        lng = x.Longitud
                    });
                PuntosJson = JsonSerializer.Serialize(puntos);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
