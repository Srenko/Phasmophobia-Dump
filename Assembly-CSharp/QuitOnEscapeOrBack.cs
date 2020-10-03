using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class QuitOnEscapeOrBack : MonoBehaviour
{
	// Token: 0x06000627 RID: 1575 RVA: 0x0002272D File Offset: 0x0002092D
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
