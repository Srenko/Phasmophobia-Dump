using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityStandardAssets.Characters.FirstPerson;

// Token: 0x020001AB RID: 427
public class PCCanvas : MonoBehaviour
{
	// Token: 0x06000B93 RID: 2963 RVA: 0x00047A55 File Offset: 0x00045C55
	private void Start()
	{
		this.UpdateCursorBrightness();
		this.SetState(PCCanvas.State.none, false);
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00047A68 File Offset: 0x00045C68
	private void Update()
	{
		if (MainManager.instance)
		{
			return;
		}
		if ((this.isPaused || LevelController.instance.journalController.isOpen) && this.player.playerInput.currentControlScheme != this.currentControlScheme)
		{
			if (this.player.view.isMine || !PhotonNetwork.inRoom)
			{
				this.OnControlSchemeChanged();
			}
			this.currentControlScheme = this.player.playerInput.currentControlScheme;
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00047AF0 File Offset: 0x00045CF0
	public void UpdateCursorBrightness()
	{
		this.defaultColour = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(6f * ((PlayerPrefs.GetFloat("cursorBrightnessValue") == 0f) ? 1f : PlayerPrefs.GetFloat("cursorBrightnessValue"))));
		this.activeColour = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(25f * ((PlayerPrefs.GetFloat("cursorBrightnessValue") == 0f) ? 1f : PlayerPrefs.GetFloat("cursorBrightnessValue"))));
		this.SetState(this.state, true);
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00047B90 File Offset: 0x00045D90
	public void SetState(PCCanvas.State state, [Optional] bool forceState)
	{
		if (!forceState && state == this.state)
		{
			return;
		}
		this.state = state;
		switch (state)
		{
		case PCCanvas.State.none:
			this.crosshair.enabled = true;
			this.crosshair.color = this.defaultColour;
			this.crosshair.sprite = this.normalSprite;
			this.crosshair.rectTransform.localScale = this.normalScale;
			return;
		case PCCanvas.State.active:
			this.crosshair.enabled = true;
			this.crosshair.color = this.activeColour;
			this.crosshair.sprite = this.normalSprite;
			this.crosshair.rectTransform.localScale = this.normalScale;
			return;
		case PCCanvas.State.locked:
			this.crosshair.enabled = true;
			this.crosshair.color = this.activeColour;
			this.crosshair.sprite = this.lockSprite;
			this.crosshair.rectTransform.localScale = this.normalScale;
			return;
		case PCCanvas.State.light:
			this.crosshair.enabled = true;
			this.crosshair.color = this.activeColour;
			this.crosshair.sprite = this.lightSprite;
			this.crosshair.rectTransform.localScale = this.normalScale * 1.5f;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00047CF9 File Offset: 0x00045EF9
	public void LoadingGame()
	{
		this.loadingFadeToBlackImage.gameObject.SetActive(true);
		this.crosshair.gameObject.SetActive(false);
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00047D20 File Offset: 0x00045F20
	public void Pause()
	{
		if (GameController.instance == null)
		{
			return;
		}
		if (LevelController.instance.journalController.isOpen)
		{
			LevelController.instance.journalController.CloseJournal();
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			return;
		}
		this.isPaused = !this.isPaused;
		PauseMenuController.instance.Pause(this.isPaused);
		base.GetComponent<FirstPersonController>().enabled = !this.isPaused;
		if (this.isPaused)
		{
			this.player.charAnim.SetFloat("speed", 0f);
			if (GameController.instance.myPlayer.player.playerInput.currentControlScheme == "Keyboard")
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				return;
			}
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00047DFB File Offset: 0x00045FFB
	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started && !XRDevice.isPresent)
		{
			this.Pause();
		}
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00047E14 File Offset: 0x00046014
	public void OnControlSchemeChanged()
	{
		if (XRDevice.isPresent)
		{
			return;
		}
		if (MainManager.instance)
		{
			return;
		}
		if (this.isPaused || LevelController.instance.journalController.isOpen)
		{
			if (GameController.instance.myPlayer.player.playerInput.currentControlScheme == "Keyboard")
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				return;
			}
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			GamepadUISelector.instance.SetSelection();
		}
	}

	// Token: 0x04000BE6 RID: 3046
	public Image crosshair;

	// Token: 0x04000BE7 RID: 3047
	[SerializeField]
	private Image loadingFadeToBlackImage;

	// Token: 0x04000BE8 RID: 3048
	[SerializeField]
	private Sprite normalSprite;

	// Token: 0x04000BE9 RID: 3049
	[SerializeField]
	private Sprite lockSprite;

	// Token: 0x04000BEA RID: 3050
	[SerializeField]
	private Sprite lightSprite;

	// Token: 0x04000BEB RID: 3051
	[SerializeField]
	private Player player;

	// Token: 0x04000BEC RID: 3052
	[HideInInspector]
	public bool isPaused;

	// Token: 0x04000BED RID: 3053
	private Color32 defaultColour;

	// Token: 0x04000BEE RID: 3054
	private Color32 activeColour;

	// Token: 0x04000BEF RID: 3055
	public GameObject canvas;

	// Token: 0x04000BF0 RID: 3056
	private Vector3 normalScale = new Vector3(0.08f, 0.08f, 0.08f);

	// Token: 0x04000BF1 RID: 3057
	private string currentControlScheme;

	// Token: 0x04000BF2 RID: 3058
	public PCCanvas.State state;

	// Token: 0x02000552 RID: 1362
	public enum State
	{
		// Token: 0x04002571 RID: 9585
		none,
		// Token: 0x04002572 RID: 9586
		active,
		// Token: 0x04002573 RID: 9587
		locked,
		// Token: 0x04002574 RID: 9588
		light
	}
}
