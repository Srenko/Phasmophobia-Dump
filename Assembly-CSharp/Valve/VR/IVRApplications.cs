using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000386 RID: 902
	public struct IVRApplications
	{
		// Token: 0x0400184B RID: 6219
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._AddApplicationManifest AddApplicationManifest;

		// Token: 0x0400184C RID: 6220
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._RemoveApplicationManifest RemoveApplicationManifest;

		// Token: 0x0400184D RID: 6221
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._IsApplicationInstalled IsApplicationInstalled;

		// Token: 0x0400184E RID: 6222
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationCount GetApplicationCount;

		// Token: 0x0400184F RID: 6223
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationKeyByIndex GetApplicationKeyByIndex;

		// Token: 0x04001850 RID: 6224
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationKeyByProcessId GetApplicationKeyByProcessId;

		// Token: 0x04001851 RID: 6225
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._LaunchApplication LaunchApplication;

		// Token: 0x04001852 RID: 6226
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._LaunchTemplateApplication LaunchTemplateApplication;

		// Token: 0x04001853 RID: 6227
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._LaunchApplicationFromMimeType LaunchApplicationFromMimeType;

		// Token: 0x04001854 RID: 6228
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._LaunchDashboardOverlay LaunchDashboardOverlay;

		// Token: 0x04001855 RID: 6229
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._CancelApplicationLaunch CancelApplicationLaunch;

		// Token: 0x04001856 RID: 6230
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._IdentifyApplication IdentifyApplication;

		// Token: 0x04001857 RID: 6231
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationProcessId GetApplicationProcessId;

		// Token: 0x04001858 RID: 6232
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationsErrorNameFromEnum GetApplicationsErrorNameFromEnum;

		// Token: 0x04001859 RID: 6233
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationPropertyString GetApplicationPropertyString;

		// Token: 0x0400185A RID: 6234
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationPropertyBool GetApplicationPropertyBool;

		// Token: 0x0400185B RID: 6235
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationPropertyUint64 GetApplicationPropertyUint64;

		// Token: 0x0400185C RID: 6236
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._SetApplicationAutoLaunch SetApplicationAutoLaunch;

		// Token: 0x0400185D RID: 6237
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationAutoLaunch GetApplicationAutoLaunch;

		// Token: 0x0400185E RID: 6238
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._SetDefaultApplicationForMimeType SetDefaultApplicationForMimeType;

		// Token: 0x0400185F RID: 6239
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetDefaultApplicationForMimeType GetDefaultApplicationForMimeType;

		// Token: 0x04001860 RID: 6240
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationSupportedMimeTypes GetApplicationSupportedMimeTypes;

		// Token: 0x04001861 RID: 6241
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationsThatSupportMimeType GetApplicationsThatSupportMimeType;

		// Token: 0x04001862 RID: 6242
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationLaunchArguments GetApplicationLaunchArguments;

		// Token: 0x04001863 RID: 6243
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetStartingApplication GetStartingApplication;

		// Token: 0x04001864 RID: 6244
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetTransitionState GetTransitionState;

		// Token: 0x04001865 RID: 6245
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._PerformApplicationPrelaunchCheck PerformApplicationPrelaunchCheck;

		// Token: 0x04001866 RID: 6246
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetApplicationsTransitionStateNameFromEnum GetApplicationsTransitionStateNameFromEnum;

		// Token: 0x04001867 RID: 6247
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._IsQuitUserPromptRequested IsQuitUserPromptRequested;

		// Token: 0x04001868 RID: 6248
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._LaunchInternalProcess LaunchInternalProcess;

		// Token: 0x04001869 RID: 6249
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRApplications._GetCurrentSceneProcessId GetCurrentSceneProcessId;

		// Token: 0x02000682 RID: 1666
		// (Invoke) Token: 0x06002C0F RID: 11279
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _AddApplicationManifest(string pchApplicationManifestFullPath, bool bTemporary);

		// Token: 0x02000683 RID: 1667
		// (Invoke) Token: 0x06002C13 RID: 11283
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _RemoveApplicationManifest(string pchApplicationManifestFullPath);

		// Token: 0x02000684 RID: 1668
		// (Invoke) Token: 0x06002C17 RID: 11287
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsApplicationInstalled(string pchAppKey);

		// Token: 0x02000685 RID: 1669
		// (Invoke) Token: 0x06002C1B RID: 11291
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetApplicationCount();

		// Token: 0x02000686 RID: 1670
		// (Invoke) Token: 0x06002C1F RID: 11295
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _GetApplicationKeyByIndex(uint unApplicationIndex, StringBuilder pchAppKeyBuffer, uint unAppKeyBufferLen);

		// Token: 0x02000687 RID: 1671
		// (Invoke) Token: 0x06002C23 RID: 11299
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _GetApplicationKeyByProcessId(uint unProcessId, string pchAppKeyBuffer, uint unAppKeyBufferLen);

		// Token: 0x02000688 RID: 1672
		// (Invoke) Token: 0x06002C27 RID: 11303
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _LaunchApplication(string pchAppKey);

		// Token: 0x02000689 RID: 1673
		// (Invoke) Token: 0x06002C2B RID: 11307
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _LaunchTemplateApplication(string pchTemplateAppKey, string pchNewAppKey, [In] [Out] AppOverrideKeys_t[] pKeys, uint unKeys);

		// Token: 0x0200068A RID: 1674
		// (Invoke) Token: 0x06002C2F RID: 11311
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _LaunchApplicationFromMimeType(string pchMimeType, string pchArgs);

		// Token: 0x0200068B RID: 1675
		// (Invoke) Token: 0x06002C33 RID: 11315
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _LaunchDashboardOverlay(string pchAppKey);

		// Token: 0x0200068C RID: 1676
		// (Invoke) Token: 0x06002C37 RID: 11319
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _CancelApplicationLaunch(string pchAppKey);

		// Token: 0x0200068D RID: 1677
		// (Invoke) Token: 0x06002C3B RID: 11323
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _IdentifyApplication(uint unProcessId, string pchAppKey);

		// Token: 0x0200068E RID: 1678
		// (Invoke) Token: 0x06002C3F RID: 11327
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetApplicationProcessId(string pchAppKey);

		// Token: 0x0200068F RID: 1679
		// (Invoke) Token: 0x06002C43 RID: 11331
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetApplicationsErrorNameFromEnum(EVRApplicationError error);

		// Token: 0x02000690 RID: 1680
		// (Invoke) Token: 0x06002C47 RID: 11335
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetApplicationPropertyString(string pchAppKey, EVRApplicationProperty eProperty, StringBuilder pchPropertyValueBuffer, uint unPropertyValueBufferLen, ref EVRApplicationError peError);

		// Token: 0x02000691 RID: 1681
		// (Invoke) Token: 0x06002C4B RID: 11339
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetApplicationPropertyBool(string pchAppKey, EVRApplicationProperty eProperty, ref EVRApplicationError peError);

		// Token: 0x02000692 RID: 1682
		// (Invoke) Token: 0x06002C4F RID: 11343
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate ulong _GetApplicationPropertyUint64(string pchAppKey, EVRApplicationProperty eProperty, ref EVRApplicationError peError);

		// Token: 0x02000693 RID: 1683
		// (Invoke) Token: 0x06002C53 RID: 11347
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _SetApplicationAutoLaunch(string pchAppKey, bool bAutoLaunch);

		// Token: 0x02000694 RID: 1684
		// (Invoke) Token: 0x06002C57 RID: 11351
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetApplicationAutoLaunch(string pchAppKey);

		// Token: 0x02000695 RID: 1685
		// (Invoke) Token: 0x06002C5B RID: 11355
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _SetDefaultApplicationForMimeType(string pchAppKey, string pchMimeType);

		// Token: 0x02000696 RID: 1686
		// (Invoke) Token: 0x06002C5F RID: 11359
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetDefaultApplicationForMimeType(string pchMimeType, string pchAppKeyBuffer, uint unAppKeyBufferLen);

		// Token: 0x02000697 RID: 1687
		// (Invoke) Token: 0x06002C63 RID: 11363
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _GetApplicationSupportedMimeTypes(string pchAppKey, string pchMimeTypesBuffer, uint unMimeTypesBuffer);

		// Token: 0x02000698 RID: 1688
		// (Invoke) Token: 0x06002C67 RID: 11367
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetApplicationsThatSupportMimeType(string pchMimeType, string pchAppKeysThatSupportBuffer, uint unAppKeysThatSupportBuffer);

		// Token: 0x02000699 RID: 1689
		// (Invoke) Token: 0x06002C6B RID: 11371
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetApplicationLaunchArguments(uint unHandle, string pchArgs, uint unArgs);

		// Token: 0x0200069A RID: 1690
		// (Invoke) Token: 0x06002C6F RID: 11375
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _GetStartingApplication(string pchAppKeyBuffer, uint unAppKeyBufferLen);

		// Token: 0x0200069B RID: 1691
		// (Invoke) Token: 0x06002C73 RID: 11379
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationTransitionState _GetTransitionState();

		// Token: 0x0200069C RID: 1692
		// (Invoke) Token: 0x06002C77 RID: 11383
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _PerformApplicationPrelaunchCheck(string pchAppKey);

		// Token: 0x0200069D RID: 1693
		// (Invoke) Token: 0x06002C7B RID: 11387
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate IntPtr _GetApplicationsTransitionStateNameFromEnum(EVRApplicationTransitionState state);

		// Token: 0x0200069E RID: 1694
		// (Invoke) Token: 0x06002C7F RID: 11391
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate bool _IsQuitUserPromptRequested();

		// Token: 0x0200069F RID: 1695
		// (Invoke) Token: 0x06002C83 RID: 11395
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate EVRApplicationError _LaunchInternalProcess(string pchBinaryPath, string pchArguments, string pchWorkingDirectory);

		// Token: 0x020006A0 RID: 1696
		// (Invoke) Token: 0x06002C87 RID: 11399
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetCurrentSceneProcessId();
	}
}
