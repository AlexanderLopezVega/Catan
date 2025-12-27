using UnityEngine;

namespace Project.Game.Unity
{
	public class RootRef : MonoBehaviour
	{
		//	Inspector
		[field: SerializeField] public Transform Root { get; private set; }
	}
}
