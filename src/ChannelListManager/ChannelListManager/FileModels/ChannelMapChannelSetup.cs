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
		public int ChannelNumber { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string ChannelName { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ChannelLock { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int UserModifiedName { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int LogoID { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int UserModifiedLogo { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int LogoLock { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int UserHidden { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int FavoriteNumber { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Scramble { get; set; }
	}
}
