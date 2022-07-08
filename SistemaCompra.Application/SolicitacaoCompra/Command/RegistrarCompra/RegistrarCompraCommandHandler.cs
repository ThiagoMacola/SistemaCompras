using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
	public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
	{
		private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;

		public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
		{
			solicitacaoCompraRepository = _solicitacaoCompraRepository;
		}

		public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
		{
			var solicitacaoCompra = new Domain.SolicitacaoCompraAggregate.SolicitacaoCompra(request.NomeFornecedor, request.UsuarioSolicitante);
			foreach (var item in request.Itens)
			{
				solicitacaoCompra.AdicionarItem(new Domain.ProdutoAggregate.Produto(item.Produto.Nome,item.Produto.Descricao, item.Produto.Categoria, item.Produto.Preco), item.Quantidade);
			}
			solicitacaoCompra.RegistrarCompra(solicitacaoCompra.Itens);
			_solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);
			
			Commit();
		
			return Task.FromResult(true);
		}
	}
}
