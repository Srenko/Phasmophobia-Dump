using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK.Highlighters
{
	// Token: 0x02000342 RID: 834
	[AddComponentMenu("VRTK/Scripts/Interactions/Highlighters/VRTK_OutlineObjectCopyHighlighter")]
	public class VRTK_OutlineObjectCopyHighlighter : VRTK_BaseHighlighter
	{
		// Token: 0x06001D30 RID: 7472 RVA: 0x00095764 File Offset: 0x00093964
		public override void Initialise(Color? color = null, Dictionary<string, object> options = null)
		{
			this.usesClonedObject = true;
			if (this.stencilOutline == null)
			{
				this.stencilOutline = Object.Instantiate<Material>((Material)Resources.Load("OutlineBasic"));
			}
			this.SetOptions(options);
			this.ResetHighlighter();
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000957A2 File Offset: 0x000939A2
		public override void ResetHighlighter()
		{
			this.DeleteExistingHighlightModels();
			this.ResetHighlighterWithCustomModelPaths();
			this.ResetHighlighterWithCustomModels();
			this.ResetHighlightersWithCurrentGameObject();
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000957BC File Offset: 0x000939BC
		public override void Highlight(Color? color, float duration = 0f)
		{
			if (this.highlightModels != null && this.highlightModels.Length != 0)
			{
				this.stencilOutline.SetFloat("_Thickness", this.thickness);
				this.stencilOutline.SetColor("_OutlineColor", color.Value);
				for (int i = 0; i < this.highlightModels.Length; i++)
				{
					if (this.highlightModels[i])
					{
						this.highlightModels[i].SetActive(true);
					}
				}
			}
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00095838 File Offset: 0x00093A38
		public override void Unhighlight(Color? color = null, float duration = 0f)
		{
			if (this.highlightModels != null)
			{
				for (int i = 0; i < this.highlightModels.Length; i++)
				{
					if (this.highlightModels[i])
					{
						this.highlightModels[i].SetActive(false);
					}
				}
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0009587D File Offset: 0x00093A7D
		protected virtual void OnEnable()
		{
			if (this.customOutlineModels == null)
			{
				this.customOutlineModels = new GameObject[0];
			}
			if (this.customOutlineModelPaths == null)
			{
				this.customOutlineModelPaths = new string[0];
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000958A8 File Offset: 0x00093AA8
		protected virtual void OnDestroy()
		{
			if (this.highlightModels != null)
			{
				for (int i = 0; i < this.highlightModels.Length; i++)
				{
					if (this.highlightModels[i])
					{
						Object.Destroy(this.highlightModels[i]);
					}
				}
			}
			Object.Destroy(this.stencilOutline);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000958F8 File Offset: 0x00093AF8
		protected virtual void ResetHighlighterWithCustomModels()
		{
			if (this.customOutlineModels != null && this.customOutlineModels.Length != 0)
			{
				this.highlightModels = new GameObject[this.customOutlineModels.Length];
				for (int i = 0; i < this.customOutlineModels.Length; i++)
				{
					this.highlightModels[i] = this.CreateHighlightModel(this.customOutlineModels[i], "");
				}
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00095958 File Offset: 0x00093B58
		protected virtual void ResetHighlighterWithCustomModelPaths()
		{
			if (this.customOutlineModelPaths != null && this.customOutlineModelPaths.Length != 0)
			{
				this.highlightModels = new GameObject[this.customOutlineModels.Length];
				for (int i = 0; i < this.customOutlineModelPaths.Length; i++)
				{
					this.highlightModels[i] = this.CreateHighlightModel(null, this.customOutlineModelPaths[i]);
				}
			}
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000959B3 File Offset: 0x00093BB3
		protected virtual void ResetHighlightersWithCurrentGameObject()
		{
			if (this.highlightModels == null || this.highlightModels.Length == 0)
			{
				this.highlightModels = new GameObject[1];
				this.highlightModels[0] = this.CreateHighlightModel(null, "");
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000959E8 File Offset: 0x00093BE8
		protected virtual void SetOptions(Dictionary<string, object> options = null)
		{
			float option = this.GetOption<float>(options, "thickness");
			if (option > 0f)
			{
				this.thickness = option;
			}
			GameObject[] option2 = this.GetOption<GameObject[]>(options, "customOutlineModels");
			if (option2 != null)
			{
				this.customOutlineModels = option2;
			}
			string[] option3 = this.GetOption<string[]>(options, "customOutlineModelPaths");
			if (option3 != null)
			{
				this.customOutlineModelPaths = option3;
			}
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x00095A40 File Offset: 0x00093C40
		protected virtual void DeleteExistingHighlightModels()
		{
			VRTK_PlayerObject[] componentsInChildren = base.GetComponentsInChildren<VRTK_PlayerObject>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].objectType == VRTK_PlayerObject.ObjectTypes.Highlighter)
				{
					Object.Destroy(componentsInChildren[i].gameObject);
				}
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00095A7C File Offset: 0x00093C7C
		protected virtual GameObject CreateHighlightModel(GameObject givenOutlineModel, string givenOutlineModelPath)
		{
			if (givenOutlineModel != null)
			{
				givenOutlineModel = (givenOutlineModel.GetComponent<Renderer>() ? givenOutlineModel : givenOutlineModel.GetComponentInChildren<Renderer>().gameObject);
			}
			else if (givenOutlineModelPath != "")
			{
				Transform transform = base.transform.Find(givenOutlineModelPath);
				givenOutlineModel = (transform ? transform.gameObject : null);
			}
			GameObject gameObject = givenOutlineModel;
			if (gameObject == null)
			{
				gameObject = (base.GetComponent<Renderer>() ? base.gameObject : base.GetComponentInChildren<Renderer>().gameObject);
			}
			if (gameObject == null)
			{
				VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"VRTK_OutlineObjectCopyHighlighter",
					"Renderer",
					"the same or child",
					" to add the highlighter to"
				}));
				return null;
			}
			GameObject gameObject2 = new GameObject(base.name + "_HighlightModel");
			gameObject2.transform.SetParent(gameObject.transform.parent, false);
			gameObject2.transform.localPosition = gameObject.transform.localPosition;
			gameObject2.transform.localRotation = gameObject.transform.localRotation;
			gameObject2.transform.localScale = gameObject.transform.localScale;
			gameObject2.transform.SetParent(base.transform);
			foreach (Component component in gameObject.GetComponents<Component>())
			{
				if (Array.IndexOf<string>(this.copyComponents, component.GetType().ToString()) >= 0)
				{
					VRTK_SharedMethods.CloneComponent(component, gameObject2, false);
				}
			}
			MeshFilter component2 = gameObject.GetComponent<MeshFilter>();
			MeshFilter component3 = gameObject2.GetComponent<MeshFilter>();
			if (component3)
			{
				if (this.enableSubmeshHighlight)
				{
					List<CombineInstance> list = new List<CombineInstance>();
					for (int j = 0; j < component2.mesh.subMeshCount; j++)
					{
						list.Add(new CombineInstance
						{
							mesh = component2.mesh,
							subMeshIndex = j,
							transform = component2.transform.localToWorldMatrix
						});
					}
					component3.mesh = new Mesh();
					component3.mesh.CombineMeshes(list.ToArray(), true, false);
				}
				else
				{
					component3.mesh = component2.mesh;
				}
				gameObject2.GetComponent<Renderer>().material = this.stencilOutline;
			}
			gameObject2.SetActive(false);
			VRTK_PlayerObject.SetPlayerObject(gameObject2, VRTK_PlayerObject.ObjectTypes.Highlighter);
			return gameObject2;
		}

		// Token: 0x0400171D RID: 5917
		[Tooltip("The thickness of the outline effect")]
		public float thickness = 1f;

		// Token: 0x0400171E RID: 5918
		[Tooltip("The GameObjects to use as the model to outline. If one isn't provided then the first GameObject with a valid Renderer in the current GameObject hierarchy will be used.")]
		public GameObject[] customOutlineModels;

		// Token: 0x0400171F RID: 5919
		[Tooltip("A path to a GameObject to find at runtime, if the GameObject doesn't exist at edit time.")]
		public string[] customOutlineModelPaths;

		// Token: 0x04001720 RID: 5920
		[Tooltip("If the mesh has multiple sub-meshes to highlight then this should be checked, otherwise only the first mesh will be highlighted.")]
		public bool enableSubmeshHighlight;

		// Token: 0x04001721 RID: 5921
		protected Material stencilOutline;

		// Token: 0x04001722 RID: 5922
		protected GameObject[] highlightModels;

		// Token: 0x04001723 RID: 5923
		protected string[] copyComponents = new string[]
		{
			"UnityEngine.MeshFilter",
			"UnityEngine.MeshRenderer"
		};
	}
}
