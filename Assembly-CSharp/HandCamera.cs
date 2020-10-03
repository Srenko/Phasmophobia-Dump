using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200015A RID: 346
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
public class HandCamera : MonoBehaviour
{
	// Token: 0x06000932 RID: 2354 RVA: 0x00037D57 File Offset: 0x00035F57
	private void Awake()
	{
		this.noise.gameObject.SetActive(false);
		this.UpdateUIValue();
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00037D70 File Offset: 0x00035F70
	private void Start()
	{
		this.photonInteract.AddUseEvent(new UnityAction(this.TakePhoto));
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00037D89 File Offset: 0x00035F89
	private void Update()
	{
		if (!this.canTakePhoto)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				this.canTakePhoto = true;
				this.timer = 3f;
			}
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00037DC4 File Offset: 0x00035FC4
	private void OnDisable()
	{
		this.canTakePhoto = true;
		this.timer = 3f;
		if (this.flashLight)
		{
			this.flashLight.enabled = false;
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00037DF1 File Offset: 0x00035FF1
	private void TakePhoto()
	{
		if (this.canTakePhoto)
		{
			this.view.RPC("NetworkTakePhoto", PhotonTargets.All, Array.Empty<object>());
		}
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00037E11 File Offset: 0x00036011
	[PunRPC]
	private IEnumerator NetworkTakePhoto()
	{
		this.canTakePhoto = false;
		if (this.currentAmountOfPhotos > 0)
		{
			this.flashLight.enabled = true;
			base.StartCoroutine(this.PlayNoiseObject());
			this.source.Play();
			string text = "";
			int num = 0;
			this.currentAmountOfPhotos--;
			this.UpdateUIValue();
			bool flag = false;
			if (this.view.isMine)
			{
				Vector3 vector = this.cam.WorldToViewportPoint(LevelController.instance.currentGhost.raycastPoint.position);
				if (vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f && !Physics.Linecast(base.transform.position, LevelController.instance.currentGhost.raycastPoint.position, this.mask, QueryTriggerInteraction.Ignore) && LevelController.instance.currentGhost.ghostIsAppeared)
				{
					flag = true;
					if (MissionCapturePhoto.instance)
					{
						MissionCapturePhoto.instance.CompleteMission();
					}
					if (LevelController.instance.currentGhost.ghostInfo.ghostTraits.ghostType == GhostTraits.Type.Phantom)
					{
						LevelController.instance.currentGhost.UnAppear(false);
					}
					DailyChallengesController.Instance.ChangeChallengeProgression(ChallengeType.PhotoOfGhost, 1);
				}
				if (flag)
				{
					for (int i = 0; i < EvidenceController.instance.evidenceInLevel.Count; i++)
					{
						if (EvidenceController.instance.evidenceInLevel[i] != null && !EvidenceController.instance.evidenceInLevel[i].hasAlreadyTakenPhoto && EvidenceController.instance.evidenceInLevel[i].EvidenceType == Evidence.Type.ghost)
						{
							num = EvidenceController.instance.evidenceInLevel[i].GetEvidenceAmount();
							text = EvidenceController.instance.evidenceInLevel[i].evidenceName;
							break;
						}
					}
				}
				else
				{
					for (int j = 0; j < EvidenceController.instance.evidenceInLevel.Count; j++)
					{
						if (EvidenceController.instance.evidenceInLevel[j] != null && EvidenceController.instance.evidenceInLevel[j].gameObject.activeInHierarchy && EvidenceController.instance.evidenceInLevel[j].isActiveAndEnabled && !EvidenceController.instance.evidenceInLevel[j].hasAlreadyTakenPhoto)
						{
							vector = this.cam.WorldToViewportPoint(EvidenceController.instance.evidenceInLevel[j].transform.position);
							if (vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f && !Physics.Linecast(base.transform.position, EvidenceController.instance.evidenceInLevel[j].transform.position, this.mask, QueryTriggerInteraction.Ignore) && Vector3.Distance(base.transform.position, EvidenceController.instance.evidenceInLevel[j].transform.position) < 5f)
							{
								if (EvidenceController.instance.evidenceInLevel[j].EvidenceType != Evidence.Type.ghost || LevelController.instance.currentGhost.ghostIsAppeared)
								{
									num = EvidenceController.instance.evidenceInLevel[j].GetEvidenceAmount();
									if (MissionVictimName.instance && EvidenceController.instance.evidenceInLevel[j].showsGhostVictim)
									{
										MissionVictimName.instance.CompleteMission();
									}
									if (MissionDirtyWater.instance && EvidenceController.instance.evidenceInLevel[j].EvidenceType == Evidence.Type.dirtyWater)
									{
										MissionDirtyWater.instance.CompleteMission();
									}
									text = EvidenceController.instance.evidenceInLevel[j].evidenceName;
									break;
								}
								break;
							}
						}
					}
				}
				this.view.RPC("AddPhotoToJournal", PhotonTargets.All, new object[]
				{
					text,
					num
				});
			}
		}
		yield return new WaitForSeconds(0.125f);
		this.flashLight.enabled = false;
		yield break;
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00037E20 File Offset: 0x00036020
	private void UpdateUIValue()
	{
		if (this.currentAmountOfPhotos >= 0)
		{
			this.photosValueText.text = this.currentAmountOfPhotos.ToString();
		}
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00037E41 File Offset: 0x00036041
	private IEnumerator PlayNoiseObject()
	{
		this.noise.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		this.noise.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00037E50 File Offset: 0x00036050
	[PunRPC]
	private void AddPhotoToJournal(string evidenceName, int evidenceAmount)
	{
		RenderTexture renderTexture = new RenderTexture(this.resWidth, this.resHeight, 24);
		this.cam.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(this.resWidth, this.resHeight, TextureFormat.RGB24, false);
		this.cam.Render();
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, (float)this.resWidth, (float)this.resHeight), 0, 0);
		texture2D.Apply();
		this.cam.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(renderTexture);
		for (int i = 0; i < LevelController.instance.journals.Count; i++)
		{
			LevelController.instance.journals[i].AddPhoto(texture2D, evidenceName, evidenceAmount);
		}
		this.screen.material.mainTexture = texture2D;
		this.screen.material.color = Color.white;
		this.screen.material.SetTexture("_EmissionMap", texture2D);
		this.screen.material.SetColor("_EmissionColor", Color.white);
		this.screen.material.EnableKeyword("_EMISSION");
		int num = PlayerPrefs.GetInt("SavedPhotosIndex");
		byte[] bytes = texture2D.EncodeToPNG();
		File.WriteAllBytes(string.Concat(new object[]
		{
			Application.dataPath,
			"/../SavedScreen",
			num,
			".png"
		}), bytes);
		num++;
		if (num > 5)
		{
			num = 0;
		}
		PlayerPrefs.SetInt("SavedPhotosIndex", num);
	}

	// Token: 0x04000940 RID: 2368
	public Camera cam;

	// Token: 0x04000941 RID: 2369
	[SerializeField]
	private AudioSource source;

	// Token: 0x04000942 RID: 2370
	[SerializeField]
	private Light flashLight;

	// Token: 0x04000943 RID: 2371
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000944 RID: 2372
	[SerializeField]
	private PhotonObjectInteract photonInteract;

	// Token: 0x04000945 RID: 2373
	public LayerMask mask;

	// Token: 0x04000946 RID: 2374
	[SerializeField]
	private Noise noise;

	// Token: 0x04000947 RID: 2375
	private float timer = 2f;

	// Token: 0x04000948 RID: 2376
	private bool canTakePhoto = true;

	// Token: 0x04000949 RID: 2377
	private int currentAmountOfPhotos = 5;

	// Token: 0x0400094A RID: 2378
	[SerializeField]
	private Text photosValueText;

	// Token: 0x0400094B RID: 2379
	[SerializeField]
	private Renderer screen;

	// Token: 0x0400094C RID: 2380
	private int resWidth = 896;

	// Token: 0x0400094D RID: 2381
	private int resHeight = 504;
}
