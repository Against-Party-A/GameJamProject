using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public enum PlayerState
{

        Default = 0,
        FixedMove = 1,
        RandomMove = 2,
        Searching = 3,
        ForcedMove = 4 ///被玩家拖拽的时候
}
public class BabyControl : MonoBehaviour
{
    ///目前逻辑是，先去走到中心点，然后再去搜寻点，防止穿模等问题。然后开始搜寻。

    private PointMove _move;

    [FormerlySerializedAs("playerState")] 
    public PlayerState _playerState = PlayerState.Default;
    
    /// <summary>
    /// 卧室的中心点。小孩要先到卧室里最中间，然后再去随机找一个地点去搜索
    /// </summary>
    public List<Vector2> MovePosList;

    public Vector2 lastMovePos;

    
    /// <summary>
    /// 所有需要小孩子搜寻的点。
    /// </summary>
    public List<Vector2> SearchPos;

    public int SearchIndex;

    /// <summary>
    /// 已经被搜寻过的点。
    /// </summary>
    public List<Vector2> HasBeSearchPos;

    public bool beginPart2 = false;

    private void Awake()
    {
        lastMovePos = MovePosList[^1];
        _move = GetComponent<PointMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beginPart2)
        {
            switch (_playerState)
            {
                case PlayerState.Default:
                    _playerState = PlayerState.FixedMove;
                    break;
                case PlayerState.FixedMove:
                    ///先去固定位置。
                    foreach (var pos in MovePosList)
                    {
                        ///通过X轴判断已经到达的房间（先这么写）
                        if (transform.position.x > pos.x)
                        {
                            continue;
                        }

                        if (!_move.beginMove)
                        {
                            _move.BeginMove(pos);
                        }
                    }
                    break;
                case PlayerState.RandomMove:
                    ///随机挑选一个地点去搜索
                    if (!_move.beginMove)
                    {
                        SearchIndex = Random.Range(0, SearchPos.Count - 1);
                        var Search = SearchPos[SearchIndex];
                        _move.BeginMove(Search);
                    }
                    break;
                case PlayerState.Searching:
                    ///执行搜寻功能
                    
                    
                    break;
                case PlayerState.ForcedMove:
                    ///执行强制移动动画
                    /// 人物pos跟随主角pos移动
                    
                    
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void EndMove()
    {
        switch (_playerState)
        {
            case PlayerState.FixedMove:
                if (new Vector2(transform.position.x, transform.position.z) == lastMovePos)
                {
                    _playerState = PlayerState.RandomMove;
                }
                break;
            case PlayerState.RandomMove:
                _playerState = PlayerState.Searching;
                ///执行搜寻功能
                ///var pos = SearchPos[SearchIndex];
                break;
        }
    }
}
