namespace Assets.Scripts.Managers
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Advertisements;

    /// <summary>
    /// This handles our platforms to jump on
    /// </summary>
    public class PlatformManager : MonoBehaviour
    {
        #region Public Fields

        public Material[] Materials;
        public Vector3 MaxGap = new Vector3(4f, 1.5f, 0f);
        public Vector3 MaxSize = new Vector3(10f, 1f, 1f);
        public float MaxY = 10;
        public Vector3 MinGap = new Vector3(2f, -1.5f, 0f);
        public Vector3 MinSize = new Vector3(5f, 1f, 1f);
        public float MinY = -5;
        public int NumberOfObjects = 6;
        public List<PhysicMaterial> PhysicMaterials;
        public PowerUp Powerup;
        public Transform Prefab;
        public float RecycleOffset = 20;
        public Vector3 StartPosition = new Vector3(0f, 0f, 0f);

        #endregion Public Fields

        #region Private Fields

        private Vector3 _nextPosition;
        private Queue<Transform> _objectQueue = null;

        #endregion Private Fields

        #region Private Methods

        private void GameOver()
        {
            enabled = false;

            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }

        private void GameStart()
        {
            _nextPosition = StartPosition;

            for (int i = 0; i < NumberOfObjects; i++)
            {
                Recycle();
            }
            enabled = true;
        }

        /// <summary>
        /// Recycles our platforms when off screen
        /// </summary>
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
            Powerup.SpawnIfAvailable(position);

            Transform o = _objectQueue.Dequeue();
            o.localScale = scale;
            o.localPosition = position;

            int materialIndex = Random.Range(0, Materials.Length);
            o.GetComponent<Renderer>().material = Materials[materialIndex];
            o.GetComponent<Collider>().material = PhysicMaterials[materialIndex];
            _nextPosition.x += scale.x;
            _objectQueue.Enqueue(o);

            _nextPosition += new Vector3(
                Random.Range(MinGap.x, MaxGap.x) + scale.x,
                Random.Range(MinGap.y, MaxGap.y),
                Random.Range(MinGap.z, MaxGap.z));

            if (_nextPosition.y < MinY)
            {
                _nextPosition.y = MinY + MaxGap.y;
            }
            else if (_nextPosition.y > MaxY)
            {
                _nextPosition.y = MaxY - MaxGap.y;
            }
        }

        // Use this for initialization
        private void Start()
        {
            GameEventManager.GameStart += GameStart;
            GameEventManager.GameOver += GameOver;

            // create our queue of objects
            _objectQueue = new Queue<Transform>(NumberOfObjects);

            for (int i = 0; i < NumberOfObjects; i++)
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