using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PageGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> leftPageContentList;
    void Awake()
    {
        Generate();
    }
    
    void Generate()
    {
        for (int i = 0; i < leftPageContentList.Count; i++)
        {
            //Undskyld olga
            GameObject bookPage = GameObject.Find($"Left Page ({i + 1})");
            GameObject pageContent = Instantiate(leftPageContentList[i], bookPage.transform.position, bookPage.transform.rotation * Quaternion.identity, bookPage.transform);
        }
    }
}
