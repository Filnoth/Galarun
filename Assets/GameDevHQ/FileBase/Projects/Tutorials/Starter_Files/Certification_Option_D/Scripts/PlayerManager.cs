using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private PlayerInputActions _input;
    [SerializeField]
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        InitializeInputs();
    }

    // Update is called once per frame
    void Update()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();
        _player.Move(move);
    }

    void InitializeInputs()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();

        _input.Player.Thrusters.started += Thrusters_started;
        _input.Player.Thrusters.canceled += Thrusters_canceled;

        _input.Player.Fire.performed += Fire_performed;
        _input.Player.Firework.performed += Firework_performed;

    }

    private void Firework_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.Explode();
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.Fire();
    }

    private void Thrusters_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.Boost(false);
    }

    private void Thrusters_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.Boost(true);
        
    }
}
