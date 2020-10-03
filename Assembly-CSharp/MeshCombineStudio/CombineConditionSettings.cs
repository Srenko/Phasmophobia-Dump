using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004BF RID: 1215
	[Serializable]
	public class CombineConditionSettings
	{
		// Token: 0x0400231B RID: 8987
		public bool foldout = true;

		// Token: 0x0400231C RID: 8988
		public bool sameMaterial = true;

		// Token: 0x0400231D RID: 8989
		public bool sameShadowCastingMode;

		// Token: 0x0400231E RID: 8990
		public bool sameReceiveShadows;

		// Token: 0x0400231F RID: 8991
		public bool sameReceiveGI;

		// Token: 0x04002320 RID: 8992
		public bool sameLightProbeUsage;

		// Token: 0x04002321 RID: 8993
		public bool sameReflectionProbeUsage;

		// Token: 0x04002322 RID: 8994
		public bool sameProbeAnchor;

		// Token: 0x04002323 RID: 8995
		public bool sameMotionVectorGenerationMode;

		// Token: 0x04002324 RID: 8996
		public bool sameStaticEditorFlags;

		// Token: 0x04002325 RID: 8997
		public bool sameLayer;

		// Token: 0x04002326 RID: 8998
		public Material material;

		// Token: 0x04002327 RID: 8999
		public CombineCondition combineCondition = CombineCondition.Default;
	}
}
