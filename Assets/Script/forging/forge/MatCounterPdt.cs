using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatCounterPdt : MonoBehaviour    // 材料对应的锻造产物
{
    // 定义可以使产物锻造概率提升的材料组
    [System.Serializable]
    public struct McP
    {
        public ForgingProducts pdt;
        public InputMaterial[] mats;
    }

    public List<McP> allMcp;
    public bool isRelated;   // 判断材料跟产物之间是否相关
    public ForgingProducts matCpdt;   // 某材料对应的产物

    private Dictionary<ForgingProducts, HashSet<InputMaterial>> pdtToMat;

    private void Start()
    {
        BuildCache();
    }

    // 建立对应关系
    public void BuildCache()
    {
        pdtToMat = new Dictionary<ForgingProducts, HashSet<InputMaterial>>();

        foreach (var mcp in allMcp)
        {
            if (!pdtToMat.ContainsKey(mcp.pdt))
            {
                pdtToMat[mcp.pdt] = new HashSet<InputMaterial>();
            }

            // 将材料加入对应产物的集合
            foreach (var mat in mcp.mats)
            {
                pdtToMat[mcp.pdt].Add(mat);   // HashSet 自动去重
            }
        }
    }

    // 判断材料与产物是否相关
    public void IsMatCouPdt(ForgingProducts pdt, InputMaterial mat)
    {
        isRelated = pdtToMat[pdt].Contains(mat);
    }

    // 找到材料对应的产物
    public void FindMatCouPdt(InputMaterial mat)
    {
        for (int i = 0; i < allMcp.Count; i++)   // 查找所有关系组
        {
            for (int j = 0; j < allMcp[i].mats.Length; j++)   // 查找某关系组中的全部材料组
            {
                if (mat == allMcp[i].mats[j])   // 如果这个材料属于该材料组
                {
                    matCpdt = allMcp[i].pdt;   // 找到对应的产物
                    return;   // 结束该函数
                }
            }
        }
    }
}
