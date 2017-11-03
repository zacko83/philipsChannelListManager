namespace ChannelListManager.FileModels
{
	[System.Serializable]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(AnonymousType = true)]
	public class ChannelMapChannel
	{
		public ChannelMapChannelSetup Setup { get; set; }

		public ChannelMapChannelBroadcast Broadcast { get; set; }
	}
}
