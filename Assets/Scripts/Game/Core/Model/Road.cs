using System;

namespace Project.Game.Core
{
	public class Road : IIdentifiable
	{
		//	Constructors
		public Road(Guid edge)
		{
			Edge = edge;
		}

		//	Properties

		public Guid Id { get; } = Guid.NewGuid();
		public Guid Edge { get; }
	}
}