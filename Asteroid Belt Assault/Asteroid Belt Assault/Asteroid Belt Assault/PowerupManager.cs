using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Belt_Assault
{
    class PowerupManager
    {
        Texture2D spriteSheet;
        private PlayerManager playerManager;
        public List<Sprite> Powerups = new List<Sprite>();
        public Sprite powerups;
        private bool newPowerup;

        public PowerupManager(Texture2D spriteSheet, PlayerManager playerManager)
        {
            this.spriteSheet = spriteSheet;
            this.playerManager = playerManager;

            Powerups.Add(new Sprite(new Vector2(300, 300), spriteSheet, new Rectangle(355, 181, 97, 93), Vector2.Zero));
        }

        public void Update(GameTime gameTime)
        {
           powerups.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            powerups.Draw(spriteBatch);
        }
    }
}
