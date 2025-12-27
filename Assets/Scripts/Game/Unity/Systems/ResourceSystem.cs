using System;
using Project.Game.Core;
using TMPro;

namespace Project.Game.Unity
{
	internal struct ResourceSystemConfig
	{
		//	Fields
		public GameController GameController;
		public  TextMeshProUGUI BrickAmountLabel;
		public  TextMeshProUGUI GrainAmountLabel;
		public  TextMeshProUGUI LumberAmountLabel;
		public  TextMeshProUGUI OreAmountLabel;
		public  TextMeshProUGUI WoolAmountLabel;
	}
	internal class ResourceSystem
	{
		//	Fields
		private readonly GameController _gameController;
		private readonly TextMeshProUGUI _brickAmountLabel;
		private readonly TextMeshProUGUI _grainAmountLabel;
		private readonly TextMeshProUGUI _lumberAmountLabel;
		private readonly TextMeshProUGUI _oreAmountLabel;
		private readonly TextMeshProUGUI _woolAmountLabel;

		//	Constructors
		public ResourceSystem(ResourceSystemConfig config)
		{
			_gameController = config.GameController;
			_brickAmountLabel = config.BrickAmountLabel;
			_grainAmountLabel = config.GrainAmountLabel;
			_lumberAmountLabel = config.LumberAmountLabel;
			_oreAmountLabel = config.OreAmountLabel;
			_woolAmountLabel = config.WoolAmountLabel;
		}

		//	Methods
		public void Setup()
		{
			_gameController.ResourceSystem.OnResourceStackChanged += OnResourceStackChanged;
		}
		public void Cleanup()
		{
			_gameController.ResourceSystem.OnResourceStackChanged -= OnResourceStackChanged;
		}

		private void OnResourceStackChanged(Guid playerId, Resource resource, uint previousAmount, uint newAmount)
		{
			TextMeshProUGUI label = resource switch
			{
				Resource.Brick => _brickAmountLabel,
				Resource.Grain => _grainAmountLabel,
				Resource.Lumber => _lumberAmountLabel,
				Resource.Ore => _oreAmountLabel,
				Resource.Wool => _woolAmountLabel,
				_ => null
			};

			label.text = $"{newAmount}";
		}
	}
}