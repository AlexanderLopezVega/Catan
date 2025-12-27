namespace Project.Game.Core
{
	public class ResourceStacks
	{
		//	Constructors
		public ResourceStacks(
			Stack brick = null,
			Stack grain = null,
			Stack lumber = null,
			Stack ore = null,
			Stack wool = null
		)
		{
			Brick = brick ?? new();
			Grain = grain ?? new();
			Lumber = lumber ?? new();
			Ore = ore ?? new();
			Wool = wool ?? new();
		}

		//	Properties
		internal Stack Brick { get; }
		internal Stack Grain { get; }
		internal Stack Lumber { get; }
		internal Stack Ore { get; }
		internal Stack Wool { get; }
		
		//	Indexers
		internal Stack this[Resource resource] => resource switch
		{
			Resource.Brick => Brick,
			Resource.Grain => Grain,
			Resource.Lumber => Lumber,
			Resource.Ore => Ore,
			Resource.Wool => Wool,
			_ => null
		};
	}
}