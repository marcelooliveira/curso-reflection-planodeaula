namespace ByteBank.Common
{
    public class BoletosPorSacado
    {
        // Informações do Sacado (Pagador)
        public string SacadoNome { get; set; }
        public string SacadoCpfCnpj { get; set; }
        public string SacadoEndereco { get; set; }
        // Totais
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}