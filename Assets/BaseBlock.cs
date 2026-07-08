using UnityEngine;
using DG.Tweening;
public class BaseBlock : MonoBehaviour
{
	[SerializeField]
	private BlockColor _color;
	[SerializeField]
	private Renderer _baseBaseRenderer;
	private Vector3 _originalPos;

	void Awake()
	{
		_originalPos = transform.position;
		transform.position -= Vector3.up * 50;
		
	}
	private void Start()
	{
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.color = _color.GetColor();
		_baseBaseRenderer.material.SetColor("_TopColor", _color.GetColor());
	}

	public Tween Getup()
	{
		return transform.DOMoveY(_originalPos.y, 0.5f).SetEase(Ease.OutCubic);
	}
}
