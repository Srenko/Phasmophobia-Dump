using System;
using UnityEngine;

namespace VRTK.Examples
{
	// Token: 0x0200035C RID: 860
	public class Menu_Object_Spawner : VRTK_InteractableObject
	{
		// Token: 0x06001DBC RID: 7612 RVA: 0x000976EB File Offset: 0x000958EB
		public void SetSelectedColor(Color color)
		{
			this.selectedColor = color;
			base.gameObject.GetComponent<MeshRenderer>().material.color = color;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0009770A File Offset: 0x0009590A
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
			if (this.shape == Menu_Object_Spawner.PrimitiveTypes.Cube)
			{
				this.CreateShape(PrimitiveType.Cube, this.selectedColor);
			}
			else if (this.shape == Menu_Object_Spawner.PrimitiveTypes.Sphere)
			{
				this.CreateShape(PrimitiveType.Sphere, this.selectedColor);
			}
			this.ResetMenuItems();
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00097748 File Offset: 0x00095948
		private void CreateShape(PrimitiveType shape, Color color)
		{
			GameObject gameObject = GameObject.CreatePrimitive(shape);
			gameObject.transform.position = base.transform.position;
			gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			gameObject.GetComponent<MeshRenderer>().material.color = color;
			gameObject.AddComponent<Rigidbody>();
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x000977A8 File Offset: 0x000959A8
		private void ResetMenuItems()
		{
			Menu_Object_Spawner[] array = Object.FindObjectsOfType<Menu_Object_Spawner>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].StopUsing(null);
			}
		}

		// Token: 0x04001772 RID: 6002
		public Menu_Object_Spawner.PrimitiveTypes shape;

		// Token: 0x04001773 RID: 6003
		private Color selectedColor;

		// Token: 0x02000641 RID: 1601
		public enum PrimitiveTypes
		{
			// Token: 0x040028D6 RID: 10454
			Cube,
			// Token: 0x040028D7 RID: 10455
			Sphere
		}
	}
}
