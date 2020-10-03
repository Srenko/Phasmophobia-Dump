using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x020001E5 RID: 485
public static class SteamVR_Events
{
	// Token: 0x06000D66 RID: 3430 RVA: 0x0005408D File Offset: 0x0005228D
	public static SteamVR_Events.Action CalibratingAction(UnityAction<bool> action)
	{
		return new SteamVR_Events.Action<bool>(SteamVR_Events.Calibrating, action);
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0005409A File Offset: 0x0005229A
	public static SteamVR_Events.Action DeviceConnectedAction(UnityAction<int, bool> action)
	{
		return new SteamVR_Events.Action<int, bool>(SteamVR_Events.DeviceConnected, action);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x000540A7 File Offset: 0x000522A7
	public static SteamVR_Events.Action FadeAction(UnityAction<Color, float, bool> action)
	{
		return new SteamVR_Events.Action<Color, float, bool>(SteamVR_Events.Fade, action);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x000540B4 File Offset: 0x000522B4
	public static SteamVR_Events.Action FadeReadyAction(UnityAction action)
	{
		return new SteamVR_Events.ActionNoArgs(SteamVR_Events.FadeReady, action);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x000540C1 File Offset: 0x000522C1
	public static SteamVR_Events.Action HideRenderModelsAction(UnityAction<bool> action)
	{
		return new SteamVR_Events.Action<bool>(SteamVR_Events.HideRenderModels, action);
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x000540CE File Offset: 0x000522CE
	public static SteamVR_Events.Action InitializingAction(UnityAction<bool> action)
	{
		return new SteamVR_Events.Action<bool>(SteamVR_Events.Initializing, action);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x000540DB File Offset: 0x000522DB
	public static SteamVR_Events.Action InputFocusAction(UnityAction<bool> action)
	{
		return new SteamVR_Events.Action<bool>(SteamVR_Events.InputFocus, action);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x000540E8 File Offset: 0x000522E8
	public static SteamVR_Events.Action LoadingAction(UnityAction<bool> action)
	{
		return new SteamVR_Events.Action<bool>(SteamVR_Events.Loading, action);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x000540F5 File Offset: 0x000522F5
	public static SteamVR_Events.Action LoadingFadeInAction(UnityAction<float> action)
	{
		return new SteamVR_Events.Action<float>(SteamVR_Events.LoadingFadeIn, action);
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00054102 File Offset: 0x00052302
	public static SteamVR_Events.Action LoadingFadeOutAction(UnityAction<float> action)
	{
		return new SteamVR_Events.Action<float>(SteamVR_Events.LoadingFadeOut, action);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0005410F File Offset: 0x0005230F
	public static SteamVR_Events.Action NewPosesAction(UnityAction<TrackedDevicePose_t[]> action)
	{
		return new SteamVR_Events.Action<TrackedDevicePose_t[]>(SteamVR_Events.NewPoses, action);
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0005411C File Offset: 0x0005231C
	public static SteamVR_Events.Action NewPosesAppliedAction(UnityAction action)
	{
		return new SteamVR_Events.ActionNoArgs(SteamVR_Events.NewPosesApplied, action);
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00054129 File Offset: 0x00052329
	public static SteamVR_Events.Action OutOfRangeAction(UnityAction<bool> action)
	{
		return new SteamVR_Events.Action<bool>(SteamVR_Events.OutOfRange, action);
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00054136 File Offset: 0x00052336
	public static SteamVR_Events.Action RenderModelLoadedAction(UnityAction<SteamVR_RenderModel, bool> action)
	{
		return new SteamVR_Events.Action<SteamVR_RenderModel, bool>(SteamVR_Events.RenderModelLoaded, action);
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00054144 File Offset: 0x00052344
	public static SteamVR_Events.Event<VREvent_t> System(EVREventType eventType)
	{
		SteamVR_Events.Event<VREvent_t> @event;
		if (!SteamVR_Events.systemEvents.TryGetValue(eventType, out @event))
		{
			@event = new SteamVR_Events.Event<VREvent_t>();
			SteamVR_Events.systemEvents.Add(eventType, @event);
		}
		return @event;
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00054173 File Offset: 0x00052373
	public static SteamVR_Events.Action SystemAction(EVREventType eventType, UnityAction<VREvent_t> action)
	{
		return new SteamVR_Events.Action<VREvent_t>(SteamVR_Events.System(eventType), action);
	}

	// Token: 0x04000DC9 RID: 3529
	public static SteamVR_Events.Event<bool> Calibrating = new SteamVR_Events.Event<bool>();

	// Token: 0x04000DCA RID: 3530
	public static SteamVR_Events.Event<int, bool> DeviceConnected = new SteamVR_Events.Event<int, bool>();

	// Token: 0x04000DCB RID: 3531
	public static SteamVR_Events.Event<Color, float, bool> Fade = new SteamVR_Events.Event<Color, float, bool>();

	// Token: 0x04000DCC RID: 3532
	public static SteamVR_Events.Event FadeReady = new SteamVR_Events.Event();

	// Token: 0x04000DCD RID: 3533
	public static SteamVR_Events.Event<bool> HideRenderModels = new SteamVR_Events.Event<bool>();

	// Token: 0x04000DCE RID: 3534
	public static SteamVR_Events.Event<bool> Initializing = new SteamVR_Events.Event<bool>();

	// Token: 0x04000DCF RID: 3535
	public static SteamVR_Events.Event<bool> InputFocus = new SteamVR_Events.Event<bool>();

	// Token: 0x04000DD0 RID: 3536
	public static SteamVR_Events.Event<bool> Loading = new SteamVR_Events.Event<bool>();

	// Token: 0x04000DD1 RID: 3537
	public static SteamVR_Events.Event<float> LoadingFadeIn = new SteamVR_Events.Event<float>();

	// Token: 0x04000DD2 RID: 3538
	public static SteamVR_Events.Event<float> LoadingFadeOut = new SteamVR_Events.Event<float>();

	// Token: 0x04000DD3 RID: 3539
	public static SteamVR_Events.Event<TrackedDevicePose_t[]> NewPoses = new SteamVR_Events.Event<TrackedDevicePose_t[]>();

	// Token: 0x04000DD4 RID: 3540
	public static SteamVR_Events.Event NewPosesApplied = new SteamVR_Events.Event();

	// Token: 0x04000DD5 RID: 3541
	public static SteamVR_Events.Event<bool> OutOfRange = new SteamVR_Events.Event<bool>();

	// Token: 0x04000DD6 RID: 3542
	public static SteamVR_Events.Event<SteamVR_RenderModel, bool> RenderModelLoaded = new SteamVR_Events.Event<SteamVR_RenderModel, bool>();

	// Token: 0x04000DD7 RID: 3543
	private static Dictionary<EVREventType, SteamVR_Events.Event<VREvent_t>> systemEvents = new Dictionary<EVREventType, SteamVR_Events.Event<VREvent_t>>();

	// Token: 0x0200056B RID: 1387
	public abstract class Action
	{
		// Token: 0x06002863 RID: 10339
		public abstract void Enable(bool enabled);

		// Token: 0x170002E6 RID: 742
		// (set) Token: 0x06002864 RID: 10340 RVA: 0x000C37EB File Offset: 0x000C19EB
		public bool enabled
		{
			set
			{
				this.Enable(value);
			}
		}
	}

	// Token: 0x0200056C RID: 1388
	[Serializable]
	public class ActionNoArgs : SteamVR_Events.Action
	{
		// Token: 0x06002866 RID: 10342 RVA: 0x000C37F4 File Offset: 0x000C19F4
		public ActionNoArgs(SteamVR_Events.Event _event, UnityAction action)
		{
			this._event = _event;
			this.action = action;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000C380A File Offset: 0x000C1A0A
		public override void Enable(bool enabled)
		{
			if (enabled)
			{
				this._event.Listen(this.action);
				return;
			}
			this._event.Remove(this.action);
		}

		// Token: 0x040025D1 RID: 9681
		private SteamVR_Events.Event _event;

		// Token: 0x040025D2 RID: 9682
		private UnityAction action;
	}

	// Token: 0x0200056D RID: 1389
	[Serializable]
	public class Action<T> : SteamVR_Events.Action
	{
		// Token: 0x06002868 RID: 10344 RVA: 0x000C3832 File Offset: 0x000C1A32
		public Action(SteamVR_Events.Event<T> _event, UnityAction<T> action)
		{
			this._event = _event;
			this.action = action;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000C3848 File Offset: 0x000C1A48
		public override void Enable(bool enabled)
		{
			if (enabled)
			{
				this._event.Listen(this.action);
				return;
			}
			this._event.Remove(this.action);
		}

		// Token: 0x040025D3 RID: 9683
		private SteamVR_Events.Event<T> _event;

		// Token: 0x040025D4 RID: 9684
		private UnityAction<T> action;
	}

	// Token: 0x0200056E RID: 1390
	[Serializable]
	public class Action<T0, T1> : SteamVR_Events.Action
	{
		// Token: 0x0600286A RID: 10346 RVA: 0x000C3870 File Offset: 0x000C1A70
		public Action(SteamVR_Events.Event<T0, T1> _event, UnityAction<T0, T1> action)
		{
			this._event = _event;
			this.action = action;
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000C3886 File Offset: 0x000C1A86
		public override void Enable(bool enabled)
		{
			if (enabled)
			{
				this._event.Listen(this.action);
				return;
			}
			this._event.Remove(this.action);
		}

		// Token: 0x040025D5 RID: 9685
		private SteamVR_Events.Event<T0, T1> _event;

		// Token: 0x040025D6 RID: 9686
		private UnityAction<T0, T1> action;
	}

	// Token: 0x0200056F RID: 1391
	[Serializable]
	public class Action<T0, T1, T2> : SteamVR_Events.Action
	{
		// Token: 0x0600286C RID: 10348 RVA: 0x000C38AE File Offset: 0x000C1AAE
		public Action(SteamVR_Events.Event<T0, T1, T2> _event, UnityAction<T0, T1, T2> action)
		{
			this._event = _event;
			this.action = action;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000C38C4 File Offset: 0x000C1AC4
		public override void Enable(bool enabled)
		{
			if (enabled)
			{
				this._event.Listen(this.action);
				return;
			}
			this._event.Remove(this.action);
		}

		// Token: 0x040025D7 RID: 9687
		private SteamVR_Events.Event<T0, T1, T2> _event;

		// Token: 0x040025D8 RID: 9688
		private UnityAction<T0, T1, T2> action;
	}

	// Token: 0x02000570 RID: 1392
	public class Event : UnityEvent
	{
		// Token: 0x0600286E RID: 10350 RVA: 0x000C38EC File Offset: 0x000C1AEC
		public void Listen(UnityAction action)
		{
			base.AddListener(action);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000C38F5 File Offset: 0x000C1AF5
		public void Remove(UnityAction action)
		{
			base.RemoveListener(action);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000C38FE File Offset: 0x000C1AFE
		public void Send()
		{
			base.Invoke();
		}
	}

	// Token: 0x02000571 RID: 1393
	public class Event<T> : UnityEvent<T>
	{
		// Token: 0x06002872 RID: 10354 RVA: 0x000C390E File Offset: 0x000C1B0E
		public void Listen(UnityAction<T> action)
		{
			base.AddListener(action);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000C3917 File Offset: 0x000C1B17
		public void Remove(UnityAction<T> action)
		{
			base.RemoveListener(action);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000C3920 File Offset: 0x000C1B20
		public void Send(T arg0)
		{
			base.Invoke(arg0);
		}
	}

	// Token: 0x02000572 RID: 1394
	public class Event<T0, T1> : UnityEvent<T0, T1>
	{
		// Token: 0x06002876 RID: 10358 RVA: 0x000C3931 File Offset: 0x000C1B31
		public void Listen(UnityAction<T0, T1> action)
		{
			base.AddListener(action);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000C393A File Offset: 0x000C1B3A
		public void Remove(UnityAction<T0, T1> action)
		{
			base.RemoveListener(action);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000C3943 File Offset: 0x000C1B43
		public void Send(T0 arg0, T1 arg1)
		{
			base.Invoke(arg0, arg1);
		}
	}

	// Token: 0x02000573 RID: 1395
	public class Event<T0, T1, T2> : UnityEvent<T0, T1, T2>
	{
		// Token: 0x0600287A RID: 10362 RVA: 0x000C3955 File Offset: 0x000C1B55
		public void Listen(UnityAction<T0, T1, T2> action)
		{
			base.AddListener(action);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000C395E File Offset: 0x000C1B5E
		public void Remove(UnityAction<T0, T1, T2> action)
		{
			base.RemoveListener(action);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000C3967 File Offset: 0x000C1B67
		public void Send(T0 arg0, T1 arg1, T2 arg2)
		{
			base.Invoke(arg0, arg1, arg2);
		}
	}
}
