
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INCA.Data;
using INCA.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;





namespace INCA
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquiposController : ControllerBase
    {
        private readonly INCAContext _context;

        public EquiposController(INCAContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipos>>> listaequipos()
        {
            var equipos = await _context.Equipos.ToListAsync();
            return Ok(equipos);
        }

        // GET: Usuarios especifico
        [HttpGet("Buscarinv/{id}")]

        public async Task<ActionResult<Equipos>> Details(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound("ID es nulo o vacío");
            }

            try
            {
                var equipo = await _context.Equipos.FirstOrDefaultAsync(x => x.Id_InventarioPK == id);

                if (equipo == null)
                {
                    return Conflict("NO SE ENCONTRO EL INVENTARIO");
                }

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                // Loguear la excepción (aquí deberías inyectar ILogger en el controlador para loguear)
                // Por ahora, simplemente retorna el error para depurar
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }



        [HttpPost("AgregarEquipo")]
        public async Task<ActionResult<Equipos>> Create(Equipos disp)
        {
            bool equipoexist = await _context.Equipos
                .AnyAsync(x => x.Id_InventarioPK == disp.Id_InventarioPK);


            if (equipoexist)
            {
                return Conflict("EL EQUIPO YA EXISTE");
            }



            if (disp.Id_InventarioPK != null)
            {
                if (disp.Id_InventarioPK.Length > 7)
                {
                    _context.Equipos.Add(disp);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(Details), new { id = disp.Id_InventarioPK }, disp);

                }
                else
                {
                    return BadRequest("EL NUMERO DE INVENTARIO NO CUMPLE CON LOS REQUERIMIENTOS");
                }

            }
            else
            {
                return BadRequest("EL NUMERO DE INVENTARIO NO PUEDE SER VACIO");
            }
        }






        [HttpPatch("Acutalizarequipo/{id}")]
        public async Task<ActionResult> Actequipos(string id, [FromBody] JsonPatchDocument<Equipos> patchDoc)

        {

            if (id == null)
            {
                return BadRequest("INVENTARIO NO ENCONTRADO");
            }
            var equipoexist = await _context.Equipos.FindAsync(id);

            if (equipoexist == null)
            {
                return NotFound("INVENTARIO NO ENCONTRADO");
            }

            if (patchDoc == null)
            {
                return BadRequest("El documento patch es requerido");
            }
            patchDoc.ApplyTo(equipoexist, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _context.SaveChangesAsync();

            return Ok(equipoexist);
        }


        [HttpPut("actualizarcompleto/{id}")]
        public async Task<ActionResult> Actualizarequipocompleto(String id, [FromBody] Equipos actualizareq)
        {
            if (id != actualizareq.Id_InventarioPK)
            {
                return BadRequest("EL ID INGRESADO NO COINCIDE CON EL DEL EQUIPO");
            }
            var equipoexiste = await _context.Equipos.FindAsync(id);

            if (equipoexiste == null)
            {

                return NotFound("EQUIPO NO ENCONTRADO");
            }
            equipoexiste.Marca = actualizareq.Marca;
            equipoexiste.Modelo = actualizareq.Modelo;
            equipoexiste.Color = actualizareq.Color;
            equipoexiste.Num_serie = actualizareq.Num_serie;
            equipoexiste.Procesador = actualizareq.Procesador;
            equipoexiste.Memoria = actualizareq.Memoria;
            equipoexiste.tipo_HDD = actualizareq.tipo_HDD;
            equipoexiste.Sistema_operativo = actualizareq.Sistema_operativo;
            equipoexiste.observacion = actualizareq.observacion;
            equipoexiste.fecha_Compra = actualizareq.fecha_Compra;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error al actualizar en base de datos: {ex.InnerException?.Message ?? ex.Message}");
            }

            return Ok("EQUIPO ACTUALIZADO CON EXITO");

        }


        [HttpPatch("Actualizarfecha/{id}")]
        public async Task<ActionResult> colocarfecha(string id)

        {

            var equipo = await _context.Equipos.FindAsync(id);

            {
                if (equipo == null)
                {
                    return BadRequest("INVENTARIO NO ENCONTRADO");

                }
                equipo.Fecha_modificacion = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(equipo);
            }


        }
    }

}















    

