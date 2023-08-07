namespace Proyecto3.Models.Entities
{
    public class VentaDelDia
    {
        public DateTime FechaHoraVenta { get; set; }
        public string NombrePlatoVendido { get; set; }
        public int CantidadVendida { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
