namespace ComexT2.Modelos;

public class ItemPedido
{
    public ItemPedido(Produto produto, int qtde)
    {
        Produto = produto;
        Qtde = qtde;
        PrecoUnitario = produto.Preco_unitario;
        Subtotal = qtde * produto.Preco_unitario;
    }

    public Produto Produto { get; set; }
    public int  Qtde { get; set; }
    public double PrecoUnitario { get; private set; }
    public double Subtotal { get; private set; }

    public override string ToString()
    {
        return $"Produto: {Produto.Nome}, Quantidade: {Qtde}, " +
            $"Preço Unitário: {PrecoUnitario:F2}, Subtotal: {Subtotal:F2}";
    }
}
