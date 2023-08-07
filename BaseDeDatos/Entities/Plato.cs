using System;

namespace BaseDeDatos.Entities
{
    public class Plato
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] ImagenData { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }
}


