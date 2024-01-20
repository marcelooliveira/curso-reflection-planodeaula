using Calculadora.Library;
using System;
using System.Reflection;

[assembly: AssemblyTitle("AdvancedOperationsPlugin")]

namespace Calculadora.Plugins
{
    public class AdvancedOperationsPlugin : ICalculatorPlugin
    {
        public string GetPluginName()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
        }

        public double PerformAdvancedOperation(double x, double y)
        {
            Console.WriteLine("Operação avançada sendo executada...");
            // Exemplo de uma operação avançada: multiplicação de x e y
            return x * y;
        }
    }
}
