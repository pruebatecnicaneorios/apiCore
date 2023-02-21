using apiCore.Data.Base;
using apiCore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace apiCore.Data.Services
{
    public class ClienteService
    {
          
        public static async Task<List<Cliente>>  GetAsync(DataBaseContext DbContext)
        {

            try
            {

                List<Cliente> cliente = await DbContext.Cliente.ToListAsync();

                return cliente;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Cliente> GetClienteByIdAsync(DataBaseContext DbContext, int ClienteId)
        {

            try
            {
                             

                Cliente cliente = await DbContext.Cliente.FindAsync(ClienteId);

                return cliente;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Cliente> PostAsync(DataBaseContext DbContext, Cliente _Cliente)
        {

            try
            {


                DbContext.Cliente.Add(_Cliente);
                await DbContext.SaveChangesAsync();
                return _Cliente;
               
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Cliente> PutAsync(DataBaseContext DbContext, Cliente _Cliente)
        {

            try
            {


                DbContext.Cliente.Update(_Cliente);
                await DbContext.SaveChangesAsync();
                return _Cliente;

            }
            catch (Exception)
            {
                throw;
            }


        }

        public static async Task<bool> DeleteAsync(DataBaseContext DbContext, int clienteId)
        {

            try
            {
                           

                Cliente clienteToDelete = await DbContext.Cliente.FindAsync(clienteId);

                if (clienteToDelete == null)
                {
                    throw new BusinessException(BusinessException.EL_CLIENTE_NO_EXISTE,new Exception());
                }

                DbContext.Cliente.Remove(clienteToDelete);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }


        }


    }
}
