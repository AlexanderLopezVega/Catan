using System;
using System.Collections.Generic;

namespace Project.Game.Core
{
	public class Vertex : IIdentifiable
	{
		//	Constructors
		public Vertex(VertexCoordinates coordinates)
		{
			Coordinates = coordinates;
		}

		//	Properties
		public Guid Id { get; } = Guid.NewGuid();
		public VertexCoordinates Coordinates { get; }
		public HashSet<Guid> Tiles { get; } = new();
		public HashSet<Guid> Edges { get; } = new();
	}
}