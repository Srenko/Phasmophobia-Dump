using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000146 RID: 326
public class FuseBox : MonoBehaviour
{
	// Token: 0x06000886 RID: 2182 RVA: 0x00032D58 File Offset: 0x00030F58
	private void Awake()
	{
		if (!this.noise)
		{
			this.noise = base.GetComponentInChildren<Noise>();
		}
		if (!this.source)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		if (!this.view)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		if (!this.photonInteract)
		{
			this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		}
		this.noise.gameObject.SetActive(false);
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00032DF4 File Offset: 0x00030FF4
	public void SetupAudioGroup()
	{
		this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
		this.floorType = SoundController.instance.GetFloorTypeFromAudioGroup(this.source.outputAudioMixerGroup);
		MapController.instance.AssignIcon(this.mapIcon, this.floorType);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00032E57 File Offset: 0x00031057
	private void Start()
	{
		if (LevelController.instance.type == LevelController.levelType.small)
		{
			this.maxLights = 10;
			return;
		}
		if (LevelController.instance.type == LevelController.levelType.medium)
		{
			this.maxLights = 9;
			return;
		}
		this.maxLights = 8;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00032E8B File Offset: 0x0003108B
	public void TurnOff()
	{
		if (PhotonNetwork.isMasterClient)
		{
			if (PhotonNetwork.inRoom)
			{
				this.view.RPC("UseNetworked", PhotonTargets.AllBuffered, new object[]
				{
					true
				});
				return;
			}
			this.UseNetworked(true);
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00032EC3 File Offset: 0x000310C3
	public void Use()
	{
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("UseNetworked", PhotonTargets.AllBuffered, new object[]
			{
				false
			});
			return;
		}
		this.UseNetworked(false);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00032EF4 File Offset: 0x000310F4
	[PunRPC]
	private void UseNetworked(bool isGhostUsing)
	{
		if (isGhostUsing)
		{
			this.isOn = false;
		}
		else
		{
			this.isOn = !this.isOn;
		}
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		base.StartCoroutine(this.PlayNoiseObject());
		if (this.isOn)
		{
			this.source.clip = this.onClip;
			foreach (LightSwitch lightSwitch in this.switches)
			{
				if (lightSwitch != null)
				{
					lightSwitch.FuseOn();
				}
			}
			for (int i = 0; i < this.rends.Length; i++)
			{
				if (this.rends[i] != null)
				{
					this.rends[i].materials[1].SetColor("_EmissionColor", Color.green);
				}
			}
			for (int j = 0; j < this.rendsToTurnOff.Length; j++)
			{
				if (this.rendsToTurnOff[j] != null)
				{
					this.rendsToTurnOff[j].materials[0].EnableKeyword("_EMISSION");
				}
			}
			for (int k = 0; k < this.lights.Length; k++)
			{
				if (this.lights[k] != null)
				{
					this.lights[k].enabled = true;
				}
			}
			if (PhotonNetwork.isMasterClient && CCTVController.instance.activatedCCTVCameras)
			{
				for (int l = 0; l < CCTVController.instance.allFixedCCTVCameras.Count; l++)
				{
					if (CCTVController.instance.allFixedCCTVCameras[l] != null)
					{
						CCTVController.instance.allFixedCCTVCameras[l].TurnOn();
					}
				}
			}
		}
		else
		{
			this.source.clip = this.offClip;
			foreach (LightSwitch lightSwitch2 in this.switches)
			{
				if (lightSwitch2 != null)
				{
					lightSwitch2.FuseOff();
				}
			}
			for (int m = 0; m < this.rends.Length; m++)
			{
				if (this.rends[m] != null)
				{
					this.rends[m].materials[1].SetColor("_EmissionColor", Color.red);
				}
			}
			for (int n = 0; n < this.rendsToTurnOff.Length; n++)
			{
				if (this.rendsToTurnOff[n] != null)
				{
					this.rendsToTurnOff[n].materials[0].DisableKeyword("_EMISSION");
				}
			}
			for (int num = 0; num < this.lights.Length; num++)
			{
				if (this.lights[num] != null)
				{
					this.lights[num].enabled = false;
				}
			}
			if (PhotonNetwork.isMasterClient)
			{
				for (int num2 = 0; num2 < CCTVController.instance.allFixedCCTVCameras.Count; num2++)
				{
					if (CCTVController.instance.allFixedCCTVCameras[num2] != null)
					{
						CCTVController.instance.allFixedCCTVCameras[num2].TurnOff();
					}
				}
			}
		}
		if (this.source != null)
		{
			this.source.Play();
		}
		foreach (ReflectionProbe reflectionProbe in this.probes)
		{
			if (reflectionProbe != null)
			{
				reflectionProbe.RenderProbe();
			}
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x000332A8 File Offset: 0x000314A8
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x000332B8 File Offset: 0x000314B8
	public void ChangeOnLights(int value)
	{
		this.currentOnLights += value;
		if (this.currentOnLights == this.maxLights)
		{
			if (PhotonNetwork.isMasterClient)
			{
				for (int i = 0; i < this.switches.Count; i++)
				{
					if (this.switches[i].isOn)
					{
						this.switches[i].TurnOff();
					}
				}
			}
			this.TurnOff();
			this.currentOnLights = 0;
		}
	}

	// Token: 0x040008A3 RID: 2211
	[SerializeField]
	private Renderer[] rends;

	// Token: 0x040008A4 RID: 2212
	[SerializeField]
	private Light[] lights;

	// Token: 0x040008A5 RID: 2213
	[SerializeField]
	private Renderer[] rendsToTurnOff;

	// Token: 0x040008A6 RID: 2214
	[SerializeField]
	private AudioSource source;

	// Token: 0x040008A7 RID: 2215
	[SerializeField]
	private PhotonView view;

	// Token: 0x040008A8 RID: 2216
	[SerializeField]
	private Noise noise;

	// Token: 0x040008A9 RID: 2217
	public Transform parentObject;

	// Token: 0x040008AA RID: 2218
	[SerializeField]
	private AudioClip onClip;

	// Token: 0x040008AB RID: 2219
	[SerializeField]
	private AudioClip offClip;

	// Token: 0x040008AC RID: 2220
	[HideInInspector]
	public bool isOn = true;

	// Token: 0x040008AD RID: 2221
	public List<LightSwitch> switches = new List<LightSwitch>();

	// Token: 0x040008AE RID: 2222
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x040008AF RID: 2223
	[HideInInspector]
	public List<ReflectionProbe> probes = new List<ReflectionProbe>();

	// Token: 0x040008B0 RID: 2224
	[HideInInspector]
	private int currentOnLights;

	// Token: 0x040008B1 RID: 2225
	private int maxLights = 10;

	// Token: 0x040008B2 RID: 2226
	[SerializeField]
	private Transform mapIcon;

	// Token: 0x040008B3 RID: 2227
	private LevelRoom.Type floorType = LevelRoom.Type.firstFloor;
}
