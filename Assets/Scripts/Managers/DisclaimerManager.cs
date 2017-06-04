// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : raan0
// Created          : 06-04-2017
//
// Last Modified By : raan0
// Last Modified On : 06-04-2017
// ***********************************************************************
// <copyright file="DisclaimerManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Assets.Scripts.Managers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Handles our disclaimer text on start up
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class DisclaimerManager : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The disclaimer
        /// </summary>
        public GUIText Disclaimer;

        #endregion Public Fields

        #region Private Methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            Disclaimer.fontSize = Common.GenerateDynamicFontSize(Disclaimer.fontSize);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            if (Time.time > 5)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }

        #endregion Private Methods
    }
}