/* ************************************************************
 * Namespace: xSprite
 * Class:  Frame
 * 
 * Class for animation frames.
 * 
 * Animated Sprites consist of Animations. 
 * Animations consist of Frames.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xSprite
{
    /// <summary>
    /// Class for animation frames.  Animated Sprites consist of Animations.  Animations consist of Frames.
    /// </summary>
    public class Frame
    {

        #region MEMBERS

        // this frame's number
        private int _frameNumber;

        // holds the reference to the sprite sheet
        private Texture2D _spriteSheetTexture;

        // sprite's X position on the sprite sheet
        private int _spriteSheetTexturePositionX;

        // sprite's Y position on the sprite sheet
        private int _spriteSheetTexturePositionY;

        // width of the sprite on the sprite sheet
        private int _spriteSheetTextureWidth;

        // height of the sprite on the sprite sheet
        private int _spriteSheetTextureHeight;

        // color to tint the sprite
        // _color.A sets the sprite's transparency
        private Color _color;

        // amount sprite is rotated in radians
        private float _rotationAmountInRadians;

        // anchor point the sprite rotates around
        private RotationAnchorPoint _rotationAnchorPoint;

        // origin point for the sprite's rotation
        private Vector2 _rotationOriginPoint;

        // amount sprite is scaled on both the X and Y axis
        private float _scaleFactor;

        // adjusted width calculated using the X part of the _scaleFactor vector
        private float _scaledWidth;

        // adjusted height calculated using the Y part of the _scaleFactor vector
        private float _scaledHeight;

        // determines whether the sprite is flipped
        // either horizontally or vertically
        private SpriteEffects _transformEffect;

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Returns the frame number. Type: int
        /// </summary>
        public int FrameNumber
        {
            get { return _frameNumber; }
        }

        /// <summary>
        /// Returns a reference to the sprite sheet containing the frame.  Type: Texture2D
        /// </summary>
        public Texture2D SpriteSheetTexture
        {
            get { return _spriteSheetTexture; }
        }
        
        /// <summary>
        /// Returns the horizontal position of the frame on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTexturePositionX
        {
            get { return _spriteSheetTexturePositionX; }
        }

        /// <summary>
        /// Returns the vertical position of the frame on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTexturePositionY
        {
            get { return _spriteSheetTexturePositionY; }
        }
        
        /// <summary>
        /// Returns the width of the frame on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTextureWidth
        {
            get { return _spriteSheetTextureWidth; }
        }

        /// <summary>
        /// Returns the height of the frame on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTextureHeight
        {
            get { return _spriteSheetTextureHeight; }
        }

        /// <summary>
        /// The width of the frame after taking into account it's scaling.  Type: int
        /// </summary>
        public int Width
        {
            get { return (int)_scaledWidth; }
        }

        /// <summary>
        /// The height of the frame after taking into account it's scaling.  Type int
        /// </summary>
        public int Height
        {
            get { return (int)_scaledHeight; }
        }

        /// <summary>
        /// Returns the color of the tint applied to the frame.  Type: Color
        /// </summary>
        public Color ColorTint
        {
            get { return _color; }
        }

        /// <summary>
        /// Determines the transparency level of the frame.  It must be set to a value between 0 and 255.  0 is not completely transparent.  If you want to make the frame completely transparent, set the parent animated sptie's Visible property to false.  Type: byte        
        /// </summary>
        public byte AlphaTransparencyLevel
        {
            get { return _color.A; }

            set { _color.A = value; }
        }

        /// <summary>
        /// The amount of rotation in radians applied to the frame.  If you prefer to work with degrees, use MathHelper.ToRadians() to convert the degrees into radians.  Type: float
        /// </summary>
        public float RotationAmount
        {
            get { return _rotationAmountInRadians; }

            set { _rotationAmountInRadians = value; }
        }

        /// <summary>
        /// Returns the anchor point the frame rotates around.  Type: RotationAnchorPoint (enum)
        /// </summary>
        public RotationAnchorPoint RotationAnchorPoint
        {
            get { return _rotationAnchorPoint; }
        }

        /// <summary>
        /// Returns a Vector2 containing the origin point the frame rotates around.  Type: Vector2
        /// </summary>
        public Vector2 RotationOriginPoint
        {
            get { return _rotationOriginPoint; }
        }

        /// <summary>
        /// Determines the amount of scaling applied to the frame.  Type: float
        /// </summary>
        public float ScaleFactor
        {
            get { return _scaleFactor; }

            set
            {
                _scaleFactor = value;
                CalculateScaledWidthAndHeight();
            }
        }

        /// <summary>
        /// Returns whether the frame is flipped horizontally, flipped vertically, or not flipped.  Type: SpriteEffects
        /// </summary>
        public SpriteEffects TransformEffect
        {
            get { return _transformEffect; }
        }

        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for the Frame class.
        /// </summary>
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
        public Frame(int frameNumber, Texture2D spriteSheetTexture, int spriteSheetTexturePositionX, int spriteSheetTexturePositionY, int spriteSheetTextureWidth, int spriteSheetTextureHeight, Color spriteColorTint, byte alphaTransparencyLevel, float rotationAmountInRadians, RotationAnchorPoint rotationAnchorPoint, float scaleFactor, SpriteEffects transformEffect)
        {
            // assigns the parameters to the appropriate members
            _frameNumber = frameNumber;            
            _spriteSheetTexture = spriteSheetTexture;
            _spriteSheetTexturePositionX = spriteSheetTexturePositionX;
            _spriteSheetTexturePositionY = spriteSheetTexturePositionY;
            _spriteSheetTextureWidth = spriteSheetTextureWidth;
            _spriteSheetTextureHeight = spriteSheetTextureHeight;            
            _rotationAmountInRadians = rotationAmountInRadians;
            _rotationAnchorPoint = rotationAnchorPoint;
            _transformEffect = transformEffect;

            // sets the color tint and alpha transparency levels
            _color = spriteColorTint;            
            _color.A = alphaTransparencyLevel;

            // sets the scale factor
            _scaleFactor = scaleFactor;

            // calculates the scaled width and height based on _scaleFactor,
            // _spriteSheetTextureWidth and _spriteSheetTextureHeight
            CalculateScaledWidthAndHeight();

            // set the rotation origin point
            SetRotationOriginPoint();
        }

        #endregion


        #region METHODS

        /// <summary>
        /// Sets the _scaledWidth and _scaledHeight members based on
        /// _scaleFactor, _spriteSheetTextureWidth, and _spriteSheetTextureHeight
        /// </summary>
        private void CalculateScaledWidthAndHeight()
        {
            _scaledWidth = (float)_spriteSheetTextureWidth * _scaleFactor;
            _scaledHeight = (float)_spriteSheetTextureHeight * _scaleFactor;
        }

        /// <summary>
        /// Sets _rotationOriginPoint based on the anchor point
        /// chosen for _rotationAnchorPoint
        /// </summary>
        private void SetRotationOriginPoint()
        {
            switch (_rotationAnchorPoint)
            {
                case RotationAnchorPoint.TopLeft:
                    {
                        _rotationOriginPoint = new Vector2(0, 0);
                        break;
                    }

                case RotationAnchorPoint.TopCenter:
                    {
                        _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth / 2, 0);
                        break;
                    }

                case RotationAnchorPoint.TopRight:
                    {
                        _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth, 0);
                        break;
                    }

                case RotationAnchorPoint.CenterLeft:
                    {
                        _rotationOriginPoint = new Vector2(0, _spriteSheetTextureHeight / 2);
                        break;
                    }

                case RotationAnchorPoint.Center:
                    {
                        _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth / 2, _spriteSheetTextureHeight / 2);
                        break;
                    }

                case RotationAnchorPoint.CenterRight:
                    {
                        _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth, _spriteSheetTextureHeight / 2);
                        break;
                    }

                case RotationAnchorPoint.BottomLeft:
                    {
                        _rotationOriginPoint = new Vector2(0, _spriteSheetTextureHeight);
                        break;
                    }

                case RotationAnchorPoint.BottomCenter:
                    {
                        _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth / 2, _spriteSheetTextureHeight);
                        break;
                    }

                case RotationAnchorPoint.BottomRight:
                    {
                        _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth, _spriteSheetTextureHeight);
                        break;
                    }

            }
        }

        /// <summary>
        /// Returns a Rectangle of the frame's texture area on the sprite sheet.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetTextureArea()
        {
            return new Rectangle(_spriteSheetTexturePositionX, _spriteSheetTexturePositionY, _spriteSheetTextureWidth, _spriteSheetTextureHeight);
        }

        /// <summary>
        /// Returns a Rectangle of the drawing area for the frame.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetDrawArea(Vector2 drawPosition)
        {
            switch (_rotationAnchorPoint)
            {
                case RotationAnchorPoint.TopLeft:
                    return new Rectangle((int)drawPosition.X, (int)drawPosition.Y, Width, Height);

                case RotationAnchorPoint.TopCenter:
                    return new Rectangle((int)(drawPosition.X + (Width / 2)), (int)drawPosition.Y, Width, Height);

                case RotationAnchorPoint.TopRight:
                    return new Rectangle((int)(drawPosition.X + Width), (int)drawPosition.Y, Width, Height);

                case RotationAnchorPoint.CenterLeft:
                    return new Rectangle((int)drawPosition.X, (int)(drawPosition.Y + (Width / 2)), Width, Height);

                case RotationAnchorPoint.Center:
                    return new Rectangle((int)(drawPosition.X + (Width / 2)), (int)(drawPosition.Y + (Height / 2)), Width, Height);

                case RotationAnchorPoint.CenterRight:
                    return new Rectangle((int)(drawPosition.X + Width), (int)(drawPosition.Y + (Height / 2)), Width, Height);

                case RotationAnchorPoint.BottomLeft:
                    return new Rectangle((int)drawPosition.X, (int)(drawPosition.Y + Height), Width, Height);

                case RotationAnchorPoint.BottomCenter:
                    return new Rectangle((int)(drawPosition.X + (Width / 2)), (int)(drawPosition.Y + Height), Width, Height);

                case RotationAnchorPoint.BottomRight:
                    return new Rectangle((int)(drawPosition.X + Width), (int)(drawPosition.Y + Height), Width, Height);

                default:
                    {
                        System.Diagnostics.Debug.WriteLine(" ");
                        System.Diagnostics.Debug.WriteLine("=====================================");
                        System.Diagnostics.Debug.WriteLine("ERROR in xSprite.Frame.GetDrawArea()");
                        System.Diagnostics.Debug.WriteLine("RotationAnchorPoint was not found in the switch statement.");
                        System.Diagnostics.Debug.WriteLine("Returned a Rectangle (0,0,0,0).");
                        System.Diagnostics.Debug.WriteLine("=====================================");
                        System.Diagnostics.Debug.WriteLine(" ");
                        return new Rectangle(0, 0, 0, 0);
                    }

            }

        }

        /// <summary>
        /// The frame draws itself.  
        /// </summary>
        /// <param name="spriteBatch">A reference to a spriteBatch object.  Type: SpriteBatch</param>
        /// <param name="drawPosition">The point on the screen where the frame should draw itself. Type: Vector2</param>
        /// <param name="layerDepth">The layer depth the frame should draw itself on.  This value must be between 0.0f and 1.0f.  0 is the front layer and 1 is the back layer.  Type: float</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition, float layerDepth)
        {
            // checks to see if layerDepth is between 0.0f and 1.0f
            if (layerDepth < 0)
                layerDepth = 0;

            if (layerDepth > 1)
                layerDepth = 1.0f;

            // draw the frame
            spriteBatch.Draw(
                _spriteSheetTexture,
                drawPosition,
                GetTextureArea(),
                _color,
                _rotationAmountInRadians,
                _rotationOriginPoint,
                _scaleFactor,
                _transformEffect,
                layerDepth);
        }

        #endregion
    } // <-- end Frame class
} // <-- end xSprite namespace
