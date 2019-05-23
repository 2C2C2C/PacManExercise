using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HentaiTools.PoolWa
{

    /// <summary>
    /// stupid pool for mono
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MuyObjectPool
    {

        public MonoBehaviour m_referenceObj;
        public Transform m_parent;
        private Queue<MonoBehaviour> m_objs;

        private int m_lendOutCount = 0;
        public int LendOutCount => m_lendOutCount;
        private int m_storeCount = 0;
        public int StoreCount => m_storeCount;


        public MuyObjectPool()
        {
            m_objs = new Queue<MonoBehaviour>();
            // AddExtend();
        }
        public MuyObjectPool(MonoBehaviour _mono, Transform _parent = null, int _poolSize = 1)
        {
            m_objs = new Queue<MonoBehaviour>();
            m_referenceObj = _mono;
            m_parent = _parent;
            AddExtend(_poolSize);
            m_lendOutCount = 0;
            m_storeCount = m_objs.Count;
        }

        public void AddExtend(int _addCount = 15)
        {
            for (int i = 0; i < _addCount; i++)
            {
                MonoBehaviour tmp = GameObject.Instantiate(m_referenceObj, m_parent.position, Quaternion.identity, m_parent);
                tmp.gameObject.SetActive(false);
                m_objs.Enqueue(tmp);
            }
            m_storeCount = m_objs.Count;
        }

        public MonoBehaviour GetOne(Transform _parent = null)
        {
            MonoBehaviour temp;
            if (m_objs.Count < 0)
                AddExtend(5);
            temp = m_objs.Dequeue();
            m_lendOutCount++;
            if (_parent != null)
                temp.transform.SetParent(_parent);
            // temp
            return temp;
        }

        public void TakeOneBack(MonoBehaviour _obj)
        {
            if (_obj == null)
            {
                Debug.LogError("why send a null obj into the pool");
                return;
            }
            else if (_obj.GetType() != m_referenceObj.GetType())
            {
                Debug.LogError("why send a different obj into this pool");
                return;
            }
            m_objs.Enqueue(_obj);
            m_lendOutCount--;
            m_storeCount = m_objs.Count;
        }

        // todo : should I support Gameobject?
        // public void TakeOneBack(GameObject _obj)
        // {
        //     if (_obj == null)
        //     {
        //         Debug.LogError("why send a null obj into the pool");
        //         return;
        //     }
        //     else if (_obj.GetType() != m_referenceObj.GetType())
        //     {
        //         Debug.LogError("why send a different obj into this pool");
        //         return;
        //     }
        //     m_objs.Enqueue(_obj);
        //     m_lendOutCount--;
        //     m_storeCount = m_objs.Count;
        // }


        /// <summary>
        /// todo : fix it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void TakeAllBack<T>() where T : MonoBehaviour
        {
            if (typeof(T) != m_referenceObj.GetType())
            {
                Debug.LogError($"dis pool can not store {typeof(T)} ");
                return;
            }

            if (m_parent == null)
            {
                Debug.LogError($"idk how to do it without a empty parent");
                return;
            }
            else
            {
                T[] monos = m_parent.GetComponentsInChildren<T>();
                for (int i = 0; i < monos.Length; i++)
                {
                    if (monos[i].gameObject.activeSelf)
                        TakeOneBack(monos[i]);
                }

            }
        }


        /// <summary>
        /// contains extend or decrease
        /// </summary>
        /// <param name="_size"></param>
        public void ReSizePool(int _size = 20)
        {
            if (_size > StoreCount + LendOutCount)
            {
                AddExtend(_size - (StoreCount + LendOutCount));
            }
            else
            {
                for (int i = 0; i < (StoreCount + LendOutCount) - _size; i++)
                    GameObject.Destroy(m_objs.Dequeue().gameObject);
            }
        }

        public void ClearPool()
        {
            while (m_objs.Count > 0)
            {
                GameObject.Destroy(m_objs.Dequeue());
            }
        }


        // class end
    }

    // namespace end
}
