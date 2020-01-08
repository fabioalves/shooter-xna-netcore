using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using shooter.Enum;

namespace shooter
{
    public class Enemy
    {
        // animation represneting the enemy.
        public AnimatedSprite EnemyAnimation;

        // The postion of the enemy ship relative to the 
        // top of left corner of the screen
        public Vector2 Position;

        // state of the enemy ship
        public bool Active;

        // Hit points of the enemy, if this goes
        // to zero the enemy dies.      
        public int Health;

        // the amount of damage that the enemy
        // ship inflicts on the player.
        public int Damage;

        // the amount of the score enemy is worth.
        public int Value;
        private Rectangle safeArea;

        // Get the width of the enemy ship
        public int Width
        {
            get; private set;
        }

        // Get the height of the enemy ship.
        public int Height
        {
            get; private set;
        }
        private Movement nextMovement = Movement.RIGHT;

        public float enemyMoveSpeed;
        private float elapsedTime;
        private int direction;

        public void Initialize(Texture2D texture,
            Vector2 position, Rectangle safeArea)
        {
            this.safeArea = safeArea;
            Width = 69;
            Height = 64;
            // load the enemy ship texture;
            EnemyAnimation = new AnimatedSprite(texture, 1, Width, Height);

            // set the postion of th enemy ship
            Position = position;

            // set the enemy to be active
            Active = true;

            // set the health of the enemy
            Health = 10;

            // Set the amount of damage the enemy does
            Damage = 10;

            // Set how fast the enemy moves.
            enemyMoveSpeed = 1f;

            // set the value of the enemy
            Value = 100;
        }

        public void Update(GameTime gameTime)
        {
            // Update the postion of the anaimation
            EnemyAnimation.HandleSpriteMovement(gameTime, new Movement[] { nextMovement }, false);

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 1.5f)
            {
                elapsedTime -= 1.5f; //Subtract the 1 second we've already checked
                direction = new Random().Next(1, 4); //Set the direction to a random value (0 or 1)
            }

            if (direction == 1)
            {
                Position.X -= enemyMoveSpeed;
                nextMovement = Movement.LEFT;
            }
            if (direction == 2)
            {
                Position.X += enemyMoveSpeed;
                nextMovement = Movement.RIGHT;
            }
            if (direction == 3)
            {
                Position.Y -= enemyMoveSpeed;
                nextMovement = Movement.UP;
            }
            if (direction == 4)
            {
                Position.Y += enemyMoveSpeed;
                nextMovement = Movement.DOWN;
            }
            

            /* If the enenmy is past the screen or its
             * health reaches 0 then deactivate it. */
            if (Health <= 0)
            {
                //deactivate the enemy
                Active = false;

            }
            if (Position.X < -Width)
            {
                Position.X = Position.X + Width;
            }
            if (Position.Y > this.safeArea.Height - Height)
            {
                Position.Y = this.safeArea.Height - Height;
            }
        }

        private Movement GetNextMovement()
        {
            return (Movement)new Random().Next(37, 40);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw the animation
            EnemyAnimation.Draw(spriteBatch, this.Position);
        }
    }
}