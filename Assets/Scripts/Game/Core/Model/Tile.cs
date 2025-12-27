using System;
using System.Collections.Generic;

namespace Project.Game.Core
{
	public class Tile : IIdentifiable
	{
		//	Constants
		public const float Width = 6f;
		public const float Height = 0.2f;

		//	Properties
		public Guid Id { get; } = Guid.NewGuid();
		public TileCoordinates Coordinates { get; set; }
		public Resource? Resource { get; set; }
		public HashSet<Guid> Vertices { get; } = new();
		public HashSet<Guid> Edges { get; } = new();
	}
}