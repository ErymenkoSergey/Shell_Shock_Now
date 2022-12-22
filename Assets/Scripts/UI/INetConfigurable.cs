using UnityEngine;

public interface INetConfigurable
{
    void SetNamePlayer(string name);
    void TransferPlayer(GameObject player);
    string GetName();
}