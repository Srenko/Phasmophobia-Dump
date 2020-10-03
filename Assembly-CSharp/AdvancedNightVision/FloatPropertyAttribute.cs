using System;
using UnityEngine;

namespace AdvancedNightVision
{
	// Token: 0x02000478 RID: 1144
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class FloatPropertyAttribute : PropertyAttribute
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x000AFBBC File Offset: 0x000ADDBC
		public FloatPropertyAttribute(string tooltip = "")
		{
			this.min = (this.max = (this.defaultValue = 0f));
			this.tooltip = tooltip;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000AFBF3 File Offset: 0x000ADDF3
		public FloatPropertyAttribute(float min, float max, float defaultColor, string tooltip = "")
		{
			this.min = min;
			this.max = max;
			this.defaultValue = defaultColor;
			this.tooltip = tooltip;
		}

		// Token: 0x0400212D RID: 8493
		public readonly float min;

		// Token: 0x0400212E RID: 8494
		public readonly float max;

		// Token: 0x0400212F RID: 8495
		public readonly float defaultValue;

		// Token: 0x04002130 RID: 8496
		public readonly string tooltip;
	}
}
