namespace ChannelListManager.FileModels
{
	[System.Serializable]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(AnonymousType = true)]
	public class ChannelMapChannelSetup
	{
		[System.Xml.Serialization.XmlAttribute]
		public string SatelliteName { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort ChannelNumber { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string ChannelName { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte ChannelLock { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte UserModifiedName { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public ushort LogoID { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte UserModifiedLogo { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte LogoLock { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte UserHidden { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte FavoriteNumber { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public byte Scramble { get; set; }
	}
}
