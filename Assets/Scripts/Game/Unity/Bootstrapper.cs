using System;
using System.Collections.Generic;
using Project.Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game.Unity
{
	public class Bootstrapper : MonoBehaviour, IUnity
	{
		//	Inspector
		[Header("Camera")]
		[SerializeField] private Camera _camera;
		[SerializeField] private Transform _environmentRoot;

		[Header("Map")]
		[SerializeField] private MapPrefabs _mapPrefabs;

		[Header("Build")]
		[SerializeField] private BuildPrefabs _buildPrefabs;
		
		[Header("UI")]
		[SerializeField] private Button _settlementModeButton;
		[SerializeField] private Button _cityModeButton;
		[SerializeField] private Button _roadModeButton;
		[Space]
		[SerializeReference] private TextMeshProUGUI _brickAmountLabel;
		[SerializeReference] private TextMeshProUGUI _grainAmountLabel;
		[SerializeReference] private TextMeshProUGUI _lumberAmountLabel;
		[SerializeReference] private TextMeshProUGUI _oreAmountLabel;
		[SerializeReference] private TextMeshProUGUI _woolAmountLabel;

		//  Fields
		private Input _input;
		private GameController _gameController;
		private BuildTool _buildTool;
		private MapSystem _mapSystem;
		private BuildSystem _buildSystem;
		private ResourceSystem _resourceSystem;

		//	Events
		private event IUnity.OnUpdateDelegate OnUpdate;

		//	Interface implementations
		event IUnity.OnUpdateDelegate IUnity.OnUpdate
		{
			add => OnUpdate += value;
			remove => OnUpdate -= value;
		}

		GameObject IUnity.Instantiate(GameObject prefab) => Instantiate(prefab);
		void IUnity.Destroy(GameObject instance) => Destroy(instance);

		//  Methods
		private void Awake()
		{
			int environmentSiblingIndex = _environmentRoot.GetSiblingIndex();
			GameObject verticesParent = new("Vertices");
			GameObject edgesParent = new("Edges");
			GameObject tilesParent = new("Tiles");
			GameObject settlementsParent = new("Settlements");
			GameObject citiesParent = new("Cities");
			GameObject roadsParent = new("Roads");

			verticesParent.transform.SetSiblingIndex(++environmentSiblingIndex);
			edgesParent.transform.SetSiblingIndex(++environmentSiblingIndex);
			tilesParent.transform.SetSiblingIndex(++environmentSiblingIndex);
			settlementsParent.transform.SetSiblingIndex(++environmentSiblingIndex);
			citiesParent.transform.SetSiblingIndex(++environmentSiblingIndex);
			roadsParent.transform.SetSiblingIndex(++environmentSiblingIndex);

			//	Setup input
			_input = new();

			//	Setup game controller
			_gameController = new GameController(
				new GameRules(
					settlementCost: new ResourceStacks(
						lumber: new(1),
						brick: new(1),
						wool: new(1),
						grain: new(1)
					),
					cityCost: new ResourceStacks(
						wool: new(2),
						ore: new(3)
					)
				)
			);
			_gameController.MapSystem.LoadMap();

			//	Setup Unity dependencies
			_buildTool = new (
				_gameController,
				_settlementModeButton,
				_cityModeButton,
				_roadModeButton,
				_input,
				_camera
			);
			_mapSystem = new(
				new()
				{
					Unity = this,
					Prefabs = _mapPrefabs,
					TilesParent = tilesParent.transform,
					VerticesParent = verticesParent.transform,
					EdgesParent = edgesParent.transform
				}
			);
			_buildSystem = new(
				new()
				{	
					Unity = this,
					GameController = _gameController,
					SettlementsParent = settlementsParent.transform,
					CitiesParent = citiesParent.transform,
					RoadsParent = roadsParent.transform,
					Prefabs = _buildPrefabs
				}
			);
			_resourceSystem = new(
				new()
				{
					GameController = _gameController,
					BrickAmountLabel = _brickAmountLabel,
					GrainAmountLabel = _grainAmountLabel,
					LumberAmountLabel = _lumberAmountLabel,
					OreAmountLabel = _oreAmountLabel,
					WoolAmountLabel = _woolAmountLabel,
				}
			);
		}
		private void Start()
		{
			_input.Setup();
			_gameController.Setup();
			_buildTool.Setup();
			_buildSystem.Setup();
			_resourceSystem.Setup();
			
			_mapSystem.BuildMap(_gameController);

			Guid currentPlayer = _gameController.PlayerSystem.GetCurrentPlayer();
			uint amount = 0;

			foreach (Resource resource in Enum.GetValues(typeof(Resource)))
				_gameController.ResourceSystem.TryAdd(currentPlayer, resource, ++amount);
		}
		private void OnDestroy()
		{
			_input.Cleanup();
			_gameController.Cleanup();
			_buildTool.Cleanup();
			_buildSystem.Cleanup();
			_resourceSystem.Cleanup();
		}
		private void Update() => OnUpdate?.Invoke(Time.deltaTime);
	}
}
