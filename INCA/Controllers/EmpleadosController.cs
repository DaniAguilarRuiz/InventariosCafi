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
    public class EmpleadosController : ControllerBase
    {
        private readonly INCAContext _context;

        public EmpleadosController(INCAContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Empleados>>> listaempleados()
        {
            var empleados = await _context.Empleados.ToListAsync();
            return Ok(empleados);
        }
        // GET: Usuarios especifico
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleados>> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Empleados.FirstOrDefaultAsync(m => m.Id_empleadopk == id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // GET: Usuarios/Create
        [HttpPost("Agregarempleado")]
        public async Task<ActionResult<Empleados>> Create(Empleados usuario)

        {
            bool existeEmpleado = await _context.Empleados
        .AnyAsync(e => e.Id_empleadopk == usuario.Id_empleadopk || e.email == usuario.email);


            if (existeEmpleado)
            {

                return Conflict("Ya existe un empleado con el mismo Numero de Empleado o Email");
            }
            if (usuario.Id_empleadopk != null) { 
                if (usuario.Id_empleadopk.Length > 5)
                { 
                  _context.Empleados.Add(usuario);
                   await _context.SaveChangesAsync();

                   return CreatedAtAction(nameof(Details), new { id = usuario.Id_empleadopk }, usuario);
                }
                else
                {
                    return BadRequest("El ID del empleado no cumple con los requerimientos");
                }
            }
            else
            {
                return BadRequest("El ID del empleado no puede ser vacio");

            }

        }


        [HttpPut("actualizarempleado/{id}")]
        public async Task<IActionResult> ActualizarEmpleado(string id, [FromBody] Empleados empleadoActualizado)
        {
            if (id != empleadoActualizado.Id_empleadopk)
            {
                return BadRequest("El ID proporcionado no coincide con el del empleado.");
            }
            var empleadoExistente = await _context.Empleados.FindAsync(id);
            if (empleadoExistente == null)
            {
                return NotFound("Empleado no encontrado.");
            }

            empleadoExistente.Apellido_paterno = empleadoActualizado.Apellido_paterno;
            empleadoExistente.Apellido_Materno = empleadoActualizado.Apellido_Materno;
            empleadoExistente.nombres = empleadoActualizado.nombres;
            empleadoExistente.email = empleadoActualizado.email;
            empleadoExistente.FkId_Estatus = empleadoActualizado.FkId_Estatus;
            empleadoExistente.Fkid_Departamento = empleadoActualizado.Fkid_Departamento;
            empleadoExistente.Fkid_Sucursal = empleadoActualizado.Fkid_Sucursal;
            empleadoExistente.Fkid_Roles = empleadoActualizado.Fkid_Roles;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error al actualizar en base de datos: {ex.InnerException?.Message ?? ex.Message}");
            }

            return Ok("Empleado actualizado correctamente.");
        }

    }


        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idempleadopk,Apellido_paterno,Apellido_Materno,nombres,email,password,Fecha_alta,Estatus,Fkid_Departamento,Fkid_Sucursal,Fkid_Roles,Fecha_Modificacion,User_Crea,User_Modifica")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Idempleadopk,Apellido_paterno,Apellido_Materno,nombres,email,password,Fecha_alta,Estatus,Fkid_Departamento,Fkid_Sucursal,Fkid_Roles,Fecha_Modificacion,User_Crea,User_Modifica")] Usuarios usuarios)
        {
            if (id != usuarios.Idempleadopk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.Idempleadopk))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Idempleadopk == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(string id)
        {
            return _context.Usuarios.Any(e => e.Idempleadopk == id);
        }*/
    }
    
