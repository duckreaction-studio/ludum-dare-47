using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public interface ISoundManager
    {
        void PlaySound(string soundCategory, AudioSource audioSource = null);
    }

    public class SoundManager : MonoBehaviour, ISoundManager
    {
        private Dictionary<string, RandomSoundPlayer> soundPlayers = new Dictionary<string, RandomSoundPlayer>();

        void Start()
        {
            var components = GetComponentsInChildren<RandomSoundPlayer>(true);
            foreach (var component in components)
            {
                soundPlayers.Add(component.gameObject.name, component);
            }
        }

        public void PlaySound(string soundCategory, AudioSource audioSource = null)
        {
            RandomSoundPlayer player;
            if (soundPlayers.TryGetValue(soundCategory, out player))
            {
                player.PlaySound(audioSource);
            }
        }
    }
}