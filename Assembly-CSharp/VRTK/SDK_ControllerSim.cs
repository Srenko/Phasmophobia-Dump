using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000277 RID: 631
	public class SDK_ControllerSim : MonoBehaviour
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000695C0 File Offset: 0x000677C0
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000695C8 File Offset: 0x000677C8
		public bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000695D4 File Offset: 0x000677D4
		public Vector3 GetVelocity()
		{
			Vector3 vector = Vector3.zero;
			foreach (Vector3 b in this.posList)
			{
				vector += b;
			}
			vector /= (float)this.posList.Count;
			return vector;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00069644 File Offset: 0x00067844
		public Vector3 GetAngularVelocity()
		{
			Vector3 vector = Vector3.zero;
			foreach (Vector3 b in this.rotList)
			{
				vector += b;
			}
			vector /= (float)this.rotList.Count;
			return vector;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000696B4 File Offset: 0x000678B4
		private void Awake()
		{
			this.posList = new List<Vector3>();
			this.rotList = new List<Vector3>();
			this.lastPos = base.transform.position;
			this.lastRot = base.transform.rotation;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000696F0 File Offset: 0x000678F0
		private void Update()
		{
			this.posList.Add((base.transform.position - this.lastPos) / Time.deltaTime);
			if (this.posList.Count > 4)
			{
				this.posList.RemoveAt(0);
			}
			(base.transform.rotation * Quaternion.Inverse(this.lastRot)).ToAngleAxis(out this.magnitude, out this.axis);
			this.rotList.Add(this.axis * this.magnitude);
			if (this.rotList.Count > 4)
			{
				this.rotList.RemoveAt(0);
			}
			this.lastPos = base.transform.position;
			this.lastRot = base.transform.rotation;
		}

		// Token: 0x040010B1 RID: 4273
		private Vector3 lastPos;

		// Token: 0x040010B2 RID: 4274
		private Quaternion lastRot;

		// Token: 0x040010B3 RID: 4275
		private List<Vector3> posList;

		// Token: 0x040010B4 RID: 4276
		private List<Vector3> rotList;

		// Token: 0x040010B5 RID: 4277
		private bool selected;

		// Token: 0x040010B6 RID: 4278
		private float magnitude;

		// Token: 0x040010B7 RID: 4279
		private Vector3 axis;
	}
}
