// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : raan0
// Created          : 06-04-2017
//
// Last Modified By : raan0
// Last Modified On : 06-04-2017
// ***********************************************************************
// <copyright file="Runner.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Scripts namespace.
/// </summary>
namespace Assets.Scripts
{
    using Managers;
    using UnityEngine;

    /// <summary>
    /// Class Runner.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Runner : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The distance traveled
        /// </summary>
        public static int DistanceTraveled;

        /// <summary>
        /// The acceleration
        /// </summary>
        public float Acceleration = 5f;

        /// <summary>
        /// The boost velocity
        /// </summary>
        public Vector3 BoostVelocity;

        /// <summary>
        /// The game over y
        /// </summary>
        public float GameOverY;

        /// <summary>
        /// The jump velocity
        /// </summary>
        public Vector3 JumpVelocity;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// The boosts
        /// </summary>
        private static int _boosts;

        /// <summary>
        /// The maximum distance traveled
        /// </summary>
        private float _maximumDistanceTraveled;

        /// <summary>
        /// The start position
        /// </summary>
        private Vector3 _startPosition;

        /// <summary>
        /// The touching platform
        /// </summary>
        private bool _touchingPlatform;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Adds the boost.
        /// </summary>
        public static void AddBoost()
        {
            _boosts++;
            GUIManager.SetPowerUps(_boosts);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// FixedUpdate = Same as Update, except give room for physics to handle
        /// </summary>
        private void FixedUpdate()
        {
            // if we're on a platform, let him accelerate
            if (_touchingPlatform)
            {
                GetComponent<Rigidbody>().AddForce(Acceleration, 0f, 0f, ForceMode.Acceleration);
            }
        }

        /// <summary>
        /// Games the over.
        /// </summary>
        private void GameOver()
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            enabled = false;
            SaveMaximumDistanceTraveled();
        }

        /// <summary>
        /// Games the start.
        /// </summary>
        private void GameStart()
        {
            _boosts = 5;
            DistanceTraveled = 0;

            GUIManager.SetPowerUps(_boosts);
            GUIManager.SetDistance(DistanceTraveled);

            transform.localPosition = _startPosition;

            GetComponent<Renderer>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
            enabled = true;

            LoadMaximumDistanceTraveled();
        }

        /// <summary>
        /// Loads the maximum distance traveled.
        /// </summary>
        private void LoadMaximumDistanceTraveled()
        {
            _maximumDistanceTraveled = PlayerPrefs.GetInt("_maximumDistanceTraveled", 0);
        }

        /// <summary>
        /// Called when [collision enter].
        /// </summary>
        private void OnCollisionEnter()
        {
            _touchingPlatform = true;
        }

        /// <summary>
        /// Called when [collision exit].
        /// </summary>
        private void OnCollisionExit()
        {
            _touchingPlatform = false;
        }

        /// <summary>
        /// Saves the maximum distance traveled.
        /// </summary>
        private void SaveMaximumDistanceTraveled()
        {
            PlayerPrefs.SetFloat("_maximumDistanceTraveled", _maximumDistanceTraveled);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            GameEventManager.GameStart += GameStart;
            GameEventManager.GameOver += GameOver;

            _startPosition = transform.localPosition;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            enabled = false;
        }

        // Update is called once per frame
        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Update_Android();
                    break;

                default:
                    Update_Desktop();
                    break;
            }

            DistanceTraveled = (int)transform.localPosition.x;

            if (DistanceTraveled > _maximumDistanceTraveled)
            {
                _maximumDistanceTraveled = DistanceTraveled;
            }
            GUIManager.SetDistance(DistanceTraveled);
            GUIManager.SetMaximumDistance(_maximumDistanceTraveled);

            // we fell into oblivion!
            if (transform.localPosition.y < GameOverY)
            {
                GameEventManager.TriggerGameOver();
            }
        }

        /// <summary>
        /// Updates the android.
        /// </summary>
        private void Update_Android()
        {
            // if there is a tap
            if (Input.touchCount > 0)
            {
                // are we on the platform?
                switch (_touchingPlatform)
                {
                    case true:
                        GetComponent<Rigidbody>().AddForce(JumpVelocity, ForceMode.VelocityChange);
                        _touchingPlatform = false;

                        break;

                    default:
                        if ((Input.GetTouch(0).phase == TouchPhase.Began) && (Input.GetTouch(0).tapCount == 2))
                        {
                            // we double tapped
                            if (_boosts > 0)
                            {
                                // we use boost!
                                GetComponent<Rigidbody>().AddForce(BoostVelocity, ForceMode.VelocityChange);
                                _boosts -= 1;
                                GUIManager.SetPowerUps(_boosts);
                            }
                        }
                        break;
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
                switch (_touchingPlatform)
                {
                    case true:
                        GetComponent<Rigidbody>().AddForce(JumpVelocity, ForceMode.VelocityChange);
                        _touchingPlatform = false;

                        break;

                    default:
                        if (_boosts > 0)
                        {
                            GetComponent<Rigidbody>().AddForce(BoostVelocity, ForceMode.VelocityChange);
                            _boosts -= 1;
                            GUIManager.SetPowerUps(_boosts);
                        }

                        break;
                }
            }
        }

        #endregion Private Methods
    }
}