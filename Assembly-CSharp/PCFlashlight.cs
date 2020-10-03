using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

// Token: 0x020001AE RID: 430
public class PCFlashlight : MonoBehaviour
{
	// Token: 0x06000BA8 RID: 2984 RVA: 0x0004829C File Offset: 0x0004649C
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.isOn = false;
		this.blinkTimer = Random.Range(0f, 0.5f);
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x000482C8 File Offset: 0x000464C8
	public void GrabbedOrDroppedFlashlight(Torch torch, bool grabbed)
	{
		this.inventoryLight = (grabbed ? torch.myLight : null);
		this.headLight.cookie = torch.myLight.cookie;
		this.lightIntensity = torch.myLight.intensity;
		this.isOn = false;
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("GrabbedOrDroppedFlashlightNetworked", PhotonTargets.AllBuffered, new object[]
			{
				torch.myLight.intensity,
				torch.myLight.range,
				torch.myLight.spotAngle
			});
			return;
		}
		this.GrabbedOrDroppedFlashlightNetworked(torch.myLight.intensity, torch.myLight.range, torch.myLight.spotAngle);
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00048394 File Offset: 0x00046594
	[PunRPC]
	private void GrabbedOrDroppedFlashlightNetworked(float _intensity, float _range, float _angle)
	{
		this.headLight.intensity = _intensity;
		this.headLight.range = _range;
		this.headLight.spotAngle = _angle;
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x000483BC File Offset: 0x000465BC
	public void EnableOrDisableLight(bool _active, bool _isSwapping)
	{
		if (_active && this.inventoryLight == null)
		{
			return;
		}
		if (!_isSwapping && this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex] != null && this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex].GetComponent<Torch>() && this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex].GetComponent<Torch>().isBlacklight)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("EnableOrDisableLightNetworked", PhotonTargets.AllBuffered, new object[]
			{
				_active,
				_isSwapping
			});
			return;
		}
		this.EnableOrDisableLightNetworked(_active, _isSwapping);
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0004848C File Offset: 0x0004668C
	[PunRPC]
	private void EnableOrDisableLightNetworked(bool _active, bool _isSwapping)
	{
		this.headLight.enabled = _active;
		this.isOn = _active;
		if (PhotonNetwork.inRoom)
		{
			this.source.outputAudioMixerGroup = SoundController.instance.GetPlayersAudioGroup(this.view.owner.ID);
		}
		if (!_isSwapping)
		{
			if (this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex] == null)
			{
				this.source.Play();
				return;
			}
			if (this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex].GetComponent<Torch>())
			{
				if (this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex].GetComponent<Torch>().isBlacklight)
				{
					this.source.Play();
					return;
				}
			}
			else if (this.inventoryLight)
			{
				this.source.Play();
			}
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0004857F File Offset: 0x0004677F
	public void TurnBlinkOnOrOff(bool active)
	{
		this.isBlinking = active;
		if (!this.isBlinking && this.inventoryLight != null)
		{
			this.headLight.intensity = this.lightIntensity;
		}
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x000485B0 File Offset: 0x000467B0
	private void Update()
	{
		if (this.isBlinking)
		{
			this.blinkTimer -= Time.deltaTime;
			if (this.blinkTimer < 0f)
			{
				this.Blink();
				this.blinkTimer = Random.Range(0.1f, 0.5f);
			}
		}
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00048600 File Offset: 0x00046800
	private void Blink()
	{
		if (this.inventoryLight != null)
		{
			if (this.headLight.intensity == 0f)
			{
				this.headLight.intensity = this.lightIntensity;
				return;
			}
			this.headLight.intensity = 0f;
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00048650 File Offset: 0x00046850
	public void OnTorchUse(InputAction.CallbackContext context)
	{
		if (XRDevice.isPresent)
		{
			return;
		}
		if (this.player.isDead)
		{
			return;
		}
		if (PhotonNetwork.inRoom && !this.view.isMine)
		{
			return;
		}
		if (this.inventoryLight == null)
		{
			return;
		}
		if (context.phase == InputActionPhase.Started)
		{
			this.isOn = !this.isOn;
			if (this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex] != null && this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex].GetComponent<Torch>() && !this.pcPropGrab.inventoryProps[this.pcPropGrab.inventoryIndex].GetComponent<Torch>().isBlacklight)
			{
				this.isOn = false;
			}
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("EnableOrDisableLightNetworked", PhotonTargets.AllBuffered, new object[]
				{
					this.isOn,
					false
				});
				return;
			}
			this.EnableOrDisableLightNetworked(this.isOn, false);
		}
	}

	// Token: 0x04000BFB RID: 3067
	[SerializeField]
	private Light headLight;

	// Token: 0x04000BFC RID: 3068
	[SerializeField]
	private PCPropGrab pcPropGrab;

	// Token: 0x04000BFD RID: 3069
	[SerializeField]
	private Player player;

	// Token: 0x04000BFE RID: 3070
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000BFF RID: 3071
	private PhotonView view;

	// Token: 0x04000C00 RID: 3072
	private bool isOn;

	// Token: 0x04000C01 RID: 3073
	private Light inventoryLight;

	// Token: 0x04000C02 RID: 3074
	private bool isBlinking;

	// Token: 0x04000C03 RID: 3075
	private float lightIntensity;

	// Token: 0x04000C04 RID: 3076
	private float blinkTimer;
}
