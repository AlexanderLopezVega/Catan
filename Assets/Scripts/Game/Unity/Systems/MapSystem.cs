using Project.Game.Core;
using UnityEngine;

namespace Project.Game.Unity
{
	internal struct MapSystemConfig
	{
		//	Fields
		public IUnity Unity;
		public MapPrefabs Prefabs;
		public Transform TilesParent;
		public Transform VerticesParent;
		public Transform EdgesParent;
	}
    internal class MapSystem
    {
		//	Fields
		private readonly IUnity _unity;
		private readonly MapPrefabs _prefabs;
		private readonly Transform _tilesParent;
		private readonly Transform _verticesParent;
		private readonly Transform _edgesParent;

		//	Constructors
		internal MapSystem(MapSystemConfig config)
		{
			_unity = config.Unity;
			_prefabs = config.Prefabs;
			_tilesParent = config.TilesParent;
			_verticesParent = config.VerticesParent;
			_edgesParent = config.EdgesParent;
		}

		//	Methods
		public void BuildMap(GameController gameController)
		{
			foreach (GameObject child in _tilesParent)
				_unity.Destroy(child);
				
			foreach (GameObject child in _verticesParent)
				_unity.Destroy(child);

			foreach (Tile tile in gameController.MapSystem.GetTiles())
				InstantiateTile(tile);

			foreach (Vertex vertex in gameController.MapSystem.GetVertices())
				InstantiateVertex(vertex);

			foreach (Edge edge in gameController.MapSystem.GetEdges())
				InstantiateEdge(edge);
		}

		private void InstantiateTile(Tile tile)
		{
			GameObject tilePrefab = tile.Resource switch
			{
				Resource.Brick => _prefabs.BrickTilePrefab,
				Resource.Grain => _prefabs.GrainTilePrefab,
				Resource.Lumber => _prefabs.LumberTilePrefab,
				Resource.Ore => _prefabs.OreTilePrefab,
				Resource.Wool => _prefabs.WoolTilePrefab,
				_ => _prefabs.DesertTilePrefab,
			};
			GameObject tileInstance = _unity.Instantiate(tilePrefab);
			Transform tileTransform = tileInstance.transform;
			
			tileTransform.SetParent(_tilesParent);
			tileTransform.position = _tilesParent.TransformPoint(tile.Coordinates.ToLocalPosition());
		}
		private void InstantiateVertex(Vertex vertex)
		{
			GameObject vertexInstance = _unity.Instantiate(_prefabs.VertexPrefab);
			Transform vertexTransform = vertexInstance.transform;
			
			vertexInstance.GetComponent<VertexUO>().Id = vertex.Id;
			vertexTransform.SetParent(_verticesParent);
			vertexTransform.position = _verticesParent.TransformPoint(vertex.Coordinates.ToLocalPosition());
		}
		private void InstantiateEdge(Edge edge)
		{
			GameObject edgeInstance = _unity.Instantiate(_prefabs.EdgePrefab);
			Transform edgeTransform = edgeInstance.transform;
			
			edgeInstance.GetComponent<EdgeUO>().Id = edge.Id;
			edgeTransform.SetParent(_edgesParent);
			edgeTransform.SetPositionAndRotation(
				_edgesParent.TransformPoint(edge.Coordinates.ToLocalPosition()),
				_edgesParent.rotation * Quaternion.Euler(0f, edge.Coordinates.GetRotation(), 0f)
			);
		}
	}
}
