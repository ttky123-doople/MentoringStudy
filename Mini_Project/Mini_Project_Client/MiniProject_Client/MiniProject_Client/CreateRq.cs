using System.Xml.Serialization;

namespace MiniProject_Client
{
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Root)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "Client")]
	public class Client
	{

		[XmlElement(ElementName = "IP")]
		public string IP { get; set; }

		[XmlElement(ElementName = "Port")]
		public string Port { get; set; }
	}

	[XmlRoot(ElementName = "Student")]
	public class Student
	{

		[XmlElement(ElementName = "Number")]
		public string Number { get; set; }

		[XmlElement(ElementName = "Phone")]
		public string Phone { get; set; }

		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "Data")]
	public class Data
	{

		[XmlElement(ElementName = "Student")]
		public Student Student { get; set; }
	}

	[XmlRoot(ElementName = "Content")]
	public class Content
	{

		[XmlElement(ElementName = "Command")]
		public string Command { get; set; }

		[XmlElement(ElementName = "Data")]
		public Data Data { get; set; }
	}

	[XmlRoot(ElementName = "Root")]
	public class Root
	{

		[XmlElement(ElementName = "Client")]
		public Client Client { get; set; }

		[XmlElement(ElementName = "Content")]
		public Content Content { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "xsd")]
		public string Xsd { get; set; }

		[XmlText]
		public string Text { get; set; }
	}


}
