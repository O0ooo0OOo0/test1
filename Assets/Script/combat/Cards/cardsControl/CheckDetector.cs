using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDetecktor : MonoBehaviour
{
    public List<TargetDetector> targetDetectors;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CheckIsCanBeMark();
    }

    //public void CheckIsCanBeMark()
    //{
    //    for (int i = 0; i < targetDetectors.Count; i++)
    //    {
    //        TargetDetector target0 = targetDetectors[i];
    //        if (target0.isTargetInside == true)
    //        {
    //            for (int j = 0; j < targetDetectors.Count; j++)
    //            {
    //                TargetDetector target1 = targetDetectors[j];
    //                if (j != i)
    //                {
    //                    target1.isCanCheck = false;
    //                    targetDetectors[j] = target1;
    //                }
    //            }
    //        }
    //        else if (target0.isTargetInside == false)
    //        {
    //            for (int j = 0; j < targetDetectors.Count; j++)
    //            {
    //                TargetDetector target1 = targetDetectors[j];
    //                if (j != i)
    //                {
    //                    target1.isCanCheck = false;
    //                    targetDetectors[j] = target1;
    //                }
    //            }
    //        }
    //        targetDetectors[i] = target0;
    //    }
    //}
}
