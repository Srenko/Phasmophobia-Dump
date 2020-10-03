using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002F3 RID: 755
	[RequireComponent(typeof(VRTK_HeadsetCollision))]
	[RequireComponent(typeof(VRTK_HeadsetFade))]
	[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HeadsetCollisionFade")]
	public class VRTK_HeadsetCollisionFade : MonoBehaviour
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x0008B0E8 File Offset: 0x000892E8
		protected virtual void OnEnable()
		{
			this.headsetFade = ((this.headsetFade != null) ? this.headsetFade : base.GetComponentInChildren<VRTK_HeadsetFade>());
			this.headsetCollision = ((this.headsetCollision != null) ? this.headsetCollision : base.GetComponentInChildren<VRTK_HeadsetCollision>());
			this.headsetCollision.HeadsetCollisionDetect += this.OnHeadsetCollisionDetect;
			this.headsetCollision.HeadsetCollisionEnded += this.OnHeadsetCollisionEnded;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0008B169 File Offset: 0x00089369
		protected virtual void OnDisable()
		{
			this.headsetCollision.HeadsetCollisionDetect -= this.OnHeadsetCollisionDetect;
			this.headsetCollision.HeadsetCollisionEnded -= this.OnHeadsetCollisionEnded;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0008B19B File Offset: 0x0008939B
		protected virtual void OnHeadsetCollisionDetect(object sender, HeadsetCollisionEventArgs e)
		{
			base.Invoke("StartFade", this.timeTillFade);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0008B1AE File Offset: 0x000893AE
		protected virtual void OnHeadsetCollisionEnded(object sender, HeadsetCollisionEventArgs e)
		{
			base.CancelInvoke("StartFade");
			this.headsetFade.Unfade(this.blinkTransitionSpeed);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0008B1CC File Offset: 0x000893CC
		protected virtual void StartFade()
		{
			this.headsetFade.Fade(this.fadeColor, this.blinkTransitionSpeed);
		}

		// Token: 0x04001543 RID: 5443
		[Header("Collision Fade Settings")]
		[Tooltip("The amount of time to wait until a fade occurs.")]
		public float timeTillFade;

		// Token: 0x04001544 RID: 5444
		[Tooltip("The fade blink speed on collision.")]
		public float blinkTransitionSpeed = 0.1f;

		// Token: 0x04001545 RID: 5445
		[Tooltip("The colour to fade the headset to on collision.")]
		public Color fadeColor = Color.black;

		// Token: 0x04001546 RID: 5446
		[Header("Custom Settings")]
		[Tooltip("The VRTK Headset Collision script to use when determining headset collisions. If this is left blank then the script will need to be applied to the same GameObject.")]
		public VRTK_HeadsetCollision headsetCollision;

		// Token: 0x04001547 RID: 5447
		[Tooltip("The VRTK Headset Fade script to use when fading the headset. If this is left blank then the script will need to be applied to the same GameObject.")]
		public VRTK_HeadsetFade headsetFade;
	}
}
