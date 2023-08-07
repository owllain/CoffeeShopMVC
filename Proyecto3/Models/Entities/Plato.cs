using System.ComponentModel.DataAnnotations;

namespace Proyecto3.Models.Entities
{
    public class Plato
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        public string Descripcion { get; set; }

        public byte[] ImagenData { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo Categoría es obligatorio.")]
        public string Categoria { get; set; }

        public string ImagenBase64
        {
            get
            {
                if (ImagenData != null && ImagenData.Length > 0)
                {
                    return $"data:image/jpeg;base64,{Convert.ToBase64String(ImagenData)}";
                }
                return string.Empty;
            }
        }
    }
}


