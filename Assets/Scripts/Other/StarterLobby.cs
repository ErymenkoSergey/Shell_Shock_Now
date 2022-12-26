using UnityEngine;

public class StarterLobby : MonoBehaviour
{
    [SerializeField] private GameObject _choose;

    private void Awake()
    {
        _choose.SetActive(true);
    }

    private void Start()
    {
        _choose.SetActive(true);
    }
}
