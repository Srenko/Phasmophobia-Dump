using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
	// Token: 0x02000313 RID: 787
	[AddComponentMenu("VRTK/Scripts/Utilities/VRTK_PolicyList")]
	public class VRTK_PolicyList : MonoBehaviour
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x00090FA8 File Offset: 0x0008F1A8
		public virtual bool Find(GameObject obj)
		{
			if (this.operation == VRTK_PolicyList.OperationTypes.Ignore)
			{
				return this.TypeCheck(obj, true);
			}
			return this.TypeCheck(obj, false);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00090FC3 File Offset: 0x0008F1C3
		public static bool Check(GameObject obj, VRTK_PolicyList list)
		{
			return list != null && list.Find(obj);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00090FD8 File Offset: 0x0008F1D8
		protected virtual bool ScriptCheck(GameObject obj, bool returnState)
		{
			for (int i = 0; i < this.identifiers.Count; i++)
			{
				if (obj.GetComponent(this.identifiers[i]))
				{
					return returnState;
				}
			}
			return !returnState;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0009101A File Offset: 0x0008F21A
		protected virtual bool TagCheck(GameObject obj, bool returnState)
		{
			if (returnState)
			{
				return this.identifiers.Contains(obj.tag);
			}
			return !this.identifiers.Contains(obj.tag);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00091045 File Offset: 0x0008F245
		protected virtual bool LayerCheck(GameObject obj, bool returnState)
		{
			if (returnState)
			{
				return this.identifiers.Contains(LayerMask.LayerToName(obj.layer));
			}
			return !this.identifiers.Contains(LayerMask.LayerToName(obj.layer));
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0009107C File Offset: 0x0008F27C
		protected virtual bool TypeCheck(GameObject obj, bool returnState)
		{
			int num = 0;
			if ((this.checkType & VRTK_PolicyList.CheckTypes.Tag) != (VRTK_PolicyList.CheckTypes)0)
			{
				num++;
			}
			if ((this.checkType & VRTK_PolicyList.CheckTypes.Script) != (VRTK_PolicyList.CheckTypes)0)
			{
				num += 2;
			}
			if ((this.checkType & VRTK_PolicyList.CheckTypes.Layer) != (VRTK_PolicyList.CheckTypes)0)
			{
				num += 4;
			}
			switch (num)
			{
			case 1:
				return this.TagCheck(obj, returnState);
			case 2:
				return this.ScriptCheck(obj, returnState);
			case 3:
				if ((returnState && this.TagCheck(obj, returnState)) || (!returnState && !this.TagCheck(obj, returnState)))
				{
					return returnState;
				}
				if ((returnState && this.ScriptCheck(obj, returnState)) || (!returnState && !this.ScriptCheck(obj, returnState)))
				{
					return returnState;
				}
				break;
			case 4:
				return this.LayerCheck(obj, returnState);
			case 5:
				if ((returnState && this.TagCheck(obj, returnState)) || (!returnState && !this.TagCheck(obj, returnState)))
				{
					return returnState;
				}
				if ((returnState && this.LayerCheck(obj, returnState)) || (!returnState && !this.LayerCheck(obj, returnState)))
				{
					return returnState;
				}
				break;
			case 6:
				if ((returnState && this.ScriptCheck(obj, returnState)) || (!returnState && !this.ScriptCheck(obj, returnState)))
				{
					return returnState;
				}
				if ((returnState && this.LayerCheck(obj, returnState)) || (!returnState && !this.LayerCheck(obj, returnState)))
				{
					return returnState;
				}
				break;
			case 7:
				if ((returnState && this.TagCheck(obj, returnState)) || (!returnState && !this.TagCheck(obj, returnState)))
				{
					return returnState;
				}
				if ((returnState && this.ScriptCheck(obj, returnState)) || (!returnState && !this.ScriptCheck(obj, returnState)))
				{
					return returnState;
				}
				if ((returnState && this.LayerCheck(obj, returnState)) || (!returnState && !this.LayerCheck(obj, returnState)))
				{
					return returnState;
				}
				break;
			}
			return !returnState;
		}

		// Token: 0x04001640 RID: 5696
		[Tooltip("The operation to apply on the list of identifiers.")]
		public VRTK_PolicyList.OperationTypes operation;

		// Token: 0x04001641 RID: 5697
		[Tooltip("The element type on the game object to check against.")]
		public VRTK_PolicyList.CheckTypes checkType = VRTK_PolicyList.CheckTypes.Tag;

		// Token: 0x04001642 RID: 5698
		[Tooltip("A list of identifiers to check for against the given check type (either tag or script).")]
		public List<string> identifiers = new List<string>
		{
			""
		};

		// Token: 0x02000614 RID: 1556
		public enum OperationTypes
		{
			// Token: 0x040028A5 RID: 10405
			Ignore,
			// Token: 0x040028A6 RID: 10406
			Include
		}

		// Token: 0x02000615 RID: 1557
		public enum CheckTypes
		{
			// Token: 0x040028A8 RID: 10408
			Tag = 1,
			// Token: 0x040028A9 RID: 10409
			Script,
			// Token: 0x040028AA RID: 10410
			Layer = 4
		}
	}
}
