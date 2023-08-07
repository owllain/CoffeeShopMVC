using System;
using System.ComponentModel.DataAnnotations;
namespace BaseDeDatos.Entities
{
    public class Reserva
    {
        [Key]
        public int NumeroReserva { get; set; }

        [Required(ErrorMessage = "El campo Fecha y Hora de Reserva es obligatorio.")]
        [Display(Name = "Fecha y Hora de Reserva")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaHoraReserva { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Cliente es obligatorio.")]
        [Display(Name = "Nombre del Cliente")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "El campo Número de Personas es obligatorio.")]
        [Display(Name = "Número de Personas")]
        public int NumPersonas { get; set; }
    }


}
