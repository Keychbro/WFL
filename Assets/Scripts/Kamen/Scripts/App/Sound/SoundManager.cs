using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen
{
    public class SoundManager : SingletonComponent<SoundManager>
    {
        #region Enums

        public enum SoundType
        {
            SoundEffect,
            Music
        }

        #endregion

        #region Classes

        [Serializable] private class SoundInfo
        {
            #region SoundInfo Variables

            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;
            [SerializeField] private SoundType _type;
            [SerializeField] private bool _playAndLoopOnStart;
            [SerializeField] [Range(0f, 1f)] private float _volume;

            #endregion

            #region SoundInfo Properties

            public string ID { get => _id; }
            public AudioClip Clip { get => _clip; }
            public SoundType Type { get => _type; }
            public bool PlayAndLoopOnStart { get => _playAndLoopOnStart; }
            public float Volume { get => _volume; }

            #endregion
        }
        private class PlayingSound
        {
            #region PlayingSound Variables

            [SerializeField] private SoundInfo _info;
            [SerializeField] private AudioSource _source;

            #endregion

            #region PlayingSound Properties

            public SoundInfo Info { get => _info; }
            public AudioSource Source { get => _source; }

            #endregion

            #region Other

            public PlayingSound(SoundInfo info, AudioSource source)
            {
                _info = info;
                _source = source;
            }

            #endregion
        }

        #endregion

        #region Variables 

        [SerializeField] private SoundInfo[] _soundInfos;
        [SerializeField] private Dictionary<string,SoundInfo> _souvndInfos;
        private List<PlayingSound> PlayingAudioSources = new List<PlayingSound>();
        private List<PlayingSound> LoopingAudioSources = new List<PlayingSound>();

        #endregion

        #region Properties

        public bool IsMusicOn { get; private set; }
        public bool IsSoundEffectsOn { get; private set; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            Initialize();
            PlayAtStart(SoundType.SoundEffect);
            PlayAtStart(SoundType.Music);
        }
        private void Update()
        {
            for (int i = 0; i < PlayingAudioSources.Count; i++)
            {
                AudioSource audioSource = PlayingAudioSources[i].Source;
                if (!audioSource.isPlaying)
                {
                    Destroy(audioSource.gameObject);
                    PlayingAudioSources.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion

        #region Control Methods 

        private void Initialize()
        {
            IsSoundEffectsOn = true;
            IsMusicOn = true;
        }
        public void Play(string id, bool isLoop = false, float playDelay = 0)
        {
            SoundInfo soundInfo = GetSoundInfoByID(id);

            if (soundInfo == null)
            {
                Debug.LogError($"[Kamen - SoundManager] Sound with id \"{id}\" does not exist!");
                return;
            }

            if ((soundInfo.Type == SoundType.Music && !IsMusicOn) || (soundInfo.Type == SoundType.SoundEffect && !IsSoundEffectsOn)) return;

            AudioSource audioSource = CreateAudioSource(soundInfo.ID);

            audioSource.clip = soundInfo.Clip;
            audioSource.loop = isLoop;
            audioSource.time = 0;
            audioSource.volume = soundInfo.Volume;

            if (playDelay > 0) audioSource.PlayDelayed(playDelay);
            else audioSource.Play();

            PlayingSound playingSound = new PlayingSound(soundInfo, audioSource);

            if (isLoop) LoopingAudioSources.Add(playingSound);
            else PlayingAudioSources.Add(playingSound);
        }
        private void PlayAtStart(SoundType type)
        {
            for (int i = 0; i < _soundInfos.Length; i++)
            {
                if (_soundInfos[i].Type == type && _soundInfos[i].PlayAndLoopOnStart) Play(_soundInfos[i].ID, true, 0);
            }
        }

        public void Stop(string id)
        {
            StopAllSounds(id, PlayingAudioSources);
            StopAllSounds(id, LoopingAudioSources);
        }
        public void Stop(SoundType type)
        {
            StopAllSounds(type, PlayingAudioSources);
            StopAllSounds(type, LoopingAudioSources);
        }
        private void StopAllSounds(string id, List<PlayingSound> playingSounds)
        {
            for (int i = 0; i < playingSounds.Count; i++)
            {
                if (id == playingSounds[i].Info.ID)
                {
                    playingSounds[i].Source.Stop();
                    Destroy(playingSounds[i].Source.gameObject);
                    playingSounds.RemoveAt(i);
                    break;
                }
            }
        }
        private void StopAllSounds(SoundType type, List<PlayingSound> playingSounds)
        {
            for (int i = 0; i < playingSounds.Count; i++)
            {
                if (type == playingSounds[0].Info.Type)
                {
                    playingSounds[0].Source.Stop();
                    Destroy(playingSounds[0].Source.gameObject);
                    playingSounds.RemoveAt(0);
                    i--;
                }
            }
        }

        public void SetSoundTypeOnOff(SoundType type, bool isOn)
        {
            switch (type)
            {
                case SoundType.SoundEffect:
                    if (isOn == IsSoundEffectsOn) return;
                    IsSoundEffectsOn = isOn;
                    break;
                case SoundType.Music:
                    if (isOn == IsMusicOn) return;
                    IsMusicOn = isOn;
                    break;
            }

            if (!isOn) Stop(type);
            else PlayAtStart(type);
        }

        #endregion

        #region Calculate Methods

        private SoundInfo GetSoundInfoByID(string id)
        {
            for (int i = 0; i < _soundInfos.Length; i++)
            {
                if (id == _soundInfos[i].ID) return _soundInfos[i];
            }
            return null;
        }
        private AudioSource CreateAudioSource(string id)
        {
            GameObject obj = new GameObject("sound_" + id);
            obj.transform.SetParent(transform);
            return obj.AddComponent<AudioSource>();
        }

        #endregion
    }
}