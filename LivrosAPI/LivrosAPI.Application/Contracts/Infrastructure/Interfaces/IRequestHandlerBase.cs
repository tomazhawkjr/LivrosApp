using LivrosAPI.Application.Features.Livro.Commands.AtualizarLivro;
using LivrosAPI.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Application.Contracts.Infrastructure.Interfaces
{
    public interface IRequestHandlerBase<T> : IRequestHandler<T, RetornoService> where T : IRequest<RetornoService>
    {
    }
}
