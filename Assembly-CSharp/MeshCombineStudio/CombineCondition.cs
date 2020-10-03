using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MeshCombineStudio
{
	// Token: 0x020004BE RID: 1214
	[Serializable]
	public struct CombineCondition
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000BC364 File Offset: 0x000BA564
		public static CombineCondition Default
		{
			get
			{
				return new CombineCondition
				{
					matInstanceId = -1,
					lightmapIndex = -1,
					shadowCastingMode = ShadowCastingMode.On,
					receiveShadows = true,
					lightProbeUsage = LightProbeUsage.BlendProbes,
					reflectionProbeUsage = ReflectionProbeUsage.BlendProbes,
					probeAnchor = null,
					motionVectorGenerationMode = MotionVectorGenerationMode.Camera,
					layer = 0
				};
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000BC3C4 File Offset: 0x000BA5C4
		public static void MakeFoundReport(FoundCombineConditions fcc)
		{
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition.matInstanceId);
			}
			fcc.matCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition2 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition2.lightmapIndex);
			}
			fcc.lightmapIndexCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition3 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition3.shadowCastingMode);
			}
			fcc.shadowCastingCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition4 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition4.receiveShadows);
			}
			fcc.receiveShadowsCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition5 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition5.lightProbeUsage);
			}
			fcc.lightProbeUsageCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition6 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition6.reflectionProbeUsage);
			}
			fcc.reflectionProbeUsageCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition7 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition7.probeAnchor);
			}
			fcc.probeAnchorCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition8 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition8.motionVectorGenerationMode);
			}
			fcc.motionVectorGenerationModeCount = CombineCondition.countSet.Count;
			CombineCondition.countSet.Clear();
			foreach (CombineCondition combineCondition9 in fcc.combineConditions)
			{
				CombineCondition.countSet.Add(combineCondition9.layer);
			}
			fcc.layerCount = CombineCondition.countSet.Count;
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000BC7A0 File Offset: 0x000BA9A0
		public void ReadFromGameObject(CombineConditionSettings combineConditions, bool copyBakedLighting, GameObject go, MeshRenderer mr, Material mat)
		{
			this.matInstanceId = (combineConditions.sameMaterial ? mat.GetInstanceID() : combineConditions.combineCondition.matInstanceId);
			this.lightmapIndex = (copyBakedLighting ? mr.lightmapIndex : (this.lightmapIndex = -1));
			this.shadowCastingMode = (combineConditions.sameShadowCastingMode ? mr.shadowCastingMode : combineConditions.combineCondition.shadowCastingMode);
			this.receiveShadows = (combineConditions.sameReceiveShadows ? mr.receiveShadows : combineConditions.combineCondition.receiveShadows);
			this.lightProbeUsage = (combineConditions.sameLightProbeUsage ? mr.lightProbeUsage : combineConditions.combineCondition.lightProbeUsage);
			this.reflectionProbeUsage = (combineConditions.sameReflectionProbeUsage ? mr.reflectionProbeUsage : combineConditions.combineCondition.reflectionProbeUsage);
			this.probeAnchor = (combineConditions.sameProbeAnchor ? mr.probeAnchor : combineConditions.combineCondition.probeAnchor);
			this.motionVectorGenerationMode = (combineConditions.sameMotionVectorGenerationMode ? mr.motionVectorGenerationMode : combineConditions.combineCondition.motionVectorGenerationMode);
			this.layer = (combineConditions.sameLayer ? go.layer : combineConditions.combineCondition.layer);
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000BC8D8 File Offset: 0x000BAAD8
		public void WriteToGameObject(GameObject go, MeshRenderer mr)
		{
			mr.lightmapIndex = this.lightmapIndex;
			mr.shadowCastingMode = this.shadowCastingMode;
			mr.receiveShadows = this.receiveShadows;
			mr.lightProbeUsage = this.lightProbeUsage;
			mr.reflectionProbeUsage = this.reflectionProbeUsage;
			mr.motionVectorGenerationMode = this.motionVectorGenerationMode;
			go.layer = this.layer;
		}

		// Token: 0x04002311 RID: 8977
		public static HashSet<object> countSet = new HashSet<object>();

		// Token: 0x04002312 RID: 8978
		public int matInstanceId;

		// Token: 0x04002313 RID: 8979
		public int lightmapIndex;

		// Token: 0x04002314 RID: 8980
		public ShadowCastingMode shadowCastingMode;

		// Token: 0x04002315 RID: 8981
		public bool receiveShadows;

		// Token: 0x04002316 RID: 8982
		public LightProbeUsage lightProbeUsage;

		// Token: 0x04002317 RID: 8983
		public ReflectionProbeUsage reflectionProbeUsage;

		// Token: 0x04002318 RID: 8984
		public Transform probeAnchor;

		// Token: 0x04002319 RID: 8985
		public MotionVectorGenerationMode motionVectorGenerationMode;

		// Token: 0x0400231A RID: 8986
		public int layer;
	}
}
