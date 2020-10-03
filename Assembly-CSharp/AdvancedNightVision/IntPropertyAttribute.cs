using System;
using UnityEngine;

namespace AdvancedNightVision
{
	// Token: 0x02000479 RID: 1145
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class IntPropertyAttribute : PropertyAttribute
	{
		// Token: 0x060023D3 RID: 9171 RVA: 0x000AFC18 File Offset: 0x000ADE18
		public IntPropertyAttribute(int min, int max, int defaultColor, string tooltip = "")
		{
			this.min = min;
			this.max = max;
			this.defaultValue = defaultColor;
			this.tooltip = tooltip;
		}

		// Token: 0x04002131 RID: 8497
		public readonly int min;

		// Token: 0x04002132 RID: 8498
		public readonly int max;

		// Token: 0x04002133 RID: 8499
		public readonly int defaultValue;

		// Token: 0x04002134 RID: 8500
		public readonly string tooltip;
	}
}
