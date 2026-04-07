using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeManager : MonoBehaviour
{
    public static TypeManager tm;

    // 锻造产物类别
    public enum ForgingProductType
    {
        prop,      // 道具
        target,    // 目标
        number,     // 数字
        element     // 元素
    }
}
