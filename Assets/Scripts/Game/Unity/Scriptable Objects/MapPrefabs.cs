using UnityEngine;

namespace Project.Game.Unity
{
	[CreateAssetMenu(menuName = "Project / Map Prefabs")]
	internal class MapPrefabs : ScriptableObject
	{
		//	Fields
		[field: SerializeField] public GameObject VertexPrefab { get; private set; }
		[field: SerializeField] public GameObject EdgePrefab { get; private set; }
		[field: SerializeField] public GameObject DesertTilePrefab { get; private set; }
		[field: SerializeField] public GameObject BrickTilePrefab { get; private set; }
		[field: SerializeField] public GameObject GrainTilePrefab { get; private set; }
		[field: SerializeField] public GameObject LumberTilePrefab { get; private set; }
		[field: SerializeField] public GameObject OreTilePrefab { get; private set; }
		[field: SerializeField] public GameObject WoolTilePrefab { get; private set; }
	}
}
