using System;
using UnityEngine;
using VRTK.GrabAttachMechanics;

namespace VRTK
{
	// Token: 0x020002D5 RID: 725
	[RequireComponent(typeof(VRTK_BodyPhysics))]
	[AddComponentMenu("VRTK/Scripts/Locomotion/VRTK_PlayerClimb")]
	public class VRTK_PlayerClimb : MonoBehaviour
	{
		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06001814 RID: 6164 RVA: 0x00080378 File Offset: 0x0007E578
		// (remove) Token: 0x06001815 RID: 6165 RVA: 0x000803B0 File Offset: 0x0007E5B0
		public event PlayerClimbEventHandler PlayerClimbStarted;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06001816 RID: 6166 RVA: 0x000803E8 File Offset: 0x0007E5E8
		// (remove) Token: 0x06001817 RID: 6167 RVA: 0x00080420 File Offset: 0x0007E620
		public event PlayerClimbEventHandler PlayerClimbEnded;

		// Token: 0x06001818 RID: 6168 RVA: 0x00080458 File Offset: 0x0007E658
		protected virtual void Awake()
		{
			this.bodyPhysics = ((this.bodyPhysics != null) ? this.bodyPhysics : base.GetComponentInChildren<VRTK_BodyPhysics>());
			this.teleporter = ((this.teleporter != null) ? this.teleporter : base.GetComponentInChildren<VRTK_BasicTeleport>());
			this.headsetCollision = ((this.headsetCollision != null) ? this.headsetCollision : base.GetComponentInChildren<VRTK_HeadsetCollision>());
			this.positionRewind = ((this.positionRewind != null) ? this.positionRewind : base.GetComponentInChildren<VRTK_PositionRewind>());
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000804F8 File Offset: 0x0007E6F8
		protected virtual void OnEnable()
		{
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			this.InitListeners(true);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0008050C File Offset: 0x0007E70C
		protected virtual void OnDisable()
		{
			this.Ungrab(false, null, this.climbingObject);
			this.InitListeners(false);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00080524 File Offset: 0x0007E724
		protected virtual void Update()
		{
			if (this.isClimbing)
			{
				Vector3 b = this.GetScaledLocalPosition(this.grabbingController.transform) - this.startControllerScaledLocalPosition;
				Vector3 vector = this.climbingObject.transform.TransformPoint(this.startGrabPointLocalPosition);
				this.playArea.position = vector + this.startPlayAreaWorldOffset - b;
				if (this.useGrabbedObjectRotation)
				{
					Vector3 vector2 = this.climbingObjectLastRotation * Vector3.forward;
					Vector3 vector3 = this.climbingObject.transform.rotation * Vector3.forward;
					Vector3 axis = Vector3.Cross(vector2, vector3);
					float angle = Vector3.Angle(vector2, vector3);
					this.playArea.RotateAround(vector, axis, angle);
					this.climbingObjectLastRotation = this.climbingObject.transform.rotation;
				}
				if (this.positionRewind != null && !this.IsHeadsetColliding())
				{
					this.positionRewind.SetLastGoodPosition();
				}
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00080616 File Offset: 0x0007E816
		protected virtual void OnPlayerClimbStarted(PlayerClimbEventArgs e)
		{
			if (this.PlayerClimbStarted != null)
			{
				this.PlayerClimbStarted(this, e);
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0008062D File Offset: 0x0007E82D
		protected virtual void OnPlayerClimbEnded(PlayerClimbEventArgs e)
		{
			if (this.PlayerClimbEnded != null)
			{
				this.PlayerClimbEnded(this, e);
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00080644 File Offset: 0x0007E844
		protected virtual PlayerClimbEventArgs SetPlayerClimbEvent(VRTK_ControllerReference controllerReference, GameObject target)
		{
			PlayerClimbEventArgs result;
			result.controllerIndex = VRTK_ControllerReference.GetRealIndex(controllerReference);
			result.controllerReference = controllerReference;
			result.target = target;
			return result;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0008066F File Offset: 0x0007E86F
		protected virtual void InitListeners(bool state)
		{
			this.InitControllerListeners(VRTK_DeviceFinder.GetControllerLeftHand(false), state);
			this.InitControllerListeners(VRTK_DeviceFinder.GetControllerRightHand(false), state);
			this.InitTeleportListener(state);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00080694 File Offset: 0x0007E894
		protected virtual void InitTeleportListener(bool state)
		{
			if (this.teleporter != null)
			{
				if (state)
				{
					this.teleporter.Teleporting += this.OnTeleport;
					return;
				}
				this.teleporter.Teleporting -= this.OnTeleport;
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000806E3 File Offset: 0x0007E8E3
		protected virtual void OnTeleport(object sender, DestinationMarkerEventArgs e)
		{
			if (this.isClimbing)
			{
				this.Ungrab(false, e.controllerReference, e.target.gameObject);
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00080708 File Offset: 0x0007E908
		protected virtual Vector3 GetScaledLocalPosition(Transform objTransform)
		{
			if (this.usePlayerScale)
			{
				return this.playArea.localRotation * Vector3.Scale(objTransform.localPosition, this.playArea.localScale);
			}
			return this.playArea.localRotation * objTransform.localPosition;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0008075C File Offset: 0x0007E95C
		protected virtual void OnGrabObject(object sender, ObjectInteractEventArgs e)
		{
			if (this.IsClimbableObject(e.target))
			{
				GameObject actualController = VRTK_DeviceFinder.GetActualController(((VRTK_InteractGrab)sender).gameObject);
				this.Grab(actualController, e.controllerReference, e.target);
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0008079C File Offset: 0x0007E99C
		protected virtual void OnUngrabObject(object sender, ObjectInteractEventArgs e)
		{
			GameObject actualController = VRTK_DeviceFinder.GetActualController(((VRTK_InteractGrab)sender).gameObject);
			if (e.target && this.IsClimbableObject(e.target) && this.IsActiveClimbingController(actualController))
			{
				this.Ungrab(true, e.controllerReference, e.target);
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000807F4 File Offset: 0x0007E9F4
		protected virtual void Grab(GameObject currentGrabbingController, VRTK_ControllerReference controllerReference, GameObject target)
		{
			this.bodyPhysics.ResetFalling();
			this.bodyPhysics.TogglePreventSnapToFloor(true);
			this.bodyPhysics.enableBodyCollisions = false;
			this.bodyPhysics.ToggleOnGround(false);
			this.isClimbing = true;
			this.climbingObject = target;
			this.grabbingController = currentGrabbingController;
			this.startControllerScaledLocalPosition = this.GetScaledLocalPosition(this.grabbingController.transform);
			this.startGrabPointLocalPosition = this.climbingObject.transform.InverseTransformPoint(this.grabbingController.transform.position);
			this.startPlayAreaWorldOffset = this.playArea.transform.position - this.grabbingController.transform.position;
			this.climbingObjectLastRotation = this.climbingObject.transform.rotation;
			this.useGrabbedObjectRotation = this.climbingObject.GetComponent<VRTK_ClimbableGrabAttach>().useObjectRotation;
			this.OnPlayerClimbStarted(this.SetPlayerClimbEvent(controllerReference, this.climbingObject));
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000808EC File Offset: 0x0007EAEC
		protected virtual void Ungrab(bool carryMomentum, VRTK_ControllerReference controllerReference, GameObject target)
		{
			this.isClimbing = false;
			if (this.positionRewind != null && this.IsHeadsetColliding())
			{
				this.positionRewind.RewindPosition();
			}
			if (this.IsBodyColliding() && !this.IsHeadsetColliding())
			{
				this.bodyPhysics.ForceSnapToFloor();
			}
			this.bodyPhysics.enableBodyCollisions = true;
			if (carryMomentum)
			{
				Vector3 vector = Vector3.zero;
				if (VRTK_ControllerReference.IsValid(controllerReference))
				{
					vector = -VRTK_DeviceFinder.GetControllerVelocity(controllerReference);
					if (this.usePlayerScale)
					{
						vector = this.playArea.TransformVector(vector);
					}
					else
					{
						vector = this.playArea.TransformDirection(vector);
					}
				}
				this.bodyPhysics.ApplyBodyVelocity(vector, true, true);
			}
			this.grabbingController = null;
			this.climbingObject = null;
			this.OnPlayerClimbEnded(this.SetPlayerClimbEvent(controllerReference, target));
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000809B3 File Offset: 0x0007EBB3
		protected virtual bool IsActiveClimbingController(GameObject controller)
		{
			return controller == this.grabbingController;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000809C4 File Offset: 0x0007EBC4
		protected virtual bool IsClimbableObject(GameObject obj)
		{
			VRTK_InteractableObject component = obj.GetComponent<VRTK_InteractableObject>();
			return component && component.grabAttachMechanicScript && component.grabAttachMechanicScript.IsClimbable();
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000809FC File Offset: 0x0007EBFC
		protected virtual void InitControllerListeners(GameObject controller, bool state)
		{
			if (controller)
			{
				VRTK_InteractGrab component = controller.GetComponent<VRTK_InteractGrab>();
				if (component)
				{
					if (state)
					{
						component.ControllerGrabInteractableObject += this.OnGrabObject;
						component.ControllerUngrabInteractableObject += this.OnUngrabObject;
						return;
					}
					component.ControllerGrabInteractableObject -= this.OnGrabObject;
					component.ControllerUngrabInteractableObject -= this.OnUngrabObject;
				}
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00080A70 File Offset: 0x0007EC70
		protected virtual bool IsBodyColliding()
		{
			return this.bodyPhysics != null && this.bodyPhysics.GetCurrentCollidingObject() != null;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00080A93 File Offset: 0x0007EC93
		protected virtual bool IsHeadsetColliding()
		{
			return this.headsetCollision != null && this.headsetCollision.IsColliding();
		}

		// Token: 0x040013A1 RID: 5025
		[Header("Climb Settings")]
		[Tooltip("Will scale movement up and down based on the player transform's scale.")]
		public bool usePlayerScale = true;

		// Token: 0x040013A2 RID: 5026
		[Header("Custom Settings")]
		[Tooltip("The VRTK Body Physics script to use for dealing with climbing and falling. If this is left blank then the script will need to be applied to the same GameObject.")]
		public VRTK_BodyPhysics bodyPhysics;

		// Token: 0x040013A3 RID: 5027
		[Tooltip("The VRTK Teleport script to use when snapping to nearest floor on release. If this is left blank then a Teleport script will need to be applied to the same GameObject.")]
		public VRTK_BasicTeleport teleporter;

		// Token: 0x040013A4 RID: 5028
		[Tooltip("The VRTK Headset Collision script to use for determining if the user is climbing inside a collidable object. If this is left blank then the script will need to be applied to the same GameObject.")]
		public VRTK_HeadsetCollision headsetCollision;

		// Token: 0x040013A5 RID: 5029
		[Tooltip("The VRTK Position Rewind script to use for dealing resetting invalid positions. If this is left blank then the script will need to be applied to the same GameObject.")]
		public VRTK_PositionRewind positionRewind;

		// Token: 0x040013A8 RID: 5032
		protected Transform playArea;

		// Token: 0x040013A9 RID: 5033
		protected Vector3 startControllerScaledLocalPosition;

		// Token: 0x040013AA RID: 5034
		protected Vector3 startGrabPointLocalPosition;

		// Token: 0x040013AB RID: 5035
		protected Vector3 startPlayAreaWorldOffset;

		// Token: 0x040013AC RID: 5036
		protected GameObject grabbingController;

		// Token: 0x040013AD RID: 5037
		protected GameObject climbingObject;

		// Token: 0x040013AE RID: 5038
		protected Quaternion climbingObjectLastRotation;

		// Token: 0x040013AF RID: 5039
		protected bool isClimbing;

		// Token: 0x040013B0 RID: 5040
		protected bool useGrabbedObjectRotation;
	}
}
