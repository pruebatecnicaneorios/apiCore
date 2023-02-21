namespace apiCore.Data.Model
{
    public class Cliente: Persona
    {
        public int clienteId { get; set; }

        public string? contraseña { get; set; }

        public bool estado { get; set; }

    }

}
