using Microsoft.Xna.Framework;

namespace ProjectDreamland.Components
{
  public class Timer
  {
    private float _time;
    private readonly float _timeLimit;

    public Timer(float timeLimit)
    {
      _time = 0f;
      _timeLimit = timeLimit;
    }

    public float Count(GameTime gameTime)
    {
      _time += (float)gameTime.ElapsedGameTime.TotalSeconds / 2;
      float timeRemaining = _time >= _timeLimit ? 0 : _timeLimit - _time;
      return timeRemaining;
    }

    public void Reset()
    {
      _time = 0f;
    }
  }
}
