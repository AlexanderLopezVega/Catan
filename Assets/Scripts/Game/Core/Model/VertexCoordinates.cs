using UnityEngine;

namespace Project.Game.Core
{
	public enum VertexOffset
	{
		Top,
		TopRight,
		BottomRight,
		Bottom,
		BottomLeft,
		TopLeft
	}
	public struct VertexCoordinates
	{
		//	Fields
		public TileCoordinates TileCoordinates;
		public VertexOffset Offset;

		//	Constructors
		public VertexCoordinates(TileCoordinates tileCoordinates, VertexOffset offset)
		{
			TileCoordinates = tileCoordinates;
			Offset = offset;
		}

		//	Methods
		public readonly Vector3Int ToAxialCoordinates()
		{
			int q = TileCoordinates.Q;
			int r = TileCoordinates.R;

			int tx = q * 3;
			int ty = (-q - r) * 3;
			int tz = r * 3;

			Vector3Int offset = Offset switch
			{
				VertexOffset.Top => new Vector3Int(+1, +1, -2),
				VertexOffset.TopRight => new Vector3Int(+2, -1, -1),
				VertexOffset.BottomRight => new Vector3Int(+1, -2, +1),
				VertexOffset.Bottom => new Vector3Int(-1, -1, +2),
				VertexOffset.BottomLeft => new Vector3Int(-2, +1, +1),
				VertexOffset.TopLeft => new Vector3Int(-1, +2, -1),
				_ => default
			};

			return new Vector3Int(tx + offset.x, ty + offset.y, tz + offset.z);
		}
		public Vector3 ToLocalPosition()
		{
			float w = 0.5f;
			float h = 1f / Mathf.Sqrt(3f);
			Vector3 vertexPosition = TileCoordinates.ToLocalPosition();

			vertexPosition += Offset switch
			{
				VertexOffset.Top => new Vector3(0f, 0f, h),
				VertexOffset.TopRight => new Vector3(w, 0f, h / 2f),
				VertexOffset.BottomRight => new Vector3(w, 0f, -h / 2f),
				VertexOffset.Bottom => new Vector3(0f, 0f, -h),
				VertexOffset.BottomLeft => new Vector3(-w, 0f, -h / 2f),
				VertexOffset.TopLeft => new Vector3(-w, 0f, h / 2f),
				_ => default
			} * Tile.Width;

			return vertexPosition;
		}
		public override readonly string ToString()
		{
			return $"[{TileCoordinates}, {Offset}]";
		}
	}
}