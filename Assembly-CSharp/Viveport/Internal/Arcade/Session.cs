using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal.Arcade
{
	// Token: 0x0200022C RID: 556
	internal class Session
	{
		// Token: 0x06000FA9 RID: 4009
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IViveportArcadeSession_IsReady")]
		internal static extern void IsReady(SessionCallback callback);

		// Token: 0x06000FAA RID: 4010
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IViveportArcadeSession_IsReady")]
		internal static extern void IsReady_64(SessionCallback callback);

		// Token: 0x06000FAB RID: 4011
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IViveportArcadeSession_Start")]
		internal static extern void Start(SessionCallback callback);

		// Token: 0x06000FAC RID: 4012
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IViveportArcadeSession_Start")]
		internal static extern void Start_64(SessionCallback callback);

		// Token: 0x06000FAD RID: 4013
		[DllImport("viveport_api", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IViveportArcadeSession_Stop")]
		internal static extern void Stop(SessionCallback callback);

		// Token: 0x06000FAE RID: 4014
		[DllImport("viveport_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IViveportArcadeSession_Stop")]
		internal static extern void Stop_64(SessionCallback callback);
	}
}
