using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;

namespace LivrosAPI.Persistence.Repositories
{
    public class ErroRepository : IErroRepository
    {
        private IRepository<Erro> _erroRepository;

        public ErroRepository(BDContext context)
        {
            _erroRepository = new Repository<Erro>(context);
        }

        public async Task InserirErro(Erro erro)
        {
            await _erroRepository.InsertAsync(erro);
        }

        public async Task<List<Erro>> GetListaErro()
        {           
            return await _erroRepository.GetAllAsync();
        }
    }
}
