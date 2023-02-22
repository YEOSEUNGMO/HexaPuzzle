using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.GameInit();
    }

    void UnionTest()
    {
        List<int> a = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            a.Add(i);
        }
        List<int> b = new List<int>();
        Debug.Log($"Before Union : {a.Count}");
        a = a.Union(b).ToList();
        Debug.Log($"After Union : {a.Count}");
    }
}
