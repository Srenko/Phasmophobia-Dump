using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x020002B3 RID: 691
	public class VRTK_ControllerReference : IEquatable<VRTK_ControllerReference>
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x0007B15A File Offset: 0x0007935A
		public static VRTK_ControllerReference GetControllerReference(uint controllerIndex)
		{
			if (controllerIndex >= 4294967295U)
			{
				return null;
			}
			if (VRTK_ControllerReference.controllerReferences.ContainsKey(controllerIndex))
			{
				return VRTK_ControllerReference.controllerReferences[controllerIndex];
			}
			return new VRTK_ControllerReference(controllerIndex);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0007B184 File Offset: 0x00079384
		public static VRTK_ControllerReference GetControllerReference(GameObject controllerObject)
		{
			uint controllerIndex = VRTK_SDK_Bridge.GetControllerIndex(controllerObject);
			if (controllerIndex >= 4294967295U)
			{
				controllerIndex = VRTK_SDK_Bridge.GetControllerIndex(VRTK_ControllerReference.GetValidObjectFromHand(VRTK_SDK_Bridge.GetControllerModelHand(controllerObject)));
			}
			if (VRTK_ControllerReference.controllerReferences.ContainsKey(controllerIndex))
			{
				return VRTK_ControllerReference.controllerReferences[controllerIndex];
			}
			return new VRTK_ControllerReference(controllerIndex);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0007B1CC File Offset: 0x000793CC
		public static VRTK_ControllerReference GetControllerReference(SDK_BaseController.ControllerHand controllerHand)
		{
			GameObject validObjectFromHand = VRTK_ControllerReference.GetValidObjectFromHand(controllerHand);
			uint controllerIndex = VRTK_SDK_Bridge.GetControllerIndex(validObjectFromHand);
			if (VRTK_ControllerReference.controllerReferences.ContainsKey(controllerIndex))
			{
				return VRTK_ControllerReference.controllerReferences[controllerIndex];
			}
			return new VRTK_ControllerReference(validObjectFromHand);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0007B206 File Offset: 0x00079406
		public static bool IsValid(VRTK_ControllerReference controllerReference)
		{
			return controllerReference != null && controllerReference.IsValid();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0007B213 File Offset: 0x00079413
		public static uint GetRealIndex(VRTK_ControllerReference controllerReference)
		{
			if (!VRTK_ControllerReference.IsValid(controllerReference))
			{
				return uint.MaxValue;
			}
			return controllerReference.index;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0007B225 File Offset: 0x00079425
		public VRTK_ControllerReference(uint controllerIndex)
		{
			if (VRTK_SDK_Bridge.GetControllerByIndex(controllerIndex, true) != null)
			{
				this.storedControllerIndex = controllerIndex;
				this.AddToCache();
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0007B250 File Offset: 0x00079450
		public VRTK_ControllerReference(GameObject controllerObject) : this(VRTK_ControllerReference.GetControllerHand(controllerObject))
		{
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0007B25E File Offset: 0x0007945E
		public VRTK_ControllerReference(SDK_BaseController.ControllerHand controllerHand)
		{
			this.storedControllerIndex = VRTK_SDK_Bridge.GetControllerIndex(VRTK_ControllerReference.GetValidObjectFromHand(controllerHand));
			this.AddToCache();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0007B284 File Offset: 0x00079484
		public uint index
		{
			get
			{
				return this.storedControllerIndex;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0007B28C File Offset: 0x0007948C
		public GameObject scriptAlias
		{
			get
			{
				return VRTK_SDK_Bridge.GetControllerByIndex(this.storedControllerIndex, false);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0007B29A File Offset: 0x0007949A
		public GameObject actual
		{
			get
			{
				return VRTK_SDK_Bridge.GetControllerByIndex(this.storedControllerIndex, true);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0007B2A8 File Offset: 0x000794A8
		public GameObject model
		{
			get
			{
				return VRTK_SDK_Bridge.GetControllerModel(this.GetValidObjectFromIndex());
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0007B2B5 File Offset: 0x000794B5
		public SDK_BaseController.ControllerHand hand
		{
			get
			{
				return VRTK_ControllerReference.GetControllerHand(this.GetValidObjectFromIndex());
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0007B2C2 File Offset: 0x000794C2
		public bool IsValid()
		{
			return this.index < uint.MaxValue;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0007B2D0 File Offset: 0x000794D0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				" --> INDEX[",
				this.index,
				"] - ACTUAL[",
				this.actual,
				"] - SCRIPT_ALIAS[",
				this.scriptAlias,
				"] - MODEL[",
				this.model,
				"] - HAND[",
				this.hand,
				"]"
			});
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0007B35C File Offset: 0x0007955C
		public override int GetHashCode()
		{
			return (int)this.index;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0007B364 File Offset: 0x00079564
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			VRTK_ControllerReference vrtk_ControllerReference = obj as VRTK_ControllerReference;
			return vrtk_ControllerReference != null && this.Equals(vrtk_ControllerReference);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0007B38C File Offset: 0x0007958C
		public bool Equals(VRTK_ControllerReference other)
		{
			return other != null && this.index.Equals(other.index);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0007B3B4 File Offset: 0x000795B4
		protected virtual GameObject GetValidObjectFromIndex()
		{
			GameObject controllerByIndex = VRTK_SDK_Bridge.GetControllerByIndex(this.storedControllerIndex, false);
			if (!(controllerByIndex == null))
			{
				return controllerByIndex;
			}
			return VRTK_SDK_Bridge.GetControllerByIndex(this.storedControllerIndex, true);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0007B3E8 File Offset: 0x000795E8
		protected virtual void AddToCache()
		{
			if (this.IsValid() && VRTK_ControllerReference.controllerReferences.ContainsKey(this.storedControllerIndex))
			{
				VRTK_ControllerReference.controllerReferences.Remove(this.storedControllerIndex);
			}
			if (this.IsValid() && !VRTK_ControllerReference.controllerReferences.ContainsKey(this.storedControllerIndex))
			{
				VRTK_ControllerReference.controllerReferences.Add(this.storedControllerIndex, this);
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0007B44C File Offset: 0x0007964C
		private static GameObject GetValidObjectFromHand(SDK_BaseController.ControllerHand controllerHand)
		{
			if (controllerHand != SDK_BaseController.ControllerHand.Left)
			{
				if (controllerHand != SDK_BaseController.ControllerHand.Right)
				{
					return null;
				}
				if (!VRTK_SDK_Bridge.GetControllerRightHand(false))
				{
					return VRTK_SDK_Bridge.GetControllerRightHand(true);
				}
				return VRTK_SDK_Bridge.GetControllerRightHand(false);
			}
			else
			{
				if (!VRTK_SDK_Bridge.GetControllerLeftHand(false))
				{
					return VRTK_SDK_Bridge.GetControllerLeftHand(true);
				}
				return VRTK_SDK_Bridge.GetControllerLeftHand(false);
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0007B49A File Offset: 0x0007969A
		private static SDK_BaseController.ControllerHand GetControllerHand(GameObject controllerObject)
		{
			if (VRTK_SDK_Bridge.IsControllerLeftHand(controllerObject, false) || VRTK_SDK_Bridge.IsControllerLeftHand(controllerObject, true))
			{
				return SDK_BaseController.ControllerHand.Left;
			}
			if (VRTK_SDK_Bridge.IsControllerRightHand(controllerObject, false) || VRTK_SDK_Bridge.IsControllerRightHand(controllerObject, true))
			{
				return SDK_BaseController.ControllerHand.Right;
			}
			return VRTK_SDK_Bridge.GetControllerModelHand(controllerObject);
		}

		// Token: 0x040012DF RID: 4831
		public static Dictionary<uint, VRTK_ControllerReference> controllerReferences = new Dictionary<uint, VRTK_ControllerReference>();

		// Token: 0x040012E0 RID: 4832
		protected uint storedControllerIndex = uint.MaxValue;
	}
}
