using System.Xml.Serialization;

namespace ResponseMsg
{

	[XmlRoot(ElementName = "Root")]
	public class Root
	{

		[XmlElement(ElementName = "Response")]
		public string response { get; set; }
	}
}
