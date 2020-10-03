using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002CE RID: 718
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_HeightAdjustTeleport")]
	public class VRTK_HeightAdjustTeleport : VRTK_BasicTeleport
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x0007F197 File Offset: 0x0007D397
		protected override void OnEnable()
		{
			base.OnEnable();
			this.adjustYForTerrain = true;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0007F1A6 File Offset: 0x0007D3A6
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0007F1B0 File Offset: 0x0007D3B0
		protected override Vector3 GetNewPosition(Vector3 tipPosition, Transform target, bool returnOriginalPosition)
		{
			Vector3 newPosition = base.GetNewPosition(tipPosition, target, returnOriginalPosition);
			if (!returnOriginalPosition)
			{
				newPosition.y = this.GetTeleportY(target, tipPosition);
			}
			return newPosition;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0007F1DC File Offset: 0x0007D3DC
		protected virtual float GetTeleportY(Transform target, Vector3 tipPosition)
		{
			if (!this.snapToNearestFloor || !this.ValidRigObjects())
			{
				return tipPosition.y;
			}
			float result = this.playArea.position.y;
			float num = 0.1f;
			Vector3 b = Vector3.up * num;
			Ray ray = new Ray(tipPosition + b, -this.playArea.up);
			RaycastHit raycastHit;
			if (target != null && VRTK_CustomRaycast.Raycast(this.customRaycast, ray, out raycastHit, this.layersToIgnore, float.PositiveInfinity, QueryTriggerInteraction.Ignore))
			{
				result = tipPosition.y - raycastHit.distance + num;
			}
			return result;
		}

		// Token: 0x04001360 RID: 4960
		[Header("Height Adjust Settings")]
		[Tooltip("If this is checked, then the teleported Y position will snap to the nearest available below floor. If it is unchecked, then the teleported Y position will be where ever the destination Y position is.")]
		public bool snapToNearestFloor = true;

		// Token: 0x04001361 RID: 4961
		[Tooltip("A custom raycaster to use when raycasting to find floors.")]
		public VRTK_CustomRaycast customRaycast;

		// Token: 0x04001362 RID: 4962
		[Tooltip("**OBSOLETE [Use customRaycast]** The layers to ignore when raycasting to find floors.")]
		[Obsolete("`VRTK_HeightAdjustTeleport.layersToIgnore` is no longer used in the `VRTK_HeightAdjustTeleport` class, use the `customRaycast` parameter instead. This parameter will be removed in a future version of VRTK.")]
		public LayerMask layersToIgnore = 4;
	}
}
