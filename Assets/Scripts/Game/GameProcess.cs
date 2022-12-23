using UnityEngine;

public class GameProcess : CommonBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputControl _input;
    [SerializeField] private GameObject _land;

    public Camera GetCamera() => _camera;
    public InputControl Input => _input;

    private void Awake()
    {
        SetLand();
    }

    private void SetLand()
    {
        _land.SetActive(true);
    }
}
