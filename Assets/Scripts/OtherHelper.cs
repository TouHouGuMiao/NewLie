using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherHelper  {

    private static OtherHelper _instance = null;
    public static OtherHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new OtherHelper();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 随机打乱list元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    /// <returns></returns>
    public List<T> Shuffle<T>(List<T> original)
    {
        System.Random randomNum = new System.Random();
        int index = 0;
        T temp;
        for (int i = 0; i < original.Count; i++)
        {
            index = randomNum.Next(0, original.Count - 1);
            if (index != i)
            {
                temp = original[i];
                original[i] = original[index];
                original[index] = temp;
            }
        }
        return original;
    }

}
