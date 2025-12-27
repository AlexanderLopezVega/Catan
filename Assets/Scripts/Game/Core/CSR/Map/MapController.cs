using System;
using System.Collections.Generic;

namespace Project.Game.Core
{
	public class MapController
	{
		//	Fields
		private readonly MapService _service;

		//	Constructors
		internal MapController(MapService service)
		{
			_service = service;
		}

		//	Interface implementations
		public Edge GetEdge(Guid edgeId) => _service.GetEdge(edgeId);
		public IEnumerable<Edge> GetEdges() => _service.GetEdges();
		public IEnumerable<Tile> GetTiles() => _service.GetTiles();
		public Vertex GetVertex(Guid vertexId) => _service.GetVertex(vertexId);
		public IEnumerable<Vertex> GetVertices() => _service.GetVertices();
		public void LoadMap() => _service.LoadMap();
	}
}