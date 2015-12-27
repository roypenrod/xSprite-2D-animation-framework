/* ************************************************************
 * Namespace: xSprite
 * Class:  Animation
 * 
 * Class for animations.
 * 
 * Animated Sprites consist of Animations. 
 * Animations consist of Frames.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;


namespace xSprite
{
    /// <summary>
    /// Class for animations.  Animated Sprites consist of Animations.  Animations consist of Frames.
    /// </summary>
    public class Animation
    {
        #region MEMBERS

        // animation name
        private string _name;

        /// <summary>
        /// The array of Frame objects.
        /// </summary>
        public Frame[] Frames;

        // current frame in the animation sequence
        private int _currentFrame;

        // delay between frames in
        // the animation sequence
        private float _delayBetweenFrames;

        // time until the next frame in
        // the animation sequence
        private float _timeToNextFrame;

        // number of times to play 
        // through the animation
        // set it to 0 for an infinite loop
        private int _numberOfLoops;

        // number of times left to 
        // play through the animation
        private int _loopsLeft;

        // flag for telling when the
        // animation has finished
        private bool _finished;

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Returns the animation name.  Type: string
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Returns the current frame in the animation sequence.  Type: int
        /// </summary>
        public int CurrentFrame
        {
            get { return _currentFrame; }
        }

        /// <summary>
        /// Returns the number of seconds to delay before switching frames while drawing the animation.  Type: float
        /// </summary>
        public float DelayBetweenFrames
        {
            get { return _delayBetweenFrames; }
        }

        /// <summary>
        /// Returns the number of times the animation should loop.  If NumberOfLoops is set to 0, it's an infinite loop.  Type: int
        /// </summary>
        public int NumberOfLoops
        {
            get { return _numberOfLoops; }

        }

        /// <summary>
        /// Returns true if the animation has finished playing.  Returns false if it is still playing.  Type: bool
        /// </summary>
        public bool Finished
        {
            get { return _finished; }
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for the Animation class.
        /// </summary>
        /// <param name="animationName">The name used to identify this animation.  Type: string</param>
        /// <param name="numberOfFrames">The number of Frame objects this animation is composed of.  Type: int</param>
        /// <param name="delayBetweenFrames">The number of seconds to delay before switching frames while drawing the animation.  Type: float</param>
        /// <param name="numberOfLoops">The number of times the animation should loop.  If you want an infinite loop, set numberOfLoops to 0.  Type: int</param>
        public Animation(string animationName, int numberOfFrames, float delayBetweenFrames, int numberOfLoops)
        {
            // sets the animation name
            _name = animationName;

            // initializes the array of frames
            Frames = new Frame[numberOfFrames];

            // sets the current frame to 
            // the start of the animation
            _currentFrame = 0;

            // sets the delay between frames
            _delayBetweenFrames = delayBetweenFrames;

            // sets the time until the next frame
            // to the delay between frames
            _timeToNextFrame = _delayBetweenFrames;

            // sets the number of times to play
            // through the animation
            _numberOfLoops = numberOfLoops;

            // sets the number of loops left
            // to play through the animation
            _loopsLeft = numberOfLoops;

            // checks to make sure there is at least one Frame object
            if (numberOfFrames < 0)
                numberOfFrames = 0;

            if (numberOfFrames == 0)
            {
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("==================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.Animation Constructor");
                System.Diagnostics.Debug.WriteLine("You cannot create an animation with");
                System.Diagnostics.Debug.WriteLine("0 or fewer Frame objects.");
                System.Diagnostics.Debug.WriteLine("You must have at least 1 Frame object for");
                System.Diagnostics.Debug.WriteLine("an animation.");
                System.Diagnostics.Debug.WriteLine("==================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new System.ArgumentException("You cannot create an animation with 0 or fewer Frame objects.  Check the Debug Output for more information.");
            }

            // initializes the array of frames
            Frames = new Frame[numberOfFrames];
        }

        #endregion


        #region METHODS

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="gameTime">Reference to a GameTime object.</param>
        public void Update(GameTime gameTime)
        {
            // counts down time to the next animation frame using delta time
            _timeToNextFrame -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            // check to see if the counter goes into the negative
            // and set it to 0 if it did
            if (_timeToNextFrame < 0)
                _timeToNextFrame = 0;

            // check to see if the current animation frame is finished
            if (_timeToNextFrame == 0)
            {
                // if _numberOfLoops is set to 0, keep
                // looping the animation infinitely
                if (_numberOfLoops == 0)
                {
                    // checks to see if it's in the last frame of the animation
                    // if it is, reset the current animation frame to 0
                    // if it's not, advance the current animation frame by 1
                    if (_currentFrame == (Frames.Length - 1))
                        _currentFrame = 0;
                    else
                        _currentFrame++;

                    // reset the counter
                    _timeToNextFrame = _delayBetweenFrames;                    
                }
                // if _numberofLoops is greater than 0,
                // keep looping through the animation until
                // _loopsLeft is 0
                else if (_numberOfLoops > 0)
                {
                    if (_loopsLeft > 0)
                    {
                        // checks to see if it's in the last frame of the animation
                        // if it is, reset the current animation frame to 0
                        // and decrease the number of loops left by 1
                        // if it's not, advance the current animation frame by 1
                        if (_currentFrame == (Frames.Length - 1))
                        {
                            // checks to see if there is more than one animation
                            // loop left
                            // if there is, reset the current animation frame to 0
                            // if there isn't, don't reset the current animation frame
                            if (_loopsLeft > 1)                            
                                _currentFrame = 0;                            

                            _loopsLeft--;

                            // checks to see if _loopsLeft drops below 0
                            // if it does, set it to 0
                            if (_loopsLeft < 0)
                                _loopsLeft = 0;

                            // checks to see if _loopsLeft is 0
                            // if it is, set the _finished flag to true
                            if (_loopsLeft == 0)
                                _finished = true;
                        }
                        else
                            _currentFrame++;

                        // reset the counter
                        _timeToNextFrame = _delayBetweenFrames;                                            
                    }
                }
                
            }
        }

        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        public void Play()
        {
            // sets the _finished flag to false
            // since the animation is just starting
            _finished = false;

            // resets the _currentFrame to the
            // start of the animation sequence
            _currentFrame = 0;

            // resets the _loopsLeft
            _loopsLeft = _numberOfLoops;
        }

        #endregion

    } // <-- end Animation class
} // <-- end xSprite namespace
