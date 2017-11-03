using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using ChannelListManager.Common;
using ChannelListManager.FileModels;

namespace ChannelListManager.ViewModels
{
	internal class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
	{
		private SortCriteria _sortCriteria;
		private string _filterText;
		public ICommand ReadChannelMapFileCommand { get; }
		public ICommand SaveChannelMapFileCommand { get; }
		public ICommand DeleteSelectedCommand { get; }
		public ICommand DeleteUnselectedCommand { get; }
		public ICommand MoveUpCommand { get; }
		public ICommand MoveDownCommand { get; }
		private ChannelMap ChannelMap { get; set; }
		public ObservableCollection<ChannelViewModel> ViewChannels { get; }  = new ObservableCollection<ChannelViewModel>();
		private readonly ObservableCollection<ChannelViewModel> _channels = new ObservableCollection<ChannelViewModel>();
		private string _sidFilter;
		private const string DEFAULT_FILE = @"d:\git\github\philipsChannelListManager\tst\DVBS.9.xml";

		public string FilterText
		{
			get => _filterText;
			set
			{
				SetProperty(ref _filterText, value);
				FilterList(_filterText);
			}
		}

		public string SIDFilter
		{
			get => _sidFilter;
			set
			{
				SetProperty(ref _sidFilter, value);
				FilterSids(_sidFilter);
			}
		}

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
			DeleteUnselectedCommand = new RelayCommand(DeleteUnselected);
			MoveDownCommand = new RelayCommand(MoveDown);
			MoveUpCommand = new RelayCommand(MoveUp);
			ReadFile(DEFAULT_FILE);
		}

		private void MoveUp(object selected)
		{
			var selectedItems = ((IList)selected).Cast<ChannelViewModel>().ToList();

			foreach (var item in selectedItems)
			{
				int oldIndex = ViewChannels.IndexOf(item);
				if (oldIndex <= 0)
					continue;

				ViewChannels.Move(oldIndex, --oldIndex);
			}

			int i = 1;
			foreach (var channel in ViewChannels)
				channel.Number = i++;
		}

		private void MoveDown(object selected)
		{
			var selectedItems = ((IList)selected).Cast<ChannelViewModel>().ToList();

			foreach (var item in selectedItems)
			{
				int oldIndex = ViewChannels.IndexOf(item);
				if (oldIndex >= ViewChannels.IndexOf(ViewChannels.Last()))
					continue;

				ViewChannels.Move(oldIndex, ++oldIndex);
			}

			int i = 1;
			foreach (var channel in ViewChannels)
				channel.Number = i++;
		}

		private void DeleteUnselected(object selected)
		{
			var selectedItems = ((IList)selected).Cast<ChannelViewModel>().ToList();
			_channels.Clear();
			ViewChannels.Clear();
			foreach (var item in selectedItems)
			{
				_channels.Add(item);
				ViewChannels.Add(item);
			}
		}

		private void FilterList(string filterText)
		{
			ViewChannels.Clear();
			foreach (var channel in _channels)
			{
				if (string.IsNullOrWhiteSpace(filterText) ||channel.Name.ToUpperInvariant().Contains(filterText.ToUpperInvariant()))
					ViewChannels.Add(channel);
			}
		}

		private void FilterSids(string sidFilter)
		{
			var sids = sidFilter
				.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToList();

			ViewChannels.Clear();
			foreach (var channel in _channels)
			{
				if (sids.Count == 0 || sids.Contains(channel.SID))
					ViewChannels.Add(channel);
			}
		}

		private void DeleteSelected(object selected)
		{
			var selectedItems = ((IList)selected).Cast<ChannelViewModel>().ToList();
			foreach (var item in selectedItems)
			{
				_channels.Remove(item);
				ViewChannels.Remove(item);
			}
		}

		private void SaveFile()
		{
			var sortedChannels = _channels
				.OrderBy(x => x.Number);

			int i = 1;
			foreach (var channel in sortedChannels)
				channel.Number = i++;

			ChannelMap.Channel = sortedChannels
				.Select(x => x.Channel)
				.ToArray();

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

			_channels.Clear();
			ViewChannels.Clear();
			foreach (var channel in channelMap.Channel)
			{
				var model = new ChannelViewModel(channel);
				_channels.Add(model);
				ViewChannels.Add(model);
			}
		}

		private void ReorderChannels()
		{
			List<ChannelViewModel> orderedItems;
			switch (SortCriteria)
			{
				case SortCriteria.ChannelNumber:
					orderedItems = ViewChannels.OrderBy(x => x.Number).ToList();
					break;
				case SortCriteria.ChannelName:
					orderedItems = ViewChannels.OrderBy(x => x.Name).ToList();
					break;
				case SortCriteria.ServiceType:
					orderedItems = ViewChannels.OrderBy(x => x.ServiceType).ToList();
					break;
				case SortCriteria.SatelliteName:
					orderedItems = ViewChannels.OrderBy(x => x.SatelliteName).ToList();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			ViewChannels.Clear();
			foreach (var item in orderedItems)
				ViewChannels.Add(item);
		}
	}
}
