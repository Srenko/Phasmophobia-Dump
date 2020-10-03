using System;
using UnityEngine;

namespace MeshCombineStudio
{
	// Token: 0x020004AF RID: 1199
	public class MCS_CameraController : MonoBehaviour
	{
		// Token: 0x0600256D RID: 9581 RVA: 0x000B9F1C File Offset: 0x000B811C
		private void Awake()
		{
			this.t = base.transform;
			this.CreateParents();
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000B9F30 File Offset: 0x000B8130
		private void CreateParents()
		{
			this.cameraMountGO = new GameObject("CameraMount");
			this.cameraChildGO = new GameObject("CameraChild");
			this.cameraMountT = this.cameraMountGO.transform;
			this.cameraChildT = this.cameraChildGO.transform;
			this.cameraChildT.SetParent(this.cameraMountT);
			this.cameraMountT.position = this.t.position;
			this.cameraMountT.rotation = this.t.rotation;
			this.t.SetParent(this.cameraChildT);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000B9FD0 File Offset: 0x000B81D0
		private void Update()
		{
			Vector3 vector = (Input.mousePosition - this.oldMousePosition) * this.mouseMoveSpeed * (Time.deltaTime * 60f);
			if (Input.GetMouseButton(1))
			{
				this.cameraMountT.Rotate(0f, vector.x, 0f, Space.Self);
				this.cameraChildT.Rotate(-vector.y, 0f, 0f, Space.Self);
			}
			this.oldMousePosition = Input.mousePosition;
			Vector3 vector2 = Vector3.zero;
			if (Input.GetKey(KeyCode.W))
			{
				vector2.z = this.speed;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				vector2.z = -this.speed;
			}
			else if (Input.GetKey(KeyCode.A))
			{
				vector2.x = -this.speed;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				vector2.x = this.speed;
			}
			else if (Input.GetKey(KeyCode.Q))
			{
				vector2.y = -this.speed;
			}
			else if (Input.GetKey(KeyCode.E))
			{
				vector2.y = this.speed;
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				vector2 *= this.shiftMulti;
			}
			else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				vector2 *= this.controlMulti;
			}
			vector2 *= Time.deltaTime * 60f;
			Quaternion identity = Quaternion.identity;
			identity.eulerAngles = new Vector3(this.cameraChildT.eulerAngles.x, this.cameraMountT.eulerAngles.y, 0f);
			vector2 = identity * vector2;
			this.cameraMountT.position += vector2;
		}

		// Token: 0x040022A0 RID: 8864
		public float speed = 10f;

		// Token: 0x040022A1 RID: 8865
		public float mouseMoveSpeed = 1f;

		// Token: 0x040022A2 RID: 8866
		public float shiftMulti = 3f;

		// Token: 0x040022A3 RID: 8867
		public float controlMulti = 0.5f;

		// Token: 0x040022A4 RID: 8868
		private Vector3 oldMousePosition;

		// Token: 0x040022A5 RID: 8869
		private GameObject cameraMountGO;

		// Token: 0x040022A6 RID: 8870
		private GameObject cameraChildGO;

		// Token: 0x040022A7 RID: 8871
		private Transform cameraMountT;

		// Token: 0x040022A8 RID: 8872
		private Transform cameraChildT;

		// Token: 0x040022A9 RID: 8873
		private Transform t;
	}
}
