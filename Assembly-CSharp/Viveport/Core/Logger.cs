using System;
using System.Reflection;

namespace Viveport.Core
{
	// Token: 0x0200022E RID: 558
	public class Logger
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0005ED36 File Offset: 0x0005CF36
		public static void Log(string message)
		{
			if (!Logger._hasDetected || Logger._usingUnityLog)
			{
				Logger.UnityLog(message);
				return;
			}
			Logger.ConsoleLog(message);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0005ED53 File Offset: 0x0005CF53
		private static void ConsoleLog(string message)
		{
			Console.WriteLine(message);
			Logger._hasDetected = true;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0005ED64 File Offset: 0x0005CF64
		private static void UnityLog(string message)
		{
			try
			{
				if (Logger._unityLogType == null)
				{
					Logger._unityLogType = Logger.GetType("UnityEngine.Debug");
				}
				Logger._unityLogType.GetMethod("Log", new Type[]
				{
					typeof(string)
				}).Invoke(null, new object[]
				{
					message
				});
				Logger._usingUnityLog = true;
			}
			catch (Exception)
			{
				Logger.ConsoleLog(message);
				Logger._usingUnityLog = false;
			}
			Logger._hasDetected = true;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		private static Type GetType(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type != null)
			{
				return type;
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				type = assemblies[i].GetType(typeName);
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x04000F3C RID: 3900
		private const string LoggerTypeNameUnity = "UnityEngine.Debug";

		// Token: 0x04000F3D RID: 3901
		private static bool _hasDetected;

		// Token: 0x04000F3E RID: 3902
		private static bool _usingUnityLog = true;

		// Token: 0x04000F3F RID: 3903
		private static Type _unityLogType;
	}
}
