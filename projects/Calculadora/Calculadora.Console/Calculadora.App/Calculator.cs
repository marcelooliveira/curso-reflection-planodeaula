namespace Calculadora.App
{
    partial class Program
    {
        public static class Calculator
        {
            private static Dictionary<string, Func<double, double, double>> operations = new Dictionary<string, Func<double, double, double>>();

            public static void AddOperation(string operationName, Func<double, double, double> operation)
            {
                operations.Add(operationName, operation);
            }

            public static void Run()
            {
                while (true)
                {
                    Console.WriteLine("\nOperações Disponíveis:");
                    foreach (var operation in operations.Keys)
                    {
                        Console.WriteLine($"- {operation}");
                    }

                    Console.Write("Escolha uma operação (ou 'sair' para encerrar): ");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "sair")
                        break;

                    if (operations.ContainsKey(input))
                    {
                        Console.Write("Digite o primeiro número: ");
                        double x = Convert.ToDouble(Console.ReadLine());

                        Console.Write("Digite o segundo número: ");
                        double y = Convert.ToDouble(Console.ReadLine());

                        double result = operations[input](x, y);
                        Console.WriteLine($"Resultado: {result}");
                    }
                    else
                    {
                        Console.WriteLine("Operação inválida.");
                    }
                }
            }
        }
    }
}