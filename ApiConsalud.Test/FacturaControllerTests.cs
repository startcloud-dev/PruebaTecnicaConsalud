using ApiConsalud.Controllers;
using ApiConsalud.Dto;
using ApiConsalud.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace ApiConsalud.Test
{
    [TestClass]
    public sealed class FacturaControllerTests
    {


        [TestMethod]
        public void ObtenerGetAllFacturaMontoTotal_DevuelveLista()
        {

            //Preparacion
            FacturasController facturaapi = new FacturasController();
            ActionResult<List<Factura>> facturas = new List<Factura>();

            //Ejecucion
            facturas = facturaapi.GetAllFacturaMontoTotal();

            //Verificacion
            Assert.IsNotNull(facturas.Value);
        }

        [TestMethod]
        public void GetFacturaPorRutComprador_DevuelveCorrectoComprador()
        {

            //Preparacion
            FacturasController facturaapi = new FacturasController();
            ActionResult<List<Factura>> facturas = new List<Factura>();
            double rutComprador = 21595854;
            string dvComprador = "k";
            bool verificacion = false;
            //Ejecucion
            facturas = facturaapi.GetFacturaPorRutComprador(rutComprador, dvComprador);
            verificacion = facturas.Value.Any(x => x.RUTComprador == rutComprador);
            //Verificacion

            Assert.IsTrue(verificacion);
        }

        [TestMethod]
        public void GetFacturaAgrupadaPorComuna_DevuelveCorrectoComuna()
        {

            //Preparacion
            FacturasController facturaapi = new FacturasController();
            ActionResult<List<Factura>> facturas = new List<Factura>();
            double comunaComprador = 45;

            bool verificacion = false;
            //Ejecucion
            facturas = facturaapi.GetFacturaAgrupadaPorComuna(comunaComprador);
            verificacion = facturas.Value.Any(x => x.ComunaComprador == comunaComprador);
            //Verificacion

            Assert.IsTrue(verificacion);
        }

        [TestMethod]
        public void GetCompradorConMasCompras_DevuelveCorrecto()
        {

            //Preparacion
            FacturasController facturaapi = new FacturasController();
            ActionResult<List<Factura>> facturas = new List<Factura>();

            //Ejecucion
            facturas = facturaapi.GetCompradorConMasCompras();

            double montoMayor = facturas.Value.Max(x => x.TotalFactura);

            var listaFiltrada = facturas.Value.Where(x => x.TotalFactura == montoMayor).ToList();

            //Verificacion

            Assert.IsNotNull(listaFiltrada);
        }

        [TestMethod]
        public void GetListaCompradorTotal_DevuelveListaCorrecta()
        {

            //Preparacion
            FacturasController facturaapi = new FacturasController();
            ActionResult<List<CompradorDto>> CompradorDtoLista = new List<CompradorDto>();
            bool verificacion = false;

            //Ejecucion
            CompradorDtoLista = facturaapi.GetListaCompradorTotal();
            if (CompradorDtoLista.Value.Count > 0)
            {
                verificacion = true;

            }

            //Verificacion

            Assert.IsTrue(verificacion);
        }


    }
}
