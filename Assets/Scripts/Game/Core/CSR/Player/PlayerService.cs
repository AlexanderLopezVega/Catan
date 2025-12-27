using System;

namespace Project.Game.Core
{
	public interface IPlayerService
	{
		//	Methods
		Guid GetCurrentPlayer();
		void NextPlayer();
	}
	internal interface IPlayerInternalService : IPlayerService { }
	internal class PlayerService : IPlayerInternalService
	{
		//	Fields
		private readonly GameKernel _kernel;
		private readonly Guid[] _players;
		private uint _currentPlayer;

		//	Constructors
		internal PlayerService(GameKernel kernel, uint numPlayers)
		{
			_kernel = kernel;
			_players = new Guid[numPlayers];
		}

		//	Methods
		public void Setup()
		{
			for (uint i = 0; i < _players.Length; ++i)
			{
				_players[i] = Guid.NewGuid();
				_kernel.ResourceSystem.Register(_players[i]);
			}
		}
		public void Cleanup()
		{
			foreach (Guid player in _players)
				_kernel.ResourceSystem.Deregister(player);
		}
		public Guid GetCurrentPlayer() => _players[_currentPlayer];
		public void NextPlayer()
		{
			_currentPlayer = (_currentPlayer + 1) % (uint)_players.Length;
		}
	}
}