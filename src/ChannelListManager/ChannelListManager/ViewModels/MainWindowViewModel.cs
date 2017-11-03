using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ChannelListManager.Common;
using ChannelListManager.FileModels;

namespace ChannelListManager.ViewModels
{
	internal class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
	{
		private SortCriteria _sortCriteria;
		public ICommand ReadChannelMapFileCommand { get; }
		public ICommand SaveChannelMapFileCommand { get; }
		public ICommand DeleteSelectedCommand { get; }
		private ChannelMap ChannelMap { get; set; }
		public ObservableCollection<ChannelViewModel> Channels { get; }  = new ObservableCollection<ChannelViewModel>();
		private const string DEFAULT_FILE = @"d:\git\github\philipsChannelListManager\tst\DVBS.xml";

		public SortCriteria SortCriteria
		{
			get => _sortCriteria;
			set
			{
				SetProperty(ref _sortCriteria, value);

				ReorderChannels();
			}
		}

		public MainWindowViewModel()
		{
			ReadChannelMapFileCommand = new RelayCommand(() => ReadFile());
			SaveChannelMapFileCommand = new RelayCommand(SaveFile);
			DeleteSelectedCommand = new RelayCommand(DeleteSelected);

			ReadFile(DEFAULT_FILE);
		}

		private void DeleteSelected(object selected)
		{
			var selectedItems = ((IList)selected).Cast<ChannelViewModel>().ToList();
			foreach (var item in selectedItems)
				Channels.Remove(item);
		}

		private void SaveFile()
		{
			ChannelMap.Channel = Channels
				.Select(x => x.Channel)
				.OrderBy(x => x.Setup.ChannelNumber)
				.ToArray();

			ushort i = 1;
			foreach (var channel in ChannelMap.Channel)
			{
				channel.Setup.ChannelNumber = i;
				i++;
			}

			ChannelFileHandler.SaveFile(ChannelMap);
		}

		private void ReadFile(string filePath = null)
		{
			ChannelMap channelMap;
			if (string.IsNullOrWhiteSpace(filePath))
			{
				if (!ChannelFileHandler.TryReadFile(out channelMap))
					return;
			}
			else if (!ChannelFileHandler.TryDeserialize(filePath, out channelMap))
			{
				return;
			}

			ChannelMap = channelMap;

			Channels.Clear();
			foreach (var channel in channelMap.Channel)
				Channels.Add(new ChannelViewModel(channel));
		}

		private void ReorderChannels()
		{
			List<ChannelViewModel> orderedItems;
			switch (SortCriteria)
			{
				case SortCriteria.ChannelNumber:
					orderedItems = Channels.OrderBy(x => x.Number).ToList();
					break;
				case SortCriteria.ChannelName:
					orderedItems = Channels.OrderBy(x => x.Name).ToList();
					break;
				case SortCriteria.ServiceType:
					orderedItems = Channels.OrderBy(x => x.ServiceType).ToList();
					break;
				case SortCriteria.SatelliteName:
					orderedItems = Channels.OrderBy(x => x.SatelliteName).ToList();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			Channels.Clear();
			foreach (var item in orderedItems)
				Channels.Add(item);
		}
	}
}
