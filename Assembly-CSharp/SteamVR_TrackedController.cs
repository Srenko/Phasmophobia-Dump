using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR;

// Token: 0x020001DD RID: 477
public class SteamVR_TrackedController : MonoBehaviour
{
	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000CE2 RID: 3298 RVA: 0x00051D00 File Offset: 0x0004FF00
	// (remove) Token: 0x06000CE3 RID: 3299 RVA: 0x00051D38 File Offset: 0x0004FF38
	public event ClickedEventHandler MenuButtonClicked;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000CE4 RID: 3300 RVA: 0x00051D70 File Offset: 0x0004FF70
	// (remove) Token: 0x06000CE5 RID: 3301 RVA: 0x00051DA8 File Offset: 0x0004FFA8
	public event ClickedEventHandler MenuButtonUnclicked;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000CE6 RID: 3302 RVA: 0x00051DE0 File Offset: 0x0004FFE0
	// (remove) Token: 0x06000CE7 RID: 3303 RVA: 0x00051E18 File Offset: 0x00050018
	public event ClickedEventHandler TriggerClicked;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000CE8 RID: 3304 RVA: 0x00051E50 File Offset: 0x00050050
	// (remove) Token: 0x06000CE9 RID: 3305 RVA: 0x00051E88 File Offset: 0x00050088
	public event ClickedEventHandler TriggerUnclicked;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06000CEA RID: 3306 RVA: 0x00051EC0 File Offset: 0x000500C0
	// (remove) Token: 0x06000CEB RID: 3307 RVA: 0x00051EF8 File Offset: 0x000500F8
	public event ClickedEventHandler SteamClicked;

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000CEC RID: 3308 RVA: 0x00051F30 File Offset: 0x00050130
	// (remove) Token: 0x06000CED RID: 3309 RVA: 0x00051F68 File Offset: 0x00050168
	public event ClickedEventHandler PadClicked;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06000CEE RID: 3310 RVA: 0x00051FA0 File Offset: 0x000501A0
	// (remove) Token: 0x06000CEF RID: 3311 RVA: 0x00051FD8 File Offset: 0x000501D8
	public event ClickedEventHandler PadUnclicked;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000CF0 RID: 3312 RVA: 0x00052010 File Offset: 0x00050210
	// (remove) Token: 0x06000CF1 RID: 3313 RVA: 0x00052048 File Offset: 0x00050248
	public event ClickedEventHandler PadTouched;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000CF2 RID: 3314 RVA: 0x00052080 File Offset: 0x00050280
	// (remove) Token: 0x06000CF3 RID: 3315 RVA: 0x000520B8 File Offset: 0x000502B8
	public event ClickedEventHandler PadUntouched;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000CF4 RID: 3316 RVA: 0x000520F0 File Offset: 0x000502F0
	// (remove) Token: 0x06000CF5 RID: 3317 RVA: 0x00052128 File Offset: 0x00050328
	public event ClickedEventHandler Gripped;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000CF6 RID: 3318 RVA: 0x00052160 File Offset: 0x00050360
	// (remove) Token: 0x06000CF7 RID: 3319 RVA: 0x00052198 File Offset: 0x00050398
	public event ClickedEventHandler Ungripped;

	// Token: 0x06000CF8 RID: 3320 RVA: 0x000521D0 File Offset: 0x000503D0
	protected virtual void Start()
	{
		if (base.GetComponent<SteamVR_TrackedObject>() == null)
		{
			base.gameObject.AddComponent<SteamVR_TrackedObject>();
		}
		if (this.controllerIndex != 0U)
		{
			base.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)this.controllerIndex;
			if (base.GetComponent<SteamVR_RenderModel>() != null)
			{
				base.GetComponent<SteamVR_RenderModel>().index = (SteamVR_TrackedObject.EIndex)this.controllerIndex;
				return;
			}
		}
		else
		{
			this.controllerIndex = (uint)base.GetComponent<SteamVR_TrackedObject>().index;
		}
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x00052241 File Offset: 0x00050441
	public void SetDeviceIndex(int index)
	{
		this.controllerIndex = (uint)index;
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0005224A File Offset: 0x0005044A
	public virtual void OnTriggerClicked(ClickedEventArgs e)
	{
		if (this.TriggerClicked != null)
		{
			this.TriggerClicked(this, e);
		}
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x00052261 File Offset: 0x00050461
	public virtual void OnTriggerUnclicked(ClickedEventArgs e)
	{
		if (this.TriggerUnclicked != null)
		{
			this.TriggerUnclicked(this, e);
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x00052278 File Offset: 0x00050478
	public virtual void OnMenuClicked(ClickedEventArgs e)
	{
		if (this.MenuButtonClicked != null)
		{
			this.MenuButtonClicked(this, e);
		}
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0005228F File Offset: 0x0005048F
	public virtual void OnMenuUnclicked(ClickedEventArgs e)
	{
		if (this.MenuButtonUnclicked != null)
		{
			this.MenuButtonUnclicked(this, e);
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x000522A6 File Offset: 0x000504A6
	public virtual void OnSteamClicked(ClickedEventArgs e)
	{
		if (this.SteamClicked != null)
		{
			this.SteamClicked(this, e);
		}
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x000522BD File Offset: 0x000504BD
	public virtual void OnPadClicked(ClickedEventArgs e)
	{
		if (this.PadClicked != null)
		{
			this.PadClicked(this, e);
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x000522D4 File Offset: 0x000504D4
	public virtual void OnPadUnclicked(ClickedEventArgs e)
	{
		if (this.PadUnclicked != null)
		{
			this.PadUnclicked(this, e);
		}
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x000522EB File Offset: 0x000504EB
	public virtual void OnPadTouched(ClickedEventArgs e)
	{
		if (this.PadTouched != null)
		{
			this.PadTouched(this, e);
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x00052302 File Offset: 0x00050502
	public virtual void OnPadUntouched(ClickedEventArgs e)
	{
		if (this.PadUntouched != null)
		{
			this.PadUntouched(this, e);
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x00052319 File Offset: 0x00050519
	public virtual void OnGripped(ClickedEventArgs e)
	{
		if (this.Gripped != null)
		{
			this.Gripped(this, e);
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x00052330 File Offset: 0x00050530
	public virtual void OnUngripped(ClickedEventArgs e)
	{
		if (this.Ungripped != null)
		{
			this.Ungripped(this, e);
		}
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x00052348 File Offset: 0x00050548
	protected virtual void Update()
	{
		CVRSystem system = OpenVR.System;
		if (system != null && system.GetControllerState(this.controllerIndex, ref this.controllerState, (uint)Marshal.SizeOf(typeof(VRControllerState_t))))
		{
			ulong num = this.controllerState.ulButtonPressed & 8589934592UL;
			if (num > 0UL && !this.triggerPressed)
			{
				this.triggerPressed = true;
				ClickedEventArgs e;
				e.controllerIndex = this.controllerIndex;
				e.flags = (uint)this.controllerState.ulButtonPressed;
				e.padX = this.controllerState.rAxis0.x;
				e.padY = this.controllerState.rAxis0.y;
				this.OnTriggerClicked(e);
			}
			else if (num == 0UL && this.triggerPressed)
			{
				this.triggerPressed = false;
				ClickedEventArgs e2;
				e2.controllerIndex = this.controllerIndex;
				e2.flags = (uint)this.controllerState.ulButtonPressed;
				e2.padX = this.controllerState.rAxis0.x;
				e2.padY = this.controllerState.rAxis0.y;
				this.OnTriggerUnclicked(e2);
			}
			ulong num2 = this.controllerState.ulButtonPressed & 4UL;
			if (num2 > 0UL && !this.gripped)
			{
				this.gripped = true;
				ClickedEventArgs e3;
				e3.controllerIndex = this.controllerIndex;
				e3.flags = (uint)this.controllerState.ulButtonPressed;
				e3.padX = this.controllerState.rAxis0.x;
				e3.padY = this.controllerState.rAxis0.y;
				this.OnGripped(e3);
			}
			else if (num2 == 0UL && this.gripped)
			{
				this.gripped = false;
				ClickedEventArgs e4;
				e4.controllerIndex = this.controllerIndex;
				e4.flags = (uint)this.controllerState.ulButtonPressed;
				e4.padX = this.controllerState.rAxis0.x;
				e4.padY = this.controllerState.rAxis0.y;
				this.OnUngripped(e4);
			}
			ulong num3 = this.controllerState.ulButtonPressed & 4294967296UL;
			if (num3 > 0UL && !this.padPressed)
			{
				this.padPressed = true;
				ClickedEventArgs e5;
				e5.controllerIndex = this.controllerIndex;
				e5.flags = (uint)this.controllerState.ulButtonPressed;
				e5.padX = this.controllerState.rAxis0.x;
				e5.padY = this.controllerState.rAxis0.y;
				this.OnPadClicked(e5);
			}
			else if (num3 == 0UL && this.padPressed)
			{
				this.padPressed = false;
				ClickedEventArgs e6;
				e6.controllerIndex = this.controllerIndex;
				e6.flags = (uint)this.controllerState.ulButtonPressed;
				e6.padX = this.controllerState.rAxis0.x;
				e6.padY = this.controllerState.rAxis0.y;
				this.OnPadUnclicked(e6);
			}
			ulong num4 = this.controllerState.ulButtonPressed & 2UL;
			if (num4 > 0UL && !this.menuPressed)
			{
				this.menuPressed = true;
				ClickedEventArgs e7;
				e7.controllerIndex = this.controllerIndex;
				e7.flags = (uint)this.controllerState.ulButtonPressed;
				e7.padX = this.controllerState.rAxis0.x;
				e7.padY = this.controllerState.rAxis0.y;
				this.OnMenuClicked(e7);
			}
			else if (num4 == 0UL && this.menuPressed)
			{
				this.menuPressed = false;
				ClickedEventArgs e8;
				e8.controllerIndex = this.controllerIndex;
				e8.flags = (uint)this.controllerState.ulButtonPressed;
				e8.padX = this.controllerState.rAxis0.x;
				e8.padY = this.controllerState.rAxis0.y;
				this.OnMenuUnclicked(e8);
			}
			num3 = (this.controllerState.ulButtonTouched & 4294967296UL);
			if (num3 > 0UL && !this.padTouched)
			{
				this.padTouched = true;
				ClickedEventArgs e9;
				e9.controllerIndex = this.controllerIndex;
				e9.flags = (uint)this.controllerState.ulButtonPressed;
				e9.padX = this.controllerState.rAxis0.x;
				e9.padY = this.controllerState.rAxis0.y;
				this.OnPadTouched(e9);
				return;
			}
			if (num3 == 0UL && this.padTouched)
			{
				this.padTouched = false;
				ClickedEventArgs e10;
				e10.controllerIndex = this.controllerIndex;
				e10.flags = (uint)this.controllerState.ulButtonPressed;
				e10.padX = this.controllerState.rAxis0.x;
				e10.padY = this.controllerState.rAxis0.y;
				this.OnPadUntouched(e10);
			}
		}
	}

	// Token: 0x04000D8A RID: 3466
	public uint controllerIndex;

	// Token: 0x04000D8B RID: 3467
	public VRControllerState_t controllerState;

	// Token: 0x04000D8C RID: 3468
	public bool triggerPressed;

	// Token: 0x04000D8D RID: 3469
	public bool steamPressed;

	// Token: 0x04000D8E RID: 3470
	public bool menuPressed;

	// Token: 0x04000D8F RID: 3471
	public bool padPressed;

	// Token: 0x04000D90 RID: 3472
	public bool padTouched;

	// Token: 0x04000D91 RID: 3473
	public bool gripped;
}
