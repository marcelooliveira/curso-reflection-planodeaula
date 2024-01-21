using System.Globalization;
using System.Reflection;

namespace ByteBank.Common
{
    public class LeitorDeBoleto<Boleto>
    {
        public List<Boleto> ReadCsv(string filePath)
        {
            try
            {
                List<Boleto> records = new List<Boleto>();

                using (var reader = new StreamReader(filePath))
                {
                    string[] header = reader.ReadLine()?.Split(',');

                    while (!reader.EndOfStream)
                    {
                        string[] data = reader.ReadLine()?.Split(',');

                        if (data != null && header != null && data.Length == header.Length)
                        {
                            Boleto record = MapToType(data, header);
                            records.Add(record);
                        }
                    }
                }

                return records;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo CSV: {ex.Message}");
                return new List<Boleto>();
            }
        }

        private Boleto MapToType(string[] data, string[] header)
        {
            Type type = typeof(Boleto);
            Boleto instance = Activator.CreateInstance<Boleto>();

            for (int i = 0; i < header.Length; i++)
            {
                PropertyInfo property = type.GetProperty(header[i]);

                if (property != null)
                {
                    Type propertyType = property.PropertyType;
                    object convertedValue = Convert.ChangeType(data[i], propertyType, CultureInfo.InvariantCulture);
                    property.SetValue(instance, convertedValue);
                }
            }

            return instance;
        }
    }
}
