using ByteBank.Common;
using System.Reflection;

MostrarBanner();

while (true)
{
    MostrarMenu();

    if (int.TryParse(Console.ReadLine(), out int escolha))
    {
        ExecutarEscolha(escolha);
    }
    else
    {
        Console.WriteLine("Opção inválida. Tente novamente.");
    }
}

static void MostrarBanner()
{
    Console.WriteLine(@"


    ____        __       ____              __      
   / __ )__  __/ /____  / __ )____ _____  / /__    
  / __  / / / / __/ _ \/ __  / __ `/ __ \/ //_/    
 / /_/ / /_/ / /_/  __/ /_/ / /_/ / / / / ,<       
/_____/\__, /\__/\___/_____/\__,_/_/ /_/_/|_|      
      /____/                                       
                                
        ");
}

static void MostrarMenu()
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine();
    Console.WriteLine("1. Ler arquivo de boletos");
    Console.WriteLine("2. Gravar arquivo com totais de boletos");
    Console.WriteLine("3. Executar Plugins");
    Console.WriteLine();
    Console.Write("Digite o número da opção desejada: ");
}

static void ExecutarEscolha(int escolha)
{
    switch (escolha)
    {
        case 1:
            LerArquivoBoletos();
            break;
        case 2:
            GravarGrupoBoletos();
            break;
        case 3:
            ExecutarPlugins();
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

static void LerArquivoBoletos()
{
    Console.WriteLine("Lendo arquivo de boletos...");

    LeitorDeBoleto<Boleto> leitorDeCSV = new LeitorDeBoleto<Boleto>();
    List<Boleto> boletos = leitorDeCSV.ReadCsv("Boletos.csv");

    // Agora você pode usar a lista de boletos conforme necessário.
    foreach (var boleto in boletos)
    {
        Console.WriteLine($"Cedente: {boleto.CedenteNome}, Valor: {boleto.Valor}, Vencimento: {boleto.DataVencimento}");
    }
}

static void GravarGrupoBoletos()
{

    Console.WriteLine("Gravando arquivo de boletos...");

    LeitorDeBoleto<Boleto> leitorDeCSV = new LeitorDeBoleto<Boleto>();
    List<Boleto> boletos = leitorDeCSV.ReadCsv("Boletos.csv");
    RelatorioDeBoleto gravadorDeCSV = new RelatorioDeBoleto();
    gravadorDeCSV.SalvarBoletosPorCedente(boletos);
}

static void ExecutarPlugins()
{
    Console.WriteLine("Gravando arquivo de boletos...");

    LeitorDeBoleto<Boleto> leitorDeCSV = new LeitorDeBoleto<Boleto>();
    List<Boleto> boletos = leitorDeCSV.ReadCsv("Boletos.csv");

    List<Type> classesDePlugin = ObterClassesDePlugin<IRelatorioDeBoleto<Boleto>>();

    Console.WriteLine("Identificando plugins...");

    foreach (var classe in classesDePlugin)
    {
        Console.WriteLine($"Plugin identificado: {classe}");
        // Criar uma instância do tipo encontrado
        var instancia = Activator.CreateInstance(classe);

        // Verificar se a instância implementa a interface
        if (instancia is IRelatorioDeBoleto<Boleto> relatorio)
        {
            // Chamar o método SalvarBoletosPorCedente usando Reflection
            MethodInfo metodoSalvar = classe.GetMethod("SalvarBoletosPorCedente");
            metodoSalvar.Invoke(relatorio, new object[] { boletos });

            Console.WriteLine($"Método SalvarBoletosPorCedente chamado para o tipo {classe.FullName}");
        }
    }
}

static List<Type> ObterClassesDePlugin<T>()
{
    List<Type> tiposEncontrados = new List<Type>();

    // Carregar o assembly
    //Assembly assembly = Assembly.GetExecutingAssembly();
    Assembly assembly = typeof(Boleto).Assembly;

    // Encontrar tipos que implementam a interface T
    IEnumerable<Type> tiposImplementandoT = assembly.GetTypes()
        .Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

    tiposEncontrados.AddRange(tiposImplementandoT);

    return tiposEncontrados;
}