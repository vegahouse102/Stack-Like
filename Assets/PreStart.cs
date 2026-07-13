using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreStart : MonoBehaviour
{
	[SerializeField]
	private float _time;
	void Start()
	{
		AsyncOperation operation =  SceneManager.LoadSceneAsync("Start");
		operation.allowSceneActivation = false;
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(_time);
		sequence.AppendCallback(() => operation.allowSceneActivation = true);
	}

}
