using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters)
        {
            var categorias = await GetAllAsync();
            var categoriasOrdenadas = categorias.OrderBy(p => p.CategoriaId).AsQueryable();
            var resultado = await categoriasOrdenadas.ToPagedListAsync(categoriasParameters.PageNumber, categoriasParameters.PageSize);

            return resultado;
        }

        public async Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasNameFilter categoriasNameFilter)
        {
            var categorias = await GetAllAsync();

            if (!string.IsNullOrEmpty(categoriasNameFilter.Nome))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriasNameFilter.Nome));
            }

            var categoriasFiltradas = await categorias.ToPagedListAsync(categoriasNameFilter.PageNumber, categoriasNameFilter.PageSize);

            return categoriasFiltradas;
        }
    }
}
