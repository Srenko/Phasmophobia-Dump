using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000412 RID: 1042
	[RequireComponent(typeof(Interactable))]
	public class CircularDrive : MonoBehaviour
	{
		// Token: 0x0600202C RID: 8236 RVA: 0x0009E8CC File Offset: 0x0009CACC
		private void Freeze(Hand hand)
		{
			this.frozen = true;
			this.frozenAngle = this.outAngle;
			this.frozenHandWorldPos = hand.hoverSphereTransform.position;
			this.frozenSqDistanceMinMaxThreshold.x = this.frozenDistanceMinMaxThreshold.x * this.frozenDistanceMinMaxThreshold.x;
			this.frozenSqDistanceMinMaxThreshold.y = this.frozenDistanceMinMaxThreshold.y * this.frozenDistanceMinMaxThreshold.y;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0009E941 File Offset: 0x0009CB41
		private void UnFreeze()
		{
			this.frozen = false;
			this.frozenHandWorldPos.Set(0f, 0f, 0f);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0009E964 File Offset: 0x0009CB64
		private void Start()
		{
			if (this.childCollider == null)
			{
				this.childCollider = base.GetComponentInChildren<Collider>();
			}
			if (this.linearMapping == null)
			{
				this.linearMapping = base.GetComponent<LinearMapping>();
			}
			if (this.linearMapping == null)
			{
				this.linearMapping = base.gameObject.AddComponent<LinearMapping>();
			}
			this.worldPlaneNormal = new Vector3(0f, 0f, 0f);
			this.worldPlaneNormal[(int)this.axisOfRotation] = 1f;
			this.localPlaneNormal = this.worldPlaneNormal;
			if (base.transform.parent)
			{
				this.worldPlaneNormal = base.transform.parent.localToWorldMatrix.MultiplyVector(this.worldPlaneNormal).normalized;
			}
			if (this.limited)
			{
				this.start = Quaternion.identity;
				this.outAngle = base.transform.localEulerAngles[(int)this.axisOfRotation];
				if (this.forceStart)
				{
					this.outAngle = Mathf.Clamp(this.startAngle, this.minAngle, this.maxAngle);
				}
			}
			else
			{
				this.start = Quaternion.AngleAxis(base.transform.localEulerAngles[(int)this.axisOfRotation], this.localPlaneNormal);
				this.outAngle = 0f;
			}
			if (this.debugText)
			{
				this.debugText.alignment = TextAlignment.Left;
				this.debugText.anchor = TextAnchor.UpperLeft;
			}
			this.UpdateAll();
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0009EAF7 File Offset: 0x0009CCF7
		private void OnDisable()
		{
			if (this.handHoverLocked)
			{
				ControllerButtonHints.HideButtonHint(this.handHoverLocked, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
				this.handHoverLocked.HoverUnlock(base.GetComponent<Interactable>());
				this.handHoverLocked = null;
			}
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0009EB34 File Offset: 0x0009CD34
		private IEnumerator HapticPulses(SteamVR_Controller.Device controller, float flMagnitude, int nCount)
		{
			if (controller != null)
			{
				int nRangeMax = (int)Util.RemapNumberClamped(flMagnitude, 0f, 1f, 100f, 900f);
				nCount = Mathf.Clamp(nCount, 1, 10);
				ushort i = 0;
				while ((int)i < nCount)
				{
					ushort durationMicroSec = (ushort)Random.Range(100, nRangeMax);
					controller.TriggerHapticPulse(durationMicroSec, EVRButtonId.k_EButton_Axis0);
					yield return new WaitForSeconds(0.01f);
					ushort num = i + 1;
					i = num;
				}
			}
			yield break;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0009EB51 File Offset: 0x0009CD51
		private void OnHandHoverBegin(Hand hand)
		{
			ControllerButtonHints.ShowButtonHint(hand, new EVRButtonId[]
			{
				EVRButtonId.k_EButton_Axis1
			});
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0009EB64 File Offset: 0x0009CD64
		private void OnHandHoverEnd(Hand hand)
		{
			ControllerButtonHints.HideButtonHint(hand, new EVRButtonId[]
			{
				EVRButtonId.k_EButton_Axis1
			});
			if (this.driving && hand.GetStandardInteractionButton())
			{
				base.StartCoroutine(this.HapticPulses(hand.controller, 1f, 10));
			}
			this.driving = false;
			this.handHoverLocked = null;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0009EBBC File Offset: 0x0009CDBC
		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				this.lastHandProjected = this.ComputeToTransformProjected(hand.hoverSphereTransform);
				if (this.hoverLock)
				{
					hand.HoverLock(base.GetComponent<Interactable>());
					this.handHoverLocked = hand;
				}
				this.driving = true;
				this.ComputeAngle(hand);
				this.UpdateAll();
				ControllerButtonHints.HideButtonHint(hand, new EVRButtonId[]
				{
					EVRButtonId.k_EButton_Axis1
				});
				return;
			}
			if (hand.GetStandardInteractionButtonUp())
			{
				if (this.hoverLock)
				{
					hand.HoverUnlock(base.GetComponent<Interactable>());
					this.handHoverLocked = null;
					return;
				}
			}
			else if (this.driving && hand.GetStandardInteractionButton() && hand.hoveringInteractable == base.GetComponent<Interactable>())
			{
				this.ComputeAngle(hand);
				this.UpdateAll();
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0009EC78 File Offset: 0x0009CE78
		private Vector3 ComputeToTransformProjected(Transform xForm)
		{
			Vector3 normalized = (xForm.position - base.transform.position).normalized;
			Vector3 normalized2 = new Vector3(0f, 0f, 0f);
			if (normalized.sqrMagnitude > 0f)
			{
				normalized2 = Vector3.ProjectOnPlane(normalized, this.worldPlaneNormal).normalized;
			}
			else
			{
				Debug.LogFormat("The collider needs to be a minimum distance away from the CircularDrive GameObject {0}", new object[]
				{
					base.gameObject.ToString()
				});
			}
			if (this.debugPath && this.dbgPathLimit > 0)
			{
				this.DrawDebugPath(xForm, normalized2);
			}
			return normalized2;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0009ED18 File Offset: 0x0009CF18
		private void DrawDebugPath(Transform xForm, Vector3 toTransformProjected)
		{
			if (this.dbgObjectCount == 0)
			{
				this.dbgObjectsParent = new GameObject("Circular Drive Debug");
				this.dbgHandObjects = new GameObject[this.dbgPathLimit];
				this.dbgProjObjects = new GameObject[this.dbgPathLimit];
				this.dbgObjectCount = this.dbgPathLimit;
				this.dbgObjectIndex = 0;
			}
			GameObject gameObject;
			if (this.dbgHandObjects[this.dbgObjectIndex])
			{
				gameObject = this.dbgHandObjects[this.dbgObjectIndex];
			}
			else
			{
				gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				gameObject.transform.SetParent(this.dbgObjectsParent.transform);
				this.dbgHandObjects[this.dbgObjectIndex] = gameObject;
			}
			gameObject.name = string.Format("actual_{0}", (int)((1f - this.red.r) * 10f));
			gameObject.transform.position = xForm.position;
			gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			gameObject.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
			gameObject.gameObject.GetComponent<Renderer>().material.color = this.red;
			if (this.red.r > 0.1f)
			{
				this.red.r = this.red.r - 0.1f;
			}
			else
			{
				this.red.r = 1f;
			}
			if (this.dbgProjObjects[this.dbgObjectIndex])
			{
				gameObject = this.dbgProjObjects[this.dbgObjectIndex];
			}
			else
			{
				gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				gameObject.transform.SetParent(this.dbgObjectsParent.transform);
				this.dbgProjObjects[this.dbgObjectIndex] = gameObject;
			}
			gameObject.name = string.Format("projed_{0}", (int)((1f - this.green.g) * 10f));
			gameObject.transform.position = base.transform.position + toTransformProjected * 0.25f;
			gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			gameObject.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
			gameObject.gameObject.GetComponent<Renderer>().material.color = this.green;
			if (this.green.g > 0.1f)
			{
				this.green.g = this.green.g - 0.1f;
			}
			else
			{
				this.green.g = 1f;
			}
			this.dbgObjectIndex = (this.dbgObjectIndex + 1) % this.dbgObjectCount;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0009EFDC File Offset: 0x0009D1DC
		private void UpdateLinearMapping()
		{
			if (this.limited)
			{
				this.linearMapping.value = (this.outAngle - this.minAngle) / (this.maxAngle - this.minAngle);
			}
			else
			{
				float num = this.outAngle / 360f;
				this.linearMapping.value = num - Mathf.Floor(num);
			}
			this.UpdateDebugText();
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0009F03F File Offset: 0x0009D23F
		private void UpdateGameObject()
		{
			if (this.rotateGameObject)
			{
				base.transform.localRotation = this.start * Quaternion.AngleAxis(this.outAngle, this.localPlaneNormal);
			}
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0009F070 File Offset: 0x0009D270
		private void UpdateDebugText()
		{
			if (this.debugText)
			{
				this.debugText.text = string.Format("Linear: {0}\nAngle:  {1}\n", this.linearMapping.value, this.outAngle);
			}
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0009F0AF File Offset: 0x0009D2AF
		private void UpdateAll()
		{
			this.UpdateLinearMapping();
			this.UpdateGameObject();
			this.UpdateDebugText();
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0009F0C4 File Offset: 0x0009D2C4
		private void ComputeAngle(Hand hand)
		{
			Vector3 vector = this.ComputeToTransformProjected(hand.hoverSphereTransform);
			if (!vector.Equals(this.lastHandProjected))
			{
				float num = Vector3.Angle(this.lastHandProjected, vector);
				if (num > 0f)
				{
					if (this.frozen)
					{
						float sqrMagnitude = (hand.hoverSphereTransform.position - this.frozenHandWorldPos).sqrMagnitude;
						if (sqrMagnitude > this.frozenSqDistanceMinMaxThreshold.x)
						{
							this.outAngle = this.frozenAngle + Random.Range(-1f, 1f);
							float num2 = Util.RemapNumberClamped(sqrMagnitude, this.frozenSqDistanceMinMaxThreshold.x, this.frozenSqDistanceMinMaxThreshold.y, 0f, 1f);
							if (num2 > 0f)
							{
								base.StartCoroutine(this.HapticPulses(hand.controller, num2, 10));
							}
							else
							{
								base.StartCoroutine(this.HapticPulses(hand.controller, 0.5f, 10));
							}
							if (sqrMagnitude >= this.frozenSqDistanceMinMaxThreshold.y)
							{
								this.onFrozenDistanceThreshold.Invoke();
								return;
							}
						}
					}
					else
					{
						Vector3 normalized = Vector3.Cross(this.lastHandProjected, vector).normalized;
						float num3 = Vector3.Dot(this.worldPlaneNormal, normalized);
						float num4 = num;
						if (num3 < 0f)
						{
							num4 = -num4;
						}
						if (this.limited)
						{
							float num5 = Mathf.Clamp(this.outAngle + num4, this.minAngle, this.maxAngle);
							if (this.outAngle == this.minAngle)
							{
								if (num5 > this.minAngle && num < this.minMaxAngularThreshold)
								{
									this.outAngle = num5;
									this.lastHandProjected = vector;
									return;
								}
							}
							else if (this.outAngle == this.maxAngle)
							{
								if (num5 < this.maxAngle && num < this.minMaxAngularThreshold)
								{
									this.outAngle = num5;
									this.lastHandProjected = vector;
									return;
								}
							}
							else if (num5 == this.minAngle)
							{
								this.outAngle = num5;
								this.lastHandProjected = vector;
								this.onMinAngle.Invoke();
								if (this.freezeOnMin)
								{
									this.Freeze(hand);
									return;
								}
							}
							else
							{
								if (num5 != this.maxAngle)
								{
									this.outAngle = num5;
									this.lastHandProjected = vector;
									return;
								}
								this.outAngle = num5;
								this.lastHandProjected = vector;
								this.onMaxAngle.Invoke();
								if (this.freezeOnMax)
								{
									this.Freeze(hand);
									return;
								}
							}
						}
						else
						{
							this.outAngle += num4;
							this.lastHandProjected = vector;
						}
					}
				}
			}
		}

		// Token: 0x04001DAD RID: 7597
		[Tooltip("The axis around which the circular drive will rotate in local space")]
		public CircularDrive.Axis_t axisOfRotation;

		// Token: 0x04001DAE RID: 7598
		[Tooltip("Child GameObject which has the Collider component to initiate interaction, only needs to be set if there is more than one Collider child")]
		public Collider childCollider;

		// Token: 0x04001DAF RID: 7599
		[Tooltip("A LinearMapping component to drive, if not specified one will be dynamically added to this GameObject")]
		public LinearMapping linearMapping;

		// Token: 0x04001DB0 RID: 7600
		[Tooltip("If true, the drive will stay manipulating as long as the button is held down, if false, it will stop if the controller moves out of the collider")]
		public bool hoverLock;

		// Token: 0x04001DB1 RID: 7601
		[Header("Limited Rotation")]
		[Tooltip("If true, the rotation will be limited to [minAngle, maxAngle], if false, the rotation is unlimited")]
		public bool limited;

		// Token: 0x04001DB2 RID: 7602
		public Vector2 frozenDistanceMinMaxThreshold = new Vector2(0.1f, 0.2f);

		// Token: 0x04001DB3 RID: 7603
		public UnityEvent onFrozenDistanceThreshold;

		// Token: 0x04001DB4 RID: 7604
		[Header("Limited Rotation Min")]
		[Tooltip("If limited is true, the specifies the lower limit, otherwise value is unused")]
		public float minAngle = -45f;

		// Token: 0x04001DB5 RID: 7605
		[Tooltip("If limited, set whether drive will freeze its angle when the min angle is reached")]
		public bool freezeOnMin;

		// Token: 0x04001DB6 RID: 7606
		[Tooltip("If limited, event invoked when minAngle is reached")]
		public UnityEvent onMinAngle;

		// Token: 0x04001DB7 RID: 7607
		[Header("Limited Rotation Max")]
		[Tooltip("If limited is true, the specifies the upper limit, otherwise value is unused")]
		public float maxAngle = 45f;

		// Token: 0x04001DB8 RID: 7608
		[Tooltip("If limited, set whether drive will freeze its angle when the max angle is reached")]
		public bool freezeOnMax;

		// Token: 0x04001DB9 RID: 7609
		[Tooltip("If limited, event invoked when maxAngle is reached")]
		public UnityEvent onMaxAngle;

		// Token: 0x04001DBA RID: 7610
		[Tooltip("If limited is true, this forces the starting angle to be startAngle, clamped to [minAngle, maxAngle]")]
		public bool forceStart;

		// Token: 0x04001DBB RID: 7611
		[Tooltip("If limited is true and forceStart is true, the starting angle will be this, clamped to [minAngle, maxAngle]")]
		public float startAngle;

		// Token: 0x04001DBC RID: 7612
		[Tooltip("If true, the transform of the GameObject this component is on will be rotated accordingly")]
		public bool rotateGameObject = true;

		// Token: 0x04001DBD RID: 7613
		[Tooltip("If true, the path of the Hand (red) and the projected value (green) will be drawn")]
		public bool debugPath;

		// Token: 0x04001DBE RID: 7614
		[Tooltip("If debugPath is true, this is the maximum number of GameObjects to create to draw the path")]
		public int dbgPathLimit = 50;

		// Token: 0x04001DBF RID: 7615
		[Tooltip("If not null, the TextMesh will display the linear value and the angular value of this circular drive")]
		public TextMesh debugText;

		// Token: 0x04001DC0 RID: 7616
		[Tooltip("The output angle value of the drive in degrees, unlimited will increase or decrease without bound, take the 360 modulus to find number of rotations")]
		public float outAngle;

		// Token: 0x04001DC1 RID: 7617
		private Quaternion start;

		// Token: 0x04001DC2 RID: 7618
		private Vector3 worldPlaneNormal = new Vector3(1f, 0f, 0f);

		// Token: 0x04001DC3 RID: 7619
		private Vector3 localPlaneNormal = new Vector3(1f, 0f, 0f);

		// Token: 0x04001DC4 RID: 7620
		private Vector3 lastHandProjected;

		// Token: 0x04001DC5 RID: 7621
		private Color red = new Color(1f, 0f, 0f);

		// Token: 0x04001DC6 RID: 7622
		private Color green = new Color(0f, 1f, 0f);

		// Token: 0x04001DC7 RID: 7623
		private GameObject[] dbgHandObjects;

		// Token: 0x04001DC8 RID: 7624
		private GameObject[] dbgProjObjects;

		// Token: 0x04001DC9 RID: 7625
		private GameObject dbgObjectsParent;

		// Token: 0x04001DCA RID: 7626
		private int dbgObjectCount;

		// Token: 0x04001DCB RID: 7627
		private int dbgObjectIndex;

		// Token: 0x04001DCC RID: 7628
		private bool driving;

		// Token: 0x04001DCD RID: 7629
		private float minMaxAngularThreshold = 1f;

		// Token: 0x04001DCE RID: 7630
		private bool frozen;

		// Token: 0x04001DCF RID: 7631
		private float frozenAngle;

		// Token: 0x04001DD0 RID: 7632
		private Vector3 frozenHandWorldPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04001DD1 RID: 7633
		private Vector2 frozenSqDistanceMinMaxThreshold = new Vector2(0f, 0f);

		// Token: 0x04001DD2 RID: 7634
		private Hand handHoverLocked;

		// Token: 0x0200076C RID: 1900
		public enum Axis_t
		{
			// Token: 0x040028FC RID: 10492
			XAxis,
			// Token: 0x040028FD RID: 10493
			YAxis,
			// Token: 0x040028FE RID: 10494
			ZAxis
		}
	}
}
