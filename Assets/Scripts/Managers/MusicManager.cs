namespace Assets.Scripts.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class MusicManager : MonoBehaviour
    {
        #region Public Fields

        public List<AudioClip> MyMusic;

        #endregion Public Fields

        #region Private Fields

        private static List<int> _played = new List<int>();

        #endregion Private Fields

        #region Private Methods

        private void DisplayMusic()
        {
            try
            {
                GUIManager.SetMusic(GetComponent<AudioSource>().clip.name);
            }
            catch (System.Exception e)
            {
                print(string.Format("Caught MusicManager->PlayRandomMusic::SetMusic() Exception: {0}", e.Message));
            }
        }

        private void PlayRandomMusic()
        {
            // make sure we have music available [...]
            if (MyMusic.Count == 0)
            {
                return;
            }

            // if we played all available music, reset the list!
            if (_played.Count == MyMusic.Count)
            {
                _played.Clear();
            }

            int rng = Random.Range(0, MyMusic.Count);

            while (_played.Contains(rng))
            {
                // so we don't play music already played while others aren't heard of yet
                rng = Random.Range(0, MyMusic.Count);
            }

            _played.Add(rng);

            GetComponent<AudioSource>().clip = MyMusic[rng];
            GetComponent<AudioSource>().Play();

            DisplayMusic();
        }

        private void Start()
        {
            PlayRandomMusic();
        }

        private void Update()
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                PlayRandomMusic();
            }
        }

        #endregion Private Methods
    }
}