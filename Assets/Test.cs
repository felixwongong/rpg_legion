using System;
using UnityEditor;
using UnityEngine;

public class Test: MonoBehaviour
{
    [MenuItem("Test/Test Casting")]
    public static void TestPooling()
    {
        for (int i = 0; i < 1000; i++)
        {
            FLog.Log("10");
        }
    }

    private void Update()
    {
        Test.TestPooling();
    }
}
