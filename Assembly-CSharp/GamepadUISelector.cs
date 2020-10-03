using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

// Token: 0x0200013A RID: 314
public class GamepadUISelector : MonoBehaviour
{
	// Token: 0x06000838 RID: 2104 RVA: 0x00031224 File Offset: 0x0002F424
	private void OnEnable()
	{
		if (this.startOnEnable)
		{
			if (this.delaySelection)
			{
				base.Invoke("SetSelection", 1f);
				return;
			}
			this.SetSelection();
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00031250 File Offset: 0x0002F450
	public void SetSelection()
	{
		GamepadUISelector.instance = this;
		if (XRDevice.isPresent)
		{
			return;
		}
		if (this.eventSystem)
		{
			this.eventSystem.SetSelectedGameObject(null);
		}
		else
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
		if (this.selectByDefault)
		{
			if (this.eventSystem)
			{
				this.eventSystem.SetSelectedGameObject(this.selectedObject);
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(this.selectedObject);
			}
		}
		if (MainManager.instance)
		{
			if (MainManager.instance.localPlayer && MainManager.instance.localPlayer.playerInput.currentControlScheme == "Gamepad")
			{
				EventSystem.current.SetSelectedGameObject(this.selectedObject);
			}
		}
		else if (GameController.instance && GameController.instance.myPlayer != null && GameController.instance.myPlayer.player.playerInput.currentControlScheme == "Gamepad")
		{
			EventSystem.current.SetSelectedGameObject(this.selectedObject);
		}
		for (int i = 0; i < this.objectsToDisable.Length; i++)
		{
			this.objectsToDisable[i].SetActive(false);
		}
		for (int j = 0; j < this.objectsToEnable.Length; j++)
		{
			this.objectsToEnable[j].SetActive(true);
		}
	}

	// Token: 0x04000851 RID: 2129
	public static GamepadUISelector instance;

	// Token: 0x04000852 RID: 2130
	[SerializeField]
	private GameObject selectedObject;

	// Token: 0x04000853 RID: 2131
	[SerializeField]
	private GameObject[] objectsToDisable;

	// Token: 0x04000854 RID: 2132
	[SerializeField]
	private GameObject[] objectsToEnable;

	// Token: 0x04000855 RID: 2133
	[SerializeField]
	private bool startOnEnable = true;

	// Token: 0x04000856 RID: 2134
	[SerializeField]
	private EventSystem eventSystem;

	// Token: 0x04000857 RID: 2135
	[SerializeField]
	private bool selectByDefault;

	// Token: 0x04000858 RID: 2136
	[SerializeField]
	private bool delaySelection;
}
