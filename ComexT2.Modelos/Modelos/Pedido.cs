namespace ComexT2.Modelos;
/// <summary>
/// Representa um pedido realizado por um cliente
/// </summary>
public class Pedido
{
    /// <summary>
    /// Inicializa uma nova instância da classe Pedido.
    /// </summary>
    /// <param name="cliente">O cliente que realizou o pedido</param>
    public Pedido(Cliente cliente)
    {
        Cliente = cliente;
        Data = DateTime.Now;
        Itens = new List<ItemPedido>();
    }
    /// <summary>
    /// Obtem o cliente que realizou o pedido.
    /// </summary>
    public Cliente Cliente { get; private set; }
    public DateTime Data { get; private set; }
    public List<ItemPedido> Itens { get; private set; }
    public double Total { get; private set; }
    /// <summary>
    /// Add um item ao pedido e atualiza o valor total.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemPedido item)
    {
        Itens.Add(item);
        Total += item.Subtotal;
    }
    /// <summary>
    /// Retorna uma string que representa o pedido atual.
    /// </summary>
    /// <returns>Uma string do pedido atual</returns>
    public override string ToString()
    {
        return $"Cliente: {Cliente.Nome}, Data: {Data}, Total: {Total:F2}";
    }
    //public double Total
    //{
    //    get
    //    {
    //        return Itens.Sum(item => item.Subtotal);
    //    }
    //}
}
