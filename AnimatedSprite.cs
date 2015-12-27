/* ************************************************************
 * Namespace: xSprite
 * Class:  AnimatedSprite
 * 
 * Class for sprites with animations.
 * 
 * Animated Sprites contain Animations.
 * Animations contain Frames.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xSprite
{
    /// <summary>
    /// Class for sprites with animations.  Animated Sprites contain Animations.  Animations contain Frames.
    /// </summary>
    public class AnimatedSprite : IxSpriteDrawable, IxSpriteUpdatable
    {
        #region MEMBERS

        /// <summary>
        /// The array of Animation objects.
        /// </summary>
        public Animation[] Animations;

        // flag to determine whether the animated sprite is drawn to the screen.  If true, the animated sprite is drawn to the screen at the DrawPosition.  If false, the static sprite is not drawn to the screen. 
        private bool _visible;

        // holds the position where the animated sprite 
        // should be drawn on the screen
        private Vector2 _drawPosition;

        // layerDepth is a float between 0 and 1
        // 0 is the front layer, 1 is the back layer
        private float _layerDepth;

        // index number for the current animation in the Animations array
        private Nullable<int> _currentAnimationIndex;        

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Flag to determine whether the animated sprite is drawn to the screen.  If true, the animated sprite is drawn to the screen at the DrawPosition.  If false, the animated sprite is not drawn to the screen.  Type: bool
        /// </summary>
        public bool Visible
        {
            get { return _visible; }

            set { _visible = value; }
        }

        /// <summary>
        /// The point on the screen where the animated sprite is drawn.  Starts drawing at the top left corner of the sprite.  Type: Vector2
        /// </summary>
        public Vector2 DrawPosition
        {
            get { return _drawPosition; }

            set { _drawPosition = value; }
        }

        /// <summary>
        /// The layer the animated sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float
        /// </summary>
        public float Layer
        {
            get { return _layerDepth; }

            set
            {
                if (value < 0.0f)
                    value = 0.0f;

                if (value > 1.0f)
                    value = 1.0f;

                _layerDepth = value;
            }
        }

        /// <summary>
        /// Returns the name of the animation that is currently playing for this animated sprite.  Type: string
        /// </summary>
        public string CurrentAnimationName
        {
            get { return Animations[(int)_currentAnimationIndex].Name; } 
        }

        /// <summary>
        /// Returns true if the current animation is finished playing.  Returns false if the current animation is still playing.  This will always return false if the animation is set to an infinite loop.  Type: bool
        /// </summary>
        public bool CurrentAnimationFinished
        {
            get { return Animations[(int)_currentAnimationIndex].Finished; }
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for the AnimatedSprite class.
        /// </summary>
        /// <param name="layer">The layer the animated sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float</param>
        /// <param name="numberOfAnimations">The number of Animation objects attached to the animated sprite.  Must be greater than 0.  Type: int</param>
        public AnimatedSprite(float layer, int numberOfAnimations)
        {
            // _visible flag starts out as false
            _visible = false;

            // _drawPosition starts out at 0,0
            _drawPosition = new Vector2(0, 0);

            // checks to make sure layer is between 0.0f and 1.0f
            // then sets the layerDepth
            if (layer < 0.0f)
                layer = 0.0f;

            if (layer > 1.0f)
                layer = 1.0f;

            _layerDepth = layer;

            // checks to make sure there is at least one Animation object
            if (numberOfAnimations < 0)
                numberOfAnimations = 0;

            if (numberOfAnimations == 0)
            {
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("==================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.AnimatedSprite Constructor");
                System.Diagnostics.Debug.WriteLine("You cannot create an animated sprite with");
                System.Diagnostics.Debug.WriteLine("0 or fewer Animation objects.");
                System.Diagnostics.Debug.WriteLine("You must have at least 1 Animation object for");
                System.Diagnostics.Debug.WriteLine("an animated sprite.");
                System.Diagnostics.Debug.WriteLine("==================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new System.ArgumentException("You cannot create an animated sprite with 0 or fewer Animation objects.  Check the Debug Output for more information.");
            }

            // initializes the Animations array
            Animations = new Animation[numberOfAnimations];

            // initializes the _currentAnimationIndex to 0            
            _currentAnimationIndex = 0;
        }

        #endregion


        #region METHODS

        /// <summary>
        /// Tells the animated sprite to start playing this animation.  You must call the PlayAnimation() method before the Update() and Draw() methods are called.
        /// </summary>
        /// <param name="animationName">Name of the animation you want to play.</param>
        public void PlayAnimation(string animationName)
        {
            // loop through the Animations array and find the animationName
            for (int count = 0; count < Animations.Length; count++)
            {
                if (Animations[count].Name == animationName)
                {
                    // if we find it, call the Animation Play() method and set
                    // the current animation
                    Animations[count].Play();
                    _currentAnimationIndex = count;
                    return;
                }
            }

            // we couldn't find the animationName
            // throw an exception
            System.Diagnostics.Debug.WriteLine(" ");
            System.Diagnostics.Debug.WriteLine("=========================================================================");
            System.Diagnostics.Debug.WriteLine("ERROR in xSprite.AnimatedSprite.PlayAnimation() method");
            System.Diagnostics.Debug.WriteLine("Could not find an animation with with the " + animationName + " name.");
            System.Diagnostics.Debug.WriteLine("Please verify you supplied the correct name and");
            System.Diagnostics.Debug.WriteLine("that you created an Animation object with that");
            System.Diagnostics.Debug.WriteLine("name for this animated sprite.");
            System.Diagnostics.Debug.WriteLine("=========================================================================");
            System.Diagnostics.Debug.WriteLine(" ");

            throw new System.InvalidOperationException("Could not find an animation with the " + animationName + " name.  See the Debug Output for more information.");
        }


        /// <summary>
        /// Updates the animated sprite.
        /// </summary>
        /// <param name="gameTime">Reference to a GameTime object.</param>
        public void Update(GameTime gameTime)
        {
            // only updates the animated sprite if it is visible
            if (_visible == true)            
                Animations[(int)_currentAnimationIndex].Update(gameTime);
        }


        /// <summary>
        /// Draws the animated sprite to the screen at the DrawPosition if the Visible flag is set to true.
        /// </summary>
        /// <param name="spriteBatch">Reference to a SpriteBatch object.</param>
        public void Draw(SpriteBatch spriteBatch)
        {    
            // only draws the animated sprite if it is visible
            if (_visible == true)            
                Animations[(int)_currentAnimationIndex].Frames[Animations[(int)_currentAnimationIndex].CurrentFrame].Draw(spriteBatch, _drawPosition, _layerDepth);
            
        }

        #endregion

    } // <-- end AnimatedSprite class
} // <-- end xSprite namespace
