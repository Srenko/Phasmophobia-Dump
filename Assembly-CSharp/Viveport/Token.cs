using System;
using AOT;
using Viveport.Internal;

namespace Viveport
{
	// Token: 0x02000212 RID: 530
	internal class Token
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0005EAFC File Offset: 0x0005CCFC
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void IsReadyIl2cppCallback(int errorCode)
		{
			Token.isReadyIl2cppCallback(errorCode);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0005EB0C File Offset: 0x0005CD0C
		public static void IsReady(StatusCallback callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			Token.isReadyIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(Token.IsReadyIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				Token.IsReady_64(new StatusCallback(Token.IsReadyIl2cppCallback));
				return;
			}
			Token.IsReady(new StatusCallback(Token.IsReadyIl2cppCallback));
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0005EB7B File Offset: 0x0005CD7B
		[MonoPInvokeCallback(typeof(StatusCallback2))]
		private static void GetSessionTokenIl2cppCallback(int errorCode, string message)
		{
			Token.getSessionTokenIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0005EB8C File Offset: 0x0005CD8C
		public static void GetSessionToken(StatusCallback2 callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			Token.getSessionTokenIl2cppCallback = new StatusCallback2(callback.Invoke);
			Api.InternalStatusCallback2s.Add(new StatusCallback2(Token.GetSessionTokenIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				Token.GetSessionToken_64(new StatusCallback2(Token.GetSessionTokenIl2cppCallback));
				return;
			}
			Token.GetSessionToken(new StatusCallback2(Token.GetSessionTokenIl2cppCallback));
		}

		// Token: 0x04000F09 RID: 3849
		private static StatusCallback isReadyIl2cppCallback;

		// Token: 0x04000F0A RID: 3850
		private static StatusCallback2 getSessionTokenIl2cppCallback;
	}
}
