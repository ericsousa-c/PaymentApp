namespace PaymentApp.Domain.Entities
{
    public partial class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Pagamento> Pagamentos { get; set; }

        private Cliente() { }

        public static Cliente CriarCliente(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome não pode ser vazio.");
            }

            var cliente = new Cliente
            {
                Nome = nome,
                Email = email
            };

            return cliente;
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
            {
                throw new ArgumentException("Nome não pode ser vazio.");
            }

            Nome = novoNome;
        }
    }
}
