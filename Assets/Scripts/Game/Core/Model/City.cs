using System;

namespace Project.Game.Core
{
	public class City : IIdentifiable
	{
		//	Constructors
		public City(Guid vertex)
		{
			Vertex = vertex;
		}

		//	Properties
		public Guid Id { get; } = Guid.NewGuid();
		public Guid Vertex { get; }
	}
}