using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Examples.Tests
{
	// Token: 0x02000376 RID: 886
	public class VRTK_ControllerReference_Setup : VRTK_BaseTest
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x0009A554 File Offset: 0x00098754
		protected override void SetUp(string message)
		{
			base.SetUp(message);
			this.actualController = ((this.overrideActualController == null) ? VRTK_SDK_Bridge.GetControllerByHand(this.actualControllerHand, true) : this.overrideActualController);
			this.aliasController = ((this.overrideAliasController == null) ? VRTK_SDK_Bridge.GetControllerByHand(this.actualControllerHand, false) : this.overrideAliasController);
			this.modelController = ((this.overrideModelController == null) ? VRTK_SDK_Bridge.GetControllerModel(this.actualController) : this.overrideModelController);
			this.actualIndex = ((this.overrideActualIndex == uint.MaxValue) ? VRTK_SDK_Bridge.GetControllerIndex(this.actualController) : this.overrideActualIndex);
			this.testReference = null;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0009A608 File Offset: 0x00098808
		protected override void TearDown()
		{
			base.TearDown();
			this.testReference = null;
			this.actualController = null;
			this.aliasController = null;
			this.modelController = null;
			this.actualIndex = uint.MaxValue;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0009A633 File Offset: 0x00098833
		protected override void Test()
		{
			this.BeginTest("VRTK_ControllerReference", 1);
			this.TestNullState();
			this.TestStaticIndex();
			this.TestStaticActual();
			this.TestStaticAlias();
			this.TestStaticModel();
			this.TestStaticHand();
			this.TestStaticIsValid();
			this.TestStaticGetRealIndex();
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0009A674 File Offset: 0x00098874
		protected virtual List<SDK_BaseController.ControllerHand> GetOtherHands(SDK_BaseController.ControllerHand ignore)
		{
			List<SDK_BaseController.ControllerHand> list = new List<SDK_BaseController.ControllerHand>();
			if (ignore != SDK_BaseController.ControllerHand.Left)
			{
				list.Add(SDK_BaseController.ControllerHand.Left);
			}
			if (ignore != SDK_BaseController.ControllerHand.Right)
			{
				list.Add(SDK_BaseController.ControllerHand.Right);
			}
			if (ignore != SDK_BaseController.ControllerHand.None)
			{
				list.Add(SDK_BaseController.ControllerHand.None);
			}
			return list;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0009A6A8 File Offset: 0x000988A8
		protected virtual void TestNullState()
		{
			this.SetUp("Null State");
			this.Assert("testReference == null", this.testReference == null, "reference should be null by default", "");
			this.TearDown();
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0009A6DC File Offset: 0x000988DC
		protected virtual void TestStaticIndex()
		{
			this.SetUp("Static Index");
			this.testReference = VRTK_ControllerReference.GetControllerReference(this.actualIndex);
			this.Assert("testReference != null", this.testReference != null, "reference should not be null", this.testReference.ToString());
			this.Assert("testReference.index == actualIndex", this.testReference.index == this.actualIndex, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.index,
				"] does not match the actual [",
				this.actualIndex,
				"]"
			}), "");
			this.Assert("testReference.actual == actualController", this.testReference.actual == this.actualController, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.actual,
				"] does not match the actual [",
				this.actualController,
				"]"
			}), "");
			this.Assert("testReference.scriptAlias == aliasController", this.testReference.scriptAlias == this.aliasController, string.Concat(new object[]
			{
				"The reference script alias [",
				this.testReference.scriptAlias,
				"] does not match the actual [",
				this.aliasController,
				"]"
			}), "");
			this.Assert("testReference.model == modelController", this.testReference.model == this.modelController, string.Concat(new object[]
			{
				"The reference model [",
				this.testReference.model,
				"] does not match the model [",
				this.modelController,
				"]"
			}), "");
			this.Assert("testReference.hand == actualControllerHand", this.testReference.hand == this.actualControllerHand, string.Concat(new object[]
			{
				"The reference hand [",
				this.testReference.hand,
				"] does not match the hand [",
				this.actualControllerHand,
				"]"
			}), "");
			foreach (SDK_BaseController.ControllerHand controllerHand in this.GetOtherHands(this.actualControllerHand))
			{
				this.Assert("testReference.hand == " + controllerHand, this.testReference.hand != controllerHand, string.Concat(new object[]
				{
					"The reference hand [",
					this.testReference.hand,
					"] should not match the hand [",
					controllerHand,
					"]"
				}), "");
			}
			this.TearDown();
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0009A9D0 File Offset: 0x00098BD0
		protected virtual void TestStaticActual()
		{
			this.SetUp("Static Actual");
			this.testReference = VRTK_ControllerReference.GetControllerReference(this.actualController);
			this.Assert("testReference != null", this.testReference != null, "reference should not be null", this.testReference.ToString());
			this.Assert("testReference.index == actualIndex", this.testReference.index == this.actualIndex, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.index,
				"] does not match the actual [",
				this.actualIndex,
				"]"
			}), "");
			this.Assert("testReference.actual == actualController", this.testReference.actual == this.actualController, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.actual,
				"] does not match the actual [",
				this.actualController,
				"]"
			}), "");
			this.Assert("testReference.scriptAlias == aliasController", this.testReference.scriptAlias == this.aliasController, string.Concat(new object[]
			{
				"The reference script alias [",
				this.testReference.scriptAlias,
				"] does not match the actual [",
				this.aliasController,
				"]"
			}), "");
			this.Assert("testReference.model == modelController", this.testReference.model == this.modelController, string.Concat(new object[]
			{
				"The reference model [",
				this.testReference.model,
				"] does not match the model [",
				this.modelController,
				"]"
			}), "");
			this.Assert("testReference.hand == actualControllerHand", this.testReference.hand == this.actualControllerHand, string.Concat(new object[]
			{
				"The reference hand [",
				this.testReference.hand,
				"] does not match the hand [",
				this.actualControllerHand,
				"]"
			}), "");
			foreach (SDK_BaseController.ControllerHand controllerHand in this.GetOtherHands(this.actualControllerHand))
			{
				this.Assert("testReference.hand == " + controllerHand, this.testReference.hand != controllerHand, string.Concat(new object[]
				{
					"The reference hand [",
					this.testReference.hand,
					"] should not match the hand [",
					controllerHand,
					"]"
				}), "");
			}
			this.TearDown();
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0009ACC4 File Offset: 0x00098EC4
		protected virtual void TestStaticAlias()
		{
			this.SetUp("Static Alias");
			this.testReference = VRTK_ControllerReference.GetControllerReference(this.aliasController);
			this.Assert("testReference != null", this.testReference != null, "reference should not be null", this.testReference.ToString());
			this.Assert("testReference.index == actualIndex", this.testReference.index == this.actualIndex, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.index,
				"] does not match the actual [",
				this.actualIndex,
				"]"
			}), "");
			this.Assert("testReference.actual == actualController", this.testReference.actual == this.actualController, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.actual,
				"] does not match the actual [",
				this.actualController,
				"]"
			}), "");
			this.Assert("testReference.scriptAlias == aliasController", this.testReference.scriptAlias == this.aliasController, string.Concat(new object[]
			{
				"The reference script alias [",
				this.testReference.scriptAlias,
				"] does not match the actual [",
				this.aliasController,
				"]"
			}), "");
			this.Assert("testReference.model == modelController", this.testReference.model == this.modelController, string.Concat(new object[]
			{
				"The reference model [",
				this.testReference.model,
				"] does not match the model [",
				this.modelController,
				"]"
			}), "");
			this.Assert("testReference.hand == actualControllerHand", this.testReference.hand == this.actualControllerHand, string.Concat(new object[]
			{
				"The reference hand [",
				this.testReference.hand,
				"] does not match the hand [",
				this.actualControllerHand,
				"]"
			}), "");
			foreach (SDK_BaseController.ControllerHand controllerHand in this.GetOtherHands(this.actualControllerHand))
			{
				this.Assert("testReference.hand == " + controllerHand, this.testReference.hand != controllerHand, string.Concat(new object[]
				{
					"The reference hand [",
					this.testReference.hand,
					"] should not match the hand [",
					controllerHand,
					"]"
				}), "");
			}
			this.TearDown();
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0009AFB8 File Offset: 0x000991B8
		protected virtual void TestStaticModel()
		{
			this.SetUp("Static Model");
			this.testReference = VRTK_ControllerReference.GetControllerReference(this.modelController);
			this.Assert("testReference != null", this.testReference != null, "reference should not be null", this.testReference.ToString());
			this.Assert("testReference.index == actualIndex", this.testReference.index == this.actualIndex, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.index,
				"] does not match the actual [",
				this.actualIndex,
				"]"
			}), "");
			this.Assert("testReference.actual == actualController", this.testReference.actual == this.actualController, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.actual,
				"] does not match the actual [",
				this.actualController,
				"]"
			}), "");
			this.Assert("testReference.scriptAlias == aliasController", this.testReference.scriptAlias == this.aliasController, string.Concat(new object[]
			{
				"The reference script alias [",
				this.testReference.scriptAlias,
				"] does not match the actual [",
				this.aliasController,
				"]"
			}), "");
			this.Assert("testReference.model == modelController", this.testReference.model == this.modelController, string.Concat(new object[]
			{
				"The reference model [",
				this.testReference.model,
				"] does not match the model [",
				this.modelController,
				"]"
			}), "");
			this.Assert("testReference.hand == actualControllerHand", this.testReference.hand == this.actualControllerHand, string.Concat(new object[]
			{
				"The reference hand [",
				this.testReference.hand,
				"] does not match the hand [",
				this.actualControllerHand,
				"]"
			}), "");
			foreach (SDK_BaseController.ControllerHand controllerHand in this.GetOtherHands(this.actualControllerHand))
			{
				this.Assert("testReference.hand == " + controllerHand, this.testReference.hand != controllerHand, string.Concat(new object[]
				{
					"The reference hand [",
					this.testReference.hand,
					"] should not match the hand [",
					controllerHand,
					"]"
				}), "");
			}
			this.TearDown();
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0009B2AC File Offset: 0x000994AC
		protected virtual void TestStaticHand()
		{
			this.SetUp("Static Hand");
			this.testReference = VRTK_ControllerReference.GetControllerReference(this.actualControllerHand);
			this.Assert("testReference != null", this.testReference != null, "reference should not be null", this.testReference.ToString());
			this.Assert("testReference.index == actualIndex", this.testReference.index == this.actualIndex, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.index,
				"] does not match the actual [",
				this.actualIndex,
				"]"
			}), "");
			this.Assert("testReference.actual == actualController", this.testReference.actual == this.actualController, string.Concat(new object[]
			{
				"The reference actual [",
				this.testReference.actual,
				"] does not match the actual [",
				this.actualController,
				"]"
			}), "");
			this.Assert("testReference.scriptAlias == aliasController", this.testReference.scriptAlias == this.aliasController, string.Concat(new object[]
			{
				"The reference script alias [",
				this.testReference.scriptAlias,
				"] does not match the actual [",
				this.aliasController,
				"]"
			}), "");
			this.Assert("testReference.model == modelController", this.testReference.model == this.modelController, string.Concat(new object[]
			{
				"The reference model [",
				this.testReference.model,
				"] does not match the model [",
				this.modelController,
				"]"
			}), "");
			this.Assert("testReference.hand == actualControllerHand", this.testReference.hand == this.actualControllerHand, string.Concat(new object[]
			{
				"The reference hand [",
				this.testReference.hand,
				"] does not match the hand [",
				this.actualControllerHand,
				"]"
			}), "");
			foreach (SDK_BaseController.ControllerHand controllerHand in this.GetOtherHands(this.actualControllerHand))
			{
				this.Assert("testReference.hand == " + controllerHand, this.testReference.hand != controllerHand, string.Concat(new object[]
				{
					"The reference hand [",
					this.testReference.hand,
					"] should not match the hand [",
					controllerHand,
					"]"
				}), "");
			}
			this.TearDown();
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0009B5A0 File Offset: 0x000997A0
		protected virtual void TestStaticIsValid()
		{
			this.SetUp("Static IsValid");
			this.Assert("!VRTK_ControllerReference.IsValid(null)", !VRTK_ControllerReference.IsValid(null), "null reference should not be valid", "");
			this.Assert("!VRTK_ControllerReference.IsValid(VRTK_ControllerReference.GetControllerReference(999))", !VRTK_ControllerReference.IsValid(VRTK_ControllerReference.GetControllerReference(999U)), "invalid reference should not be valid", "");
			this.Assert("!VRTK_ControllerReference.IsValid(VRTK_ControllerReference.GetControllerReference(new GameObject()))", !VRTK_ControllerReference.IsValid(VRTK_ControllerReference.GetControllerReference(new GameObject())), "invalid reference should not be valid", "");
			this.Assert("VRTK_ControllerReference.IsValid(VRTK_ControllerReference.GetControllerReference(actualIndex))", VRTK_ControllerReference.IsValid(VRTK_ControllerReference.GetControllerReference(this.actualIndex)), "valid reference should be valid", "");
			this.TearDown();
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0009B650 File Offset: 0x00099850
		protected virtual void TestStaticGetRealIndex()
		{
			this.SetUp("Static GetRealIndex");
			this.Assert("VRTK_ControllerReference.GetRealIndex(null) == uint.MaxValue", VRTK_ControllerReference.GetRealIndex(null) == uint.MaxValue, "null reference should have index of uint.MaxValue", "");
			this.Assert("VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(999)) == uint.MaxValue", VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(999U)) == uint.MaxValue, "invalid reference should have index of uint.MaxValue", "");
			this.Assert("VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(actualIndex)) == actualIndex", VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(this.actualIndex)) == this.actualIndex, "valid reference should have index of actualIndex", "");
			this.Assert("VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(actualController)) == actualIndex", VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(this.actualController)) == this.actualIndex, "valid reference should have index of actualIndex", "");
			this.Assert("VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(aliasController)) == actualIndex", VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(this.aliasController)) == this.actualIndex, "valid reference should have index of actualIndex", "");
			this.Assert("VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(modelController)) == actualIndex", VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(this.modelController)) == this.actualIndex, "valid reference should have index of actualIndex", "");
			this.Assert("VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(actualControllerHand)) == actualIndex", VRTK_ControllerReference.GetRealIndex(VRTK_ControllerReference.GetControllerReference(this.actualControllerHand)) == this.actualIndex, "valid reference should have index of actualIndex", "");
			this.TearDown();
		}

		// Token: 0x040017D4 RID: 6100
		public uint overrideActualIndex = uint.MaxValue;

		// Token: 0x040017D5 RID: 6101
		public GameObject overrideActualController;

		// Token: 0x040017D6 RID: 6102
		public GameObject overrideAliasController;

		// Token: 0x040017D7 RID: 6103
		public GameObject overrideModelController;

		// Token: 0x040017D8 RID: 6104
		public SDK_BaseController.ControllerHand actualControllerHand = SDK_BaseController.ControllerHand.Right;

		// Token: 0x040017D9 RID: 6105
		protected VRTK_ControllerReference testReference;

		// Token: 0x040017DA RID: 6106
		protected uint actualIndex;

		// Token: 0x040017DB RID: 6107
		protected GameObject actualController;

		// Token: 0x040017DC RID: 6108
		protected GameObject aliasController;

		// Token: 0x040017DD RID: 6109
		protected GameObject modelController;
	}
}
