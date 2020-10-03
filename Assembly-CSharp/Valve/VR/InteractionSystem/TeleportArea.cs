using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000453 RID: 1107
	public class TeleportArea : TeleportMarkerBase
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000A9A9D File Offset: 0x000A7C9D
		// (set) Token: 0x06002221 RID: 8737 RVA: 0x000A9AA5 File Offset: 0x000A7CA5
		public Bounds meshBounds { get; private set; }

		// Token: 0x06002222 RID: 8738 RVA: 0x000A9AAE File Offset: 0x000A7CAE
		public void Awake()
		{
			this.areaMesh = base.GetComponent<MeshRenderer>();
			this.tintColorId = Shader.PropertyToID("_TintColor");
			this.CalculateBounds();
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000A9AD4 File Offset: 0x000A7CD4
		public void Start()
		{
			this.visibleTintColor = Teleport.instance.areaVisibleMaterial.GetColor(this.tintColorId);
			this.highlightedTintColor = Teleport.instance.areaHighlightedMaterial.GetColor(this.tintColorId);
			this.lockedTintColor = Teleport.instance.areaLockedMaterial.GetColor(this.tintColorId);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000694B6 File Offset: 0x000676B6
		public override bool ShouldActivate(Vector3 playerPosition)
		{
			return true;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000694B6 File Offset: 0x000676B6
		public override bool ShouldMovePlayer()
		{
			return true;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000A9B32 File Offset: 0x000A7D32
		public override void Highlight(bool highlight)
		{
			if (!this.locked)
			{
				this.highlighted = highlight;
				if (highlight)
				{
					this.areaMesh.material = Teleport.instance.areaHighlightedMaterial;
					return;
				}
				this.areaMesh.material = Teleport.instance.areaVisibleMaterial;
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000A9B74 File Offset: 0x000A7D74
		public override void SetAlpha(float tintAlpha, float alphaPercent)
		{
			Color tintColor = this.GetTintColor();
			tintColor.a *= alphaPercent;
			this.areaMesh.material.SetColor(this.tintColorId, tintColor);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000A9BAB File Offset: 0x000A7DAB
		public override void UpdateVisuals()
		{
			if (this.locked)
			{
				this.areaMesh.material = Teleport.instance.areaLockedMaterial;
				return;
			}
			this.areaMesh.material = Teleport.instance.areaVisibleMaterial;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000A9BE0 File Offset: 0x000A7DE0
		public void UpdateVisualsInEditor()
		{
			this.areaMesh = base.GetComponent<MeshRenderer>();
			if (this.locked)
			{
				this.areaMesh.sharedMaterial = Teleport.instance.areaLockedMaterial;
				return;
			}
			this.areaMesh.sharedMaterial = Teleport.instance.areaVisibleMaterial;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000A9C2C File Offset: 0x000A7E2C
		private bool CalculateBounds()
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			if (component == null)
			{
				return false;
			}
			Mesh sharedMesh = component.sharedMesh;
			if (sharedMesh == null)
			{
				return false;
			}
			this.meshBounds = sharedMesh.bounds;
			return true;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000A9C6A File Offset: 0x000A7E6A
		private Color GetTintColor()
		{
			if (this.locked)
			{
				return this.lockedTintColor;
			}
			if (this.highlighted)
			{
				return this.highlightedTintColor;
			}
			return this.visibleTintColor;
		}

		// Token: 0x04001FCA RID: 8138
		private MeshRenderer areaMesh;

		// Token: 0x04001FCB RID: 8139
		private int tintColorId;

		// Token: 0x04001FCC RID: 8140
		private Color visibleTintColor = Color.clear;

		// Token: 0x04001FCD RID: 8141
		private Color highlightedTintColor = Color.clear;

		// Token: 0x04001FCE RID: 8142
		private Color lockedTintColor = Color.clear;

		// Token: 0x04001FCF RID: 8143
		private bool highlighted;
	}
}
