using System;
using System.Runtime.InteropServices;
using AOT;
using LitJson;
using Viveport.Core;
using Viveport.Internal.Arcade;

namespace Viveport.Arcade
{
	// Token: 0x0200022D RID: 557
	internal class Session
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0005EC55 File Offset: 0x0005CE55
		[MonoPInvokeCallback(typeof(SessionCallback))]
		private static void IsReadyIl2cppCallback(int errorCode, string message)
		{
			Session.isReadyIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0005EC63 File Offset: 0x0005CE63
		public static void IsReady(Session.SessionListener listener)
		{
			Session.isReadyIl2cppCallback = new Session.SessionHandler(listener).getIsReadyHandler();
			if (IntPtr.Size == 8)
			{
				Session.IsReady_64(new SessionCallback(Session.IsReadyIl2cppCallback));
				return;
			}
			Session.IsReady(new SessionCallback(Session.IsReadyIl2cppCallback));
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0005ECA0 File Offset: 0x0005CEA0
		[MonoPInvokeCallback(typeof(SessionCallback))]
		private static void StartIl2cppCallback(int errorCode, string message)
		{
			Session.startIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0005ECAE File Offset: 0x0005CEAE
		public static void Start(Session.SessionListener listener)
		{
			Session.startIl2cppCallback = new Session.SessionHandler(listener).getStartHandler();
			if (IntPtr.Size == 8)
			{
				Session.Start_64(new SessionCallback(Session.StartIl2cppCallback));
				return;
			}
			Session.Start(new SessionCallback(Session.StartIl2cppCallback));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0005ECEB File Offset: 0x0005CEEB
		[MonoPInvokeCallback(typeof(SessionCallback))]
		private static void StopIl2cppCallback(int errorCode, string message)
		{
			Session.stopIl2cppCallback(errorCode, message);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0005ECF9 File Offset: 0x0005CEF9
		public static void Stop(Session.SessionListener listener)
		{
			Session.stopIl2cppCallback = new Session.SessionHandler(listener).getStopHandler();
			if (IntPtr.Size == 8)
			{
				Session.Stop_64(new SessionCallback(Session.StopIl2cppCallback));
				return;
			}
			Session.Stop(new SessionCallback(Session.StopIl2cppCallback));
		}

		// Token: 0x04000F39 RID: 3897
		private static SessionCallback isReadyIl2cppCallback;

		// Token: 0x04000F3A RID: 3898
		private static SessionCallback startIl2cppCallback;

		// Token: 0x04000F3B RID: 3899
		private static SessionCallback stopIl2cppCallback;

		// Token: 0x020005AA RID: 1450
		private class SessionHandler : Session.BaseHandler
		{
			// Token: 0x060029B0 RID: 10672 RVA: 0x000C70EC File Offset: 0x000C52EC
			public SessionHandler(Session.SessionListener cb)
			{
				Session.SessionHandler.listener = cb;
			}

			// Token: 0x060029B1 RID: 10673 RVA: 0x000C70FA File Offset: 0x000C52FA
			public SessionCallback getIsReadyHandler()
			{
				return new SessionCallback(this.IsReadyHandler);
			}

			// Token: 0x060029B2 RID: 10674 RVA: 0x000C710C File Offset: 0x000C530C
			protected override void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log(string.Concat(new object[]
				{
					"[Session IsReadyHandler] message=",
					message,
					",code=",
					code
				}));
				JsonData jsonData = null;
				try
				{
					jsonData = JsonMapper.ToObject(message);
				}
				catch (Exception arg)
				{
					Logger.Log("[Session IsReadyHandler] exception=" + arg);
				}
				int num = -1;
				string text = "";
				string text2 = "";
				if (code == 0 && jsonData != null)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg2)
					{
						Logger.Log("[IsReadyHandler] statusCode, message ex=" + arg2);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[IsReadyHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						try
						{
							text2 = (string)jsonData["appID"];
						}
						catch (Exception arg3)
						{
							Logger.Log("[IsReadyHandler] appID ex=" + arg3);
						}
						Logger.Log("[IsReadyHandler] appID=" + text2);
					}
				}
				if (Session.SessionHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							Session.SessionHandler.listener.OnSuccess(text2);
							return;
						}
						Session.SessionHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						Session.SessionHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x060029B3 RID: 10675 RVA: 0x000C727C File Offset: 0x000C547C
			public SessionCallback getStartHandler()
			{
				return new SessionCallback(this.StartHandler);
			}

			// Token: 0x060029B4 RID: 10676 RVA: 0x000C728C File Offset: 0x000C548C
			protected override void StartHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log(string.Concat(new object[]
				{
					"[Session StartHandler] message=",
					message,
					",code=",
					code
				}));
				JsonData jsonData = null;
				try
				{
					jsonData = JsonMapper.ToObject(message);
				}
				catch (Exception arg)
				{
					Logger.Log("[Session StartHandler] exception=" + arg);
				}
				int num = -1;
				string text = "";
				string text2 = "";
				string text3 = "";
				if (code == 0 && jsonData != null)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg2)
					{
						Logger.Log("[StartHandler] statusCode, message ex=" + arg2);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[StartHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						try
						{
							text2 = (string)jsonData["appID"];
							text3 = (string)jsonData["Guid"];
						}
						catch (Exception arg3)
						{
							Logger.Log("[StartHandler] appID, Guid ex=" + arg3);
						}
						Logger.Log("[StartHandler] appID=" + text2 + ",Guid=" + text3);
					}
				}
				if (Session.SessionHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							Session.SessionHandler.listener.OnStartSuccess(text2, text3);
							return;
						}
						Session.SessionHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						Session.SessionHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x060029B5 RID: 10677 RVA: 0x000C7420 File Offset: 0x000C5620
			public SessionCallback getStopHandler()
			{
				return new SessionCallback(this.StopHandler);
			}

			// Token: 0x060029B6 RID: 10678 RVA: 0x000C7430 File Offset: 0x000C5630
			protected override void StopHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
			{
				Logger.Log(string.Concat(new object[]
				{
					"[Session StopHandler] message=",
					message,
					",code=",
					code
				}));
				JsonData jsonData = null;
				try
				{
					jsonData = JsonMapper.ToObject(message);
				}
				catch (Exception arg)
				{
					Logger.Log("[Session StopHandler] exception=" + arg);
				}
				int num = -1;
				string text = "";
				string text2 = "";
				string text3 = "";
				if (code == 0 && jsonData != null)
				{
					try
					{
						num = (int)jsonData["statusCode"];
						text = (string)jsonData["message"];
					}
					catch (Exception arg2)
					{
						Logger.Log("[StopHandler] statusCode, message ex=" + arg2);
					}
					Logger.Log(string.Concat(new object[]
					{
						"[StopHandler] statusCode =",
						num,
						",errMessage=",
						text
					}));
					if (num == 0)
					{
						try
						{
							text2 = (string)jsonData["appID"];
							text3 = (string)jsonData["Guid"];
						}
						catch (Exception arg3)
						{
							Logger.Log("[StopHandler] appID, Guid ex=" + arg3);
						}
						Logger.Log("[StopHandler] appID=" + text2 + ",Guid=" + text3);
					}
				}
				if (Session.SessionHandler.listener != null)
				{
					if (code == 0)
					{
						if (num == 0)
						{
							Session.SessionHandler.listener.OnStopSuccess(text2, text3);
							return;
						}
						Session.SessionHandler.listener.OnFailure(num, text);
						return;
					}
					else
					{
						Session.SessionHandler.listener.OnFailure(code, message);
					}
				}
			}

			// Token: 0x040026AF RID: 9903
			private static Session.SessionListener listener;
		}

		// Token: 0x020005AB RID: 1451
		private abstract class BaseHandler
		{
			// Token: 0x060029B7 RID: 10679
			protected abstract void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x060029B8 RID: 10680
			protected abstract void StartHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x060029B9 RID: 10681
			protected abstract void StopHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
		}

		// Token: 0x020005AC RID: 1452
		public class SessionListener
		{
			// Token: 0x060029BB RID: 10683 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnSuccess(string pchAppID)
			{
			}

			// Token: 0x060029BC RID: 10684 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnStartSuccess(string pchAppID, string pchGuid)
			{
			}

			// Token: 0x060029BD RID: 10685 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnStopSuccess(string pchAppID, string pchGuid)
			{
			}

			// Token: 0x060029BE RID: 10686 RVA: 0x00003F60 File Offset: 0x00002160
			public virtual void OnFailure(int nCode, string pchMessage)
			{
			}
		}
	}
}
