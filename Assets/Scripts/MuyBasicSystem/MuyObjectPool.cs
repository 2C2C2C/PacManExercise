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


        public MuyObjectPool()
        {
            m_objs = new Queue<MonoBehaviour>();
            // AddExtend();
        }
        public MuyObjectPool(MonoBehaviour _mono, Transform _parent = null)
        {
            m_objs = new Queue<MonoBehaviour>();
            m_referenceObj = _mono;
            m_parent = _parent;
            AddExtend();
        }

        public void AddExtend(int _addCount = 15)
        {
            for (int i = 0; i < _addCount; i++)
            {
                MonoBehaviour tmp = GameObject.Instantiate(m_referenceObj, m_parent.position, Quaternion.identity, m_parent);
                tmp.gameObject.SetActive(false);
                m_objs.Enqueue(tmp);
            }
        }

        public MonoBehaviour GetOne(Transform _parent = null)
        {
            MonoBehaviour temp;
            if (m_objs.Count < 0)
                AddExtend(5);
            temp = m_objs.Dequeue();
            if (_parent != null)
                temp.transform.SetParent(_parent);
            // temp
            return temp;
        }

        public void SendBackOne(MonoBehaviour _obj)
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
