using System.Reflection;

namespace ByteBank.Common
{
    public class RelatorioDeBoleto : BaseRelatorioDeBoleto
    {
        private readonly string nomeArquivoSaida;
        private readonly DateTime dataRelatorio = DateTime.Now;

        //public RelatorioDeBoleto(string nomeArquivoSaida, DateTime dataRelatorio)
        //{
        //    this.nomeArquivoSaida = nomeArquivoSaida;
        //    this.dataRelatorio = dataRelatorio;
        //}

        //public RelatorioDeBoleto(DateTime dataRelatorio)
        //{
        //    this.dataRelatorio = dataRelatorio;
        //}

        public RelatorioDeBoleto(string nomeArquivoSaida)
        {
            this.nomeArquivoSaida = nomeArquivoSaida;
        }

        protected override void GravarArquivo(List<BoletosPorCedente> listaObjetos)
        {
            // Caminho do arquivo CSV
            string caminhoArquivo = nomeArquivoSaida;

            // Usar Reflection para obter propriedades do tipo genérico
            PropertyInfo[] objetoProperties = typeof(BoletosPorCedente).GetProperties();

            // Escrever os dados no arquivo CSV
            using (StreamWriter sw = new StreamWriter(caminhoArquivo))
            {
                // Escrever cabeçalho
                var headers = objetoProperties
                    .Select(p => p.GetCustomAttribute<NomeColunaAttribute>()?.Header ?? p.Name);
                sw.WriteLine(string.Join(',', headers));

                // Escrever dados
                foreach (var objeto in listaObjetos)
                {
                    var values = objetoProperties
                        .Select(p => p.GetValue(objeto));
                    sw.WriteLine(string.Join(',', values));
                }
            }

            Console.WriteLine($"Arquivo '{caminhoArquivo}' criado com sucesso!");
        }
    }
}