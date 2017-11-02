using System.Windows;
using ChannelListManager.Common;
using ChannelListManager.ViewModels;

namespace ChannelListManager
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			var iocContainer = ContainerFactory.GetIocContainer();
			var mainWindowViewModel = iocContainer.GetInstance<MainWindowViewModel>();
			DataContext = mainWindowViewModel;
			InitializeComponent();
		}
	}
}
