using System;

namespace Project.Game.Core
{
	public class BuildController
	{
		//	Fields
		private readonly BuildService _service;

		//	Constructors
		internal BuildController(BuildService service)
		{
			_service = service;
		}

		//	Interface implementations
		public event BuildService.SettlementDelegate OnSettlementBuilt
		{
			add => _service.OnSettlementBuilt += value;
			remove => _service.OnSettlementBuilt -= value;
		}
		public event BuildService.CityDelegate OnCityBuilt
		{
			add => _service.OnCityBuilt += value;
			remove => _service.OnCityBuilt -= value;
		}
		public event BuildService.RoadDelegate OnRoadBuilt
		{
			add => _service.OnRoadBuilt += value;
			remove => _service.OnRoadBuilt -= value;
		}
		public event BuildService.SettlementDelegate OnSettlementDestroyed
		{
			add => _service.OnSettlementDestroyed += value;
			remove => _service.OnSettlementDestroyed -= value;
		}
		public event BuildService.CityDelegate OnCityDestroyed
		{
			add => _service.OnCityDestroyed += value;
			remove => _service.OnCityDestroyed -= value;
		}
		public event BuildService.RoadDelegate OnRoadDestroyed
		{
			add => _service.OnRoadDestroyed += value;
			remove => _service.OnRoadDestroyed -= value;
		}

		public Guid BuildSettlement(Guid vertex)
		{
			return _service.BuildSettlement(vertex);
		}

		public Guid UpgradeSettlementToCity(Guid vertex) => _service.UpgradeSettlementToCity(vertex);
		public Guid BuildRoad(Guid edge) => _service.BuildRoad(edge);
		public Settlement GetSettlementAt(Guid vertex) => _service.GetSettlementAt(vertex);
		public City GetCityAt(Guid vertex) => _service.GetCityAt(vertex);
		public Guid GetLocalityAt(Guid vertex) => _service.GetLocalityAt(vertex);
		public Road GetRoadAt(Guid edge) => _service.GetRoadAt(edge);
	}
}