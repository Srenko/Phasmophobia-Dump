using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRTK.Examples.PanelMenu
{
	// Token: 0x0200037A RID: 890
	public class PanelMenuUIGrid : MonoBehaviour
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x0009BA68 File Offset: 0x00099C68
		private void Start()
		{
			this.gridLayoutGroup = base.GetComponent<GridLayoutGroup>();
			if (this.gridLayoutGroup == null)
			{
				VRTK_Logger.Warn(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, new object[]
				{
					"PanelMenuUIGrid",
					"GridLayoutGroup",
					"the same"
				}));
				return;
			}
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemSwipeTop += this.OnPanelMenuItemSwipeTop;
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemSwipeBottom += this.OnPanelMenuItemSwipeBottom;
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemSwipeLeft += this.OnPanelMenuItemSwipeLeft;
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemSwipeRight += this.OnPanelMenuItemSwipeRight;
			base.GetComponentInParent<VRTK_PanelMenuItemController>().PanelMenuItemTriggerPressed += this.OnPanelMenuItemTriggerPressed;
			this.SetGridLayoutItemSelectedState(this.selectedIndex);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0009BB38 File Offset: 0x00099D38
		public bool MoveSelectGridLayoutItem(PanelMenuUIGrid.Direction direction, GameObject interactableObject)
		{
			int num = this.FindNextItemBasedOnMoveDirection(direction);
			if (num != this.selectedIndex)
			{
				this.SetGridLayoutItemSelectedState(num);
				this.selectedIndex = num;
			}
			return true;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0009BB68 File Offset: 0x00099D68
		private int FindNextItemBasedOnMoveDirection(PanelMenuUIGrid.Direction direction)
		{
			float preferredWidth = this.gridLayoutGroup.preferredWidth;
			float x = this.gridLayoutGroup.cellSize.x;
			float x2 = this.gridLayoutGroup.spacing.x;
			int num = (int)Mathf.Floor(preferredWidth / (x + x2 / 2f));
			int childCount = this.gridLayoutGroup.transform.childCount;
			switch (direction)
			{
			case PanelMenuUIGrid.Direction.Up:
			{
				int num2 = this.selectedIndex - num;
				if (num2 < 0)
				{
					return this.selectedIndex;
				}
				return num2;
			}
			case PanelMenuUIGrid.Direction.Down:
			{
				int num3 = this.selectedIndex + num;
				if (num3 >= childCount)
				{
					return this.selectedIndex;
				}
				return num3;
			}
			case PanelMenuUIGrid.Direction.Left:
			{
				int num4 = this.selectedIndex - 1;
				if (num4 < 0)
				{
					return this.selectedIndex;
				}
				return num4;
			}
			case PanelMenuUIGrid.Direction.Right:
			{
				int num5 = this.selectedIndex + 1;
				if (num5 >= childCount)
				{
					return this.selectedIndex;
				}
				return num5;
			}
			default:
				return this.selectedIndex;
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0009BC48 File Offset: 0x00099E48
		private void SetGridLayoutItemSelectedState(int index)
		{
			foreach (object obj in this.gridLayoutGroup.transform)
			{
				GameObject gameObject = ((Transform)obj).gameObject;
				if (gameObject != null)
				{
					Color color = this.colorDefault;
					color.a = this.colorAlpha;
					gameObject.GetComponent<Image>().color = color;
				}
			}
			Transform child = this.gridLayoutGroup.transform.GetChild(index);
			if (child != null)
			{
				Color color2 = this.colorSelected;
				color2.a = this.colorAlpha;
				child.GetComponent<Image>().color = color2;
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0009BD10 File Offset: 0x00099F10
		private void OnPanelMenuItemSwipeTop(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.MoveSelectGridLayoutItem(PanelMenuUIGrid.Direction.Up, e.interactableObject);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0009BD20 File Offset: 0x00099F20
		private void OnPanelMenuItemSwipeBottom(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.MoveSelectGridLayoutItem(PanelMenuUIGrid.Direction.Down, e.interactableObject);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0009BD30 File Offset: 0x00099F30
		private void OnPanelMenuItemSwipeLeft(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.MoveSelectGridLayoutItem(PanelMenuUIGrid.Direction.Left, e.interactableObject);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0009BD40 File Offset: 0x00099F40
		private void OnPanelMenuItemSwipeRight(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.MoveSelectGridLayoutItem(PanelMenuUIGrid.Direction.Right, e.interactableObject);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0009BD50 File Offset: 0x00099F50
		private void OnPanelMenuItemTriggerPressed(object sender, PanelMenuItemControllerEventArgs e)
		{
			this.SendMessageToInteractableObject(e.interactableObject);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0009BD5E File Offset: 0x00099F5E
		private void SendMessageToInteractableObject(GameObject interactableObject)
		{
			interactableObject.SendMessage("UpdateGridLayoutValue", this.selectedIndex);
		}

		// Token: 0x040017E2 RID: 6114
		private readonly Color colorDefault = Color.white;

		// Token: 0x040017E3 RID: 6115
		private readonly Color colorSelected = Color.green;

		// Token: 0x040017E4 RID: 6116
		private readonly float colorAlpha = 0.25f;

		// Token: 0x040017E5 RID: 6117
		private GridLayoutGroup gridLayoutGroup;

		// Token: 0x040017E6 RID: 6118
		private int selectedIndex;

		// Token: 0x02000644 RID: 1604
		public enum Direction
		{
			// Token: 0x040028DE RID: 10462
			None,
			// Token: 0x040028DF RID: 10463
			Up,
			// Token: 0x040028E0 RID: 10464
			Down,
			// Token: 0x040028E1 RID: 10465
			Left,
			// Token: 0x040028E2 RID: 10466
			Right
		}
	}
}
