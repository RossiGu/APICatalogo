using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdenados;
        }

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutosPriceFilter produtosPriceFilter)
        {
            var produtos = GetAll().AsQueryable();

            if (produtosPriceFilter.Preco.HasValue && !string.IsNullOrEmpty(produtosPriceFilter.PrecoCriterio)) 
            {
                if (produtosPriceFilter.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosPriceFilter.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosPriceFilter.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosPriceFilter.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosPriceFilter.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosPriceFilter.Preco.Value).OrderBy(p => p.Preco);
                }
            }

            var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosPriceFilter.PageNumber, produtosPriceFilter.PageSize);
            return produtosFiltrados;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}
