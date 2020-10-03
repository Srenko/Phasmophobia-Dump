using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000438 RID: 1080
	public class SpawnRenderModel : MonoBehaviour
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x000A2D93 File Offset: 0x000A0F93
		private void Awake()
		{
			this.renderModels = new SteamVR_RenderModel[this.materials.Length];
			this.renderModelLoadedAction = SteamVR_Events.RenderModelLoadedAction(new UnityAction<SteamVR_RenderModel, bool>(this.OnRenderModelLoaded));
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000A2DBF File Offset: 0x000A0FBF
		private void OnEnable()
		{
			this.ShowController();
			this.renderModelLoadedAction.enabled = true;
			SpawnRenderModel.spawnRenderModels.Add(this);
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000A2DDE File Offset: 0x000A0FDE
		private void OnDisable()
		{
			this.HideController();
			this.renderModelLoadedAction.enabled = false;
			SpawnRenderModel.spawnRenderModels.Remove(this);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000A2DFE File Offset: 0x000A0FFE
		private void OnAttachedToHand(Hand hand)
		{
			this.hand = hand;
			this.ShowController();
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000A2E0D File Offset: 0x000A100D
		private void OnDetachedFromHand(Hand hand)
		{
			this.hand = null;
			this.HideController();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000A2E1C File Offset: 0x000A101C
		private void Update()
		{
			if (SpawnRenderModel.lastFrameUpdated == Time.renderedFrameCount)
			{
				return;
			}
			SpawnRenderModel.lastFrameUpdated = Time.renderedFrameCount;
			if (SpawnRenderModel.spawnRenderModelUpdateIndex >= SpawnRenderModel.spawnRenderModels.Count)
			{
				SpawnRenderModel.spawnRenderModelUpdateIndex = 0;
			}
			if (SpawnRenderModel.spawnRenderModelUpdateIndex < SpawnRenderModel.spawnRenderModels.Count)
			{
				SteamVR_RenderModel steamVR_RenderModel = SpawnRenderModel.spawnRenderModels[SpawnRenderModel.spawnRenderModelUpdateIndex].renderModels[0];
				if (steamVR_RenderModel != null)
				{
					steamVR_RenderModel.UpdateComponents(OpenVR.RenderModels);
				}
			}
			SpawnRenderModel.spawnRenderModelUpdateIndex++;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000A2EA0 File Offset: 0x000A10A0
		private void ShowController()
		{
			if (this.hand == null || this.hand.controller == null)
			{
				return;
			}
			for (int i = 0; i < this.renderModels.Length; i++)
			{
				if (this.renderModels[i] == null)
				{
					this.renderModels[i] = new GameObject("SteamVR_RenderModel").AddComponent<SteamVR_RenderModel>();
					this.renderModels[i].updateDynamically = false;
					this.renderModels[i].transform.parent = base.transform;
					Util.ResetTransform(this.renderModels[i].transform, true);
				}
				this.renderModels[i].gameObject.SetActive(true);
				this.renderModels[i].SetDeviceIndex((int)this.hand.controller.index);
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000A2F74 File Offset: 0x000A1174
		private void HideController()
		{
			for (int i = 0; i < this.renderModels.Length; i++)
			{
				if (this.renderModels[i] != null)
				{
					this.renderModels[i].gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000A2FB8 File Offset: 0x000A11B8
		private void OnRenderModelLoaded(SteamVR_RenderModel renderModel, bool success)
		{
			for (int i = 0; i < this.renderModels.Length; i++)
			{
				if (renderModel == this.renderModels[i] && this.materials[i] != null)
				{
					this.renderers.Clear();
					this.renderModels[i].GetComponentsInChildren<MeshRenderer>(this.renderers);
					for (int j = 0; j < this.renderers.Count; j++)
					{
						Texture mainTexture = this.renderers[j].material.mainTexture;
						this.renderers[j].sharedMaterial = this.materials[i];
						this.renderers[j].material.mainTexture = mainTexture;
						this.renderers[j].gameObject.layer = base.gameObject.layer;
						this.renderers[j].tag = base.gameObject.tag;
					}
				}
			}
		}

		// Token: 0x04001E8D RID: 7821
		public Material[] materials;

		// Token: 0x04001E8E RID: 7822
		private SteamVR_RenderModel[] renderModels;

		// Token: 0x04001E8F RID: 7823
		private Hand hand;

		// Token: 0x04001E90 RID: 7824
		private List<MeshRenderer> renderers = new List<MeshRenderer>();

		// Token: 0x04001E91 RID: 7825
		private static List<SpawnRenderModel> spawnRenderModels = new List<SpawnRenderModel>();

		// Token: 0x04001E92 RID: 7826
		private static int lastFrameUpdated;

		// Token: 0x04001E93 RID: 7827
		private static int spawnRenderModelUpdateIndex;

		// Token: 0x04001E94 RID: 7828
		private SteamVR_Events.Action renderModelLoadedAction;
	}
}
