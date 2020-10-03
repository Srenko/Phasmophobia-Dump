using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;

// Token: 0x020001B1 RID: 433
public class PCMenu : MonoBehaviour
{
	// Token: 0x06000BB9 RID: 3001 RVA: 0x000488B3 File Offset: 0x00046AB3
	private void Awake()
	{
		this.isOnMenu = false;
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x000488BC File Offset: 0x00046ABC
	private void Start()
	{
		if (!MainManager.instance)
		{
			base.enabled = false;
			return;
		}
		base.StartCoroutine(this.DisableUIDelay());
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x000488DF File Offset: 0x00046ADF
	private IEnumerator DisableUIDelay()
	{
		yield return new WaitUntil(() => EventSystem.current);
		yield return new WaitUntil(() => EventSystem.current.currentInputModule);
		EventSystem.current.currentInputModule.enabled = false;
		yield break;
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x000488E7 File Offset: 0x00046AE7
	public void ForceIntoMenu()
	{
		this.OpenMenu();
		base.StopAllCoroutines();
		base.StartCoroutine(this.EnableInputDelay());
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00048902 File Offset: 0x00046B02
	private IEnumerator EnableInputDelay()
	{
		yield return new WaitUntil(() => EventSystem.current);
		yield return new WaitUntil(() => EventSystem.current.currentInputModule);
		EventSystem.current.currentInputModule.enabled = true;
		yield break;
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0004890C File Offset: 0x00046B0C
	public void LeaveMenu()
	{
		if (this.isOnMenu)
		{
			this.isOnMenu = false;
			this.player.cam.enabled = true;
			this.player.firstPersonController.enabled = true;
			MainManager.instance.sceneCamera.gameObject.SetActive(false);
			this.player.pcCanvas.crosshair.gameObject.SetActive(true);
			if (this.player.playerInput.currentControlScheme == "Keyboard")
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			MainManager.instance.serverManager.EnableOrDisablePlayerModels(true);
			EventSystem.current.currentInputModule.enabled = false;
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x000489C8 File Offset: 0x00046BC8
	public void OpenMenu()
	{
		this.isOnMenu = true;
		this.player.cam.enabled = false;
		this.player.firstPersonController.enabled = false;
		if (this.player.charAnim != null)
		{
			this.player.charAnim.SetFloat("speed", 0f);
		}
		MainManager.instance.sceneCamera.gameObject.SetActive(true);
		this.player.pcCanvas.crosshair.gameObject.SetActive(false);
		if (GamepadUISelector.instance)
		{
			GamepadUISelector.instance.SetSelection();
		}
		if (this.player.playerInput.currentControlScheme == "Keyboard")
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			GamepadUISelector.instance.SetSelection();
		}
		MainManager.instance.serverManager.EnableOrDisablePlayerModels(false);
		base.StartCoroutine(this.EnableInputDelay());
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00048AC4 File Offset: 0x00046CC4
	private void Update()
	{
		RaycastHit raycastHit;
		if (this.isOnMenu)
		{
			if (this.player.playerInput.currentControlScheme != this.currentControlScheme)
			{
				if (this.player.view.isMine || !PhotonNetwork.inRoom)
				{
					this.OnControlSchemeChanged();
				}
				this.currentControlScheme = this.player.playerInput.currentControlScheme;
				return;
			}
		}
		else if (Mouse.current.leftButton.wasPressedThisFrame && Physics.Raycast(this.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, 5f, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.CompareTag("MainMenuUI"))
		{
			this.OpenMenu();
		}
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00048B98 File Offset: 0x00046D98
	public void OnControlSchemeChanged()
	{
		if (XRDevice.isPresent)
		{
			return;
		}
		if (!MainManager.instance)
		{
			return;
		}
		if (!this.isOnMenu)
		{
			return;
		}
		if (this.player.playerInput.currentControlScheme == "Keyboard")
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			return;
		}
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		GamepadUISelector.instance.SetSelection();
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00048C04 File Offset: 0x00046E04
	public void OnInteract(InputAction.CallbackContext context)
	{
		if (this.player.playerInput.currentControlScheme == "Keyboard")
		{
			return;
		}
		RaycastHit raycastHit;
		if (context.phase == InputActionPhase.Canceled && !this.isOnMenu && Physics.Raycast(this.player.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out raycastHit, 5f, this.mask, QueryTriggerInteraction.Ignore) && raycastHit.collider.CompareTag("MainMenuUI"))
		{
			this.OpenMenu();
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00048C97 File Offset: 0x00046E97
	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Canceled)
		{
			this.LeaveMenu();
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00048C97 File Offset: 0x00046E97
	public void OnDrop(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Canceled)
		{
			this.LeaveMenu();
		}
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00048CA9 File Offset: 0x00046EA9
	public void OnMenuSecondary(InputAction.CallbackContext context)
	{
		if (MainManager.instance == null)
		{
			return;
		}
		if (context.phase == InputActionPhase.Canceled)
		{
			if (this.isOnMenu)
			{
				this.LeaveMenu();
				return;
			}
			this.OpenMenu();
		}
	}

	// Token: 0x04000C0F RID: 3087
	[HideInInspector]
	public bool isOnMenu;

	// Token: 0x04000C10 RID: 3088
	[SerializeField]
	private Player player;

	// Token: 0x04000C11 RID: 3089
	[SerializeField]
	private LayerMask mask;

	// Token: 0x04000C12 RID: 3090
	[SerializeField]
	private GameObject firstSelectedButton;

	// Token: 0x04000C13 RID: 3091
	private string currentControlScheme;
}
