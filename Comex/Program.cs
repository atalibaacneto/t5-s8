// COMEX
using ComexT2.Modelos;
using System.Text.Json;
string mensagemDeBoasVindas = "\nSeja Bem-vindo(a)!";

var listaPedidos = new List<Pedido>();
var listaDeProdutos = new List<Produto>
{
    new Produto("Notebook")
    {
        Descricao = "Notebook Dell Inspiron",
        Preco_unitario = 3500.00,
        Qtde = 10
    },
    new Produto("Smartphone")
    {
        Descricao = "Smartphone Samsung Galaxy",
        Preco_unitario = 1200.00,
        Qtde = 25
    },
    new Produto("Monitor")
    {
        Descricao = "Monitor LG Ultrawide",
        Preco_unitario = 800.00,
        Qtde = 15
    },
    new Produto("Teclado")
    {
        Descricao = "Teclado Mecânico RGB",
        Preco_unitario = 250.00,
        Qtde = 50
    }
};

void ExibirLogo()
{
    Console.WriteLine(@"
░█████╗░░█████╗░███╗░░░███╗███████╗██╗░░██╗
██╔══██╗██╔══██╗████╗░████║██╔════╝╚██╗██╔╝
██║░░╚═╝██║░░██║██╔████╔██║█████╗░░░╚███╔╝░
██║░░██╗██║░░██║██║╚██╔╝██║██╔══╝░░░██╔██╗░
╚█████╔╝╚█████╔╝██║░╚═╝░██║███████╗██╔╝╚██╗
░╚════╝░░╚════╝░╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚═╝
");
    Console.WriteLine(mensagemDeBoasVindas);
}


//Produto produto = new Produto();
//produto.Nome = "DVD";
//produto.Descricao = "DVD de Música";
//produto.Preco_unitario = 39.90;
//produto.Qtde = 23;

//Console.WriteLine($"Produto: {produto.Nome}");
//Console.WriteLine($"Descrição: {produto.Descricao}");
//Console.WriteLine($"Preço Unitário: R$ {produto.Preco_unitario}");
//Console.WriteLine($"Quantidade: {produto.Qtde}");

async Task MenuDeOpcoes()
{
    Console.Clear();
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para cadastrar produtos");
    Console.WriteLine("Digite 2 para listar produtos cadastrados");
    Console.WriteLine("Digite 3 Consultar API Externa");
    Console.WriteLine("Digite 4 Criar Pedido");
    Console.WriteLine("Digite 5 Listar Pedidos");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    switch (opcaoEscolhidaNumerica)
    {
        case 1:
            CadastrarProduto();
            break;
        case 2:
            ListarProdutos();
            break;
        case 3:
            await ConsultarApiExterna();
            break;
        case 4:
            await CriarPedido();
            break;
        case 5:
            ListarPedidos();
            break;
        case -1: Console.WriteLine("Tchau!");
            break;
        default: Console.WriteLine("Opção Inválida");
            break;
    }
}

async Task CriarPedido()
{
    Console.Clear();
    Console.WriteLine("Criar um novo pedido\n");
    Console.WriteLine("Digite o nome do Cliente: ");
    string nomeCliente = Console.ReadLine()!;
    var cliente = new Cliente();
    cliente.Nome = nomeCliente;

    var pedido = new Pedido(cliente);

    Console.WriteLine("\nProdutos Disponíveis: ");
    for (int i = 0; i < listaDeProdutos.Count; i++)
    {
        Console.WriteLine($"{i + 1} - {listaDeProdutos[i].Nome}");
    }
    Console.WriteLine("Digite o número do produto: ");
    int numeroProduto = int.Parse(Console.ReadLine()!);

    var produto = listaDeProdutos[numeroProduto - 1];
    Console.WriteLine("Digite a Quantidade: ");
    var qtde = int.Parse(Console.ReadLine()!);

    var itemDePedido = new ItemPedido(produto, qtde);
    pedido.AddItem(itemDePedido);
    Console.WriteLine($"{itemDePedido} - Item adicionado com sucesso!");
    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    await MenuDeOpcoes();

}

async Task ListarPedidos()
{
    Console.Clear();
    Console.WriteLine("Listar todos os pedidos");
    var pedidosOrdenados = listaPedidos.OrderBy(p => p.Cliente.Nome).ToList();
    foreach (var pedido in pedidosOrdenados)
    {
        Console.WriteLine($"Pedido: {pedido}");
    }
    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    await MenuDeOpcoes();

}

async Task ConsultarApiExterna()
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Mostrndo produtos da API Externa");
            Console.WriteLine("********************************\n");
            string resposta = await client.GetStringAsync("https://fakestoreapi.com/products");
            var produtos = JsonSerializer.Deserialize<List<Produto>>(resposta);
            foreach (var produto in produtos)
            {
                Console.WriteLine($"\nNome: {produto.Nome}, " +
                    $"\nDescrição: {produto.Descricao}, " +
                    $"\nPreço: {produto.Preco_unitario}");
            }
            //Console.WriteLine(resposta);
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            await MenuDeOpcoes();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro de leitura de API : {ex.Message}");
        }
    }
}

await MenuDeOpcoes();

void CadastrarProduto()
{
    Console.Clear();
    ExibirLogo();
    Console.WriteLine("\nCadastro de Produtos");
    Console.WriteLine("********************");
    Console.Write("\nDigite o Produto para cadastrar: ");
    string nomeProduto = Console.ReadLine();

    var produto = new Produto(nomeProduto);

    Console.WriteLine("\nDigite a descrição do Produto: ");
    string descricaoProduto = Console.ReadLine();
    produto.Descricao = descricaoProduto;

    Console.WriteLine("\nDigite o preço do Produto: ");
    string precoProduto = Console.ReadLine();
    produto.Preco_unitario = double.Parse(precoProduto);

    Console.WriteLine("\nDigite a quantidade do Produto: ");
    string quantidadeProduto = Console.ReadLine();
    produto.Qtde = int.Parse(quantidadeProduto);

    listaDeProdutos.Add(produto);
    Console.WriteLine($"O Produto {produto.Nome} foi registrado " +
        $"com sucesso!"); MenuDeOpcoes();
}
void ListarProdutos()
{
    Console.Clear();
    ExibirLogo();
    Console.WriteLine("\nListar Produtos Cadastrados");
    Console.WriteLine("***************************\n");
    //for (int i = 0; i < listaDeProdutos.Count; i++)
    //{
    //    Console.WriteLine($"Produto: {listaDeProdutos[i]}");
    //}
    foreach (var produto in listaDeProdutos)
    {
        Console.WriteLine($"Produto: {produto}");
    }
    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    Thread.Sleep(2000);
    MenuDeOpcoes();
 }
//Livro biblia = new Livro("Biblia");
//biblia.Isbn = "123456789";
//var identificacaoBiblia = biblia.Identificar();
//Console.WriteLine(identificacaoBiblia);

