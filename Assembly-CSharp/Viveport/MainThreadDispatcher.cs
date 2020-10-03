using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Viveport
{
	// Token: 0x02000203 RID: 515
	public class MainThreadDispatcher : MonoBehaviour
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x0005D1AD File Offset: 0x0005B3AD
		private void Awake()
		{
			if (MainThreadDispatcher.instance == null)
			{
				MainThreadDispatcher.instance = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0005D1D0 File Offset: 0x0005B3D0
		public void Update()
		{
			Queue<Action> obj = MainThreadDispatcher.actions;
			lock (obj)
			{
				while (MainThreadDispatcher.actions.Count > 0)
				{
					MainThreadDispatcher.actions.Dequeue()();
				}
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0005D228 File Offset: 0x0005B428
		public static MainThreadDispatcher Instance()
		{
			if (MainThreadDispatcher.instance == null)
			{
				throw new Exception("Could not find the MainThreadDispatcher GameObject. Please ensure you have added this script to an empty GameObject in your scene.");
			}
			return MainThreadDispatcher.instance;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0005D247 File Offset: 0x0005B447
		private void OnDestroy()
		{
			MainThreadDispatcher.instance = null;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0005D250 File Offset: 0x0005B450
		public void Enqueue(IEnumerator action)
		{
			Queue<Action> obj = MainThreadDispatcher.actions;
			lock (obj)
			{
				MainThreadDispatcher.actions.Enqueue(delegate
				{
					this.StartCoroutine(action);
				});
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0005D2B4 File Offset: 0x0005B4B4
		public void Enqueue(Action action)
		{
			this.Enqueue(this.ActionWrapper(action));
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0005D2C3 File Offset: 0x0005B4C3
		public void Enqueue<T1>(Action<T1> action, T1 param1)
		{
			this.Enqueue(this.ActionWrapper<T1>(action, param1));
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0005D2D3 File Offset: 0x0005B4D3
		public void Enqueue<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
		{
			this.Enqueue(this.ActionWrapper<T1, T2>(action, param1, param2));
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0005D2E4 File Offset: 0x0005B4E4
		public void Enqueue<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
		{
			this.Enqueue(this.ActionWrapper<T1, T2, T3>(action, param1, param2, param3));
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0005D2F7 File Offset: 0x0005B4F7
		public void Enqueue<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
		{
			this.Enqueue(this.ActionWrapper<T1, T2, T3, T4>(action, param1, param2, param3, param4));
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0005D30C File Offset: 0x0005B50C
		private IEnumerator ActionWrapper(Action action)
		{
			action();
			yield return null;
			yield break;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0005D31B File Offset: 0x0005B51B
		private IEnumerator ActionWrapper<T1>(Action<T1> action, T1 param1)
		{
			action(param1);
			yield return null;
			yield break;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0005D331 File Offset: 0x0005B531
		private IEnumerator ActionWrapper<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
		{
			action(param1, param2);
			yield return null;
			yield break;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0005D34E File Offset: 0x0005B54E
		private IEnumerator ActionWrapper<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
		{
			action(param1, param2, param3);
			yield return null;
			yield break;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0005D373 File Offset: 0x0005B573
		private IEnumerator ActionWrapper<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
		{
			action(param1, param2, param3, param4);
			yield return null;
			yield break;
		}

		// Token: 0x04000ECC RID: 3788
		private static readonly Queue<Action> actions = new Queue<Action>();

		// Token: 0x04000ECD RID: 3789
		private static MainThreadDispatcher instance = null;
	}
}
