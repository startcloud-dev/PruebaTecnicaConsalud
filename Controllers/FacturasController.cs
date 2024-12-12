using ApiConsalud.Modelo;
using ApiConsalud.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiConsalud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        VolcarData volcarData = new VolcarData();
        List<Factura> facturas = new List<Factura>();


        public FacturasController()
        {
            facturas = volcarData.VolcarDatosJson();
        }

        [HttpGet("FacturaPorRutComprador/{rut:double}/{dv}")]

        public ActionResult<List<Factura>> GetFacturaPorRutComprador(double rut,string dv)
        {

            var facturasPorComprador = 
                facturas.Where(x => x.RUTComprador == rut && x.DvComprador.Equals(dv)).ToList();

            if (facturasPorComprador.Count == 0)
            {
                return NotFound();
            }

            return facturasPorComprador;

        }

        [HttpGet("GetAllFacturaMontoTotal")]
        public ActionResult<List<Factura>> GetAllFacturaMontoTotal()
        {
            
            foreach (var item in facturas)
            {
                double saldototal = 0;

                foreach (var itemdetalle in item.DetalleFactura)
                {
                    saldototal += itemdetalle.TotalProducto;
                }

                item.TotalFactura = saldototal;

            }

            return facturas;
        }

    }
}
