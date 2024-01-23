using ByteBank.Common;
using System.Reflection;
using System.Xml;

namespace ByteBank.Library
{
    public class RelatorioDeBoleto : IRelatorioDeBoleto<Boleto>
    {
        public void Processar(List<Boleto> boletos)
        {
            var boletosPorCedenteList = PegaBoletosAgrupados(boletos);

            GravarArquivo(boletosPorCedenteList);
        }

        private void GravarArquivo(List<BoletosPorCedente> listaObjetos)
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

        private List<BoletosPorCedente> PegaBoletosAgrupados(List<Boleto> boletos)
        {
            // Agrupar boletos por cedente
            var boletosAgrupados = boletos.GroupBy(b => new
            {
                b.CedenteNome,
                b.CedenteCpfCnpj,
                b.CedenteAgencia,
                b.CedenteConta
            });

            // Lista para armazenar instâncias de BoletosPorCedente
            List<BoletosPorCedente> boletosPorCedenteList = new List<BoletosPorCedente>();

            // Iterar sobre os grupos de boletos por cedente
            foreach (var grupo in boletosAgrupados)
            {
                // Criar instância de BoletosPorCedente
                BoletosPorCedente boletosPorCedente = new BoletosPorCedente
                {
                    CedenteNome = grupo.Key.CedenteNome,
                    CedenteCpfCnpj = grupo.Key.CedenteCpfCnpj,
                    CedenteAgencia = grupo.Key.CedenteAgencia,
                    CedenteConta = grupo.Key.CedenteConta,
                    Valor = grupo.Sum(b => b.Valor),
                    Quantidade = grupo.Count()
                };

                // Adicionar à lista
                boletosPorCedenteList.Add(boletosPorCedente);
            }

            return boletosPorCedenteList;
        }
    }
}