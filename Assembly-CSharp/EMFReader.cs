using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000155 RID: 341
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
public class EMFReader : MonoBehaviour
{
	// Token: 0x06000902 RID: 2306 RVA: 0x00035FC1 File Offset: 0x000341C1
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00035FD4 File Offset: 0x000341D4
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.Use));
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00035FF0 File Offset: 0x000341F0
	public void Update()
	{
		if (this.isOn)
		{
			this.strength = 0;
			this.source.volume = 0f;
			for (int i = 0; i < this.emfZones.Count; i++)
			{
				if (this.emfZones[i].strength > this.strength)
				{
					this.strength = this.emfZones[i].strength;
				}
			}
			if (this.strength > 0)
			{
				this.source.volume = (float)this.strength / 25f;
				if (this.strength >= 1)
				{
					this.rend.materials[5].SetColor("_EmissionColor", this.lightGreen);
				}
				else
				{
					this.rend.materials[5].SetColor("_EmissionColor", Color.black);
				}
				if (this.strength >= 2)
				{
					this.rend.materials[6].SetColor("_EmissionColor", Color.yellow);
				}
				else
				{
					this.rend.materials[6].SetColor("_EmissionColor", Color.black);
				}
				if (this.strength >= 3)
				{
					this.rend.materials[4].SetColor("_EmissionColor", this.orange);
				}
				else
				{
					this.rend.materials[4].SetColor("_EmissionColor", Color.black);
				}
				if (this.strength == 4)
				{
					this.rend.materials[3].SetColor("_EmissionColor", Color.red);
				}
				else
				{
					this.rend.materials[3].SetColor("_EmissionColor", Color.black);
				}
				if (MissionEMFEvidence.instance != null && !MissionEMFEvidence.instance.completed)
				{
					MissionEMFEvidence.instance.CompleteMission();
				}
			}
			else
			{
				this.rend.materials[2].SetColor("_EmissionColor", Color.green);
				this.rend.materials[5].SetColor("_EmissionColor", Color.black);
				this.rend.materials[6].SetColor("_EmissionColor", Color.black);
				this.rend.materials[4].SetColor("_EmissionColor", Color.black);
				this.rend.materials[3].SetColor("_EmissionColor", Color.black);
			}
			this.noise.volume = (float)(this.strength / 4);
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00036265 File Offset: 0x00034465
	public void Use()
	{
		this.view.RPC("NetworkedUse", PhotonTargets.All, new object[]
		{
			PhotonNetwork.player.ID
		});
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00036290 File Offset: 0x00034490
	[PunRPC]
	public void NetworkedUse(int actorID)
	{
		this.isOn = !this.isOn;
		this.source.outputAudioMixerGroup = SoundController.instance.GetPlayersAudioGroup(actorID);
		this.onOffAudioSource.outputAudioMixerGroup = SoundController.instance.GetPlayersAudioGroup(actorID);
		this.onOffAudioSource.Play();
		if (this.isOn)
		{
			this.strength = 0;
			this.source.volume = 0f;
			this.source.enabled = true;
			this.noise.gameObject.SetActive(true);
			this.rend.materials[2].SetColor("_EmissionColor", Color.green);
			this.rend.materials[5].SetColor("_EmissionColor", Color.black);
			this.rend.materials[6].SetColor("_EmissionColor", Color.black);
			this.rend.materials[4].SetColor("_EmissionColor", Color.black);
			this.rend.materials[3].SetColor("_EmissionColor", Color.black);
			return;
		}
		this.strength = 0;
		this.source.volume = 0f;
		this.source.enabled = false;
		this.noise.gameObject.SetActive(false);
		this.rend.materials[2].SetColor("_EmissionColor", Color.black);
		this.rend.materials[5].SetColor("_EmissionColor", Color.black);
		this.rend.materials[6].SetColor("_EmissionColor", Color.black);
		this.rend.materials[4].SetColor("_EmissionColor", Color.black);
		this.rend.materials[3].SetColor("_EmissionColor", Color.black);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00036470 File Offset: 0x00034670
	public void RemoveEMFZone(EMF emf)
	{
		if (emf == null)
		{
			return;
		}
		if (this.emfZones.Contains(emf))
		{
			this.emfZones.Remove(emf);
		}
		if (this.emfZones.Count == 0)
		{
			this.strength = 0;
			this.source.volume = 0f;
			this.rend.materials[2].SetColor("_EmissionColor", Color.black);
			this.rend.materials[5].SetColor("_EmissionColor", Color.black);
			this.rend.materials[6].SetColor("_EmissionColor", Color.black);
			this.rend.materials[4].SetColor("_EmissionColor", Color.black);
			this.rend.materials[3].SetColor("_EmissionColor", Color.black);
		}
		for (int i = 0; i < this.emfZones.Count; i++)
		{
			if (this.emfZones[i] == null)
			{
				this.emfZones.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0003658C File Offset: 0x0003478C
	public void AddEMFZone(EMF emf)
	{
		if (!this.emfZones.Contains(emf))
		{
			this.emfZones.Add(emf);
			emf.emfReaders.Add(this);
		}
		for (int i = 0; i < this.emfZones.Count; i++)
		{
			if (this.emfZones[i] == null)
			{
				this.emfZones.RemoveAt(i);
			}
		}
	}

	// Token: 0x04000912 RID: 2322
	public Renderer rend;

	// Token: 0x04000913 RID: 2323
	public AudioSource source;

	// Token: 0x04000914 RID: 2324
	public PhotonView view;

	// Token: 0x04000915 RID: 2325
	public int strength;

	// Token: 0x04000916 RID: 2326
	public List<EMF> emfZones = new List<EMF>();

	// Token: 0x04000917 RID: 2327
	private GhostAI nearestGhost;

	// Token: 0x04000918 RID: 2328
	[HideInInspector]
	public bool isOn;

	// Token: 0x04000919 RID: 2329
	private Color lightGreen = new Color(0.4f, 1f, 0.4f);

	// Token: 0x0400091A RID: 2330
	private Color orange = new Color(1f, 0.5f, 0f);

	// Token: 0x0400091B RID: 2331
	[SerializeField]
	private Noise noise;

	// Token: 0x0400091C RID: 2332
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x0400091D RID: 2333
	[SerializeField]
	private AudioSource onOffAudioSource;
}
