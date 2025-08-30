using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INCA.Models
{
    public class Empleados
    {
        [Key]
        public string Id_empleadopk { get; set; } 

        public string Apellido_paterno { get; set; }

        public string Apellido_Materno { get; set; }

        public string nombres { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public DateTime? Fecha_alta { get; set; }

        [ForeignKey("Estatus")] // Indica la clave foránea

        public string FkId_Estatus { get; set; }
        [ForeignKey("FkidDepartamento")] // Indica la clave foránea


        public string Fkid_Departamento { get; set; }
        [ForeignKey("FkidSucursal")] // Indica la clave foránea

        public string Fkid_Sucursal { get; set; }
        [ForeignKey("FkidRoles")] // Indica la clave foránea

        public int Fkid_Roles { get; set; }

        public DateTime? Fecha_Modificacion { get; set; }

        public int? User_Crea { get; set; }

        public int? User_Modifica { get; set; }



    }
}
