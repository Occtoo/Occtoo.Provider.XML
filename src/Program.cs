using Occtoo.Onboarding.Sdk;
using System.Reflection;
using System.Xml.Serialization;

namespace XmlImporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "books.xml";

            // deserialize xml
            var books = DeserializeXml(path);

            if (books != null && books.BookVariants.Any())
            {
                // create dynamic entiteis using occtoo onboarding nuget package
                var entities = GetEntitiesToOnboard(books);

                // send the data to specific occtoo datasource via your dataprovider created in the studio.
                OnboardDataToOcctoo(entities, "##DataProviderClientId##", "##DataProviderSecret##", "##OcctooSourceName##");

                Console.WriteLine($"{entities.Count} books got onboarded to Occtoo!");
            }
        }
        public static Books DeserializeXml(string path)
        {
            using (TextReader reader = new StreamReader(path))
            {
                // deserializing
                XmlSerializer serializer = new XmlSerializer(typeof(Books));
                var books = (Books)serializer.Deserialize(reader);
                return books;
            }
        }
        public static List<DynamicEntity> GetEntitiesToOnboard(Books books)
        {
            List<DynamicEntity> entities = new List<DynamicEntity>();
            foreach (var book in books.BookVariants)
            {
                DynamicEntity entity = new DynamicEntity
                {
                    Key = book.Id
                };

                foreach (PropertyInfo propertyInfo in book.GetType().GetProperties())
                {
                    if (propertyInfo.Name != nameof(book.Id))
                    {
                        DynamicProperty property = new DynamicProperty
                        {
                            Id = propertyInfo.Name,
                            Value = propertyInfo.GetValue(book, null)?.ToString()
                        };
                        entity.Properties.Add(property);
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }
        public static void OnboardDataToOcctoo(List<DynamicEntity> entities, string dataProviderClientId, string dataProviderSecret, string occtooSourceName)
        {

            var onboardingServliceClient = new OnboardingServiceClient(dataProviderClientId, dataProviderSecret);
            var response = onboardingServliceClient.StartEntityImport(occtooSourceName, entities);
            if (response.StatusCode != 202)
            {
                throw new Exception($"Batch import was not successful - status code: {response.StatusCode}. {response.Message}");
            }
        }
    }
}