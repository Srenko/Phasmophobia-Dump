using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x0200024F RID: 591
	public class VRTK_IndependentRadialMenuController : VRTK_RadialMenuController
	{
		// Token: 0x06001140 RID: 4416 RVA: 0x00064FE4 File Offset: 0x000631E4
		public virtual void UpdateEventsManager()
		{
			VRTK_InteractableObject componentInParent = base.transform.GetComponentInParent<VRTK_InteractableObject>();
			if (componentInParent == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_NOT_INJECTED, new object[]
				{
					"VRTK_IndependentRadialMenuController",
					"VRTK_InteractableObject",
					"eventsManager",
					"the parent"
				}));
				return;
			}
			if (componentInParent != this.eventsManager)
			{
				if (this.eventsManager != null)
				{
					this.OnDisable();
				}
				this.eventsManager = componentInParent;
				this.OnEnable();
				Object.Destroy(this.menuCollider);
				this.Initialize();
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00065078 File Offset: 0x00063278
		protected override void Initialize()
		{
			if (this.eventsManager == null)
			{
				this.initialRotation = base.transform.localRotation;
				this.UpdateEventsManager();
				return;
			}
			this.interactingObjects = new List<GameObject>();
			this.collidingObjects = new List<GameObject>();
			if (this.disableCoroutine != null)
			{
				base.StopCoroutine(this.disableCoroutine);
				this.disableCoroutine = null;
			}
			this.isClicked = false;
			this.waitingToDisableCollider = false;
			this.counter = 2;
			if (base.transform.childCount == 0)
			{
				return;
			}
			float z = base.transform.GetChild(0).GetComponent<RectTransform>().rect.width / 2f * this.offsetMultiplier;
			base.transform.localPosition = new Vector3(0f, 0f, z);
			if (this.addMenuCollider)
			{
				base.gameObject.SetActive(false);
				base.transform.localScale = Vector3.one;
				Quaternion rotation = base.transform.rotation;
				base.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				SphereCollider sphereCollider = this.eventsManager.gameObject.AddComponent<SphereCollider>();
				sphereCollider.radius = base.transform.GetChild(0).GetComponent<RectTransform>().rect.width / 2f * this.colliderRadiusMultiplier * this.eventsManager.transform.InverseTransformVector(base.transform.GetChild(0).TransformVector(Vector3.one)).x;
				sphereCollider.center = this.eventsManager.transform.InverseTransformVector(base.transform.position - this.eventsManager.transform.position);
				sphereCollider.isTrigger = true;
				sphereCollider.enabled = false;
				this.menuCollider = sphereCollider;
				this.desiredColliderCenter = sphereCollider.center;
				base.transform.rotation = rotation;
			}
			if (!this.menu.isShown)
			{
				base.transform.localScale = Vector3.zero;
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00065297 File Offset: 0x00063497
		protected override void Awake()
		{
			this.menu = base.GetComponent<VRTK_RadialMenu>();
			VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000652B0 File Offset: 0x000634B0
		protected virtual void Start()
		{
			this.Initialize();
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000652B8 File Offset: 0x000634B8
		protected override void OnEnable()
		{
			if (this.eventsManager != null)
			{
				this.eventsManager.InteractableObjectUsed += this.ObjectClicked;
				this.eventsManager.InteractableObjectUnused += this.ObjectUnClicked;
				this.eventsManager.InteractableObjectTouched += this.ObjectTouched;
				this.eventsManager.InteractableObjectUntouched += this.ObjectUntouched;
				this.menu.FireHapticPulse += this.AttemptHapticPulse;
				return;
			}
			this.Initialize();
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00065354 File Offset: 0x00063554
		protected override void OnDisable()
		{
			if (this.eventsManager != null)
			{
				this.eventsManager.InteractableObjectUsed -= this.ObjectClicked;
				this.eventsManager.InteractableObjectUnused -= this.ObjectUnClicked;
				this.eventsManager.InteractableObjectTouched -= this.ObjectTouched;
				this.eventsManager.InteractableObjectUntouched -= this.ObjectUntouched;
				this.menu.FireHapticPulse -= this.AttemptHapticPulse;
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00063DD5 File Offset: 0x00061FD5
		protected virtual void OnDestroy()
		{
			VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000653E8 File Offset: 0x000635E8
		protected virtual void Update()
		{
			if (this.rotateTowards == null)
			{
				Transform transform = VRTK_DeviceFinder.HeadsetTransform();
				if (transform)
				{
					this.rotateTowards = transform.gameObject;
				}
				else
				{
					VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.COULD_NOT_FIND_OBJECT_FOR_ACTION, new object[]
					{
						"IndependentRadialMenu",
						"an object",
						"rotate towards"
					}));
				}
			}
			if (this.menu.isShown)
			{
				if (this.interactingObjects.Count > 0)
				{
					this.DoChangeAngle(this.CalculateAngle(this.interactingObjects[0]), this);
				}
				if (this.rotateTowards != null)
				{
					base.transform.rotation = Quaternion.LookRotation((this.rotateTowards.transform.position - base.transform.position) * -1f, Vector3.up) * this.initialRotation;
				}
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000654D4 File Offset: 0x000636D4
		protected virtual void FixedUpdate()
		{
			if (this.waitingToDisableCollider)
			{
				if (this.counter == 0)
				{
					this.menuCollider.enabled = false;
					this.waitingToDisableCollider = false;
					this.counter = 2;
					return;
				}
				this.counter--;
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0006550F File Offset: 0x0006370F
		protected override void AttemptHapticPulse(float strength)
		{
			if (this.interactingObjects.Count > 0)
			{
				VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(this.interactingObjects[0]), strength);
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00065536 File Offset: 0x00063736
		protected virtual void ObjectClicked(object sender, InteractableObjectEventArgs e)
		{
			this.DoClickButton(sender);
			this.isClicked = true;
			if (this.hideAfterExecution && !this.menu.executeOnUnclick)
			{
				this.ImmediatelyHideMenu(e);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00065564 File Offset: 0x00063764
		protected virtual void ObjectUnClicked(object sender, InteractableObjectEventArgs e)
		{
			this.DoUnClickButton(sender);
			this.isClicked = false;
			if ((this.hideAfterExecution || (this.collidingObjects.Count == 0 && this.menu.hideOnRelease)) && this.menu.executeOnUnclick)
			{
				this.ImmediatelyHideMenu(e);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000655B8 File Offset: 0x000637B8
		protected virtual void ObjectTouched(object sender, InteractableObjectEventArgs e)
		{
			this.DoShowMenu(this.CalculateAngle(e.interactingObject), sender);
			this.collidingObjects.Add(e.interactingObject);
			this.interactingObjects.Add(e.interactingObject);
			if (this.addMenuCollider && this.menuCollider != null)
			{
				this.SetColliderState(true, e);
				if (this.disableCoroutine != null)
				{
					base.StopCoroutine(this.disableCoroutine);
				}
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0006562C File Offset: 0x0006382C
		protected virtual void ObjectUntouched(object sender, InteractableObjectEventArgs e)
		{
			this.collidingObjects.Remove(e.interactingObject);
			if (((!this.menu.executeOnUnclick || !this.isClicked) && this.menu.hideOnRelease) || (Object)sender == this)
			{
				this.DoHideMenu(this.hideAfterExecution, sender);
				this.interactingObjects.Remove(e.interactingObject);
				if (this.addMenuCollider && this.menuCollider != null)
				{
					this.disableCoroutine = base.StartCoroutine(this.DelayedSetColliderEnabled(false, 0.25f, e));
				}
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000656CC File Offset: 0x000638CC
		protected virtual float CalculateAngle(GameObject interactingObject)
		{
			Vector3 vector = interactingObject.transform.position - base.transform.position;
			Vector3 a = base.transform.position + Vector3.ProjectOnPlane(vector, base.transform.forward);
			float num = this.AngleSigned(base.transform.right * -1f, a - base.transform.position, base.transform.forward);
			if (num < 0f)
			{
				num += 360f;
			}
			return num;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00065766 File Offset: 0x00063966
		protected virtual float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
		{
			return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00065787 File Offset: 0x00063987
		protected virtual void ImmediatelyHideMenu(InteractableObjectEventArgs e)
		{
			this.ObjectUntouched(this, e);
			if (this.disableCoroutine != null)
			{
				base.StopCoroutine(this.disableCoroutine);
			}
			this.SetColliderState(false, e);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000657B0 File Offset: 0x000639B0
		protected virtual void SetColliderState(bool state, InteractableObjectEventArgs e)
		{
			if (this.addMenuCollider && this.menuCollider != null)
			{
				if (state)
				{
					this.menuCollider.enabled = true;
					this.menuCollider.center = this.desiredColliderCenter;
					return;
				}
				bool flag = true;
				Collider[] components = this.eventsManager.GetComponents<Collider>();
				Collider[] array = e.interactingObject.GetComponent<VRTK_InteractTouch>().ControllerColliders();
				foreach (Collider collider in components)
				{
					if (collider != this.menuCollider)
					{
						Collider[] array3 = array;
						for (int j = 0; j < array3.Length; j++)
						{
							if (array3[j].bounds.Intersects(collider.bounds))
							{
								flag = false;
							}
						}
					}
				}
				if (flag)
				{
					this.menuCollider.center = new Vector3(1E+08f, 1E+08f, 1E+08f);
					this.waitingToDisableCollider = true;
					return;
				}
				this.menuCollider.enabled = false;
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000658A5 File Offset: 0x00063AA5
		protected virtual IEnumerator DelayedSetColliderEnabled(bool enabled, float delay, InteractableObjectEventArgs e)
		{
			yield return new WaitForSeconds(delay);
			this.SetColliderState(enabled, e);
			base.StopCoroutine("delayedSetColliderEnabled");
			yield break;
		}

		// Token: 0x04001019 RID: 4121
		[Tooltip("If the RadialMenu is the child of an object with VRTK_InteractableObject attached, this will be automatically obtained. It can also be manually set.")]
		public VRTK_InteractableObject eventsManager;

		// Token: 0x0400101A RID: 4122
		[Tooltip("Whether or not the script should dynamically add a SphereCollider to surround the menu.")]
		public bool addMenuCollider = true;

		// Token: 0x0400101B RID: 4123
		[Tooltip("This times the size of the RadialMenu is the size of the collider.")]
		[Range(0f, 10f)]
		public float colliderRadiusMultiplier = 1.2f;

		// Token: 0x0400101C RID: 4124
		[Tooltip("If true, after a button is clicked, the RadialMenu will hide.")]
		public bool hideAfterExecution = true;

		// Token: 0x0400101D RID: 4125
		[Tooltip("How far away from the object the menu should be placed, relative to the size of the RadialMenu.")]
		[Range(-10f, 10f)]
		public float offsetMultiplier = 1.1f;

		// Token: 0x0400101E RID: 4126
		[Tooltip("The object the RadialMenu should face towards. If left empty, it will automatically try to find the Headset Camera.")]
		public GameObject rotateTowards;

		// Token: 0x0400101F RID: 4127
		protected List<GameObject> interactingObjects;

		// Token: 0x04001020 RID: 4128
		protected List<GameObject> collidingObjects;

		// Token: 0x04001021 RID: 4129
		protected SphereCollider menuCollider;

		// Token: 0x04001022 RID: 4130
		protected Coroutine disableCoroutine;

		// Token: 0x04001023 RID: 4131
		protected Vector3 desiredColliderCenter;

		// Token: 0x04001024 RID: 4132
		protected Quaternion initialRotation;

		// Token: 0x04001025 RID: 4133
		protected bool isClicked;

		// Token: 0x04001026 RID: 4134
		protected bool waitingToDisableCollider;

		// Token: 0x04001027 RID: 4135
		protected int counter = 2;
	}
}
