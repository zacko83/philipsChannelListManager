using StructureMap;

namespace ChannelListManager.Common
{
	internal static class ContainerFactory
	{
		public static IContainer GetIocContainer()
		{
			return new Container(x =>
			{
				
			});
		}
	}
}
