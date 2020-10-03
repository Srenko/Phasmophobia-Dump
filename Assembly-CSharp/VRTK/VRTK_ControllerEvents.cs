using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200029B RID: 667
	[AddComponentMenu("VRTK/Scripts/Interactions/VRTK_ControllerEvents")]
	public class VRTK_ControllerEvents : MonoBehaviour
	{
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600147C RID: 5244 RVA: 0x00071E84 File Offset: 0x00070084
		// (remove) Token: 0x0600147D RID: 5245 RVA: 0x00071EBC File Offset: 0x000700BC
		public event ControllerInteractionEventHandler TriggerPressed;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600147E RID: 5246 RVA: 0x00071EF4 File Offset: 0x000700F4
		// (remove) Token: 0x0600147F RID: 5247 RVA: 0x00071F2C File Offset: 0x0007012C
		public event ControllerInteractionEventHandler TriggerReleased;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001480 RID: 5248 RVA: 0x00071F64 File Offset: 0x00070164
		// (remove) Token: 0x06001481 RID: 5249 RVA: 0x00071F9C File Offset: 0x0007019C
		public event ControllerInteractionEventHandler TriggerTouchStart;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001482 RID: 5250 RVA: 0x00071FD4 File Offset: 0x000701D4
		// (remove) Token: 0x06001483 RID: 5251 RVA: 0x0007200C File Offset: 0x0007020C
		public event ControllerInteractionEventHandler TriggerTouchEnd;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06001484 RID: 5252 RVA: 0x00072044 File Offset: 0x00070244
		// (remove) Token: 0x06001485 RID: 5253 RVA: 0x0007207C File Offset: 0x0007027C
		public event ControllerInteractionEventHandler TriggerHairlineStart;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001486 RID: 5254 RVA: 0x000720B4 File Offset: 0x000702B4
		// (remove) Token: 0x06001487 RID: 5255 RVA: 0x000720EC File Offset: 0x000702EC
		public event ControllerInteractionEventHandler TriggerHairlineEnd;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06001488 RID: 5256 RVA: 0x00072124 File Offset: 0x00070324
		// (remove) Token: 0x06001489 RID: 5257 RVA: 0x0007215C File Offset: 0x0007035C
		public event ControllerInteractionEventHandler TriggerClicked;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600148A RID: 5258 RVA: 0x00072194 File Offset: 0x00070394
		// (remove) Token: 0x0600148B RID: 5259 RVA: 0x000721CC File Offset: 0x000703CC
		public event ControllerInteractionEventHandler TriggerUnclicked;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600148C RID: 5260 RVA: 0x00072204 File Offset: 0x00070404
		// (remove) Token: 0x0600148D RID: 5261 RVA: 0x0007223C File Offset: 0x0007043C
		public event ControllerInteractionEventHandler TriggerAxisChanged;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600148E RID: 5262 RVA: 0x00072274 File Offset: 0x00070474
		// (remove) Token: 0x0600148F RID: 5263 RVA: 0x000722AC File Offset: 0x000704AC
		public event ControllerInteractionEventHandler GripPressed;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001490 RID: 5264 RVA: 0x000722E4 File Offset: 0x000704E4
		// (remove) Token: 0x06001491 RID: 5265 RVA: 0x0007231C File Offset: 0x0007051C
		public event ControllerInteractionEventHandler GripReleased;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001492 RID: 5266 RVA: 0x00072354 File Offset: 0x00070554
		// (remove) Token: 0x06001493 RID: 5267 RVA: 0x0007238C File Offset: 0x0007058C
		public event ControllerInteractionEventHandler GripTouchStart;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001494 RID: 5268 RVA: 0x000723C4 File Offset: 0x000705C4
		// (remove) Token: 0x06001495 RID: 5269 RVA: 0x000723FC File Offset: 0x000705FC
		public event ControllerInteractionEventHandler GripTouchEnd;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001496 RID: 5270 RVA: 0x00072434 File Offset: 0x00070634
		// (remove) Token: 0x06001497 RID: 5271 RVA: 0x0007246C File Offset: 0x0007066C
		public event ControllerInteractionEventHandler GripHairlineStart;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001498 RID: 5272 RVA: 0x000724A4 File Offset: 0x000706A4
		// (remove) Token: 0x06001499 RID: 5273 RVA: 0x000724DC File Offset: 0x000706DC
		public event ControllerInteractionEventHandler GripHairlineEnd;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600149A RID: 5274 RVA: 0x00072514 File Offset: 0x00070714
		// (remove) Token: 0x0600149B RID: 5275 RVA: 0x0007254C File Offset: 0x0007074C
		public event ControllerInteractionEventHandler GripClicked;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600149C RID: 5276 RVA: 0x00072584 File Offset: 0x00070784
		// (remove) Token: 0x0600149D RID: 5277 RVA: 0x000725BC File Offset: 0x000707BC
		public event ControllerInteractionEventHandler GripUnclicked;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600149E RID: 5278 RVA: 0x000725F4 File Offset: 0x000707F4
		// (remove) Token: 0x0600149F RID: 5279 RVA: 0x0007262C File Offset: 0x0007082C
		public event ControllerInteractionEventHandler GripAxisChanged;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060014A0 RID: 5280 RVA: 0x00072664 File Offset: 0x00070864
		// (remove) Token: 0x060014A1 RID: 5281 RVA: 0x0007269C File Offset: 0x0007089C
		public event ControllerInteractionEventHandler TouchpadPressed;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060014A2 RID: 5282 RVA: 0x000726D4 File Offset: 0x000708D4
		// (remove) Token: 0x060014A3 RID: 5283 RVA: 0x0007270C File Offset: 0x0007090C
		public event ControllerInteractionEventHandler TouchpadReleased;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060014A4 RID: 5284 RVA: 0x00072744 File Offset: 0x00070944
		// (remove) Token: 0x060014A5 RID: 5285 RVA: 0x0007277C File Offset: 0x0007097C
		public event ControllerInteractionEventHandler TouchpadTouchStart;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060014A6 RID: 5286 RVA: 0x000727B4 File Offset: 0x000709B4
		// (remove) Token: 0x060014A7 RID: 5287 RVA: 0x000727EC File Offset: 0x000709EC
		public event ControllerInteractionEventHandler TouchpadTouchEnd;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060014A8 RID: 5288 RVA: 0x00072824 File Offset: 0x00070A24
		// (remove) Token: 0x060014A9 RID: 5289 RVA: 0x0007285C File Offset: 0x00070A5C
		public event ControllerInteractionEventHandler TouchpadAxisChanged;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060014AA RID: 5290 RVA: 0x00072894 File Offset: 0x00070A94
		// (remove) Token: 0x060014AB RID: 5291 RVA: 0x000728CC File Offset: 0x00070ACC
		public event ControllerInteractionEventHandler ButtonOneTouchStart;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060014AC RID: 5292 RVA: 0x00072904 File Offset: 0x00070B04
		// (remove) Token: 0x060014AD RID: 5293 RVA: 0x0007293C File Offset: 0x00070B3C
		public event ControllerInteractionEventHandler ButtonOneTouchEnd;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060014AE RID: 5294 RVA: 0x00072974 File Offset: 0x00070B74
		// (remove) Token: 0x060014AF RID: 5295 RVA: 0x000729AC File Offset: 0x00070BAC
		public event ControllerInteractionEventHandler ButtonOnePressed;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060014B0 RID: 5296 RVA: 0x000729E4 File Offset: 0x00070BE4
		// (remove) Token: 0x060014B1 RID: 5297 RVA: 0x00072A1C File Offset: 0x00070C1C
		public event ControllerInteractionEventHandler ButtonOneReleased;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060014B2 RID: 5298 RVA: 0x00072A54 File Offset: 0x00070C54
		// (remove) Token: 0x060014B3 RID: 5299 RVA: 0x00072A8C File Offset: 0x00070C8C
		public event ControllerInteractionEventHandler ButtonTwoTouchStart;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060014B4 RID: 5300 RVA: 0x00072AC4 File Offset: 0x00070CC4
		// (remove) Token: 0x060014B5 RID: 5301 RVA: 0x00072AFC File Offset: 0x00070CFC
		public event ControllerInteractionEventHandler ButtonTwoTouchEnd;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060014B6 RID: 5302 RVA: 0x00072B34 File Offset: 0x00070D34
		// (remove) Token: 0x060014B7 RID: 5303 RVA: 0x00072B6C File Offset: 0x00070D6C
		public event ControllerInteractionEventHandler ButtonTwoPressed;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060014B8 RID: 5304 RVA: 0x00072BA4 File Offset: 0x00070DA4
		// (remove) Token: 0x060014B9 RID: 5305 RVA: 0x00072BDC File Offset: 0x00070DDC
		public event ControllerInteractionEventHandler ButtonTwoReleased;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060014BA RID: 5306 RVA: 0x00072C14 File Offset: 0x00070E14
		// (remove) Token: 0x060014BB RID: 5307 RVA: 0x00072C4C File Offset: 0x00070E4C
		public event ControllerInteractionEventHandler StartMenuPressed;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060014BC RID: 5308 RVA: 0x00072C84 File Offset: 0x00070E84
		// (remove) Token: 0x060014BD RID: 5309 RVA: 0x00072CBC File Offset: 0x00070EBC
		public event ControllerInteractionEventHandler StartMenuReleased;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060014BE RID: 5310 RVA: 0x00072CF4 File Offset: 0x00070EF4
		// (remove) Token: 0x060014BF RID: 5311 RVA: 0x00072D2C File Offset: 0x00070F2C
		[Obsolete("`VRTK_ControllerEvents.AliasPointerOn` has been replaced with `VRTK_Pointer.ActivationButtonPressed`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasPointerOn;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060014C0 RID: 5312 RVA: 0x00072D64 File Offset: 0x00070F64
		// (remove) Token: 0x060014C1 RID: 5313 RVA: 0x00072D9C File Offset: 0x00070F9C
		[Obsolete("`VRTK_ControllerEvents.AliasPointerOff` has been replaced with `VRTK_Pointer.ActivationButtonReleased`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasPointerOff;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060014C2 RID: 5314 RVA: 0x00072DD4 File Offset: 0x00070FD4
		// (remove) Token: 0x060014C3 RID: 5315 RVA: 0x00072E0C File Offset: 0x0007100C
		[Obsolete("`VRTK_ControllerEvents.AliasPointerSet` has been replaced with `VRTK_Pointer.SelectionButtonReleased`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasPointerSet;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060014C4 RID: 5316 RVA: 0x00072E44 File Offset: 0x00071044
		// (remove) Token: 0x060014C5 RID: 5317 RVA: 0x00072E7C File Offset: 0x0007107C
		[Obsolete("`VRTK_ControllerEvents.AliasGrabOn` has been replaced with `VRTK_InteractGrab.GrabButtonPressed`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasGrabOn;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060014C6 RID: 5318 RVA: 0x00072EB4 File Offset: 0x000710B4
		// (remove) Token: 0x060014C7 RID: 5319 RVA: 0x00072EEC File Offset: 0x000710EC
		[Obsolete("`VRTK_ControllerEvents.AliasGrabOff` has been replaced with `VRTK_InteractGrab.GrabButtonReleased`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasGrabOff;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060014C8 RID: 5320 RVA: 0x00072F24 File Offset: 0x00071124
		// (remove) Token: 0x060014C9 RID: 5321 RVA: 0x00072F5C File Offset: 0x0007115C
		[Obsolete("`VRTK_ControllerEvents.AliasUseOn` has been replaced with `VRTK_InteractUse.UseButtonPressed`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasUseOn;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060014CA RID: 5322 RVA: 0x00072F94 File Offset: 0x00071194
		// (remove) Token: 0x060014CB RID: 5323 RVA: 0x00072FCC File Offset: 0x000711CC
		[Obsolete("`VRTK_ControllerEvents.AliasUseOff` has been replaced with `VRTK_InteractUse.UseButtonReleased`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasUseOff;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060014CC RID: 5324 RVA: 0x00073004 File Offset: 0x00071204
		// (remove) Token: 0x060014CD RID: 5325 RVA: 0x0007303C File Offset: 0x0007123C
		[Obsolete("`VRTK_ControllerEvents.AliasMenuOn` is no longer used, use `VRTK_ControllerEvents.ButtonTwoPressed` instead. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasMenuOn;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060014CE RID: 5326 RVA: 0x00073074 File Offset: 0x00071274
		// (remove) Token: 0x060014CF RID: 5327 RVA: 0x000730AC File Offset: 0x000712AC
		[Obsolete("`VRTK_ControllerEvents.AliasMenuOff` is no longer used, use `VRTK_ControllerEvents.ButtonTwoReleased` instead. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasMenuOff;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060014D0 RID: 5328 RVA: 0x000730E4 File Offset: 0x000712E4
		// (remove) Token: 0x060014D1 RID: 5329 RVA: 0x0007311C File Offset: 0x0007131C
		[Obsolete("`VRTK_ControllerEvents.AliasUIClickOn` has been replaced with `VRTK_UIPointer.SelectionButtonPressed`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasUIClickOn;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060014D2 RID: 5330 RVA: 0x00073154 File Offset: 0x00071354
		// (remove) Token: 0x060014D3 RID: 5331 RVA: 0x0007318C File Offset: 0x0007138C
		[Obsolete("`VRTK_ControllerEvents.AliasUIClickOff` has been replaced with `VRTK_UIPointer.SelectionButtonReleased`. This parameter will be removed in a future version of VRTK.")]
		public event ControllerInteractionEventHandler AliasUIClickOff;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060014D4 RID: 5332 RVA: 0x000731C4 File Offset: 0x000713C4
		// (remove) Token: 0x060014D5 RID: 5333 RVA: 0x000731FC File Offset: 0x000713FC
		public event ControllerInteractionEventHandler ControllerEnabled;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060014D6 RID: 5334 RVA: 0x00073234 File Offset: 0x00071434
		// (remove) Token: 0x060014D7 RID: 5335 RVA: 0x0007326C File Offset: 0x0007146C
		public event ControllerInteractionEventHandler ControllerDisabled;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060014D8 RID: 5336 RVA: 0x000732A4 File Offset: 0x000714A4
		// (remove) Token: 0x060014D9 RID: 5337 RVA: 0x000732DC File Offset: 0x000714DC
		public event ControllerInteractionEventHandler ControllerIndexChanged;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060014DA RID: 5338 RVA: 0x00073314 File Offset: 0x00071514
		// (remove) Token: 0x060014DB RID: 5339 RVA: 0x0007334C File Offset: 0x0007154C
		public event ControllerInteractionEventHandler ControllerVisible;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060014DC RID: 5340 RVA: 0x00073384 File Offset: 0x00071584
		// (remove) Token: 0x060014DD RID: 5341 RVA: 0x000733BC File Offset: 0x000715BC
		public event ControllerInteractionEventHandler ControllerHidden;

		// Token: 0x060014DE RID: 5342 RVA: 0x000733F1 File Offset: 0x000715F1
		public virtual void OnTriggerPressed(ControllerInteractionEventArgs e)
		{
			if (this.TriggerPressed != null)
			{
				this.TriggerPressed(this, e);
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00073408 File Offset: 0x00071608
		public virtual void OnTriggerReleased(ControllerInteractionEventArgs e)
		{
			if (this.TriggerReleased != null)
			{
				this.TriggerReleased(this, e);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0007341F File Offset: 0x0007161F
		public virtual void OnTriggerTouchStart(ControllerInteractionEventArgs e)
		{
			if (this.TriggerTouchStart != null)
			{
				this.TriggerTouchStart(this, e);
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00073436 File Offset: 0x00071636
		public virtual void OnTriggerTouchEnd(ControllerInteractionEventArgs e)
		{
			if (this.TriggerTouchEnd != null)
			{
				this.TriggerTouchEnd(this, e);
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0007344D File Offset: 0x0007164D
		public virtual void OnTriggerHairlineStart(ControllerInteractionEventArgs e)
		{
			if (this.TriggerHairlineStart != null)
			{
				this.TriggerHairlineStart(this, e);
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00073464 File Offset: 0x00071664
		public virtual void OnTriggerHairlineEnd(ControllerInteractionEventArgs e)
		{
			if (this.TriggerHairlineEnd != null)
			{
				this.TriggerHairlineEnd(this, e);
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0007347B File Offset: 0x0007167B
		public virtual void OnTriggerClicked(ControllerInteractionEventArgs e)
		{
			if (this.TriggerClicked != null)
			{
				this.TriggerClicked(this, e);
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00073492 File Offset: 0x00071692
		public virtual void OnTriggerUnclicked(ControllerInteractionEventArgs e)
		{
			if (this.TriggerUnclicked != null)
			{
				this.TriggerUnclicked(this, e);
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x000734A9 File Offset: 0x000716A9
		public virtual void OnTriggerAxisChanged(ControllerInteractionEventArgs e)
		{
			if (this.TriggerAxisChanged != null)
			{
				this.TriggerAxisChanged(this, e);
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000734C0 File Offset: 0x000716C0
		public virtual void OnGripPressed(ControllerInteractionEventArgs e)
		{
			if (this.GripPressed != null)
			{
				this.GripPressed(this, e);
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000734D7 File Offset: 0x000716D7
		public virtual void OnGripReleased(ControllerInteractionEventArgs e)
		{
			if (this.GripReleased != null)
			{
				this.GripReleased(this, e);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000734EE File Offset: 0x000716EE
		public virtual void OnGripTouchStart(ControllerInteractionEventArgs e)
		{
			if (this.GripTouchStart != null)
			{
				this.GripTouchStart(this, e);
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00073505 File Offset: 0x00071705
		public virtual void OnGripTouchEnd(ControllerInteractionEventArgs e)
		{
			if (this.GripTouchEnd != null)
			{
				this.GripTouchEnd(this, e);
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0007351C File Offset: 0x0007171C
		public virtual void OnGripHairlineStart(ControllerInteractionEventArgs e)
		{
			if (this.GripHairlineStart != null)
			{
				this.GripHairlineStart(this, e);
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00073533 File Offset: 0x00071733
		public virtual void OnGripHairlineEnd(ControllerInteractionEventArgs e)
		{
			if (this.GripHairlineEnd != null)
			{
				this.GripHairlineEnd(this, e);
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0007354A File Offset: 0x0007174A
		public virtual void OnGripClicked(ControllerInteractionEventArgs e)
		{
			if (this.GripClicked != null)
			{
				this.GripClicked(this, e);
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00073561 File Offset: 0x00071761
		public virtual void OnGripUnclicked(ControllerInteractionEventArgs e)
		{
			if (this.GripUnclicked != null)
			{
				this.GripUnclicked(this, e);
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00073578 File Offset: 0x00071778
		public virtual void OnGripAxisChanged(ControllerInteractionEventArgs e)
		{
			if (this.GripAxisChanged != null)
			{
				this.GripAxisChanged(this, e);
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0007358F File Offset: 0x0007178F
		public virtual void OnTouchpadPressed(ControllerInteractionEventArgs e)
		{
			if (this.TouchpadPressed != null)
			{
				this.TouchpadPressed(this, e);
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000735A6 File Offset: 0x000717A6
		public virtual void OnTouchpadReleased(ControllerInteractionEventArgs e)
		{
			if (this.TouchpadReleased != null)
			{
				this.TouchpadReleased(this, e);
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000735BD File Offset: 0x000717BD
		public virtual void OnTouchpadTouchStart(ControllerInteractionEventArgs e)
		{
			if (this.TouchpadTouchStart != null)
			{
				this.TouchpadTouchStart(this, e);
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000735D4 File Offset: 0x000717D4
		public virtual void OnTouchpadTouchEnd(ControllerInteractionEventArgs e)
		{
			if (this.TouchpadTouchEnd != null)
			{
				this.TouchpadTouchEnd(this, e);
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000735EB File Offset: 0x000717EB
		public virtual void OnTouchpadAxisChanged(ControllerInteractionEventArgs e)
		{
			if (this.TouchpadAxisChanged != null)
			{
				this.TouchpadAxisChanged(this, e);
			}
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00073602 File Offset: 0x00071802
		public virtual void OnButtonOneTouchStart(ControllerInteractionEventArgs e)
		{
			if (this.ButtonOneTouchStart != null)
			{
				this.ButtonOneTouchStart(this, e);
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00073619 File Offset: 0x00071819
		public virtual void OnButtonOneTouchEnd(ControllerInteractionEventArgs e)
		{
			if (this.ButtonOneTouchEnd != null)
			{
				this.ButtonOneTouchEnd(this, e);
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00073630 File Offset: 0x00071830
		public virtual void OnButtonOnePressed(ControllerInteractionEventArgs e)
		{
			if (this.ButtonOnePressed != null)
			{
				this.ButtonOnePressed(this, e);
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00073647 File Offset: 0x00071847
		public virtual void OnButtonOneReleased(ControllerInteractionEventArgs e)
		{
			if (this.ButtonOneReleased != null)
			{
				this.ButtonOneReleased(this, e);
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0007365E File Offset: 0x0007185E
		public virtual void OnButtonTwoTouchStart(ControllerInteractionEventArgs e)
		{
			if (this.ButtonTwoTouchStart != null)
			{
				this.ButtonTwoTouchStart(this, e);
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00073675 File Offset: 0x00071875
		public virtual void OnButtonTwoTouchEnd(ControllerInteractionEventArgs e)
		{
			if (this.ButtonTwoTouchEnd != null)
			{
				this.ButtonTwoTouchEnd(this, e);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0007368C File Offset: 0x0007188C
		public virtual void OnButtonTwoPressed(ControllerInteractionEventArgs e)
		{
			if (this.ButtonTwoPressed != null)
			{
				this.ButtonTwoPressed(this, e);
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000736A3 File Offset: 0x000718A3
		public virtual void OnButtonTwoReleased(ControllerInteractionEventArgs e)
		{
			if (this.ButtonTwoReleased != null)
			{
				this.ButtonTwoReleased(this, e);
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000736BA File Offset: 0x000718BA
		public virtual void OnStartMenuPressed(ControllerInteractionEventArgs e)
		{
			if (this.StartMenuPressed != null)
			{
				this.StartMenuPressed(this, e);
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000736D1 File Offset: 0x000718D1
		public virtual void OnStartMenuReleased(ControllerInteractionEventArgs e)
		{
			if (this.StartMenuReleased != null)
			{
				this.StartMenuReleased(this, e);
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000736E8 File Offset: 0x000718E8
		[Obsolete("`VRTK_ControllerEvents.OnAliasPointerOn` has been replaced with `VRTK_Pointer.OnActivationButtonPressed`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasPointerOn(ControllerInteractionEventArgs e)
		{
			if (this.AliasPointerOn != null)
			{
				this.AliasPointerOn(this, e);
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000736FF File Offset: 0x000718FF
		[Obsolete("`VRTK_ControllerEvents.OnAliasPointerOff` has been replaced with `VRTK_Pointer.OnActivationButtonReleased`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasPointerOff(ControllerInteractionEventArgs e)
		{
			if (this.AliasPointerOff != null)
			{
				this.AliasPointerOff(this, e);
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00073716 File Offset: 0x00071916
		[Obsolete("`VRTK_ControllerEvents.OnAliasPointerSet` has been replaced with `VRTK_Pointer.OnSelectionButtonReleased`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasPointerSet(ControllerInteractionEventArgs e)
		{
			if (this.AliasPointerSet != null)
			{
				this.AliasPointerSet(this, e);
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0007372D File Offset: 0x0007192D
		[Obsolete("`VRTK_ControllerEvents.OnAliasGrabOn` has been replaced with `VRTK_InteractGrab.OnGrabButtonPressed`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasGrabOn(ControllerInteractionEventArgs e)
		{
			if (this.AliasGrabOn != null)
			{
				this.AliasGrabOn(this, e);
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00073744 File Offset: 0x00071944
		[Obsolete("`VRTK_ControllerEvents.OnAliasGrabOff` has been replaced with `VRTK_InteractGrab.OnGrabButtonReleased`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasGrabOff(ControllerInteractionEventArgs e)
		{
			if (this.AliasGrabOff != null)
			{
				this.AliasGrabOff(this, e);
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0007375B File Offset: 0x0007195B
		[Obsolete("`VRTK_ControllerEvents.OnAliasUseOn` has been replaced with `VRTK_InteractUse.OnUseButtonPressed`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasUseOn(ControllerInteractionEventArgs e)
		{
			if (this.AliasUseOn != null)
			{
				this.AliasUseOn(this, e);
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00073772 File Offset: 0x00071972
		[Obsolete("`VRTK_ControllerEvents.OnAliasUseOff` has been replaced with `VRTK_InteractUse.OnUseButtonReleased`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasUseOff(ControllerInteractionEventArgs e)
		{
			if (this.AliasUseOff != null)
			{
				this.AliasUseOff(this, e);
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00073789 File Offset: 0x00071989
		[Obsolete("`VRTK_ControllerEvents.OnAliasUIClickOn` has been replaced with `VRTK_UIPointer.OnSelectionButtonPressed`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasUIClickOn(ControllerInteractionEventArgs e)
		{
			if (this.AliasUIClickOn != null)
			{
				this.AliasUIClickOn(this, e);
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000737A0 File Offset: 0x000719A0
		[Obsolete("`VRTK_ControllerEvents.OnAliasUIClickOff` has been replaced with `VRTK_UIPointer.OnSelectionButtonReleased`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasUIClickOff(ControllerInteractionEventArgs e)
		{
			if (this.AliasUIClickOff != null)
			{
				this.AliasUIClickOff(this, e);
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000737B7 File Offset: 0x000719B7
		[Obsolete("`VRTK_ControllerEvents.OnAliasMenuOn` has been replaced with `VRTK_ControllerEvents.OnButtonTwoPressed`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasMenuOn(ControllerInteractionEventArgs e)
		{
			if (this.AliasMenuOn != null)
			{
				this.AliasMenuOn(this, e);
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000737CE File Offset: 0x000719CE
		[Obsolete("`VRTK_ControllerEvents.OnAliasMenuOff` has been replaced with `VRTK_ControllerEvents.OnButtonTwoReleased`. This method will be removed in a future version of VRTK.")]
		public virtual void OnAliasMenuOff(ControllerInteractionEventArgs e)
		{
			if (this.AliasMenuOff != null)
			{
				this.AliasMenuOff(this, e);
			}
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000737E5 File Offset: 0x000719E5
		public virtual void OnControllerEnabled(ControllerInteractionEventArgs e)
		{
			if (this.ControllerEnabled != null)
			{
				this.ControllerEnabled(this, e);
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000737FC File Offset: 0x000719FC
		public virtual void OnControllerDisabled(ControllerInteractionEventArgs e)
		{
			if (this.ControllerDisabled != null)
			{
				this.ControllerDisabled(this, e);
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00073813 File Offset: 0x00071A13
		public virtual void OnControllerIndexChanged(ControllerInteractionEventArgs e)
		{
			if (this.ControllerIndexChanged != null)
			{
				this.ControllerIndexChanged(this, e);
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0007382A File Offset: 0x00071A2A
		public virtual void OnControllerVisible(ControllerInteractionEventArgs e)
		{
			this.controllerVisible = true;
			if (this.ControllerVisible != null)
			{
				this.ControllerVisible(this, e);
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00073848 File Offset: 0x00071A48
		public virtual void OnControllerHidden(ControllerInteractionEventArgs e)
		{
			this.controllerVisible = false;
			if (this.ControllerHidden != null)
			{
				this.ControllerHidden(this, e);
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00073868 File Offset: 0x00071A68
		public virtual ControllerInteractionEventArgs SetControllerEvent()
		{
			bool flag = false;
			return this.SetControllerEvent(ref flag, false, 0f);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00073888 File Offset: 0x00071A88
		public virtual ControllerInteractionEventArgs SetControllerEvent(ref bool buttonBool, bool value = false, float buttonPressure = 0f)
		{
			VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(base.gameObject);
			buttonBool = value;
			ControllerInteractionEventArgs controllerInteractionEventArgs;
			controllerInteractionEventArgs.controllerIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			controllerInteractionEventArgs.controllerReference = controllerReference;
			controllerInteractionEventArgs.buttonPressure = buttonPressure;
			controllerInteractionEventArgs.touchpadAxis = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Touchpad, controllerReference);
			controllerInteractionEventArgs.touchpadAngle = this.CalculateTouchpadAxisAngle(controllerInteractionEventArgs.touchpadAxis);
			return controllerInteractionEventArgs;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000738E3 File Offset: 0x00071AE3
		[Obsolete("`VRTK_ControllerEvents.GetVelocity()` has been replaced with `VRTK_DeviceFinder.GetControllerVelocity(givenController)`. This method will be removed in a future version of VRTK.")]
		public virtual Vector3 GetVelocity()
		{
			return VRTK_DeviceFinder.GetControllerVelocity(base.gameObject);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000738F0 File Offset: 0x00071AF0
		[Obsolete("`VRTK_ControllerEvents.GetAngularVelocity()` has been replaced with `VRTK_DeviceFinder.GetControllerAngularVelocity(givenController)`. This method will be removed in a future version of VRTK.")]
		public virtual Vector3 GetAngularVelocity()
		{
			return VRTK_DeviceFinder.GetControllerAngularVelocity(base.gameObject);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000738FD File Offset: 0x00071AFD
		public virtual Vector2 GetTouchpadAxis()
		{
			return this.touchpadAxis;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00073905 File Offset: 0x00071B05
		public virtual float GetTouchpadAxisAngle()
		{
			return this.CalculateTouchpadAxisAngle(this.touchpadAxis);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00073913 File Offset: 0x00071B13
		public virtual float GetTriggerAxis()
		{
			return this.triggerAxis.x;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00073920 File Offset: 0x00071B20
		public virtual float GetGripAxis()
		{
			return this.gripAxis.x;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0007392D File Offset: 0x00071B2D
		public virtual float GetHairTriggerDelta()
		{
			return this.hairTriggerDelta;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00073935 File Offset: 0x00071B35
		public virtual float GetHairGripDelta()
		{
			return this.hairGripDelta;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0007393D File Offset: 0x00071B3D
		public virtual bool AnyButtonPressed()
		{
			return this.triggerPressed || this.gripPressed || this.touchpadPressed || this.buttonOnePressed || this.buttonTwoPressed || this.startMenuPressed;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00073970 File Offset: 0x00071B70
		public virtual bool IsButtonPressed(VRTK_ControllerEvents.ButtonAlias button)
		{
			switch (button)
			{
			case VRTK_ControllerEvents.ButtonAlias.TriggerHairline:
				return this.triggerHairlinePressed;
			case VRTK_ControllerEvents.ButtonAlias.TriggerTouch:
				return this.triggerTouched;
			case VRTK_ControllerEvents.ButtonAlias.TriggerPress:
				return this.triggerPressed;
			case VRTK_ControllerEvents.ButtonAlias.TriggerClick:
				return this.triggerClicked;
			case VRTK_ControllerEvents.ButtonAlias.GripHairline:
				return this.gripHairlinePressed;
			case VRTK_ControllerEvents.ButtonAlias.GripTouch:
				return this.gripTouched;
			case VRTK_ControllerEvents.ButtonAlias.GripPress:
				return this.gripPressed;
			case VRTK_ControllerEvents.ButtonAlias.GripClick:
				return this.gripClicked;
			case VRTK_ControllerEvents.ButtonAlias.TouchpadTouch:
				return this.touchpadTouched;
			case VRTK_ControllerEvents.ButtonAlias.TouchpadPress:
				return this.touchpadPressed;
			case VRTK_ControllerEvents.ButtonAlias.ButtonOneTouch:
				return this.buttonOneTouched;
			case VRTK_ControllerEvents.ButtonAlias.ButtonOnePress:
				return this.buttonOnePressed;
			case VRTK_ControllerEvents.ButtonAlias.ButtonTwoTouch:
				return this.buttonTwoTouched;
			case VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress:
				return this.buttonTwoPressed;
			case VRTK_ControllerEvents.ButtonAlias.StartMenuPress:
				return this.startMenuPressed;
			default:
				return false;
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00073A2D File Offset: 0x00071C2D
		public virtual void SubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias givenButton, bool startEvent, ControllerInteractionEventHandler callbackMethod)
		{
			this.ButtonAliasEventSubscription(true, givenButton, startEvent, callbackMethod);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00073A39 File Offset: 0x00071C39
		public virtual void UnsubscribeToButtonAliasEvent(VRTK_ControllerEvents.ButtonAlias givenButton, bool startEvent, ControllerInteractionEventHandler callbackMethod)
		{
			this.ButtonAliasEventSubscription(false, givenButton, startEvent, callbackMethod);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00073A48 File Offset: 0x00071C48
		protected virtual void OnEnable()
		{
			GameObject actualController = VRTK_DeviceFinder.GetActualController(base.gameObject);
			if (actualController)
			{
				VRTK_TrackedController component = actualController.GetComponent<VRTK_TrackedController>();
				if (component)
				{
					component.ControllerEnabled += this.TrackedControllerEnabled;
					component.ControllerDisabled += this.TrackedControllerDisabled;
					component.ControllerIndexChanged += this.TrackedControllerIndexChanged;
				}
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00073AB4 File Offset: 0x00071CB4
		protected virtual void OnDisable()
		{
			base.Invoke("DisableEvents", 0f);
			GameObject actualController = VRTK_DeviceFinder.GetActualController(base.gameObject);
			if (actualController)
			{
				VRTK_TrackedController component = actualController.GetComponent<VRTK_TrackedController>();
				if (component)
				{
					component.ControllerEnabled -= this.TrackedControllerEnabled;
					component.ControllerDisabled -= this.TrackedControllerDisabled;
				}
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00073B1C File Offset: 0x00071D1C
		protected virtual void Update()
		{
			VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(base.gameObject);
			if (!VRTK_ControllerReference.IsValid(controllerReference))
			{
				return;
			}
			Vector2 controllerAxis = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Trigger, controllerReference);
			Vector2 controllerAxis2 = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Grip, controllerReference);
			Vector2 controllerAxis3 = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Touchpad, controllerReference);
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.TouchDown, controllerReference))
			{
				this.OnTriggerTouchStart(this.SetControllerEvent(ref this.triggerTouched, true, controllerAxis.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerTouch, true, controllerAxis.x, ref this.triggerTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.TriggerHairline, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnTriggerHairlineStart(this.SetControllerEvent(ref this.triggerHairlinePressed, true, controllerAxis.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerHairline, true, controllerAxis.x, ref this.triggerHairlinePressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnTriggerPressed(this.SetControllerEvent(ref this.triggerPressed, true, controllerAxis.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerPress, true, controllerAxis.x, ref this.triggerPressed);
			}
			if (!this.triggerClicked && controllerAxis.x >= this.triggerClickThreshold)
			{
				this.OnTriggerClicked(this.SetControllerEvent(ref this.triggerClicked, true, controllerAxis.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerClick, true, controllerAxis.x, ref this.triggerClicked);
			}
			else if (this.triggerClicked && controllerAxis.x < this.triggerClickThreshold)
			{
				this.OnTriggerUnclicked(this.SetControllerEvent(ref this.triggerClicked, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerClick, false, 0f, ref this.triggerClicked);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnTriggerReleased(this.SetControllerEvent(ref this.triggerPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerPress, false, 0f, ref this.triggerPressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.TriggerHairline, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnTriggerHairlineEnd(this.SetControllerEvent(ref this.triggerHairlinePressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerHairline, false, 0f, ref this.triggerHairlinePressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Trigger, SDK_BaseController.ButtonPressTypes.TouchUp, controllerReference))
			{
				this.OnTriggerTouchEnd(this.SetControllerEvent(ref this.triggerTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerTouch, false, 0f, ref this.triggerTouched);
			}
			controllerAxis.x = (((!this.triggerTouched && this.triggerAxisZeroOnUntouch) || controllerAxis.x < this.triggerForceZeroThreshold) ? 0f : controllerAxis.x);
			if (VRTK_SharedMethods.Vector2ShallowCompare(this.triggerAxis, controllerAxis, this.axisFidelity))
			{
				this.triggerAxisChanged = false;
			}
			else
			{
				this.OnTriggerAxisChanged(this.SetControllerEvent(ref this.triggerAxisChanged, true, controllerAxis.x));
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.TouchDown, controllerReference))
			{
				this.OnGripTouchStart(this.SetControllerEvent(ref this.gripTouched, true, controllerAxis2.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripTouch, true, controllerAxis2.x, ref this.gripTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.GripHairline, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnGripHairlineStart(this.SetControllerEvent(ref this.gripHairlinePressed, true, controllerAxis2.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripHairline, true, controllerAxis2.x, ref this.gripHairlinePressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnGripPressed(this.SetControllerEvent(ref this.gripPressed, true, controllerAxis2.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripPress, true, controllerAxis2.x, ref this.gripPressed);
			}
			if (!this.gripClicked && controllerAxis2.x >= this.gripClickThreshold)
			{
				this.OnGripClicked(this.SetControllerEvent(ref this.gripClicked, true, controllerAxis2.x));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripClick, true, controllerAxis2.x, ref this.gripClicked);
			}
			else if (this.gripClicked && controllerAxis2.x < this.gripClickThreshold)
			{
				this.OnGripUnclicked(this.SetControllerEvent(ref this.gripClicked, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripClick, false, 0f, ref this.gripClicked);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnGripReleased(this.SetControllerEvent(ref this.gripPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripPress, false, 0f, ref this.gripPressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.GripHairline, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnGripHairlineEnd(this.SetControllerEvent(ref this.gripHairlinePressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripHairline, false, 0f, ref this.gripHairlinePressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Grip, SDK_BaseController.ButtonPressTypes.TouchUp, controllerReference))
			{
				this.OnGripTouchEnd(this.SetControllerEvent(ref this.gripTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripTouch, false, 0f, ref this.gripTouched);
			}
			controllerAxis2.x = (((!this.gripTouched && this.gripAxisZeroOnUntouch) || controllerAxis2.x < this.gripForceZeroThreshold) ? 0f : controllerAxis2.x);
			if (VRTK_SharedMethods.Vector2ShallowCompare(this.gripAxis, controllerAxis2, this.axisFidelity))
			{
				this.gripAxisChanged = false;
			}
			else
			{
				this.OnGripAxisChanged(this.SetControllerEvent(ref this.gripAxisChanged, true, controllerAxis2.x));
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.TouchDown, controllerReference))
			{
				this.OnTouchpadTouchStart(this.SetControllerEvent(ref this.touchpadTouched, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TouchpadTouch, true, 1f, ref this.touchpadTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnTouchpadPressed(this.SetControllerEvent(ref this.touchpadPressed, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TouchpadPress, true, 1f, ref this.touchpadPressed);
			}
			else if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnTouchpadReleased(this.SetControllerEvent(ref this.touchpadPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TouchpadPress, false, 0f, ref this.touchpadPressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.Touchpad, SDK_BaseController.ButtonPressTypes.TouchUp, controllerReference))
			{
				this.OnTouchpadTouchEnd(this.SetControllerEvent(ref this.touchpadTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TouchpadTouch, false, 0f, ref this.touchpadTouched);
				this.touchpadAxis = Vector2.zero;
			}
			if (VRTK_SDK_Bridge.IsTouchpadStatic(this.touchpadTouched, this.touchpadAxis, controllerAxis3, this.axisFidelity))
			{
				this.touchpadAxisChanged = false;
			}
			else
			{
				this.OnTouchpadAxisChanged(this.SetControllerEvent(ref this.touchpadAxisChanged, true, 1f));
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.TouchDown, controllerReference))
			{
				this.OnButtonOneTouchStart(this.SetControllerEvent(ref this.buttonOneTouched, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonOneTouch, true, 1f, ref this.buttonOneTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnButtonOnePressed(this.SetControllerEvent(ref this.buttonOnePressed, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress, true, 1f, ref this.buttonOnePressed);
			}
			else if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnButtonOneReleased(this.SetControllerEvent(ref this.buttonOnePressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress, false, 0f, ref this.buttonOnePressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonOne, SDK_BaseController.ButtonPressTypes.TouchUp, controllerReference))
			{
				this.OnButtonOneTouchEnd(this.SetControllerEvent(ref this.buttonOneTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonOneTouch, false, 0f, ref this.buttonOneTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.TouchDown, controllerReference))
			{
				this.OnButtonTwoTouchStart(this.SetControllerEvent(ref this.buttonTwoTouched, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonTwoTouch, true, 1f, ref this.buttonTwoTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnButtonTwoPressed(this.SetControllerEvent(ref this.buttonTwoPressed, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress, true, 1f, ref this.buttonTwoPressed);
			}
			else if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnButtonTwoReleased(this.SetControllerEvent(ref this.buttonTwoPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress, false, 0f, ref this.buttonTwoPressed);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.TouchUp, controllerReference))
			{
				this.OnButtonTwoTouchEnd(this.SetControllerEvent(ref this.buttonTwoTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonTwoTouch, false, 0f, ref this.buttonTwoTouched);
			}
			if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.PressDown, controllerReference))
			{
				this.OnStartMenuPressed(this.SetControllerEvent(ref this.startMenuPressed, true, 1f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.StartMenuPress, true, 1f, ref this.startMenuPressed);
			}
			else if (VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.StartMenu, SDK_BaseController.ButtonPressTypes.PressUp, controllerReference))
			{
				this.OnStartMenuReleased(this.SetControllerEvent(ref this.startMenuPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.StartMenuPress, false, 0f, ref this.startMenuPressed);
			}
			this.touchpadAxis = (this.touchpadAxisChanged ? new Vector2(controllerAxis3.x, controllerAxis3.y) : this.touchpadAxis);
			this.triggerAxis = (this.triggerAxisChanged ? new Vector2(controllerAxis.x, controllerAxis.y) : this.triggerAxis);
			this.gripAxis = (this.gripAxisChanged ? new Vector2(controllerAxis2.x, controllerAxis2.y) : this.gripAxis);
			this.hairTriggerDelta = VRTK_SDK_Bridge.GetControllerHairlineDelta(SDK_BaseController.ButtonTypes.TriggerHairline, controllerReference);
			this.hairGripDelta = VRTK_SDK_Bridge.GetControllerHairlineDelta(SDK_BaseController.ButtonTypes.GripHairline, controllerReference);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0007438C File Offset: 0x0007258C
		protected virtual void ButtonAliasEventSubscription(bool subscribe, VRTK_ControllerEvents.ButtonAlias givenButton, bool startEvent, ControllerInteractionEventHandler callbackMethod)
		{
			switch (givenButton)
			{
			case VRTK_ControllerEvents.ButtonAlias.TriggerHairline:
				if (subscribe)
				{
					if (startEvent)
					{
						this.TriggerHairlineStart += callbackMethod;
						return;
					}
					this.TriggerHairlineEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.TriggerHairlineStart -= callbackMethod;
						return;
					}
					this.TriggerHairlineEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.TriggerTouch:
				if (subscribe)
				{
					if (startEvent)
					{
						this.TriggerTouchStart += callbackMethod;
						return;
					}
					this.TriggerTouchEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.TriggerTouchStart -= callbackMethod;
						return;
					}
					this.TriggerTouchEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.TriggerPress:
				if (subscribe)
				{
					if (startEvent)
					{
						this.TriggerPressed += callbackMethod;
						return;
					}
					this.TriggerReleased += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.TriggerPressed -= callbackMethod;
						return;
					}
					this.TriggerReleased -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.TriggerClick:
				if (subscribe)
				{
					if (startEvent)
					{
						this.TriggerClicked += callbackMethod;
						return;
					}
					this.TriggerUnclicked += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.TriggerClicked -= callbackMethod;
						return;
					}
					this.TriggerUnclicked -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.GripHairline:
				if (subscribe)
				{
					if (startEvent)
					{
						this.GripHairlineStart += callbackMethod;
						return;
					}
					this.GripHairlineEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.GripHairlineStart -= callbackMethod;
						return;
					}
					this.GripHairlineEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.GripTouch:
				if (subscribe)
				{
					if (startEvent)
					{
						this.GripTouchStart += callbackMethod;
						return;
					}
					this.GripTouchEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.GripTouchStart -= callbackMethod;
						return;
					}
					this.GripTouchEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.GripPress:
				if (subscribe)
				{
					if (startEvent)
					{
						this.GripPressed += callbackMethod;
						return;
					}
					this.GripReleased += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.GripPressed -= callbackMethod;
						return;
					}
					this.GripReleased -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.GripClick:
				if (subscribe)
				{
					if (startEvent)
					{
						this.GripClicked += callbackMethod;
						return;
					}
					this.GripUnclicked += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.GripClicked -= callbackMethod;
						return;
					}
					this.GripUnclicked -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.TouchpadTouch:
				if (subscribe)
				{
					if (startEvent)
					{
						this.TouchpadTouchStart += callbackMethod;
						return;
					}
					this.TouchpadTouchEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.TouchpadTouchStart -= callbackMethod;
						return;
					}
					this.TouchpadTouchEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.TouchpadPress:
				if (subscribe)
				{
					if (startEvent)
					{
						this.TouchpadPressed += callbackMethod;
						return;
					}
					this.TouchpadReleased += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.TouchpadPressed -= callbackMethod;
						return;
					}
					this.TouchpadReleased -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.ButtonOneTouch:
				if (subscribe)
				{
					if (startEvent)
					{
						this.ButtonOneTouchStart += callbackMethod;
						return;
					}
					this.ButtonOneTouchEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.ButtonOneTouchStart -= callbackMethod;
						return;
					}
					this.ButtonOneTouchEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.ButtonOnePress:
				if (subscribe)
				{
					if (startEvent)
					{
						this.ButtonOnePressed += callbackMethod;
						return;
					}
					this.ButtonOneReleased += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.ButtonOnePressed -= callbackMethod;
						return;
					}
					this.ButtonOneReleased -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.ButtonTwoTouch:
				if (subscribe)
				{
					if (startEvent)
					{
						this.ButtonTwoTouchStart += callbackMethod;
						return;
					}
					this.ButtonTwoTouchEnd += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.ButtonTwoTouchStart -= callbackMethod;
						return;
					}
					this.ButtonTwoTouchEnd -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress:
				if (subscribe)
				{
					if (startEvent)
					{
						this.ButtonTwoPressed += callbackMethod;
						return;
					}
					this.ButtonTwoReleased += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.ButtonTwoPressed -= callbackMethod;
						return;
					}
					this.ButtonTwoReleased -= callbackMethod;
					return;
				}
				break;
			case VRTK_ControllerEvents.ButtonAlias.StartMenuPress:
				if (subscribe)
				{
					if (startEvent)
					{
						this.StartMenuPressed += callbackMethod;
						return;
					}
					this.StartMenuReleased += callbackMethod;
					return;
				}
				else
				{
					if (startEvent)
					{
						this.StartMenuPressed -= callbackMethod;
						return;
					}
					this.StartMenuReleased -= callbackMethod;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00074680 File Offset: 0x00072880
		protected virtual void TrackedControllerEnabled(object sender, VRTKTrackedControllerEventArgs e)
		{
			this.OnControllerEnabled(this.SetControllerEvent());
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0007468E File Offset: 0x0007288E
		protected virtual void TrackedControllerDisabled(object sender, VRTKTrackedControllerEventArgs e)
		{
			this.DisableEvents();
			this.OnControllerDisabled(this.SetControllerEvent());
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x000746A2 File Offset: 0x000728A2
		protected virtual void TrackedControllerIndexChanged(object sender, VRTKTrackedControllerEventArgs e)
		{
			this.OnControllerIndexChanged(this.SetControllerEvent());
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000746B0 File Offset: 0x000728B0
		protected virtual float CalculateTouchpadAxisAngle(Vector2 axis)
		{
			float num = Mathf.Atan2(axis.y, axis.x) * 57.29578f;
			num = 90f - num;
			if (num < 0f)
			{
				num += 360f;
			}
			return num;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000746F0 File Offset: 0x000728F0
		protected virtual void EmitAlias(VRTK_ControllerEvents.ButtonAlias type, bool touchDown, float buttonPressure, ref bool buttonBool)
		{
			if (this.pointerToggleButton == type)
			{
				if (touchDown)
				{
					this.pointerPressed = true;
					this.OnAliasPointerOn(this.SetControllerEvent(ref buttonBool, true, buttonPressure));
				}
				else
				{
					this.pointerPressed = false;
					this.OnAliasPointerOff(this.SetControllerEvent(ref buttonBool, false, buttonPressure));
				}
			}
			if (this.pointerSetButton == type && !touchDown)
			{
				this.OnAliasPointerSet(this.SetControllerEvent(ref buttonBool, false, buttonPressure));
			}
			if (this.grabToggleButton == type)
			{
				if (touchDown)
				{
					this.grabPressed = true;
					this.OnAliasGrabOn(this.SetControllerEvent(ref buttonBool, true, buttonPressure));
				}
				else
				{
					this.grabPressed = false;
					this.OnAliasGrabOff(this.SetControllerEvent(ref buttonBool, false, buttonPressure));
				}
			}
			if (this.useToggleButton == type)
			{
				if (touchDown)
				{
					this.usePressed = true;
					this.OnAliasUseOn(this.SetControllerEvent(ref buttonBool, true, buttonPressure));
				}
				else
				{
					this.usePressed = false;
					this.OnAliasUseOff(this.SetControllerEvent(ref buttonBool, false, buttonPressure));
				}
			}
			if (this.uiClickButton == type)
			{
				if (touchDown)
				{
					this.uiClickPressed = true;
					this.OnAliasUIClickOn(this.SetControllerEvent(ref buttonBool, true, buttonPressure));
				}
				else
				{
					this.uiClickPressed = false;
					this.OnAliasUIClickOff(this.SetControllerEvent(ref buttonBool, false, buttonPressure));
				}
			}
			if (this.menuToggleButton == type)
			{
				if (touchDown)
				{
					this.menuPressed = true;
					this.OnAliasMenuOn(this.SetControllerEvent(ref buttonBool, true, buttonPressure));
					return;
				}
				this.menuPressed = false;
				this.OnAliasMenuOff(this.SetControllerEvent(ref buttonBool, false, buttonPressure));
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00074844 File Offset: 0x00072A44
		protected virtual void DisableEvents()
		{
			if (this.triggerPressed)
			{
				this.OnTriggerReleased(this.SetControllerEvent(ref this.triggerPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerPress, false, 0f, ref this.triggerPressed);
			}
			if (this.triggerTouched)
			{
				this.OnTriggerTouchEnd(this.SetControllerEvent(ref this.triggerTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerTouch, false, 0f, ref this.triggerTouched);
			}
			if (this.triggerHairlinePressed)
			{
				this.OnTriggerHairlineEnd(this.SetControllerEvent(ref this.triggerHairlinePressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerHairline, false, 0f, ref this.triggerHairlinePressed);
			}
			if (this.triggerClicked)
			{
				this.OnTriggerUnclicked(this.SetControllerEvent(ref this.triggerClicked, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TriggerClick, false, 0f, ref this.triggerClicked);
			}
			if (this.gripPressed)
			{
				this.OnGripReleased(this.SetControllerEvent(ref this.gripPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripPress, false, 0f, ref this.gripPressed);
			}
			if (this.gripTouched)
			{
				this.OnGripTouchEnd(this.SetControllerEvent(ref this.gripTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripTouch, false, 0f, ref this.gripTouched);
			}
			if (this.gripHairlinePressed)
			{
				this.OnGripHairlineEnd(this.SetControllerEvent(ref this.gripHairlinePressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripHairline, false, 0f, ref this.gripHairlinePressed);
			}
			if (this.gripClicked)
			{
				this.OnGripUnclicked(this.SetControllerEvent(ref this.gripClicked, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.GripClick, false, 0f, ref this.gripClicked);
			}
			if (this.touchpadPressed)
			{
				this.OnTouchpadReleased(this.SetControllerEvent(ref this.touchpadPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TouchpadPress, false, 0f, ref this.touchpadPressed);
			}
			if (this.touchpadTouched)
			{
				this.OnTouchpadTouchEnd(this.SetControllerEvent(ref this.touchpadTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.TouchpadTouch, false, 0f, ref this.touchpadTouched);
			}
			if (this.buttonOnePressed)
			{
				this.OnButtonOneReleased(this.SetControllerEvent(ref this.buttonOnePressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress, false, 0f, ref this.buttonOnePressed);
			}
			if (this.buttonOneTouched)
			{
				this.OnButtonOneTouchEnd(this.SetControllerEvent(ref this.buttonOneTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonOneTouch, false, 0f, ref this.buttonOneTouched);
			}
			if (this.buttonTwoPressed)
			{
				this.OnButtonTwoReleased(this.SetControllerEvent(ref this.buttonTwoPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress, false, 0f, ref this.buttonTwoPressed);
			}
			if (this.buttonTwoTouched)
			{
				this.OnButtonTwoTouchEnd(this.SetControllerEvent(ref this.buttonTwoTouched, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.ButtonTwoTouch, false, 0f, ref this.buttonTwoTouched);
			}
			if (this.startMenuPressed)
			{
				this.OnStartMenuReleased(this.SetControllerEvent(ref this.startMenuPressed, false, 0f));
				this.EmitAlias(VRTK_ControllerEvents.ButtonAlias.StartMenuPress, false, 0f, ref this.startMenuPressed);
			}
			this.triggerAxisChanged = false;
			this.gripAxisChanged = false;
			this.touchpadAxisChanged = false;
			VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(base.gameObject);
			if (VRTK_ControllerReference.IsValid(controllerReference))
			{
				Vector2 controllerAxis = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Trigger, controllerReference);
				Vector2 controllerAxis2 = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Grip, controllerReference);
				Vector2 controllerAxis3 = VRTK_SDK_Bridge.GetControllerAxis(SDK_BaseController.ButtonTypes.Touchpad, controllerReference);
				this.touchpadAxis = new Vector2(controllerAxis3.x, controllerAxis3.y);
				this.triggerAxis = new Vector2(controllerAxis.x, controllerAxis.y);
				this.gripAxis = new Vector2(controllerAxis2.x, controllerAxis2.y);
				this.hairTriggerDelta = VRTK_SDK_Bridge.GetControllerHairlineDelta(SDK_BaseController.ButtonTypes.TriggerHairline, controllerReference);
				this.hairGripDelta = VRTK_SDK_Bridge.GetControllerHairlineDelta(SDK_BaseController.ButtonTypes.GripHairline, controllerReference);
			}
		}

		// Token: 0x040011AC RID: 4524
		[Header("Action Alias Buttons")]
		[Tooltip("**OBSOLETE [use VRTK_Pointer.activationButton]** The button to use for the action of turning a laser pointer on / off.")]
		[Obsolete("`VRTK_ControllerEvents.pointerToggleButton` is no longer used in the new `VRTK_Pointer` class, use `VRTK_Pointer.activationButton` instead. This parameter will be removed in a future version of VRTK.")]
		public VRTK_ControllerEvents.ButtonAlias pointerToggleButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x040011AD RID: 4525
		[Tooltip("**OBSOLETE [use VRTK_Pointer.selectionButton]** The button to use for the action of setting a destination marker from the cursor position of the pointer.")]
		[Obsolete("`VRTK_ControllerEvents.pointerSetButton` is no longer used in the new `VRTK_Pointer` class, use `VRTK_Pointer.selectionButton` instead. This parameter will be removed in a future version of VRTK.")]
		public VRTK_ControllerEvents.ButtonAlias pointerSetButton = VRTK_ControllerEvents.ButtonAlias.TouchpadPress;

		// Token: 0x040011AE RID: 4526
		[Tooltip("**OBSOLETE [use VRTK_InteractGrab.grabButton]** The button to use for the action of grabbing game objects.")]
		[Obsolete("`VRTK_ControllerEvents.grabToggleButton` is no longer used in the `VRTK_InteractGrab` class, use `VRTK_InteractGrab.grabButton` instead. This parameter will be removed in a future version of VRTK.")]
		public VRTK_ControllerEvents.ButtonAlias grabToggleButton = VRTK_ControllerEvents.ButtonAlias.GripPress;

		// Token: 0x040011AF RID: 4527
		[Tooltip("**OBSOLETE [use VRTK_InteractUse.useButton]** The button to use for the action of using game objects.")]
		[Obsolete("`VRTK_ControllerEvents.useToggleButton` is no longer used in the `VRTK_InteractUse` class, use `VRTK_InteractUse.useButton` instead. This parameter will be removed in a future version of VRTK.")]
		public VRTK_ControllerEvents.ButtonAlias useToggleButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;

		// Token: 0x040011B0 RID: 4528
		[Tooltip("**OBSOLETE [use VRTK_UIPointer.selectionButton]** The button to use for the action of clicking a UI element.")]
		[Obsolete("`VRTK_ControllerEvents.uiClickButton` is no longer used in the `VRTK_UIPointer` class, use `VRTK_UIPointer.selectionButton` instead. This parameter will be removed in a future version of VRTK.")]
		public VRTK_ControllerEvents.ButtonAlias uiClickButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;

		// Token: 0x040011B1 RID: 4529
		[Tooltip("**OBSOLETE [use VRTK_ControllerEvents.buttonTwoPressed]** The button to use for the action of bringing up an in-game menu.")]
		[Obsolete("`VRTK_ControllerEvents.menuToggleButton` is no longer used, use `VRTK_ControllerEvents.buttonTwoPressed` instead. This parameter will be removed in a future version of VRTK.")]
		public VRTK_ControllerEvents.ButtonAlias menuToggleButton = VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress;

		// Token: 0x040011B2 RID: 4530
		[Header("Axis Refinement")]
		[Tooltip("The amount of fidelity in the changes on the axis, which is defaulted to 1. Any number higher than 2 will probably give too sensitive results.")]
		public int axisFidelity = 1;

		// Token: 0x040011B3 RID: 4531
		[Tooltip("The level on the trigger axis to reach before a click is registered.")]
		public float triggerClickThreshold = 1f;

		// Token: 0x040011B4 RID: 4532
		[Tooltip("The level on the trigger axis to reach before the axis is forced to 0f.")]
		public float triggerForceZeroThreshold = 0.01f;

		// Token: 0x040011B5 RID: 4533
		[Tooltip("If this is checked then the trigger axis will be forced to 0f when the trigger button reports an untouch event.")]
		public bool triggerAxisZeroOnUntouch;

		// Token: 0x040011B6 RID: 4534
		[Tooltip("The level on the grip axis to reach before a click is registered.")]
		public float gripClickThreshold = 1f;

		// Token: 0x040011B7 RID: 4535
		[Tooltip("The level on the grip axis to reach before the axis is forced to 0f.")]
		public float gripForceZeroThreshold = 0.01f;

		// Token: 0x040011B8 RID: 4536
		[Tooltip("If this is checked then the grip axis will be forced to 0f when the grip button reports an untouch event.")]
		public bool gripAxisZeroOnUntouch;

		// Token: 0x040011B9 RID: 4537
		[HideInInspector]
		public bool triggerPressed;

		// Token: 0x040011BA RID: 4538
		[HideInInspector]
		public bool triggerTouched;

		// Token: 0x040011BB RID: 4539
		[HideInInspector]
		public bool triggerHairlinePressed;

		// Token: 0x040011BC RID: 4540
		[HideInInspector]
		public bool triggerClicked;

		// Token: 0x040011BD RID: 4541
		[HideInInspector]
		public bool triggerAxisChanged;

		// Token: 0x040011BE RID: 4542
		[HideInInspector]
		public bool gripPressed;

		// Token: 0x040011BF RID: 4543
		[HideInInspector]
		public bool gripTouched;

		// Token: 0x040011C0 RID: 4544
		[HideInInspector]
		public bool gripHairlinePressed;

		// Token: 0x040011C1 RID: 4545
		[HideInInspector]
		public bool gripClicked;

		// Token: 0x040011C2 RID: 4546
		[HideInInspector]
		public bool gripAxisChanged;

		// Token: 0x040011C3 RID: 4547
		[HideInInspector]
		public bool touchpadPressed;

		// Token: 0x040011C4 RID: 4548
		[HideInInspector]
		public bool touchpadTouched;

		// Token: 0x040011C5 RID: 4549
		[HideInInspector]
		public bool touchpadAxisChanged;

		// Token: 0x040011C6 RID: 4550
		[HideInInspector]
		public bool buttonOnePressed;

		// Token: 0x040011C7 RID: 4551
		[HideInInspector]
		public bool buttonOneTouched;

		// Token: 0x040011C8 RID: 4552
		[HideInInspector]
		public bool buttonTwoPressed;

		// Token: 0x040011C9 RID: 4553
		[HideInInspector]
		public bool buttonTwoTouched;

		// Token: 0x040011CA RID: 4554
		[HideInInspector]
		public bool startMenuPressed;

		// Token: 0x040011CB RID: 4555
		[HideInInspector]
		[Obsolete("`VRTK_ControllerEvents.pointerPressed` is no longer used, use `VRTK_Pointer.IsActivationButtonPressed()` instead. This parameter will be removed in a future version of VRTK.")]
		public bool pointerPressed;

		// Token: 0x040011CC RID: 4556
		[HideInInspector]
		[Obsolete("`VRTK_ControllerEvents.grabPressed` is no longer used, use `VRTK_InteractGrab.IsGrabButtonPressed()` instead. This parameter will be removed in a future version of VRTK.")]
		public bool grabPressed;

		// Token: 0x040011CD RID: 4557
		[HideInInspector]
		[Obsolete("`VRTK_ControllerEvents.usePressed` is no longer used, use `VRTK_InteractUse.IsUseButtonPressed()` instead. This parameter will be removed in a future version of VRTK.")]
		public bool usePressed;

		// Token: 0x040011CE RID: 4558
		[HideInInspector]
		[Obsolete("`VRTK_ControllerEvents.uiClickPressed` is no longer used, use `VRTK_UIPointer.IsSelectionButtonPressed()` instead. This parameter will be removed in a future version of VRTK.")]
		public bool uiClickPressed;

		// Token: 0x040011CF RID: 4559
		[HideInInspector]
		[Obsolete("`VRTK_ControllerEvents.menuPressed` is no longer used, use `VRTK_ControllerEvents.buttonTwoPressed` instead. This parameter will be removed in a future version of VRTK.")]
		public bool menuPressed;

		// Token: 0x040011D0 RID: 4560
		[HideInInspector]
		public bool controllerVisible = true;

		// Token: 0x04001202 RID: 4610
		protected Vector2 touchpadAxis = Vector2.zero;

		// Token: 0x04001203 RID: 4611
		protected Vector2 triggerAxis = Vector2.zero;

		// Token: 0x04001204 RID: 4612
		protected Vector2 gripAxis = Vector2.zero;

		// Token: 0x04001205 RID: 4613
		protected float hairTriggerDelta;

		// Token: 0x04001206 RID: 4614
		protected float hairGripDelta;

		// Token: 0x020005CF RID: 1487
		public enum ButtonAlias
		{
			// Token: 0x0400275E RID: 10078
			Undefined,
			// Token: 0x0400275F RID: 10079
			TriggerHairline,
			// Token: 0x04002760 RID: 10080
			TriggerTouch,
			// Token: 0x04002761 RID: 10081
			TriggerPress,
			// Token: 0x04002762 RID: 10082
			TriggerClick,
			// Token: 0x04002763 RID: 10083
			GripHairline,
			// Token: 0x04002764 RID: 10084
			GripTouch,
			// Token: 0x04002765 RID: 10085
			GripPress,
			// Token: 0x04002766 RID: 10086
			GripClick,
			// Token: 0x04002767 RID: 10087
			TouchpadTouch,
			// Token: 0x04002768 RID: 10088
			TouchpadPress,
			// Token: 0x04002769 RID: 10089
			ButtonOneTouch,
			// Token: 0x0400276A RID: 10090
			ButtonOnePress,
			// Token: 0x0400276B RID: 10091
			ButtonTwoTouch,
			// Token: 0x0400276C RID: 10092
			ButtonTwoPress,
			// Token: 0x0400276D RID: 10093
			StartMenuPress
		}
	}
}
