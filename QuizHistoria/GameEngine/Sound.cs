using SDL2;
using System;

namespace QuizHistoria
{
    class Sound
    {
        IntPtr chunck;
        int volume = MIX.MIX_MAX_VOLUME;

        public static Sound CreateSound(string path)
        {
            return new Sound(path);
        }

        Sound(string path)
        {
            chunck = MIX.Mix_LoadWAV(path);
            MIX.Mix_VolumeChunk(chunck, volume);
        }

        public void Play(int loop)
        {
            MIX.Mix_PlayChannel(-1, chunck, loop);
        }

        public void SetVolume(int volume)
        {
            this.volume = volume;
            MIX.Mix_VolumeChunk(chunck, volume);
        }
    }
}
