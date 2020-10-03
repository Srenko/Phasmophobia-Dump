using System;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	// Token: 0x02000428 RID: 1064
	[RequireComponent(typeof(Interactable))]
	public class ItemPackageSpawner : MonoBehaviour
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000A1245 File Offset: 0x0009F445
		// (set) Token: 0x060020AA RID: 8362 RVA: 0x000A124D File Offset: 0x0009F44D
		public ItemPackage itemPackage
		{
			get
			{
				return this._itemPackage;
			}
			set
			{
				this.CreatePreviewObject();
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000A1258 File Offset: 0x0009F458
		private void CreatePreviewObject()
		{
			if (!this.useItemPackagePreview)
			{
				return;
			}
			this.ClearPreview();
			if (this.useItemPackagePreview)
			{
				if (this.itemPackage == null)
				{
					return;
				}
				if (!this.useFadedPreview)
				{
					if (this.itemPackage.previewPrefab != null)
					{
						this.previewObject = Object.Instantiate<GameObject>(this.itemPackage.previewPrefab, base.transform.position, Quaternion.identity);
						this.previewObject.transform.parent = base.transform;
						this.previewObject.transform.localRotation = Quaternion.identity;
						return;
					}
				}
				else if (this.itemPackage.fadedPreviewPrefab != null)
				{
					this.previewObject = Object.Instantiate<GameObject>(this.itemPackage.fadedPreviewPrefab, base.transform.position, Quaternion.identity);
					this.previewObject.transform.parent = base.transform;
					this.previewObject.transform.localRotation = Quaternion.identity;
				}
			}
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000A1362 File Offset: 0x0009F562
		private void Start()
		{
			this.VerifyItemPackage();
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000A136A File Offset: 0x0009F56A
		private void VerifyItemPackage()
		{
			if (this.itemPackage == null)
			{
				this.ItemPackageNotValid();
			}
			if (this.itemPackage.itemPrefab == null)
			{
				this.ItemPackageNotValid();
			}
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000A1399 File Offset: 0x0009F599
		private void ItemPackageNotValid()
		{
			Debug.LogError("ItemPackage assigned to " + base.gameObject.name + " is not valid. Destroying this game object.");
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000A13C8 File Offset: 0x0009F5C8
		private void ClearPreview()
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (Time.time > 0f)
				{
					Object.Destroy(transform.gameObject);
				}
				else
				{
					Object.DestroyImmediate(transform.gameObject);
				}
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000A1440 File Offset: 0x0009F640
		private void Update()
		{
			if (this.itemIsSpawned && this.spawnedItem == null)
			{
				this.itemIsSpawned = false;
				this.useFadedPreview = false;
				this.dropEvent.Invoke();
				this.CreatePreviewObject();
			}
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000A1478 File Offset: 0x0009F678
		private void OnHandHoverBegin(Hand hand)
		{
			if (this.GetAttachedItemPackage(hand) == this.itemPackage && this.takeBackItem && !this.requireTriggerPressToReturn)
			{
				this.TakeBackItem(hand);
			}
			if (!this.requireTriggerPressToTake)
			{
				this.SpawnAndAttachObject(hand);
			}
			if (this.requireTriggerPressToTake && this.showTriggerHint)
			{
				ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_Axis1, "PickUp", true);
			}
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000A14DD File Offset: 0x0009F6DD
		private void TakeBackItem(Hand hand)
		{
			this.RemoveMatchingItemsFromHandStack(this.itemPackage, hand);
			if (this.itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded)
			{
				this.RemoveMatchingItemsFromHandStack(this.itemPackage, hand.otherHand);
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000A150C File Offset: 0x0009F70C
		private ItemPackage GetAttachedItemPackage(Hand hand)
		{
			if (hand.currentAttachedObject == null)
			{
				return null;
			}
			ItemPackageReference component = hand.currentAttachedObject.GetComponent<ItemPackageReference>();
			if (component == null)
			{
				return null;
			}
			return component.itemPackage;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000A1546 File Offset: 0x0009F746
		private void HandHoverUpdate(Hand hand)
		{
			if (this.requireTriggerPressToTake && hand.controller != null && hand.controller.GetHairTriggerDown())
			{
				this.SpawnAndAttachObject(hand);
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000A156C File Offset: 0x0009F76C
		private void OnHandHoverEnd(Hand hand)
		{
			if (!this.justPickedUpItem && this.requireTriggerPressToTake && this.showTriggerHint)
			{
				ControllerButtonHints.HideTextHint(hand, EVRButtonId.k_EButton_Axis1);
			}
			this.justPickedUpItem = false;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000A1598 File Offset: 0x0009F798
		private void RemoveMatchingItemsFromHandStack(ItemPackage package, Hand hand)
		{
			for (int i = 0; i < hand.AttachedObjects.Count; i++)
			{
				ItemPackageReference component = hand.AttachedObjects[i].attachedObject.GetComponent<ItemPackageReference>();
				if (component != null)
				{
					ItemPackage itemPackage = component.itemPackage;
					if (itemPackage != null && itemPackage == package)
					{
						GameObject attachedObject = hand.AttachedObjects[i].attachedObject;
						hand.DetachObject(attachedObject, true);
					}
				}
			}
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000A1610 File Offset: 0x0009F810
		private void RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType packageType, Hand hand)
		{
			for (int i = 0; i < hand.AttachedObjects.Count; i++)
			{
				ItemPackageReference component = hand.AttachedObjects[i].attachedObject.GetComponent<ItemPackageReference>();
				if (component != null && component.itemPackage.packageType == packageType)
				{
					GameObject attachedObject = hand.AttachedObjects[i].attachedObject;
					hand.DetachObject(attachedObject, true);
				}
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000A167C File Offset: 0x0009F87C
		private void SpawnAndAttachObject(Hand hand)
		{
			if (hand.otherHand != null && this.GetAttachedItemPackage(hand.otherHand) == this.itemPackage)
			{
				this.TakeBackItem(hand.otherHand);
			}
			if (this.showTriggerHint)
			{
				ControllerButtonHints.HideTextHint(hand, EVRButtonId.k_EButton_Axis1);
			}
			if (this.itemPackage.otherHandItemPrefab != null && hand.otherHand.hoverLocked)
			{
				return;
			}
			if (this.itemPackage.packageType == ItemPackage.ItemPackageType.OneHanded)
			{
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand);
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand);
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand.otherHand);
			}
			if (this.itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded)
			{
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand);
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.OneHanded, hand.otherHand);
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand);
				this.RemoveMatchingItemTypesFromHand(ItemPackage.ItemPackageType.TwoHanded, hand.otherHand);
			}
			this.spawnedItem = Object.Instantiate<GameObject>(this.itemPackage.itemPrefab);
			this.spawnedItem.SetActive(true);
			hand.AttachObject(this.spawnedItem, this.attachmentFlags, this.attachmentPoint);
			if (this.itemPackage.otherHandItemPrefab != null && hand.otherHand.controller != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemPackage.otherHandItemPrefab);
				gameObject.SetActive(true);
				hand.otherHand.AttachObject(gameObject, this.attachmentFlags, "");
			}
			this.itemIsSpawned = true;
			this.justPickedUpItem = true;
			if (this.takeBackItem)
			{
				this.useFadedPreview = true;
				this.pickupEvent.Invoke();
				this.CreatePreviewObject();
			}
		}

		// Token: 0x04001E2B RID: 7723
		public ItemPackage _itemPackage;

		// Token: 0x04001E2C RID: 7724
		private bool useItemPackagePreview = true;

		// Token: 0x04001E2D RID: 7725
		private bool useFadedPreview;

		// Token: 0x04001E2E RID: 7726
		private GameObject previewObject;

		// Token: 0x04001E2F RID: 7727
		public bool requireTriggerPressToTake;

		// Token: 0x04001E30 RID: 7728
		public bool requireTriggerPressToReturn;

		// Token: 0x04001E31 RID: 7729
		public bool showTriggerHint;

		// Token: 0x04001E32 RID: 7730
		[EnumFlags]
		public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.ParentToHand;

		// Token: 0x04001E33 RID: 7731
		public string attachmentPoint;

		// Token: 0x04001E34 RID: 7732
		public bool takeBackItem;

		// Token: 0x04001E35 RID: 7733
		public bool acceptDifferentItems;

		// Token: 0x04001E36 RID: 7734
		private GameObject spawnedItem;

		// Token: 0x04001E37 RID: 7735
		private bool itemIsSpawned;

		// Token: 0x04001E38 RID: 7736
		public UnityEvent pickupEvent;

		// Token: 0x04001E39 RID: 7737
		public UnityEvent dropEvent;

		// Token: 0x04001E3A RID: 7738
		public bool justPickedUpItem;
	}
}
