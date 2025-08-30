using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INCA.Models
{
    public class Equipos
    {
        [Key]
        public string Id_InventarioPK { get; set; }

        public string? Marca { get; set; }

        public string? Modelo { get; set; }

        public string? Color { get; set; }


        public string? Num_serie { get; set; }


        public string? Procesador { get; set; }

        public string? Memoria { get; set; }


        public string? tipo_HDD { get; set; }

        public string? Sistema_operativo { get; set; }


        public string? observacion { get; set; }

        public DateTime? fecha_Compra { get; set; }

        public DateTime? Fecha_baja { get; set; }


        public string? motivo_Baja { get; set; }

        [ForeignKey("FkidTipo")]
        public string Fkid_Tipo { get; set; }

        [ForeignKey("FkidEstatus")]
        public string Fkid_Estatus { get; set; }


        public long? Asignado { get; set; }

        public DateTime? fecha_alta { get; set; }

        public DateTime? Fecha_modificacion { get; set; }
        public long? User_modifica { get; set; }

        public long? User_crea { get; set; }







    }
}
