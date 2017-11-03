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
			string fileContent = null;
			var openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() != true)
				return false;

			if (!File.Exists(openFileDialog.FileName))
			{
				MessageBox.Show("File not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			fileContent = File.ReadAllText(openFileDialog.FileName);

			try
			{
				channelMap = Deserialize(fileContent);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private static ChannelMap Deserialize(string fileContent)
		{
			var serializer = new XmlSerializer(typeof(ChannelMap));
			using (var stream = new StringReader(fileContent))
			using (var reader = XmlReader.Create(stream))
			{
				return (ChannelMap)serializer.Deserialize(reader);
			}
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
