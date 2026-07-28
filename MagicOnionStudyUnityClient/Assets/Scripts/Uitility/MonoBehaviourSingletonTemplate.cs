using UnityEngine;

namespace Uitility
{
    public class MonoBehaviourSingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
                return _instance;
            }
        }
    
        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}