using UnityEngine;

namespace Project.Game.Core
{
	public enum EdgeOffset
	{
		TopRight,
		Right,
		BottomRight,
		BottomLeft,
		Left,
		TopLeft
	}
	public struct EdgeCoordinates
	{
		//	Constants
		private static readonly float AxisRotation = 60f;

		//	Fields
		public TileCoordinates TileCoordinates;
		public EdgeOffset Offset;

		//	Constructors
		public EdgeCoordinates(TileCoordinates tileCoordinates, EdgeOffset offset)
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
				EdgeOffset.TopRight => new Vector3Int(+2, 0, -2),
				EdgeOffset.Right => new Vector3Int(+2, -2, 0),
				EdgeOffset.BottomRight => new Vector3Int(0, -2, +2),
				EdgeOffset.BottomLeft => new Vector3Int(-2, 0, +2),
				EdgeOffset.Left => new Vector3Int(-2, +2, 0),
				EdgeOffset.TopLeft => new Vector3Int(0, +2, -2),
				_ => default
			};

			return new Vector3Int(tx + offset.x, ty + offset.y, tz + offset.z);
		}
		public Vector3 ToLocalPosition()
		{
			float w = 0.5f;
			float h = 1f / Mathf.Sqrt(3f);

			return TileCoordinates.ToLocalPosition() + Offset switch
			{
				EdgeOffset.TopRight => new Vector3(w / 2f, 0f, 3f * h / 4f),
				EdgeOffset.Right => new Vector3(w, 0f, 0f),
				EdgeOffset.BottomRight => new Vector3(w / 2f, 0f, -3f * h / 4f),
				EdgeOffset.BottomLeft => new Vector3(-w / 2f, 0f, -3f * h / 4f),
				EdgeOffset.Left => new Vector3(-w, 0f, 0f),
				EdgeOffset.TopLeft => new Vector3(-w / 2f, 0f, 3f * h / 4f),
				_ => default
			} * Tile.Width;
		}
		public readonly float GetRotation() => Offset switch
		{
			EdgeOffset.TopRight or EdgeOffset.BottomLeft => -AxisRotation,
			EdgeOffset.Right or EdgeOffset.Left => 0f,
			EdgeOffset.BottomRight or EdgeOffset.TopLeft => +AxisRotation,
			_ => 0f
		};
		public override readonly string ToString()
		{
			return $"[{TileCoordinates}, {Offset}]";
		}
	}
}
