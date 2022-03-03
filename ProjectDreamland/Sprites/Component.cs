using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDreamland.Sprites {
    public abstract class Component {
        public abstract void Update(GameTime gameTime, List<Sprite> components);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
