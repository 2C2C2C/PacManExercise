using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HentaiTools.PoolWa
{

    /// <summary>
    /// todo : define how this handler will do on different states
    /// such as enabled/disabled and more as a mono
    /// manager is to fit different game
    /// </summary>
    public class MuyPoolManager : MonoBehaviour
    {

        private static MuyPoolManager instance;
        public static MuyPoolManager Instance => instance ?? (instance = MuyPoolManager.GetCreateSelf());

        private Dictionary<Type, MuyObjectPool> m_pools;


        private static MuyPoolManager GetCreateSelf()
        {
            if (MuyPoolManager.instance == null)
            {
                GameObject go = new GameObject("MuyPoolManager");
                DontDestroyOnLoad(go);
                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.identity;
                MuyPoolManager tmp = go.AddComponent<MuyPoolManager>();
                tmp.m_pools = new Dictionary<Type, MuyObjectPool>();
                return tmp;
            }
            return MuyPoolManager.instance;
        }

        // do not use this
        public static void CallInitIfItDidnt()
        {
            if (MuyPoolManager.instance != null)
                return;
            else
            {


            }
        }

        #region init pool

        public static void CallInitPool4GridObjs()
        {
            if (MuyPoolManager.Instance == null)
                MuyPoolManager.Instance.EmptyFunc();

            if (MuyPoolManager.Instance.m_pools == null)
                MuyPoolManager.Instance.m_pools = new Dictionary<Type, MuyObjectPool>();
            // todo : to load prefabs at folders
            BaseGridObject[] gos = Resources.LoadAll<BaseGridObject>("GridObjs");
            BaseGridObject bgo;
            Debug.Log(gos.Length);
            if (gos == null)
            {
                Debug.LogError("wawawa");
                return;
            }

            for (int i = 0; i < gos.Length; i++)
            {
                bgo = gos[i].GetComponent<BaseGridObject>();
                if (MuyPoolManager.Instance.m_pools.ContainsKey(bgo.GetType()))
                    continue;
                else
                {
                    Transform refPos = (new GameObject(bgo.GetType().ToString())).transform;
                    refPos.SetParent(MuyPoolManager.Instance.transform);
                    refPos.localPosition = Vector3.zero;
                    refPos.localRotation = Quaternion.identity;
                    MuyPoolManager.Instance.m_pools.Add(bgo.GetType(), new MuyObjectPool(bgo, refPos));
                    // MuyPoolManager.Instance.m_pools
                }
            }
        }


        public void InitSomePool<T>() where T : MonoBehaviour
        {
            if (MuyPoolManager.Instance.m_pools == null)
                MuyPoolManager.Instance.m_pools = new Dictionary<Type, MuyObjectPool>();

            // todo : to load prefabs at folders
            // T[] gos = (Resources.LoadAll(typeof(T).ToString()) as T[]);
            GameObject[] gos = (Resources.LoadAll<GameObject>("BaseGridMovement") as GameObject[]);
            if (gos == null)
            {
                Debug.LogError($"cant get this kind({typeof(T).ToString()}) of objs");
                return;
            }
            T tmpWa;
            for (int i = 0; i < gos.Length; i++)
            {
                tmpWa = gos[i].GetComponent<T>();
                if (MuyPoolManager.Instance.m_pools.ContainsKey(tmpWa.GetType()))
                    continue;
                else
                {
                    Transform refPos = (new GameObject(tmpWa.GetType().ToString())).transform;
                    refPos.SetParent(MuyPoolManager.Instance.transform);
                    refPos.localPosition = Vector3.zero;
                    refPos.localRotation = Quaternion.identity;
                    MuyPoolManager.Instance.m_pools.Add(tmpWa.GetType(), new MuyObjectPool((tmpWa as MonoBehaviour), refPos));
                    // MuyPoolManager.Instance.m_pools
                }
            }
        }


        #endregion


        #region pool funcs

        // add self to secene
        public void EmptyFunc()
        {
            // idk wat is this
            // GameObject go = new GameObject();
        }

        protected IEnumerator DeTest()
        {
            yield return new WaitForSeconds(2.0f);
        }


        public MonoBehaviour SpawnOne()
        {
            return null;
        }

        public void ExtendPool<T>(int _count) where T : MonoBehaviour
        {
            if (MuyPoolManager.Instance.m_pools == null)
                MuyPoolManager.Instance.m_pools = new Dictionary<Type, MuyObjectPool>();

            if (!m_pools.ContainsKey(typeof(T)))
            {
                InitSomePool<T>();
            }
            m_pools[typeof(T)].AddExtend(_count);

        }

        #endregion



        // public void Despawn(MonoBehaviour _mono, float _delay = float.NaN)
        // {

        //     if (_delay == float.NaN)
        //     {
        //         _mono.StopAllCoroutines();
        //         // despawn it now
        //     }
        //     else
        //     {
        //         _mono.StartCoroutine(DespawnLast(_mono, _delay));
        //     }
        // }
        // protected IEnumerator DespawnLast(MonoBehaviour _mono, float _delay)
        // {
        //     yield return new WaitForSeconds(_delay);
        //     Despawn(_mono);
        // }


        // class end
    }

    // namespace end
}