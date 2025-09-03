using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INCA.Models
{
    public class Mantenimientos
    {
        [Key]
        public int Id_Mantenimiento { get; set; }


        public string? FK_Empleado_registra { get; set; }

        public string? FKid_Inventario { get; set; }

        public string? Descripcion_falla { get; set; }

        public DateTime? Fecha_registro { get; set; }

        [ForeignKey("FKempleadoAsignado")]
        public string? FK_Empleado_Asignado { get; set; }

        public DateTime? Fecha_Mantenimiento { get; set; }

        [ForeignKey("FkidEstatus")]
        public string? Fkid_Estatus { get; set; }


        public string? Descripcion_Solucion { get; set; }

        public DateTime? Fecha_Realizacion {get; set;}

        [ForeignKey("FkEmpleadoRealizaMtto")]
        public string? Fk_Empleado_Realiza_Mtto { get; set; }

        public string? Imagen_recep { get; set; }

        public string? Imagen_entrega { get; set; }




    }
}
