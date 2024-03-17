using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Stats", menuName = "Prefabs/Projectile Stats")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] public float Velocity;
}