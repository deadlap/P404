using System.Collections.Generic;
using UnityEngine;

public class GenerateIcon : MonoBehaviour
{
    [SerializeField] List<GameObject> iconList;
    void Start()
    {
        Generate();
    }
    
    void Generate()
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            GameObject icon = Instantiate(iconList[i], gameObject.transform, false);
        }
    }
}
