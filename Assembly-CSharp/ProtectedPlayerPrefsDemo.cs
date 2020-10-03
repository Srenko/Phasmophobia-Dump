using System;
using OPS.AntiCheat.Prefs;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class ProtectedPlayerPrefsDemo : MonoBehaviour
{
	// Token: 0x06000141 RID: 321 RVA: 0x00009B28 File Offset: 0x00007D28
	private void Start()
	{
		Debug.Log("------------------");
		Debug.Log("Protected Player Prefs Demo");
		Debug.Log("------------------");
		ProtectedPlayerPrefs.SetInt("My Int Key", 1234);
		int @int = PlayerPrefs.GetInt("My Int Key_Protected");
		Debug.Log("Value saved by Unity: " + @int);
		int int2 = ProtectedPlayerPrefs.GetInt("My Int Key");
		Debug.Log("Real Value: " + int2);
		ProtectedPlayerPrefs.SetFloat("My Float Key", 1234.56f);
		float num = (float)PlayerPrefs.GetInt("My Float Key_Protected");
		Debug.Log("Value saved by Unity: " + num);
		float @float = ProtectedPlayerPrefs.GetFloat("My Float Key");
		Debug.Log("Real Value: " + @float);
		ProtectedPlayerPrefs.SetString("My String Key", "Hello World!");
		string @string = PlayerPrefs.GetString("My String Key_Protected");
		Debug.Log("Value saved by Unity: " + @string);
		string string2 = ProtectedPlayerPrefs.GetString("My String Key");
		Debug.Log("Real Value: " + string2);
	}
}
