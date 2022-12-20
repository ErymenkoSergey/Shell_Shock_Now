using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : CommonBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputControl _input;

    public Camera GetCamera() => _camera;
    public InputControl Input => _input;

}
