using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using CofyEngine.Engine;
using OpenCover.Framework.Model;
using UnityEditor;
using UnityEngine;

public class Test 
{
    [MenuItem("Test/Test Pooling")]
    public static void TestPooling()
    {
        var count = 1000000;

        ClassSmall[] smalls = new ClassSmall[count];
        
        var pool = new CtorObjectPool<ClassSmall>();

        Stopwatch sw = new Stopwatch();
        
        sw.Start();

        for (int i = 0; i < count; i++)
        {

            smalls[i] = pool.Get();
        }
        
        sw.Stop();

        FLog.Log($"only get, pool time: {sw.ElapsedMilliseconds}");
    }

    public class ClassSmall
    {
        public double d1;
        public string s1;
    }
}
