﻿namespace APICatalogo.Repositories
{
    public interface IUnityofWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        Task CommitAsync();
    }
}
