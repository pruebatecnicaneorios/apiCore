using apiCore.Data.Base;
using apiCore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace apiCore.Data.Services
{
    public class CuentaService
    {
          
        public static async Task<List<Cuenta>>  GetAsync(DataBaseContext _dbContext)
        {

            try
            {

                List<Cuenta> _cliente = await _dbContext.Cuenta.ToListAsync();

                return _cliente;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Cuenta> GetCuentaByIdAsync(DataBaseContext _dbContext, int _cuentaId)
        {

            try
            {


                Cuenta _cuenta = await _dbContext.Cuenta.FindAsync(_cuentaId);

                return _cuenta;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Cuenta> PostAsync(DataBaseContext _dbContext, Cuenta _cuenta)
        {

            try
            {

                Cliente _cliente = await _dbContext.Cliente.FindAsync(_cuenta.clienteId);

                if (_cliente == null)
                {
                    throw new BusinessException(BusinessException.EL_CLIENTE_NO_EXISTE, new Exception());
                }

                Cuenta _cuentaExiste = await _dbContext.Cuenta.FindAsync(_cuenta.cuentaId);

                if (_cuentaExiste != null)
                {
                    throw new BusinessException(BusinessException.LA_CUENTA_YA_EXISTE, new Exception());
                }
                 
                if (_cuenta.saldoInicial <0)
                {
                    throw new BusinessException(BusinessException.SALDO_NO_NEGATIVO, new Exception());
                }


                _dbContext.Cuenta.Add(_cuenta);
                await _dbContext.SaveChangesAsync();
                            

                return _cuenta;
               
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Cuenta> PutAsync(DataBaseContext _dbContext, Cuenta _cuenta)
        {

            try
            {

                Cuenta _cuentaExiste = await _dbContext.Cuenta.FindAsync(_cuenta.cuentaId);

                if (_cuentaExiste == null)
                {
                    throw new BusinessException(BusinessException.LA_CUENTA_NO_EXISTE, new Exception());
                }


                Cliente _cliente = await _dbContext.Cliente.FindAsync(_cuenta.clienteId);

                if (_cliente == null)
                {
                    throw new BusinessException(BusinessException.EL_CLIENTE_NO_EXISTE, new Exception());
                }

                if (_cuenta.clienteId != _cuentaExiste.clienteId)
                {
                    throw new BusinessException(BusinessException.LA_CUENTA_NO_PUEDE_CAMBIAR_DE_CLIENTE, new Exception());
                }


                if (_cuentaExiste.saldoInicial < 0)
                {
                    throw new BusinessException(BusinessException.SALDO_NO_NEGATIVO, new Exception());
                }

                _dbContext.Cuenta.Update(_cuenta);
                await _dbContext.SaveChangesAsync();
                return _cuenta;

            }
            catch (Exception)
            {
                throw;
            }


        }

        public static async Task<bool> DeleteAsync(DataBaseContext _dbContext, int _cuentaId)
        {

            try
            {


                Cuenta _cuentaToDelete = await _dbContext.Cuenta.FindAsync(_cuentaId);

                if (_cuentaToDelete == null)
                {
                    throw new BusinessException(BusinessException.LA_CUENTA_NO_EXISTE, new Exception());
                }

                _dbContext.Cuenta.Remove(_cuentaToDelete);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }


        }


    }
}
