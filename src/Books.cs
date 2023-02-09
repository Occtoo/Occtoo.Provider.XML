using System.Xml.Serialization;

namespace XmlImporter
{
    public class Book
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement("author")]
        public string Author { get; set; } = string.Empty;
        [XmlElement("title")]
        public string Title { get; set; } = string.Empty;
        [XmlElement("genre")]
        public string Genre { get; set; } = string.Empty;
        [XmlElement("price")]
        public string Price { get; set; } = string.Empty;
        [XmlElement("publish_date")]
        public string PublishDate { get; set; } = string.Empty;
        [XmlElement("description")]
        public string Description { get; set; } = string.Empty;
    }

    [XmlRootAttribute("books")]
    public class Books
    {
        [XmlElement("book")]
        public Book[] BookVariants { get; set; } = new Book[0];
    }
}
