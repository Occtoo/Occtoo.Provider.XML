using System.Xml.Serialization;

namespace XmlImporter
{
    public class Book
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement("author")]
        public string Author { get; set; } = String.Empty;
        [XmlElement("title")]
        public string Title { get; set; } = String.Empty;
        [XmlElement("genre")]
        public string Genre { get; set; } = String.Empty;
        [XmlElement("price")]
        public string Price { get; set; } = String.Empty;
        [XmlElement("publish_date")]
        public string PublishDate { get; set; } = String.Empty;
        [XmlElement("description")]
        public string Description { get; set; } = String.Empty;
    }

    [XmlRootAttribute("books")]
    public class Books
    {
        [XmlElement("book")]
        public Book[] BookVariants { get; set; } = new Book[0];
    }
}
