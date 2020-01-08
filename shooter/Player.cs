using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using shooter.Enum;

namespace shooter
{
    public class Player
    {
        public Vector2 Position;
        public bool Active;
        public int Health;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private AnimatedSprite _playerAnimation;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            Width = 53;
            Height = 62;
            var sprite = new AnimatedSprite(texture, 1, Width, Height);
            _playerAnimation = sprite;
            Position = position;
            Active = true;
            Health = 100;
        }
        public void Update(GameTime gameTime, float positionY, float positionX, Keys[] keysPressed, bool isNewKeyPressed)
        {
            _playerAnimation.HandleSpriteMovement(gameTime, GetMovementsFromKeys(keysPressed).ToArray(), isNewKeyPressed);
            this.Position.Y = positionY;
            this.Position.X = positionX;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _playerAnimation.Draw(spriteBatch, this.Position);
        }
        private List<Movement> GetMovementsFromKeys(Keys[] keys)
        {
            var movements = new List<Movement>();
            if (keys.Any(k => (Movement)k == Movement.UP))
                movements.Add(Movement.UP);
            if (keys.Any(k => (Movement)k == Movement.DOWN))
                movements.Add(Movement.DOWN);
            if (keys.Any(k => (Movement)k == Movement.LEFT))
                movements.Add(Movement.LEFT);
            if (keys.Any(k => (Movement)k == Movement.RIGHT))
                movements.Add(Movement.RIGHT);

            return movements;
        }
    }
}