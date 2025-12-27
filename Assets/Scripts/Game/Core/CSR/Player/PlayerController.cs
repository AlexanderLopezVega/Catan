using System;

namespace Project.Game.Core
{
	internal class PlayerController : IPlayerService
	{
		//	Fields
		private readonly IPlayerInternalService _service;

		//	Constructors
		internal PlayerController(IPlayerInternalService service)
		{
			_service = service;
		}

		//	Interface implementations
		Guid IPlayerService.GetCurrentPlayer() => _service.GetCurrentPlayer();
		void IPlayerService.NextPlayer() => _service.NextPlayer();
	}
}