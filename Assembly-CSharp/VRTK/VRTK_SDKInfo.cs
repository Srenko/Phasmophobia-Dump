using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200030C RID: 780
	[Serializable]
	public sealed class VRTK_SDKInfo : ISerializationCallbackReceiver
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0008DF13 File Offset: 0x0008C113
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x0008DF1B File Offset: 0x0008C11B
		public Type type { get; private set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0008DF24 File Offset: 0x0008C124
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x0008DF2C File Offset: 0x0008C12C
		public string originalTypeNameWhenFallbackIsUsed { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0008DF35 File Offset: 0x0008C135
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x0008DF3D File Offset: 0x0008C13D
		public SDK_DescriptionAttribute description { get; private set; }

		// Token: 0x06001B1E RID: 6942 RVA: 0x0008DF46 File Offset: 0x0008C146
		public static VRTK_SDKInfo[] Create<BaseType, FallbackType, ActualType>() where BaseType : SDK_Base where FallbackType : BaseType where ActualType : BaseType
		{
			return VRTK_SDKInfo.Create<BaseType, FallbackType>(typeof(ActualType));
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0008DF58 File Offset: 0x0008C158
		public static VRTK_SDKInfo[] Create<BaseType, FallbackType>(Type actualType) where BaseType : SDK_Base where FallbackType : BaseType
		{
			string fullName = actualType.FullName;
			SDK_DescriptionAttribute[] descriptions = SDK_DescriptionAttribute.GetDescriptions(actualType);
			if (descriptions.Length == 0)
			{
				VRTK_Logger.Fatal(string.Format("'{0}' doesn't specify any SDK descriptions via '{1}'.", fullName, typeof(SDK_DescriptionAttribute).Name));
				return new VRTK_SDKInfo[0];
			}
			List<VRTK_SDKInfo> list = new List<VRTK_SDKInfo>(descriptions.Length);
			foreach (SDK_DescriptionAttribute sdk_DescriptionAttribute in descriptions)
			{
				VRTK_SDKInfo vrtk_SDKInfo = new VRTK_SDKInfo();
				vrtk_SDKInfo.SetUp(typeof(BaseType), typeof(FallbackType), fullName, sdk_DescriptionAttribute.index);
				list.Add(vrtk_SDKInfo);
			}
			return list.ToArray();
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00008842 File Offset: 0x00006A42
		private VRTK_SDKInfo()
		{
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0008DFF8 File Offset: 0x0008C1F8
		public VRTK_SDKInfo(VRTK_SDKInfo infoToCopy)
		{
			this.SetUp(Type.GetType(infoToCopy.baseTypeName), Type.GetType(infoToCopy.fallbackTypeName), infoToCopy.typeName, infoToCopy.descriptionIndex);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0008E028 File Offset: 0x0008C228
		private void SetUp(Type baseType, Type fallbackType, string actualTypeName, int descriptionIndex)
		{
			if (baseType == null || fallbackType == null)
			{
				return;
			}
			if (!baseType.IsSubclassOf(typeof(SDK_Base)))
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("baseType", baseType, string.Format("'{0}' is not a subclass of the SDK base type '{1}'.", baseType.Name, typeof(SDK_Base).Name)));
				return;
			}
			if (!fallbackType.IsSubclassOf(baseType))
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("fallbackType", fallbackType, string.Format("'{0}' is not a subclass of the SDK base type '{1}'.", fallbackType.Name, baseType.Name)));
				return;
			}
			this.baseTypeName = baseType.FullName;
			this.fallbackTypeName = fallbackType.FullName;
			this.typeName = actualTypeName;
			if (string.IsNullOrEmpty(actualTypeName))
			{
				this.type = fallbackType;
				this.originalTypeNameWhenFallbackIsUsed = null;
				this.descriptionIndex = -1;
				this.description = new SDK_DescriptionAttribute(typeof(SDK_FallbackSystem), 0);
				return;
			}
			Type type = Type.GetType(actualTypeName);
			if (type == null)
			{
				this.type = fallbackType;
				this.originalTypeNameWhenFallbackIsUsed = actualTypeName;
				this.descriptionIndex = -1;
				this.description = new SDK_DescriptionAttribute(typeof(SDK_FallbackSystem), 0);
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.SDK_NOT_FOUND, new object[]
				{
					actualTypeName,
					fallbackType.Name
				}));
				return;
			}
			if (!type.IsSubclassOf(baseType))
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("actualTypeName", actualTypeName, string.Format("'{0}' is not a subclass of the SDK base type '{1}'.", actualTypeName, baseType.Name)));
				return;
			}
			SDK_DescriptionAttribute[] descriptions = SDK_DescriptionAttribute.GetDescriptions(type);
			if (descriptions.Length <= descriptionIndex)
			{
				VRTK_Logger.Fatal(new ArgumentOutOfRangeException("descriptionIndex", descriptionIndex, string.Format("'{0}' has no '{1}' at that index.", actualTypeName, typeof(SDK_DescriptionAttribute).Name)));
				return;
			}
			this.type = type;
			this.originalTypeNameWhenFallbackIsUsed = null;
			this.descriptionIndex = descriptionIndex;
			this.description = descriptions[descriptionIndex];
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00003F60 File Offset: 0x00002160
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0008E1F1 File Offset: 0x0008C3F1
		public void OnAfterDeserialize()
		{
			this.SetUp(Type.GetType(this.baseTypeName), Type.GetType(this.fallbackTypeName), this.typeName, this.descriptionIndex);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0008E21C File Offset: 0x0008C41C
		public override bool Equals(object obj)
		{
			VRTK_SDKInfo vrtk_SDKInfo = obj as VRTK_SDKInfo;
			return vrtk_SDKInfo != null && this == vrtk_SDKInfo;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0008E23C File Offset: 0x0008C43C
		public bool Equals(VRTK_SDKInfo other)
		{
			return this == other;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0008E245 File Offset: 0x0008C445
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0008E252 File Offset: 0x0008C452
		public static bool operator ==(VRTK_SDKInfo x, VRTK_SDKInfo y)
		{
			return x == y || (x != null && y != null && x.type == y.type && x.descriptionIndex == y.descriptionIndex);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0008E285 File Offset: 0x0008C485
		public static bool operator !=(VRTK_SDKInfo x, VRTK_SDKInfo y)
		{
			return !(x == y);
		}

		// Token: 0x040015E9 RID: 5609
		[SerializeField]
		private string baseTypeName;

		// Token: 0x040015EA RID: 5610
		[SerializeField]
		private string fallbackTypeName;

		// Token: 0x040015EB RID: 5611
		[SerializeField]
		private string typeName;

		// Token: 0x040015EC RID: 5612
		[SerializeField]
		private int descriptionIndex;
	}
}
