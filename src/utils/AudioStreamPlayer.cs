using NAudio.Wave;

namespace SpaceShooter.utils
{
    public class AudioStreamPlayer
    {
        private readonly RawSourceLoopStream formattedAudioStream;
        private readonly WaveOut audioOutput;

        public AudioStreamPlayer(Stream audioStream)
        {
            var audioFormat = new WaveFormat(rate: 44100, bits: 16, channels: 1);
            formattedAudioStream = new RawSourceLoopStream(audioStream, audioFormat);
            audioOutput = new WaveOut();
            audioOutput.Init(formattedAudioStream);
        }

        public void Play(bool isLooped = false)
        {
            formattedAudioStream.EnableLooping = isLooped;
            formattedAudioStream.ReloadStream();
            audioOutput.Play();
        }

        public void Stop() => audioOutput.Stop();
    }

    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class RawSourceLoopStream : RawSourceWaveStream
    {
        private readonly Stream sourceStream;

        public bool EnableLooping { get; set; }

        /// <summary>
        /// Creates a new Loop stream
        /// </summary>
        /// <param name="sourceStream">The stream to read from. Note: the Read
        /// method of this stream should return 0 when it reaches the end or 
        /// else we will not loop to the start again.</param>
        public RawSourceLoopStream(Stream sourceStream, WaveFormat waveFormat) : base(sourceStream, waveFormat)
        {
            this.sourceStream = sourceStream;
            EnableLooping = false;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                        // something wrong with the source stream
                        break;
                    ReloadStream();
                }
                totalBytesRead += bytesRead;
            }
            
            return totalBytesRead;
        }

        public void ReloadStream() => sourceStream.Position = 0;
    }
}
