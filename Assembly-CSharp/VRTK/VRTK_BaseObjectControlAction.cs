using System;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002C3 RID: 707
	public abstract class VRTK_BaseObjectControlAction : MonoBehaviour
	{
		// Token: 0x06001771 RID: 6001
		protected abstract void Process(GameObject controlledGameObject, Transform directionDevice, Vector3 axisDirection, float axis, float deadzone, bool currentlyFalling, bool modifierActive);

		// Token: 0x06001772 RID: 6002 RVA: 0x00064607 File Offset: 0x00062807
		protected virtual void Awake()
		{
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0007D9E4 File Offset: 0x0007BBE4
		protected virtual void OnEnable()
		{
			this.playArea = VRTK_DeviceFinder.PlayAreaTransform();
			if (this.objectControlScript)
			{
				VRTK_BaseObjectControlAction.AxisListeners axisListeners = this.listenOnAxisChange;
				if (axisListeners == VRTK_BaseObjectControlAction.AxisListeners.XAxisChanged)
				{
					this.objectControlScript.XAxisChanged += this.AxisChanged;
					return;
				}
				if (axisListeners != VRTK_BaseObjectControlAction.AxisListeners.YAxisChanged)
				{
					return;
				}
				this.objectControlScript.YAxisChanged += this.AxisChanged;
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0007DA4C File Offset: 0x0007BC4C
		protected virtual void OnDisable()
		{
			if (this.objectControlScript)
			{
				VRTK_BaseObjectControlAction.AxisListeners axisListeners = this.listenOnAxisChange;
				if (axisListeners == VRTK_BaseObjectControlAction.AxisListeners.XAxisChanged)
				{
					this.objectControlScript.XAxisChanged -= this.AxisChanged;
					return;
				}
				if (axisListeners != VRTK_BaseObjectControlAction.AxisListeners.YAxisChanged)
				{
					return;
				}
				this.objectControlScript.YAxisChanged -= this.AxisChanged;
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0007DAA6 File Offset: 0x0007BCA6
		protected virtual void AxisChanged(object sender, ObjectControlEventArgs e)
		{
			this.Process(e.controlledGameObject, e.directionDevice, e.axisDirection, e.axis, e.deadzone, e.currentlyFalling, e.modifierActive);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0007DAD8 File Offset: 0x0007BCD8
		protected virtual void RotateAroundPlayer(GameObject controlledGameObject, float angle)
		{
			Vector3 objectCenter = this.GetObjectCenter(controlledGameObject.transform);
			Vector3 vector = controlledGameObject.transform.TransformPoint(objectCenter);
			controlledGameObject.transform.Rotate(Vector3.up, angle);
			vector -= controlledGameObject.transform.TransformPoint(objectCenter);
			controlledGameObject.transform.position += vector;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0007DB3A File Offset: 0x0007BD3A
		protected virtual void Blink(float blinkSpeed)
		{
			if (blinkSpeed > 0f)
			{
				VRTK_SDK_Bridge.HeadsetFade(Color.black, 0f, false);
				this.ReleaseBlink(blinkSpeed);
			}
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0007DB5B File Offset: 0x0007BD5B
		protected virtual void ReleaseBlink(float blinkSpeed)
		{
			VRTK_SDK_Bridge.HeadsetFade(Color.clear, blinkSpeed, false);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0007DB6C File Offset: 0x0007BD6C
		protected virtual Vector3 GetObjectCenter(Transform checkObject)
		{
			if (this.centerCollider == null || checkObject != this.controlledTransform)
			{
				this.controlledTransform = checkObject;
				if (checkObject == this.playArea)
				{
					CapsuleCollider componentInChildren = this.playArea.GetComponentInChildren<CapsuleCollider>();
					this.centerCollider = componentInChildren;
					if (componentInChildren != null)
					{
						this.colliderRadius = componentInChildren.radius;
						this.colliderHeight = componentInChildren.height;
						this.colliderCenter = componentInChildren.center;
					}
					else
					{
						VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
						{
							"PlayArea",
							"CapsuleCollider",
							"the same or child"
						}));
					}
				}
				else
				{
					this.centerCollider = checkObject.GetComponentInChildren<Collider>();
					if (this.centerCollider == null)
					{
						VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
						{
							"CheckObject",
							"Collider",
							"the same or child"
						}));
					}
					this.colliderRadius = 0.1f;
					this.colliderHeight = 0.1f;
				}
			}
			return this.colliderCenter;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0007DC7C File Offset: 0x0007BE7C
		protected virtual int GetAxisDirection(float axis)
		{
			int result = 0;
			if (axis < 0f)
			{
				result = -1;
			}
			else if (axis > 0f)
			{
				result = 1;
			}
			return result;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0007DCA4 File Offset: 0x0007BEA4
		protected virtual bool CanMove(VRTK_BodyPhysics givenBodyPhysics, Vector3 currentPosition, Vector3 proposedPosition)
		{
			if (givenBodyPhysics == null)
			{
				return true;
			}
			Vector3 normalized = (proposedPosition - currentPosition).normalized;
			float maxDistance = Vector3.Distance(currentPosition, proposedPosition);
			return !givenBodyPhysics.SweepCollision(normalized, maxDistance);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0007DCE0 File Offset: 0x0007BEE0
		protected virtual void CheckForPlayerBeforeRotation(GameObject controlledGameObject)
		{
			VRTK_PlayerObject component = controlledGameObject.GetComponent<VRTK_PlayerObject>();
			if (this.headsetTransform == null)
			{
				this.headsetTransform = VRTK_DeviceFinder.HeadsetTransform();
			}
			this.validPlayerObject = (component != null && component.objectType == VRTK_PlayerObject.ObjectTypes.CameraRig && this.headsetTransform != null);
			if (this.validPlayerObject)
			{
				this.playerHeadPositionBeforeRotation = this.headsetTransform.position;
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0007DD4D File Offset: 0x0007BF4D
		protected virtual void CheckForPlayerAfterRotation(GameObject controlledGameObject)
		{
			if (this.validPlayerObject)
			{
				controlledGameObject.transform.position += this.playerHeadPositionBeforeRotation - this.headsetTransform.position;
				this.validPlayerObject = false;
			}
		}

		// Token: 0x04001315 RID: 4885
		[Tooltip("The Object Control script to receive axis change events from.")]
		public VRTK_ObjectControl objectControlScript;

		// Token: 0x04001316 RID: 4886
		[Tooltip("Determines which Object Control Axis event to listen to.")]
		public VRTK_BaseObjectControlAction.AxisListeners listenOnAxisChange;

		// Token: 0x04001317 RID: 4887
		protected Collider centerCollider;

		// Token: 0x04001318 RID: 4888
		protected Vector3 colliderCenter = Vector3.zero;

		// Token: 0x04001319 RID: 4889
		protected float colliderRadius;

		// Token: 0x0400131A RID: 4890
		protected float colliderHeight;

		// Token: 0x0400131B RID: 4891
		protected Transform controlledTransform;

		// Token: 0x0400131C RID: 4892
		protected Transform playArea;

		// Token: 0x0400131D RID: 4893
		protected Vector3 playerHeadPositionBeforeRotation;

		// Token: 0x0400131E RID: 4894
		protected Transform headsetTransform;

		// Token: 0x0400131F RID: 4895
		protected bool validPlayerObject;

		// Token: 0x020005E3 RID: 1507
		public enum AxisListeners
		{
			// Token: 0x040027D6 RID: 10198
			XAxisChanged,
			// Token: 0x040027D7 RID: 10199
			YAxisChanged
		}
	}
}
