using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Valve.VR
{
	// Token: 0x02000394 RID: 916
	public class CVRApplications
	{
		// Token: 0x06001F14 RID: 7956 RVA: 0x0009CF5F File Offset: 0x0009B15F
		internal CVRApplications(IntPtr pInterface)
		{
			this.FnTable = (IVRApplications)Marshal.PtrToStructure(pInterface, typeof(IVRApplications));
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0009CF82 File Offset: 0x0009B182
		public EVRApplicationError AddApplicationManifest(string pchApplicationManifestFullPath, bool bTemporary)
		{
			return this.FnTable.AddApplicationManifest(pchApplicationManifestFullPath, bTemporary);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0009CF96 File Offset: 0x0009B196
		public EVRApplicationError RemoveApplicationManifest(string pchApplicationManifestFullPath)
		{
			return this.FnTable.RemoveApplicationManifest(pchApplicationManifestFullPath);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0009CFA9 File Offset: 0x0009B1A9
		public bool IsApplicationInstalled(string pchAppKey)
		{
			return this.FnTable.IsApplicationInstalled(pchAppKey);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0009CFBC File Offset: 0x0009B1BC
		public uint GetApplicationCount()
		{
			return this.FnTable.GetApplicationCount();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0009CFCE File Offset: 0x0009B1CE
		public EVRApplicationError GetApplicationKeyByIndex(uint unApplicationIndex, StringBuilder pchAppKeyBuffer, uint unAppKeyBufferLen)
		{
			return this.FnTable.GetApplicationKeyByIndex(unApplicationIndex, pchAppKeyBuffer, unAppKeyBufferLen);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0009CFE3 File Offset: 0x0009B1E3
		public EVRApplicationError GetApplicationKeyByProcessId(uint unProcessId, string pchAppKeyBuffer, uint unAppKeyBufferLen)
		{
			return this.FnTable.GetApplicationKeyByProcessId(unProcessId, pchAppKeyBuffer, unAppKeyBufferLen);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0009CFF8 File Offset: 0x0009B1F8
		public EVRApplicationError LaunchApplication(string pchAppKey)
		{
			return this.FnTable.LaunchApplication(pchAppKey);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0009D00B File Offset: 0x0009B20B
		public EVRApplicationError LaunchTemplateApplication(string pchTemplateAppKey, string pchNewAppKey, AppOverrideKeys_t[] pKeys)
		{
			return this.FnTable.LaunchTemplateApplication(pchTemplateAppKey, pchNewAppKey, pKeys, (uint)pKeys.Length);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0009D023 File Offset: 0x0009B223
		public EVRApplicationError LaunchApplicationFromMimeType(string pchMimeType, string pchArgs)
		{
			return this.FnTable.LaunchApplicationFromMimeType(pchMimeType, pchArgs);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0009D037 File Offset: 0x0009B237
		public EVRApplicationError LaunchDashboardOverlay(string pchAppKey)
		{
			return this.FnTable.LaunchDashboardOverlay(pchAppKey);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0009D04A File Offset: 0x0009B24A
		public bool CancelApplicationLaunch(string pchAppKey)
		{
			return this.FnTable.CancelApplicationLaunch(pchAppKey);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0009D05D File Offset: 0x0009B25D
		public EVRApplicationError IdentifyApplication(uint unProcessId, string pchAppKey)
		{
			return this.FnTable.IdentifyApplication(unProcessId, pchAppKey);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0009D071 File Offset: 0x0009B271
		public uint GetApplicationProcessId(string pchAppKey)
		{
			return this.FnTable.GetApplicationProcessId(pchAppKey);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0009D084 File Offset: 0x0009B284
		public string GetApplicationsErrorNameFromEnum(EVRApplicationError error)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetApplicationsErrorNameFromEnum(error));
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0009D09C File Offset: 0x0009B29C
		public uint GetApplicationPropertyString(string pchAppKey, EVRApplicationProperty eProperty, StringBuilder pchPropertyValueBuffer, uint unPropertyValueBufferLen, ref EVRApplicationError peError)
		{
			return this.FnTable.GetApplicationPropertyString(pchAppKey, eProperty, pchPropertyValueBuffer, unPropertyValueBufferLen, ref peError);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0009D0B5 File Offset: 0x0009B2B5
		public bool GetApplicationPropertyBool(string pchAppKey, EVRApplicationProperty eProperty, ref EVRApplicationError peError)
		{
			return this.FnTable.GetApplicationPropertyBool(pchAppKey, eProperty, ref peError);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0009D0CA File Offset: 0x0009B2CA
		public ulong GetApplicationPropertyUint64(string pchAppKey, EVRApplicationProperty eProperty, ref EVRApplicationError peError)
		{
			return this.FnTable.GetApplicationPropertyUint64(pchAppKey, eProperty, ref peError);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0009D0DF File Offset: 0x0009B2DF
		public EVRApplicationError SetApplicationAutoLaunch(string pchAppKey, bool bAutoLaunch)
		{
			return this.FnTable.SetApplicationAutoLaunch(pchAppKey, bAutoLaunch);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0009D0F3 File Offset: 0x0009B2F3
		public bool GetApplicationAutoLaunch(string pchAppKey)
		{
			return this.FnTable.GetApplicationAutoLaunch(pchAppKey);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0009D106 File Offset: 0x0009B306
		public EVRApplicationError SetDefaultApplicationForMimeType(string pchAppKey, string pchMimeType)
		{
			return this.FnTable.SetDefaultApplicationForMimeType(pchAppKey, pchMimeType);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0009D11A File Offset: 0x0009B31A
		public bool GetDefaultApplicationForMimeType(string pchMimeType, string pchAppKeyBuffer, uint unAppKeyBufferLen)
		{
			return this.FnTable.GetDefaultApplicationForMimeType(pchMimeType, pchAppKeyBuffer, unAppKeyBufferLen);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0009D12F File Offset: 0x0009B32F
		public bool GetApplicationSupportedMimeTypes(string pchAppKey, string pchMimeTypesBuffer, uint unMimeTypesBuffer)
		{
			return this.FnTable.GetApplicationSupportedMimeTypes(pchAppKey, pchMimeTypesBuffer, unMimeTypesBuffer);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0009D144 File Offset: 0x0009B344
		public uint GetApplicationsThatSupportMimeType(string pchMimeType, string pchAppKeysThatSupportBuffer, uint unAppKeysThatSupportBuffer)
		{
			return this.FnTable.GetApplicationsThatSupportMimeType(pchMimeType, pchAppKeysThatSupportBuffer, unAppKeysThatSupportBuffer);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0009D159 File Offset: 0x0009B359
		public uint GetApplicationLaunchArguments(uint unHandle, string pchArgs, uint unArgs)
		{
			return this.FnTable.GetApplicationLaunchArguments(unHandle, pchArgs, unArgs);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0009D16E File Offset: 0x0009B36E
		public EVRApplicationError GetStartingApplication(string pchAppKeyBuffer, uint unAppKeyBufferLen)
		{
			return this.FnTable.GetStartingApplication(pchAppKeyBuffer, unAppKeyBufferLen);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0009D182 File Offset: 0x0009B382
		public EVRApplicationTransitionState GetTransitionState()
		{
			return this.FnTable.GetTransitionState();
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0009D194 File Offset: 0x0009B394
		public EVRApplicationError PerformApplicationPrelaunchCheck(string pchAppKey)
		{
			return this.FnTable.PerformApplicationPrelaunchCheck(pchAppKey);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0009D1A7 File Offset: 0x0009B3A7
		public string GetApplicationsTransitionStateNameFromEnum(EVRApplicationTransitionState state)
		{
			return Marshal.PtrToStringAnsi(this.FnTable.GetApplicationsTransitionStateNameFromEnum(state));
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0009D1BF File Offset: 0x0009B3BF
		public bool IsQuitUserPromptRequested()
		{
			return this.FnTable.IsQuitUserPromptRequested();
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0009D1D1 File Offset: 0x0009B3D1
		public EVRApplicationError LaunchInternalProcess(string pchBinaryPath, string pchArguments, string pchWorkingDirectory)
		{
			return this.FnTable.LaunchInternalProcess(pchBinaryPath, pchArguments, pchWorkingDirectory);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0009D1E6 File Offset: 0x0009B3E6
		public uint GetCurrentSceneProcessId()
		{
			return this.FnTable.GetCurrentSceneProcessId();
		}

		// Token: 0x0400192D RID: 6445
		private IVRApplications FnTable;
	}
}
