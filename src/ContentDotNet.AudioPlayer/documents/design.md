# The AudioPlayer Design

The AudioPlayer is a component that allows users to play audio files within the application. It supports various audio formats and provides controls for playback, volume adjustment, and track navigation.

First, we have a method, `BeginPlay()` which begins audio playback process.

We also have `StopPlay()` to stop playback.

We have `FinishedPlaying` boolean property which indicates whether the audio playback has finished.

As well as `Stopped` which is invoked if `StopPlay()` was invoked.

Then we have `TimeSpan Elapsed { get; set; }` which returns the elapsed time of the currently playing audio track.

We have `TimeSpan Duration { get; }` which returns the total duration of the currently playing audio track.

`SetVolume(float volume)` sets the volume of the audio player.

`float Volume { get; set; }` is the volume, which is the percentage with 100 being default.

`bool IsPlaying { get; }` is if audio is playing right now.

# Flavors

We have:
`DefaultAudioPlayer` which blocks the thread until `BeginPlay()` is completed.

`StackableAudioPlayer` which allows multiple audio tracks to be played simultaneously without blocking the thread. `StopPlay()` here stops all playing audios.

`ThreadedAudioPlayer` is the same as `StackableAudioPlayer` but does not allow `BeginPlay()` to be invoked while audio is playing.
