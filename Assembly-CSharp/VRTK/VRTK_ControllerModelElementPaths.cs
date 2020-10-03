using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000308 RID: 776
	[Serializable]
	public class VRTK_ControllerModelElementPaths
	{
		// Token: 0x040015C9 RID: 5577
		[Tooltip("The overall shape of the controller.")]
		public string bodyModelPath = "";

		// Token: 0x040015CA RID: 5578
		[Tooltip("The model that represents the trigger button.")]
		public string triggerModelPath = "";

		// Token: 0x040015CB RID: 5579
		[Tooltip("The model that represents the left grip button.")]
		public string leftGripModelPath = "";

		// Token: 0x040015CC RID: 5580
		[Tooltip("The model that represents the right grip button.")]
		public string rightGripModelPath = "";

		// Token: 0x040015CD RID: 5581
		[Tooltip("The model that represents the touchpad.")]
		public string touchpadModelPath = "";

		// Token: 0x040015CE RID: 5582
		[Tooltip("The model that represents button one.")]
		public string buttonOneModelPath = "";

		// Token: 0x040015CF RID: 5583
		[Tooltip("The model that represents button two.")]
		public string buttonTwoModelPath = "";

		// Token: 0x040015D0 RID: 5584
		[Tooltip("The model that represents the system menu button.")]
		public string systemMenuModelPath = "";

		// Token: 0x040015D1 RID: 5585
		[Tooltip("The model that represents the start menu button.")]
		public string startMenuModelPath = "";
	}
}
