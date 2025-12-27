using System;
using System.Collections.Generic;
using Project.Game.Core;
using UnityEngine;

namespace Project.Game.Unity
{
	internal struct BuildSystemConfig
	{
		//	Fields
		public IUnity Unity;
		public GameController GameController;
		public Transform SettlementsParent;
		public Transform CitiesParent;
		public Transform RoadsParent;
		public BuildPrefabs Prefabs;
	}
	internal class BuildSystem
	{
		//	Fields
		private readonly IUnity _unity;
		private readonly GameController _gameController;
		private readonly Transform _settlementsParent;
		private readonly Transform _citiesParent;
		private readonly Transform _roadsParent;
		private readonly BuildPrefabs _prefabs;
		private readonly Dictionary<Guid, GameObject> _entityInstanceMap;

		//	Constructors
		public BuildSystem(BuildSystemConfig config)
		{
			_unity = config.Unity;
			_gameController = config.GameController;
			_settlementsParent = config.SettlementsParent;
			_citiesParent = config.CitiesParent;
			_roadsParent = config.RoadsParent;
			_prefabs = config.Prefabs;
			_entityInstanceMap = new();
		}

		//	Methods
		public void Setup()
		{
			_gameController.BuildController.OnSettlementBuilt += OnSettlementBuilt;
			_gameController.BuildController.OnCityBuilt += OnCityBuilt;
			_gameController.BuildController.OnRoadBuilt += OnRoadBuilt;
			_gameController.BuildController.OnSettlementDestroyed += OnSettlementDestroyed;
			_gameController.BuildController.OnCityDestroyed += OnCityDestroyed;
			_gameController.BuildController.OnRoadDestroyed += OnRoadDestroyed;
		}
		public void Cleanup()
		{
			_gameController.BuildController.OnSettlementBuilt -= OnSettlementBuilt;
			_gameController.BuildController.OnCityBuilt -= OnCityBuilt;
			_gameController.BuildController.OnRoadBuilt -= OnRoadBuilt;
			_gameController.BuildController.OnSettlementDestroyed -= OnSettlementDestroyed;
			_gameController.BuildController.OnCityDestroyed -= OnCityDestroyed;
			_gameController.BuildController.OnRoadDestroyed -= OnRoadDestroyed;
		}

		private void OnSettlementBuilt(Settlement settlement)
		{
			Debug.Log("Instantiating settlement GameObject");
			
			GameObject settlementInstance = _unity.Instantiate(_prefabs.SettlementPrefab);
			Vertex vertex = _gameController.MapSystem.GetVertex(settlement.Vertex);

			_entityInstanceMap.Add(settlement.Id, settlementInstance);
			settlementInstance.transform.SetParent(_settlementsParent);
			settlementInstance.transform.position = _settlementsParent.TransformPoint(vertex.Coordinates.ToLocalPosition() + 0.2f * Vector3.up);
		}
		private void OnCityBuilt(City city)
		{
			Debug.Log("Instantiating city GameObject");

			GameObject cityInstance = _unity.Instantiate(_prefabs.CityPrefab);
			Vertex vertex = _gameController.MapSystem.GetVertex(city.Vertex);

			_entityInstanceMap.Add(city.Id, cityInstance);
			cityInstance.transform.SetParent(_citiesParent);
			cityInstance.transform.position = _citiesParent.TransformPoint(vertex.Coordinates.ToLocalPosition() + 0.2f * Vector3.up);
		}
		private void OnRoadBuilt(Road road)
		{
			Debug.Log("Instantiating road GameObject");

			GameObject roadInstance = _unity.Instantiate(_prefabs.RoadPrefab);
			Edge edge = _gameController.MapSystem.GetEdge(road.Edge);

			_entityInstanceMap.Add(road.Id, roadInstance);
			roadInstance.transform.SetParent(_roadsParent);
			roadInstance.transform.SetPositionAndRotation(
				_roadsParent.TransformPoint(edge.Coordinates.ToLocalPosition() + Tile.Height * Vector3.up),
				_roadsParent.rotation * Quaternion.Euler(0f, edge.Coordinates.GetRotation(), 0f)
			);
		}
		private void OnSettlementDestroyed(Settlement settlement)
		{
			Debug.Log("Destroying settlement GameObject");
			OnEntityDestroyed(settlement.Id);
		}

		private void OnCityDestroyed(City city)
		{
			Debug.Log("Destroying city GameObject");
			OnEntityDestroyed(city.Id);
		}
		private void OnRoadDestroyed(Road road)
		{
			Debug.Log("Destroying road GameObject");
			OnEntityDestroyed(road.Id);
		}
		private void OnEntityDestroyed(Guid entityId)
		{
			_entityInstanceMap.Remove(entityId, out GameObject settlement);
			_unity.Destroy(settlement);
		}
	}
}