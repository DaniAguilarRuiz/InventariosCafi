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
using System.Numerics;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;




namespace INCA
{
    [ApiController]
    [Route("api/[controller]")]
    public class MantenimientoController : ControllerBase
    {
        private readonly INCAContext _context;


        public MantenimientoController(INCAContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimientos>>> listamtto()
        {
            var mtto = await _context.Mantenimientos.ToListAsync();
            return Ok(mtto);
        }



        [HttpGet]
        [Route("Buscarmant/{id}")]
        public async Task<ActionResult<Mantenimientos>> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var mantte = await _context.Mantenimientos.FindAsync(id);


                if (mantte == null)
                {
                    return NotFound();
                }

                return Ok(mantte);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }




        [HttpPost]
        [Route("newmant")]

        public async Task<ActionResult<Mantenimientos>> Crear(Mantenimientos mante)
        {
           if(mante.FKid_Inventario != null)
           {
                if (mante.FK_Empleado_Asignado != null)
                {
                    mante.Fecha_registro = DateTime.Now;
                     _context.Mantenimientos.Add(mante);

                    await _context.SaveChangesAsync();
                    return mante;
                }
                else
                {
                    return BadRequest("NO SE AH ASIGNADO UN TECNICO");
                }

           }
            else
            {
                return BadRequest("SE CUENTA CON UN INVENTARIO VALIDO");

            }
          
        }




        [HttpPatch]
        [Route("editmant/{id}")]
        public async Task<ActionResult<Mantenimientos>> editmant(String id, [FromBody] JsonPatchDocument<Mantenimientos> patchmtto)
        {
            if (id == null)
            {
                return BadRequest();

            }
            var mante = await _context.Mantenimientos.FindAsync(id);

            if (mante == null)
            {
                return BadRequest();
            }

            if (patchmtto == null)
            {
                return BadRequest("El documento patch es requerido");
            }

            patchmtto.ApplyTo(mante, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _context.SaveChangesAsync();
            return Ok(mante);

        }



        [HttpPut]
        [Route("actucommante/{id}")]
        public async Task<ActionResult<Mantenimientos>> putmante(string ig, [FromBody] Mantenimientos actumante)
        {
            if (ig == null)
            {
                return BadRequest();
            }
            var mante = await _context.Mantenimientos.FindAsync(ig);

            if (mante != null)
            {
                mante.Descripcion_falla = actumante.Descripcion_falla;





                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al actualizar en base de datos: {ex.InnerException?.Message ?? ex.Message}");


                }


            }
            return Ok(mante);

        }

            

    }
}
