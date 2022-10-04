﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectDreamland.Data.GameFiles.Characters;
using ProjectDreamland.UI;

namespace ProjectDreamland.Handlers
{
  public class UIHandler
  {
    private int _screenWidth;
    private int _screenHeight;

    private Player _player;
    private ExperienceBar _experienceBar;

    public UIHandler(int screenWidth, int screenHeight, Player player, ContentManager content)
    {
      _screenWidth = screenWidth;
      _screenHeight = screenHeight;
      _player = player;
      _experienceBar = new ExperienceBar(content);
    }

    public void Update(GameTime gameTime)
    {
      _experienceBar.Update(gameTime, _player.ExperienceNeeded, _player.CurrentExperience, _screenWidth, _player.Level);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
      _experienceBar.Draw(gameTime, spriteBatch, graphicsDevice, 0, _screenHeight);
    }
  }
}
