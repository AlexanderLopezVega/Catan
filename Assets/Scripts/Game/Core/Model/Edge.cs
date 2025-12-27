using System;
using System.Collections.Generic;

namespace Project.Game.Core
{
	public class Edge : IIdentifiable
	{
		//	Constructors
		public Edge(EdgeCoordinates coordinates)
		{
			Coordinates = coordinates;
		}

		//	Properties
		public Guid Id { get; } = Guid.NewGuid();
		public EdgeCoordinates Coordinates { get; }
		public HashSet<Guid> Tiles { get; } = new();
		public HashSet<Guid> Edges { get; } = new();
	}
}