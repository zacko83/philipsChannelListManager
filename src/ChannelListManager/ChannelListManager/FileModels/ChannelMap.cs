namespace ChannelListManager.FileModels
{
	[System.Serializable]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(AnonymousType = true)]
	[System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
	public class ChannelMap
	{
		[System.Xml.Serialization.XmlElementAttribute("Channel")]
		public ChannelMapChannel[] Channel { get; set; }
	}
}
