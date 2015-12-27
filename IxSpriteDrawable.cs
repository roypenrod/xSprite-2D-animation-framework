/* ************************************************************
 * Namespace: xSprite
 * Interface:  IxSpriteDrawable
 * 
 * Interface to allow the SpriteManager to draw both
 * static sprites and animated sprites.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xSprite
{
    /// <summary>
    /// Interface to allow the SpriteManger to draw both static sprites and animated sprites.
    /// </summary>
    interface IxSpriteDrawable
    {
        #region PROPERTIES       

        bool Visible
        {
            get;
            set;
        }

        #endregion


        #region METHODS

        void Draw(SpriteBatch spriteBatch);

        #endregion
    } // <-- end IxSpriteDrawable interface
}
