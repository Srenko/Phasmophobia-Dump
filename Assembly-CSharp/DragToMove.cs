using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class DragToMove : MonoBehaviour
{
	// Token: 0x06000255 RID: 597 RVA: 0x0000FC08 File Offset: 0x0000DE08
	public void Start()
	{
		this.cubeStartPositions = new Vector3[this.cubes.Length];
		for (int i = 0; i < this.cubes.Length; i++)
		{
			Transform transform = this.cubes[i];
			this.cubeStartPositions[i] = transform.position;
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000FC58 File Offset: 0x0000DE58
	public void Update()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			return;
		}
		if (this.recording)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
		{
			base.StartCoroutine("RecordMouse");
			return;
		}
		if (this.PositionsQueue.Count == 0)
		{
			return;
		}
		Vector3 a = this.PositionsQueue[this.nextPosIndex];
		int index = (this.nextPosIndex > 0) ? (this.nextPosIndex - 1) : (this.PositionsQueue.Count - 1);
		Vector3 a2 = this.PositionsQueue[index];
		this.lerpTime += Time.deltaTime * this.speed;
		for (int i = 0; i < this.cubes.Length; i++)
		{
			Component component = this.cubes[i];
			Vector3 b = a + this.cubeStartPositions[i];
			Vector3 a3 = a2 + this.cubeStartPositions[i];
			component.transform.position = Vector3.Lerp(a3, b, this.lerpTime);
		}
		if (this.lerpTime > 1f)
		{
			this.nextPosIndex = (this.nextPosIndex + 1) % this.PositionsQueue.Count;
			this.lerpTime = 0f;
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000FD88 File Offset: 0x0000DF88
	public IEnumerator RecordMouse()
	{
		this.recording = true;
		this.PositionsQueue.Clear();
		while (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			yield return new WaitForSeconds(0.1f);
			Vector3 pos = Input.mousePosition;
			if (Input.touchCount > 0)
			{
				pos = Input.GetTouch(0).position;
			}
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out raycastHit))
			{
				this.PositionsQueue.Add(raycastHit.point);
			}
		}
		this.nextPosIndex = 0;
		this.recording = false;
		yield break;
	}

	// Token: 0x0400028C RID: 652
	public float speed = 5f;

	// Token: 0x0400028D RID: 653
	public Transform[] cubes;

	// Token: 0x0400028E RID: 654
	public List<Vector3> PositionsQueue = new List<Vector3>(20);

	// Token: 0x0400028F RID: 655
	private Vector3[] cubeStartPositions;

	// Token: 0x04000290 RID: 656
	private int nextPosIndex;

	// Token: 0x04000291 RID: 657
	private float lerpTime;

	// Token: 0x04000292 RID: 658
	private bool recording;
}
