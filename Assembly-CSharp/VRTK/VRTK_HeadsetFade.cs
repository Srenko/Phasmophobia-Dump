using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002F9 RID: 761
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HeadsetFade")]
	public class VRTK_HeadsetFade : MonoBehaviour
	{
		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06001A56 RID: 6742 RVA: 0x0008B700 File Offset: 0x00089900
		// (remove) Token: 0x06001A57 RID: 6743 RVA: 0x0008B738 File Offset: 0x00089938
		public event HeadsetFadeEventHandler HeadsetFadeStart;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06001A58 RID: 6744 RVA: 0x0008B770 File Offset: 0x00089970
		// (remove) Token: 0x06001A59 RID: 6745 RVA: 0x0008B7A8 File Offset: 0x000899A8
		public event HeadsetFadeEventHandler HeadsetFadeComplete;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06001A5A RID: 6746 RVA: 0x0008B7E0 File Offset: 0x000899E0
		// (remove) Token: 0x06001A5B RID: 6747 RVA: 0x0008B818 File Offset: 0x00089A18
		public event HeadsetFadeEventHandler HeadsetUnfadeStart;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06001A5C RID: 6748 RVA: 0x0008B850 File Offset: 0x00089A50
		// (remove) Token: 0x06001A5D RID: 6749 RVA: 0x0008B888 File Offset: 0x00089A88
		public event HeadsetFadeEventHandler HeadsetUnfadeComplete;

		// Token: 0x06001A5E RID: 6750 RVA: 0x0008B8BD File Offset: 0x00089ABD
		public virtual void OnHeadsetFadeStart(HeadsetFadeEventArgs e)
		{
			if (this.HeadsetFadeStart != null)
			{
				this.HeadsetFadeStart(this, e);
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0008B8D4 File Offset: 0x00089AD4
		public virtual void OnHeadsetFadeComplete(HeadsetFadeEventArgs e)
		{
			if (this.HeadsetFadeComplete != null)
			{
				this.HeadsetFadeComplete(this, e);
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0008B8EB File Offset: 0x00089AEB
		public virtual void OnHeadsetUnfadeStart(HeadsetFadeEventArgs e)
		{
			if (this.HeadsetUnfadeStart != null)
			{
				this.HeadsetUnfadeStart(this, e);
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0008B902 File Offset: 0x00089B02
		public virtual void OnHeadsetUnfadeComplete(HeadsetFadeEventArgs e)
		{
			if (this.HeadsetUnfadeComplete != null)
			{
				this.HeadsetUnfadeComplete(this, e);
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0008B919 File Offset: 0x00089B19
		public virtual bool IsFaded()
		{
			return this.isFaded;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0008B921 File Offset: 0x00089B21
		public virtual bool IsTransitioning()
		{
			return this.isTransitioning;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0008B92C File Offset: 0x00089B2C
		public virtual void Fade(Color color, float duration)
		{
			this.isFaded = false;
			this.isTransitioning = true;
			VRTK_SDK_Bridge.HeadsetFade(color, duration, false);
			this.OnHeadsetFadeStart(this.SetHeadsetFadeEvent(this.headset, duration));
			base.CancelInvoke("UnfadeComplete");
			base.Invoke("FadeComplete", duration);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0008B97C File Offset: 0x00089B7C
		public virtual void Unfade(float duration)
		{
			this.isFaded = true;
			this.isTransitioning = true;
			VRTK_SDK_Bridge.HeadsetFade(Color.clear, duration, false);
			this.OnHeadsetUnfadeStart(this.SetHeadsetFadeEvent(this.headset, duration));
			base.CancelInvoke("FadeComplete");
			base.Invoke("UnfadeComplete", duration);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0008B9D0 File Offset: 0x00089BD0
		protected virtual void OnEnable()
		{
			this.headset = VRTK_DeviceFinder.HeadsetCamera();
			this.isTransitioning = false;
			this.isFaded = false;
			VRTK_SharedMethods.AddCameraFade();
			if (!VRTK_SDK_Bridge.HasHeadsetFade(this.headset))
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_HeadsetFade",
					"compatible fade",
					"Camera"
				}));
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0008BA34 File Offset: 0x00089C34
		protected virtual HeadsetFadeEventArgs SetHeadsetFadeEvent(Transform currentTransform, float duration)
		{
			HeadsetFadeEventArgs result;
			result.timeTillComplete = duration;
			result.currentTransform = currentTransform;
			return result;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0008BA52 File Offset: 0x00089C52
		protected virtual void FadeComplete()
		{
			this.isFaded = true;
			this.isTransitioning = false;
			this.OnHeadsetFadeComplete(this.SetHeadsetFadeEvent(this.headset, 0f));
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0008BA79 File Offset: 0x00089C79
		protected virtual void UnfadeComplete()
		{
			this.isFaded = false;
			this.isTransitioning = false;
			this.OnHeadsetUnfadeComplete(this.SetHeadsetFadeEvent(this.headset, 0f));
		}

		// Token: 0x04001566 RID: 5478
		protected Transform headset;

		// Token: 0x04001567 RID: 5479
		protected bool isTransitioning;

		// Token: 0x04001568 RID: 5480
		protected bool isFaded;
	}
}
