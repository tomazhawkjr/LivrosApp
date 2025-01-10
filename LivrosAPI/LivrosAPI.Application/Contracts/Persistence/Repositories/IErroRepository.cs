using LivrosAPI.Application.Responses;

namespace LivrosAPI.Application.Contracts.Persistence.Repositories
{
    public interface IErroRepository
    {
        public Task InserirErro(Erro erro);
        public Task<List<Erro>> GetListaErro();
    }
}
