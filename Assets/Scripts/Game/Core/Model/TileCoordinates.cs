using UnityEngine;

namespace Project.Game.Core
{
	public struct TileCoordinates
	{
		//	Fields
		public int Q;
		public int R;

		//	Constructors
		public TileCoordinates(int q, int r)
		{
			Q = q;
			R = r;
		}

		//	Methods
		public readonly TileCoordinates Move(int q, int r) => new(Q + q, R + r);
		public Vector3 ToLocalPosition() => new Vector3()
		{
			x = Q + R * 0.5f,
			y = 0f,
			z = 1.5f / Mathf.Sqrt(3) * -R
		} * Tile.Width;
		public override readonly string ToString()
		{
			return $"[{Q}, {R}]";
		}
	}
}