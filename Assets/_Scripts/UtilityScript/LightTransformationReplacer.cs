using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class LightTransformationReplacer : MonoBehaviour
{
    public GameObject fromObjectParentGroup;
    public GameObject toObjectParentGroup;

    public List<Transform> fromObject;
    public List<Transform> toObject;

    public GameObject createNewObject;
}
