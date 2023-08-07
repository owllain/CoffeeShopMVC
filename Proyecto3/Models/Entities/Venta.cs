using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto3.Models.Entities
{
    public class Venta
    {
        public int NumeroOrden { get; set; }

        [Required(ErrorMessage = "El campo Fecha y Hora de Venta es obligatorio.")]
        [Display(Name = "Fecha y Hora de Venta")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaHoraVenta { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Plato Vendido es obligatorio.")]
        [Display(Name = "Nombre del Plato Vendido")]
        public string NombrePlatoVendido { get; set; }

        [Required(ErrorMessage = "El campo Cantidad Vendida es obligatorio.")]
        [Display(Name = "Cantidad Vendida")]
        public int CantidadVendida { get; set; }
    }
}
