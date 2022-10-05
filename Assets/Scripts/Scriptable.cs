using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)
[CreateAssetMenu(menuName ="GameSettings/New Game Settings")]
public class Scriptable : ScriptableObject
{
    [Header("Camera Settings")]
    [Tooltip("Lower Value for Faster Camera Track")]
    public float smoothTime;
    [Header("Knife Movement Settings")]
    public float gravity;
    public float forcePower;
    public Vector3 forceVector;
    public ForceMode forceType;
    public float torquePower;
    public Vector3 torqueVector;
    public ForceMode torqueForceType;
    [Header("Object Slice Settings")]
    public float forcePowerForParts;
    public Material sliceAreaMaterial;
    public List<Color> areaColors = new List<Color>();
    
}
