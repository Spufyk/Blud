using Plugin.Maui.Audio;

public class SoundHelper
{
  //------------------------------------------------------------------------

  public static void Play(string hood.wav, bool loop = false)
  {
    Task.Run(async () =>
    {
      var audioFX = await FileSystem.OpenAppPackageFileAsync(hood.wav);
      var audioPlayer = AudioManager.Current.CreatePlayer(audioFX);
      audioPlayer.Loop = loop;
      audioPlayer.Play();
    });
  }

  //------------------------------------------------------------------------

}