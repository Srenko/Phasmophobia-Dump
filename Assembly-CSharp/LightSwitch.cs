using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000148 RID: 328
public class LightSwitch : MonoBehaviour
{
	// Token: 0x06000893 RID: 2195 RVA: 0x000334E4 File Offset: 0x000316E4
	private void Awake()
	{
		this.isOn = false;
		if (this.noise == null)
		{
			this.noise = base.GetComponentInChildren<Noise>();
		}
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		if (this.view == null)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		if (this.photonInteract == null)
		{
			this.photonInteract = base.GetComponent<PhotonObjectInteract>();
		}
		foreach (Light light in this.lights)
		{
			this.lightsMaxIntensity.Add(light.intensity);
		}
		this.blinkTimer = Random.Range(0f, 0.5f);
		if (this.noise != null)
		{
			this.noise.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x000335E4 File Offset: 0x000317E4
	private void Start()
	{
		if (this.handPrintObject != null)
		{
			this.handPrintObject.SetActive(false);
		}
		this.photonInteract.AddUseEvent(new UnityAction(this.UseLight));
		for (int i = 0; i < this.probes.Count; i++)
		{
			if (this.probes[i] != null)
			{
				LevelController.instance.fuseBox.probes.Add(this.probes[i]);
			}
		}
		if (PhotonNetwork.isMasterClient)
		{
			this.TurnOff();
		}
		this.source.outputAudioMixerGroup = SoundController.instance.GetFloorAudioSnapshot(base.transform.position.y);
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x000336A0 File Offset: 0x000318A0
	public void UseLight()
	{
		if (this.isBlinking)
		{
			return;
		}
		if (GameController.instance.myPlayer.player.isDead)
		{
			return;
		}
		if (PhotonNetwork.inRoom)
		{
			this.view.RPC("Use", PhotonTargets.All, Array.Empty<object>());
			return;
		}
		this.Use();
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x000336F4 File Offset: 0x000318F4
	[PunRPC]
	private void Use()
	{
		this.isOn = !this.isOn;
		if (this.source == null)
		{
			this.source = base.GetComponent<AudioSource>();
		}
		if (this.view == null)
		{
			this.view = base.GetComponent<PhotonView>();
		}
		if (this.isOn)
		{
			this.source.clip = this.clips[Random.Range(0, this.clips.Length)];
			this.source.Play();
			if (this.lever != null)
			{
				Quaternion localRotation = this.lever.transform.localRotation;
				localRotation.eulerAngles = new Vector3(15f, 0f, 0f);
				this.lever.transform.localRotation = localRotation;
			}
			if (LevelController.instance.fuseBox.isOn)
			{
				foreach (Light light in this.lights)
				{
					light.enabled = true;
				}
				foreach (Renderer renderer in this.rends)
				{
					for (int i = 0; i < renderer.materials.Length; i++)
					{
						renderer.materials[i].EnableKeyword("_EMISSION");
					}
				}
				Animator[] array = this.animators;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].SetBool("isOn", true);
				}
				AudioSource[] array2 = this.sources;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Play();
				}
				for (int k = 0; k < this.objectsToActivate.Length; k++)
				{
					this.objectsToActivate[k].SetActive(true);
				}
				this.ResetReflectionProbes();
				base.StartCoroutine(this.FlickerAfterTimer());
			}
		}
		else
		{
			this.source.clip = this.clips[Random.Range(0, this.clips.Length)];
			this.source.Play();
			if (this.lever != null)
			{
				Quaternion localRotation2 = this.lever.transform.localRotation;
				localRotation2.eulerAngles = new Vector3(-15f, 0f, 0f);
				this.lever.transform.localRotation = localRotation2;
			}
			if (LevelController.instance.fuseBox.isOn)
			{
				foreach (Light light2 in this.lights)
				{
					if (light2 != null)
					{
						light2.enabled = false;
					}
				}
				foreach (Renderer renderer2 in this.rends)
				{
					if (renderer2 != null)
					{
						for (int l = 0; l < renderer2.materials.Length; l++)
						{
							renderer2.materials[l].DisableKeyword("_EMISSION");
						}
					}
				}
				Animator[] array = this.animators;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].SetBool("isOn", false);
				}
				AudioSource[] array2 = this.sources;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Stop();
				}
				for (int m = 0; m < this.objectsToActivate.Length; m++)
				{
					this.objectsToActivate[m].SetActive(false);
				}
				this.ResetReflectionProbes();
			}
			base.StopAllCoroutines();
		}
		LevelController.instance.fuseBox.ChangeOnLights(this.isOn ? 1 : -1);
		if (this.noise != null)
		{
			base.StartCoroutine(this.PlayNoiseObject());
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00033B14 File Offset: 0x00031D14
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return 0;
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00033B24 File Offset: 0x00031D24
	public void FuseOff()
	{
		foreach (Light light in this.lights)
		{
			light.enabled = false;
		}
		foreach (Renderer renderer in this.rends)
		{
			for (int i = 0; i < renderer.materials.Length; i++)
			{
				renderer.materials[i].DisableKeyword("_EMISSION");
			}
		}
		Animator[] array = this.animators;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetBool("isOn", false);
		}
		AudioSource[] array2 = this.sources;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].Stop();
		}
		for (int k = 0; k < this.objectsToActivate.Length; k++)
		{
			this.objectsToActivate[k].SetActive(false);
		}
		base.StopAllCoroutines();
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00033C54 File Offset: 0x00031E54
	public void FuseOn()
	{
		if (this.isOn)
		{
			foreach (Light light in this.lights)
			{
				light.enabled = true;
			}
			foreach (Renderer renderer in this.rends)
			{
				for (int i = 0; i < renderer.materials.Length; i++)
				{
					renderer.materials[i].EnableKeyword("_EMISSION");
				}
			}
			Animator[] array = this.animators;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetBool("isOn", true);
			}
			AudioSource[] array2 = this.sources;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].Play();
			}
			for (int k = 0; k < this.objectsToActivate.Length; k++)
			{
				this.objectsToActivate[k].SetActive(true);
			}
			base.StartCoroutine(this.FlickerAfterTimer());
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00033D94 File Offset: 0x00031F94
	public void StartBlinking()
	{
		this.isBlinking = true;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00033DA0 File Offset: 0x00031FA0
	public void StopBlinking()
	{
		this.isBlinking = false;
		if (PhotonNetwork.isMasterClient)
		{
			this.view.RPC("ResetLights", PhotonTargets.All, Array.Empty<object>());
			this.view.RPC("TurnOffNetworked", PhotonTargets.All, new object[]
			{
				false
			});
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00033DF4 File Offset: 0x00031FF4
	[PunRPC]
	private void ResetLights()
	{
		for (int i = 0; i < this.lights.Count; i++)
		{
			this.lights[i].intensity = this.lightsMaxIntensity[i];
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00033E34 File Offset: 0x00032034
	private void Update()
	{
		if (this.isBlinking)
		{
			if (!this.isOn)
			{
				return;
			}
			this.blinkTimer -= Time.deltaTime;
			if (this.blinkTimer < 0f)
			{
				this.Blink();
				this.blinkTimer = Random.Range(0.1f, 0.5f);
			}
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00033E8C File Offset: 0x0003208C
	public void Blink()
	{
		if (LevelController.instance.fuseBox.isOn)
		{
			if (LevelController.instance.currentGhostRoom == this.myRoom)
			{
				for (int i = 0; i < this.lights.Count; i++)
				{
					this.lights[i].intensity = 0f;
				}
				for (int j = 0; j < this.rends.Count; j++)
				{
					for (int k = 0; k < this.rends[j].materials.Length; k++)
					{
						if (this.rends[j].materials[k].IsKeywordEnabled("_EMISSION"))
						{
							this.rends[j].materials[k].DisableKeyword("_EMISSION");
						}
					}
				}
				return;
			}
			for (int l = 0; l < this.lights.Count; l++)
			{
				if (this.lights[l].intensity == 0f)
				{
					this.lights[l].intensity = this.lightsMaxIntensity[l];
				}
				else
				{
					this.lights[l].intensity = 0f;
				}
			}
			for (int m = 0; m < this.rends.Count; m++)
			{
				for (int n = 0; n < this.rends[m].materials.Length; n++)
				{
					if (this.rends[m].materials[n].IsKeywordEnabled("_EMISSION"))
					{
						this.rends[m].materials[n].DisableKeyword("_EMISSION");
					}
					else
					{
						this.rends[m].materials[n].EnableKeyword("_EMISSION");
					}
				}
			}
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0003406B File Offset: 0x0003226B
	public void TurnOff()
	{
		this.view.RPC("TurnOffNetworked", PhotonTargets.AllBuffered, new object[]
		{
			true
		});
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00034090 File Offset: 0x00032290
	[PunRPC]
	public void TurnOffNetworked(bool playSound)
	{
		this.isOn = false;
		if (playSound)
		{
			this.source.clip = this.clips[Random.Range(0, this.clips.Length)];
			this.source.Play();
			if (this.lever != null)
			{
				Quaternion localRotation = this.lever.transform.localRotation;
				localRotation.eulerAngles = new Vector3(-15f, 0f, 0f);
				this.lever.transform.localRotation = localRotation;
			}
		}
		if (LevelController.instance.fuseBox.isOn)
		{
			foreach (Light light in this.lights)
			{
				if (light != null)
				{
					light.enabled = false;
				}
			}
			foreach (Renderer renderer in this.rends)
			{
				if (renderer != null)
				{
					for (int i = 0; i < renderer.materials.Length; i++)
					{
						renderer.materials[i].DisableKeyword("_EMISSION");
					}
				}
			}
			Animator[] array = this.animators;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetBool("isOn", false);
			}
			AudioSource[] array2 = this.sources;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].Stop();
			}
			for (int k = 0; k < this.objectsToActivate.Length; k++)
			{
				this.objectsToActivate[k].SetActive(false);
			}
			this.ResetReflectionProbes();
		}
		if (this.noise != null)
		{
			base.StartCoroutine(this.PlayNoiseObject());
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0003428C File Offset: 0x0003248C
	public void TurnOn(bool playSound)
	{
		this.view.RPC("TurnOnNetworked", PhotonTargets.All, new object[]
		{
			playSound
		});
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x000342B0 File Offset: 0x000324B0
	[PunRPC]
	public void TurnOnNetworked(bool playSound)
	{
		this.isOn = true;
		if (playSound)
		{
			this.source.clip = this.clips[Random.Range(0, this.clips.Length)];
			this.source.Play();
			if (this.lever != null)
			{
				Quaternion localRotation = this.lever.transform.localRotation;
				localRotation.eulerAngles = new Vector3(15f, 0f, 0f);
				this.lever.transform.localRotation = localRotation;
			}
		}
		if (LevelController.instance.fuseBox.isOn)
		{
			foreach (Light light in this.lights)
			{
				light.enabled = true;
			}
			foreach (Renderer renderer in this.rends)
			{
				for (int i = 0; i < renderer.materials.Length; i++)
				{
					renderer.materials[i].EnableKeyword("_EMISSION");
				}
			}
			Animator[] array = this.animators;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetBool("isOn", true);
			}
			AudioSource[] array2 = this.sources;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].Play();
			}
			for (int k = 0; k < this.objectsToActivate.Length; k++)
			{
				this.objectsToActivate[k].SetActive(true);
			}
			this.ResetReflectionProbes();
			if (playSound)
			{
				base.StartCoroutine(this.FlickerAfterTimer());
			}
		}
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00034488 File Offset: 0x00032688
	private IEnumerator FlickerAfterTimer()
	{
		yield return new WaitForSeconds(Random.Range(60f, 200f));
		if (PhotonNetwork.isMasterClient && GameController.instance.allPlayersAreConnected && SoundController.instance.GetFloorTypeFromPosition(base.transform.position.y) == LevelController.instance.currentGhostRoom.floorType && Vector3.Distance(base.transform.position, LevelController.instance.currentGhost.transform.position) < 8f)
		{
			this.view.RPC("FlickerNetworked", PhotonTargets.All, Array.Empty<object>());
		}
		base.StartCoroutine(this.FlickerAfterTimer());
		if (GameController.instance.myPlayer.player.insanity > 50f)
		{
			yield return new WaitForSeconds(Random.Range(20f, 40f));
			this.FlickerNetworked();
		}
		else if (GameController.instance.myPlayer.player.insanity > 75f)
		{
			yield return new WaitForSeconds(Random.Range(10f, 30f));
			this.FlickerNetworked();
		}
		yield break;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00034497 File Offset: 0x00032697
	[PunRPC]
	private void FlickerNetworked()
	{
		base.StartCoroutine(this.Flicker());
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x000344A6 File Offset: 0x000326A6
	private IEnumerator Flicker()
	{
		if (!this.isOn)
		{
			yield return null;
		}
		foreach (Light light in this.lights)
		{
			light.enabled = false;
		}
		foreach (Renderer renderer in this.rends)
		{
			for (int i = 0; i < renderer.materials.Length; i++)
			{
				renderer.materials[i].DisableKeyword("_EMISSION");
			}
		}
		Animator[] array = this.animators;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetBool("isOn", false);
		}
		yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
		foreach (Light light2 in this.lights)
		{
			light2.enabled = true;
		}
		foreach (Renderer renderer2 in this.rends)
		{
			for (int k = 0; k < renderer2.materials.Length; k++)
			{
				renderer2.materials[k].EnableKeyword("_EMISSION");
			}
		}
		array = this.animators;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetBool("isOn", true);
		}
		yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
		foreach (Light light3 in this.lights)
		{
			light3.enabled = false;
		}
		foreach (Renderer renderer3 in this.rends)
		{
			for (int l = 0; l < renderer3.materials.Length; l++)
			{
				renderer3.materials[l].DisableKeyword("_EMISSION");
			}
		}
		array = this.animators;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetBool("isOn", false);
		}
		yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
		this.TurnOn(false);
		yield break;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x000344B8 File Offset: 0x000326B8
	public void ResetReflectionProbes()
	{
		foreach (ReflectionProbe reflectionProbe in this.probes)
		{
			if (reflectionProbe != null)
			{
				reflectionProbe.RenderProbe();
			}
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00034514 File Offset: 0x00032714
	public void SmashBulb()
	{
		this.view.RPC("SmashBulbNetworked", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0003452C File Offset: 0x0003272C
	[PunRPC]
	public void SmashBulbNetworked()
	{
		this.isOn = false;
		this.source.clip = this.bulbSmashClip;
		this.source.Play();
		if (LevelController.instance.fuseBox.isOn)
		{
			foreach (Light light in this.lights)
			{
				if (light != null)
				{
					light.enabled = false;
				}
			}
			foreach (Renderer renderer in this.rends)
			{
				if (renderer != null)
				{
					for (int i = 0; i < renderer.materials.Length; i++)
					{
						renderer.materials[i].DisableKeyword("_EMISSION");
					}
				}
			}
			Animator[] array = this.animators;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetBool("isOn", false);
			}
			AudioSource[] array2 = this.sources;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].Stop();
			}
			for (int k = 0; k < this.objectsToActivate.Length; k++)
			{
				this.objectsToActivate[k].SetActive(false);
			}
			this.ResetReflectionProbes();
		}
		base.StartCoroutine(this.PlayNoiseObject());
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x000346B8 File Offset: 0x000328B8
	public void SpawnHandPrintEvidence()
	{
		if (this.handPrintObject == null)
		{
			return;
		}
		if (this.handPrintObject.activeInHierarchy)
		{
			return;
		}
		this.view.RPC("NetworkedSpawnHandPrintEvidence", PhotonTargets.All, Array.Empty<object>());
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x000346ED File Offset: 0x000328ED
	[PunRPC]
	private void NetworkedSpawnHandPrintEvidence()
	{
		this.handPrintObject.SetActive(true);
	}

	// Token: 0x040008B6 RID: 2230
	[SerializeField]
	private AudioSource source;

	// Token: 0x040008B7 RID: 2231
	public PhotonView view;

	// Token: 0x040008B8 RID: 2232
	[SerializeField]
	private AudioClip[] clips;

	// Token: 0x040008B9 RID: 2233
	[SerializeField]
	private AudioClip bulbSmashClip;

	// Token: 0x040008BA RID: 2234
	public List<Light> lights = new List<Light>();

	// Token: 0x040008BB RID: 2235
	public List<Renderer> rends = new List<Renderer>();

	// Token: 0x040008BC RID: 2236
	public List<ReflectionProbe> probes = new List<ReflectionProbe>();

	// Token: 0x040008BD RID: 2237
	public Animator[] animators;

	// Token: 0x040008BE RID: 2238
	public AudioSource[] sources;

	// Token: 0x040008BF RID: 2239
	public GameObject[] objectsToActivate;

	// Token: 0x040008C0 RID: 2240
	public Transform lever;

	// Token: 0x040008C1 RID: 2241
	public PhotonObjectInteract photonInteract;

	// Token: 0x040008C2 RID: 2242
	private List<float> lightsMaxIntensity = new List<float>();

	// Token: 0x040008C3 RID: 2243
	private float blinkTimer;

	// Token: 0x040008C4 RID: 2244
	[HideInInspector]
	public bool isBlinking;

	// Token: 0x040008C5 RID: 2245
	[SerializeField]
	private Noise noise;

	// Token: 0x040008C6 RID: 2246
	[HideInInspector]
	public bool isOn;

	// Token: 0x040008C7 RID: 2247
	[HideInInspector]
	public LevelRoom myRoom;

	// Token: 0x040008C8 RID: 2248
	[SerializeField]
	private GameObject handPrintObject;
}
