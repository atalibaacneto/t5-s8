namespace ComexT2.Modelos
{
    internal class Livro : Produto, IIdentificavel
    {
        public Livro(string nome) : base(nome)
        {
        }

        public string Isbn { get; set; }
        public int TotalDePaginas { get; set; }

        public string Identificar()
        {
            return $"Livro: {Nome}, ISBN: {Isbn}";
        }
    }
}
