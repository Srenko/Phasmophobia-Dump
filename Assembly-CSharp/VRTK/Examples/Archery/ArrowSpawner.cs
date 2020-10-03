using System;
using UnityEngine;

namespace VRTK.Examples.Archery
{
	// Token: 0x0200037E RID: 894
	public class ArrowSpawner : MonoBehaviour
	{
		// Token: 0x06001EBD RID: 7869 RVA: 0x0009C180 File Offset: 0x0009A380
		private void Start()
		{
			this.spawnDelayTimer = 0f;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0009C190 File Offset: 0x0009A390
		private void OnTriggerStay(Collider collider)
		{
			VRTK_InteractGrab vrtk_InteractGrab = collider.gameObject.GetComponent<VRTK_InteractGrab>() ? collider.gameObject.GetComponent<VRTK_InteractGrab>() : collider.gameObject.GetComponentInParent<VRTK_InteractGrab>();
			if (this.CanGrab(vrtk_InteractGrab) && this.NoArrowNotched(vrtk_InteractGrab.gameObject) && Time.time >= this.spawnDelayTimer)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.arrowPrefab);
				gameObject.name = "ArrowClone";
				vrtk_InteractGrab.GetComponent<VRTK_InteractTouch>().ForceTouch(gameObject);
				vrtk_InteractGrab.AttemptGrab();
				this.spawnDelayTimer = Time.time + this.spawnDelay;
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0009C227 File Offset: 0x0009A427
		private bool CanGrab(VRTK_InteractGrab grabbingController)
		{
			return grabbingController && grabbingController.GetGrabbedObject() == null && grabbingController.IsGrabButtonPressed();
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0009C248 File Offset: 0x0009A448
		private bool NoArrowNotched(GameObject controller)
		{
			if (VRTK_DeviceFinder.IsControllerLeftHand(controller))
			{
				GameObject controllerRightHand = VRTK_DeviceFinder.GetControllerRightHand(true);
				this.bow = controllerRightHand.GetComponentInChildren<BowAim>();
				if (this.bow == null)
				{
					this.bow = VRTK_DeviceFinder.GetModelAliasController(controllerRightHand).GetComponentInChildren<BowAim>();
				}
			}
			else if (VRTK_DeviceFinder.IsControllerRightHand(controller))
			{
				GameObject controllerLeftHand = VRTK_DeviceFinder.GetControllerLeftHand(true);
				this.bow = controllerLeftHand.GetComponentInChildren<BowAim>();
				if (this.bow == null)
				{
					this.bow = VRTK_DeviceFinder.GetModelAliasController(controllerLeftHand).GetComponentInChildren<BowAim>();
				}
			}
			return this.bow == null || !this.bow.HasArrow();
		}

		// Token: 0x040017F2 RID: 6130
		public GameObject arrowPrefab;

		// Token: 0x040017F3 RID: 6131
		public float spawnDelay = 1f;

		// Token: 0x040017F4 RID: 6132
		private float spawnDelayTimer;

		// Token: 0x040017F5 RID: 6133
		private BowAim bow;
	}
}
