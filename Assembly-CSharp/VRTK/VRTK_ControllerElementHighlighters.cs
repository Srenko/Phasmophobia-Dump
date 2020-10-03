using System;
using UnityEngine;
using VRTK.Highlighters;

namespace VRTK
{
	// Token: 0x02000307 RID: 775
	[Serializable]
	public class VRTK_ControllerElementHighlighters
	{
		// Token: 0x040015C0 RID: 5568
		[Tooltip("The highlighter to use on the overall shape of the controller.")]
		public VRTK_BaseHighlighter body;

		// Token: 0x040015C1 RID: 5569
		[Tooltip("The highlighter to use on the trigger button.")]
		public VRTK_BaseHighlighter trigger;

		// Token: 0x040015C2 RID: 5570
		[Tooltip("The highlighter to use on the left grip button.")]
		public VRTK_BaseHighlighter gripLeft;

		// Token: 0x040015C3 RID: 5571
		[Tooltip("The highlighter to use on the right grip button.")]
		public VRTK_BaseHighlighter gripRight;

		// Token: 0x040015C4 RID: 5572
		[Tooltip("The highlighter to use on the touchpad.")]
		public VRTK_BaseHighlighter touchpad;

		// Token: 0x040015C5 RID: 5573
		[Tooltip("The highlighter to use on button one.")]
		public VRTK_BaseHighlighter buttonOne;

		// Token: 0x040015C6 RID: 5574
		[Tooltip("The highlighter to use on button two.")]
		public VRTK_BaseHighlighter buttonTwo;

		// Token: 0x040015C7 RID: 5575
		[Tooltip("The highlighter to use on the system menu button.")]
		public VRTK_BaseHighlighter systemMenu;

		// Token: 0x040015C8 RID: 5576
		[Tooltip("The highlighter to use on the start menu button.")]
		public VRTK_BaseHighlighter startMenu;
	}
}
