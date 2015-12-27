/* ************************************************************
 * Namespace: xSprite
 * Class:  SpriteManager
 * 
 * Class to manage the xSprite sprite objects.
 * 
 * You can use one SpriteManager instance to manage all of 
 * the xSprite sprite objects.
 *  
 * ************************************************************ */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xSprite
{

    /// <summary>
    /// Class to manage the xSprite sprite objects.  You can use one SpriteManager instance to manage all of the xSprite sprite objects.
    /// </summary>
    public class SpriteManager
    {
        #region MEMBERS
        
        // holds all of the loaded static sprite templates
        private List<StaticSpriteTemplate> _staticSpriteTemplatesList;

        // holds all of the loaded animated sprite templates
        private List<AnimatedSpriteTemplate> _animatedSpriteTemplatesList;

        // holds all of the sprite objects created from templates
        private List<IxSpriteDrawable> _drawableSpriteObjectsList;

        // holds the animated sprite template object while
        // it's being constructed   
        // reset it to null after the animated sprite template
        // object is added to the animated sprite templates list
        private AnimatedSpriteTemplate _currentAnimatedSpriteTemplate;

        // holds all of the animated sprite objects created from templates
        private List<IxSpriteUpdatable> _updatableSpriteObjectsList;
        
        #endregion



        #region PROPERTIES

        #endregion
        
        
        
        #region CONSTRUCTORS
                
        /// <summary>
        /// Default constructor for the SpriteManager class.
        /// </summary>
        public SpriteManager()
        {
            // initializes the lists
            _staticSpriteTemplatesList = new List<StaticSpriteTemplate>();
            _animatedSpriteTemplatesList = new List<AnimatedSpriteTemplate>();
            _drawableSpriteObjectsList = new List<IxSpriteDrawable>();
            _updatableSpriteObjectsList = new List<IxSpriteUpdatable>();

            // sets the _currentAnimatedSpriteTemplate to null 
            // since we're not using it yet
            _currentAnimatedSpriteTemplate = null;
        }

        #endregion


        
        #region METHODS

        /// <summary>
        /// Draws all of the visible sprite objects currently managed by the SpriteManger.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IxSpriteDrawable spriteObject in _drawableSpriteObjectsList)
            {
                // if the object is visible, draw it
                if (spriteObject.Visible == true)
                    spriteObject.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Updates all of the animated sprite objects currently managed by the SpriteManager.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (IxSpriteUpdatable spriteObject in _updatableSpriteObjectsList)
                spriteObject.Update(gameTime);            
        }


        /// <summary>
        /// Creates a static sprite template.  You can create as many copies of a static sprite as you need from a single static sprite template.
        /// </summary>
        /// <param name="templateName">The internal name for the static sprite template.  Type: string</param>
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
        public void CreateStaticSpriteTemplate(string templateName, Texture2D spriteSheetTexture, int spriteSheetTexturePositionX, int spriteSheetTexturePositionY, int spriteSheetTextureWidth, int spriteSheetTextureHeight, Color spriteColorTint, byte alphaTransparencyLevel, float rotationAmountInRadians, RotationAnchorPoint rotationAnchorPoint, float scaleFactor, SpriteEffects transformEffect, float layer)
        {
            // checks to see if a template already exists with that name
            foreach (StaticSpriteTemplate template in _staticSpriteTemplatesList)
            {
                // if the template name exists, print out a 
                // message and thrown an exception
                if (template.TemplateName == templateName)
                {
                    System.Diagnostics.Debug.WriteLine(" ");
                    System.Diagnostics.Debug.WriteLine("===========================================================================");
                    System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.CreateStaticSpriteTemplate()");
                    System.Diagnostics.Debug.WriteLine("You tried to create a static sprite template with a template");
                    System.Diagnostics.Debug.WriteLine("name that already exists.");
                    System.Diagnostics.Debug.WriteLine("Template Name: " + templateName);
                    System.Diagnostics.Debug.WriteLine("===========================================================================");
                    System.Diagnostics.Debug.WriteLine(" ");

                    throw new Exception("A static sprite template with that name already exists.  See the Debug Output for more info.");
                }
            }

            // if the spriteSheetTexture is null,
            // thrown an exception
            if (spriteSheetTexture == null)
            {
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("===========================================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.CreateStaticSpriteTemplate()");
                System.Diagnostics.Debug.WriteLine("You tried to create a static sprite template with a null texture.");
                System.Diagnostics.Debug.WriteLine("You may have forgot to load the texture before using it in ");
                System.Diagnostics.Debug.WriteLine("CreateStaticSpriteTemplate().");
                System.Diagnostics.Debug.WriteLine("===========================================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new Exception("You tried to create a static sprite template with a null texture.  See the Debug Output for more info.");
            }

            // template isn't already in the list 
            // create it and add it to the list
            _staticSpriteTemplatesList.Add(new StaticSpriteTemplate(templateName, spriteSheetTexture, spriteSheetTexturePositionX, spriteSheetTexturePositionY, spriteSheetTextureWidth, spriteSheetTextureHeight, spriteColorTint, alphaTransparencyLevel, rotationAmountInRadians, rotationAnchorPoint, scaleFactor, transformEffect, layer));
        }

        /// <summary>
        /// Creates a static sprite object from a static sprite template.  If it can't find a template with the template name, it throws an exception. 
        /// </summary>
        /// <param name="templateName">The template name to copy the static sprite from.</param>
        /// <returns>StaticSprite</returns>
        public StaticSprite CreateNewStaticSpriteFromTemplate(string templateName)
        {
            // checks to see if the static sprite template in the list matches the template name parameter
            foreach (StaticSpriteTemplate staticSpriteTemplate in _staticSpriteTemplatesList)
            {
                if (staticSpriteTemplate.TemplateName == templateName)
                {
                    // adds the new static sprite to the sprite objects list
                    // then returns it
                    StaticSprite newStaticSprite = new StaticSprite(staticSpriteTemplate.SpriteSheetTexture, staticSpriteTemplate.SpriteSheetTexturePositionX, staticSpriteTemplate.SpriteSheetTexturePositionY, staticSpriteTemplate.SpriteSheetTextureWidth, staticSpriteTemplate.SpriteSheetTextureHeight, staticSpriteTemplate.ColorTint, staticSpriteTemplate.AlphaTransparencyLevel, staticSpriteTemplate.RotationAmount, staticSpriteTemplate.RotationAnchorPoint, staticSpriteTemplate.ScaleFactor, staticSpriteTemplate.TransformEffect, staticSpriteTemplate.Layer);
                    _drawableSpriteObjectsList.Add(newStaticSprite);                   
                    return newStaticSprite;
                }                
            }

            // couldn't find the static sprite template so throw an exception
            System.Diagnostics.Debug.WriteLine(" ");
            System.Diagnostics.Debug.WriteLine("===============================================================================");
            System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.CreateNewStaticSpriteFromTemplate() method");
            System.Diagnostics.Debug.WriteLine("Could not find a static sprite template with the specified name.");
            System.Diagnostics.Debug.WriteLine("Double check the spelling and make sure you actually created the.");
            System.Diagnostics.Debug.WriteLine("static sprite template with the CreateStaticSpriteTemplate() method.");
            System.Diagnostics.Debug.WriteLine("===============================================================================");
            System.Diagnostics.Debug.WriteLine(" ");

            throw new Exception("Could not find a static sprite template with the specified name.  See Debug Output for more information.");
        }

        /// <summary>
        /// Starts the process of creating an animated sprite template.  You can create as many copies of an animated sprite as you need from a single animated sprite template.
        /// </summary>
        /// <param name="templateName">The internal name for the animated sprite template.  Type: string</param>
        /// <param name="layer">The layer the animated sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float</param>
        /// <param name="numberOfAnimations">The number of Animation objects attached to the animated sprite.  Must be greater than 0.  Type: int</param>
        public void StartCreateAnimatedSpriteTemplate(string templateName, float layer, int numberOfAnimations)
        {
            // _currentAnimatedSpriteTemplate holds the AnimatedSpriteTemplate
            // object while we're constructing it            
            _currentAnimatedSpriteTemplate = new AnimatedSpriteTemplate(templateName, layer, numberOfAnimations);
        }

        /// <summary>
        /// Adds an animation to the current animated sprite template.  Call this once for each animation before you call any AddFrameToAnimation() methods.
        /// </summary>
        /// <param name="animationName">The name used to identify this animation.  Type: string</param>
        /// <param name="numberOfFrames">The number of Frame objects this animation is composed of.  Type: intThe number of Frame objects this animation is composed of.  Type: int</param>
        /// <param name="delayBetweenFrames">The number of seconds to delay before switching frames while drawing the animation.  Type: float</param>
        /// <param name="numberOfLoops">The number of times the animation should loop.  If you want an infinite loop, set numberOfLoops to 0.  Type: int</param>
        public void AddAnimationToAnimatedSpriteTemplate(string animationName, int numberOfFrames, float delayBetweenFrames, int numberOfLoops)
        {
            // go through the Aninmations array and look for the first null value
            // add the Animation to this index and return from the method
            try
            {
                for (int count = 0; count < _currentAnimatedSpriteTemplate.Animations.Length; count++)
                {
                    if (_currentAnimatedSpriteTemplate.Animations[count] == null)
                    {
                        _currentAnimatedSpriteTemplate.Animations[count] = new Animation(animationName, numberOfFrames, delayBetweenFrames, numberOfLoops);
                        return;
                    }
                }
            }
            // catches the exception thrown if _currentAnimatedSpriteTemplate is null
            catch (NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("===================================================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.AddAnimationToAnimatedSpriteTemplate() method");
                System.Diagnostics.Debug.WriteLine("The AnimatedSpriteTemplate object doesn't exist.");                
                System.Diagnostics.Debug.WriteLine("Make sure you call the StartCreateAnimatedSpriteTemplate() method");
                System.Diagnostics.Debug.WriteLine("before you call this method.");
                System.Diagnostics.Debug.WriteLine("===================================================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new Exception("The AnimatedSpriteTemplate object doesn't exist.  See the Debug Output for more information.");
            }
        }

        /// <summary>
        /// Adds a frame to an animation in the current animated sprite template.  Call this once for each frame in an animation before you call the EndCreateNewAnimatedSpriteTemplate() method.
        /// </summary>
        /// <param name="animationName">The name of the animation this frame should be attached to.</param>
        /// <param name="frameNumber">The number used to identify this Frame object.  Type: int</param>
        /// <param name="spriteSheetTexture">Reference to the sprite sheet containing the frame.  Type: Texture2D</param>
        /// <param name="spriteSheetTexturePositionX">The horizontal position of the frame on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTexturePositionY">The vertical position of the frame on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTextureWidth">The width of the frame on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTextureHeight">The height of the frame on the sprite sheet.  Type: int</param>
        /// <param name="spriteColorTint">The color of the tint applied to the frame.  Type: Color</param>
        /// <param name="alphaTransparencyLevel">The transparency level of the frame.  It must be set to a value between 0 and 255. 0 is not completely transparent.  Type: byte</param>
        /// <param name="rotationAmountInRadians">The amount of rotation in radians applied to the frame.  If you prefer to work with degrees, use MathHelper.ToRadians() to convert the degrees into radians.  Type:  float</param>
        /// <param name="rotationAnchorPoint">The anchor point the frame rotates around.  Type: RotationAnchorPoint (enum)</param>
        /// <param name="scaleFactor">The amount of scaling applied to the frame.  Type: float</param>
        /// <param name="transformEffect">Determines whether the frame is flipped horizontally, flipped vertically, or not flipped.  Type: SpriteEffects</param>
        public void AddFrameToAnimation(string animationName, int frameNumber, Texture2D spriteSheetTexture, int spriteSheetTexturePositionX, int spriteSheetTexturePositionY, int spriteSheetTextureWidth, int spriteSheetTextureHeight, Color spriteColorTint, byte alphaTransparencyLevel, float rotationAmountInRadians, RotationAnchorPoint rotationAnchorPoint, float scaleFactor, SpriteEffects transformEffect)
        {
            // look through the Animations array and find the animationName
            // then add this frame to that animation
            try
            {                                
                for (int animationsCount = 0; animationsCount < _currentAnimatedSpriteTemplate.Animations.Length; animationsCount++)
                {
                    if (_currentAnimatedSpriteTemplate.Animations[animationsCount].Name == animationName)
                    {
                        // if we find the animation with that name,
                        // search the Frames array for the first null value and 
                        // add the Frame object at that index
                        // then return from the method
                        for (int framesCount = 0; framesCount < _currentAnimatedSpriteTemplate.Animations[animationsCount].Frames.Length; framesCount++)
                        {
                            if (_currentAnimatedSpriteTemplate.Animations[animationsCount].Frames[framesCount] == null)
                            {
                                _currentAnimatedSpriteTemplate.Animations[animationsCount].Frames[framesCount] = new Frame(frameNumber, spriteSheetTexture, spriteSheetTexturePositionX, spriteSheetTexturePositionY, spriteSheetTextureWidth, spriteSheetTextureHeight, spriteColorTint, alphaTransparencyLevel, rotationAmountInRadians, rotationAnchorPoint, scaleFactor, transformEffect);
                                return;
                            }
                        }
                    }                    
                }

                // if we reach this point, we couldn't find the
                // animationName in the Animations array
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("================================================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.AddFrameToAnimation() method");
                System.Diagnostics.Debug.WriteLine("Couldn't find the animationName in the Animations array.");
                System.Diagnostics.Debug.WriteLine("Double check the spelling of the animationName.");
                System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");                
                System.Diagnostics.Debug.WriteLine("================================================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new Exception("Couldn't find the animationName in the Animations array.  See Debug Output for more information.");
            }
            // catches the exception thrown if _currentAnimatedSpriteTemplate is null
            catch (NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("==============================================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.AddFrameToAnimation() method");
                System.Diagnostics.Debug.WriteLine("The AnimatedSpriteTemplate object doesn't exists or you tried");                
                System.Diagnostics.Debug.WriteLine("to add a frame to an animation that doesn't exist.");
                System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");                
                System.Diagnostics.Debug.WriteLine("==============================================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new Exception("The AnimatedSpriteTemplate object doesn't exists or you tried to add a frame to an animation that doesn't exist.  See the Debug Output for more information.");
            }
        }

        /// <summary>
        /// Call this method after you've finished adding all the animations and frames to the current animated sprite template.  It checks to make sure the animated sprite template is valid.  If it's not, it will throw exceptions explaining what's wrong.
        /// </summary>
        public void EndCreateAnimatedSpriteTemplate()
        {
            try
            {
                // check to see if there is at least 1 Animation
                // if not, throw an exception
                if (_currentAnimatedSpriteTemplate.Animations.Length < 1)
                {
                    System.Diagnostics.Debug.WriteLine(" ");
                    System.Diagnostics.Debug.WriteLine("============================================================================");
                    System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.EndCreateNewAnimatedSpriteTemplate()");
                    System.Diagnostics.Debug.WriteLine("You must have at least 1 Animation object attached to the");
                    System.Diagnostics.Debug.WriteLine("AnimatedSpriteTemplate.");
                    System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                    System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                    System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                    System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                    System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                    System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");
                    System.Diagnostics.Debug.WriteLine("============================================================================");
                    System.Diagnostics.Debug.WriteLine(" ");

                    throw new Exception("You must have at least 1 Animation object attached to the AnimatedSpriteTemplate.  See the Debug Output for more info.");
                }

                // loop through the Animations array and see if there are any
                // null values in the array
                for (int animationsCount = 0; animationsCount < _currentAnimatedSpriteTemplate.Animations.Length; animationsCount++)
                {
                    // if it finds a null value, throw an exception
                    if (_currentAnimatedSpriteTemplate.Animations[animationsCount] == null)
                    {
                        System.Diagnostics.Debug.WriteLine(" ");
                        System.Diagnostics.Debug.WriteLine("============================================================================");
                        System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.EndCreateNewAnimatedSpriteTemplate()");
                        System.Diagnostics.Debug.WriteLine("The Animations array contains a null value.  You either forgot");
                        System.Diagnostics.Debug.WriteLine("to add an Animation or you specified too many animations in");
                        System.Diagnostics.Debug.WriteLine("the StartCreateNewAnimatedSpriteTemplate() method.");
                        System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                        System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                        System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                        System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                        System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                        System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");
                        System.Diagnostics.Debug.WriteLine("============================================================================");
                        System.Diagnostics.Debug.WriteLine(" ");

                        throw new Exception("The Animations array contains a null value.  See the Debug Output for more info.");
                    }
                    // if it doesn't find a null, check through the Frames array
                    // and see if there are any null values in that array
                    // then check to see if any of the textures are null
                    else
                    {
                        for (int framesCount = 0; framesCount < _currentAnimatedSpriteTemplate.Animations[animationsCount].Frames.Length; framesCount++)
                        {
                            // if it finds a null value, throw an exception
                            if (_currentAnimatedSpriteTemplate.Animations[animationsCount].Frames[framesCount] == null)
                            {
                                System.Diagnostics.Debug.WriteLine(" ");
                                System.Diagnostics.Debug.WriteLine("============================================================================");
                                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.EndCreateNewAnimatedSpriteTemplate()");
                                System.Diagnostics.Debug.WriteLine("The Frames array contains a null value.  You either forgot");
                                System.Diagnostics.Debug.WriteLine("to add a Frame to an animation or you specified too many frames in");
                                System.Diagnostics.Debug.WriteLine("the AddAnimationToAnimatedSpriteTemplate() method.");
                                System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                                System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                                System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                                System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                                System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                                System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");
                                System.Diagnostics.Debug.WriteLine("============================================================================");
                                System.Diagnostics.Debug.WriteLine(" ");

                                throw new Exception("The Frames array contains a null value.  See the Debug Output for more info.");
                            }

                            // check to see if any of the textures are null
                            if (_currentAnimatedSpriteTemplate.Animations[animationsCount].Frames[framesCount].SpriteSheetTexture == null)
                            {
                                System.Diagnostics.Debug.WriteLine(" ");
                                System.Diagnostics.Debug.WriteLine("============================================================================");
                                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.EndCreateNewAnimatedSpriteTemplate()");
                                System.Diagnostics.Debug.WriteLine("The Frames array contains a null value for a texture.  You may have");
                                System.Diagnostics.Debug.WriteLine("forgot to load a texture before using it in the AddFrameToAnimation()");
                                System.Diagnostics.Debug.WriteLine("method.");
                                System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                                System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                                System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                                System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                                System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                                System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");
                                System.Diagnostics.Debug.WriteLine("============================================================================");
                                System.Diagnostics.Debug.WriteLine(" ");

                                throw new Exception("The Frames array contains a null value for a texture.  See the Debug Output for more info.");
                            }
                        }
                    }
                }

                // it passed the above checks, so it's a valid AnimatedSpriteTemplate
                // loop through the Frames for each Animation and sort them by FrameNumber
                for (int animationsCount = 0; animationsCount < _currentAnimatedSpriteTemplate.Animations.Length; animationsCount++)
                    for (int framesCount = 0; framesCount < _currentAnimatedSpriteTemplate.Animations[animationsCount].Frames.Length; framesCount++)
                    {
                        Array.Sort(_currentAnimatedSpriteTemplate.Animations[animationsCount].Frames, delegate(Frame frame1, Frame frame2) { return frame1.FrameNumber.CompareTo(frame2.FrameNumber); });
                    }

                // check to see if it's already in the _animatedSpritesTemplateList
                foreach (AnimatedSpriteTemplate template in _animatedSpriteTemplatesList)
                {
                    if (template.TemplateName == _currentAnimatedSpriteTemplate.TemplateName)
                    {
                        // if it finds another template with that name, print a warning
                        // and throw an exception
                        System.Diagnostics.Debug.WriteLine(" ");
                        System.Diagnostics.Debug.WriteLine("===========================================================================");
                        System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.EndCreateNewAnimatedSpriteTemplate()");
                        System.Diagnostics.Debug.WriteLine("You tried to create an animated sprite template with a template");
                        System.Diagnostics.Debug.WriteLine("name that already exists.");
                        System.Diagnostics.Debug.WriteLine("Template Name: " + _currentAnimatedSpriteTemplate.TemplateName);
                        System.Diagnostics.Debug.WriteLine("===========================================================================");
                        System.Diagnostics.Debug.WriteLine(" ");

                        throw new Exception("An animated sprite template with that name already exists.  See the Debug Output for more info.");
                    }
                }

                // add it to the _animatedSpritesTemplateList
                _animatedSpriteTemplatesList.Add(_currentAnimatedSpriteTemplate);

                // set the _currentAnimatedSpriteTemplate to null since we're done with it
                _currentAnimatedSpriteTemplate = null;

            }
            // catches the exception thrown if _currentAnimatedSpriteTemplate is null
            catch (NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine(" ");
                System.Diagnostics.Debug.WriteLine("============================================================================");
                System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.EndCreateNewAnimatedSpriteTemplate()");
                System.Diagnostics.Debug.WriteLine("The AnimatedSpriteTemplate object doesn't exist.");
                System.Diagnostics.Debug.WriteLine("Make sure you follow this order when creating an AnimatedSpriteTemplate:");
                System.Diagnostics.Debug.WriteLine("* Call the StartCreateAnimatedSpriteTemplate() method first.");
                System.Diagnostics.Debug.WriteLine("* Call the AddAnimationToAnimatedSpriteTemplate() method for each");
                System.Diagnostics.Debug.WriteLine("  animation in the AnimatedSpriteTemplate.");
                System.Diagnostics.Debug.WriteLine("* Call the AddFrameToAnimation() method for each frame in each animation.");
                System.Diagnostics.Debug.WriteLine("* Call the EndCreateAnimatedSpriteTemplate() method last.");
                System.Diagnostics.Debug.WriteLine("============================================================================");
                System.Diagnostics.Debug.WriteLine(" ");

                throw new Exception("The AnimatedSpriteTemplate object doesn't exist.  See Debug Output for more information.");
            }
        }

        /// <summary>
        /// Creates an animated sprite object from an animated sprite template.  If it can't find a template with the template name, it throws an exception. 
        /// </summary>
        /// <param name="templateName">The template name to copy the animated sprite from.</param>
        /// <returns>AnimatedSprite</returns>
        public AnimatedSprite CreateNewAnimatedSpriteFromTemplate(string templateName)
        {
            // search through the _animatedSpriteTemplatesList for templateName
            foreach (AnimatedSpriteTemplate template in _animatedSpriteTemplatesList)
            {
                if (template.TemplateName == templateName)
                {
                    // make a copy of the AnimatedSprite from the AnimatedSpriteTemplate                    
                    AnimatedSprite newAnimatedSprite = new AnimatedSprite(template.Layer, template.Animations.Length);

                    // make a copy of each Animation object
                    for (int animationsCount = 0; animationsCount < template.Animations.Length; animationsCount++ )
                    {
                        newAnimatedSprite.Animations[animationsCount] = new Animation(template.Animations[animationsCount].Name, template.Animations[animationsCount].Frames.Length, template.Animations[animationsCount].DelayBetweenFrames, template.Animations[animationsCount].NumberOfLoops);

                        // make a copy of each Frame object for each Animation
                        for (int framesCount = 0; framesCount < template.Animations[animationsCount].Frames.Length; framesCount++ )
                        {
                            newAnimatedSprite.Animations[animationsCount].Frames[framesCount] = new Frame(template.Animations[animationsCount].Frames[framesCount].FrameNumber, template.Animations[animationsCount].Frames[framesCount].SpriteSheetTexture, template.Animations[animationsCount].Frames[framesCount].SpriteSheetTexturePositionX, template.Animations[animationsCount].Frames[framesCount].SpriteSheetTexturePositionY, template.Animations[animationsCount].Frames[framesCount].SpriteSheetTextureWidth, template.Animations[animationsCount].Frames[framesCount].SpriteSheetTextureHeight, template.Animations[animationsCount].Frames[framesCount].ColorTint, template.Animations[animationsCount].Frames[framesCount].AlphaTransparencyLevel, template.Animations[animationsCount].Frames[framesCount].RotationAmount, template.Animations[animationsCount].Frames[framesCount].RotationAnchorPoint, template.Animations[animationsCount].Frames[framesCount].ScaleFactor, template.Animations[animationsCount].Frames[framesCount].TransformEffect);
                        }
                    }

                    // add it to the drawable and updatable objects lists
                    _drawableSpriteObjectsList.Add(newAnimatedSprite);
                    _updatableSpriteObjectsList.Add(newAnimatedSprite);

                    // return the copy
                    return newAnimatedSprite;
                }
            }

            // couldn't find the templateName
            System.Diagnostics.Debug.WriteLine(" ");
            System.Diagnostics.Debug.WriteLine("===============================================================================");
            System.Diagnostics.Debug.WriteLine("ERROR in xSprite.SpriteManager.CreateNewAnimatedSpriteFromTemplate() method");
            System.Diagnostics.Debug.WriteLine("Could not find an animated sprite template with the specified name.");
            System.Diagnostics.Debug.WriteLine("Double check the spelling and make sure you actually created the animated");
            System.Diagnostics.Debug.WriteLine("sprite template with the StartCreateNewAnimatedSpriteTemplate() method.");
            System.Diagnostics.Debug.WriteLine("===============================================================================");
            System.Diagnostics.Debug.WriteLine(" ");

            throw new Exception("Couldn't find an animated sprite template with the specified name.  See Debug Output for more information.");
        }

        /// <summary>
        /// Removes the static sprite object from the SpriteManager.  Returns true if the static sprite object was successfully removed.  Returns false if the static sprite object was not found.  If it returns true, remember to set the animated sprite object to null so it is garbage collected.
        /// </summary>
        /// <param name="staticSpriteObjectToRemove">The static sprite object you want removed.  Type: StaticSprite</param>
        /// <returns>bool</returns>
        public bool RemoveStaticSpriteObjectFromSpriteManager(StaticSprite staticSpriteObjectToRemove)
        {
            // removes the static sprite object from the _drawableSpriteObjectsList and returns           
            return _drawableSpriteObjectsList.Remove(staticSpriteObjectToRemove);
        }

        /// <summary>
        /// Removes the animated sprite object from the SpriteManager.  Returns true if the animated sprite object was successfully removed.  Returns false if the animated sprite object was not found.  if it returns true, remember to set the animated sprite object to null so it is garbage collected.
        /// </summary>
        /// <param name="animatedSpriteObjectToRemove">The animated sprite object you want removed.  Type: AnimatedSprite</param>
        /// <returns>bool</returns>
        public bool RemoveAnimatedSpriteObjectFromSpriteManager(AnimatedSprite animatedSpriteObjectToRemove)
        {
            // local flags for determining whether the animated
            // sprite object was removed from both the 
            // _drawableSpriteObjectsList and the _updatableSpriteObjectsList            
            bool removedFromDrawable = false;
            bool removedFromUpdatable = false;

            // remove the animated sprite object from the 
            // _drawableSpriteObjectsList
            removedFromDrawable = _drawableSpriteObjectsList.Remove(animatedSpriteObjectToRemove);

            // remove the animated sprite object from the
            // _updatableSpriteObjectsList
            removedFromUpdatable = _updatableSpriteObjectsList.Remove(animatedSpriteObjectToRemove);

            // if it removed the animated sprite object from both lists, it returns true
            // if not, it returns false            
            if ((removedFromDrawable == true) && (removedFromUpdatable == true))
                return true;
            else
                return false;            
        }

        /// <summary>
        /// Removes all sprite objects from the SpriteManager.  Returns true if all the sprite objects were successfully removed.  Returns false if it couldn't clear them all.  
        /// </summary>
        /// <returns>bool</returns>
        public bool ClearAllSpriteObjectsFromSpriteManager()
        {
            // local flags to track whether we removed all objects from 
            // the _drawableSpriteObjectsList and the _updatableSpriteObjectsList
            bool drawableCleared = false;
            bool updatableCleared = false;

            // checks to see if the _drawableSpriteObjectsList needs to be cleared
            if (_drawableSpriteObjectsList.Count > 0)
            {

                // removes all of the sprite objects from the _drawableSpriteObjectsList
                // then resets the capacity of the list so the memory gets released
                _drawableSpriteObjectsList.Clear();
                _drawableSpriteObjectsList.TrimExcess();
            }

            // checks to see if _drawableSpiteObjectsList was reset
            if (_drawableSpriteObjectsList.Count == 0)
                drawableCleared = true;

            // checks to see if the _updatableSpriteObjectsList needs to be cleared
            if (_updatableSpriteObjectsList.Count > 0)
            {

                // removes all of the sprite objects from the _updatableSpriteObjectsList
                // then resets the capacity of the list so the memory gets released
                _updatableSpriteObjectsList.Clear();
                _updatableSpriteObjectsList.TrimExcess();
            }

            // checks to see if _updatableSpriteObjectsList was reset
            if (_updatableSpriteObjectsList.Count == 0)
                updatableCleared = true;

            // returns true if both lists were cleared
            // returns false if they weren't
            if ((drawableCleared == true) && (updatableCleared == true))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Removes the static sprite template you specified from the SpriteManager.  Returns true if the static sprite template was removed successfully.  Returns false if the static sprite template could not be found.
        /// </summary>
        /// <param name="templateName">The name of the static sprite template you want removed.  Type: string</param>
        /// <returns>bool</returns>
        public bool RemoveStaticSpriteTemplateFromSpriteManager(string templateName)
        {
            // local variable holds the reference to the template
            // if it's found
            StaticSpriteTemplate staticSpriteTemplateToRemove = null;

            // local flag to track whether the static sprite template
            // was removed
            bool templateRemoved = false;

            // loops through the list looking for the template name
            foreach (StaticSpriteTemplate template in _staticSpriteTemplatesList)
            {
                // if we find the template name, assign it to
                // the local variable
                if (template.TemplateName == templateName)
                    staticSpriteTemplateToRemove = template;
            }

            // removes the template from the _staticSpriteTemplatesList
            if (staticSpriteTemplateToRemove != null)
                templateRemoved = _staticSpriteTemplatesList.Remove(staticSpriteTemplateToRemove);

            // sets the local variable to null
            staticSpriteTemplateToRemove = null;

            // returns true if the static sprite template was removed
            // returns false if it couldn't be found
            return templateRemoved;             
        }

        /// <summary>
        /// Removes the animated sprite template you specified from the SpriteManager.  Returns true if the animated sprite template was removed successfully.  Returns false if the animated sprite template could not be found.
        /// </summary>
        /// <param name="templateName">The name of the animated sprite template you want removed.  Type: string</param>
        /// <returns>bool</returns>
        public bool RemoveAnimatedSpriteTemplateFromSpriteManager(string templateName)
        {
            // local variable holds the reference to the template
            // if it's found
            AnimatedSpriteTemplate animatedSpriteTemplateToRemove = null;

            // local flag to track whether the static sprite template
            // was removed
            bool templateRemoved = false;
            
            // loops through the list looking for the template name
            foreach (AnimatedSpriteTemplate template in _animatedSpriteTemplatesList)
            {
                // if we find the template name, assign it to
                // the local variable
                if (template.TemplateName == templateName)
                    animatedSpriteTemplateToRemove = template;
            }

            // removes the template from the _animatedSpriteTemplatesList
            if (animatedSpriteTemplateToRemove != null)
                templateRemoved = _animatedSpriteTemplatesList.Remove(animatedSpriteTemplateToRemove);

            // sets the local variable to null
            animatedSpriteTemplateToRemove = null;

            // returns true if the animated sprite template was removed
            // returns false if it couldn't be found
            return templateRemoved;             
        }

        /// <summary>
        /// Removes only the static sprite templates from the SpriteManager.  Returns true if all the static sprite templates were successfully removed.  Returns false if it couldn't clear them all. 
        /// </summary>
        /// <returns>bool</returns>
        public bool ClearOnlyStaticSpriteTemplatesFromSpriteManager()
        {
            // local flag to track whether we removed all
            // the static sprite template objects from the 
            // _staticSpriteTemplatesList
            bool staticCleared = false;

            // checks to see if the _staticSpriteTemplatesList 
            // needs to be cleared
            if (_staticSpriteTemplatesList.Count > 0)
            {
                // removes all of the static sprite template objects from the 
                // _staticSpriteTemplatesList then resets the capacity of the 
                // list so the memory gets released
                _staticSpriteTemplatesList.Clear();
                _staticSpriteTemplatesList.TrimExcess();
            }

            // checks to see if the _staticSpriteTemplatesList was cleared
            if (_staticSpriteTemplatesList.Count == 0)
                staticCleared = true;
            
            // returns true if the _staticSpriteTemplatesList was cleared
            // returns false if it wasn't
            return staticCleared;               
        }

        /// <summary>
        /// Removes only the animated sprite templates from the SpriteManager.  Returns true if all the animated sprite templates were successfully removed.  Returns false if it couldn't clear them all. 
        /// </summary>
        /// <returns>bool</returns>
        public bool ClearOnlyAnimatedSpriteTemplatesFromSpriteManager()
        {
            // local flag to track whether we removed all
            // the animated sprite template objects from the 
            // _animatedSpriteTemplatesList
            bool animatedCleared = false;

            // checks to see if the _animatedSpriteTemplatesList 
            // needs to be cleared
            if (_animatedSpriteTemplatesList.Count > 0)
            {
                // removes all of the animated sprite template objects from the 
                // _animatedSpriteTemplatesList then resets the capacity of the 
                // list so the memory gets released
                _animatedSpriteTemplatesList.Clear();
                _animatedSpriteTemplatesList.TrimExcess();
            }

            // checks to see if the _animatedSpriteTemplatesList was cleared
            if (_animatedSpriteTemplatesList.Count == 0)
                animatedCleared = true;

            // returns true if the _animatedSpriteTemplatesList was cleared
            // returns false if it wasn't
            return animatedCleared;     
        }

        /// <summary>
        /// Removes all sprite templates from the SpriteManager.  Returns true if all the sprite templates were successfully removed.  Returns false if it couldn't clear them all.  
        /// </summary>
        /// <returns>bool</returns>
        public bool ClearAllSpriteTemplatesFromSpriteManager()
        {
            // local flags to track whether we removed all objects from 
            // the _staticSpriteTemplatesList and the 
            // _animatedSpriteTemplatesList 
            bool staticCleared = false;
            bool animatedCleared = false;

            // checks to see if the _staticSpriteTemplatesList needs cleared
            if (_staticSpriteTemplatesList.Count > 0)
            {
                // removes all of the sprite template objects from the _staticSpriteTemplatesList
                // then resets the capacity of the list so the memory gets released
                _staticSpriteTemplatesList.Clear();
                _staticSpriteTemplatesList.TrimExcess();
            }

            // checks to see if the _staticSpriteTemplatesList was cleared
            if (_staticSpriteTemplatesList.Count == 0)
                staticCleared = true;

            // checks to see if the _animatedSpriteTemplatesList needs cleared
            if (_animatedSpriteTemplatesList.Count > 0)
            {
                // removes all of the sprite template objects from the _animatedSpriteTemplatesList
                // then resets the capacity of the list so the memory gets released
                _animatedSpriteTemplatesList.Clear();
                _animatedSpriteTemplatesList.TrimExcess();
            }

            // checks to see if the _animatedSpriteTemplatesList was cleared
            if (_animatedSpriteTemplatesList.Count == 0)
                animatedCleared = true;

            // returns true if both lists were cleared
            // returns false if they weren't
            if ((staticCleared == true) && (animatedCleared == true))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Adds a static sprite object back into the SpriteManager.  Returns true if the static sprite object is in the SpriteManger.  Returns false if the static sprite object is not in the SpriteManager. 
        /// </summary>
        /// <param name="staticSpriteObject">The static sprite object to be added to the SpriteManager.</param>
        /// <returns>bool</returns>
        public bool AddStaticSpriteObjectToSpriteManager(StaticSprite staticSpriteObject)
        {
            // local flag to track whether the static sprite 
            // is already in the _drawableSpriteObjectsList
            bool staticExists = false;

            // check to see if the static sprite object is already 
            // in the _drawableSpriteObjectsList
            foreach (IxSpriteDrawable spriteObject in _drawableSpriteObjectsList)
            {
                // we only check the static sprites
                if (spriteObject.GetType().ToString() == "xSprite.StaticSprite")
                {
                    if (spriteObject == staticSpriteObject)
                        staticExists = true;
                }                
            }

            // if it was not found, add it
            if (staticExists == false)
            {
                _drawableSpriteObjectsList.Add(staticSpriteObject);
                staticExists = true;
            }

            return staticExists;
        }

        /// <summary>
        /// Adds an animated sprite object back into the SpriteManager.  Returns true if the animated sprite object is in the SpriteManger.  Returns false if the animated sprite object is not in the SpriteManager.  
        /// </summary>
        /// <param name="animatedSpriteObject">The animated sprite object to be added to the SpriteManager.</param>
        /// <returns>bool</returns>
        public bool AddAninmatedSpriteObjectToSpriteManager(AnimatedSprite animatedSpriteObject)
        {
            // local flag to track whether the animated sprite 
            // is already in the _drawableSpriteObjectsList
            bool animatedDrawableExists = false;

            // local flag to track whether the animated sprite
            // is already in the _updatableSpriteObjectsList 
            bool animatedUpdatableExists = false;

            // check to see if the animated sprite object is already 
            // in the _drawableSpriteObjectsList
            foreach (IxSpriteDrawable spriteObject in _drawableSpriteObjectsList)
            {
                // we only check the animated sprites
                if (spriteObject.GetType().ToString() == "xSprite.AnimatedSprite")
                {
                    if (spriteObject == animatedSpriteObject)
                        animatedDrawableExists = true;
                }
            }

            // check to see if the animated sprite object is already
            // in the _updatableSpriteObjectsList
            foreach (IxSpriteUpdatable spriteObject in _updatableSpriteObjectsList)
            {
                // all sprites in the _updatableSpriteObjectsList are animated sprites
                if (spriteObject == animatedSpriteObject)
                    animatedUpdatableExists = true;
            }

            // if the animated sprite is not in the _drawableSpriteObjectsList,
            // add it
            if (animatedDrawableExists == false)
            {
                _drawableSpriteObjectsList.Add(animatedSpriteObject);
                animatedDrawableExists = true;
            }

            // if the animated sprite is not in the _updatableSpriteObjectsList,
            // add it
            if (animatedUpdatableExists == false)
            {
                _updatableSpriteObjectsList.Add(animatedSpriteObject);
                animatedUpdatableExists = true;
            }

            if ((animatedDrawableExists == true) && (animatedUpdatableExists == true))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Copies the static sprite template you specified.  Returns a StaticSpriteTemplate object if the template name was found.  Returns null if the template name was not found.  
        /// </summary>
        /// <param name="templateName">Name of the template you want to copy.</param>
        /// <returns>StaticSpriteTemplate if template name is found.  Null if template name is not found.</returns>
        public StaticSpriteTemplate CopyStaticSpriteTemplateFromSpriteManager(string templateName)
        {
            // looks up the template name in the _staticSpriteTemplatesList
            foreach (StaticSpriteTemplate template in _staticSpriteTemplatesList)
            {
                // if the template name is found, return the template
                if (template.TemplateName == templateName)
                    return template;
            }

            // couldn't find the template
            return null;
        }

        /// <summary>
        /// Copies the animated sprite template you specified.  Returns an AnimatedSpriteTemplate object if the template name was found.  Returns null if the template name was not found.  
        /// </summary>
        /// <param name="templateName">Name of the template you want to copy.</param>
        /// <returns>AnimatedSpriteTemplate if template name is found.  Null if template name is not found.</returns>
        public AnimatedSpriteTemplate CopyAnimatedSpriteTemplateFromSpriteManager(string templateName)
        {
            // looks up the template name in the _animatedSpriteTemplatesList
            foreach (AnimatedSpriteTemplate template in _animatedSpriteTemplatesList)
            {
                // if the template name is found, return the template
                if (template.TemplateName == templateName)
                    return template;
            }

            // couldn't find the template
            return null;
        }

        /// <summary>
        /// Adds a static sprite template back into the SpriteManager.  Returns true if the static sprite template is in the SpriteManger.  Returns false if the static sprite template is not in the SpriteManager. 
        /// </summary>
        /// <param name="staticSpriteObject">The static sprite template to be added to the SpriteManager.</param>
        /// <returns>bool</returns>
        public bool AddStaticSpriteTemplateToSpriteManager(StaticSpriteTemplate staticSpriteTemplateObject)
        {
            // local flag to track whether the static sprite template
            // is already in the _staticSpriteTemplatesList
            bool staticExists = false;

            // check to see if the static sprite template object is already 
            // in the _staticSpriteTemplatesList
            foreach (StaticSpriteTemplate template in _staticSpriteTemplatesList)
            {
                if (template == staticSpriteTemplateObject)
                    staticExists = true;
                
            }

            // if it was not found, add it
            if (staticExists == false)
            {
                _staticSpriteTemplatesList.Add(staticSpriteTemplateObject);                
                staticExists = true;
            }

            return staticExists;
        }

        /// <summary>
        /// Adds an animated sprite template back into the SpriteManager.  Returns true if the animated sprite template is in the SpriteManger.  Returns false if the animated sprite template is not in the SpriteManager. 
        /// </summary>
        /// <param name="staticSpriteObject">The animated sprite template to be added to the SpriteManager.</param>
        /// <returns>bool</returns>
        public bool AddAnimatedSpriteTemplateToSpriteManager(AnimatedSpriteTemplate animatedSpriteTemplateObject)
        {
            // local flag to track whether the animated sprite template
            // is already in the _animatedSpriteTemplatesList
            bool animatedExists = false;

            // check to see if the animated sprite template object is already 
            // in the _animatedSpriteTemplatesList
            foreach (AnimatedSpriteTemplate template in _animatedSpriteTemplatesList)
            {
                if (template == animatedSpriteTemplateObject)
                    animatedExists = true;
            }

            // if it was not found, add it
            if (animatedExists == false)
            {
                _animatedSpriteTemplatesList.Add(animatedSpriteTemplateObject);
                animatedExists = true;
            }

            return animatedExists;
        }

        #endregion

    } // <-- end SpriteManager class
} // <-- end xSprite namespace
