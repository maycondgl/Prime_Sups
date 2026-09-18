namespace ObjetoTransferencia
{
    public class Cliente
    {
        public int IdCliente {  get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public bool Sexo { get; set; }

        public decimal LimiteCompra { get; set; }

    }
}
