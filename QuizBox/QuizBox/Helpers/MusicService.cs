using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBox.Helpers
{
    class MusicService
    {
        private static MusicService _instance;
        public static MusicService Instance => _instance ??= new MusicService();

        private IAudioPlayer _player;
        private bool _isInitialized;
        private const string MusicPrefKey = "MusicIsEnabled";

        public bool IsPlaying => _player?.IsPlaying ?? false;
        public bool IsEnabled => Preferences.Get(MusicPrefKey, true);

        private MusicService() { }

        public async Task InitAsync()
        {
            if (_isInitialized) return;

            var file = await FileSystem.OpenAppPackageFileAsync("theme.mp3");
            _player = AudioManager.Current.CreatePlayer(file);
            _player.Loop = true;

            _isInitialized = true;

            if (IsEnabled)
                _player.Play();
        }

        public void ToggleMusic()
        {
            if (_player == null) return;

            if (_player.IsPlaying)
            {
                _player.Pause();
                Preferences.Set(MusicPrefKey, false);
            }
            else
            {
                _player.Play();
                Preferences.Set(MusicPrefKey, true);
            }
        }

        public void StopMusic()
        {
            _player?.Pause();
        }

        public void StartMusic()
        {
            if (_player != null && !IsPlaying && IsEnabled)
                _player.Play();
        }
    }
}
