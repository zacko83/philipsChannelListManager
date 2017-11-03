namespace ChannelListManager.FileModels
{
	[System.Serializable]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(AnonymousType = true)]
	public class ChannelMapChannelSetup
	{

		private string satelliteNameField;

		private ushort channelNumberField;

		private string channelNameField;

		private byte channelLockField;

		private byte userModifiedNameField;

		private ushort logoIDField;

		private byte userModifiedLogoField;

		private byte logoLockField;

		private byte userHiddenField;

		private byte favoriteNumberField;

		[System.Xml.Serialization.XmlAttribute]
		public string SatelliteName
		{
			get
			{
				return satelliteNameField;
			}
			set
			{
				satelliteNameField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public ushort ChannelNumber
		{
			get
			{
				return channelNumberField;
			}
			set
			{
				channelNumberField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public string ChannelName
		{
			get
			{
				return channelNameField;
			}
			set
			{
				channelNameField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte ChannelLock
		{
			get
			{
				return channelLockField;
			}
			set
			{
				channelLockField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte UserModifiedName
		{
			get
			{
				return userModifiedNameField;
			}
			set
			{
				userModifiedNameField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public ushort LogoID
		{
			get
			{
				return logoIDField;
			}
			set
			{
				logoIDField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte UserModifiedLogo
		{
			get
			{
				return userModifiedLogoField;
			}
			set
			{
				userModifiedLogoField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte LogoLock
		{
			get
			{
				return logoLockField;
			}
			set
			{
				logoLockField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte UserHidden
		{
			get
			{
				return userHiddenField;
			}
			set
			{
				userHiddenField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte FavoriteNumber
		{
			get
			{
				return favoriteNumberField;
			}
			set
			{
				favoriteNumberField = value;
			}
		}

		[System.Xml.Serialization.XmlAttribute]
		public byte Scramble { get; set; }
	}
}
