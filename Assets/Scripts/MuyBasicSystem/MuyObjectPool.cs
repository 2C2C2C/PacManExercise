using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HentaiTools
{

    // to change to poco
    public class MuyObjectPool : MonoBehaviour
    {
        private static MuyObjectPool instance;
        public static MuyObjectPool Instance => instance;
        //public static MuyObjectPool Instance => instance ?? (instance = new MuyObjectPool());

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        //// Start is called before the first frame update
        //void Start()
        //{

        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}

        // class end
    }

    // namespace end
}
