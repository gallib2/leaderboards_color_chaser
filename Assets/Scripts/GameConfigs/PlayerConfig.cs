using UnityEngine;

[CreateAssetMenu(menuName = "ColorChaser/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("General")]
    public PlayerController PlayerPrefab;
    public KeyCode JumpKey = KeyCode.Space;

    [Space]

    [Header("Floats and Ints")]
    public float InitialSpeed = 5.0f;
    public float SpeedPerSecond = 1.0f;
    public float JumpForce = 10.0f;
    public float JumpTime = 1f;
    public float GravityMultiplier = 1.0f;

}
