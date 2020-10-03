using System;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000414 RID: 1044
	public class ControllerHoverHighlight : MonoBehaviour
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x0009F7D1 File Offset: 0x0009D9D1
		private void Start()
		{
			this.hand = base.GetComponentInParent<Hand>();
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0009F7DF File Offset: 0x0009D9DF
		private void Awake()
		{
			this.renderModelLoadedAction = SteamVR_Events.RenderModelLoadedAction(new UnityAction<SteamVR_RenderModel, bool>(this.OnRenderModelLoaded));
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0009F7F8 File Offset: 0x0009D9F8
		private void OnEnable()
		{
			this.renderModelLoadedAction.enabled = true;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0009F806 File Offset: 0x0009DA06
		private void OnDisable()
		{
			this.renderModelLoadedAction.enabled = false;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0009F814 File Offset: 0x0009DA14
		private void OnHandInitialized(int deviceIndex)
		{
			this.renderModel = base.gameObject.AddComponent<SteamVR_RenderModel>();
			this.renderModel.SetDeviceIndex(deviceIndex);
			this.renderModel.updateDynamically = false;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0009F840 File Offset: 0x0009DA40
		private void OnRenderModelLoaded(SteamVR_RenderModel renderModel, bool success)
		{
			if (renderModel != this.renderModel)
			{
				return;
			}
			Transform transform = base.transform.Find("body");
			if (transform != null)
			{
				transform.gameObject.layer = base.gameObject.layer;
				transform.gameObject.tag = base.gameObject.tag;
				this.bodyMeshRenderer = transform.GetComponent<MeshRenderer>();
				this.bodyMeshRenderer.material = this.highLightMaterial;
				this.bodyMeshRenderer.enabled = false;
			}
			Transform transform2 = base.transform.Find("trackhat");
			if (transform2 != null)
			{
				transform2.gameObject.layer = base.gameObject.layer;
				transform2.gameObject.tag = base.gameObject.tag;
				this.trackingHatMeshRenderer = transform2.GetComponent<MeshRenderer>();
				this.trackingHatMeshRenderer.material = this.highLightMaterial;
				this.trackingHatMeshRenderer.enabled = false;
			}
			foreach (object obj in base.transform)
			{
				Transform transform3 = (Transform)obj;
				if (transform3.name != "body" && transform3.name != "trackhat")
				{
					Object.Destroy(transform3.gameObject);
				}
			}
			this.renderModelLoaded = true;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0009F9B8 File Offset: 0x0009DBB8
		private void OnParentHandHoverBegin(Interactable other)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (other.transform.parent != base.transform.parent)
			{
				this.ShowHighlight();
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0009F9E6 File Offset: 0x0009DBE6
		private void OnParentHandHoverEnd(Interactable other)
		{
			this.HideHighlight();
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0009F9F0 File Offset: 0x0009DBF0
		private void OnParentHandInputFocusAcquired()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (this.hand.hoveringInteractable && this.hand.hoveringInteractable.transform.parent != base.transform.parent)
			{
				this.ShowHighlight();
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0009F9E6 File Offset: 0x0009DBE6
		private void OnParentHandInputFocusLost()
		{
			this.HideHighlight();
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0009FA48 File Offset: 0x0009DC48
		public void ShowHighlight()
		{
			if (!this.renderModelLoaded)
			{
				return;
			}
			if (this.fireHapticsOnHightlight)
			{
				this.hand.controller.TriggerHapticPulse(500, EVRButtonId.k_EButton_Axis0);
			}
			if (this.bodyMeshRenderer != null)
			{
				this.bodyMeshRenderer.enabled = true;
			}
			if (this.trackingHatMeshRenderer != null)
			{
				this.trackingHatMeshRenderer.enabled = true;
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0009FAB4 File Offset: 0x0009DCB4
		public void HideHighlight()
		{
			if (!this.renderModelLoaded)
			{
				return;
			}
			if (this.fireHapticsOnHightlight)
			{
				this.hand.controller.TriggerHapticPulse(300, EVRButtonId.k_EButton_Axis0);
			}
			if (this.bodyMeshRenderer != null)
			{
				this.bodyMeshRenderer.enabled = false;
			}
			if (this.trackingHatMeshRenderer != null)
			{
				this.trackingHatMeshRenderer.enabled = false;
			}
		}

		// Token: 0x04001DDB RID: 7643
		public Material highLightMaterial;

		// Token: 0x04001DDC RID: 7644
		public bool fireHapticsOnHightlight = true;

		// Token: 0x04001DDD RID: 7645
		private Hand hand;

		// Token: 0x04001DDE RID: 7646
		private MeshRenderer bodyMeshRenderer;

		// Token: 0x04001DDF RID: 7647
		private MeshRenderer trackingHatMeshRenderer;

		// Token: 0x04001DE0 RID: 7648
		private SteamVR_RenderModel renderModel;

		// Token: 0x04001DE1 RID: 7649
		private bool renderModelLoaded;

		// Token: 0x04001DE2 RID: 7650
		private SteamVR_Events.Action renderModelLoadedAction;
	}
}
