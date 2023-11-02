using UnityEngine;

public static class InputManager
{
    private static Controls _ctrls;

    private static Vector3 _mousePos;

    private static Camera cam;
    public static Ray GetCameraRay()
    {
        return cam.ScreenPointToRay(_mousePos);
    }

    public static void Init(Player p)
    {
        _ctrls = new();
        
        cam = Camera.main;
        
        _ctrls.Permenanet.Enable();

        _ctrls.InGame.Shoot.performed += _ =>
        {
            p.Shoot();
        };

        _ctrls.Permenanet.MousePos.performed += ctx =>
        {
            _mousePos = ctx.ReadValue<Vector2>();
        };
    }

    public static void EnableInGame()
    {
        _ctrls.InGame.Enable();
    }
}
