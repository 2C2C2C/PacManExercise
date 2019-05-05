using System;
using UnityEngine;
using System.Collections.Generic;

namespace MacManTools
{
    public class Evently
    {
        private static Evently instance;
        public static Evently Instance => instance ?? (instance = new Evently());

        private Dictionary<Type, Delegate> delegates = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> _del)
        {
            if (delegates.ContainsKey(typeof(T)))
                delegates[typeof(T)] = Delegate.Combine(delegates[typeof(T)], _del);
            else
                delegates[typeof(T)] = _del;
        }

        public void UnSubscribe<T>(Action<T> _del)
        {
            if (delegates.ContainsKey(typeof(T)))
            {
                var dels = Delegate.Remove(delegates[typeof(T)], _del);
                if (dels == null)
                    delegates.Remove(typeof(T));
                else
                    delegates[typeof(T)] = dels;
            }

        }


        public void Publish<T>(T e)
        {
            //Debug.Log("try to publish");
            if (e == null)
                Debug.Log($"Invalid event argument: {e.GetType()}");

            if (delegates.ContainsKey(typeof(T)))
                delegates[e.GetType()].DynamicInvoke(e);
            //else
            //errro    
        }


        // class end
    }


    // namespace end
}