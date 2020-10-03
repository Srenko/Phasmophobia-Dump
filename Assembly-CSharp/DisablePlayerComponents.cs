using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using RootMotion.FinalIK;
using UnityEngine;
using VRTK;

// Token: 0x020001A6 RID: 422
[RequireComponent(typeof(PhotonView))]
public class DisablePlayerComponents : Photon.MonoBehaviour
{
	// Token: 0x06000B65 RID: 2917 RVA: 0x00045E72 File Offset: 0x00044072
	private void Start()
	{
		this.SetupPlayer();
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x00045E7C File Offset: 0x0004407C
	private void SetupPlayer()
	{
		if (!GameController.instance)
		{
			this.shadowCamera.gameObject.SetActive(false);
		}
		if (PhotonNetwork.inRoom)
		{
			if (!this.view.isMine)
			{
				for (int i = 0; i < this.scriptsToDisable.Count; i++)
				{
					Object.Destroy(this.scriptsToDisable[i]);
				}
				for (int j = 0; j < this.objectsToDisable.Count; j++)
				{
					this.objectsToDisable[j].SetActive(false);
				}
				for (int k = 0; k < this.camerasToDisable.Count; k++)
				{
					this.camerasToDisable[k].enabled = false;
					this.camerasToDisable[k].tag = "Untagged";
				}
				Object.Destroy(this.player.postProcessingLayer);
				Object.Destroy(this.player.postProcessingVolume);
				GameObject[] array = this.characterModels;
				for (int l = 0; l < array.Length; l++)
				{
					foreach (object obj in array[l].transform)
					{
						((Transform)obj).gameObject.layer = 0;
					}
				}
				for (int m = 0; m < this.shoulderFlashlights.Length; m++)
				{
					this.shoulderFlashlights[m].layer = 0;
				}
				this.shoulderFlashlightLight.enabled = false;
				this.eye.SetParent(this.head.parent);
				this.head.SetParent(this.eye);
				this.VRRig.SetActive(true);
				this.shadowCamera.gameObject.SetActive(false);
			}
			else
			{
				this.view.RPC("LoadPlayerModel", PhotonTargets.AllBuffered, new object[]
				{
					PlayerPrefs.GetInt("CharacterIndex")
				});
				if (MainManager.instance == null)
				{
					this.journalObject = PhotonNetwork.Instantiate(this.vrJournal.name, base.transform.position, Quaternion.identity, 0);
				}
			}
		}
		else
		{
			if (MainManager.instance == null)
			{
				Object.Instantiate(Resources.Load(this.vrJournal.name), base.transform.position, Quaternion.identity);
			}
			for (int n = 0; n < this.transformViews.Length; n++)
			{
				this.transformViews[n].enabled = false;
			}
		}
		this.VRPlayer.SetActive(true);
		if (this.view.isMine || !PhotonNetwork.inRoom)
		{
			this.sdkManager.TryLoadSDKSetupFromList(true);
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00046144 File Offset: 0x00044344
	private IEnumerator DisableBeltDelay()
	{
		yield return new WaitForSeconds(0.2f);
		this.vrBelt.SetActive(false);
		yield break;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00046154 File Offset: 0x00044354
	[PunRPC]
	private void LoadPlayerModel(int index)
	{
		this.player.modelID = index;
		this.characterModels[this.player.modelID].SetActive(true);
		this.player.charAnim = this.characterModels[this.player.modelID].GetComponent<Animator>();
		this.player.movementSettings.anim = this.characterModels[this.player.modelID].GetComponent<Animator>();
		for (int i = 0; i < this.vriks.Length; i++)
		{
			this.vriks[i].solver.spine.headTarget = ((index < 5) ? this.femaleHeadTarget : this.maleHeadTarget);
		}
	}

	// Token: 0x04000B92 RID: 2962
	public List<GameObject> objectsToDisable = new List<GameObject>();

	// Token: 0x04000B93 RID: 2963
	public List<UnityEngine.MonoBehaviour> scriptsToDisable = new List<UnityEngine.MonoBehaviour>();

	// Token: 0x04000B94 RID: 2964
	public List<Camera> camerasToDisable = new List<Camera>();

	// Token: 0x04000B95 RID: 2965
	public GameObject VRRig;

	// Token: 0x04000B96 RID: 2966
	public GameObject VRPlayer;

	// Token: 0x04000B97 RID: 2967
	public Transform eye;

	// Token: 0x04000B98 RID: 2968
	public Transform head;

	// Token: 0x04000B99 RID: 2969
	[SerializeField]
	private Camera shadowCamera;

	// Token: 0x04000B9A RID: 2970
	[SerializeField]
	private Player player;

	// Token: 0x04000B9B RID: 2971
	[SerializeField]
	private PhotonView view;

	// Token: 0x04000B9C RID: 2972
	[SerializeField]
	private GameObject[] shoulderFlashlights;

	// Token: 0x04000B9D RID: 2973
	[SerializeField]
	private Light shoulderFlashlightLight;

	// Token: 0x04000B9E RID: 2974
	[SerializeField]
	private GameObject[] characterModels;

	// Token: 0x04000B9F RID: 2975
	[SerializeField]
	private GameObject vrJournal;

	// Token: 0x04000BA0 RID: 2976
	[SerializeField]
	private PhotonTransformView[] transformViews;

	// Token: 0x04000BA1 RID: 2977
	[SerializeField]
	private VRTK_SDKManager sdkManager;

	// Token: 0x04000BA2 RID: 2978
	[SerializeField]
	private GameObject playArea;

	// Token: 0x04000BA3 RID: 2979
	[SerializeField]
	private GameObject vrBelt;

	// Token: 0x04000BA4 RID: 2980
	private GameObject journalObject;

	// Token: 0x04000BA5 RID: 2981
	[SerializeField]
	private VRIK[] vriks;

	// Token: 0x04000BA6 RID: 2982
	[SerializeField]
	private Transform maleHeadTarget;

	// Token: 0x04000BA7 RID: 2983
	[SerializeField]
	private Transform femaleHeadTarget;
}
