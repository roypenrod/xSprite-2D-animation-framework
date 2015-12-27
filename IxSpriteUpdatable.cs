/* ************************************************************
 * Namespace: xSprite
 * Interface:  IxSpriteUpdatable
 * 
 * Interface to allow the SpriteManager to update
 * the animated sprites.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;

namespace xSprite
{
    /// <summary>
    /// Interface to allow the SpriteManager to update the animated sprites.
    /// </summary>
    interface IxSpriteUpdatable
    {
        #region PROPERTIES
        #endregion


        #region METHODS

        void Update(GameTime gameTime);

        #endregion
    } // <-- end IxSpriteUpdatable interface
}
