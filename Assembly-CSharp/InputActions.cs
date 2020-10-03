using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x02000002 RID: 2
public class InputActions : IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public InputActionAsset asset { get; }

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public InputActions()
	{
		this.asset = InputActionAsset.FromJson("{\n    \"name\": \"InputActions\",\n    \"maps\": [\n        {\n            \"name\": \"Player\",\n            \"id\": \"d9e57c9f-cea9-4ba5-b77b-68ffbd0e8af7\",\n            \"actions\": [\n                {\n                    \"name\": \"Pickup\",\n                    \"type\": \"Button\",\n                    \"id\": \"71c07b7d-0e74-4d09-9da2-0fd4a68d07cd\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Secondary Use\",\n                    \"type\": \"Button\",\n                    \"id\": \"8ce367c3-5bd5-4bde-85e8-13d979a1bb9c\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Move\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"d66c0646-e6f0-4cf2-89ae-87042fbc5ee3\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Primary Use\",\n                    \"type\": \"Button\",\n                    \"id\": \"76409695-f27d-414c-9723-33d5a08db084\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Interact\",\n                    \"type\": \"Button\",\n                    \"id\": \"89606fa6-07c3-4eff-b2b1-4d7c654940cf\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Look\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"e2d59420-1dce-4825-844a-b8f5e8d267d3\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Run\",\n                    \"type\": \"Button\",\n                    \"id\": \"e10902dd-6155-47ff-8654-f7e5630a918e\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"InventorySwap\",\n                    \"type\": \"Button\",\n                    \"id\": \"434fc894-cd8c-42e1-bd40-ca35517fdecd\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"InventorySwapScroll\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"ec93de2b-0d19-4a1e-89e6-ae47b05d6309\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Pause\",\n                    \"type\": \"Button\",\n                    \"id\": \"343669c7-445b-41d8-9626-44a86057dafb\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Crouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"b223a5f1-dc66-4c52-9d00-85e7c44107f3\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"LocalPushToTalk\",\n                    \"type\": \"Button\",\n                    \"id\": \"ad60a67e-3b33-4fbd-bfb2-5a28204ff7a8\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"GlobalPushToTalk\",\n                    \"type\": \"Button\",\n                    \"id\": \"8c8c4c91-118a-4cee-9e98-3def62087b76\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Journal\",\n                    \"type\": \"Button\",\n                    \"id\": \"5fe72b78-d2bb-4520-82c7-a681e287bc77\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Drop\",\n                    \"type\": \"Button\",\n                    \"id\": \"343a50f7-ee57-4f13-afed-e0c7b1bb7e5d\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"Torch\",\n                    \"type\": \"Button\",\n                    \"id\": \"a568d88a-a090-40d2-b667-4e7ea316c789\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                },\n                {\n                    \"name\": \"MainMenuSecondary\",\n                    \"type\": \"Button\",\n                    \"id\": \"b4a65954-c7f5-4645-9a89-234ae72409e3\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\"\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"515d28fb-1246-4344-91d4-9bc71d96e1c9\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Pickup\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"edfcfcd6-871b-40a6-9981-29ef21468a18\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Pickup\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"19c28615-5f2e-497a-ac99-f6cc90651aff\",\n                    \"path\": \"<Keyboard>/f\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Secondary Use\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d7c28668-0ddc-4117-a694-6553d1d5deaa\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Secondary Use\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"c184d6f8-5556-4e42-aa16-55fd314df722\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Up\",\n                    \"id\": \"2664767e-2882-4cb7-92af-a7ecf02a092d\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Down\",\n                    \"id\": \"a41b514a-929d-402d-ab3d-bd9dfbcecbfa\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Left\",\n                    \"id\": \"38b452ec-f04e-4c96-829c-a8314caaa958\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Right\",\n                    \"id\": \"0c200238-7467-4858-ba64-b6bb543574a1\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8130c9a8-808c-494c-b150-2e5d743993df\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1fab9cbd-740e-4ed0-925f-ed6166304c84\",\n                    \"path\": \"<Mouse>/rightButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Primary Use\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c531817e-3cc8-4c3d-8d89-4a9e9e5c8297\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Primary Use\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c8fee025-8b88-4e72-9563-9d0c01b9e620\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"Press(behavior=2)\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Interact\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cc46adec-8f12-4b68-b285-a46194b47470\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Interact\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b955b067-b7d7-48e3-8aef-744c7eb050e8\",\n                    \"path\": \"<Pointer>/delta\",\n                    \"interactions\": \"\",\n                    \"processors\": \"ScaleVector2(x=0.1,y=0.1)\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5d2483b5-9ed7-47e6-85c5-eebf26c8f7bd\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"ScaleVector2(x=3,y=3)\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a26d3ca1-3a35-4557-afff-465b1287a6ea\",\n                    \"path\": \"<Keyboard>/shift\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Run\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9fafc86c-f496-4303-80a9-42cac2accf64\",\n                    \"path\": \"<Gamepad>/leftStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Run\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"96643975-c1ba-4945-953a-06238dcd17e8\",\n                    \"path\": \"<Keyboard>/q\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"InventorySwap\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fa990469-42a4-466a-be30-d3dccb71cf2d\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"InventorySwap\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ce378805-51a5-4d1c-b4fa-0a2abbdbb360\",\n                    \"path\": \"<Mouse>/scroll\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"InventorySwapScroll\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4d445293-3254-428d-8c17-10008ba29f5a\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"296654ed-ef0e-40d1-8f43-5388af03cf5b\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Pause\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3db28f36-27d2-43b1-be6d-2e1a0b5f68ed\",\n                    \"path\": \"<Keyboard>/c\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Crouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"49083b18-7dbf-4819-88a0-0765b7da9949\",\n                    \"path\": \"<Gamepad>/rightStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Crouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"07e6602c-3df2-4f7c-8ab8-fbbf688ca6a9\",\n                    \"path\": \"<Keyboard>/v\",\n                    \"interactions\": \"Press\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"LocalPushToTalk\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9304eca3-755e-4624-8c7f-2d1cc4863237\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"LocalPushToTalk\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e0702bef-74ba-42ed-a28b-980e6458b77d\",\n                    \"path\": \"<Keyboard>/b\",\n                    \"interactions\": \"Press\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"GlobalPushToTalk\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8e3325c5-a458-4c68-9a72-ed2d65b13090\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"GlobalPushToTalk\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"761adae2-e780-4081-9eb3-1fcf4358bf67\",\n                    \"path\": \"<Keyboard>/j\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Journal\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8ebc6d09-7ef8-4e47-90e2-c1c6029b554a\",\n                    \"path\": \"<Gamepad>/dpad/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Journal\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"061812a3-9037-4de0-80c8-85b75044eb94\",\n                    \"path\": \"<Keyboard>/g\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Drop\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4f15614e-31e9-4d4e-af6c-29fdd7ed1cee\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad\",\n                    \"action\": \"Drop\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n            [...string is too long...]");
		this.m_Player = this.asset.FindActionMap("Player", true);
		this.m_Player_Pickup = this.m_Player.FindAction("Pickup", true);
		this.m_Player_SecondaryUse = this.m_Player.FindAction("Secondary Use", true);
		this.m_Player_Move = this.m_Player.FindAction("Move", true);
		this.m_Player_PrimaryUse = this.m_Player.FindAction("Primary Use", true);
		this.m_Player_Interact = this.m_Player.FindAction("Interact", true);
		this.m_Player_Look = this.m_Player.FindAction("Look", true);
		this.m_Player_Run = this.m_Player.FindAction("Run", true);
		this.m_Player_InventorySwap = this.m_Player.FindAction("InventorySwap", true);
		this.m_Player_InventorySwapScroll = this.m_Player.FindAction("InventorySwapScroll", true);
		this.m_Player_Pause = this.m_Player.FindAction("Pause", true);
		this.m_Player_Crouch = this.m_Player.FindAction("Crouch", true);
		this.m_Player_LocalPushToTalk = this.m_Player.FindAction("LocalPushToTalk", true);
		this.m_Player_GlobalPushToTalk = this.m_Player.FindAction("GlobalPushToTalk", true);
		this.m_Player_Journal = this.m_Player.FindAction("Journal", true);
		this.m_Player_Drop = this.m_Player.FindAction("Drop", true);
		this.m_Player_Torch = this.m_Player.FindAction("Torch", true);
		this.m_Player_MainMenuSecondary = this.m_Player.FindAction("MainMenuSecondary", true);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002227 File Offset: 0x00000427
	public void Dispose()
	{
		Object.Destroy(this.asset);
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002234 File Offset: 0x00000434
	// (set) Token: 0x06000005 RID: 5 RVA: 0x00002241 File Offset: 0x00000441
	public InputBinding? bindingMask
	{
		get
		{
			return this.asset.bindingMask;
		}
		set
		{
			this.asset.bindingMask = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000006 RID: 6 RVA: 0x0000224F File Offset: 0x0000044F
	// (set) Token: 0x06000007 RID: 7 RVA: 0x0000225C File Offset: 0x0000045C
	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return this.asset.devices;
		}
		set
		{
			this.asset.devices = value;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000008 RID: 8 RVA: 0x0000226A File Offset: 0x0000046A
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			return this.asset.controlSchemes;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002277 File Offset: 0x00000477
	public bool Contains(InputAction action)
	{
		return this.asset.Contains(action);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002285 File Offset: 0x00000485
	public IEnumerator<InputAction> GetEnumerator()
	{
		return this.asset.GetEnumerator();
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002292 File Offset: 0x00000492
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000229A File Offset: 0x0000049A
	public void Enable()
	{
		this.asset.Enable();
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000022A7 File Offset: 0x000004A7
	public void Disable()
	{
		this.asset.Disable();
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000022B4 File Offset: 0x000004B4
	public InputActions.PlayerActions Player
	{
		get
		{
			return new InputActions.PlayerActions(this);
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600000F RID: 15 RVA: 0x000022BC File Offset: 0x000004BC
	public InputControlScheme KeyboardScheme
	{
		get
		{
			if (this.m_KeyboardSchemeIndex == -1)
			{
				this.m_KeyboardSchemeIndex = this.asset.FindControlSchemeIndex("Keyboard");
			}
			return this.asset.controlSchemes[this.m_KeyboardSchemeIndex];
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002304 File Offset: 0x00000504
	public InputControlScheme GamepadScheme
	{
		get
		{
			if (this.m_GamepadSchemeIndex == -1)
			{
				this.m_GamepadSchemeIndex = this.asset.FindControlSchemeIndex("Gamepad");
			}
			return this.asset.controlSchemes[this.m_GamepadSchemeIndex];
		}
	}

	// Token: 0x04000002 RID: 2
	private readonly InputActionMap m_Player;

	// Token: 0x04000003 RID: 3
	private InputActions.IPlayerActions m_PlayerActionsCallbackInterface;

	// Token: 0x04000004 RID: 4
	private readonly InputAction m_Player_Pickup;

	// Token: 0x04000005 RID: 5
	private readonly InputAction m_Player_SecondaryUse;

	// Token: 0x04000006 RID: 6
	private readonly InputAction m_Player_Move;

	// Token: 0x04000007 RID: 7
	private readonly InputAction m_Player_PrimaryUse;

	// Token: 0x04000008 RID: 8
	private readonly InputAction m_Player_Interact;

	// Token: 0x04000009 RID: 9
	private readonly InputAction m_Player_Look;

	// Token: 0x0400000A RID: 10
	private readonly InputAction m_Player_Run;

	// Token: 0x0400000B RID: 11
	private readonly InputAction m_Player_InventorySwap;

	// Token: 0x0400000C RID: 12
	private readonly InputAction m_Player_InventorySwapScroll;

	// Token: 0x0400000D RID: 13
	private readonly InputAction m_Player_Pause;

	// Token: 0x0400000E RID: 14
	private readonly InputAction m_Player_Crouch;

	// Token: 0x0400000F RID: 15
	private readonly InputAction m_Player_LocalPushToTalk;

	// Token: 0x04000010 RID: 16
	private readonly InputAction m_Player_GlobalPushToTalk;

	// Token: 0x04000011 RID: 17
	private readonly InputAction m_Player_Journal;

	// Token: 0x04000012 RID: 18
	private readonly InputAction m_Player_Drop;

	// Token: 0x04000013 RID: 19
	private readonly InputAction m_Player_Torch;

	// Token: 0x04000014 RID: 20
	private readonly InputAction m_Player_MainMenuSecondary;

	// Token: 0x04000015 RID: 21
	private int m_KeyboardSchemeIndex = -1;

	// Token: 0x04000016 RID: 22
	private int m_GamepadSchemeIndex = -1;

	// Token: 0x020004C4 RID: 1220
	public struct PlayerActions
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x000BCB14 File Offset: 0x000BAD14
		public PlayerActions(InputActions wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000BCB1D File Offset: 0x000BAD1D
		public InputAction Pickup
		{
			get
			{
				return this.m_Wrapper.m_Player_Pickup;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000BCB2A File Offset: 0x000BAD2A
		public InputAction SecondaryUse
		{
			get
			{
				return this.m_Wrapper.m_Player_SecondaryUse;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000BCB37 File Offset: 0x000BAD37
		public InputAction Move
		{
			get
			{
				return this.m_Wrapper.m_Player_Move;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000BCB44 File Offset: 0x000BAD44
		public InputAction PrimaryUse
		{
			get
			{
				return this.m_Wrapper.m_Player_PrimaryUse;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000BCB51 File Offset: 0x000BAD51
		public InputAction Interact
		{
			get
			{
				return this.m_Wrapper.m_Player_Interact;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000BCB5E File Offset: 0x000BAD5E
		public InputAction Look
		{
			get
			{
				return this.m_Wrapper.m_Player_Look;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000BCB6B File Offset: 0x000BAD6B
		public InputAction Run
		{
			get
			{
				return this.m_Wrapper.m_Player_Run;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000BCB78 File Offset: 0x000BAD78
		public InputAction InventorySwap
		{
			get
			{
				return this.m_Wrapper.m_Player_InventorySwap;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000BCB85 File Offset: 0x000BAD85
		public InputAction InventorySwapScroll
		{
			get
			{
				return this.m_Wrapper.m_Player_InventorySwapScroll;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000BCB92 File Offset: 0x000BAD92
		public InputAction Pause
		{
			get
			{
				return this.m_Wrapper.m_Player_Pause;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000BCB9F File Offset: 0x000BAD9F
		public InputAction Crouch
		{
			get
			{
				return this.m_Wrapper.m_Player_Crouch;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000BCBAC File Offset: 0x000BADAC
		public InputAction LocalPushToTalk
		{
			get
			{
				return this.m_Wrapper.m_Player_LocalPushToTalk;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000BCBB9 File Offset: 0x000BADB9
		public InputAction GlobalPushToTalk
		{
			get
			{
				return this.m_Wrapper.m_Player_GlobalPushToTalk;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000BCBC6 File Offset: 0x000BADC6
		public InputAction Journal
		{
			get
			{
				return this.m_Wrapper.m_Player_Journal;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000BCBD3 File Offset: 0x000BADD3
		public InputAction Drop
		{
			get
			{
				return this.m_Wrapper.m_Player_Drop;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000BCBE0 File Offset: 0x000BADE0
		public InputAction Torch
		{
			get
			{
				return this.m_Wrapper.m_Player_Torch;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000BCBED File Offset: 0x000BADED
		public InputAction MainMenuSecondary
		{
			get
			{
				return this.m_Wrapper.m_Player_MainMenuSecondary;
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000BCBFA File Offset: 0x000BADFA
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_Player;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000BCC07 File Offset: 0x000BAE07
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000BCC14 File Offset: 0x000BAE14
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000BCC21 File Offset: 0x000BAE21
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000BCC2E File Offset: 0x000BAE2E
		public static implicit operator InputActionMap(InputActions.PlayerActions set)
		{
			return set.Get();
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000BCC38 File Offset: 0x000BAE38
		public void SetCallbacks(InputActions.IPlayerActions instance)
		{
			if (this.m_Wrapper.m_PlayerActionsCallbackInterface != null)
			{
				this.Pickup.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
				this.Pickup.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
				this.Pickup.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
				this.SecondaryUse.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryUse;
				this.SecondaryUse.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryUse;
				this.SecondaryUse.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryUse;
				this.Move.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
				this.Move.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
				this.Move.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
				this.PrimaryUse.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryUse;
				this.PrimaryUse.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryUse;
				this.PrimaryUse.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimaryUse;
				this.Interact.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
				this.Interact.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
				this.Interact.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
				this.Look.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
				this.Look.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
				this.Look.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
				this.Run.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
				this.Run.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
				this.Run.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
				this.InventorySwap.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInventorySwap;
				this.InventorySwap.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInventorySwap;
				this.InventorySwap.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInventorySwap;
				this.InventorySwapScroll.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInventorySwapScroll;
				this.InventorySwapScroll.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInventorySwapScroll;
				this.InventorySwapScroll.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnInventorySwapScroll;
				this.Pause.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
				this.Pause.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
				this.Pause.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
				this.Crouch.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
				this.Crouch.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
				this.Crouch.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
				this.LocalPushToTalk.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLocalPushToTalk;
				this.LocalPushToTalk.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLocalPushToTalk;
				this.LocalPushToTalk.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLocalPushToTalk;
				this.GlobalPushToTalk.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnGlobalPushToTalk;
				this.GlobalPushToTalk.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnGlobalPushToTalk;
				this.GlobalPushToTalk.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnGlobalPushToTalk;
				this.Journal.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnJournal;
				this.Journal.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnJournal;
				this.Journal.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnJournal;
				this.Drop.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
				this.Drop.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
				this.Drop.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
				this.Torch.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnTorch;
				this.Torch.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnTorch;
				this.Torch.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnTorch;
				this.MainMenuSecondary.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMainMenuSecondary;
				this.MainMenuSecondary.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMainMenuSecondary;
				this.MainMenuSecondary.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMainMenuSecondary;
			}
			this.m_Wrapper.m_PlayerActionsCallbackInterface = instance;
			if (instance != null)
			{
				this.Pickup.started += instance.OnPickup;
				this.Pickup.performed += instance.OnPickup;
				this.Pickup.canceled += instance.OnPickup;
				this.SecondaryUse.started += instance.OnSecondaryUse;
				this.SecondaryUse.performed += instance.OnSecondaryUse;
				this.SecondaryUse.canceled += instance.OnSecondaryUse;
				this.Move.started += instance.OnMove;
				this.Move.performed += instance.OnMove;
				this.Move.canceled += instance.OnMove;
				this.PrimaryUse.started += instance.OnPrimaryUse;
				this.PrimaryUse.performed += instance.OnPrimaryUse;
				this.PrimaryUse.canceled += instance.OnPrimaryUse;
				this.Interact.started += instance.OnInteract;
				this.Interact.performed += instance.OnInteract;
				this.Interact.canceled += instance.OnInteract;
				this.Look.started += instance.OnLook;
				this.Look.performed += instance.OnLook;
				this.Look.canceled += instance.OnLook;
				this.Run.started += instance.OnRun;
				this.Run.performed += instance.OnRun;
				this.Run.canceled += instance.OnRun;
				this.InventorySwap.started += instance.OnInventorySwap;
				this.InventorySwap.performed += instance.OnInventorySwap;
				this.InventorySwap.canceled += instance.OnInventorySwap;
				this.InventorySwapScroll.started += instance.OnInventorySwapScroll;
				this.InventorySwapScroll.performed += instance.OnInventorySwapScroll;
				this.InventorySwapScroll.canceled += instance.OnInventorySwapScroll;
				this.Pause.started += instance.OnPause;
				this.Pause.performed += instance.OnPause;
				this.Pause.canceled += instance.OnPause;
				this.Crouch.started += instance.OnCrouch;
				this.Crouch.performed += instance.OnCrouch;
				this.Crouch.canceled += instance.OnCrouch;
				this.LocalPushToTalk.started += instance.OnLocalPushToTalk;
				this.LocalPushToTalk.performed += instance.OnLocalPushToTalk;
				this.LocalPushToTalk.canceled += instance.OnLocalPushToTalk;
				this.GlobalPushToTalk.started += instance.OnGlobalPushToTalk;
				this.GlobalPushToTalk.performed += instance.OnGlobalPushToTalk;
				this.GlobalPushToTalk.canceled += instance.OnGlobalPushToTalk;
				this.Journal.started += instance.OnJournal;
				this.Journal.performed += instance.OnJournal;
				this.Journal.canceled += instance.OnJournal;
				this.Drop.started += instance.OnDrop;
				this.Drop.performed += instance.OnDrop;
				this.Drop.canceled += instance.OnDrop;
				this.Torch.started += instance.OnTorch;
				this.Torch.performed += instance.OnTorch;
				this.Torch.canceled += instance.OnTorch;
				this.MainMenuSecondary.started += instance.OnMainMenuSecondary;
				this.MainMenuSecondary.performed += instance.OnMainMenuSecondary;
				this.MainMenuSecondary.canceled += instance.OnMainMenuSecondary;
			}
		}

		// Token: 0x0400234F RID: 9039
		private InputActions m_Wrapper;
	}

	// Token: 0x020004C5 RID: 1221
	public interface IPlayerActions
	{
		// Token: 0x060025D5 RID: 9685
		void OnPickup(InputAction.CallbackContext context);

		// Token: 0x060025D6 RID: 9686
		void OnSecondaryUse(InputAction.CallbackContext context);

		// Token: 0x060025D7 RID: 9687
		void OnMove(InputAction.CallbackContext context);

		// Token: 0x060025D8 RID: 9688
		void OnPrimaryUse(InputAction.CallbackContext context);

		// Token: 0x060025D9 RID: 9689
		void OnInteract(InputAction.CallbackContext context);

		// Token: 0x060025DA RID: 9690
		void OnLook(InputAction.CallbackContext context);

		// Token: 0x060025DB RID: 9691
		void OnRun(InputAction.CallbackContext context);

		// Token: 0x060025DC RID: 9692
		void OnInventorySwap(InputAction.CallbackContext context);

		// Token: 0x060025DD RID: 9693
		void OnInventorySwapScroll(InputAction.CallbackContext context);

		// Token: 0x060025DE RID: 9694
		void OnPause(InputAction.CallbackContext context);

		// Token: 0x060025DF RID: 9695
		void OnCrouch(InputAction.CallbackContext context);

		// Token: 0x060025E0 RID: 9696
		void OnLocalPushToTalk(InputAction.CallbackContext context);

		// Token: 0x060025E1 RID: 9697
		void OnGlobalPushToTalk(InputAction.CallbackContext context);

		// Token: 0x060025E2 RID: 9698
		void OnJournal(InputAction.CallbackContext context);

		// Token: 0x060025E3 RID: 9699
		void OnDrop(InputAction.CallbackContext context);

		// Token: 0x060025E4 RID: 9700
		void OnTorch(InputAction.CallbackContext context);

		// Token: 0x060025E5 RID: 9701
		void OnMainMenuSecondary(InputAction.CallbackContext context);
	}
}
