// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : raan0
// Created          : 06-04-2017
//
// Last Modified By : raan0
// Last Modified On : 06-04-2017
// ***********************************************************************
// <copyright file="AvertisingManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Assets.Scripts.Managers
{
    using UnityEngine;
    using UnityEngine.Advertisements;

    /// <summary>
    /// Class AdvertisingManager.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class AdvertisingManager : MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Shows the advertisement.
        /// </summary>
        public void ShowAdvertisement()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
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