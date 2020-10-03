using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000268 RID: 616
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SDK_ScriptingDefineSymbolPredicateAttribute : Attribute, ISerializationCallbackReceiver
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		private SDK_ScriptingDefineSymbolPredicateAttribute()
		{
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000693A8 File Offset: 0x000675A8
		public SDK_ScriptingDefineSymbolPredicateAttribute(string symbol, string buildTargetGroupName)
		{
			if (symbol == null)
			{
				VRTK_Logger.Fatal(new ArgumentNullException("symbol"));
				return;
			}
			if (symbol == string.Empty)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("symbol", symbol, "An empty string isn't allowed."));
				return;
			}
			this.symbol = symbol;
			if (buildTargetGroupName == null)
			{
				VRTK_Logger.Fatal(new ArgumentNullException("buildTargetGroupName"));
				return;
			}
			if (buildTargetGroupName == string.Empty)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("buildTargetGroupName", buildTargetGroupName, "An empty string isn't allowed."));
				return;
			}
			this.SetBuildTarget(buildTargetGroupName);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00069435 File Offset: 0x00067635
		public SDK_ScriptingDefineSymbolPredicateAttribute(SDK_ScriptingDefineSymbolPredicateAttribute attributeToCopy)
		{
			this.symbol = attributeToCopy.symbol;
			this.SetBuildTarget(attributeToCopy.buildTargetGroupName);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00003F60 File Offset: 0x00002160
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00069455 File Offset: 0x00067655
		public void OnAfterDeserialize()
		{
			this.SetBuildTarget(this.buildTargetGroupName);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00069463 File Offset: 0x00067663
		private void SetBuildTarget(string groupName)
		{
			this.buildTargetGroupName = groupName;
		}

		// Token: 0x040010AA RID: 4266
		public const string RemovableSymbolPrefix = "VRTK_DEFINE_";

		// Token: 0x040010AB RID: 4267
		public string symbol;

		// Token: 0x040010AC RID: 4268
		[SerializeField]
		private string buildTargetGroupName;
	}
}
