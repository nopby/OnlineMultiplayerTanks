//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Resources/Game/Scripts/Utilities/Settings/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""29809950-caff-45f0-9047-ae86635cf199"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fe7f46d3-bcc8-4add-ba2d-c32d08c97e50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeSelectable"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d647453c-bded-4471-92ba-ede6ffdafaf0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""387652da-2c4a-4d05-949e-a019a70b1ceb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b6799e93-9277-4e2f-8fb4-be1edcad4bea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scroll Wheel"",
                    ""type"": ""Value"",
                    ""id"": ""2dd17d00-4384-4d5e-904e-7bbaa4bf63a9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CloseMessage"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5ebed458-7a03-4465-ab76-19027eed4b3b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Debug"",
                    ""type"": ""Button"",
                    ""id"": ""1ffe37c0-537a-46de-b435-227756b538b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c344ee78-d86d-4e89-9050-01a3ab739a16"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9e1ddd9-dea7-4a0d-b800-5838f46b654c"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc5aaa84-1e07-4a7a-b4fa-049c3c2e6814"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeSelectable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bac7f7db-1069-4158-9744-6c75b5a96ac9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc00cd5d-9eb2-49b1-b894-f60f897a5c90"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce51209f-1087-4732-b654-def8766439ec"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll Wheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d472bcaa-9595-49c0-aa8c-67614c2b8a99"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseMessage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfe348ea-c568-4786-8e27-568d49bf395f"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Debug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game"",
            ""id"": ""d9aaf669-31c5-49bc-a0f0-9cb438ddf22d"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7d185b40-5f5c-41c4-97af-c8799ce03902"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FarLook"",
                    ""type"": ""Button"",
                    ""id"": ""2a8038f1-6f14-4047-8531-28c1187d3c1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""09d5b4b1-324a-4b40-9077-16fd45b83dcb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LookUp"",
                    ""type"": ""Value"",
                    ""id"": ""12b433bc-8e3e-4c84-abb0-45de6efd4625"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""b10e6e29-6558-4692-b1eb-a30a6acf01cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""98cf9445-21a9-4df5-97bd-e091a9fd14b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""53c26cf7-fc54-46a6-b6d9-0de3c1e03dc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2d2f8c3d-d4ff-46ae-bf6f-7e3a70c64346"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a8f02b5a-24d2-4a10-b976-33837bf0738a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ae8d7845-d901-4ac9-865d-7cf75d3795d2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""10d8a465-7e9f-47e1-9763-9e55b37bbc12"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""70d89c7c-1cc2-4a02-86fd-4fd11426247f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""224d7961-d650-4b1e-8cc2-1585f9c56d06"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FarLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b085c434-5894-4f40-a52b-d92f3368c1e1"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b31853c-73d0-4b85-9b55-ad702fd90e60"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6731b1c5-c6f9-4a50-ac59-d3d9050f202d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0f11a51-94d8-4a14-ab30-f8e129654fde"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f08d609-3a11-4d0c-8ba7-5658e8c8dbd2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Submit = m_Menu.FindAction("Submit", throwIfNotFound: true);
        m_Menu_ChangeSelectable = m_Menu.FindAction("ChangeSelectable", throwIfNotFound: true);
        m_Menu_Point = m_Menu.FindAction("Point", throwIfNotFound: true);
        m_Menu_LeftClick = m_Menu.FindAction("Left Click", throwIfNotFound: true);
        m_Menu_ScrollWheel = m_Menu.FindAction("Scroll Wheel", throwIfNotFound: true);
        m_Menu_CloseMessage = m_Menu.FindAction("CloseMessage", throwIfNotFound: true);
        m_Menu_Debug = m_Menu.FindAction("Debug", throwIfNotFound: true);
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Movement = m_Game.FindAction("Movement", throwIfNotFound: true);
        m_Game_FarLook = m_Game.FindAction("FarLook", throwIfNotFound: true);
        m_Game_Accelerate = m_Game.FindAction("Accelerate", throwIfNotFound: true);
        m_Game_LookUp = m_Game.FindAction("LookUp", throwIfNotFound: true);
        m_Game_Menu = m_Game.FindAction("Menu", throwIfNotFound: true);
        m_Game_Fire1 = m_Game.FindAction("Fire1", throwIfNotFound: true);
        m_Game_Fire2 = m_Game.FindAction("Fire2", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Submit;
    private readonly InputAction m_Menu_ChangeSelectable;
    private readonly InputAction m_Menu_Point;
    private readonly InputAction m_Menu_LeftClick;
    private readonly InputAction m_Menu_ScrollWheel;
    private readonly InputAction m_Menu_CloseMessage;
    private readonly InputAction m_Menu_Debug;
    public struct MenuActions
    {
        private @InputActions m_Wrapper;
        public MenuActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Submit => m_Wrapper.m_Menu_Submit;
        public InputAction @ChangeSelectable => m_Wrapper.m_Menu_ChangeSelectable;
        public InputAction @Point => m_Wrapper.m_Menu_Point;
        public InputAction @LeftClick => m_Wrapper.m_Menu_LeftClick;
        public InputAction @ScrollWheel => m_Wrapper.m_Menu_ScrollWheel;
        public InputAction @CloseMessage => m_Wrapper.m_Menu_CloseMessage;
        public InputAction @Debug => m_Wrapper.m_Menu_Debug;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Submit.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                @ChangeSelectable.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnChangeSelectable;
                @ChangeSelectable.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnChangeSelectable;
                @ChangeSelectable.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnChangeSelectable;
                @Point.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPoint;
                @LeftClick.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeftClick;
                @ScrollWheel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollWheel;
                @CloseMessage.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCloseMessage;
                @CloseMessage.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCloseMessage;
                @CloseMessage.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCloseMessage;
                @Debug.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnDebug;
                @Debug.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnDebug;
                @Debug.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnDebug;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @ChangeSelectable.started += instance.OnChangeSelectable;
                @ChangeSelectable.performed += instance.OnChangeSelectable;
                @ChangeSelectable.canceled += instance.OnChangeSelectable;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @CloseMessage.started += instance.OnCloseMessage;
                @CloseMessage.performed += instance.OnCloseMessage;
                @CloseMessage.canceled += instance.OnCloseMessage;
                @Debug.started += instance.OnDebug;
                @Debug.performed += instance.OnDebug;
                @Debug.canceled += instance.OnDebug;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Movement;
    private readonly InputAction m_Game_FarLook;
    private readonly InputAction m_Game_Accelerate;
    private readonly InputAction m_Game_LookUp;
    private readonly InputAction m_Game_Menu;
    private readonly InputAction m_Game_Fire1;
    private readonly InputAction m_Game_Fire2;
    public struct GameActions
    {
        private @InputActions m_Wrapper;
        public GameActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Game_Movement;
        public InputAction @FarLook => m_Wrapper.m_Game_FarLook;
        public InputAction @Accelerate => m_Wrapper.m_Game_Accelerate;
        public InputAction @LookUp => m_Wrapper.m_Game_LookUp;
        public InputAction @Menu => m_Wrapper.m_Game_Menu;
        public InputAction @Fire1 => m_Wrapper.m_Game_Fire1;
        public InputAction @Fire2 => m_Wrapper.m_Game_Fire2;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @FarLook.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFarLook;
                @FarLook.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFarLook;
                @FarLook.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFarLook;
                @Accelerate.started -= m_Wrapper.m_GameActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnAccelerate;
                @LookUp.started -= m_Wrapper.m_GameActionsCallbackInterface.OnLookUp;
                @LookUp.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnLookUp;
                @LookUp.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnLookUp;
                @Menu.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMenu;
                @Fire1.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFire1;
                @Fire2.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFire2;
                @Fire2.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFire2;
                @Fire2.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFire2;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @FarLook.started += instance.OnFarLook;
                @FarLook.performed += instance.OnFarLook;
                @FarLook.canceled += instance.OnFarLook;
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @LookUp.started += instance.OnLookUp;
                @LookUp.performed += instance.OnLookUp;
                @LookUp.canceled += instance.OnLookUp;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
                @Fire2.started += instance.OnFire2;
                @Fire2.performed += instance.OnFire2;
                @Fire2.canceled += instance.OnFire2;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface IMenuActions
    {
        void OnSubmit(InputAction.CallbackContext context);
        void OnChangeSelectable(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnCloseMessage(InputAction.CallbackContext context);
        void OnDebug(InputAction.CallbackContext context);
    }
    public interface IGameActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFarLook(InputAction.CallbackContext context);
        void OnAccelerate(InputAction.CallbackContext context);
        void OnLookUp(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
        void OnFire2(InputAction.CallbackContext context);
    }
}
