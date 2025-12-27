using static Project.Game.Unity.Controls;
using static UnityEngine.InputSystem.InputAction;

namespace Project.Game.Unity
{
    public class Input : IUIActions
    {
		//	Fields
		private readonly Controls _controls = new();

		//	Delegates
		public delegate void OnInputDelegate(CallbackContext context);

		//	Events
		public event OnInputDelegate OnCancel;
		public event OnInputDelegate OnClick;
		public event OnInputDelegate OnMiddleClick;
		public event OnInputDelegate OnNavigate;
		public event OnInputDelegate OnPoint;
		public event OnInputDelegate OnRightClick;
		public event OnInputDelegate OnScrollWheel;
		public event OnInputDelegate OnSubmit;
		public event OnInputDelegate OnTrackedDeviceOrientation;
		public event OnInputDelegate OnTrackedDevicePosition;

		//	Interface implementations
		void IUIActions.OnCancel(CallbackContext context) => OnCancel?.Invoke(context);
		void IUIActions.OnClick(CallbackContext context) => OnClick?.Invoke(context);
		void IUIActions.OnMiddleClick(CallbackContext context) => OnMiddleClick?.Invoke(context);
		void IUIActions.OnNavigate(CallbackContext context) => OnNavigate?.Invoke(context);
		void IUIActions.OnPoint(CallbackContext context) => OnPoint?.Invoke(context);
		void IUIActions.OnRightClick(CallbackContext context) => OnRightClick?.Invoke(context);
		void IUIActions.OnScrollWheel(CallbackContext context) => OnScrollWheel?.Invoke(context);
		void IUIActions.OnSubmit(CallbackContext context) => OnSubmit?.Invoke(context);
		void IUIActions.OnTrackedDeviceOrientation(CallbackContext context) => OnTrackedDeviceOrientation?.Invoke(context);
		void IUIActions.OnTrackedDevicePosition(CallbackContext context) => OnTrackedDevicePosition?.Invoke(context);

		//	Methods
		public void Setup()
		{
			_controls.UI.SetCallbacks(this);
			_controls.UI.Enable();
		}
        public void Cleanup()
		{
			_controls.UI.SetCallbacks(null);
			_controls.UI.Disable();
		}
	}
}
