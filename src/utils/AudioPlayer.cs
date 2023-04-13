using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Media;

namespace SpaceShooter.utils
{
    public class AudioPlayer : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;
        private SoundPlayer? backgroundMusicPlayer;

        public static readonly AudioPlayer Player = new AudioPlayer(44100, 2);

        private AudioPlayer(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new WaveOutEvent();
            var waveFormatter = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);
            
            mixer = new MixingSampleProvider(waveFormatter)
            {
                ReadFully = true
            };

            outputDevice.Init(mixer);
            ActivateOutputDevice();

            backgroundMusicPlayer = null;
        }

        public void PlaySound(CachedSound sound)
        {
            ISampleProvider input = new CachedSoundSampleProvider(sound);
            input = convertToRightChannelCount(input);
            mixer.AddMixerInput(input);
        }

        public void ActivateOutputDevice() => outputDevice.Play();

        public void MuteOutputDevice() => outputDevice.Stop();

        public void PlayBackgroundMusic(Stream audioStream)
        {
            backgroundMusicPlayer = new SoundPlayer(audioStream);    
            backgroundMusicPlayer.PlayLooping();
        }

        public void StopBackgroundMusic()
        {
            if (backgroundMusicPlayer == null)
                return;

            backgroundMusicPlayer.Stop();
        }

        public void Dispose() => outputDevice.Dispose();

        private ISampleProvider convertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
                return input;
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
                return new MonoToStereoSampleProvider(input);

            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

    }

    public class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }
        
        public CachedSound(Stream audioStream)
        {
            using var audioFileReader = new WaveFileReader(audioStream);
            WaveFormat = audioFileReader.WaveFormat;
            var sp = audioFileReader.ToSampleProvider();
            var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
            var sourceSamples = (int)(audioFileReader.Length / (audioFileReader.WaveFormat.BitsPerSample / 8));
            var sampleData = new float[sourceSamples];

            int samplesread;
            while ((samplesread = sp.Read(sampleData, 0, sourceSamples)) > 0)
                wholeFile.AddRange(sampleData.Take(samplesread));
            AudioData = wholeFile.ToArray();
        }
    }

    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound cachedSound;
        private long position;

        public CachedSoundSampleProvider(CachedSound cachedSound)
            => this.cachedSound = cachedSound;

        public WaveFormat WaveFormat { get { return cachedSound.WaveFormat; } }

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = cachedSound.AudioData.Length - position;
            var samplesToCopy = Math.Min(availableSamples, count);

            Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy);
            position += samplesToCopy;
            
            return (int)samplesToCopy;
        }
    }

    public class AutoDisposeFileReader : ISampleProvider
    {
        private readonly AudioFileReader reader;
        private bool isDisposed;

        public WaveFormat WaveFormat { get; private set; }

        public AutoDisposeFileReader(AudioFileReader reader)
        {
            this.reader = reader;
            WaveFormat = reader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (isDisposed)
                return 0;

            int read = reader.Read(buffer, offset, count);
            if (read == 0)
            {
                reader.Dispose();
                isDisposed = true;
            }

            return read;
        }
    }
}
