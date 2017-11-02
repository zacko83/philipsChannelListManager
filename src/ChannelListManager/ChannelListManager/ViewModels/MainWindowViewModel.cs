using System.IO;
using System.Windows;
using System.Windows.Input;
using ChannelListManager.Common;
using Microsoft.Win32;

namespace ChannelListManager.ViewModels
{
	public class MainWindowViewModel
	{
		public ICommand ReadChannelMapFileCommand { get; private set; }

		public MainWindowViewModel()
		{
			ReadChannelMapFileCommand = new RelayCommand(() => ReadChannelMapFile());
		}

		private void ReadChannelMapFile()
		{
			if (TryReadFile(out string fileContent))
				return;

		}

		private static bool TryReadFile(out string fileContent)
		{
			fileContent = null;
			var openFileDialog = new OpenFileDialog { DefaultExt = ".xml" };
			if (openFileDialog.ShowDialog() != true)
				return false;

			if (!File.Exists(openFileDialog.FileName))
			{
				MessageBox.Show("File not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return true;
			}

			fileContent = File.ReadAllText(openFileDialog.FileName);

			return false;
		}
	}
}
