using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable", menuName = "Prefabs/Collectable")]
public class BaseCollectable : ScriptableObject
{

    [SerializeField]
    public Sprite idle;

    [SerializeField]
    public AnimatorController animationController;

    [SerializeField]
    public int points = 10;
}
