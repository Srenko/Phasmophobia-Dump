using System;
using System.Linq;

namespace VRTK
{
	// Token: 0x02000267 RID: 615
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class SDK_DescriptionAttribute : Attribute
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x000691F6 File Offset: 0x000673F6
		public bool describesFallbackSDK
		{
			get
			{
				return this.prettyName == "Fallback";
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00069208 File Offset: 0x00067408
		public SDK_DescriptionAttribute(string prettyName, string symbol, string vrDeviceName, string buildTargetGroupName, int index = 0)
		{
			if (prettyName == null)
			{
				VRTK_Logger.Fatal(new ArgumentNullException("prettyName"));
				return;
			}
			if (prettyName == string.Empty)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("prettyName", prettyName, "An empty string isn't allowed."));
				return;
			}
			this.prettyName = prettyName;
			this.symbol = symbol;
			this.vrDeviceName = (string.IsNullOrEmpty(vrDeviceName) ? "None" : vrDeviceName);
			this.index = index;
			if (string.IsNullOrEmpty(buildTargetGroupName))
			{
				buildTargetGroupName = "Unknown";
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00069290 File Offset: 0x00067490
		public SDK_DescriptionAttribute(Type typeToCopyExistingDescriptionFrom, int index = 0)
		{
			if (typeToCopyExistingDescriptionFrom == null)
			{
				VRTK_Logger.Fatal(new ArgumentNullException("typeToCopyExistingDescriptionFrom"));
				return;
			}
			Type typeFromHandle = typeof(SDK_DescriptionAttribute);
			SDK_DescriptionAttribute[] descriptions = SDK_DescriptionAttribute.GetDescriptions(typeToCopyExistingDescriptionFrom);
			if (descriptions.Length == 0)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("typeToCopyExistingDescriptionFrom", typeToCopyExistingDescriptionFrom, string.Format("'{0}' doesn't specify any SDK descriptions via '{1}' to copy.", typeToCopyExistingDescriptionFrom.Name, typeFromHandle.Name)));
				return;
			}
			if (descriptions.Length <= index)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("index", index, string.Format("'{0}' has no '{1}' at that index.", typeToCopyExistingDescriptionFrom.Name, typeFromHandle.Name)));
				return;
			}
			SDK_DescriptionAttribute sdk_DescriptionAttribute = descriptions[index];
			this.prettyName = sdk_DescriptionAttribute.prettyName;
			this.symbol = sdk_DescriptionAttribute.symbol;
			this.vrDeviceName = sdk_DescriptionAttribute.vrDeviceName;
			this.index = index;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0006935C File Offset: 0x0006755C
		public static SDK_DescriptionAttribute[] GetDescriptions(Type type)
		{
			return (from SDK_DescriptionAttribute attribute in type.GetCustomAttributes(typeof(SDK_DescriptionAttribute), false)
			orderby attribute.index
			select attribute).ToArray<SDK_DescriptionAttribute>();
		}

		// Token: 0x040010A6 RID: 4262
		public readonly string prettyName;

		// Token: 0x040010A7 RID: 4263
		public readonly string symbol;

		// Token: 0x040010A8 RID: 4264
		public readonly string vrDeviceName;

		// Token: 0x040010A9 RID: 4265
		public readonly int index;
	}
}
