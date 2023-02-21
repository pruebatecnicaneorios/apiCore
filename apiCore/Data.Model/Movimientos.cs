using System.ComponentModel.DataAnnotations;

namespace apiCore.Data.Model
{
    public class Movimientos
    {
        [Key]
        public int movimientoId { get; set; }
        public int cuentaId { get; set; }
        public int clienteId { get; set; }

        public DateTime fecha { get; set; }

        public string? tipoMovimiento { get; set; }

        public decimal valor { get; set; }

        public decimal saldo { get; set; }

    }

}
