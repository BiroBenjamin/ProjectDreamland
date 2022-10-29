using Microsoft.Xna.Framework;

namespace ProjectDreamland.Components
{
  public class Timer
  {
    public float Time { get; set; }

    private readonly float _timeLimit;

    public Timer(float timeLimit)
    {
      Time = 0f;
      _timeLimit = timeLimit;
    }

    public float Count(GameTime gameTime)
    {
      Time += (float)gameTime.ElapsedGameTime.TotalSeconds / 2;
      float timeRemaining = Time >= _timeLimit ? 0 : _timeLimit - Time;
      return timeRemaining;
    }

    public void Reset()
    {
      Time = 0f;
    }
  }
}
