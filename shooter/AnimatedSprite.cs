using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using shooter.Enum;

namespace shooter
{
    public class AnimatedSprite
    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 200f;
        int currentFrame = 0;
        int spriteWidth;
        int spriteHeight;
        Rectangle sourceRect;

        public AnimatedSprite(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            sourceRect = GetSourceRect();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(spriteTexture, position, sourceRect, Color.White, 0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
        }
        private Rectangle GetSourceRect()
        {
            return new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
        }
        public void HandleSpriteMovement(GameTime gameTime, Movement[] movements, bool isNewKeyboardState)
        {
            sourceRect = GetSourceRect();

            // This check is a little bit I threw in there to allow the character to sprint.
            // if (keysPressed.Any(k => k == Keys.Space))
            // {
            //     interval = 50;
            // }
            // else
            // {
            //     interval = 200;
            // }

            if (movements.Any(m => m == (Movement)Keys.Right))
            {
                Animate(gameTime, isNewKeyboardState, AnimationFrame.RIGHT_FIRST, AnimationFrame.RIGHT_LAST);
            }

            if (movements.Any(m => m == (Movement)Keys.Left))
            {
                Animate(gameTime, isNewKeyboardState, AnimationFrame.LEFT_FIRST, AnimationFrame.LEFT_LAST);
            }

            if (movements.Any(m => m == (Movement)Keys.Down))
            {
                Animate(gameTime, isNewKeyboardState, AnimationFrame.DOWN_FIRST, AnimationFrame.DOWN_LAST);
            }

            if (movements.Any(m => m == (Movement)Keys.Up))
            {
                Animate(gameTime, isNewKeyboardState, AnimationFrame.UP_FIRST, AnimationFrame.UP_LAST);
            }
        }
        private void Animate(GameTime gameTime, bool isFirstFrame, AnimationFrame firstFrame, AnimationFrame lastFrame)
        {
            if (isFirstFrame) currentFrame = (int)firstFrame;

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                 currentFrame++;
            
                 if (currentFrame > (int)lastFrame)
                 {
                     currentFrame = (int)firstFrame;
                 }
                 timer = 0f;
            }
        }
    }
}