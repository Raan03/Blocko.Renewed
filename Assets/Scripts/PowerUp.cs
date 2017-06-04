// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : raan0
// Created          : 06-04-2017
//
// Last Modified By : raan0
// Last Modified On : 06-04-2017
// ***********************************************************************
// <copyright file="PowerUp.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Assets.Scripts
{
    using Assets.Scripts.Managers;
    using UnityEngine;

    /// <summary>
    /// Class PowerUp.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PowerUp : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The offset
        /// </summary>
        public Vector3 Offset;

        /// <summary>
        /// The recycle offset
        /// </summary>
        public int RecycleOffset;

        /// <summary>
        /// The rotation velocity
        /// </summary>
        public Vector3 RotationVelocity;

        /// <summary>
        /// The spawn chance
        /// </summary>
        public float SpawnChance;

        #endregion Public Fields

        #region Public Methods

        /// <summary>
        /// Will spawn our powerup if the RNG gods are in our favor
        /// </summary>
        /// <param name="position">The position.</param>
        public void SpawnIfAvailable(Vector3 position)
        {
            float rng = Random.Range(0f, 100f) / 100;

            if (gameObject.activeSelf || SpawnChance <= rng)
            {
                return;
            }
            transform.localPosition = position + Offset;
            gameObject.SetActive(true);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Games the over.
        /// </summary>
        private void GameOver()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Called when [trigger enter].
        /// </summary>
        private void OnTriggerEnter()
        {
            Runner.AddBoost();
            gameObject.SetActive(false);
        }

        // Use this for initialization
        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            GameEventManager.GameOver += GameOver;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            if (transform.localPosition.x + RecycleOffset < Runner.DistanceTraveled)
            {
                gameObject.SetActive(false);
                return;
            }
            transform.Rotate(RotationVelocity * Time.deltaTime);
        }

        #endregion Private Methods
    }
}