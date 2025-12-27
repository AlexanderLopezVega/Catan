using UnityEngine;

namespace Project.Game.Unity
{
	[CreateAssetMenu(menuName = "Project / Build Prefabs")]
	internal class BuildPrefabs : ScriptableObject
	{
		//	Fields
		[field: SerializeField] public GameObject SettlementPrefab { get; private set; }
		[field: SerializeField] public GameObject CityPrefab { get; private set; }
		[field: SerializeField] public GameObject RoadPrefab { get; private set; }
	}
}
