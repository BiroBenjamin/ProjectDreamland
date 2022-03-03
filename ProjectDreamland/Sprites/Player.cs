using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDreamland.Sprites{
    public class Player : Sprite {
        private KeyboardState keyState = Keyboard.GetState();
        public Player(Texture2D texture) : base(texture, true) { }

        private void Move(GameTime gameTime, List<Sprite> components) {
            keyState = Keyboard.GetState();
            velocity = new Vector2();

            // Getting player movement input
            if (keyState.IsKeyDown(Keys.W))
                velocity.Y -= speed;
            if (keyState.IsKeyDown(Keys.S))
                velocity.Y += speed;
            if (keyState.IsKeyDown(Keys.A))
                velocity.X -= speed;
            if (keyState.IsKeyDown(Keys.D))
                velocity.X += speed;

            Collision(components);
            Position += velocity;
        }
        private void Collision(List<Sprite> components) {
            foreach (Sprite comp in components) {
                if(comp == this || !comp.Collideable)
                    continue;

                if ((this.velocity.X > 0 && this.isCollidingLeft(comp)) ||
                    (this.velocity.X < 0 && this.isCollidingRight(comp)))
                    this.velocity.X = 0;
                if ((this.velocity.Y > 0 && this.isCollidingTop(comp)) ||
                    (this.velocity.Y < 0 && this.isCollidingBottom(comp)))
                    this.velocity.Y = 0;

            }
        }

        public override void Update(GameTime gameTime, List<Sprite> components) {
            Move(gameTime, components);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
