//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Script/PlayerInput/PlayerAction.inputactions
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

public partial class @PlayerAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerAction"",
    ""maps"": [
        {
            ""name"": ""Action"",
            ""id"": ""39e51fda-24c6-417c-8e90-78150a1fe125"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9833f4ec-2560-4691-b52b-068164a4408b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveViewPoint"",
                    ""type"": ""Value"",
                    ""id"": ""3cb42dd7-ccd7-4bc1-b3ed-0b025d890d7c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Value"",
                    ""id"": ""6882a30a-89f3-401b-bd3b-29cd8d98795f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Value"",
                    ""id"": ""15c6b29e-d1f9-43c0-b7f4-f2ce3cd427f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LargeShot"",
                    ""type"": ""Button"",
                    ""id"": ""229be4f4-2433-4444-997b-a058943b50a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LongShot"",
                    ""type"": ""Button"",
                    ""id"": ""7c95e497-07d6-4c6c-9faf-fec4806601b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Step"",
                    ""type"": ""Button"",
                    ""id"": ""2cf61937-c46d-42e7-9505-a84d0662055d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""4837bfc0-543e-4ca0-a34b-03d443879792"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8f2d5d56-2904-473c-a192-51df81538c24"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ecec1d68-37e2-4c87-a2f8-763072b1ae29"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5e53987c-4058-43e7-9683-17841e3d942a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""542fdad0-8e98-4240-96a2-c3a7f5b2abc8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""86478f15-c2ee-4270-b864-4522f8370c73"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b913f03d-0422-472c-b576-61785c284d22"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""237e1a53-2c80-4b72-b049-347fc879bf83"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""MoveViewPoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13e1d2a7-c77e-49fd-b536-2daf1f111d50"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94ccd1d5-e2cf-44ed-837b-860dd7bebab7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""941f73f4-83b9-4c1e-a10d-358ffd91dade"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""LargeShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a87ac67e-9a97-44e9-992a-928d0b88a8af"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""LargeShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39549526-f3d4-49d9-81b7-65fde509d574"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""LongShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""593ba58f-19ed-491d-8d9e-b17a89541e09"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""LongShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b916b25-83a8-400f-912c-e065bc41f980"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7264c0df-6584-4370-98e6-c91ad4c98f0e"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99bbc27e-b701-45ad-a0cf-8365a69e88cf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Step"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""785d5759-fef8-4911-a8e5-22b2a71fa0e8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Step"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b323b38-4441-461c-b3e9-cf7a0a4a7371"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e379761-f81d-448f-b072-f84ff51f0b16"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Action
        m_Action = asset.FindActionMap("Action", throwIfNotFound: true);
        m_Action_Move = m_Action.FindAction("Move", throwIfNotFound: true);
        m_Action_MoveViewPoint = m_Action.FindAction("MoveViewPoint", throwIfNotFound: true);
        m_Action_Guard = m_Action.FindAction("Guard", throwIfNotFound: true);
        m_Action_Shift = m_Action.FindAction("Shift", throwIfNotFound: true);
        m_Action_LargeShot = m_Action.FindAction("LargeShot", throwIfNotFound: true);
        m_Action_LongShot = m_Action.FindAction("LongShot", throwIfNotFound: true);
        m_Action_Step = m_Action.FindAction("Step", throwIfNotFound: true);
        m_Action_Reload = m_Action.FindAction("Reload", throwIfNotFound: true);
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

    // Action
    private readonly InputActionMap m_Action;
    private List<IActionActions> m_ActionActionsCallbackInterfaces = new List<IActionActions>();
    private readonly InputAction m_Action_Move;
    private readonly InputAction m_Action_MoveViewPoint;
    private readonly InputAction m_Action_Guard;
    private readonly InputAction m_Action_Shift;
    private readonly InputAction m_Action_LargeShot;
    private readonly InputAction m_Action_LongShot;
    private readonly InputAction m_Action_Step;
    private readonly InputAction m_Action_Reload;
    public struct ActionActions
    {
        private @PlayerAction m_Wrapper;
        public ActionActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Action_Move;
        public InputAction @MoveViewPoint => m_Wrapper.m_Action_MoveViewPoint;
        public InputAction @Guard => m_Wrapper.m_Action_Guard;
        public InputAction @Shift => m_Wrapper.m_Action_Shift;
        public InputAction @LargeShot => m_Wrapper.m_Action_LargeShot;
        public InputAction @LongShot => m_Wrapper.m_Action_LongShot;
        public InputAction @Step => m_Wrapper.m_Action_Step;
        public InputAction @Reload => m_Wrapper.m_Action_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Action; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionActions set) { return set.Get(); }
        public void AddCallbacks(IActionActions instance)
        {
            if (instance == null || m_Wrapper.m_ActionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActionActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @MoveViewPoint.started += instance.OnMoveViewPoint;
            @MoveViewPoint.performed += instance.OnMoveViewPoint;
            @MoveViewPoint.canceled += instance.OnMoveViewPoint;
            @Guard.started += instance.OnGuard;
            @Guard.performed += instance.OnGuard;
            @Guard.canceled += instance.OnGuard;
            @Shift.started += instance.OnShift;
            @Shift.performed += instance.OnShift;
            @Shift.canceled += instance.OnShift;
            @LargeShot.started += instance.OnLargeShot;
            @LargeShot.performed += instance.OnLargeShot;
            @LargeShot.canceled += instance.OnLargeShot;
            @LongShot.started += instance.OnLongShot;
            @LongShot.performed += instance.OnLongShot;
            @LongShot.canceled += instance.OnLongShot;
            @Step.started += instance.OnStep;
            @Step.performed += instance.OnStep;
            @Step.canceled += instance.OnStep;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
        }

        private void UnregisterCallbacks(IActionActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @MoveViewPoint.started -= instance.OnMoveViewPoint;
            @MoveViewPoint.performed -= instance.OnMoveViewPoint;
            @MoveViewPoint.canceled -= instance.OnMoveViewPoint;
            @Guard.started -= instance.OnGuard;
            @Guard.performed -= instance.OnGuard;
            @Guard.canceled -= instance.OnGuard;
            @Shift.started -= instance.OnShift;
            @Shift.performed -= instance.OnShift;
            @Shift.canceled -= instance.OnShift;
            @LargeShot.started -= instance.OnLargeShot;
            @LargeShot.performed -= instance.OnLargeShot;
            @LargeShot.canceled -= instance.OnLargeShot;
            @LongShot.started -= instance.OnLongShot;
            @LongShot.performed -= instance.OnLongShot;
            @LongShot.canceled -= instance.OnLongShot;
            @Step.started -= instance.OnStep;
            @Step.performed -= instance.OnStep;
            @Step.canceled -= instance.OnStep;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
        }

        public void RemoveCallbacks(IActionActions instance)
        {
            if (m_Wrapper.m_ActionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActionActions instance)
        {
            foreach (var item in m_Wrapper.m_ActionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActionActions @Action => new ActionActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    public interface IActionActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMoveViewPoint(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
        void OnLargeShot(InputAction.CallbackContext context);
        void OnLongShot(InputAction.CallbackContext context);
        void OnStep(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
}
