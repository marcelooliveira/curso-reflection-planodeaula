using ByteBank.Common;
using System.Reflection;

namespace ByteBank.Library
{
    public class RelatorioDeBoleto : BaseRelatorioDeBoleto
    {
        protected override void GravarArquivo(List<BoletosPorCedente> listaObjetos)
        {
            // Caminho do arquivo CSV
            string caminhoArquivo = $"{typeof(BoletosPorCedente).Name}.csv";

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