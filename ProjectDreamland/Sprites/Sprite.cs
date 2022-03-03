using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ProjectDreamland.Sprites {
    public class Sprite : Component{
        protected float speed;
        protected Vector2 velocity;
        public bool Collideable { get; set; }

        protected Texture2D _texture;
        public Vector2 Position { get; set; }
        public Rectangle Rectangle {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }

        public Sprite(Texture2D texture, bool collides) {
            _texture = texture;
            speed = 2f;
            velocity = new Vector2();
            Collideable = collides;
        }

        #region Collision
        protected bool isCollidingLeft(Sprite sprite) {
            return this.Rectangle.Right + this.velocity.X > sprite.Rectangle.Left &&
                   this.Rectangle.Right < sprite.Rectangle.Right &&
                   this.Rectangle.Top < sprite.Rectangle.Bottom &&
                   this.Rectangle.Bottom > sprite.Rectangle.Top;

        }
        protected bool isCollidingRight(Sprite sprite) {
            return this.Rectangle.Left + this.velocity.X < sprite.Rectangle.Right &&
                   this.Rectangle.Left > sprite.Rectangle.Left &&
                   this.Rectangle.Top < sprite.Rectangle.Bottom &&
                   this.Rectangle.Bottom > sprite.Rectangle.Top;

        }
        protected bool isCollidingTop(Sprite sprite) {
            return this.Rectangle.Bottom + this.velocity.Y > sprite.Rectangle.Top &&
                   this.Rectangle.Top < sprite.Rectangle.Top &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;

        }
        protected bool isCollidingBottom(Sprite sprite) {
            return this.Rectangle.Top + this.velocity.Y < sprite.Rectangle.Bottom &&
                   this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;

        }
        #endregion

        public override void Update(GameTime gameTime, List<Sprite> components) {
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
