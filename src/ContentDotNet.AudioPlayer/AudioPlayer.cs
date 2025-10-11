namespace ContentDotNet.AudioPlayer;

/// <summary>
///   The AudioPlayer class enables audio playback functionality.
/// </summary>
public abstract class AudioPlayer
{
    /// <summary>
    ///   The method begins playing audio.
    /// </summary>
    /// <remarks>
    ///   In <see cref="ThreadedAudioPlayer"/>, this function plays audio in the
    ///   background without blocking the caller thread. In <see cref="StackableAudioPlayer"/>,
    ///   same case, but it also allows stacking multiple audio tracks - e.g. you
    ///   can have multiple audios playing at once. <see cref="DefaultAudioPlayer"/>
    ///   will block the caller thread until the audio finishes playing.
    /// </remarks>
    public abstract void BeginPlay();

    /// <summary>
    ///   Abruptly stops audio playback.
    /// </summary>
    public abstract void StopPlay();

    /// <summary>
    ///   <see langword="true"/> when audio finished playing with <see cref="StopPlay()"/>
    ///   never being invoked.
    /// </summary>
    public abstract bool FinishedPlaying { get; }

    /// <summary>
    ///   Audio volume in percentages, 100 is 100% volume, 0 is muted. Default value is 100.
    /// </summary>
    public abstract float Volume { get; set; }

    /// <summary>
    ///   <see langword="true"/> when audio is currently playing.
    /// </summary>
    public abstract bool IsPlaying { get; }

    /// <summary>
    ///   <see langword="true"/> when <see cref="StopPlay()"/> was invoked.
    /// </summary>
    public abstract bool Stopped { get; }

    /// <summary>
    ///   Elapsed time since the audio started playing.
    /// </summary>
    public abstract TimeSpan Elapsed { get; set; }

    /// <summary>
    ///   Total audio duration.
    /// </summary>
    public abstract TimeSpan Duration { get; }
}
