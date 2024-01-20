using System.Globalization;
using System.Reflection;

namespace ByteBank.ConsoleApp
{
    public class LeitorDeBoleto<T>
    {
        public List<T> ReadCsv(string filePath)
        {
            try
            {
                List<T> records = new List<T>();

                using (var reader = new StreamReader(filePath))
                {
                    string[] header = reader.ReadLine()?.Split(',');

                    while (!reader.EndOfStream)
                    {
                        string[] data = reader.ReadLine()?.Split(',');

                        if (data != null && header != null && data.Length == header.Length)
                        {
                            T record = MapToType(data, header);
                            records.Add(record);
                        }
                    }
                }

                return records;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo CSV: {ex.Message}");
                return new List<T>();
            }
        }

        private T MapToType(string[] data, string[] header)
        {
            Type type = typeof(T);
            T instance = Activator.CreateInstance<T>();

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
