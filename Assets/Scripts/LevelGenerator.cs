﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using MacManTools;
using Newtonsoft.Json;
using HentaiTools.PoolWa;
using DG.Tweening;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator Instance;

    /// <summary>
    /// todo : orgnize it agagin
    /// 0 pill, 1 wall, 2 ghost, 3 macman
    /// </summary>
    public BaseGridObject[] baseGridObjects;

    public static int m_levelSizeX = 8;
    public static int m_levelSizeY = 8;
    [SerializeField]
    protected Transform m_levelRoot;

    public int m_pillCounts = 0;
    public int m_spawnerCounts = 6;
    //public int m_builderSteps = 3;

    List<PathSpawner> pathSpawners;

    public bool m_testMode = false;

    #region actions

    public System.Action OnLevelGeneratingFinish;
    public System.Action OnLevelFinished;

    #endregion


    // /// <summary>
    // /// 0 pill, 1 wall, 2 ghost, 3 macman, 4 strong_pill, 5 fast_pill
    // /// </summary>
    // public static int[,] Grids = new int[,]
    // {
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    // };

    /// <summary>
    /// 0 pill, 1 wall, 2 ghost, 3 macman, 4 strong_pill, 5 fast_pill
    /// </summary>
    public static int[,] Grids = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };



    public static void ExtendGrids(int _sizeHigh, int _sizeWid)
    {
        Grids = new int[_sizeHigh, _sizeWid];
        LevelGenerator.m_levelSizeY = LevelGenerator.Grids.GetLength(0);
        LevelGenerator.m_levelSizeX = LevelGenerator.Grids.GetLength(1);
    }

    private void Awake()
    {
        //Debug.Log(LevelGenerator.Grid.Length);

        Instance = this;

        GameManager.Instance.AddLevelCount(0);

        Debug.Log(LevelGenerator.Grids.GetLength(0)); // 7
        Debug.Log(LevelGenerator.Grids.GetLength(1)); // 9
        //
        LevelGenerator.m_levelSizeY = LevelGenerator.Grids.GetLength(0);
        LevelGenerator.m_levelSizeX = LevelGenerator.Grids.GetLength(1);
        //Debug.Log(Grid[1, 6]);


        // know the map, generate pool
        MuyPoolManager.Instance.EmptyFunc();

        // todo : how should I sized muy pool
        // MuyPoolManager.Instance.InitSomePool<Wall>(1);
        // todo : init objs, but not here
        MuyPoolManager.Instance.InitSomePool<BaseGridObject>(1);
        MuyPoolManager.Instance.InitSomePool<BaseGridMovement>(1);
        MuyPoolManager.Instance.ResizePool<Ghost>(5);
        MuyPoolManager.Instance.ResizePool<Wall>(LevelGenerator.m_levelSizeY * LevelGenerator.m_levelSizeX);
        MuyPoolManager.Instance.ResizePool<Pill>(LevelGenerator.m_levelSizeY * LevelGenerator.m_levelSizeX);

        MuyPoolManager.Instance.InitSomePool<EffectControllerBase>(1);
        MuyPoolManager.Instance.ResizePool<GhostCracked>(5);
        // MuyPoolManager.Instance.ResizePool<StrongPillEaten>(5);

        if (m_testMode)
            GenerateLevel();
        else
            NewGenerateLevel();

    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    /// <summary>
    /// wat is dis
    /// </summary>
    public void GenerateLevel()
    {
        //Vector3 pos = m_levelRoot.position;

        for (int i = m_levelSizeY; i > 0; i--)
        {
            for (int j = 0; j < m_levelSizeX; j++)
            {
                //Debug.Log(LevelGenerator.Grid[i - 1, j]);
                BaseGridObject bgo;
                if (LevelGenerator.Grids[i - 1, j] == 0)
                    m_pillCounts++;
                bgo = Instantiate(baseGridObjects[LevelGenerator.Grids[i - 1, j]], new Vector3(j, m_levelSizeY - i, 0.0f), Quaternion.identity);
                bgo.m_gridPos = new IntVector2(j, m_levelSizeY - i);
            }
        }
    }

    /// <summary>
    /// todo : take objs from the pool
    /// </summary>
    public void NewGenerateLevel()
    {
        //Debug.Log(LevelGenerator.Grids.GetLength(0)); // 7
        //Debug.Log(LevelGenerator.Grids.GetLength(1)); // 9
        //
        LevelGenerator.m_levelSizeY = LevelGenerator.Grids.GetLength(0);
        LevelGenerator.m_levelSizeX = LevelGenerator.Grids.GetLength(1);

        // temp uses
        int i = 0, j = 0;
        // filling all grids with wall
        for (i = 0; i < m_levelSizeY; i++)
        {
            for (j = 0; j < m_levelSizeX; j++)
            {
                Grids[i, j] = 1;
            }
        }

        // create path
        //UnityEngine.Random.InitState();
        pathSpawners = new List<PathSpawner>();
        List<IntVector2> spawnerPoses = new List<IntVector2>();
        Debug.Log("set spawners pos");
        for (i = 0; i < m_spawnerCounts; i++)
        {
            IntVector2 pos;
            bool goNext;
            //pos = new IntVector2(UnityEngine.Random.Range(2, LevelGenerator.m_levelSizeX - 2), UnityEngine.Random.Range(2, LevelGenerator.m_levelSizeY - 2));
            do
            {
                goNext = true;
                pos = new IntVector2(UnityEngine.Random.Range(2, LevelGenerator.m_levelSizeX - 2), UnityEngine.Random.Range(2, LevelGenerator.m_levelSizeY - 2));
                for (j = 0; j < spawnerPoses.Count; j++)
                {
                    if (pos == spawnerPoses[j])
                    {
                        goNext = false;
                        break;
                    }
                }

            } while (!goNext);
            spawnerPoses.Add(pos);
            //while (tempCheck.ContainsKey(pos))
            //{
            //    pos = new IntVector2(UnityEngine.Random.Range(2, LevelGenerator.m_levelSizeX - 2), UnityEngine.Random.Range(2, LevelGenerator.m_levelSizeY - 2));
            //}
            pathSpawners.Add(new PathSpawner(pos, 5));
            //Debug.Log($"spawner pos {pos.ToString()}");
        }

        //tempCheck.Clear();
        spawnerPoses.Clear();
        StartCoroutine(GenerateLevelLast());
    }
    protected IEnumerator GenerateLevelLast()
    {
        int builderLast = m_spawnerCounts;
        int i, j;// k;
        // build path
        while (builderLast > 0)
        {
            for (i = 0; i < m_spawnerCounts; i++)
            {
                if (!pathSpawners[i].m_moveFinished) // pathSpawners[i] != null
                {
                    //Debug.Log("spawner move builders");
                    if (pathSpawners[i].MoveBuidlers())
                    {
                        //pathSpawners[i] = null;
                        builderLast--;
                        //Debug.Log("1 spawner is finish la");
                    }
                }
                yield return null;
            }
        }

        // add player

        // need change
        Debug.Log($"set macman and special pills");
        LevelGenerator.Grids[pathSpawners[0].m_pos.y, pathSpawners[0].m_pos.x] = 3;
        LevelGenerator.Grids[pathSpawners[1].m_pos.y, pathSpawners[1].m_pos.x] = 5;
        for (i = 2; i < m_spawnerCounts; i++)
        {
            LevelGenerator.Grids[pathSpawners[i].m_pos.y, pathSpawners[i].m_pos.x] = 4;
        }
        m_pillCounts = 0;
        // fill with objs
        for (i = m_levelSizeY; i > 0; i--)
        {
            for (j = 0; j < m_levelSizeX; j++)
            {
                //add ghost
                if ((i == m_levelSizeY - 1 || i == 2) && (j == 1 || j == m_levelSizeX - 2))
                    LevelGenerator.Grids[i - 1, j] = 2;

                BaseGridObject bgo;
                bgo = TakeObjFromPool(LevelGenerator.Grids[i - 1, j]);
                // bgo = Instantiate(baseGridObjects[LevelGenerator.Grids[i - 1, j]], new Vector3(j, m_levelSizeY - i, 0.0f), Quaternion.identity);
                bgo.transform.position = new Vector3(j, m_levelSizeY - i, 0.0f);
                bgo.transform.rotation = Quaternion.identity;
                bgo.m_gridPos = new IntVector2(j, m_levelSizeY - i);
                bgo.InitSelfPostition();
                ObjMoveTest(bgo.transform, bgo.transform.position, bgo.transform.position - Vector3.up * 18.0f, 1.5f);
                // switch (LevelGenerator.Grids[i - 1, j])
                // {
                //     case 0:// pills
                //         // Debug.Log("spawn pill la");
                //         m_pillCounts++;
                //         IntVector2 iv = new IntVector2(j, i - 1);
                //         for (k = 0; k < m_spawnerCounts; k++)
                //         {
                //             if (iv == pathSpawners[k].m_pos)
                //             {
                //                 //Debug.Log($"spawn strong pill");
                //                 (bgo as Pill).InitPill(PillType.Strong);
                //                 break;
                //             }
                //         }
                //         break;
                //     case 4:
                //     case 5:
                //         m_pillCounts++;
                //         break;
                //     default:
                //         break;
                // }
            }
        }

        // CameraFollow cfwa = Camera.main.GetComponent<CameraFollow>();
        CameraFollow cfwa = FindObjectOfType<CameraFollow>();
        if (cfwa)
        {
            cfwa.m_targetToFollow = FindObjectOfType<MacMan>().transform;
        }

        // to remove
        if (UIManager.Instance)
        {
            UIManager.Instance.CloseScreen<MenuScreen>();
            UIManager.Instance.ShowScreen<GameScreenA>();
        }

        GameManager.Instance.AddScore(0);
        Debug.Log("level is done");
        OnLevelGeneratingFinish?.Invoke();
        // todo : remove dis
        SoundManager.Instance.ResetSFCPitch();

        (UIManager.Instance.GetScreen<GameScreenA>() as GameScreenA).ShowLevelStartText(true);
        GameManager.Instance.PauseTheGame(true);
        Invoke("Ptest", 3.0f);
    }
    public void Ptest()
    {
        (UIManager.Instance.GetScreen<GameScreenA>() as GameScreenA).ShowLevelStartText(false);
        GameManager.Instance.PauseTheGame(false);
    }

    //// for test
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        LevelGenerator.ReStartTest();
    //    }
    //}

    /// <summary>
    /// take grid obj from pool, to help generate
    /// Im sure dat I should not do this
    /// </summary>
    /// <param name="_objType">0 pill, 1 wall, 2 ghost, 3 macman, 4 strong_pill, 5 fast_pill</param>
    /// <returns></returns>
    public BaseGridObject TakeObjFromPool(int _objType)
    {
        BaseGridObject bgo = null;
        switch (_objType)
        {
            case 1:
                bgo = (MuyPoolManager.Instance.GetOne<Wall>() as BaseGridObject);
                break;
            case 2:
                bgo = (MuyPoolManager.Instance.GetOne<Ghost>() as BaseGridObject);
                break;
            case 3:
                bgo = (MuyPoolManager.Instance.GetOne<MacMan>() as BaseGridObject);
                break;
            case 0:
                bgo = (MuyPoolManager.Instance.GetOne<Pill>() as BaseGridObject);
                m_pillCounts++;
                (bgo as Pill).InitPill();
                break;
            case 4:
                bgo = (MuyPoolManager.Instance.GetOne<Pill>() as BaseGridObject);
                m_pillCounts++;
                (bgo as Pill).InitPill(PillType.Strong);
                break;
            case 5:
                bgo = (MuyPoolManager.Instance.GetOne<Pill>() as BaseGridObject);
                m_pillCounts++;
                (bgo as Pill).InitPill(PillType.Fast);
                break;
            default:
                bgo = (MuyPoolManager.Instance.GetOne<Pill>() as BaseGridObject);
                m_pillCounts++;
                (bgo as Pill).InitPill();
                break;
        }
        return bgo;
    }


    public void FinishLevel(bool _playerWin = true)
    {
        // MuyPoolManager.Instance
        MuyPoolManager.Instance.TakeAllBack();
        if (_playerWin)
        {
            // player win dis level
            GameManager.Instance.AddLevelCount();
            NewGenerateLevel();
        }
        else
        {
            // player lose where
            GameManager.Instance.ResetStatus();
            NewGenerateLevel();
        }

        OnLevelFinished?.Invoke();
    }

    public static void ReStartTest()
    {
        LevelGenerator.Instance.m_pillCounts = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ObjMoveTest(Transform _bgoTransform, Vector3 _posShouldBe, Vector3 _posGoesFrom, float _duration)
    {
        _bgoTransform.position = _posGoesFrom;
        _bgoTransform.DOMove(_posShouldBe, _duration + Random.Range(0.1f, 0.6f), false).OnComplete(() =>
        {
            _bgoTransform.position = _posShouldBe;
        });
    }

    // class end
}

/// <summary>
/// some shit tools for mac man
/// </summary>
namespace MacManTools
{
    /// <summary>
    /// a point to create path
    /// </summary>
    public class PathSpawner
    {
        public IntVector2 m_pos;
        List<PathBuilder> m_builders;
        public bool m_moveFinished = false;

        public PathSpawner(IntVector2 _pos, int _maxSteps = 4)
        {
            m_pos = _pos;
            int builderCount = UnityEngine.Random.Range(2, 4);
            m_builders = new List<PathBuilder>();
            for (int i = 0; i < builderCount; i++)
            {
                m_builders.Add(new PathBuilder(m_pos, _maxSteps, i));
            }
            m_moveFinished = false;
        }

        public PathSpawner(int _xPos, int _yPos, int _maxSteps = 4)
        {
            m_pos = new IntVector2(_xPos, _yPos);
            int builderCount = UnityEngine.Random.Range(2, 4);
            m_builders = new List<PathBuilder>();
            for (int i = 0; i < builderCount; i++)
            {
                m_builders.Add(new PathBuilder(m_pos, _maxSteps, i));
            }
            m_moveFinished = false;
        }

        public bool MoveBuidlers()
        {
            if (m_moveFinished)
                return false;
            m_moveFinished = true;
            for (int i = 0; i < m_builders.Count; i++)
            {
                if (!m_builders[i].m_finished)
                    m_builders[i].Move();
                m_moveFinished = m_moveFinished && m_builders[i].m_finished;
            }
            if (m_moveFinished)
                m_builders.Clear();
            return m_moveFinished;
        }
    }

    public class PathBuilder
    {
        /// <summary>
        /// 0 up, 1 left, 2 down, 3 right
        /// </summary>
        public static IntVector2[] Dirs = { IntVector2.UpVector2Int, IntVector2.LeftVector2Int, IntVector2.DownVector2Int, IntVector2.RightVector2Int };
        //public static System.Random m_rdObj;
        public int m_dirIndex;
        public IntVector2 m_currentPos;
        public int m_steps;
        public bool m_finished;

        public PathBuilder(IntVector2 _currentPos, int _maxSteps)
        {
            m_currentPos = _currentPos;
            m_steps = _maxSteps;
            m_finished = false;
            //m_dir = Dirs[PathBuilder.m_rdObj.Next(0, 3)];
            m_dirIndex = UnityEngine.Random.Range(0, 3);
            LevelGenerator.Grids[m_currentPos.x, m_currentPos.y] = 0;
        }
        public PathBuilder(IntVector2 _currentPos, int _maxSteps, int _dirIndex)
        {
            m_currentPos = _currentPos;
            m_steps = _maxSteps;
            m_finished = false;
            //m_dir = Dirs[PathBuilder.m_rdObj.Next(0, 3)];
            m_dirIndex = _dirIndex;
        }

        /// <summary>
        /// move to next grid, buld path
        /// </summary>
        public void Move()
        {
            IntVector2 nextPos;
            bool canMove = true;
            do
            {
                canMove = true;
                nextPos = m_currentPos + PathBuilder.Dirs[m_dirIndex];
                int rt = 1;
                // check if arrive bounds
                if (nextPos.x == LevelGenerator.m_levelSizeX - 1 || nextPos.x == 0)
                {
                    if (UnityEngine.Random.Range(0, 1) > 0)
                        rt = -1;
                    m_dirIndex = (m_dirIndex + rt) % PathBuilder.Dirs.Length;
                    canMove = false;
                    continue;
                }
                else if (nextPos.y == LevelGenerator.m_levelSizeY - 1 || nextPos.y == 0)
                {
                    if (UnityEngine.Random.Range(0, 1) > 0)
                        rt = -1;
                    m_dirIndex = (m_dirIndex + rt) % PathBuilder.Dirs.Length;
                    canMove = false;
                    continue;
                }

                // if encounter other path
                //Debug.Log(nextPos.ToString());
                if (LevelGenerator.Grids[nextPos.y, nextPos.x] == 0)
                {
                    //if (LevelGenerator.Grids[nextPos.y, nextPos.x] != 5)
                    //    LevelGenerator.Grids[m_currentPos.y, m_currentPos.x] = 5;
                    m_finished = true;
                    canMove = true;
                    return;
                }

            } while (!canMove);

            LevelGenerator.Grids[nextPos.y, nextPos.x] = 0;
            m_currentPos = nextPos;
        }

        // class end
    }


    // namespace end
}
