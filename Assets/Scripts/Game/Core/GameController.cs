namespace Project.Game.Core
{
	public class GameController
	{
		//	Fields
		private readonly DatabaseSystem _database;

		//	Constructors
		public GameController(GameRules gameRules, uint numPlayers = 4)
		{
			_database = new DatabaseSystem();

			BuildController = new(new(_database));
			MapController = new(new(_database));
		}

		//	Methods
		public void Setup()
		{
			_gameKernel.Setup();
		}
		public void Cleanup()
		{
			_gameKernel.Cleanup();
		}

		//	Properties
		public BuildController BuildController { get; }
		public MapController MapController { get; }
	}
}