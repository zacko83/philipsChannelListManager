namespace ChannelListManager.FileModels
{
	[System.Serializable]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(AnonymousType = true)]
	public class ChannelMapChannelBroadcast
	{
		[System.Xml.Serialization.XmlAttribute]
		public int ChannelType { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Onid { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Tsid { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Sid { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Frequency { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string Modulation { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ServiceType { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int SymbolRate { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int LNBNumber { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Polarization { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int SystemHidden { get; set; }
	}
}
