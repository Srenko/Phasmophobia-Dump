using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x0200031A RID: 794
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_BodyPhysics_UnityEvents")]
	public sealed class VRTK_BodyPhysics_UnityEvents : VRTK_UnityEvents<VRTK_BodyPhysics>
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x00091E54 File Offset: 0x00090054
		protected override void AddListeners(VRTK_BodyPhysics component)
		{
			component.StartFalling += this.StartFalling;
			component.StopFalling += this.StopFalling;
			component.StartMoving += this.StartMoving;
			component.StopMoving += this.StopMoving;
			component.StartColliding += this.StartColliding;
			component.StopColliding += this.StopColliding;
			component.StartLeaning += this.StartLeaning;
			component.StopLeaning += this.StopLeaning;
			component.StartTouchingGround += this.StartTouchingGround;
			component.StopTouchingGround += this.StopTouchingGround;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00091F18 File Offset: 0x00090118
		protected override void RemoveListeners(VRTK_BodyPhysics component)
		{
			component.StartFalling -= this.StartFalling;
			component.StopFalling -= this.StopFalling;
			component.StartMoving -= this.StartMoving;
			component.StopMoving -= this.StopMoving;
			component.StartColliding -= this.StartColliding;
			component.StopColliding -= this.StopColliding;
			component.StartLeaning -= this.StartLeaning;
			component.StopLeaning -= this.StopLeaning;
			component.StartTouchingGround -= this.StartTouchingGround;
			component.StopTouchingGround -= this.StopTouchingGround;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00091FD9 File Offset: 0x000901D9
		private void StartFalling(object o, BodyPhysicsEventArgs e)
		{
			this.OnStartFalling.Invoke(o, e);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00091FE8 File Offset: 0x000901E8
		private void StopFalling(object o, BodyPhysicsEventArgs e)
		{
			this.OnStopFalling.Invoke(o, e);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00091FF7 File Offset: 0x000901F7
		private void StartMoving(object o, BodyPhysicsEventArgs e)
		{
			this.OnStartMoving.Invoke(o, e);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00092006 File Offset: 0x00090206
		private void StopMoving(object o, BodyPhysicsEventArgs e)
		{
			this.OnStopMoving.Invoke(o, e);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00092015 File Offset: 0x00090215
		private void StartColliding(object o, BodyPhysicsEventArgs e)
		{
			this.OnStartColliding.Invoke(o, e);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00092024 File Offset: 0x00090224
		private void StopColliding(object o, BodyPhysicsEventArgs e)
		{
			this.OnStopColliding.Invoke(o, e);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00092033 File Offset: 0x00090233
		private void StartLeaning(object o, BodyPhysicsEventArgs e)
		{
			this.OnStartLeaning.Invoke(o, e);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00092042 File Offset: 0x00090242
		private void StopLeaning(object o, BodyPhysicsEventArgs e)
		{
			this.OnStopLeaning.Invoke(o, e);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00092051 File Offset: 0x00090251
		private void StartTouchingGround(object o, BodyPhysicsEventArgs e)
		{
			this.OnStartTouchingGround.Invoke(o, e);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00092060 File Offset: 0x00090260
		private void StopTouchingGround(object o, BodyPhysicsEventArgs e)
		{
			this.OnStopTouchingGround.Invoke(o, e);
		}

		// Token: 0x04001657 RID: 5719
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStartFalling = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x04001658 RID: 5720
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStopFalling = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x04001659 RID: 5721
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStartMoving = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0400165A RID: 5722
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStopMoving = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0400165B RID: 5723
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStartColliding = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0400165C RID: 5724
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStopColliding = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0400165D RID: 5725
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStartLeaning = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0400165E RID: 5726
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStopLeaning = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0400165F RID: 5727
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStartTouchingGround = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x04001660 RID: 5728
		public VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent OnStopTouchingGround = new VRTK_BodyPhysics_UnityEvents.BodyPhysicsEvent();

		// Token: 0x0200061E RID: 1566
		[Serializable]
		public sealed class BodyPhysicsEvent : UnityEvent<object, BodyPhysicsEventArgs>
		{
		}
	}
}
