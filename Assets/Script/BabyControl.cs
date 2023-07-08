using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    enum _enum
    {
        Default = 0,
        MoveToCenter = 1,
        MoveToSearch = 2,
        Searching = 3,
        BackToDoor = 4 ///被玩家拖拽的时候
        
    }
}
public class BabyControl : MonoBehaviour
{
    ///目前逻辑是，先去走到中心点，然后再去搜寻点，防止穿模等问题。然后开始搜寻。
    /// 
    
    
    /// <summary>
    /// 卧室的中心点。小孩要先到卧室里最中间，然后再去随机找一个地点去搜索
    /// </summary>
    public Vector3 CenterPos;

    /// <summary>
    /// 所有需要小孩子搜寻的点。
    /// </summary>
    public Vector3[] SearchPos;

    /// <summary>
    /// 已经被搜寻过的点。
    /// </summary>
    private Vector3[] HasBeSearchPos;
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
