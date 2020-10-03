using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x0200038D RID: 909
	public struct IVRSettings
	{
		// Token: 0x04001913 RID: 6419
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._GetSettingsErrorNameFromEnum GetSettingsErrorNameFromEnum;

		// Token: 0x04001914 RID: 6420
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._Sync Sync;

		// Token: 0x04001915 RID: 6421
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._SetBool SetBool;

		// Token: 0x04001916 RID: 6422
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._SetInt32 SetInt32;

		// Token: 0x04001917 RID: 6423
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._SetFloat SetFloat;

		// Token: 0x04001918 RID: 6424
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._SetString SetString;

		// Token: 0x04001919 RID: 6425
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._GetBool GetBool;

		// Token: 0x0400191A RID: 6426
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._GetInt32 GetInt32;

		// Token: 0x0400191B RID: 6427
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._GetFloat GetFloat;

		// Token: 0x0400191C RID: 6428
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._GetString GetString;

		// Token: 0x0400191D RID: 6429
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._RemoveSection RemoveSection;

		// Token: 0x0400191E RID: 6430
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRSettings._RemoveKeyInSection RemoveKeyInSection;

		// Token: 0x0200074A RID: 1866
		// (Invoke) Token: 0x06002F2F RID: 12079
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetSettingsErrorNameFromEnum(EVRSettingsError eError);

		// Token: 0x0200074B RID: 1867
		// (Invoke) Token: 0x06002F33 RID: 12083
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _Sync(bool bForce, ref EVRSettingsError peError);

		// Token: 0x0200074C RID: 1868
		// (Invoke) Token: 0x06002F37 RID: 12087
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetBool(string pchSection, string pchSettingsKey, bool bValue, ref EVRSettingsError peError);

		// Token: 0x0200074D RID: 1869
		// (Invoke) Token: 0x06002F3B RID: 12091
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetInt32(string pchSection, string pchSettingsKey, int nValue, ref EVRSettingsError peError);

		// Token: 0x0200074E RID: 1870
		// (Invoke) Token: 0x06002F3F RID: 12095
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetFloat(string pchSection, string pchSettingsKey, float flValue, ref EVRSettingsError peError);

		// Token: 0x0200074F RID: 1871
		// (Invoke) Token: 0x06002F43 RID: 12099
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _SetString(string pchSection, string pchSettingsKey, string pchValue, ref EVRSettingsError peError);

		// Token: 0x02000750 RID: 1872
		// (Invoke) Token: 0x06002F47 RID: 12103
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetBool(string pchSection, string pchSettingsKey, ref EVRSettingsError peError);

		// Token: 0x02000751 RID: 1873
		// (Invoke) Token: 0x06002F4B RID: 12107
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate int _GetInt32(string pchSection, string pchSettingsKey, ref EVRSettingsError peError);

		// Token: 0x02000752 RID: 1874
		// (Invoke) Token: 0x06002F4F RID: 12111
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate float _GetFloat(string pchSection, string pchSettingsKey, ref EVRSettingsError peError);

		// Token: 0x02000753 RID: 1875
		// (Invoke) Token: 0x06002F53 RID: 12115
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _GetString(string pchSection, string pchSettingsKey, StringBuilder pchValue, uint unValueLen, ref EVRSettingsError peError);

		// Token: 0x02000754 RID: 1876
		// (Invoke) Token: 0x06002F57 RID: 12119
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _RemoveSection(string pchSection, ref EVRSettingsError peError);

		// Token: 0x02000755 RID: 1877
		// (Invoke) Token: 0x06002F5B RID: 12123
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void _RemoveKeyInSection(string pchSection, string pchSettingsKey, ref EVRSettingsError peError);
	}
}
