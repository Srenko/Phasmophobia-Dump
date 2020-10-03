using System;
using System.Text;
using AOT;
using Viveport.Internal;

namespace Viveport
{
	// Token: 0x0200020B RID: 523
	public class User
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0005D9E9 File Offset: 0x0005BBE9
		[MonoPInvokeCallback(typeof(StatusCallback))]
		private static void IsReadyIl2cppCallback(int errorCode)
		{
			User.isReadyIl2cppCallback(errorCode);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0005D9F8 File Offset: 0x0005BBF8
		public static int IsReady(StatusCallback callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}
			User.isReadyIl2cppCallback = new StatusCallback(callback.Invoke);
			Api.InternalStatusCallbacks.Add(new StatusCallback(User.IsReadyIl2cppCallback));
			if (IntPtr.Size == 8)
			{
				return User.IsReady_64(new StatusCallback(User.IsReadyIl2cppCallback));
			}
			return User.IsReady(new StatusCallback(User.IsReadyIl2cppCallback));
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0005DA68 File Offset: 0x0005BC68
		public static string GetUserId()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (IntPtr.Size == 8)
			{
				User.GetUserID_64(stringBuilder, 256);
			}
			else
			{
				User.GetUserID(stringBuilder, 256);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0005DAA8 File Offset: 0x0005BCA8
		public static string GetUserName()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (IntPtr.Size == 8)
			{
				User.GetUserName_64(stringBuilder, 256);
			}
			else
			{
				User.GetUserName(stringBuilder, 256);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0005DAE8 File Offset: 0x0005BCE8
		public static string GetUserAvatarUrl()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			if (IntPtr.Size == 8)
			{
				User.GetUserAvatarUrl_64(stringBuilder, 512);
			}
			else
			{
				User.GetUserAvatarUrl(stringBuilder, 512);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000EE7 RID: 3815
		private static StatusCallback isReadyIl2cppCallback;

		// Token: 0x04000EE8 RID: 3816
		private const int MaxIdLength = 256;

		// Token: 0x04000EE9 RID: 3817
		private const int MaxNameLength = 256;

		// Token: 0x04000EEA RID: 3818
		private const int MaxUrlLength = 512;
	}
}
