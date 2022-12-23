using UnityEngine;

public interface IMoveble
{
    void Move(Controls controls, bool isOn);
    void RotateMouse(Vector2 vector);
    void JumpUp();
    void Fire(PressedStatus status);
}