using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static GameControls _gameControls;

    private static Vector3 _mousePos;

    private static Camera cam;

    public static Ray GetCameraRay()
    {
        return cam.ScreenPointToRay(_mousePos);
    }

    public static void Init(Player myPlayer)
    {
        _gameControls = new GameControls();
        cam = Camera.main;


        _gameControls.Permanent.Enable();

        _gameControls.InGame.Movement.performed += ctx => 
        {
            myPlayer.SetMovementDirection(ctx.ReadValue<Vector3>());
        };

        _gameControls.InGame.Jump.performed += ctx =>
        {
            myPlayer.PlayerJump(ctx.ReadValue<Vector3>());
        };

        _gameControls.InGame.Stealth.performed += ctx =>
        {
            myPlayer.StealthMode(ctx.ReadValue<Vector3>());
        };

        _gameControls.InGame.Look.performed += ctx =>
        {
            myPlayer.SetLookRotation(ctx.ReadValue<Vector2>());
        };

        _gameControls.InGame.Shoot.performed += ctx =>
        {
            myPlayer.Shoot();
        };

        _gameControls.InGame.Reload.started += ctx =>
        {
            myPlayer.Reload();
        };

        _gameControls.InGame.SemiWeapon.started += ctx =>
        {
            myPlayer.SemiWeapon();
        };

        _gameControls.InGame.BurstWeapon.started += ctx =>
        {
            myPlayer.BurstWeapon();
        };

        _gameControls.InGame.ShotgunWeapon.started += ctx =>
        {
            myPlayer.ShotgunWeapon();
        };
        
        _gameControls.Permanent.Enable();
    }

    public static void SetGameControls()
    {
        _gameControls.InGame.Enable();
        _gameControls.UI.Disable();
    }

    public static void SetUIControls()
    {
        _gameControls.UI.Enable();
        _gameControls.InGame.Disable();
    }
}
