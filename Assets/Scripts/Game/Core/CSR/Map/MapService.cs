using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.Core
{
	internal class MapService
	{
		//	Fields
		private readonly Dictionary<Guid, Tile> _tiles = new();
		private readonly Dictionary<Guid, Vertex> _vertices = new();
		private readonly Dictionary<Guid, Edge> _edges = new();
		private readonly Dictionary<TileCoordinates, Tile> _coordinatesTileMap = new();
		private readonly Dictionary<Vector3Int, Vertex> _coordinatesVertexMap = new();
		private readonly Dictionary<Vector3Int, Edge> _coordinatesEdgeMap = new();

		//	Constructors
		

		//	Methods
		public Tile CreateTile(TileCoordinates coordinates, Resource? resource)
		{
			if (HasTile(coordinates))
				return default;

			Tile tile = new()
			{
				Coordinates = coordinates,
				Resource = resource
			};

			_tiles.Add(tile.Id, tile);
			_coordinatesTileMap.Add(coordinates, tile);

			AddTileVertices(tile);
			AddTileEdges(tile);

			return tile;
		}
		public IEnumerable<Tile> GetTiles() => _tiles.Values;
		public IEnumerable<Vertex> GetVertices() => _vertices.Values;
		public IEnumerable<Edge> GetEdges() => _edges.Values;
		public Vertex GetVertex(Guid vertexId) => _vertices.GetValueOrDefault(vertexId);
		public Edge GetEdge(Guid edgeId) => _edges.GetValueOrDefault(edgeId);
		public Tile GetTileAt(TileCoordinates coordinates) => _coordinatesTileMap.GetValueOrDefault(coordinates);
		public bool HasTile(TileCoordinates coordinates) => _coordinatesTileMap.ContainsKey(coordinates);
		public void LoadMap()
		{
			CreateTile(new TileCoordinates(+0, -2), Resource.Lumber);
			CreateTile(new TileCoordinates(+1, -2), Resource.Grain);
			CreateTile(new TileCoordinates(+2, -2), Resource.Ore);

			CreateTile(new TileCoordinates(-1, -1), Resource.Brick);
			CreateTile(new TileCoordinates(+0, -1), Resource.Wool);
			CreateTile(new TileCoordinates(+1, -1), Resource.Brick);
			CreateTile(new TileCoordinates(+2, -1), Resource.Ore);

			CreateTile(new TileCoordinates(-2, +0), Resource.Ore);
			CreateTile(new TileCoordinates(-1, +0), Resource.Wool);
			CreateTile(new TileCoordinates(+0, +0), null);
			CreateTile(new TileCoordinates(+1, +0), Resource.Grain);
			CreateTile(new TileCoordinates(+2, +0), Resource.Wool);

			CreateTile(new TileCoordinates(-2, +1), Resource.Brick);
			CreateTile(new TileCoordinates(-1, +1), Resource.Lumber);
			CreateTile(new TileCoordinates(+0, +1), Resource.Grain);
			CreateTile(new TileCoordinates(+1, +1), Resource.Lumber);

			CreateTile(new TileCoordinates(-2, +2), Resource.Grain);
			CreateTile(new TileCoordinates(-1, +2), Resource.Lumber);
			CreateTile(new TileCoordinates(+0, +2), Resource.Wool);
		}

		private void AddTileVertices(Tile tile)
		{
			foreach (VertexOffset offset in Enum.GetValues(typeof(VertexOffset)))
			{
				Vertex vertex = GetOrCreateVertex(new(tile.Coordinates, offset));

				tile.Vertices.Add(vertex.Id);
				vertex.Tiles.Add(tile.Id);
			}
		}
		private Vertex GetOrCreateVertex(VertexCoordinates vertexCoordinates)
		{
			Vector3Int computedCoordinates = vertexCoordinates.ToAxialCoordinates();

			Debug.Log($"Vertex coordinates: {vertexCoordinates}, Computed coordinates: {computedCoordinates}");

			if (!_coordinatesVertexMap.TryGetValue(computedCoordinates, out Vertex vertex))
			{
				Debug.Log($"Creating vertex at {vertexCoordinates}");
				vertex = new Vertex(vertexCoordinates);
				_vertices.Add(vertex.Id, vertex);
				_coordinatesVertexMap.Add(computedCoordinates, vertex);
			}

			return vertex;
		}
		private void AddTileEdges(Tile tile)
		{
			foreach (EdgeOffset offset in Enum.GetValues(typeof(EdgeOffset)))
			{
				Edge edge = GetOrCreateEdge(new(tile.Coordinates, offset));

				tile.Edges.Add(edge.Id);
				edge.Tiles.Add(tile.Id);
			}
		}
		private Edge GetOrCreateEdge(EdgeCoordinates edgeCoordinates)
		{
			Vector3Int computedCoordinates = edgeCoordinates.ToAxialCoordinates();

			Debug.Log($"Edge coordinates: {edgeCoordinates}, Computed coordinates: {computedCoordinates}");

			if (!_coordinatesEdgeMap.TryGetValue(computedCoordinates, out Edge edge))
			{
				Debug.Log($"Creating edge at {edgeCoordinates}");
				edge = new Edge(edgeCoordinates);
				_edges.Add(edge.Id, edge);
				_coordinatesEdgeMap.Add(computedCoordinates, edge);
			}

			return edge;
		}
	}
}