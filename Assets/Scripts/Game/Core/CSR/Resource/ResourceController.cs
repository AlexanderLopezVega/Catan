using System;

namespace Project.Game.Core
{
	internal class ResourceController : IResourceService
	{
		//	Fields
		private readonly IResourceInternalService _service;

		//	Constructors
		internal ResourceController(IResourceInternalService service)
		{
			_service = service;
		}

		//	Interface implementations
		event IResourceService.OnResourceStackChangedDelegate IResourceService.OnResourceStackChanged
		{
			add => _service.OnResourceStackChanged += value;
			remove => _service.OnResourceStackChanged -= value;
		}

		void IResourceService.Deregister(Guid playerId) => _service.Deregister(playerId);
		void IResourceService.Register(Guid playerId) => _service.Register(playerId);
		bool IResourceService.TryAdd(Guid playerId, Resource resource, uint amount) => _service.TryAdd(playerId, resource, amount);
		bool IResourceService.TryGet(Guid playerId, Resource resource, out uint amount) => _service.TryGet(playerId, resource, out amount);
	}
}