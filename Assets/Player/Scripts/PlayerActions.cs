using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayerActions
{
    public static readonly InputAction movementAction = InputSystem.actions.FindAction("Movement");
    public static readonly InputAction rotationAction = InputSystem.actions.FindAction("Rotation");
    public static readonly InputAction clickAction = InputSystem.actions.FindAction("LMC");
    public static readonly InputAction interractAction = InputSystem.actions.FindAction("Interract");
    public static readonly InputAction dropAction = InputSystem.actions.FindAction("Drop");
    public static readonly InputAction sprintAction = InputSystem.actions.FindAction("Sprint");
    public static readonly InputAction jumpAction = InputSystem.actions.FindAction("Jump");

    public static readonly InputAction item1 = InputSystem.actions.FindAction("Item1");
    public static readonly InputAction item2 = InputSystem.actions.FindAction("Item2");
    public static readonly InputAction item3 = InputSystem.actions.FindAction("Item3");
    public static readonly InputAction item4 = InputSystem.actions.FindAction("Item4");
    public static readonly InputAction item5 = InputSystem.actions.FindAction("Item5");
    public static readonly InputAction item6 = InputSystem.actions.FindAction("Item6");
    public static readonly InputAction item7 = InputSystem.actions.FindAction("Item7");
    public static readonly InputAction item8 = InputSystem.actions.FindAction("Item8");
}
