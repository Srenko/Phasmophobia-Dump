using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000142 RID: 322
[RequireComponent(typeof(PhotonView))]
public class Car : MonoBehaviour
{
	// Token: 0x06000856 RID: 2134 RVA: 0x00031B08 File Offset: 0x0002FD08
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.rend = base.GetComponent<Renderer>();
		this.photonInteract = base.GetComponent<PhotonObjectInteract>();
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00031B30 File Offset: 0x0002FD30
	private void Start()
	{
		this.source.clip = this.alarmClip;
		this.source.loop = true;
		this.alarmOn = false;
		this.rend.material.DisableKeyword("_EMISSION");
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00031B8D File Offset: 0x0002FD8D
	private void Update()
	{
		if (this.alarmOn)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				this.SwitchLights();
				this.timer = 0.2f;
			}
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00031BC8 File Offset: 0x0002FDC8
	private void Use()
	{
		for (int i = 0; i < GameController.instance.myPlayer.player.keys.Count; i++)
		{
			if (GameController.instance.myPlayer.player.keys[i] == Key.KeyType.Car)
			{
				this.view.RPC("TurnAlarmOff", PhotonTargets.All, Array.Empty<object>());
			}
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00031C2C File Offset: 0x0002FE2C
	private void SwitchLights()
	{
		this.isOn = !this.isOn;
		for (int i = 0; i < this.lights.Length; i++)
		{
			this.lights[i].enabled = this.isOn;
		}
		if (this.isOn)
		{
			this.rend.material.EnableKeyword("_EMISSION");
			return;
		}
		this.rend.material.DisableKeyword("_EMISSION");
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00031CA4 File Offset: 0x0002FEA4
	[PunRPC]
	private void TurnAlarmOn()
	{
		this.alarmOn = true;
		this.rend.material.EnableKeyword("_EMISSION");
		this.source.loop = true;
		this.source.clip = this.alarmClip;
		this.source.Play();
		if (this.noise)
		{
			this.noise.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00031D14 File Offset: 0x0002FF14
	[PunRPC]
	private void TurnAlarmOff()
	{
		this.alarmOn = false;
		this.rend.material.DisableKeyword("_EMISSION");
		this.timer = 0.2f;
		this.source.loop = false;
		this.source.Stop();
		this.source.clip = this.offClip;
		this.source.Play();
		if (this.noise)
		{
			this.noise.gameObject.SetActive(false);
		}
		this.isOn = false;
		for (int i = 0; i < this.lights.Length; i++)
		{
			this.lights[i].enabled = false;
		}
		this.mainRoomLight.ResetReflectionProbes();
	}

	// Token: 0x04000869 RID: 2153
	[HideInInspector]
	public PhotonView view;

	// Token: 0x0400086A RID: 2154
	private bool alarmOn;

	// Token: 0x0400086B RID: 2155
	private bool isOn;

	// Token: 0x0400086C RID: 2156
	[SerializeField]
	private Light[] lights;

	// Token: 0x0400086D RID: 2157
	private Renderer rend;

	// Token: 0x0400086E RID: 2158
	[SerializeField]
	private AudioSource source;

	// Token: 0x0400086F RID: 2159
	[SerializeField]
	private AudioClip alarmClip;

	// Token: 0x04000870 RID: 2160
	[SerializeField]
	private AudioClip offClip;

	// Token: 0x04000871 RID: 2161
	private float timer = 0.2f;

	// Token: 0x04000872 RID: 2162
	public Transform raycastSpot;

	// Token: 0x04000873 RID: 2163
	[SerializeField]
	private LightSwitch mainRoomLight;

	// Token: 0x04000874 RID: 2164
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000875 RID: 2165
	[SerializeField]
	private Noise noise;
}
