using ApiConsalud.Dto;
using ApiConsalud.Modelo;
using ApiConsalud.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApiConsalud.Controllers
{
    [Route("api/Facturas")]
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

        public ActionResult<List<Factura>> GetFacturaPorRutComprador(double rut, string dv)
        {
            try
            {


                List<Factura> facturaList = GetAllFacturaMontoTotal().Value;

                facturaList =
                facturas.Where(x => x.RUTComprador == rut && x.DvComprador.Equals(dv)).ToList();

                if (facturaList.Count == 0)
                {
                    return NotFound();
                }

                return facturaList;
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }



        }

        [HttpGet("GetAllFacturaMontoTotal")]
        public ActionResult<List<Factura>> GetAllFacturaMontoTotal()
        {

            try
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
            catch (Exception ex)
            {

                return StatusCode(500);
            }


        }

        [HttpGet("FacturasAgrupadaPorComuna/{ComunaComprador:double}")]

        public ActionResult<List<Factura>> GetFacturaAgrupadaPorComuna(double ComunaComprador)
        {

            try
            {

                List<Factura> facturaList = GetAllFacturaMontoTotal().Value;

                facturaList = facturas.Where(x => x.ComunaComprador == ComunaComprador)
               .GroupBy(x => x.ComunaComprador).SelectMany(x => x).ToList();

                return facturaList;
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }


        }


        [HttpGet("GetCompradorConMasCompras")]
        public ActionResult<List<Factura>> GetCompradorConMasCompras()
        {

            try
            {
                List<Factura> facturaList = GetAllFacturaMontoTotal().Value;

                double montoMayor = facturaList.Max(x => x.TotalFactura);

                var listaFiltrada = facturaList.Where(x => x.TotalFactura == montoMayor).ToList();

                return listaFiltrada;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }


        [HttpGet("GetListaCompradorTotal")]
        public  ActionResult<List<CompradorDto>> GetListaCompradorTotal()
        {
            try
            {
                List<Factura> facturaList = GetAllFacturaMontoTotal().Value;
                List<CompradorDto> compradorDtos = new List<CompradorDto>();

                foreach (var item in facturaList)
                {
                    compradorDtos.Add(new CompradorDto
                    {
                        RUTComprador = item.RUTComprador,
                        DvComprador = item.DvComprador,
                        MontoTotalCompras = item.TotalFactura
                    });
                }

                return compradorDtos;
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }
            

        }

    }
}
