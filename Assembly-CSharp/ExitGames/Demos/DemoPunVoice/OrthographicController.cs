using System;
using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x02000492 RID: 1170
	public class OrthographicController : ThirdPersonController
	{
		// Token: 0x06002470 RID: 9328 RVA: 0x000B1FF3 File Offset: 0x000B01F3
		protected override void Init()
		{
			base.Init();
			this.ControllerCamera = Camera.main;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000B2006 File Offset: 0x000B0206
		protected override void SetCamera()
		{
			base.SetCamera();
			this.offset = this.camTrans.position - base.transform.position;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000B202F File Offset: 0x000B022F
		protected override void Move(float h, float v)
		{
			base.Move(h, v);
			this.CameraFollow();
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000B2040 File Offset: 0x000B0240
		private void CameraFollow()
		{
			Vector3 b = base.transform.position + this.offset;
			this.camTrans.position = Vector3.Lerp(this.camTrans.position, b, this.smoothing * Time.deltaTime);
		}

		// Token: 0x040021A5 RID: 8613
		public float smoothing = 5f;

		// Token: 0x040021A6 RID: 8614
		private Vector3 offset;
	}
}
