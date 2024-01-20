namespace Calculadora.Library
{
    public interface ICalculatorPlugin
    {
        string GetPluginName();
        double PerformAdvancedOperation(double x, double y);
    }
}