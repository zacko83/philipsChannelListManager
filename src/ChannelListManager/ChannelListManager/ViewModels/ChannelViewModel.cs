using System;
using System.Linq;
using System.Text;
using ChannelListManager.FileModels;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace ChannelListManager.ViewModels
{
	// ONID: Original Network ID (Ursprungsort eines Transponderstroms); z.B. Astra 1
	// NID ist die Kennzeichnung des Sendernetzwerkes
	// SID: Die Service ID markiert jeden Sender eindeutig. Das bedeutet, jedes TV oder Radio Programm und jeder Datendienst auf einem Transponder hat eine eigene ID. Die Service-ID ist nie doppelt vorhanden. Über die Service-ID werden alle anderen Infos wie EPG und Videotext zugeordnet.
	// TSID: Die ID des Transport Streams.
	internal sealed class ChannelViewModel : ViewModelBase<ChannelViewModel>
	{
		private bool _isSelected;
		private int _favourite;
		public ChannelMapChannel Channel { get; }

		public bool IsSelected
		{
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

		public int Number
		{
			get => Channel.Setup.ChannelNumber;
			set
			{
				Channel.Setup.ChannelNumber = value;
				OnPropertyChanged();
			}
		}

		public string Name => TranslateName(Channel.Setup.ChannelName);
		public string SatelliteName => TranslateName(Channel.Setup.SatelliteName);
		public int SID => Channel.Broadcast.Sid;
		public ServiceType ServiceType => (ServiceType)Channel.Broadcast.ServiceType;

		public int Favourite
		{
			get => Channel.Setup.FavoriteNumber;
			set
			{
				Channel.Setup.FavoriteNumber = value; 
				SetProperty(ref _favourite, value);
			}
		}

		public ChannelViewModel(ChannelMapChannel channel)
		{
			Channel = channel;
		}

		public override string ToString()
		{
			return $"{Channel.Setup.ChannelNumber} -  {TranslateName(Channel.Setup.ChannelName)}";
		}

		private static string TranslateName(string name)
		{
			var edited = name
				.Replace("0x00", "")
				.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => Convert.ToChar(Convert.ToUInt32(x, 16)));

			return string.Join("", edited);
		}

		public static string SetChannelName(string name)
		{
			byte[] toEncodeAsBytes = ASCII.GetBytes(name);
			string returnValue = Convert.ToBase64String(toEncodeAsBytes);

			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < name.Length; i++)
			{
				var ui = (uint)name[i];
				stringBuilder.Append("0x" + ui.ToString("X") + " ");
				stringBuilder.Append("0x00 ");
			}
			var to = 50 - (name.Length * 2) - 1;
			for (int i = 0; i < to; i++)
			{
				stringBuilder.Append("0x00 ");
			}
			stringBuilder.Append("0x00");
			return stringBuilder.ToString();
		}
	}

	internal enum ServiceType
	{
		/*
		SERVICETYPE;Number;Description
SERVICETYPE;01;SD-TV
SERVICETYPE;02;Radio
SERVICETYPE;03;Teletext
SERVICETYPE;07;FM-Radio
SERVICETYPE;10;AAC Radio
SERVICETYPE;12;Data/Test
SERVICETYPE;17;HD-TV
SERVICETYPE;22;SD-TV
SERVICETYPE;25;HD-TV
SERVICETYPE;31;UHD-TV
SERVICETYPE;211;Option
		 */
		SdTv = 1,
		Radio = 2,
		Teletext = 3,
		FmRadio = 7,
		AACRadio = 10,
		Data = 12,
		HdTv = 17,
		SdTv2 = 22,
		HdTv2 = 25,
		UhdTv = 31,
		Option = 221

	}
}
