using Mirror;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputControl _input;
    [SerializeField] private Transform _pointMap;
    [SerializeField] private GameObject _earthPrefab1;
    [SerializeField] private GameObject _earthPrefab2;
    [SerializeField] private Cutter _cutter;
    [SerializeField] private Land _currentLand;
    [SerializeField] private GameNetConfigurator _netConfigurator;

    public GameNetConfigurator GetConfigurator() => _netConfigurator;
    public Camera GetCamera() => _camera;
    public InputControl Input => _input;

    private void OnEnable()
    {
        _netConfigurator = FindObjectOfType<GameNetConfigurator>();

        int choosedMap = _netConfigurator.GetMapId();
        Debug.Log($"GameNetConfigurator __ {choosedMap}");
        if (choosedMap == 1)
            CreateMap(_earthPrefab1);
        if (choosedMap == 2)
            CreateMap(_earthPrefab2);
    }

    private void CreateMap(GameObject map)
    {
         _currentLand = Instantiate(map, _pointMap).GetComponent<Land>();
        _cutter.SetLandCollider(_currentLand.GetPolygon());
    }
}
