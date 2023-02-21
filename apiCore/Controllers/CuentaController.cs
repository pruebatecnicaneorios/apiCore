using apiCore.Data.Base;
using apiCore.Data.Model;
using apiCore.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly DataBaseContext _DbContext;
       
        public CuentaController(DataBaseContext DbContext)
        {
            _DbContext = DbContext;                            

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            try
            {
                return Ok(await CuentaService.GetAsync(_DbContext));
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
        [Route("get-cuenta-by-cuentaId")]
        public async Task<IActionResult> GetClienteByIdAsync(int cuentaId)
        {

            try
            {
                return Ok(await CuentaService.GetCuentaByIdAsync(_DbContext,cuentaId));

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
        public async Task<IActionResult> PostAsync(Cuenta cuenta)
        {

            try
            {

                return Created($"/get-cuenta-by-cuentaId?cuentaId={cuenta.cuentaId}",
                    await CuentaService.PostAsync(_DbContext, cuenta));

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
        public async Task<IActionResult> PutAsync(Cuenta cuentaToUpdate)
        {

            try
            {
                await CuentaService.PutAsync(_DbContext, cuentaToUpdate);
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

        [Route("{cuentaId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int cuentaId)
        {

            try
            {
                await CuentaService.DeleteAsync(_DbContext, cuentaId);
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
    }

   
}
