using ComexT2.Modelos;

namespace ComexT2.Tests.Modelos
{
    public class PedidoTests
    {
        [Fact]
        public void PedidoDeveInicializarComClienteEDataCorreta()
        {
            //Arrange
            var cliente = new Cliente { Nome = " Ataliba " };

            //Act
            var pedido = new Pedido(cliente);

            //Assert
            Assert.Equal(cliente, pedido.Cliente);
            Assert.Empty(pedido.Itens);
            Assert.Equal(0, pedido.Total);
        }
        [Theory]
        [InlineData("Produto A", 100.0, 2)]
        [InlineData("Produto B", 200.0, 1)]
        [InlineData("Produto C", 300.0, 3)]
        public void AdicionaItemDeveAdicionarEAtualizarTotla(string nomeProduto, double precoUnitario, int Qtde)
        {
            //Arrange
            var cliente = new Cliente { Nome = "Ataliba" };
            var pedido = new Pedido(cliente);
            var produto = new Produto(nomeProduto) { Preco_unitario = precoUnitario };
            var item = new ItemPedido(produto, Qtde);
            var totalEsperado = precoUnitario * Qtde;

            //Act
            pedido.AddItem(item);

            //Assert
            Assert.Contains(item, pedido.Itens);
            Assert.Equal(totalEsperado, pedido.Total);
        }
        [Fact]
        public void ToStringDeveRetornarStringCorreta()
        {
            //Arrange
            var cliente = new Cliente { Nome = "Ataliba" };
            var pedido = new Pedido(cliente);
            var produto = new Produto("Produto A") { Preco_unitario = 100.0 };
            var item = new ItemPedido(produto, 3);
            pedido.AddItem(item);

            var stringEsperada = $"Cliente: {cliente.Nome}, Data: {pedido.Data}, Total: {pedido.Total:F2}";
            //Act
            var result = pedido.ToString();

            //Assert
            Assert.Equal(stringEsperada, result);
        }
    }
}