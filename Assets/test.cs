using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class test : MonoBehaviour
{

    List<string> teststr = new List<string>();

    private void Awake()
    {
        teststr.Add("aa");
        teststr.Add("bb");
        teststr.Add("cfc");

        
    }

    private void Start()
    {
        var a = JsonConvert.SerializeObject(teststr);
        Debug.Log(a);
    }
}
