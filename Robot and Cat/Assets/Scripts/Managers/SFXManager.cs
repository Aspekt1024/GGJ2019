using System.Collections.Generic;
using UnityEngine;

namespace RobotCat.Audio
{
    public enum SFX
    {
        Meow, CupImpactFloor, CupImpactMug, CupImpactWall, CupTable, PawCup, PawSwipe
    }


    public class SFXManager : MonoBehaviour
    {

        public float SFXDelay = 1.2f;


        public AudioClip[] MeowClips;
        public AudioClip[] CupImpactFloorClips;
        public AudioClip[] CupImpactMugClips;
        public AudioClip[] CupImpactWallClips;
        public AudioClip[] CupTableClips;
        public AudioClip[] PawCupClips;
        public AudioClip[] PawSwipeClips;

        public AudioSource MainSource;
        public AudioSource PitchShiftSource;
        
        private readonly Dictionary<SFX, float> cooldownPresets = new Dictionary<SFX, float>()
        {
            { SFX.Meow, 0f },
            { SFX.CupImpactFloor, 0f },
            { SFX.CupImpactMug, 0.1f },
            { SFX.CupImpactWall, 0f },
            { SFX.CupTable, 0f },
            { SFX.PawCup, 0.2f },
            { SFX.PawSwipe, 0f },
        };

        private readonly Dictionary<SFX, float> cooldowns = new Dictionary<SFX, float>()
        {
            { SFX.Meow, 0f },
            { SFX.CupImpactFloor, 0f },
            { SFX.CupImpactMug, 0f },
            { SFX.CupImpactWall, 0f },
            { SFX.CupTable, 0f },
            { SFX.PawCup, 0f },
            { SFX.PawSwipe, 0f },
        };

        private readonly Dictionary<SFX, float> volumes = new Dictionary<SFX, float>()
        {
            { SFX.Meow, 0.7f },
            { SFX.CupImpactFloor, 1f },
            { SFX.CupImpactMug, 0.7f },
            { SFX.CupImpactWall, 0.7f },
            { SFX.CupTable, 0.7f },
            { SFX.PawCup, 0.7f },
            { SFX.PawSwipe, 0.7f },
        };

        private void Update()
        {
            var sfxEnums = (SFX[])System.Enum.GetValues(typeof(SFX));
            for (int i = 0; i < sfxEnums.Length; i++)
            {
                if (cooldowns[sfxEnums[i]] < 0) continue;
                cooldowns[sfxEnums[i]] -= Time.deltaTime;
            }
        }

        public void PlayRandom(AudioClip[] clips, bool requirePitchShift = false, float volume = 0.7f)
        {
            if (requirePitchShift)
            {
                PitchShiftSource.PlayOneShot(GetRandomClip(clips), volume);
            }
            else
            {
                MainSource.PlayOneShot(GetRandomClip(clips), volume);
            }
        }

        public AudioClip GetRandomClip(AudioClip[] clips)
        {
            int clipIndex = Random.Range(0, clips.Length);
            return clips[clipIndex];
        }

        public void PlayRandom(SFX sfx)
        {
            if (cooldowns[sfx] > 0f) return;
            
            switch (sfx)
            {
                case SFX.Meow:
                    PlayRandom(MeowClips, true, volumes[sfx]);
                    break;
                case SFX.CupImpactFloor:
                    PlayRandom(CupImpactFloorClips, false, volumes[sfx]);
                    break;
                case SFX.CupImpactMug:
                    PlayRandom(CupImpactMugClips, false, volumes[sfx]);
                    break;
                case SFX.CupImpactWall:
                    PlayRandom(CupImpactWallClips, false, volumes[sfx]);
                    break;
                case SFX.CupTable:
                    PlayRandom(CupTableClips, false, volumes[sfx]);
                    break;
                case SFX.PawCup:
                    PlayRandom(PawCupClips, false, volumes[sfx]);
                    break;
                case SFX.PawSwipe:
                    PlayRandom(PawSwipeClips, false, volumes[sfx]);
                    break;
                default:
                    break;
            }

            cooldowns[sfx] = cooldownPresets[sfx];
        }
        
    }
}
