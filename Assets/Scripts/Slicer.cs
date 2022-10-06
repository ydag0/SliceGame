using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
public class Slicer : MonoBehaviour
{
    public static Slicer Instance;
    GameObject upperPart, lowerPart;
    SlicedHull slicedHull;
    Material cuttedAreaMaterial;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        cuttedAreaMaterial = GameSettings.Instance.settings.sliceAreaMaterial;
    }

    public void SliceMe(GameObject objectToSlice)
    {
        print("Slicing: " + objectToSlice.name);
        slicedHull = objectToSlice.Slice(objectToSlice.transform.position, objectToSlice.transform.forward, cuttedAreaMaterial);
        if (slicedHull == null)
            return;
        upperPart = slicedHull.CreateUpperHull(objectToSlice, cuttedAreaMaterial);
        lowerPart= slicedHull.CreateLowerHull(objectToSlice, cuttedAreaMaterial);
        Destroy(objectToSlice);

        AddPhysicAndForce();
    }
    void AddPhysicAndForce()
    {
        Rigidbody tempRb;
        Renderer tempRend;
        //upper part
        //change color of the last material on the part(cutted area)
        tempRend = upperPart.GetComponent<Renderer>();
        tempRend.materials[tempRend.materials.Length - 1].color = GameSettings.Instance.settings.areaColors.GetRandomItem();

        upperPart.AddComponent<MeshCollider>().convex = true;
        tempRb = upperPart.AddComponent<Rigidbody>();
        tempRb.AddForce(GameSettings.Instance.settings.forcePowerForParts * Vector3.forward , ForceMode.Impulse);

        //lower part
        //change color of the last material on the part(cutted area)
        tempRend = lowerPart.GetComponent<Renderer>();
        tempRend.materials[tempRend.materials.Length - 1].color = GameSettings.Instance.settings.areaColors.GetRandomItem();

        lowerPart.AddComponent<MeshCollider>().convex = true;
        tempRb = lowerPart.AddComponent<Rigidbody>();
        tempRb.AddForce(GameSettings.Instance.settings.forcePowerForParts * -Vector3.forward, ForceMode.Impulse);

    }
}
