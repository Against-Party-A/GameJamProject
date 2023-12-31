using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public enum PlayerState
{
        Default = 0,
        FixedMove = 1,
        RandomMove = 2,
        Searching = 3,
        BackToRandomMove = 4,
        ForcedMove = 5, ///被玩家拖拽的时候
        EndGame = 6
}
public class BabyControl : MonoBehaviour
{
    public const int SEARCH_COMPELETE = -99;
    ///目前逻辑是，先去走到中心点，然后再去搜寻点，防止穿模等问题。然后开始搜寻。

    private PointMove _move;

    private Searchkunkun _searchkunkun;

    [FormerlySerializedAs("playerState")] 
    public PlayerState _playerState = PlayerState.Default;
    
    /// <summary>
    /// 卧室的中心点。小孩要先到卧室里最中间，然后再去随机找一个地点去搜索
    /// </summary>
    public List<Vector2> MovePosList;

    private Vector2 lastMovePos;

    
    /// <summary>
    /// 所有需要小孩子搜寻的点。
    /// </summary>
    public List<Vector2> SearchPos;

    private Vector2 kunkunPos = new Vector2(1 , 8);

    private int SearchIndex = SEARCH_COMPELETE;


    private Vector3 dis;


    private void Start()
    {
        lastMovePos = MovePosList[^1];
        _move = GetComponent<PointMove>();
        _searchkunkun = GetComponent<Searchkunkun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == 2)
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
                        ///通过z轴判断已经到达的房间（先这么写）
                        if (transform.position.z >= pos.y)
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
                        var Search = kunkunPos;
                        if (SearchPos.Count != 0)
                        {
                            if (SearchIndex == SEARCH_COMPELETE)
                            {
                                SearchIndex = Random.Range(0, SearchPos.Count - 1);
                            }
                            Search = SearchPos[SearchIndex];
                        }
                        _move.BeginMove(Search);
                    }
                    break;
                case PlayerState.Searching:
                    break;
                case PlayerState.ForcedMove:
                    ///执行强制移动动画
                    /// 人物pos跟随主角pos移动
                    transform.position = Player.Instance.transform.position + dis;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SearchSuccess();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ForceMove();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ReturnSearch();
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
                _searchkunkun.BeginSearch();
                break;
            case PlayerState.BackToRandomMove:
                _playerState = PlayerState.RandomMove;
                break;
        }
    }

    public void ForceMove()
    {
        if (GameManager.Instance.gameState == 2)
        {
            dis = transform.position - Player.Instance.transform.position;
            _playerState = PlayerState.ForcedMove;
        
            if (_move.beginMove)
            {
                _move.EndMove();
            }
            _move._animator.SetBool("ForcedMove" , true);
        }
    }

    public void ReturnSearch()
    {
        if (_playerState == PlayerState.ForcedMove)
        {
            _move._animator.SetBool("ForcedMove" , false);
            if (lastMovePos.y > transform.position.z)
            {
                _playerState = PlayerState.FixedMove;
            }
            else
            {
                _playerState = PlayerState.BackToRandomMove;
                _move.BeginMove(lastMovePos);
            }
        }
    }


    /// <summary>
    /// 搜寻结束后调用
    /// </summary>
    public void SearchSuccess()
    {
        if (SearchPos.Count > 0)
        {
            SearchPos.RemoveAt(SearchIndex);
            SearchIndex = SEARCH_COMPELETE;
            _playerState = PlayerState.BackToRandomMove;
            _move.BeginMove(lastMovePos); 
        }
        else
        {
            UIManager.Instance.PlayEnd(0);
            ///告诉UI孩子搜到了
        }

    }

    public void SetKunKunPos(int index)
    {
        kunkunPos = SearchPos[index];
        SearchPos.RemoveAt(index);
    }


    public void ResetAll()
    {
        ///重新开始功能
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
