using apiCore.Data.Base;
using apiCore.Data.Model;
using apiCore.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly DataBaseContext _DbContext;
        public MovimientosController(DataBaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            try
            {
                return Ok(await MovimientosService.GetAsync(_DbContext));
            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO, ex.Message);
            }

        }

        [HttpGet]
        [Route("get-movimiento-by-movimientoId")]
        public async Task<IActionResult> GetClienteByIdAsync(int movimientoId)
        {

            try
            {
                return Ok(await MovimientosService.GetMovimientoByIdAsync(_DbContext, movimientoId));

            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO, ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Movimientos movimiento)
        {

            try
            {

                return Created($"/get-movimiento-by-movimientoId?movimientoId={movimiento.cuentaId}",
                    await MovimientosService.PostAsync(_DbContext,movimiento));

            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO, ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Movimientos movimientoToUpdate)
        {

            try
            {
                await MovimientosService.PutAsync(_DbContext, movimientoToUpdate);
                return NoContent();
            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO, ex.Message);
            }


        }

        [Route("{movimientoId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int movimientoId)
        {

            try
            {
                await MovimientosService.DeleteAsync(_DbContext, movimientoId);
                return NoContent();
            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO, ex.Message);
            }

        }


        [HttpGet]
        [Route("get-ListadoMovimientos-by-fecha-and-clienteId")]
        public async Task<IActionResult> GetListadoMovimientosByIdAsync(int clienteId, DateTime fechaInicial, DateTime fechaFinal)
        {

            try
            {
                return Ok(await MovimientosService.GetListadoMovimientosAsync(_DbContext, clienteId, fechaInicial, fechaFinal));

            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO, ex.Message);
            }


        }
    }

   
}
