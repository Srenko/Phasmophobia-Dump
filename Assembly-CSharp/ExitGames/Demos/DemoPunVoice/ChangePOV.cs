using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	// Token: 0x0200048E RID: 1166
	public class ChangePOV : MonoBehaviour
	{
		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06002451 RID: 9297 RVA: 0x000B1864 File Offset: 0x000AFA64
		// (remove) Token: 0x06002452 RID: 9298 RVA: 0x000B1898 File Offset: 0x000AFA98
		public static event ChangePOV.OnCameraChanged CameraChanged;

		// Token: 0x06002453 RID: 9299 RVA: 0x000B18CB File Offset: 0x000AFACB
		private void OnEnable()
		{
			CharacterInstantiation.CharacterInstantiated += this.OnCharacterInstantiated;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000B18DE File Offset: 0x000AFADE
		private void OnDisable()
		{
			CharacterInstantiation.CharacterInstantiated -= this.OnCharacterInstantiated;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000B18F4 File Offset: 0x000AFAF4
		private void Start()
		{
			this.defaultCamera = Camera.main;
			this.initialCameraPosition = new Vector3(this.defaultCamera.transform.position.x, this.defaultCamera.transform.position.y, this.defaultCamera.transform.position.z);
			this.initialCameraRotation = new Quaternion(this.defaultCamera.transform.rotation.x, this.defaultCamera.transform.rotation.y, this.defaultCamera.transform.rotation.z, this.defaultCamera.transform.rotation.w);
			this.FirstPersonCamActivator.onClick.AddListener(new UnityAction(this.FirstPersonMode));
			this.ThirdPersonCamActivator.onClick.AddListener(new UnityAction(this.ThirdPersonMode));
			this.OrthographicCamActivator.onClick.AddListener(new UnityAction(this.OrthographicMode));
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000B1A0C File Offset: 0x000AFC0C
		private void OnCharacterInstantiated(GameObject character)
		{
			this.firstPersonController = character.GetComponent<FirstPersonController>();
			this.firstPersonController.enabled = false;
			this.thirdPersonController = character.GetComponent<ThirdPersonController>();
			this.thirdPersonController.enabled = false;
			this.orthographicController = character.GetComponent<OrthographicController>();
			this.ButtonsHolder.SetActive(true);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000B1A64 File Offset: 0x000AFC64
		private void OnLeftRoom()
		{
			this.defaultCamera.gameObject.SetActive(true);
			this.FirstPersonCamActivator.interactable = true;
			this.ThirdPersonCamActivator.interactable = true;
			this.OrthographicCamActivator.interactable = false;
			this.defaultCamera.transform.position = this.initialCameraPosition;
			this.defaultCamera.transform.rotation = this.initialCameraRotation;
			this.ButtonsHolder.SetActive(false);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000B1ADE File Offset: 0x000AFCDE
		private void FirstPersonMode()
		{
			this.ToggleMode(this.firstPersonController);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000B1AEC File Offset: 0x000AFCEC
		private void ThirdPersonMode()
		{
			this.ToggleMode(this.thirdPersonController);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000B1AFA File Offset: 0x000AFCFA
		private void OrthographicMode()
		{
			this.ToggleMode(this.orthographicController);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000B1B08 File Offset: 0x000AFD08
		private void ToggleMode(BaseController controller)
		{
			if (controller == null)
			{
				return;
			}
			if (controller.ControllerCamera == null)
			{
				return;
			}
			controller.ControllerCamera.gameObject.SetActive(true);
			controller.enabled = true;
			this.FirstPersonCamActivator.interactable = !(controller == this.firstPersonController);
			this.ThirdPersonCamActivator.interactable = !(controller == this.thirdPersonController);
			this.OrthographicCamActivator.interactable = !(controller == this.orthographicController);
			this.BroadcastChange(controller.ControllerCamera);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000B1BA0 File Offset: 0x000AFDA0
		private void BroadcastChange(Camera camera)
		{
			if (camera == null)
			{
				return;
			}
			if (ChangePOV.CameraChanged != null)
			{
				ChangePOV.CameraChanged(camera);
			}
		}

		// Token: 0x0400218F RID: 8591
		private FirstPersonController firstPersonController;

		// Token: 0x04002190 RID: 8592
		private ThirdPersonController thirdPersonController;

		// Token: 0x04002191 RID: 8593
		private OrthographicController orthographicController;

		// Token: 0x04002192 RID: 8594
		private Vector3 initialCameraPosition;

		// Token: 0x04002193 RID: 8595
		private Quaternion initialCameraRotation;

		// Token: 0x04002194 RID: 8596
		private Camera defaultCamera;

		// Token: 0x04002195 RID: 8597
		[SerializeField]
		private GameObject ButtonsHolder;

		// Token: 0x04002196 RID: 8598
		[SerializeField]
		private Button FirstPersonCamActivator;

		// Token: 0x04002197 RID: 8599
		[SerializeField]
		private Button ThirdPersonCamActivator;

		// Token: 0x04002198 RID: 8600
		[SerializeField]
		private Button OrthographicCamActivator;

		// Token: 0x02000799 RID: 1945
		// (Invoke) Token: 0x06003030 RID: 12336
		public delegate void OnCameraChanged(Camera newCamera);
	}
}
