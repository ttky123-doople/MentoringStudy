using System.Collections.Generic;
using System.Xml.Serialization;

namespace ResponseList
{
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
		public List<Student> Student { get; set; }

		public Data() { Student = new List<Student>(); }
	}

	[XmlRoot(ElementName = "Root")]
	public class Root
	{

		[XmlElement(ElementName = "Response")]
		public string Response { get; set; }

		[XmlElement(ElementName = "Data")]
		public Data Data { get; set; }

		public Root(){ Data = new Data(); }
	}
}
