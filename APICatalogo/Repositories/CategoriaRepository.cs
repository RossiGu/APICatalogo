using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
        {
            var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();
            var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, categoriasParameters.PageNumber, categoriasParameters.PageSize);

            return categoriasOrdenadas;
        }

        public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasNameFilter categoriasNameFilter)
        {
            var categorias = GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(categoriasNameFilter.Nome))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriasNameFilter.Nome));
            }

            var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias,
                                       categoriasNameFilter.PageNumber, categoriasNameFilter.PageSize);

            return categoriasFiltradas;
        }
    }
}
