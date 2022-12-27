using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : CommonBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    private bool _isPlayerEnable;

    public void SetGameObject(Transform gameObject)
    {
        _player = gameObject;
        SetDefaultPosition();
    }

    private void SetDefaultPosition()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y , -2f);
        SetIsPlayerEnable(true);
    }

    //private void Update()
    //{
    //    if (!_isPlayerEnable)
    //        return;

    //    transform.position = new Vector3(_player.position.x, _player.position.y, -2f);
    //}

    private void SetIsPlayerEnable(bool isEnable)
    {
        _isPlayerEnable = isEnable;
    }

    public Camera GetCamera()
    {
        return _camera;
    }
}
