namespace Project.Game.Core
{
	public readonly struct GameRules
	{
		//	Fields
		public readonly ResourceStacks SettlementCost;
		public readonly ResourceStacks CityCost;

		//	Constructors
		public GameRules(ResourceStacks settlementCost, ResourceStacks cityCost)
		{
			SettlementCost = settlementCost;
			CityCost = cityCost;
		}
	}
}