using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200039D RID: 925
	public class CVRResources
	{
		// Token: 0x06001FF8 RID: 8184 RVA: 0x0009E47D File Offset: 0x0009C67D
		internal CVRResources(IntPtr pInterface)
		{
			this.FnTable = (IVRResources)Marshal.PtrToStructure(pInterface, typeof(IVRResources));
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0009E4A0 File Offset: 0x0009C6A0
		public uint LoadSharedResource(string pchResourceName, string pchBuffer, uint unBufferLen)
		{
			return this.FnTable.LoadSharedResource(pchResourceName, pchBuffer, unBufferLen);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0009E4B5 File Offset: 0x0009C6B5
		public uint GetResourceFullPath(string pchResourceName, string pchResourceTypeDirectory, string pchPathBuffer, uint unBufferLen)
		{
			return this.FnTable.GetResourceFullPath(pchResourceName, pchResourceTypeDirectory, pchPathBuffer, unBufferLen);
		}

		// Token: 0x04001936 RID: 6454
		private IVRResources FnTable;
	}
}
