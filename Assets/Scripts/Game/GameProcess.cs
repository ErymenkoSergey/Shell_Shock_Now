using Mirror;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    [SerializeField] private InputControl _input;
    [SerializeField] private Transform _pointMap;
    [SerializeField] private GameObject _earthPrefab1;
    [SerializeField] private GameObject _earthPrefab2;
    [SerializeField] private Land _currentLand;
    [SerializeField] private GameNetConfigurator _netConfigurator;
    private Worm _worm;
    public Worm GetPlayer() => _worm;

    public GameNetConfigurator GetConfigurator() => _netConfigurator;
    public InputControl Input => _input;

    public void SetPlayer(Worm worm)
    {
        _worm = worm;
    }

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
    }
}
