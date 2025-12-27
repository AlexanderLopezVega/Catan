namespace Project.Game.Core
{
	public class Stack
	{
		//	Constructors
		public Stack(uint amount = 0)
		{
			Amount = amount;
		}

		//	Properties
		public uint Amount { get; private set; }

		//	Methods
		public void Add(uint amount)
		{
			Amount += amount;
		}
		public bool Remove(uint amount)
		{
			if (amount > Amount)
				return false;
			
			Amount -= amount;
			return true;
		}
	}
}