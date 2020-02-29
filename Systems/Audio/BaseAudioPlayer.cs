using System;
using UnityEngine;

namespace UtilityScripts
{
    public class BaseAudioPlayer : UniqueSingleton<BaseAudioPlayer>
    {
        [NonSerialized] [EnumType(typeof(int))] public EnumObject [] audioSources;
        [NonSerialized] public EnumObject [] audioDatas;

        protected override void Awake ()
        {
            base.Awake();
            audioSources = new EnumObject [ audioDatas.Length ];
            for ( int i = 0; i < audioSources.Length; i++ )
            {
                EnumObject enumObject = new EnumObject();
                AudioSource audioSource = new GameObject($"AudioSource {i}").AddComponent<AudioSource>();
                audioSource.transform.SetParent( gameObject.transform );
                audioSource.clip = audioDatas [ i ]._object as AudioClip;
                enumObject.enumAsInt = audioDatas [ i ].enumAsInt;
                enumObject._object = audioSource;
                audioSources [ i ] = enumObject;
            }
        }

        public void PlaySFX ( int id )
        {
            for ( int i = 0; i < audioSources.Length; i++ )
            {
                if(id == audioSources[i].enumAsInt)
                {
                var audioSource = ( audioSources [ i ]._object as AudioSource );
                    audioSource.Play();
                    break;
                }
            }

        }
    }
}
