using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200030B RID: 779
	[AddComponentMenu("VRTK/Scripts/Utilities/Object Follow/VRTK_TransformFollow")]
	public class VRTK_TransformFollow : VRTK_ObjectFollow
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0008DC68 File Offset: 0x0008BE68
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0008DC70 File Offset: 0x0008BE70
		public VRTK_TransformFollow.FollowMoment moment
		{
			get
			{
				return this._moment;
			}
			set
			{
				if (this._moment == value)
				{
					return;
				}
				if (base.isActiveAndEnabled)
				{
					if (this._moment == VRTK_TransformFollow.FollowMoment.OnPreRender && value != VRTK_TransformFollow.FollowMoment.OnPreRender)
					{
						Camera.onPreRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPreRender, new Camera.CameraCallback(this.OnCamPreRender));
					}
					if (this._moment != VRTK_TransformFollow.FollowMoment.OnPreRender && value == VRTK_TransformFollow.FollowMoment.OnPreRender)
					{
						Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(this.OnCamPreRender));
					}
					if (this._moment == VRTK_TransformFollow.FollowMoment.OnPreCull && value != VRTK_TransformFollow.FollowMoment.OnPreCull)
					{
						Camera.onPreCull = (Camera.CameraCallback)Delegate.Remove(Camera.onPreCull, new Camera.CameraCallback(this.OnCamPreCull));
					}
					if (this._moment != VRTK_TransformFollow.FollowMoment.OnPreCull && value == VRTK_TransformFollow.FollowMoment.OnPreCull)
					{
						Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(this.OnCamPreCull));
					}
				}
				this._moment = value;
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0008DD51 File Offset: 0x0008BF51
		public override void Follow()
		{
			this.CacheTransforms();
			base.Follow();
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0008DD60 File Offset: 0x0008BF60
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.moment == VRTK_TransformFollow.FollowMoment.OnPreRender)
			{
				Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(this.OnCamPreRender));
			}
			if (this.moment == VRTK_TransformFollow.FollowMoment.OnPreCull)
			{
				Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(this.OnCamPreCull));
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0008DDC8 File Offset: 0x0008BFC8
		protected virtual void OnDisable()
		{
			this.transformToFollow = null;
			this.transformToChange = null;
			Camera.onPreRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPreRender, new Camera.CameraCallback(this.OnCamPreRender));
			Camera.onPreCull = (Camera.CameraCallback)Delegate.Remove(Camera.onPreCull, new Camera.CameraCallback(this.OnCamPreCull));
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0008DE25 File Offset: 0x0008C025
		protected void Update()
		{
			if (this.moment == VRTK_TransformFollow.FollowMoment.OnUpdate)
			{
				this.Follow();
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0008DE35 File Offset: 0x0008C035
		protected virtual void LateUpdate()
		{
			if (this.moment == VRTK_TransformFollow.FollowMoment.OnLateUpdate)
			{
				this.Follow();
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0008DE46 File Offset: 0x0008C046
		protected virtual void OnCamPreRender(Camera cam)
		{
			if (cam.gameObject.transform == VRTK_SDK_Bridge.GetHeadsetCamera())
			{
				this.Follow();
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0008DE46 File Offset: 0x0008C046
		protected virtual void OnCamPreCull(Camera cam)
		{
			if (cam.gameObject.transform == VRTK_SDK_Bridge.GetHeadsetCamera())
			{
				this.Follow();
			}
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0008DE65 File Offset: 0x0008C065
		protected override Vector3 GetPositionToFollow()
		{
			return this.transformToFollow.position;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0008DE72 File Offset: 0x0008C072
		protected override void SetPositionOnGameObject(Vector3 newPosition)
		{
			this.transformToChange.position = newPosition;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0008DE80 File Offset: 0x0008C080
		protected override Quaternion GetRotationToFollow()
		{
			return this.transformToFollow.rotation;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0008DE8D File Offset: 0x0008C08D
		protected override void SetRotationOnGameObject(Quaternion newRotation)
		{
			this.transformToChange.rotation = newRotation;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0008DE9C File Offset: 0x0008C09C
		protected virtual void CacheTransforms()
		{
			if (this.gameObjectToFollow == null || this.gameObjectToChange == null || (this.transformToFollow != null && this.transformToChange != null))
			{
				return;
			}
			this.transformToFollow = this.gameObjectToFollow.transform;
			this.transformToChange = this.gameObjectToChange.transform;
		}

		// Token: 0x040015E3 RID: 5603
		[Tooltip("The moment at which to follow.")]
		[SerializeField]
		private VRTK_TransformFollow.FollowMoment _moment = VRTK_TransformFollow.FollowMoment.OnPreRender;

		// Token: 0x040015E4 RID: 5604
		protected Transform transformToFollow;

		// Token: 0x040015E5 RID: 5605
		protected Transform transformToChange;

		// Token: 0x020005FC RID: 1532
		public enum FollowMoment
		{
			// Token: 0x04002849 RID: 10313
			OnUpdate,
			// Token: 0x0400284A RID: 10314
			OnLateUpdate,
			// Token: 0x0400284B RID: 10315
			OnPreRender,
			// Token: 0x0400284C RID: 10316
			OnPreCull
		}
	}
}
