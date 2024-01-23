namespace ByteBank.Common
{
    public interface IRelatorioDeBoleto<T>
    {
        void Processar(List<T> boletos);
    }
}