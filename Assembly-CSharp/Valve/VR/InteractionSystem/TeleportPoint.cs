using System;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000455 RID: 1109
	public class TeleportPoint : TeleportMarkerBase
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000694A8 File Offset: 0x000676A8
		public override bool showReticle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000A9CD8 File Offset: 0x000A7ED8
		private void Awake()
		{
			this.GetRelevantComponents();
			this.animation = base.GetComponent<Animation>();
			this.tintColorID = Shader.PropertyToID("_TintColor");
			this.moveLocationIcon.gameObject.SetActive(false);
			this.switchSceneIcon.gameObject.SetActive(false);
			this.lockedIcon.gameObject.SetActive(false);
			this.UpdateVisuals();
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000A9D40 File Offset: 0x000A7F40
		private void Start()
		{
			this.player = Player.instance;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000A9D50 File Offset: 0x000A7F50
		private void Update()
		{
			if (Application.isPlaying)
			{
				this.lookAtPosition.x = this.player.hmdTransform.position.x;
				this.lookAtPosition.y = this.lookAtJointTransform.position.y;
				this.lookAtPosition.z = this.player.hmdTransform.position.z;
				this.lookAtJointTransform.LookAt(this.lookAtPosition);
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000A9DD0 File Offset: 0x000A7FD0
		public override bool ShouldActivate(Vector3 playerPosition)
		{
			return Vector3.Distance(base.transform.position, playerPosition) > 1f;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000694B6 File Offset: 0x000676B6
		public override bool ShouldMovePlayer()
		{
			return true;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000A9DEC File Offset: 0x000A7FEC
		public override void Highlight(bool highlight)
		{
			if (!this.locked)
			{
				if (highlight)
				{
					this.SetMeshMaterials(Teleport.instance.pointHighlightedMaterial, this.titleHighlightedColor);
				}
				else
				{
					this.SetMeshMaterials(Teleport.instance.pointVisibleMaterial, this.titleVisibleColor);
				}
			}
			if (highlight)
			{
				this.pointIcon.gameObject.SetActive(true);
				this.animation.Play();
				return;
			}
			this.pointIcon.gameObject.SetActive(false);
			this.animation.Stop();
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000A9E70 File Offset: 0x000A8070
		public override void UpdateVisuals()
		{
			if (!this.gotReleventComponents)
			{
				return;
			}
			if (this.locked)
			{
				this.SetMeshMaterials(Teleport.instance.pointLockedMaterial, this.titleLockedColor);
				this.pointIcon = this.lockedIcon;
				this.animation.clip = this.animation.GetClip("locked_idle");
			}
			else
			{
				this.SetMeshMaterials(Teleport.instance.pointVisibleMaterial, this.titleVisibleColor);
				TeleportPoint.TeleportPointType teleportPointType = this.teleportType;
				if (teleportPointType != TeleportPoint.TeleportPointType.MoveToLocation)
				{
					if (teleportPointType == TeleportPoint.TeleportPointType.SwitchToNewScene)
					{
						this.pointIcon = this.switchSceneIcon;
						this.animation.clip = this.animation.GetClip("switch_scenes_idle");
					}
				}
				else
				{
					this.pointIcon = this.moveLocationIcon;
					this.animation.clip = this.animation.GetClip("move_location_idle");
				}
			}
			this.titleText.text = this.title;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000A9F54 File Offset: 0x000A8154
		public override void SetAlpha(float tintAlpha, float alphaPercent)
		{
			this.tintColor = this.markerMesh.material.GetColor(this.tintColorID);
			this.tintColor.a = tintAlpha;
			this.markerMesh.material.SetColor(this.tintColorID, this.tintColor);
			this.switchSceneIcon.material.SetColor(this.tintColorID, this.tintColor);
			this.moveLocationIcon.material.SetColor(this.tintColorID, this.tintColor);
			this.lockedIcon.material.SetColor(this.tintColorID, this.tintColor);
			this.titleColor.a = this.fullTitleAlpha * alphaPercent;
			this.titleText.color = this.titleColor;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000AA020 File Offset: 0x000A8220
		public void SetMeshMaterials(Material material, Color textColor)
		{
			this.markerMesh.material = material;
			this.switchSceneIcon.material = material;
			this.moveLocationIcon.material = material;
			this.lockedIcon.material = material;
			this.titleColor = textColor;
			this.fullTitleAlpha = textColor.a;
			this.titleText.color = this.titleColor;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000AA081 File Offset: 0x000A8281
		public void TeleportToScene()
		{
			if (!string.IsNullOrEmpty(this.switchToScene))
			{
				Debug.Log("TeleportPoint: Hook up your level loading logic to switch to new scene: " + this.switchToScene);
				return;
			}
			Debug.LogError("TeleportPoint: Invalid scene name to switch to: " + this.switchToScene);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000AA0BC File Offset: 0x000A82BC
		public void GetRelevantComponents()
		{
			this.markerMesh = base.transform.Find("teleport_marker_mesh").GetComponent<MeshRenderer>();
			this.switchSceneIcon = base.transform.Find("teleport_marker_lookat_joint/teleport_marker_icons/switch_scenes_icon").GetComponent<MeshRenderer>();
			this.moveLocationIcon = base.transform.Find("teleport_marker_lookat_joint/teleport_marker_icons/move_location_icon").GetComponent<MeshRenderer>();
			this.lockedIcon = base.transform.Find("teleport_marker_lookat_joint/teleport_marker_icons/locked_icon").GetComponent<MeshRenderer>();
			this.lookAtJointTransform = base.transform.Find("teleport_marker_lookat_joint");
			this.titleText = base.transform.Find("teleport_marker_lookat_joint/teleport_marker_canvas/teleport_marker_canvas_text").GetComponent<Text>();
			this.gotReleventComponents = true;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000AA16D File Offset: 0x000A836D
		public void ReleaseRelevantComponents()
		{
			this.markerMesh = null;
			this.switchSceneIcon = null;
			this.moveLocationIcon = null;
			this.lockedIcon = null;
			this.lookAtJointTransform = null;
			this.titleText = null;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000AA19C File Offset: 0x000A839C
		public void UpdateVisualsInEditor()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this.GetRelevantComponents();
			if (this.locked)
			{
				this.lockedIcon.gameObject.SetActive(true);
				this.moveLocationIcon.gameObject.SetActive(false);
				this.switchSceneIcon.gameObject.SetActive(false);
				this.markerMesh.sharedMaterial = Teleport.instance.pointLockedMaterial;
				this.lockedIcon.sharedMaterial = Teleport.instance.pointLockedMaterial;
				this.titleText.color = this.titleLockedColor;
			}
			else
			{
				this.lockedIcon.gameObject.SetActive(false);
				this.markerMesh.sharedMaterial = Teleport.instance.pointVisibleMaterial;
				this.switchSceneIcon.sharedMaterial = Teleport.instance.pointVisibleMaterial;
				this.moveLocationIcon.sharedMaterial = Teleport.instance.pointVisibleMaterial;
				this.titleText.color = this.titleVisibleColor;
				TeleportPoint.TeleportPointType teleportPointType = this.teleportType;
				if (teleportPointType != TeleportPoint.TeleportPointType.MoveToLocation)
				{
					if (teleportPointType == TeleportPoint.TeleportPointType.SwitchToNewScene)
					{
						this.moveLocationIcon.gameObject.SetActive(false);
						this.switchSceneIcon.gameObject.SetActive(true);
					}
				}
				else
				{
					this.moveLocationIcon.gameObject.SetActive(true);
					this.switchSceneIcon.gameObject.SetActive(false);
				}
			}
			this.titleText.text = this.title;
			this.ReleaseRelevantComponents();
		}

		// Token: 0x04001FD2 RID: 8146
		public TeleportPoint.TeleportPointType teleportType;

		// Token: 0x04001FD3 RID: 8147
		public string title;

		// Token: 0x04001FD4 RID: 8148
		public string switchToScene;

		// Token: 0x04001FD5 RID: 8149
		public Color titleVisibleColor;

		// Token: 0x04001FD6 RID: 8150
		public Color titleHighlightedColor;

		// Token: 0x04001FD7 RID: 8151
		public Color titleLockedColor;

		// Token: 0x04001FD8 RID: 8152
		public bool playerSpawnPoint;

		// Token: 0x04001FD9 RID: 8153
		private bool gotReleventComponents;

		// Token: 0x04001FDA RID: 8154
		private MeshRenderer markerMesh;

		// Token: 0x04001FDB RID: 8155
		private MeshRenderer switchSceneIcon;

		// Token: 0x04001FDC RID: 8156
		private MeshRenderer moveLocationIcon;

		// Token: 0x04001FDD RID: 8157
		private MeshRenderer lockedIcon;

		// Token: 0x04001FDE RID: 8158
		private MeshRenderer pointIcon;

		// Token: 0x04001FDF RID: 8159
		private Transform lookAtJointTransform;

		// Token: 0x04001FE0 RID: 8160
		private Animation animation;

		// Token: 0x04001FE1 RID: 8161
		private Text titleText;

		// Token: 0x04001FE2 RID: 8162
		private Player player;

		// Token: 0x04001FE3 RID: 8163
		private Vector3 lookAtPosition = Vector3.zero;

		// Token: 0x04001FE4 RID: 8164
		private int tintColorID;

		// Token: 0x04001FE5 RID: 8165
		private Color tintColor = Color.clear;

		// Token: 0x04001FE6 RID: 8166
		private Color titleColor = Color.clear;

		// Token: 0x04001FE7 RID: 8167
		private float fullTitleAlpha;

		// Token: 0x04001FE8 RID: 8168
		private const string switchSceneAnimation = "switch_scenes_idle";

		// Token: 0x04001FE9 RID: 8169
		private const string moveLocationAnimation = "move_location_idle";

		// Token: 0x04001FEA RID: 8170
		private const string lockedAnimation = "locked_idle";

		// Token: 0x0200078E RID: 1934
		public enum TeleportPointType
		{
			// Token: 0x04002980 RID: 10624
			MoveToLocation,
			// Token: 0x04002981 RID: 10625
			SwitchToNewScene
		}
	}
}
