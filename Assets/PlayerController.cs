using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public UnityEvent OnClick;
	public void ClickHandler(InputAction.CallbackContext callbackContext)
	{
		if (callbackContext.started)
			OnClick?.Invoke();
	}
}
