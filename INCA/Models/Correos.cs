using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace INCA.Models
{
    public class Correos
    {


        [Key]

        public int Idcorreo { get; set; }

        public string? email { get; set; }

        public bool? Asignado { get; set; }

        public bool? UsoGeneral { get; set; }

        public DateTime? FechaAsignacion { get; set; }


       public string? DeptoUsoGeneral { get; set; }

        public bool? UsaEquipoComputo { get; set; }

        [ForeignKey("FKId_Departamento")]



        public int? Estatus { get; set; }

        public DateTime? Fecha_creacion {  get; set; }

        public long? user_crea { get; set; }

        public long? user_modifica { get; set;}

        public DateTime? Fecha_modificacion { get; set; }
    }
}
