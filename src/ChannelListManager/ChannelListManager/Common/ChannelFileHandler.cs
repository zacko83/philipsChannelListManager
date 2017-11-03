using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using ChannelListManager.FileModels;
using Microsoft.Win32;

namespace ChannelListManager.Common
{
	internal sealed class ChannelFileHandler
	{
		public static bool TryReadFile(out ChannelMap channelMap)
		{
			channelMap = null;
			var openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() != true)
				return false;

			if (File.Exists(openFileDialog.FileName))
				return TryDeserialize(openFileDialog.FileName, out channelMap);

			MessageBox.Show("File not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return false;
		}

		public static bool TryDeserialize(string filePath, out ChannelMap channelMap)
		{
			channelMap = null;
			try
			{
				var fileContent = File.ReadAllText(filePath);

				var serializer = new XmlSerializer(typeof(ChannelMap));
				using (var stream = new StringReader(fileContent))
				using (var reader = XmlReader.Create(stream))
					channelMap = (ChannelMap) serializer.Deserialize(reader);
			}
			catch (Exception e)
			{
				MessageBox.Show($"Error reading file: {e}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			return true;
		}

		public static void SaveFile(ChannelMap channelMap)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			if (dialog.ShowDialog() != true)
				return;

			var serializer = new XmlSerializer(typeof(ChannelMap));
			var settings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = true,
				IndentChars = "\t",
				NewLineHandling = NewLineHandling.None,
				CheckCharacters = false,
				Encoding = Encoding.UTF8
			};
			var ns = new XmlSerializerNamespaces();
			ns.Add("", "");

			var builder = new StringBuilder();
			using (var stream = new StringWriter(builder))
			using (var writer = XmlWriter.Create(stream, settings))
			{
				serializer.Serialize(writer, channelMap, ns);
				var output = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + builder;
				File.WriteAllText(dialog.FileName, output);
			}
		}
	}
}
