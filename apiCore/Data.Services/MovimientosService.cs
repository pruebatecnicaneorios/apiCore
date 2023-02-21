using apiCore.Data.Base;
using apiCore.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace apiCore.Data.Services
{
    public class MovimientosService
    {
          
        /// <summary>
        /// Get the Movimientos List of the current context
        /// </summary>
        /// <param name="_dbContext"></param>
        /// <returns></returns>
        public static async Task<List<Movimientos>>  GetAsync(DataBaseContext _dbContext)
        {

            try
            {

                List<Movimientos> _movimiento = await _dbContext.Movimientos.ToListAsync();

                return _movimiento;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Movimientos> GetMovimientoByIdAsync(DataBaseContext _dbContext, int _movimientoId)
        {

            try
            {

              

                Movimientos _movimiento = await _dbContext.Movimientos.FindAsync(_movimientoId);

                if (_movimiento == null)
                {
                    throw new BusinessException(BusinessException.EL_MOVIMIENTO_NO_EXISTE, new Exception());
                }

                return _movimiento;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Movimientos> PostAsync(DataBaseContext _dbContext, Movimientos _movimiento)
        {

            try
            {
                Cuenta _cuentaExiste = await _dbContext.Cuenta.FindAsync(_movimiento.cuentaId);

                if (_cuentaExiste == null)
                {
                    throw new BusinessException(BusinessException.LA_CUENTA_NO_EXISTE, new Exception());
                }


                Cliente _cliente = await _dbContext.Cliente.FindAsync(_movimiento.clienteId);

                if (_cliente == null)
                {
                    throw new BusinessException(BusinessException.EL_CLIENTE_NO_EXISTE, new Exception());
                }

                Movimientos _movimientoExiste = await _dbContext.Movimientos.FindAsync(_movimiento.movimientoId);

                if (_movimientoExiste != null)
                {
                    throw new BusinessException(BusinessException.EL_MOVIMIENTO_YA_EXISTE, new Exception());
                }


                _movimientoExiste = await _dbContext.Movimientos.Where(x => x.cuentaId == _movimiento.cuentaId).OrderBy(x=> x.fecha).LastOrDefaultAsync();

                if (_movimientoExiste != null)
                {
                    if ((_movimientoExiste.saldo + _movimiento.valor)< 0)
                    {
                        throw new BusinessException(BusinessException.SALDO_NO_DISONIBLE, new Exception());
                    }
                    else
                    {
                        _movimiento.saldo = _movimientoExiste.saldo + _movimiento.valor;
                    }

                }
                else
                {
                    _movimiento.saldo = _cuentaExiste.saldoInicial + _movimiento.valor;                
                }

                _dbContext.Movimientos.Add(_movimiento);
                await _dbContext.SaveChangesAsync();


              


                return _movimiento;
               
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<Movimientos> PutAsync(DataBaseContext _dbContext, Movimientos _movimiento)
        {

            try
            {

                Movimientos _movimientoExiste = await _dbContext.Movimientos.FindAsync(_movimiento.movimientoId);

                if (_movimientoExiste == null)
                {
                    throw new BusinessException(BusinessException.EL_MOVIMIENTO_NO_EXISTE, new Exception());
                }

                if (_movimiento.cuentaId!= _movimientoExiste.cuentaId)
                {
                    throw new BusinessException(BusinessException.EL_MOVIMIENTO_NO_PUEDE_CAMBIAR_DE_CUENTA, new Exception());
                }

                if (_movimiento.clienteId != _movimientoExiste.clienteId)
                {
                    throw new BusinessException(BusinessException.EL_MOVIMIENTO_NO_PUEDE_CAMBIAR_DE_CLIENTE, new Exception());
                }


                Cuenta _cuentaExiste = await _dbContext.Cuenta.FindAsync(_movimiento.cuentaId);






                List<Movimientos> _movimientosActualizarSaldo = await _dbContext.Movimientos.Where(x => x.cuentaId == _movimiento.cuentaId).ToListAsync();


                _movimientosActualizarSaldo.Add(_movimiento);


                _movimientosActualizarSaldo = _movimientosActualizarSaldo.OrderBy(x => x.fecha).ToList();

                decimal _saldo = _cuentaExiste.saldoInicial;

                foreach (var item in _movimientosActualizarSaldo)
                {
                    _saldo = _saldo + item.valor;

                    if (_saldo < 0)
                    {
                        throw new BusinessException(BusinessException.SALDO_NO_DISONIBLE, new Exception());
                    }

                }


                _dbContext.Movimientos.Update(_movimiento);
                await _dbContext.SaveChangesAsync();


                _movimientosActualizarSaldo = await _dbContext.Movimientos.Where(x => x.cuentaId == _movimiento.cuentaId).ToListAsync();


                _saldo = _cuentaExiste.saldoInicial;

                foreach (var item in _movimientosActualizarSaldo)
                {
                    _saldo = _saldo + item.valor;

                    item.saldo = _saldo;

                    _dbContext.Movimientos.Update(item);

                }


                await _dbContext.SaveChangesAsync();


                return _movimiento;

            }
            catch (Exception)
            {
                throw;
            }


        }

        public static async Task<bool> DeleteAsync(DataBaseContext _dbContext, int _movimientoId)
        {

            try
            {


                Movimientos _movimientoToDelete = await _dbContext.Movimientos.FindAsync(_movimientoId);

                if (_movimientoToDelete == null)
                {
                    throw new BusinessException(BusinessException.EL_MOVIMIENTO_NO_EXISTE, new Exception());
                }





                List<Movimientos> _movimientosActualizarSaldo = await _dbContext.Movimientos.Where(x => x.cuentaId == _movimientoToDelete.cuentaId).ToListAsync();


                _movimientosActualizarSaldo.Remove(_movimientoToDelete);


                _movimientosActualizarSaldo = _movimientosActualizarSaldo.OrderBy(x => x.fecha).ToList();



                Cuenta _cuentaExiste = await _dbContext.Cuenta.FindAsync(_movimientoToDelete.cuentaId);



                decimal _saldo = _cuentaExiste.saldoInicial;

                foreach (var item in _movimientosActualizarSaldo)
                {
                    _saldo = _saldo + item.valor;

                    if (_saldo < 0)
                    {
                        throw new BusinessException(BusinessException.SALDO_NO_DISONIBLE, new Exception());
                    }

                }



                _dbContext.Movimientos.Remove(_movimientoToDelete);
                await _dbContext.SaveChangesAsync();

                _movimientosActualizarSaldo = await _dbContext.Movimientos.Where(x => x.cuentaId == _movimientoToDelete.cuentaId).ToListAsync();

                 _saldo = _cuentaExiste.saldoInicial;

                foreach (var item in _movimientosActualizarSaldo)
                {
                    _saldo = _saldo + item.valor;

                    item.saldo = _saldo;

                    _dbContext.Movimientos.Update(item);

                }



                return true;

            }
            catch (Exception)
            {
                throw;
            }


        }


        public static async Task<List<ListadoMovimientos>> GetListadoMovimientosAsync(DataBaseContext _dbContext, int _clienteId, DateTime _fechaInicial, DateTime _fechaFinal)
        {

            try
            {

                List<Movimientos> _movimientos = await _dbContext.Movimientos.Where(x=> 
                x.clienteId == _clienteId
                && x.fecha.Date >= _fechaInicial.Date
                && x.fecha.Date <= _fechaFinal.Date).ToListAsync();


                Cliente _cliente = await _dbContext.Cliente.Where(x=> x.clienteId==_clienteId).FirstOrDefaultAsync();

                List<Cuenta> _cuentas = await _dbContext.Cuenta.Where(x => x.clienteId == _clienteId).ToListAsync();


                List<ListadoMovimientos> _listado = new List<ListadoMovimientos>();

                foreach (var item in _movimientos)
                {

                    ListadoMovimientos _movitem = new ListadoMovimientos();
                  
                    _movitem.cliente = _cliente.nombre;
                    _movitem.tipoMovimiento = item.tipoMovimiento;
                    _movitem.saldoInicial = _cuentas.Where(x => x.cuentaId == item.cuentaId).FirstOrDefault().saldoInicial;
                    _movitem.estado = _cuentas.Where(x => x.cuentaId == item.cuentaId).FirstOrDefault().estado;
                    _movitem.tipoCuenta = _cuentas.Where(x => x.cuentaId == item.cuentaId).FirstOrDefault().tipoCuenta;
                    _movitem.saldo = item.saldo;
                    _movitem.fecha = item.fecha;
                    _movitem.numeroCuenta = _cuentas.Where(x => x.cuentaId == item.cuentaId).FirstOrDefault().numeroCuenta;

                    _listado.Add(_movitem);

                }

                return _listado;

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
