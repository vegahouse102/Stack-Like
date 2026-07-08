
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
	private AsyncOperation _asyncOperation;
	private void Awake()
	{
		_asyncOperation =  SceneManager.LoadSceneAsync("SampleScene");
		_asyncOperation.allowSceneActivation = false;
	}
	public void RetryHandler()
	{
		_asyncOperation.allowSceneActivation = true;
	}
}


