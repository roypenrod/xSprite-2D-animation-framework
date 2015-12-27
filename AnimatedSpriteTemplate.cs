/* ************************************************************
 * Namespace: xSprite
 * Class:  AnimatedSpriteTemplate
 * 
 * Class to define a template for sprites with animations.
 * 
 * You can create as many animated sprite objects as you need
 * from a single animated sprite template.
 * 
 * ************************************************************ */

using System;

namespace xSprite
{
    /// <summary>
    /// Class to define a template for sprites with animations.  You can create as many animated sprite objects as you need from a single animated sprite template.
    /// </summary>
    public class AnimatedSpriteTemplate : AnimatedSprite
    {

        #region MEMBERS

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
        /// Constructor for the AnimatedSpriteTemplate class.
        /// </summary>
        /// <param name="templateName">The internal name used to identify this AnimatedSpriteTemplate object.  Type: string</param>
        /// <param name="layer">The layer the animated sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float</param>
        /// <param name="numberOfAnimations">The number of Animation objects attached to the animated sprite.  Must be greater than 0.  Type: int</param>
        public AnimatedSpriteTemplate(string templateName, float layer, int numberOfAnimations)
            : base(layer, numberOfAnimations)
        {
            // passes all of the other parameters on to the
            // AnimatedSprite constructor then assigns
            // the templateName
            _templateName = templateName;
        }

        #endregion


        #region METHODS
        #endregion

    } // <-- end AnimatedSpriteTemplate class
}
