using System.Drawing;
using System.Reflection;

namespace ByteBank.Common
{
    public class LeitorDeBoleto
    {
        public List<Boleto> LerBoletos(string caminhoArquivo)
        {
            var boletos = new List<Boleto>();

            using (var reader = new StreamReader(caminhoArquivo))
            {
                string linha = reader.ReadLine();
                string[] cabecalho = linha.Split(',');

                while (!reader.EndOfStream)
                {
                    linha = reader.ReadLine();
                    string[] dados = linha.Split(',');

                    Boleto boleto = MapearTextoParaBoleto(cabecalho, dados);
                    //Boleto boleto = MapearTextoParaObjeto(typeof(Boleto), cabecalho, dados);
                    //Boleto boleto = MapearTextoParaObjeto<Boleto>(typeof(Boleto), cabecalho, dados);
                    boletos.Add(boleto);
                }
            }

            return boletos;
        }

        //private Boleto MapearTextoParaObjeto(Type type, string[] nomesPropriedades, string[] valoresPropriedades)
        ////private Boleto MapearTextoParaObjeto<T>(string[] nomesPropriedades, string[] valoresPropriedades)
        //{
        //    Boleto instancia = Activator.CreateInstance<Boleto>();

        //    for (int i = 0; i < nomesPropriedades.Length; i++)
        //    {
        //        PropertyInfo property = type.GetProperty(nomesPropriedades[i]);

        //        if (property != null)
        //        {
        //            Type propertyType = property.PropertyType;
        //            object convertedValue = Convert.ChangeType(valoresPropriedades[i], propertyType);
        //            property.SetValue(instancia, convertedValue);
        //        }
        //    }

        //    return instancia;
        //}

        private Boleto MapearTextoParaBoleto(string[] nomesPropriedades, string[] valoresPropriedades)
        {
            Boleto instancia = new Boleto();
            instancia.CedenteNome = valoresPropriedades[0];
            instancia.CedenteCpfCnpj = valoresPropriedades[1];
            instancia.CedenteAgencia = valoresPropriedades[2];
            instancia.CedenteConta = valoresPropriedades[3];
            instancia.SacadoNome = valoresPropriedades[4];
            instancia.SacadoCpfCnpj = valoresPropriedades[5];
            instancia.SacadoEndereco = valoresPropriedades[6];
            instancia.Valor = Convert.ToDecimal(valoresPropriedades[7]);
            instancia.DataVencimento = Convert.ToDateTime(valoresPropriedades[8]);
            instancia.NumeroDocumento = valoresPropriedades[9];
            instancia.NossoNumero = valoresPropriedades[10];
            instancia.CodigoBarras = valoresPropriedades[11];
            instancia.LinhaDigitavel = valoresPropriedades[12];
            return instancia;
        }
    }
}
