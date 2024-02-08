using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        PagedList<Produto> GetProdutosFiltroPreco(ProdutosPriceFilter produtosPriceFilter);
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
    }
}
