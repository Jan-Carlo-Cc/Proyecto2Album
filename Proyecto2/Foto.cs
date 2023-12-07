using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    public class Foto
    {
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }
        
        public Foto(int id, string descripcion, string ruta)
        {
            Id = id;
            Descripcion = descripcion;
            Ruta = ruta;
        }
    }
}
