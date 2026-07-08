using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
	[SerializeField]
	List<TextMove> _moveTexts = new();
	AsyncOperation _startGameScene;
	void Start()
	{
		_startGameScene = SceneManager.LoadSceneAsync("SampleScene");
		_startGameScene.allowSceneActivation = false;
		Sequence sequence = DOTween.Sequence();
		foreach(var t in _moveTexts)
		{
			sequence.Join(t.StartMove());
		}
		sequence.AppendCallback(() => StartGameScene());
	}


	private void StartGameScene()
	{
		_startGameScene.allowSceneActivation = true;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
