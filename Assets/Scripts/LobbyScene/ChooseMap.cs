using UnityEngine;
using UnityEngine.UI;

public class ChooseMap : MonoBehaviour
{
    [SerializeField] private Button _firstMap;
    [SerializeField] private Button _secondMap;

    private int _choosedMap;

    private void OnEnable()
    {
        _firstMap.onClick.AddListener(() => SetMap(1));
        _secondMap.onClick.AddListener(() => SetMap(2));
    }

    private void OnDisable()
    {
        _firstMap.onClick.RemoveListener(() => SetMap(1));
        _secondMap.onClick.RemoveListener(() => SetMap(2));
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void SetMap(int index)
    {
        _choosedMap = index;
    }

    public int GetMapId()
    {
        return _choosedMap;
    }
}
