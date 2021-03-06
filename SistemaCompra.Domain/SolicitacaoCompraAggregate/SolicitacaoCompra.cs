using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
	public class SolicitacaoCompra : Entity
	{
		public UsuarioSolicitante UsuarioSolicitante { get; private set; }
		public NomeFornecedor NomeFornecedor { get; private set; }
		public IList<Item> Itens { get; private set; }
		public DateTime Data { get; private set; }
		public Money TotalGeral { get; private set; }
		public Situacao Situacao { get; private set; }
		public CondicaoPagamento CondicaoPagamento { get; set; }

		private SolicitacaoCompra() { }

		public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
		{
			Id = Guid.NewGuid();
			UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
			NomeFornecedor = new NomeFornecedor(nomeFornecedor);
			Data = DateTime.Now;
			Situacao = Situacao.Solicitado;
		}

		public void AdicionarItem(Produto produto, int qtde)
		{
			Itens.Add(new Item(produto, qtde));
		}

		public void RegistrarCompra(IEnumerable<Item> itens)
		{	
			foreach (var item in itens)
			{
				var money = item.Subtotal;

				TotalGeral = new Money().Add(new Money(money.Value));
			}
			if (TotalGeral.Value  > 50000)
			{
				CondicaoPagamento = new CondicaoPagamento(30);
			}
		}
	}
}
