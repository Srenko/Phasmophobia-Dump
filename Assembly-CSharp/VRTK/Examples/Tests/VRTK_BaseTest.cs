using System;
using System.Collections;
using UnityEngine;

namespace VRTK.Examples.Tests
{
	// Token: 0x02000375 RID: 885
	public abstract class VRTK_BaseTest : MonoBehaviour
	{
		// Token: 0x06001E7E RID: 7806
		protected abstract void Test();

		// Token: 0x06001E7F RID: 7807 RVA: 0x0009A417 File Offset: 0x00098617
		protected virtual void OnEnable()
		{
			base.StartCoroutine(this.RunTests());
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0009A428 File Offset: 0x00098628
		protected virtual void BeginTest(string name, int level = 1)
		{
			this.currentTest = name;
			Debug.Log(string.Concat(new string[]
			{
				"<color=darkblue><b>",
				"".PadLeft(level, '#'),
				" Starting Tests for ",
				name,
				"</b></color>"
			}));
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0009A478 File Offset: 0x00098678
		protected virtual void SetUp(string message)
		{
			this.currentSetup = message;
			Debug.Log("<color=blue><b>#### Preparing test for " + message + "</b></color>");
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0009A496 File Offset: 0x00098696
		protected virtual void TearDown()
		{
			Debug.Log("==============================================================================");
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0009A4A4 File Offset: 0x000986A4
		protected virtual void Assert(string description, bool assertion, string failure, string success = "")
		{
			if (assertion)
			{
				Debug.Log("<color=teal><b>## [" + description + "] PASSED ##</b></color>");
			}
			else
			{
				Debug.Log(string.Concat(new string[]
				{
					"<color=maroon><b>## [",
					description,
					"] FAILED INSIDE [",
					this.currentTest,
					".",
					this.currentSetup,
					"]##</b></color>"
				}));
			}
			if (!assertion)
			{
				Debug.LogException(new Exception(failure));
				return;
			}
			if (success != "")
			{
				Debug.Log("<color=purple><i> ~~~~~> " + success + "</i></color>");
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0009A544 File Offset: 0x00098744
		protected virtual IEnumerator RunTests()
		{
			yield return new WaitForEndOfFrame();
			this.Test();
			yield break;
		}

		// Token: 0x040017D2 RID: 6098
		protected string currentTest;

		// Token: 0x040017D3 RID: 6099
		protected string currentSetup;
	}
}
