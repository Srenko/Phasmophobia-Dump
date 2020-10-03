using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000309 RID: 777
	public abstract class VRTK_ObjectFollow : MonoBehaviour
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0008D892 File Offset: 0x0008BA92
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x0008D89A File Offset: 0x0008BA9A
		public Vector3 targetPosition { get; private set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0008D8A3 File Offset: 0x0008BAA3
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0008D8AB File Offset: 0x0008BAAB
		public Quaternion targetRotation { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x0008D8B4 File Offset: 0x0008BAB4
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x0008D8BC File Offset: 0x0008BABC
		public Vector3 targetScale { get; private set; }

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0008D8C5 File Offset: 0x0008BAC5
		public virtual void Follow()
		{
			if (this.gameObjectToFollow == null)
			{
				return;
			}
			if (this.followsPosition)
			{
				this.FollowPosition();
			}
			if (this.followsRotation)
			{
				this.FollowRotation();
			}
			if (this.followsScale)
			{
				this.FollowScale();
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0008D900 File Offset: 0x0008BB00
		protected virtual void OnEnable()
		{
			this.gameObjectToChange = ((this.gameObjectToChange != null) ? this.gameObjectToChange : base.gameObject);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0008D924 File Offset: 0x0008BB24
		protected virtual void OnValidate()
		{
			this.maxAllowedPerFrameDistanceDifference = Mathf.Max(0.0001f, this.maxAllowedPerFrameDistanceDifference);
			this.maxAllowedPerFrameAngleDifference = Mathf.Max(0.0001f, this.maxAllowedPerFrameAngleDifference);
			this.maxAllowedPerFrameSizeDifference = Mathf.Max(0.0001f, this.maxAllowedPerFrameSizeDifference);
		}

		// Token: 0x06001AF5 RID: 6901
		protected abstract Vector3 GetPositionToFollow();

		// Token: 0x06001AF6 RID: 6902
		protected abstract void SetPositionOnGameObject(Vector3 newPosition);

		// Token: 0x06001AF7 RID: 6903
		protected abstract Quaternion GetRotationToFollow();

		// Token: 0x06001AF8 RID: 6904
		protected abstract void SetRotationOnGameObject(Quaternion newRotation);

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0008D973 File Offset: 0x0008BB73
		protected virtual Vector3 GetScaleToFollow()
		{
			return this.gameObjectToFollow.transform.localScale;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0008D985 File Offset: 0x0008BB85
		protected virtual void SetScaleOnGameObject(Vector3 newScale)
		{
			this.gameObjectToChange.transform.localScale = newScale;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0008D998 File Offset: 0x0008BB98
		protected virtual void FollowPosition()
		{
			Vector3 positionToFollow = this.GetPositionToFollow();
			Vector3 vector;
			if (this.smoothsPosition)
			{
				float t = Mathf.Clamp01(Vector3.Distance(this.targetPosition, positionToFollow) / this.maxAllowedPerFrameDistanceDifference);
				vector = Vector3.Lerp(this.targetPosition, positionToFollow, t);
			}
			else
			{
				vector = positionToFollow;
			}
			this.targetPosition = vector;
			this.SetPositionOnGameObject(vector);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0008D9F0 File Offset: 0x0008BBF0
		protected virtual void FollowRotation()
		{
			Quaternion rotationToFollow = this.GetRotationToFollow();
			Quaternion quaternion;
			if (this.smoothsRotation)
			{
				float t = Mathf.Clamp01(Quaternion.Angle(this.targetRotation, rotationToFollow) / this.maxAllowedPerFrameAngleDifference);
				quaternion = Quaternion.Lerp(this.targetRotation, rotationToFollow, t);
			}
			else
			{
				quaternion = rotationToFollow;
			}
			this.targetRotation = quaternion;
			this.SetRotationOnGameObject(quaternion);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0008DA48 File Offset: 0x0008BC48
		protected virtual void FollowScale()
		{
			Vector3 scaleToFollow = this.GetScaleToFollow();
			Vector3 vector;
			if (this.smoothsScale)
			{
				float t = Mathf.Clamp01(Vector3.Distance(this.targetScale, scaleToFollow) / this.maxAllowedPerFrameSizeDifference);
				vector = Vector3.Lerp(this.targetScale, scaleToFollow, t);
			}
			else
			{
				vector = scaleToFollow;
			}
			this.targetScale = vector;
			this.SetScaleOnGameObject(vector);
		}

		// Token: 0x040015D2 RID: 5586
		[Tooltip("The game object to follow. The followed property values will be taken from this one.")]
		public GameObject gameObjectToFollow;

		// Token: 0x040015D3 RID: 5587
		[Tooltip("The game object to change the property values of. If left empty the game object this script is attached to will be changed.")]
		public GameObject gameObjectToChange;

		// Token: 0x040015D4 RID: 5588
		[Tooltip("Whether to follow the position of the given game object.")]
		public bool followsPosition = true;

		// Token: 0x040015D5 RID: 5589
		[Tooltip("Whether to smooth the position when following `gameObjectToFollow`.")]
		public bool smoothsPosition;

		// Token: 0x040015D6 RID: 5590
		[Tooltip("The maximum allowed distance between the unsmoothed source and the smoothed target per frame to use for smoothing.")]
		public float maxAllowedPerFrameDistanceDifference = 0.003f;

		// Token: 0x040015D8 RID: 5592
		[Tooltip("Whether to follow the rotation of the given game object.")]
		public bool followsRotation = true;

		// Token: 0x040015D9 RID: 5593
		[Tooltip("Whether to smooth the rotation when following `gameObjectToFollow`.")]
		public bool smoothsRotation;

		// Token: 0x040015DA RID: 5594
		[Tooltip("The maximum allowed angle between the unsmoothed source and the smoothed target per frame to use for smoothing.")]
		public float maxAllowedPerFrameAngleDifference = 1.5f;

		// Token: 0x040015DC RID: 5596
		[Tooltip("Whether to follow the scale of the given game object.")]
		public bool followsScale = true;

		// Token: 0x040015DD RID: 5597
		[Tooltip("Whether to smooth the scale when following `gameObjectToFollow`.")]
		public bool smoothsScale;

		// Token: 0x040015DE RID: 5598
		[Tooltip("The maximum allowed size between the unsmoothed source and the smoothed target per frame to use for smoothing.")]
		public float maxAllowedPerFrameSizeDifference = 0.003f;
	}
}
