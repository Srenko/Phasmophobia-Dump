using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000432 RID: 1074
	public class SeeThru : MonoBehaviour
	{
		// Token: 0x060020EE RID: 8430 RVA: 0x000A27FC File Offset: 0x000A09FC
		private void Awake()
		{
			this.interactable = base.GetComponentInParent<Interactable>();
			this.seeThru = new GameObject("_see_thru");
			this.seeThru.transform.parent = base.transform;
			this.seeThru.transform.localPosition = Vector3.zero;
			this.seeThru.transform.localRotation = Quaternion.identity;
			this.seeThru.transform.localScale = Vector3.one;
			MeshFilter component = base.GetComponent<MeshFilter>();
			if (component != null)
			{
				this.seeThru.AddComponent<MeshFilter>().sharedMesh = component.sharedMesh;
			}
			MeshRenderer component2 = base.GetComponent<MeshRenderer>();
			if (component2 != null)
			{
				this.sourceRenderer = component2;
				this.destRenderer = this.seeThru.AddComponent<MeshRenderer>();
			}
			SkinnedMeshRenderer component3 = base.GetComponent<SkinnedMeshRenderer>();
			if (component3 != null)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = this.seeThru.AddComponent<SkinnedMeshRenderer>();
				this.sourceRenderer = component3;
				this.destRenderer = skinnedMeshRenderer;
				skinnedMeshRenderer.sharedMesh = component3.sharedMesh;
				skinnedMeshRenderer.rootBone = component3.rootBone;
				skinnedMeshRenderer.bones = component3.bones;
				skinnedMeshRenderer.quality = component3.quality;
				skinnedMeshRenderer.updateWhenOffscreen = component3.updateWhenOffscreen;
			}
			if (this.sourceRenderer != null && this.destRenderer != null)
			{
				int num = this.sourceRenderer.sharedMaterials.Length;
				Material[] array = new Material[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.seeThruMaterial;
				}
				this.destRenderer.sharedMaterials = array;
				for (int j = 0; j < this.destRenderer.materials.Length; j++)
				{
					this.destRenderer.materials[j].renderQueue = 2001;
				}
				for (int k = 0; k < this.sourceRenderer.materials.Length; k++)
				{
					if (this.sourceRenderer.materials[k].renderQueue == 2000)
					{
						this.sourceRenderer.materials[k].renderQueue = 2002;
					}
				}
			}
			this.seeThru.gameObject.SetActive(false);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000A2A24 File Offset: 0x000A0C24
		private void OnEnable()
		{
			this.interactable.onAttachedToHand += this.AttachedToHand;
			this.interactable.onDetachedFromHand += this.DetachedFromHand;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000A2A54 File Offset: 0x000A0C54
		private void OnDisable()
		{
			this.interactable.onAttachedToHand -= this.AttachedToHand;
			this.interactable.onDetachedFromHand -= this.DetachedFromHand;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000A2A84 File Offset: 0x000A0C84
		private void AttachedToHand(Hand hand)
		{
			this.seeThru.SetActive(true);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000A2A92 File Offset: 0x000A0C92
		private void DetachedFromHand(Hand hand)
		{
			this.seeThru.SetActive(false);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000A2AA0 File Offset: 0x000A0CA0
		private void Update()
		{
			if (this.seeThru.activeInHierarchy)
			{
				int num = Mathf.Min(this.sourceRenderer.materials.Length, this.destRenderer.materials.Length);
				for (int i = 0; i < num; i++)
				{
					this.destRenderer.materials[i].mainTexture = this.sourceRenderer.materials[i].mainTexture;
					this.destRenderer.materials[i].color = this.destRenderer.materials[i].color * this.sourceRenderer.materials[i].color;
				}
			}
		}

		// Token: 0x04001E7B RID: 7803
		public Material seeThruMaterial;

		// Token: 0x04001E7C RID: 7804
		private GameObject seeThru;

		// Token: 0x04001E7D RID: 7805
		private Interactable interactable;

		// Token: 0x04001E7E RID: 7806
		private Renderer sourceRenderer;

		// Token: 0x04001E7F RID: 7807
		private Renderer destRenderer;
	}
}
