using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMap : NetworkBehaviour
{
    [SerializeField] private Button _firstMap;
    [SerializeField] private Button _secondMap;
    [SerializeField] private GameNetConfigurator _netConfigurator;
    //[SyncVar(hook = nameof(SetIndexScene))] public int ChoosedMap;

    public void Start()
    {
        _firstMap.onClick.AddListener(() => SetTestIndexScene(1));
        _secondMap.onClick.AddListener(() => SetTestIndexScene(2));

        _netConfigurator = FindObjectOfType<GameNetConfigurator>();
    }

    public void OnDisable()
    {
        _firstMap.onClick.RemoveListener(() => SetTestIndexScene(1));
        _secondMap.onClick.RemoveListener(() => SetTestIndexScene(2));
    }

    [Client]
    private void SetIndexScene(int oldIndex, int newIndex)
    {
        //ChoosedMap = newIndex;
        _netConfigurator.SetChoosedMap(newIndex);
    }


    public void SetTestIndexScene(int index)
    {
        //if (isLocalPlayer)
        {
            Debug.Log($"SetTestIndexScene _ {index}");
            //ChoosedMap = index;
            _netConfigurator.SetChoosedMap(index);
            CmdSetMap(index);

        }
    }


    [Command]
    public void CmdSetMap(int index)
    {
        Debug.Log($"SetTestIndexScene _ _ {index}");
        //ChoosedMap = index;
        //RpcSetMap(index);
        //_netConfigurator.SetChoosedMap(index); // set for server 
        //NetworkServer.ChoosedMap = index;
        RpcSetMap(index);
    }

    [Server]
    private void SetMap(int index)
    {
        Debug.Log($"SetTestIndexScene _ _ _ {index}");
        //ChoosedMap = index;
        _netConfigurator.SetChoosedMap(index);
    }

    [ClientRpc]
    private void RpcSetMap(int index)
    {
        Debug.Log($"SetTestIndexScene _ _ _ _ {index}");
        //ChoosedMap = index;
        _netConfigurator.SetChoosedMap(index);
    }
}
