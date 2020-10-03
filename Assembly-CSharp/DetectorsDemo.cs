using System;
using OPS.AntiCheat.Detector;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000037 RID: 55
public class DetectorsDemo : MonoBehaviour
{
	// Token: 0x0600013B RID: 315 RVA: 0x000099A7 File Offset: 0x00007BA7
	private void Start()
	{
		FieldCheatDetector.OnFieldCheatDetected += this.FieldCheatDetector_OnFieldCheatDetected;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000099BA File Offset: 0x00007BBA
	private void FieldCheatDetector_OnFieldCheatDetected()
	{
		this.Text.text = "Field Hack Detected! Cheater tried to modify memory!";
	}

	// Token: 0x0400018F RID: 399
	public Text Text;
}
