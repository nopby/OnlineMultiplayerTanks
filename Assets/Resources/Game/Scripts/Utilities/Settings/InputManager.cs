using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using Photon.Pun.UtilityScripts;
using System;
public enum ActionState
{
    Menu,
    Game
}
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private InputActions InputActions;
    // UI
    public bool Submit;
    public bool ChangeSelectable;
    // Game Scene
    public bool Fire1;
    public bool Fire2;
    public bool Accelerate;
    public bool FarLook;
    public bool Menu;
    public bool CloseMessage;
    public bool Debug;
    public Vector2 Movement;
    public Vector2 LookUp;
    public ActionState CurrentActionState;
    private ActionState DefaultActionState = ActionState.Menu;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
        InputActions = new InputActions();
        SwitchActionMap(ActionState.Menu);
    }
    public void SwitchActionMap(ActionState actionState)
    {
        CurrentActionState = actionState;
        InputActions.Disable();
        switch (CurrentActionState)
        {
            case ActionState.Menu:
                DisableGameInputAction();
                EnableMenuInputAction();
            break;
            case ActionState.Game:
                DisableMenuInputAction();
                EnableGameInputAction();
            break;
        }
        InputActions.Enable();
    }
    private void EnableMenuInputAction()
    {
        InputActions.Menu.Submit.performed += SubmitInput;
        InputActions.Menu.ChangeSelectable.performed += ChangeSelectableInput;
        InputActions.Menu.CloseMessage.performed += CloseMessageInput;
        InputActions.Menu.Debug.performed += DebugInput;
    }
    private void DisableMenuInputAction()
    {
        InputActions.Menu.Submit.performed -= SubmitInput;
        InputActions.Menu.ChangeSelectable.performed -= ChangeSelectableInput;
        InputActions.Menu.CloseMessage.performed -= CloseMessageInput;
        InputActions.Menu.Debug.performed -= DebugInput;
    }
    private void SubmitInput(InputAction.CallbackContext ctx)
    {
        Submit = ctx.ReadValueAsButton();
    }
    private void ChangeSelectableInput(InputAction.CallbackContext ctx)
    {
        ChangeSelectable = ctx.ReadValueAsButton();
        if (ChangeSelectable)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                return;
            var down = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (down != null)
            {
                down.Select();
            }
            else
            {
                var up = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (up != null)
                {
                    up.Select();
                }
            }
        }
    }
    private void CloseMessageInput(InputAction.CallbackContext ctx)
    {
        CloseMessage = ctx.ReadValueAsButton();
    }
    private void EnableGameInputAction()
    {
        InputActions.Game.Movement.performed += MovementInput;
        InputActions.Game.Accelerate.performed += AccelerateInput;
        InputActions.Game.FarLook.performed += FarLookInput;
        InputActions.Game.LookUp.performed += LookUpInput;
        InputActions.Game.Menu.performed += MenuInput;
        InputActions.Game.Fire1.performed += Fire1Input;
        InputActions.Game.Fire2.performed += Fire2Input;
        InputActions.Menu.Debug.performed += DebugInput;
    }
    private void DisableGameInputAction()
    {
        InputActions.Game.Movement.performed -= MovementInput;
        InputActions.Game.Accelerate.performed -= AccelerateInput;
        InputActions.Game.FarLook.performed -= FarLookInput;
        InputActions.Game.LookUp.performed -= LookUpInput;
        InputActions.Game.Menu.performed -= MenuInput;
        InputActions.Game.Fire1.performed -= Fire1Input;
        InputActions.Game.Fire2.performed -= Fire2Input;
        InputActions.Menu.Debug.performed -= DebugInput;
    }
    private void MovementInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Movement = ctx.ReadValue<Vector2>();
    }
    private void AccelerateInput(InputAction.CallbackContext ctx)
    {
        Accelerate = ctx.ReadValueAsButton();
    }
    private void FarLookInput(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
            FarLook = !FarLook;
    }
    private void LookUpInput(InputAction.CallbackContext ctx)
    {
        LookUp = ctx.ReadValue<Vector2>();
    }
    private void MenuInput(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
            Menu = !Menu;
    }
    private void Fire1Input(InputAction.CallbackContext ctx)
    {
        Fire1 = ctx.ReadValueAsButton();
    }
    private void Fire2Input(InputAction.CallbackContext ctx)
    {
        Fire2 = ctx.ReadValueAsButton();
    }
    private void DebugInput(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            Debug = !Debug;
        }
        if (Debug)
        {
            PhotonStatsGui.Instance.statsWindowOn = true;
            PhotonStatsGui.Instance.statsOn = true;
            PhotonLagSimulationGui.Instance.Visible = true;
        }
        else
        {
            PhotonStatsGui.Instance.statsWindowOn = false;
            PhotonStatsGui.Instance.statsOn = false;
            PhotonLagSimulationGui.Instance.Visible = false;
        }

    }
}
