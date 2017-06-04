// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : raan0
// Created          : 06-04-2017
//
// Last Modified By : raan0
// Last Modified On : 06-04-2017
// ***********************************************************************
// <copyright file="GUIManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Assets.Scripts.Managers
{
    using UnityEngine;

    /// <summary>
    /// Class GUIManager.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class GUIManager : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The blocko text
        /// </summary>
        public GUIText BlockoText;

        /// <summary>
        /// The corporate logo
        /// </summary>
        public MeshRenderer CorporateLogo;

        /// <summary>
        /// The distance text
        /// </summary>
        public GUIText DistanceText;

        /// <summary>
        /// The game over text
        /// </summary>
        public GUIText GameOverText;

        /// <summary>
        /// The instructions text
        /// </summary>
        public GUIText InstructionsText;

        /// <summary>
        /// The maximum distance text
        /// </summary>
        public GUIText MaximumDistanceText;

        /// <summary>
        /// The music text
        /// </summary>
        public GUIText MusicText;

        /// <summary>
        /// The power up text
        /// </summary>
        public GUIText PowerUpText;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// The instance
        /// </summary>
        private static GUIManager _instance;

        #endregion Private Fields

        #region Public Methods

        // draw distance text on screen
        /// <summary>
        /// Sets the distance.
        /// </summary>
        /// <param name="distance">The distance.</param>
        public static void SetDistance(float distance)
        {
            if (_instance == null)
            {
                return;
            }
            _instance.DistanceText.text = string.Format("Distance: {0}", distance.ToString("f0"));
        }

        // draw maximum achieved distance on screen
        /// <summary>
        /// Sets the maximum distance.
        /// </summary>
        /// <param name="maximumDistance">The maximum distance.</param>
        public static void SetMaximumDistance(float maximumDistance)
        {
            if (null == _instance)
            {
                return;
            }
            _instance.MaximumDistanceText.text = string.Format("Maximum Distance: {0}", maximumDistance.ToString("f0"));
        }

        // What is playing now, Einstein?
        /// <summary>
        /// Sets the music.
        /// </summary>
        /// <param name="nowPlaying">The now playing.</param>
        public static void SetMusic(string nowPlaying)
        {
            if (null == _instance)
            {
                return;
            }

            _instance.MusicText.text = string.Format("Now Playing: {0}", nowPlaying);
        }

        // draw powerups text on screen
        /// <summary>
        /// Sets the power ups.
        /// </summary>
        /// <param name="powerUp">The power up.</param>
        public static void SetPowerUps(int powerUp)
        {
            if (null == _instance)
            {
                return;
            }
            _instance.PowerUpText.text = string.Format("PowerUps: {0}", powerUp);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles what should be done when the Game is over
        /// </summary>
        private void GameOver()
        {
            GameOverText.enabled = true;
            InstructionsText.enabled = true;
            enabled = true;
        }

        /// <summary>
        /// Handles what should be done when starting the game
        /// </summary>
        private void GameStart()
        {
            CorporateLogo.enabled = false;
            GameOverText.enabled = false;
            InstructionsText.enabled = false;
            BlockoText.enabled = false;
            enabled = false;
        }

        // Use this for initialization
        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            _instance = this;

            GameEventManager.GameStart += GameStart;
            GameEventManager.GameOver += GameOver;

            GameOverText.enabled = false;
            CorporateLogo.enabled = true;

            _instance.MusicText.fontSize = Common.GenerateDynamicFontSize(_instance.MusicText.fontSize);

            _instance.PowerUpText.fontSize = Common.GenerateDynamicFontSize(_instance.PowerUpText.fontSize);

            _instance.MaximumDistanceText.fontSize = Common.GenerateDynamicFontSize(_instance.MaximumDistanceText.fontSize);

            _instance.DistanceText.fontSize = Common.GenerateDynamicFontSize(_instance.DistanceText.fontSize);

            // for android.
            if (Application.platform == RuntimePlatform.Android)
            {
                // read from our localization instead of hardcoded text?
                InstructionsText.text = "Double tap to start.";
            }
        }

        // Update is called once per frame
        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            // Logic depends on our platform
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Update_Android();
                    break;

                default:
                    Update_Desktop();
                    break;
            }
        }

        /// <summary>
        /// Updates the android.
        /// </summary>
        private void Update_Android()
        {
            // we tapped
            if (Input.touchCount > 0)
            {
                // we double tapped
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).tapCount == 2)
                {
                    GameEventManager.TriggerGameStart();
                }
            }
        }

        /// <summary>
        /// Updates the desktop.
        /// </summary>
        private void Update_Desktop()
        {
            if (Input.GetButtonDown("Jump"))
            {
                GameEventManager.TriggerGameStart();
            }
        }

        #endregion Private Methods
    }
}