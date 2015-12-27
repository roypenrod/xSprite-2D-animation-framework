/* ************************************************************
 * Namespace: xSprite
 * Class:  StaticSpriteTemplate
 * 
 * Class to define a template for sprites without animations.
 * 
 * You can create as many static sprite objects as you need
 * from a single static sprite template.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace xSprite
{

    /// <summary>
    /// Class to define a template for sprites without animations.  You can create as many static sprite objects as you need from a single static sprite template.
    /// </summary>
    public class StaticSpriteTemplate : StaticSprite
    {

        #region MEMBERS

        // name used to identify the template
        private string _templateName;

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Returns the template name.  Type: string
        /// </summary>
        public string TemplateName
        {
            get { return _templateName; } 
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for the StaticSpriteTemplate class.
        /// </summary>
        /// <param name="templateName">The internal name used to identify this StaticSpriteTemplate object.  Type: string</param>
        /// <param name="spriteSheetTexture">Reference to the sprite sheet containing the static sprite.  Type: Texture2D</param>
        /// <param name="spriteSheetTexturePositionX">The horizontal position of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTexturePositionY">The vertical position of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTextureWidth">The width of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTextureHeight">The height of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteColorTint">The color of the tint applied to the static sprite.  Type: Color</param>
        /// <param name="alphaTransparencyLevel">The transparency level of the static sprite.  It must be set to a value between 0 and 255. 0 is not completely transparent.  Type: byte</param>        
        /// <param name="rotationAmountInRadians">The amount of rotation in radians applied to the static sprite.  If you prefer to work with degrees, use MathHelper.ToRadians() to convert the degrees into radians.  Type:  float</param>        
        /// <param name="rotationAnchorPoint">The anchor point the static sprite rotates around.  Type: RotationAnchorPoint (enum)</param>
        /// <param name="scaleFactor">The amount of scaling applied to the static sprite.  Type: float</param>        
        /// <param name="transformEffect">Determines whether the static sprite is flipped horizontally, flipped vertically, or not flipped.  Type: SpriteEffects</param>
        /// <param name="layer">The layer the static sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float</param>        
        public StaticSpriteTemplate(string templateName, Texture2D spriteSheetTexture, int spriteSheetTexturePositionX, int spriteSheetTexturePositionY, int spriteSheetTextureWidth, int spriteSheetTextureHeight, Color spriteColorTint, byte alphaTransparencyLevel, float rotationAmountInRadians, RotationAnchorPoint rotationAnchorPoint, float scaleFactor, SpriteEffects transformEffect, float layer) : base(spriteSheetTexture, spriteSheetTexturePositionX, spriteSheetTexturePositionY, spriteSheetTextureWidth, spriteSheetTextureHeight, spriteColorTint, alphaTransparencyLevel, rotationAmountInRadians, rotationAnchorPoint, scaleFactor, transformEffect, layer)
        {
            // passes all the other parameters on to the 
            // StaticSprite constructor then assigns 
            // the template name parameter
            _templateName = templateName;
        }

        #endregion


        #region METHODS
        #endregion

    } // <-- end StaticSpriteTemplate class
}
