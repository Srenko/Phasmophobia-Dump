using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200027B RID: 635
	[SDK_Description(typeof(SDK_SimSystem), 0)]
	public class SDK_SimHeadset : SDK_BaseHeadset
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0006AF94 File Offset: 0x00069194
		public override void ProcessUpdate(Dictionary<string, object> options)
		{
			if (this.camera != null)
			{
				this.posList.Add((this.camera.position - this.lastPos) / Time.deltaTime);
				if (this.posList.Count > 4)
				{
					this.posList.RemoveAt(0);
				}
				(this.camera.rotation * Quaternion.Inverse(this.lastRot)).ToAngleAxis(out this.magnitude, out this.axis);
				this.rotList.Add(this.axis * this.magnitude);
				if (this.rotList.Count > 4)
				{
					this.rotList.RemoveAt(0);
				}
				this.lastPos = this.camera.position;
				this.lastRot = this.camera.rotation;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00003F60 File Offset: 0x00002160
		public override void ProcessFixedUpdate(Dictionary<string, object> options)
		{
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0006B080 File Offset: 0x00069280
		public override Transform GetHeadset()
		{
			if (this.camera == null)
			{
				GameObject gameObject = SDK_InputSimulator.FindInScene();
				if (gameObject)
				{
					this.camera = gameObject.transform.Find("Neck/Camera");
				}
			}
			return this.camera;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0006B0C5 File Offset: 0x000692C5
		public override Transform GetHeadsetCamera()
		{
			return this.GetHeadset();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0006B0D0 File Offset: 0x000692D0
		public override Vector3 GetHeadsetVelocity()
		{
			Vector3 vector = Vector3.zero;
			foreach (Vector3 b in this.posList)
			{
				vector += b;
			}
			vector /= (float)this.posList.Count;
			return vector;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0006B140 File Offset: 0x00069340
		public override Vector3 GetHeadsetAngularVelocity()
		{
			Vector3 vector = Vector3.zero;
			foreach (Vector3 b in this.rotList)
			{
				vector += b;
			}
			vector /= (float)this.rotList.Count;
			return vector;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0006B1B0 File Offset: 0x000693B0
		public override void HeadsetFade(Color color, float duration, bool fadeOverlay = false)
		{
			VRTK_ScreenFade.Start(color, duration);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0006B1B9 File Offset: 0x000693B9
		public override bool HasHeadsetFade(Transform obj)
		{
			return obj.GetComponentInChildren<VRTK_ScreenFade>() != null;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0006B1C7 File Offset: 0x000693C7
		public override void AddHeadsetFade(Transform camera)
		{
			if (camera != null && camera.GetComponent<VRTK_ScreenFade>() == null)
			{
				camera.gameObject.AddComponent<VRTK_ScreenFade>();
			}
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0006B1EC File Offset: 0x000693EC
		private void Awake()
		{
			this.posList = new List<Vector3>();
			this.rotList = new List<Vector3>();
			Transform headset = this.GetHeadset();
			if (headset != null)
			{
				this.lastPos = headset.position;
				this.lastRot = headset.rotation;
			}
		}

		// Token: 0x040010EC RID: 4332
		private Transform camera;

		// Token: 0x040010ED RID: 4333
		private Vector3 lastPos;

		// Token: 0x040010EE RID: 4334
		private Quaternion lastRot;

		// Token: 0x040010EF RID: 4335
		private List<Vector3> posList = new List<Vector3>();

		// Token: 0x040010F0 RID: 4336
		private List<Vector3> rotList = new List<Vector3>();

		// Token: 0x040010F1 RID: 4337
		private float magnitude;

		// Token: 0x040010F2 RID: 4338
		private Vector3 axis;
	}
}
