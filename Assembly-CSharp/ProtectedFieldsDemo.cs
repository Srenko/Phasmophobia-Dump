using System;
using OPS.AntiCheat.Field;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000038 RID: 56
public class ProtectedFieldsDemo : MonoBehaviour
{
	// Token: 0x0600013E RID: 318 RVA: 0x000099CC File Offset: 0x00007BCC
	private void Start()
	{
		Debug.Log("------------------");
		Debug.Log("Protected Fields Demo");
		Debug.Log("------------------");
		Debug.Log(new ProtectedUInt16(1234) + 1);
		Debug.Log(new ProtectedUInt32(5678U) + 2U);
		Debug.Log(new ProtectedUInt64(91011UL) + 3UL);
		Debug.Log(new ProtectedInt16(1234) + 1);
		Debug.Log(new ProtectedInt32(5678) + 2);
		Debug.Log(new ProtectedInt64(91011L) + 3L);
		Debug.Log(new ProtectedFloat(1234.123f) + 0.11f);
		Debug.Log(new ProtectedString("My Protected Text") + "!!");
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00009AFB File Offset: 0x00007CFB
	private void Update()
	{
		if (Keyboard.current.aKey.wasPressedThisFrame)
		{
			this.demoField += 1;
		}
	}

	// Token: 0x04000190 RID: 400
	private ProtectedInt32 demoField;
}
