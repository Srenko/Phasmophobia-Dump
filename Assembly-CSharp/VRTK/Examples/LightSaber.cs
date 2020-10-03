using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x02000359 RID: 857
	public class LightSaber : VRTK_InteractableObject
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x00097410 File Offset: 0x00095610
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			this.beamExtendSpeed = 5f;
			this.bladePhaseColors = new Color[]
			{
				Color.blue,
				Color.cyan
			};
			this.activeColor = this.bladePhaseColors[0];
			this.targetColor = this.bladePhaseColors[1];
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00097477 File Offset: 0x00095677
		public override void StopUsing(VRTK_InteractUse usingObject)
		{
			base.StopUsing(usingObject);
			this.beamExtendSpeed = -5f;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0009748B File Offset: 0x0009568B
		protected void Start()
		{
			this.blade = base.transform.Find("Blade").gameObject;
			this.currentBeamSize = this.beamLimits.x;
			this.SetBeamSize();
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000974C0 File Offset: 0x000956C0
		protected override void Update()
		{
			base.Update();
			this.currentBeamSize = Mathf.Clamp(this.blade.transform.localScale.y + this.beamExtendSpeed * Time.deltaTime, this.beamLimits.x, this.beamLimits.y);
			this.SetBeamSize();
			this.PulseBeam();
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00097524 File Offset: 0x00095724
		private void SetBeamSize()
		{
			this.blade.transform.localScale = new Vector3(1f, this.currentBeamSize, 1f);
			this.beamActive = (this.currentBeamSize >= this.beamLimits.y);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00097574 File Offset: 0x00095774
		private void PulseBeam()
		{
			if (this.beamActive)
			{
				Color color = Color.Lerp(this.activeColor, this.targetColor, Mathf.PingPong(Time.time, 1f));
				this.blade.transform.Find("Beam").GetComponent<MeshRenderer>().material.color = color;
				if (color == this.targetColor)
				{
					Color color2 = this.activeColor;
					this.activeColor = this.targetColor;
					this.targetColor = color2;
				}
			}
		}

		// Token: 0x04001769 RID: 5993
		private bool beamActive;

		// Token: 0x0400176A RID: 5994
		private Vector2 beamLimits = new Vector2(0f, 1.2f);

		// Token: 0x0400176B RID: 5995
		private float currentBeamSize;

		// Token: 0x0400176C RID: 5996
		private float beamExtendSpeed;

		// Token: 0x0400176D RID: 5997
		private GameObject blade;

		// Token: 0x0400176E RID: 5998
		private Color activeColor;

		// Token: 0x0400176F RID: 5999
		private Color targetColor;

		// Token: 0x04001770 RID: 6000
		private Color[] bladePhaseColors;
	}
}
