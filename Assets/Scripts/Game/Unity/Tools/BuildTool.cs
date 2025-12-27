using Project.Game.Core;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Project.Game.Unity
{
	internal enum BuildMode
	{
		Settlement,
		City,
		Road,
	}
	internal class BuildTool
	{
		//	Fields
		private readonly GameController _gameController;
		private readonly Button _settlementModeButton;
		private readonly Button _cityModeButton;
		private readonly Button _roadModeButton;
		private readonly Input _input;
		private readonly Camera _camera;
		private Vector2 _cursorPosition;

		//	Constructors
		internal BuildTool(
			GameController gameController,
			Button settlementModeButton,
			Button cityModeButton,
			Button roadModeButton,
			Input input,
			Camera camera
		)
		{
			_gameController = gameController;
			_settlementModeButton = settlementModeButton;
			_cityModeButton = cityModeButton;
			_roadModeButton = roadModeButton;
			_input = input;
			_camera = camera;
		}

		//	Properties
		public BuildMode Mode { get; set; }

		//	Methods
		internal void Setup()
		{
			_input.OnPoint += OnPoint;
			_input.OnClick += OnClick;
			_settlementModeButton.onClick.AddListener(SetSettlementMode);
			_cityModeButton.onClick.AddListener(SetCityMode);
			_roadModeButton.onClick.AddListener(SetRoadMode);
		}
		internal void Cleanup()
		{
			_input.OnPoint -= OnPoint;
			_input.OnClick -= OnClick;
			_settlementModeButton.onClick.RemoveListener(SetSettlementMode);
			_cityModeButton.onClick.RemoveListener(SetCityMode);
			_roadModeButton.onClick.RemoveListener(SetRoadMode);
		}

		private void OnClick(CallbackContext context)
		{
			if (!context.ReadValueAsButton())
				return;

			if (!Physics.Raycast(_camera.ScreenPointToRay(_cursorPosition), out RaycastHit hit))
				return;

			if (!hit.collider.TryGetComponent(out RootRef rootRef))
				return;

			if (rootRef.Root.TryGetComponent(out VertexUO vertexUO))
			{
				switch (Mode)
				{
					case BuildMode.Settlement: _gameController.BuildController.BuildSettlement(vertexUO.Id); break;
					case BuildMode.City: _gameController.BuildController.UpgradeSettlementToCity(vertexUO.Id); break;
				}
			}
			else if (rootRef.Root.TryGetComponent(out EdgeUO edgeUO) && Mode == BuildMode.Road)
				_gameController.BuildController.BuildRoad(edgeUO.Id);
		}
		private void OnPoint(CallbackContext context)
		{
			_cursorPosition = context.ReadValue<Vector2>();
		}
		private void SetSettlementMode() => Mode = BuildMode.Settlement;
		private void SetCityMode() => Mode = BuildMode.City;
		private void SetRoadMode() => Mode = BuildMode.Road;
	}
}
