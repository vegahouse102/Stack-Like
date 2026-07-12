using UnityEngine;

public class SlicedSFX : MonoBehaviour
{
	[SerializeField]
	private AudioSource _sliceSFXAudio;
	public void Sliced(PlaceBlockType blockType)
	{
		if(blockType== PlaceBlockType.Sliced)
		{
			_sliceSFXAudio.Play();
		}
	}
}
