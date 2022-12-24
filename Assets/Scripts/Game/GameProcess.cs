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

    public Camera GetCamera() => _camera;
    public InputControl Input => _input;

    private void OnEnable()
    {
        ChooseMap map = FindObjectOfType<ChooseMap>();

        int choosedMap = map.GetMapId();

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
