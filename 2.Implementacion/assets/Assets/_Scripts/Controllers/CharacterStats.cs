using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Scriptables/Character Stats")]
public class CharacterStats : ScriptableObject
{

    [SerializeField] public float Velocity = 10.0f;
    [SerializeField] public float JumpForce = 10.0f;
    [Range(0f, 1f)]
    [SerializeField] public float WallSpeed = 0.1f;

}
