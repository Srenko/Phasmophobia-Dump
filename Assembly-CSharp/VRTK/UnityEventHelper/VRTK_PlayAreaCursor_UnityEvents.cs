using System;
using UnityEngine;
using UnityEngine.Events;

namespace VRTK.UnityEventHelper
{
	// Token: 0x02000330 RID: 816
	[AddComponentMenu("VRTK/Scripts/Utilities/Unity Events/VRTK_PlayAreaCursor_UnityEvents")]
	public sealed class VRTK_PlayAreaCursor_UnityEvents : VRTK_UnityEvents<VRTK_PlayAreaCursor>
	{
		// Token: 0x06001CBB RID: 7355 RVA: 0x00094106 File Offset: 0x00092306
		protected override void AddListeners(VRTK_PlayAreaCursor component)
		{
			component.PlayAreaCursorStartCollision += this.PlayAreaCursorStartCollision;
			component.PlayAreaCursorEndCollision += this.PlayAreaCursorEndCollision;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0009412C File Offset: 0x0009232C
		protected override void RemoveListeners(VRTK_PlayAreaCursor component)
		{
			component.PlayAreaCursorStartCollision -= this.PlayAreaCursorStartCollision;
			component.PlayAreaCursorEndCollision -= this.PlayAreaCursorEndCollision;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00094152 File Offset: 0x00092352
		private void PlayAreaCursorStartCollision(object o, PlayAreaCursorEventArgs e)
		{
			this.OnPlayAreaCursorStartCollision.Invoke(o, e);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00094161 File Offset: 0x00092361
		private void PlayAreaCursorEndCollision(object o, PlayAreaCursorEventArgs e)
		{
			this.OnPlayAreaCursorEndCollision.Invoke(o, e);
		}

		// Token: 0x040016DC RID: 5852
		public VRTK_PlayAreaCursor_UnityEvents.PlayAreaCursorEvent OnPlayAreaCursorStartCollision = new VRTK_PlayAreaCursor_UnityEvents.PlayAreaCursorEvent();

		// Token: 0x040016DD RID: 5853
		public VRTK_PlayAreaCursor_UnityEvents.PlayAreaCursorEvent OnPlayAreaCursorEndCollision = new VRTK_PlayAreaCursor_UnityEvents.PlayAreaCursorEvent();

		// Token: 0x02000634 RID: 1588
		[Serializable]
		public sealed class PlayAreaCursorEvent : UnityEvent<object, PlayAreaCursorEventArgs>
		{
		}
	}
}
