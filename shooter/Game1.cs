using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace shooter
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Enemy enemy;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        float playerMoveSpeed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            enemy = new Enemy();
            playerMoveSpeed = 7.0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var safeArea = GraphicsDevice.Viewport.TitleSafeArea;
            var playerPosition = new Vector2(safeArea.X,
                safeArea.Y + safeArea.Height / 2);

            var enemyPosition = new Vector2(safeArea.X + 300,
            safeArea.Y + safeArea.Height / 2);

            player.Initialize(Content.Load<Texture2D>("Graphics\\sprite-sheet-cat"), playerPosition);
            enemy.Initialize(Content.Load<Texture2D>("Graphics\\sprite-sheet-rat"), enemyPosition, safeArea);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            UpdatePlayer(gameTime, currentKeyboardState, previousKeyboardState);
            enemy.Update(gameTime);
            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            HandleKeyboardUpdatePosition(gameTime);
            HandleOutOfBoundPosition();
        }

        private void HandleOutOfBoundPosition()
        {
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
        }

        private void HandleKeyboardUpdatePosition(GameTime gameTime)
        {
            var isNewKeyPressed = currentKeyboardState != previousKeyboardState;
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player.Update(gameTime, player.Position.Y, player.Position.X - playerMoveSpeed, currentKeyboardState.GetPressedKeys(), isNewKeyPressed);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.Update(gameTime, player.Position.Y, player.Position.X + playerMoveSpeed, currentKeyboardState.GetPressedKeys(), isNewKeyPressed);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player.Update(gameTime, player.Position.Y - playerMoveSpeed, player.Position.X, currentKeyboardState.GetPressedKeys(), isNewKeyPressed);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                player.Update(gameTime, player.Position.Y + playerMoveSpeed, player.Position.X, currentKeyboardState.GetPressedKeys(), isNewKeyPressed);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
