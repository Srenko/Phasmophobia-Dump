using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002E5 RID: 741
	public abstract class VRTK_DestinationMarker : MonoBehaviour
	{
		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06001906 RID: 6406 RVA: 0x00085AF8 File Offset: 0x00083CF8
		// (remove) Token: 0x06001907 RID: 6407 RVA: 0x00085B30 File Offset: 0x00083D30
		public event DestinationMarkerEventHandler DestinationMarkerEnter;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06001908 RID: 6408 RVA: 0x00085B68 File Offset: 0x00083D68
		// (remove) Token: 0x06001909 RID: 6409 RVA: 0x00085BA0 File Offset: 0x00083DA0
		public event DestinationMarkerEventHandler DestinationMarkerExit;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x0600190A RID: 6410 RVA: 0x00085BD8 File Offset: 0x00083DD8
		// (remove) Token: 0x0600190B RID: 6411 RVA: 0x00085C10 File Offset: 0x00083E10
		public event DestinationMarkerEventHandler DestinationMarkerHover;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x0600190C RID: 6412 RVA: 0x00085C48 File Offset: 0x00083E48
		// (remove) Token: 0x0600190D RID: 6413 RVA: 0x00085C80 File Offset: 0x00083E80
		public event DestinationMarkerEventHandler DestinationMarkerSet;

		// Token: 0x0600190E RID: 6414 RVA: 0x00085CB8 File Offset: 0x00083EB8
		public virtual void OnDestinationMarkerEnter(DestinationMarkerEventArgs e)
		{
			if (this.DestinationMarkerEnter != null && (!this.forceHoverOnRepeatedEnter || e.raycastHit.collider != this.existingCollider))
			{
				this.existingCollider = e.raycastHit.collider;
				this.DestinationMarkerEnter(this, e);
			}
			if (this.forceHoverOnRepeatedEnter && e.raycastHit.collider == this.existingCollider)
			{
				this.OnDestinationMarkerHover(e);
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00085D35 File Offset: 0x00083F35
		public virtual void OnDestinationMarkerExit(DestinationMarkerEventArgs e)
		{
			if (this.DestinationMarkerExit != null)
			{
				this.DestinationMarkerExit(this, e);
				this.existingCollider = null;
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00085D53 File Offset: 0x00083F53
		public virtual void OnDestinationMarkerHover(DestinationMarkerEventArgs e)
		{
			if (this.DestinationMarkerHover != null)
			{
				this.DestinationMarkerHover(this, e);
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00085D6A File Offset: 0x00083F6A
		public virtual void OnDestinationMarkerSet(DestinationMarkerEventArgs e)
		{
			if (this.DestinationMarkerSet != null)
			{
				this.DestinationMarkerSet(this, e);
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00085D81 File Offset: 0x00083F81
		[Obsolete("`DestinationMarkerEventArgs.SetInvalidTarget(list)` has been replaced with the public variable `DestinationMarkerEventArgs.targetListPolicy`. This method will be removed in a future version of VRTK.")]
		public virtual void SetInvalidTarget(VRTK_PolicyList list = null)
		{
			this.targetListPolicy = list;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00085D8A File Offset: 0x00083F8A
		public virtual void SetNavMeshCheckDistance(float distance)
		{
			this.navMeshCheckDistance = distance;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00085D93 File Offset: 0x00083F93
		public virtual void SetHeadsetPositionCompensation(bool state)
		{
			this.headsetPositionCompensation = state;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00085D9C File Offset: 0x00083F9C
		public virtual void SetForceHoverOnRepeatedEnter(bool state)
		{
			this.forceHoverOnRepeatedEnter = state;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00085DA5 File Offset: 0x00083FA5
		protected virtual void OnEnable()
		{
			VRTK_ObjectCache.registeredDestinationMarkers.Add(this);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00085DB2 File Offset: 0x00083FB2
		protected virtual void OnDisable()
		{
			VRTK_ObjectCache.registeredDestinationMarkers.Remove(this);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00085DC0 File Offset: 0x00083FC0
		protected virtual DestinationMarkerEventArgs SetDestinationMarkerEvent(float distance, Transform target, RaycastHit raycastHit, Vector3 position, VRTK_ControllerReference controllerReference, bool forceDestinationPosition = false, Quaternion? rotation = null)
		{
			DestinationMarkerEventArgs result;
			result.controllerIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			result.controllerReference = controllerReference;
			result.distance = distance;
			result.target = target;
			result.raycastHit = raycastHit;
			result.destinationPosition = position;
			result.destinationRotation = rotation;
			result.enableTeleport = this.enableTeleport;
			result.forceDestinationPosition = forceDestinationPosition;
			return result;
		}

		// Token: 0x04001488 RID: 5256
		[Header("Destination Marker Settings", order = 1)]
		[Tooltip("If this is checked then the teleport flag is set to true in the Destination Set event so teleport scripts will know whether to action the new destination.")]
		public bool enableTeleport = true;

		// Token: 0x04001489 RID: 5257
		[Tooltip("A specified VRTK_PolicyList to use to determine whether destination targets will be considered valid or invalid.")]
		public VRTK_PolicyList targetListPolicy;

		// Token: 0x0400148E RID: 5262
		protected float navMeshCheckDistance;

		// Token: 0x0400148F RID: 5263
		protected bool headsetPositionCompensation;

		// Token: 0x04001490 RID: 5264
		protected bool forceHoverOnRepeatedEnter = true;

		// Token: 0x04001491 RID: 5265
		protected Collider existingCollider;
	}
}
