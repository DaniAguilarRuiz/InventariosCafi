using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INCA.Data;
using INCA.Models;




namespace INCA
{


    [ApiController]
    [Route("api/[controller]")]
    public class CorreosController : Controller

    {

        private readonly INCAContext _context;

        public CorreosController(INCAContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Correos>>> listacorreos()
        {
            var correos = await _context.Correos.ToListAsync();
            return Ok(correos);
        }





        [HttpGet]
        [Route("buscarcorreo/{id}")]
        public async Task<ActionResult<Correos>> busquedamail(int id)
        {
            if (id == '0')
            {
                return BadRequest();
            }
            try
            {

                var mail = await _context.Correos.FindAsync(id);

                if (mail == null)
                {
                    return NotFound();
                }

                return Ok(mail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
