using System;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x0200041D RID: 1053
	[RequireComponent(typeof(Camera))]
	public class FallbackCameraController : MonoBehaviour
	{
		// Token: 0x06002063 RID: 8291 RVA: 0x0009FC79 File Offset: 0x0009DE79
		private void OnEnable()
		{
			this.realTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0009FC88 File Offset: 0x0009DE88
		private void Update()
		{
			float num = 0f;
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				num += 1f;
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				num -= 1f;
			}
			float num2 = 0f;
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				num2 += 1f;
			}
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				num2 -= 1f;
			}
			float d = this.speed;
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				d = this.shiftSpeed;
			}
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float d2 = realtimeSinceStartup - this.realTime;
			this.realTime = realtimeSinceStartup;
			Vector3 direction = new Vector3(num2, 0f, num) * d * d2;
			base.transform.position += base.transform.TransformDirection(direction);
			Vector3 mousePosition = Input.mousePosition;
			if (Input.GetMouseButtonDown(1))
			{
				this.startMousePosition = mousePosition;
				this.startEulerAngles = base.transform.localEulerAngles;
			}
			if (Input.GetMouseButton(1))
			{
				Vector3 vector = mousePosition - this.startMousePosition;
				base.transform.localEulerAngles = this.startEulerAngles + new Vector3(-vector.y * 360f / (float)Screen.height, vector.x * 360f / (float)Screen.width, 0f);
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0009FE18 File Offset: 0x0009E018
		private void OnGUI()
		{
			if (this.showInstructions)
			{
				GUI.Label(new Rect(10f, 10f, 600f, 400f), "WASD/Arrow Keys to translate the camera\nRight mouse click to rotate the camera\nLeft mouse click for standard interactions.\n");
			}
		}

		// Token: 0x04001DEC RID: 7660
		public float speed = 4f;

		// Token: 0x04001DED RID: 7661
		public float shiftSpeed = 16f;

		// Token: 0x04001DEE RID: 7662
		public bool showInstructions = true;

		// Token: 0x04001DEF RID: 7663
		private Vector3 startEulerAngles;

		// Token: 0x04001DF0 RID: 7664
		private Vector3 startMousePosition;

		// Token: 0x04001DF1 RID: 7665
		private float realTime;
	}
}
