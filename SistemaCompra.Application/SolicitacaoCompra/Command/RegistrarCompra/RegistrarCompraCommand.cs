using MediatR;
using System;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
	public class RegistrarCompraCommand : IRequest<bool>
	{
		public string UsuarioSolicitante { get; private set; }
		public string NomeFornecedor { get; private set; }
		public IList<ItemCommand> Itens { get; private set; }
		public DateTime Data { get; private set; }

	}
	public partial class ItemCommand
	{
		public ProdutoCommand Produto { get; set; }
		public int Quantidade { get; set; }

	}

	public partial class ProdutoCommand
	{
		public string Nome { get; set; }
		public string Categoria { get; set; }
		public string Descricao { get; set; }
		public decimal Preco { get; set; }

	}
}