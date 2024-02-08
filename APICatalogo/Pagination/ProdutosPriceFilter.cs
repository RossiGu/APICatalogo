namespace APICatalogo.Pagination
{
    public class ProdutosPriceFilter : QueryStringParameters
    {
        public decimal? Preco { get; set; }
        public string? PrecoCriterio { get; set; }
    }
}
