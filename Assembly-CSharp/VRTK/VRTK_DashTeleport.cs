using System;
using System.Collections;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002CD RID: 717
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_DashTeleport")]
	public class VRTK_DashTeleport : VRTK_HeightAdjustTeleport
	{
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x060017C8 RID: 6088 RVA: 0x0007EFA4 File Offset: 0x0007D1A4
		// (remove) Token: 0x060017C9 RID: 6089 RVA: 0x0007EFDC File Offset: 0x0007D1DC
		public event DashTeleportEventHandler WillDashThruObjects;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x060017CA RID: 6090 RVA: 0x0007F014 File Offset: 0x0007D214
		// (remove) Token: 0x060017CB RID: 6091 RVA: 0x0007F04C File Offset: 0x0007D24C
		public event DashTeleportEventHandler DashedThruObjects;

		// Token: 0x060017CC RID: 6092 RVA: 0x0007F081 File Offset: 0x0007D281
		public virtual void OnWillDashThruObjects(DashTeleportEventArgs e)
		{
			if (this.WillDashThruObjects != null)
			{
				this.WillDashThruObjects(this, e);
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0007F098 File Offset: 0x0007D298
		public virtual void OnDashedThruObjects(DashTeleportEventArgs e)
		{
			if (this.DashedThruObjects != null)
			{
				this.DashedThruObjects(this, e);
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0007F0AF File Offset: 0x0007D2AF
		protected override void OnEnable()
		{
			base.OnEnable();
			this.minDistanceForNormalLerp = this.minSpeedMps * this.normalLerpTime;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0007F0CA File Offset: 0x0007D2CA
		protected override Vector3 SetNewPosition(Vector3 position, Transform target, bool forceDestinationPosition)
		{
			return this.CheckTerrainCollision(position, target, forceDestinationPosition);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0007F0D5 File Offset: 0x0007D2D5
		protected override void StartTeleport(object sender, DestinationMarkerEventArgs e)
		{
			base.StartTeleport(sender, e);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0007F0DF File Offset: 0x0007D2DF
		protected override void ProcessOrientation(object sender, DestinationMarkerEventArgs e, Vector3 newPosition, Quaternion newRotation)
		{
			if (this.ValidRigObjects())
			{
				base.StartCoroutine(this.lerpToPosition(sender, e, newPosition));
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00003F60 File Offset: 0x00002160
		protected override void EndTeleport(object sender, DestinationMarkerEventArgs e)
		{
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0007F0F9 File Offset: 0x0007D2F9
		protected virtual IEnumerator lerpToPosition(object sender, DestinationMarkerEventArgs e, Vector3 targetPosition)
		{
			this.enableTeleport = false;
			bool gameObjectInTheWay = false;
			Vector3 position = this.headset.transform.position;
			Vector3 a = new Vector3(position.x, this.playArea.position.y, position.z);
			Vector3 b = position - this.playArea.position;
			Vector3 normalized = (targetPosition + b - position).normalized;
			Vector3 point = a + Vector3.up * this.capsuleBottomOffset + normalized;
			Vector3 point2 = position + Vector3.up * this.capsuleTopOffset + normalized;
			float num = Vector3.Distance(this.playArea.position, targetPosition - normalized * 0.5f);
			RaycastHit[] allHits = Physics.CapsuleCastAll(point, point2, this.capsuleRadius, normalized, num);
			foreach (RaycastHit raycastHit in allHits)
			{
				gameObjectInTheWay = (raycastHit.collider.gameObject != e.target.gameObject);
			}
			if (gameObjectInTheWay)
			{
				this.OnWillDashThruObjects(this.SetDashTeleportEvent(allHits));
			}
			if (num >= this.minDistanceForNormalLerp)
			{
				this.lerpTime = this.normalLerpTime;
			}
			else
			{
				this.lerpTime = 1f / this.minSpeedMps * num;
			}
			Vector3 startPosition = new Vector3(this.playArea.position.x, this.playArea.position.y, this.playArea.position.z);
			float elapsedTime = 0f;
			float t = 0f;
			while (t < 1f)
			{
				this.playArea.position = Vector3.Lerp(startPosition, targetPosition, t);
				elapsedTime += Time.deltaTime;
				t = elapsedTime / this.lerpTime;
				if (t > 1f)
				{
					if (this.playArea.position != targetPosition)
					{
						this.playArea.position = targetPosition;
					}
					t = 1f;
				}
				yield return new WaitForEndOfFrame();
			}
			if (gameObjectInTheWay)
			{
				this.OnDashedThruObjects(this.SetDashTeleportEvent(allHits));
			}
			base.EndTeleport(sender, e);
			gameObjectInTheWay = false;
			this.enableTeleport = true;
			yield break;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0007F120 File Offset: 0x0007D320
		protected virtual DashTeleportEventArgs SetDashTeleportEvent(RaycastHit[] hits)
		{
			DashTeleportEventArgs result;
			result.hits = hits;
			return result;
		}

		// Token: 0x04001357 RID: 4951
		[Header("Dash Settings")]
		[Tooltip("The fixed time it takes to dash to a new position.")]
		public float normalLerpTime = 0.1f;

		// Token: 0x04001358 RID: 4952
		[Tooltip("The minimum speed for dashing in meters per second.")]
		public float minSpeedMps = 50f;

		// Token: 0x04001359 RID: 4953
		[Tooltip("The Offset of the CapsuleCast above the camera.")]
		public float capsuleTopOffset = 0.2f;

		// Token: 0x0400135A RID: 4954
		[Tooltip("The Offset of the CapsuleCast below the camera.")]
		public float capsuleBottomOffset = 0.5f;

		// Token: 0x0400135B RID: 4955
		[Tooltip("The radius of the CapsuleCast.")]
		public float capsuleRadius = 0.5f;

		// Token: 0x0400135E RID: 4958
		protected float minDistanceForNormalLerp;

		// Token: 0x0400135F RID: 4959
		protected float lerpTime = 0.1f;
	}
}
