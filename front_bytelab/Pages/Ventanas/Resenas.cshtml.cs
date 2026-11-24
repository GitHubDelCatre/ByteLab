using front_bytelab.nucleo;
using lib_bytelab.entidades;
using lib_bytelab.nucleo;
using Microsoft.AspNetCore.Mvc.Filters;
using presentaciones_bytelab.interfaces;

namespace front_bytelab.Pages.Ventanas
{
    public class ResenasModel : VentanaBase<Resenas>
    {
        private IProductosPresentacion IProductosPresentacion;
        private IClientesPresentacion IClientesPresentacion;

        public ResenasModel(IResenasPresentacion IResenasPresentacion,
            IProductosPresentacion IProductosPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IReportes IReportes) : base(IResenasPresentacion, IReportes)
        {
            this.IProductosPresentacion = IProductosPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
        }

        public override string Titulo => "Reseñas";

        public List<Productos> ListaProductos { get; set; } = new List<Productos>();
        public List<Clientes> ListaClientes { get; set; } = new List<Clientes>();

        public bool EsCliente { get; set; }

        public Clientes? MiCliente { get; set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
            if (context.Result != null)
                return;

            EsCliente = Sesion.EsCliente(HttpContext);
            if (!EsCliente)
                return;

            try
            {
                var idUsuario = Sesion.IdDelUsuario(HttpContext);
                MiCliente = this.IClientesPresentacion.Consultar().FirstOrDefault(x => x.Usuario == idUsuario);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        protected override void CargarListas()
        {
            ListaProductos = this.IProductosPresentacion.Consultar();
            if (EsCliente)
                ListaClientes = MiCliente == null ? new List<Clientes>() : new List<Clientes>() { MiCliente };
            else
                ListaClientes = this.IClientesPresentacion.Consultar();
        }

        protected override void Inicializar(Resenas entidad)
        {
            entidad.FechaPublicacion = DateTime.Now;
            entidad.CalificacionEstrellas = 5;
            if (EsCliente && MiCliente != null)
                entidad.Cliente = MiCliente.Id;
        }

        public override void OnPostBtNuevo()
        {
            if (EsCliente && MiCliente == null)
            {
                ViewData["Mensaje"] = "Su usuario no está registrado como cliente. Pida al administrador que lo registre en Clientes.";
                OnPostBtConsultar();
                return;
            }
            base.OnPostBtNuevo();
        }

        public override void OnPostBtGuardar()
        {
            if (EsCliente)
            {
                if (MiCliente == null || this.Actual == null)
                {
                    ViewData["Mensaje"] = "Su usuario no está registrado como cliente.";
                    OnPostBtConsultar();
                    return;
                }
                if (this.Actual.Id != 0)
                {
                    ViewData["Mensaje"] = "Los clientes no pueden modificar reseñas.";
                    OnPostBtConsultar();
                    return;
                }
                this.Actual.Cliente = MiCliente.Id;
                this.Actual.FechaPublicacion = DateTime.Now;
            }
            base.OnPostBtGuardar();
        }

        public override void OnPostBtEditar(string data)
        {
            if (EsCliente)
            {
                ViewData["Mensaje"] = "Los clientes no pueden modificar reseñas.";
                OnPostBtConsultar();
                return;
            }
            base.OnPostBtEditar(data);
        }

        public override void OnPostBtPregBorrar(string data)
        {
            base.OnPostBtPregBorrar(data);
            if (EsCliente && !EsMia(this.Actual))
            {
                Borrar = false;
                ViewData["Mensaje"] = "Solo puede borrar sus propias reseñas.";
            }
        }

        public override void OnPostBtBorrar()
        {
            if (EsCliente)
            {
                CargarLista();
                var resena = this.Lista.FirstOrDefault(x => x.Id == this.Actual?.Id);
                if (!EsMia(resena))
                {
                    ViewData["Mensaje"] = "Solo puede borrar sus propias reseñas.";
                    return;
                }
            }
            base.OnPostBtBorrar();
        }

        public bool EsMia(Resenas? resena)
        {
            return resena != null && MiCliente != null && resena.Cliente == MiCliente.Id;
        }
    }
}
