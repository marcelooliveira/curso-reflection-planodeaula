using ByteBank.Common;
using System.Reflection;
using System.Xml;

namespace FornecedorPrimario.Library
{
    public class RelatorioDeBoleto : BaseRelatorioDeBoleto
    {
        protected override void GravarArquivo(List<BoletosPorCedente> listaObjetos)
        {
            // Caminho do arquivo XML
            string caminhoArquivo = $"{typeof(BoletosPorCedente).Name}.xml";

            // Usar Reflection para obter propriedades do tipo genérico
            PropertyInfo[] objetoProperties = typeof(BoletosPorCedente).GetProperties();

            // Escrever os dados no arquivo XML
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            using (XmlWriter xmlWriter = XmlWriter.Create(caminhoArquivo, settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("BoletosPorCedentes"); // Root element

                // Escrever dados
                foreach (var objeto in listaObjetos)
                {
                    xmlWriter.WriteStartElement("BoletoPorCedente"); // Element for each BoletosPorCedente object

                    foreach (var property in objetoProperties)
                    {
                        xmlWriter.WriteStartElement(property.Name); // Element for each property

                        // Escrever valor da propriedade como texto
                        xmlWriter.WriteString(property.GetValue(objeto)?.ToString() ?? "");

                        xmlWriter.WriteEndElement(); // End property element
                    }

                    xmlWriter.WriteEndElement(); // End BoletosPorCedente element
                }

                xmlWriter.WriteEndElement(); // End root element
                xmlWriter.WriteEndDocument();
            }

            Console.WriteLine($"Arquivo '{caminhoArquivo}' criado com sucesso!");
        }
    }
}