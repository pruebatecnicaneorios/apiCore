using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace apiCore.Data.Model
{
    public class ListadoMovimientos
    {
       
             public DateTime   fecha { get; set; }
   public string cliente        { get; set; }
    public string    numeroCuenta { get; set; }
  public string  tipoCuenta { get; set; }
public decimal saldoInicial { get; set; }
   public bool estado        { get; set; }
  public string      tipoMovimiento { get; set; }
  public decimal  saldo        { get; set; }

    }

}
