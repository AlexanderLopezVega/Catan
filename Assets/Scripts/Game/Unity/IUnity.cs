using UnityEngine;

namespace Project.Game.Unity
{
	public interface IUnity
	{
		//	Delegates
		delegate void OnUpdateDelegate(float deltaTime);

		//	Events
		event OnUpdateDelegate OnUpdate;

		//	Methods
		GameObject Instantiate(GameObject prefab);
		void Destroy(GameObject instance);
	}
}