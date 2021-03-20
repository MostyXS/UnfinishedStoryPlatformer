// GENERATED AUTOMATICALLY FROM 'Assets/Game/Controls/Player Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @BasePlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @BasePlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1a5ef03b-b866-4fdf-80f0-66da81a38494"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""97b2b08b-6e08-42d9-bab4-21243ffdc05f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c6a122b3-132b-474a-ac8d-c6d772e5bb32"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4d702c88-ae18-4874-9cb4-9537a6059aa8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Control Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""3c121555-d56a-492f-b446-bad8ffefcba9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""fdf8d733-964b-403d-b230-3086ba1975bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use/Interact"",
                    ""type"": ""Button"",
                    ""id"": ""ead70fb3-92f7-4600-a89d-61125e5f1207"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ad54e16f-8217-4ae1-acc3-9fe4b1fcacb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Sideways Keyboard"",
                    ""id"": ""816a7836-3872-4d88-8612-aadad966d1be"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3d302489-d9cb-482f-a44a-111d96518296"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d5afae2f-7a0c-447d-90fc-ea3b22dc0234"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Sideways Gamepad"",
                    ""id"": ""e54de19b-1c8d-463f-a38e-aba8c8727ab4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c8f55a81-72a1-4580-9e5a-ed7beb0d6ce8"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fa9c8ea5-117c-4c7f-b13f-f371c784863d"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a69e2acf-c38d-462a-95c1-3e0cf9f69cbb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d969f2e-877d-43ef-87b5-15e879e7e79d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c09a759-c413-4285-8d95-c5f378c17765"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed89af93-9166-4cfa-b154-5789a635e512"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0080d4aa-8e8d-4df1-8fb8-2d483edcde04"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Control Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f1d145f-2b4b-4449-b762-419ec2462df7"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Control Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc4f8aa8-638e-41e9-938e-10c5343de0ce"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41516ee5-7a25-4b15-8edb-8fe9d47d8806"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a040693-f037-45d6-aa54-ed1fe6697186"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use/Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de1f6e21-bb23-43ca-b73e-9432649d168d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use/Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82a447d5-4206-4e16-9d2f-e8a2f5a43f2c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71e36c38-5f22-40e7-8549-668d415e0a91"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""User Interface"",
            ""id"": ""951d560a-d087-4453-907e-8451072f6c19"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""c12ffaa9-59bf-49d1-8ccd-53414ed8cd06"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ab4fb8bb-db76-413e-b9ee-dc7dfedd8416"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3e873c3e-bb39-44e8-b98f-b7d6e63958a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Value"",
                    ""id"": ""ebb151b8-b8d8-4db1-a0df-5868876215cd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4755501b-1bdd-4de2-a94f-684bb1eac0b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""c3daa86e-65d4-410f-b0df-1e4859ce45e8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a3cf23b4-b197-44c4-bbc8-48be0af991cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""879f4a0e-ba20-4d64-b15f-44d0a0616a21"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aed955fa-42b5-4f78-86b0-20e0151870e6"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f99c851-8824-4b2b-8e8c-5dac596c5c95"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""f2c313a3-8a70-43dd-b369-c31b79eb3043"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c742b261-3838-4d9b-ac39-0524b062fe30"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f3143206-daba-4b79-b419-889b13ef8bd8"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a60699d4-77b3-4d98-8b53-162572452b44"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8eed1a52-5021-445c-afa5-e874fd30cbbd"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5f551a79-19b2-4d4c-870c-743f1078796e"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c1d414c-c5ca-40aa-a382-2d0c439034be"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""30c984c6-6e5f-441d-b6c6-5538eb9a61d7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bfe70ecc-1eb4-44ff-8b68-ef0f717ec998"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""cbb7f3ad-19fd-46f7-b35d-587d9368166c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6d48b9b1-7838-4da1-8735-12d31bd4463d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""406ad003-299d-4d22-9bda-0b3abcc9d99a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f5b761e5-04a7-4c2f-a86b-24255eaf8f74"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""810c94e4-67a3-4052-82c4-617631c4febc"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""610384af-32b6-448f-9878-d33c0fbf399b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""01e9ee81-0340-4911-b684-0190de02fbbd"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8ccc989c-f9ab-4211-b9f5-2708407d0a9e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6aa53f96-fa11-4dd5-8c78-5aa5d4260776"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9f4f2099-387e-4b66-8e0d-9162ed9248e0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f91e2e9-c4b1-4853-b270-c5a54a439993"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c322070-9df9-4c7e-8f83-42a169709900"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_ControlZoom = m_Player.FindAction("Control Zoom", throwIfNotFound: true);
        m_Player_ToggleZoom = m_Player.FindAction("Toggle Zoom", throwIfNotFound: true);
        m_Player_UseInteract = m_Player.FindAction("Use/Interact", throwIfNotFound: true);
        m_Player_TogglePause = m_Player.FindAction("Toggle Pause", throwIfNotFound: true);
        // User Interface
        m_UserInterface = asset.FindActionMap("User Interface", throwIfNotFound: true);
        m_UserInterface_Navigate = m_UserInterface.FindAction("Navigate", throwIfNotFound: true);
        m_UserInterface_Submit = m_UserInterface.FindAction("Submit", throwIfNotFound: true);
        m_UserInterface_Cancel = m_UserInterface.FindAction("Cancel", throwIfNotFound: true);
        m_UserInterface_Point = m_UserInterface.FindAction("Point", throwIfNotFound: true);
        m_UserInterface_Click = m_UserInterface.FindAction("Click", throwIfNotFound: true);
        m_UserInterface_ScrollWheel = m_UserInterface.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UserInterface_RightClick = m_UserInterface.FindAction("RightClick", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_ControlZoom;
    private readonly InputAction m_Player_ToggleZoom;
    private readonly InputAction m_Player_UseInteract;
    private readonly InputAction m_Player_TogglePause;
    public struct PlayerActions
    {
        private @BasePlayerControls m_Wrapper;
        public PlayerActions(@BasePlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @ControlZoom => m_Wrapper.m_Player_ControlZoom;
        public InputAction @ToggleZoom => m_Wrapper.m_Player_ToggleZoom;
        public InputAction @UseInteract => m_Wrapper.m_Player_UseInteract;
        public InputAction @TogglePause => m_Wrapper.m_Player_TogglePause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @ControlZoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnControlZoom;
                @ControlZoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnControlZoom;
                @ControlZoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnControlZoom;
                @ToggleZoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleZoom;
                @ToggleZoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleZoom;
                @ToggleZoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleZoom;
                @UseInteract.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseInteract;
                @UseInteract.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseInteract;
                @UseInteract.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseInteract;
                @TogglePause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTogglePause;
                @TogglePause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTogglePause;
                @TogglePause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTogglePause;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @ControlZoom.started += instance.OnControlZoom;
                @ControlZoom.performed += instance.OnControlZoom;
                @ControlZoom.canceled += instance.OnControlZoom;
                @ToggleZoom.started += instance.OnToggleZoom;
                @ToggleZoom.performed += instance.OnToggleZoom;
                @ToggleZoom.canceled += instance.OnToggleZoom;
                @UseInteract.started += instance.OnUseInteract;
                @UseInteract.performed += instance.OnUseInteract;
                @UseInteract.canceled += instance.OnUseInteract;
                @TogglePause.started += instance.OnTogglePause;
                @TogglePause.performed += instance.OnTogglePause;
                @TogglePause.canceled += instance.OnTogglePause;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // User Interface
    private readonly InputActionMap m_UserInterface;
    private IUserInterfaceActions m_UserInterfaceActionsCallbackInterface;
    private readonly InputAction m_UserInterface_Navigate;
    private readonly InputAction m_UserInterface_Submit;
    private readonly InputAction m_UserInterface_Cancel;
    private readonly InputAction m_UserInterface_Point;
    private readonly InputAction m_UserInterface_Click;
    private readonly InputAction m_UserInterface_ScrollWheel;
    private readonly InputAction m_UserInterface_RightClick;
    public struct UserInterfaceActions
    {
        private @BasePlayerControls m_Wrapper;
        public UserInterfaceActions(@BasePlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UserInterface_Navigate;
        public InputAction @Submit => m_Wrapper.m_UserInterface_Submit;
        public InputAction @Cancel => m_Wrapper.m_UserInterface_Cancel;
        public InputAction @Point => m_Wrapper.m_UserInterface_Point;
        public InputAction @Click => m_Wrapper.m_UserInterface_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UserInterface_ScrollWheel;
        public InputAction @RightClick => m_Wrapper.m_UserInterface_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_UserInterface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserInterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IUserInterfaceActions instance)
        {
            if (m_Wrapper.m_UserInterfaceActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnCancel;
                @Point.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnScrollWheel;
                @RightClick.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnRightClick;
            }
            m_Wrapper.m_UserInterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
            }
        }
    }
    public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnControlZoom(InputAction.CallbackContext context);
        void OnToggleZoom(InputAction.CallbackContext context);
        void OnUseInteract(InputAction.CallbackContext context);
        void OnTogglePause(InputAction.CallbackContext context);
    }
    public interface IUserInterfaceActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
}
