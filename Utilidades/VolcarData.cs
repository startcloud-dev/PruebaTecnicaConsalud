using ApiConsalud.Modelo;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ApiConsalud.Utilidades
{
    public class VolcarData
    {

        public List<Factura> VolcarDatosJson()
        {
            string json = File.ReadAllText("Json/JsonEjemplo.json");

            List<Factura> Facturas = JsonConvert.DeserializeObject<List<Factura>>(json);

            return Facturas;
        }


    }
}
