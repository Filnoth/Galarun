using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputActions _input;
    [SerializeField]
    private Player _player;
    // Start is called before the first frame update
    void Awake()
    {
        InitializeInputs();
    }

    private void OnDestroy()
    {
        _input.Player.Thrusters.started -= Thrusters_started;
        _input.Player.Thrusters.canceled -= Thrusters_canceled;
        _input.Player.Fire.performed -= Fire_performed;
        _input.Player.Pause.performed -= Pause_performed;
        _input.Player.Restart.performed -= Restart_performed;
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

        _input.Player.Pause.performed += Pause_performed;

        _input.Player.Restart.performed += Restart_performed;

    }

    private void Restart_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        GameManager.instance.RestartGame();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _player.PauseGame();
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
