using apiCore.Data.Base;
using apiCore.Data.Model;
using apiCore.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;

namespace apiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly DataBaseContext _DbContext;
            
        public ClienteController(DataBaseContext DbContext)
        {
            _DbContext = DbContext;            
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            try
            {               
                return Ok(await ClienteService.GetAsync(_DbContext));
            }
            catch (AbstractException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Problem(GeneralException.ERROR_INTERNO,ex.Message);
            }
                       
        }

        [HttpGet]
        [Route("get-cliente-by-clienteId")]
        public async Task<IActionResult> GetClienteByIdAsync(int clienteId)
        {

            try
            {
                return Ok(await ClienteService.GetClienteByIdAsync(_DbContext, clienteId));

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
        public async Task<IActionResult> PostAsync(Cliente cliente)
        {

            try
            {
                                return Created($"/get-cliente-by-clienteId?clienteId={cliente.clienteId}", 
                    await ClienteService.PostAsync(_DbContext, cliente));              

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
        public async Task<IActionResult> PutAsync(Cliente clienteToUpdate)
        {

            try
            {
                await ClienteService.PutAsync(_DbContext, clienteToUpdate);
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

        [Route("{clienteId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int clienteId)
        {

            try
            {             
                await ClienteService.DeleteAsync(_DbContext, clienteId);
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
