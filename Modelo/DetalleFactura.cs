﻿namespace ApiConsalud.Modelo
{
    public class DetalleFactura
    {
        public double CantidadProducto { get; set; }
        public Producto Producto { get; set; }
        public double TotalProducto { get; set; }
    }
}
