using System;
using System.Runtime.InteropServices;

namespace Valve.VR
{
	// Token: 0x0200038F RID: 911
	public struct IVRResources
	{
		// Token: 0x04001926 RID: 6438
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRResources._LoadSharedResource LoadSharedResource;

		// Token: 0x04001927 RID: 6439
		[MarshalAs(UnmanagedType.FunctionPtr)]
		internal IVRResources._GetResourceFullPath GetResourceFullPath;

		// Token: 0x0200075D RID: 1885
		// (Invoke) Token: 0x06002F7B RID: 12155
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _LoadSharedResource(string pchResourceName, string pchBuffer, uint unBufferLen);

		// Token: 0x0200075E RID: 1886
		// (Invoke) Token: 0x06002F7F RID: 12159
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate uint _GetResourceFullPath(string pchResourceName, string pchResourceTypeDirectory, string pchPathBuffer, uint unBufferLen);
	}
}
