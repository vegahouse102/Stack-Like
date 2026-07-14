using UnityEngine;

public class QuitGame : MonoBehaviour
{
	public void Quit()
	{
#if UNITY_EDITOR
		Debug.Log("GameQuit");

#else
		Application.Quit();
#endif
	}
}
