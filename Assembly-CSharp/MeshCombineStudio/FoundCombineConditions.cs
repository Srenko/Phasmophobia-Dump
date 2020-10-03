using System;
using System.Collections.Generic;

namespace MeshCombineStudio
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	public class FoundCombineConditions
	{
		// Token: 0x04002305 RID: 8965
		public HashSet<CombineCondition> combineConditions = new HashSet<CombineCondition>();

		// Token: 0x04002306 RID: 8966
		public int matCount;

		// Token: 0x04002307 RID: 8967
		public int lightmapIndexCount;

		// Token: 0x04002308 RID: 8968
		public int shadowCastingCount;

		// Token: 0x04002309 RID: 8969
		public int receiveShadowsCount;

		// Token: 0x0400230A RID: 8970
		public int receiveGICount;

		// Token: 0x0400230B RID: 8971
		public int lightProbeUsageCount;

		// Token: 0x0400230C RID: 8972
		public int reflectionProbeUsageCount;

		// Token: 0x0400230D RID: 8973
		public int probeAnchorCount;

		// Token: 0x0400230E RID: 8974
		public int motionVectorGenerationModeCount;

		// Token: 0x0400230F RID: 8975
		public int layerCount;

		// Token: 0x04002310 RID: 8976
		public int staticEditorFlagsCount;
	}
}
