namespace ChannelListManager.FileModels
{
	[System.Serializable]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(AnonymousType = true)]
	public class ChannelMapChannelBroadcast
	{
		[System.Xml.Serialization.XmlAttribute]
		public byte ChannelType { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort Onid { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort Tsid { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort Sid { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort Frequency { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string Modulation { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte ServiceType { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort SymbolRate { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte LNBNumber { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte Polarization { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte SystemHidden { get; set; }
	}
}
