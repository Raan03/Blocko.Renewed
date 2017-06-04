// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : raan0
// Created          : 06-04-2017
//
// Last Modified By : raan0
// Last Modified On : 06-04-2017
// ***********************************************************************
// <copyright file="GameEventManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Assets.Scripts.Managers
{
    using UnityEngine;

    /// <summary>
    /// Class GameEventManager.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class GameEventManager : MonoBehaviour
    {
        #region Public Delegates

        /// <summary>
        /// Delegate GameEvent
        /// </summary>
        public delegate void GameEvent();

        #endregion Public Delegates

        #region Public Events

        /// <summary>
        /// Occurs when [game start].
        /// </summary>
        public static event GameEvent GameStart, GameOver;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Triggers the game over.
        /// </summary>
        public static void TriggerGameOver()
        {
            if (GameOver != null)
            {
                GameOver();
            }
        }

        /// <summary>
        /// Triggers the game start.
        /// </summary>
        public static void TriggerGameStart()
        {
            if (GameStart != null)
            {
                GameStart();
            }
        }

        #endregion Public Methods

        #region Private Methods

        // Use this for initialization
        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
        }

        // Update is called once per frame
        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
        }

        #endregion Private Methods
    }
}