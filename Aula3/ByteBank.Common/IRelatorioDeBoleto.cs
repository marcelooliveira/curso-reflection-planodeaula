namespace ByteBank.Common
{
    public interface IRelatorioDeBoleto<T>
    {
        void SalvarBoletosPorCedente(List<T> boletos);
    }
}