namespace Assets.Scripts.Managers
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// This handles our dynamic generated double skyline
    /// </summary>
    public class SkylineManager : MonoBehaviour
    {
        #region Public Fields

        public Vector3 MinSize, MaxSize;
        public int NumberOfObjects = 10;
        public Transform Prefab;
        public float RecycleOffset = 10;
        public Vector3 StartPosition = new Vector3(0f, -1f, 0f);

        #endregion Public Fields

        #region Private Fields

        private Vector3 _nextPosition;
        private Queue<Transform> _objectQueue;

        #endregion Private Fields

        #region Private Methods

        private void GameOver()
        {
            enabled = false;
        }

        private void GameStart()
        {
            _nextPosition = StartPosition;
            for (var i = 0; i < NumberOfObjects; i++)
            {
                Recycle();
            }
            enabled = true;
        }

        private void Recycle()
        {
            Vector3 scale = new Vector3(
                                   Random.Range(MinSize.x, MaxSize.x),
                                   Random.Range(MinSize.y, MaxSize.y),
                                   Random.Range(MinSize.z, MaxSize.z)
                               );
            Vector3 position = _nextPosition;
            position.x += scale.x * 0.5f;
            position.y += scale.y * 0.5f;

            Transform o = _objectQueue.Dequeue();
            o.localScale = scale;
            o.localPosition = position;
            _nextPosition.x += scale.x;
            _objectQueue.Enqueue(o);
        }

        // Use this for initialization
        private void Start()
        {
            GameEventManager.GameStart += GameStart;
            GameEventManager.GameOver += GameOver;
            _objectQueue = new Queue<Transform>(NumberOfObjects);

            for (var i = 0; i < NumberOfObjects; i++)
            {
                _objectQueue.Enqueue((Transform)Instantiate(
                    Prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
            }

            enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_objectQueue.Peek().localPosition.x + RecycleOffset < Runner.DistanceTraveled)
            {
                Recycle();
            }
        }

        #endregion Private Methods
    }
}