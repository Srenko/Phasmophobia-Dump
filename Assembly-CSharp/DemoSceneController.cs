using System;
using System.Collections;
using TLGFPowerBooks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001F RID: 31
public class DemoSceneController : MonoBehaviour
{
	// Token: 0x060000D8 RID: 216 RVA: 0x000067D8 File Offset: 0x000049D8
	private void Start()
	{
		this.mainCam.transform.position = this.camWaypoints[this.camWaypointIndex].position;
		this.mainCam.transform.rotation = this.camWaypoints[this.camWaypointIndex].rotation;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000682C File Offset: 0x00004A2C
	private void Update()
	{
		if (this.camWaypointIndex == 0 && this.rotateAround)
		{
			this.mainCam.transform.RotateAround(Vector3.zero, Vector3.up, 10f * Time.deltaTime);
		}
		if (this.camWaypointIndex == 2 || this.camWaypointIndex == 4)
		{
			if (this.overlayPowerBook != null && this.overlayPowerBook.GetBookState() == PBook.BookState.CLOSED)
			{
				if (this.camWaypointIndex == 2)
				{
					this.stepTexts[this.camWaypointIndex].SetActive(true);
					this.overlayPowerBook.transform.position = this.overlayBookOriginalPos;
					this.overlayPowerBook.transform.rotation = this.overlayBookOriginalRot;
					this.overlayCam.gameObject.SetActive(false);
				}
				if (this.camWaypointIndex == 4)
				{
					this.stepTexts[this.camWaypointIndex].SetActive(true);
				}
				this.exampleUIController.gameObject.SetActive(false);
				this.exampleUIController.pBook = null;
				this.overlayPowerBook = null;
			}
			RaycastHit raycastHit;
			if (Input.GetMouseButtonDown(0) && this.overlayPowerBook == null && Physics.Raycast(this.mainCam.ScreenPointToRay(Input.mousePosition), out raycastHit) && raycastHit.transform.parent.GetComponent<PBook>() != null)
			{
				this.overlayPowerBook = raycastHit.transform.parent.GetComponent<PBook>();
				if (this.camWaypointIndex == 2)
				{
					BoxCollider boxCollider = (BoxCollider)raycastHit.collider;
					this.overlayBookOriginalPos = raycastHit.transform.position;
					this.overlayBookOriginalRot = raycastHit.transform.rotation;
					this.overlayPowerBook.transform.position = new Vector3(boxCollider.size.y / 2f - 0.005f, 0f, 9.875f);
					this.overlayPowerBook.transform.rotation = Quaternion.identity;
					this.overlayCam.gameObject.SetActive(true);
				}
				this.overlayPowerBook.OpenBook();
				this.exampleUIController.gameObject.SetActive(true);
				this.exampleUIController.pBook = this.overlayPowerBook;
				this.stepTexts[this.camWaypointIndex].SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.NextWaypoint();
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00006A94 File Offset: 0x00004C94
	public void NextWaypoint()
	{
		base.StopCoroutine("NextWaypointAnim");
		this.nextStepText.text = "Next Step";
		this.camWaypointIndex++;
		if (this.camWaypointIndex >= this.camWaypoints.Length)
		{
			this.camWaypointIndex = 0;
			this.nextStepText.text = "Start";
		}
		if (this.exampleUIController.pBook != null && this.exampleUIController.pBook.GetBookState() != PBook.BookState.CLOSED)
		{
			this.exampleUIController.pBook.CloseBook();
		}
		this.exampleUIController.gameObject.SetActive(false);
		GameObject[] array = this.stepTexts;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		this.stepTexts[this.camWaypointIndex].SetActive(true);
		if (this.camWaypointIndex == 0)
		{
			this.overlayPowerBook = null;
		}
		if (this.camWaypointIndex == 1)
		{
			this.exampleUIController.pBook = this.demoBook1;
			this.exampleUIController.gameObject.SetActive(true);
		}
		if (this.camWaypointIndex == 3 && this.overlayPowerBook != null)
		{
			this.overlayPowerBook.transform.position = this.overlayBookOriginalPos;
			this.overlayPowerBook.transform.rotation = this.overlayBookOriginalRot;
			this.overlayCam.gameObject.SetActive(false);
		}
		base.StartCoroutine("NextWaypointAnim");
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00006C02 File Offset: 0x00004E02
	private IEnumerator NextWaypointAnim()
	{
		this.rotateAround = false;
		for (;;)
		{
			this.mainCam.transform.position = Vector3.Lerp(this.mainCam.transform.position, this.camWaypoints[this.camWaypointIndex].position, Time.deltaTime * 2.5f);
			this.mainCam.transform.rotation = Quaternion.Lerp(this.mainCam.transform.rotation, this.camWaypoints[this.camWaypointIndex].rotation, Time.deltaTime * 2f);
			if (Vector3.Distance(this.mainCam.transform.position, this.camWaypoints[this.camWaypointIndex].position) < 0.02f && Quaternion.Angle(this.mainCam.transform.rotation, this.camWaypoints[this.camWaypointIndex].rotation) < 0.5f)
			{
				break;
			}
			yield return null;
		}
		this.rotateAround = true;
		yield break;
	}

	// Token: 0x040000BC RID: 188
	public PBook demoBook1;

	// Token: 0x040000BD RID: 189
	public ExampleUIController exampleUIController;

	// Token: 0x040000BE RID: 190
	public Camera mainCam;

	// Token: 0x040000BF RID: 191
	public Camera overlayCam;

	// Token: 0x040000C0 RID: 192
	public Transform[] camWaypoints;

	// Token: 0x040000C1 RID: 193
	public int camWaypointIndex;

	// Token: 0x040000C2 RID: 194
	public GameObject[] stepTexts;

	// Token: 0x040000C3 RID: 195
	public Text nextStepText;

	// Token: 0x040000C4 RID: 196
	private PBook overlayPowerBook;

	// Token: 0x040000C5 RID: 197
	private Vector3 overlayBookOriginalPos;

	// Token: 0x040000C6 RID: 198
	private Quaternion overlayBookOriginalRot;

	// Token: 0x040000C7 RID: 199
	private bool rotateAround = true;
}
