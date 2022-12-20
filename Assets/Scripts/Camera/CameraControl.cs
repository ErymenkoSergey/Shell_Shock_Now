using DG.Tweening;
using Mirror;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : CommonBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _player;
    //private bool _isServer;

    private Vector3 _defaultPosition;
   // private Vector3 _currentPosition;
    private Vector3 _startPosition;

    private bool _isPlayerEnable;
    private float _timeFlyToGarden = 2f;

    //private Vector3 _cameraApproachPosition = new Vector3(1.5f, -1, 1.5f);

    private void Awake()
    {
        //if (isServer)
            //_camera.enabled = false;
    }

    public void SetGameObject(Transform gameObject)
    {
        _player = gameObject;
        SetDefaultPosition();
    }

    private void SetDefaultPosition()
    {
        // _defaultPosition = transform.position - _player.position;
        //_defaultPosition = new Vector3(_player.position.x, _player.position.y , -2f);
        transform.position = new Vector3(_player.position.x, _player.position.y , -2f);
        SetIsPlayerEnable(true);
    }

    private void Update()
    {
        //if (_isServer)
        //    return;


        if (!_isPlayerEnable) // _currentPosition =
            return;

        transform.position = new Vector3(_player.position.x, _player.position.y, -2f); //_player.position;// + _defaultPosition;
    }

    //public void CameraZoomCalculation(Vector3 posCam)
    //{
    //    Vector3 endPos = posCam - _cameraApproachPosition;
    //    StartCoroutine(FlyToGarden(endPos));
    //}

    //private IEnumerator FlyToGarden(Vector3 GardenPoint)
    //{
    //    SetIsPlayerEnable(false);
    //    transform.DOMove(GardenPoint, _timeFlyToGarden);
    //    yield return new WaitForSeconds(_timeFlyToGarden);
    //    SetIsPlayerEnable(true);
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
