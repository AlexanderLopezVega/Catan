using System;

namespace Project.Game.Core
{
	public class Settlement : IIdentifiable
	{
		//	Constructors
		public Settlement(Guid vertex)
		{
			Vertex = vertex;
		}

		//	Properties

		public Guid Id { get; } = Guid.NewGuid();
		public Guid Vertex { get; }
	}
}