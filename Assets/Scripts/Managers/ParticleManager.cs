namespace Assets.Scripts.Managers
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// Handles our particles to look less bland
    /// </summary>
    public class ParticleManager : MonoBehaviour
    {
        #region Public Fields

        public List<ParticleSystem> ParticleSystems;

        #endregion Public Fields

        #region Private Methods

        private void GameOver()
        {
            ParticleSystems.ForEach(d =>
            {
                var em = d.emission;
                em.enabled = false;
            });
        }

        private void GameStart()
        {
            ParticleSystems.ForEach(d =>
            {
                var em = d.emission;
                d.Clear();
                em.enabled = true;
            });
        }

        // Use this for initialization
        private void Start()
        {
            GameEventManager.GameStart += GameStart;
            GameEventManager.GameOver += GameOver;
            GameOver();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        #endregion Private Methods
    }
}