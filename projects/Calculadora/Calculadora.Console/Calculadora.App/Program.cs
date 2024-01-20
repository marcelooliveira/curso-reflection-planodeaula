using Calculadora.Library;
using System;
using System.IO;
using System.Reflection;

namespace Calculadora.App
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculadora Simples");

            // Carregar plugins
            string[] pluginFiles = Directory.GetFiles("Plugins", "*.dll");

            foreach (var pluginFile in pluginFiles)
            {
                Assembly pluginAssembly = Assembly.LoadFile(Path.GetFullPath(pluginFile));

                foreach (Type type in pluginAssembly.GetTypes())
                {
                    if (typeof(ICalculatorPlugin).IsAssignableFrom(type))
                    {
                        var plugin = Activator.CreateInstance(type) as ICalculatorPlugin;

                        Console.WriteLine($"Plugin: {plugin.GetPluginName()}");

                        // Adicionar funções avançadas do plugin à calculadora
                        Calculator.AddOperation(plugin.GetPluginName(), plugin.PerformAdvancedOperation);
                    }
                }
            }

            // Executar a calculadora
            Calculator.Run();
        }
    }
}