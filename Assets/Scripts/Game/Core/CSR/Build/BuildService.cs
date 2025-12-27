using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.Core
{
	internal class BuildService
	{
		//	Fields
		private readonly Repository<Settlement> _settlements;
		private readonly Repository<City> _cities;
		private readonly Repository<Road> _roads;
		private readonly Dictionary<Guid, Guid> _vertexLocalityMap = new();
		private readonly Dictionary<Guid, Guid> _edgeRoadMap = new();

		//	Constructors
		internal BuildService(
			Repository<Settlement> settlements,
			Repository<City> cities,
			Repository<Road> roads
		)
		{
			_settlements = settlements;
			_cities = cities;
			_roads = roads;
		}

		//	Delegates
		public delegate void SettlementDelegate(Settlement settlement);
		public delegate void CityDelegate(City city);
		public delegate void RoadDelegate(Road road);

		//	Events
		internal event SettlementDelegate OnSettlementBuilt;
		internal event CityDelegate OnCityBuilt;
		internal event RoadDelegate OnRoadBuilt;
		internal event SettlementDelegate OnSettlementDestroyed;
		internal event CityDelegate OnCityDestroyed;
		internal event RoadDelegate OnRoadDestroyed;

		//	Methods
		internal Settlement GetSettlementAt(Guid vertex)
		{
			Guid settlementId = _vertexLocalityMap.GetValueOrDefault(vertex);

			if (settlementId == default)
				return null;

			return _settlements.Get(settlementId);
		}
		internal City GetCityAt(Guid vertex)
		{
			Guid cityId = _vertexLocalityMap.GetValueOrDefault(vertex);

			if (cityId == default)
				return null;
			
			return _cities.Get(cityId);
		}
		internal Guid GetLocalityAt(Guid vertex) => _vertexLocalityMap.GetValueOrDefault(vertex, default);
		internal Road GetRoadAt(Guid edge)
		{
			if (_edgeRoadMap.TryGetValue(edge, out Guid roadId))
				return _database.Roads.GetOrDefault(roadId);
			else
				return null;
		}
		internal Guid BuildSettlement(Guid vertex)
		{
			Debug.Log($"Building settlement at vertex {vertex}");

			if (GetLocalityAt(vertex) != default)
			{
				Debug.LogWarning("Cannot build at an occupied vertex");
				return default;
			}

			Settlement settlement = new(vertex);
			Guid settlementId = settlement.Id;

			if (!_database.Settlements.TryCreate(settlementId, settlement))
				return default;
			
			_vertexLocalityMap.Add(vertex, settlementId);

			OnSettlementBuilt?.Invoke(settlement);

			return settlementId;
		}
		internal Guid UpgradeSettlementToCity(Guid vertex)
		{
			Debug.Log($"Building city at vertex {vertex}");

			if (!TryDestroySettlementAt(vertex))
				return default;

			City city = new(vertex);
			Guid cityId = city.Id;

			if (!_database.Cities.TryCreate(cityId, city))
				return default;
			
			_vertexLocalityMap.Add(vertex, cityId);

			OnCityBuilt?.Invoke(city);

			return cityId;
		}
		internal Guid BuildRoad(Guid edge)
		{
			Debug.Log($"Building road at edge {edge}");

			if (GetRoadAt(edge) != null)
			{
				Debug.LogWarning($"Cannot build at an occupied edge");
				return default;
			}

			Road road = new(edge);
			Guid roadId = road.Id;

			if (!_database.Roads.TryCreate(roadId, road))
				return default;
			
			_edgeRoadMap.Add(edge, roadId);

			OnRoadBuilt?.Invoke(road);

			return roadId;
		}
		internal bool TryDestroySettlementAt(Guid vertex)
		{
			Settlement settlement;

			Debug.Log($"Destroying settlement at vertex {vertex}");

			if (!_vertexLocalityMap.TryGetValue(vertex, out Guid localityId))
			{
				Debug.LogWarning($"No locality exists at provided vertex");
				return false;
			}
			else if (!_database.Settlements.TryGet(localityId, out settlement))
			{
				Debug.LogWarning($"Locality at provided vertex is not a settlement");
				return false;
			}

			_vertexLocalityMap.Remove(vertex);

			if (!_database.Settlements.TryDelete(settlement.Id))
				return false;

			OnSettlementDestroyed?.Invoke(settlement);

			return true;
		}
		internal bool TryDestroyCityAt(Guid vertex)
		{
			City city;

			Debug.Log($"Destroying city at vertex {vertex}");

			if (!_vertexLocalityMap.TryGetValue(vertex, out Guid localityId))
			{
				Debug.LogWarning("No locality exists at provided vertex");
				return false;
			}
			else if (!_database.Cities.TryGet(localityId, out city))
			{
				Debug.LogWarning("Locality at provided vertex is not a city");
				return false;
			}

			_vertexLocalityMap.Remove(vertex);
			
			if (!_database.Cities.TryDelete(city.Id))
				return false;

			OnCityDestroyed?.Invoke(city);

			return true;
		}
		internal bool TryDestroyRoadAt(Guid edge)
		{
			Road road;

			Debug.Log($"Destroyingroad at edge {edge}");

			if (!_edgeRoadMap.TryGetValue(edge, out Guid roadId))
			{
				Debug.LogWarning("No road exists at provided edge");
				return false;
			}
			else if (!_database.Roads.TryGet(roadId, out road))
			{
				Debug.LogWarning("Road at provided vertex is not a city");
				return false;
			}

			_edgeRoadMap.Remove(edge);


			if (!_database.Roads.TryDelete(road.Id))
				return false;

			OnRoadDestroyed?.Invoke(road);

			return true;
		}
	}
}