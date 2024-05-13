using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PageGeneration : MonoBehaviour
{
    [SerializeField] List<GameObject> leftPageContentList;
    GameObject bookPage;
    void Awake()
    {
        Generate();
    }
    
    void Generate()
    {
        for (int i = 0; i < leftPageContentList.Count; i++)
        {
            print("halllo" + i+1);
            //Undskyld olga
            bookPage = GameObject.Find($"Left Page ({i + 1})");
            Instantiate(leftPageContentList[i], bookPage.transform.position, bookPage.transform.rotation * Quaternion.identity, bookPage.transform);
        }
    }
}
