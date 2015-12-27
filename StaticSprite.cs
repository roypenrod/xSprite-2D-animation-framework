/* ************************************************************
 * Namespace: xSprite
 * Class:  StaticSprite
 * 
 * Class for sprites without animations.
 * 
 * ************************************************************ */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xSprite
{

    /// <summary>
    /// Class for sprites without animations.
    /// </summary>
    public class StaticSprite : IxSpriteDrawable
    {

        #region MEMBERS
        
        // flag to determine whether the static sprite is drawn to the screen.  If true, the static sprite is drawn to the screen at the DrawPosition.  If false, the static sprite is not drawn to the screen. 
        private bool _visible;

        // holds the reference to the sprite sheet
        private Texture2D _spriteSheetTexture;

        // holds the position where the static sprite
        // should be drawn on the screen
        private Vector2 _drawPosition;

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

        // determines whether the sprite is flipped
        // either horizontally or vertically
        private SpriteEffects _transformEffect;

        // layerDepth is a float between 0 and 1
        // 0 is the front layer, 1 is the back layer
        private float _layerDepth;

        // adjusted width calculated using the X part of the _scaleFactor vector
        private float _scaledWidth;

        // adjusted height calculated using the Y part of the _scaleFactor vector
        private float _scaledHeight;

        #endregion



        #region PROPERTIES        

        /// <summary>
        /// Flag to determine whether the static sprite is drawn to the screen.  If true, the static sprite is drawn to the screen at the DrawPosition.  If false, the static sprite is not drawn to the screen.  Type: bool
        /// </summary>
        public bool Visible
        {
            get { return _visible; }

            set { _visible = value; }
        }

        /// <summary>
        /// Returns a reference to the sprite sheet containing the static sprite.  Type: Texture2D
        /// </summary>
        public Texture2D SpriteSheetTexture
        {
            get { return _spriteSheetTexture; }
        }

        /// <summary>
        /// Returns the horizontal position of the static sprite on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTexturePositionX
        {
            get { return _spriteSheetTexturePositionX; }
        }

        /// <summary>
        /// Returns the vertical position of the static sprite on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTexturePositionY
        {
            get { return _spriteSheetTexturePositionY; }
        }

        /// <summary>
        /// Returns the width of the static sprite on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTextureWidth
        {
            get { return _spriteSheetTextureWidth; }
        }

        /// <summary>
        /// Returns the height of the static sprite on the sprite sheet.  Type: int
        /// </summary>
        public int SpriteSheetTextureHeight
        {
            get { return _spriteSheetTextureHeight; }
        }

        /// <summary>
        /// Returns the color of the tint applied to the static sprite.  Type: Color
        /// </summary>
        public Color ColorTint
        {
            get { return _color; }
        }

        /// <summary>
        /// Determines the transparency level of the static sprite.  It must be set to a value between 0 and 255.  0 is not completely transparent.  If you want to make the static sprite completely transparent, set the Visible property to false.        
        /// </summary>
        public byte AlphaTransparencyLevel
        {
            get { return _color.A; }

            set { _color.A = value; }
        }

        /// <summary>
        /// The amount of rotation in radians applied to the static sprite.  If you prefer to work with degrees, use MathHelper.ToRadians() to convert the degrees into radians.
        /// </summary>
        public float RotationAmount
        {
            get { return _rotationAmountInRadians; }

            set { _rotationAmountInRadians = value; }
        }

        /// <summary>
        /// Returns the anchor point the static sprite rotates around.  Type: RotationAnchorPoint (enum)
        /// </summary>
        public RotationAnchorPoint RotationAnchorPoint
        {
            get { return _rotationAnchorPoint; }
        }

        /// <summary>
        /// The point on the screen where the static sprite is drawn.  Starts drawing at the top left corner of the sprite.  Type: Vector2
        /// </summary>
        public Vector2 DrawPosition
        {
            get { return _drawPosition; }

            set { _drawPosition = value; }
        }

        /// <summary>
        /// Determines the amount of scaling applied to the static sprite. 
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
        /// Returns whether the static sprite is flipped horizontally, flipped vertically, or not flipped.  Type: SpriteEffects
        /// </summary>
        public SpriteEffects TransformEffect
        {
            get { return _transformEffect; }
        }

        /// <summary>
        /// The layer the static sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float
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
        /// The width of the static sprite after taking into account it's scaling.
        /// </summary>
        public int Width
        {
            get { return (int)_scaledWidth; }
        }

        /// <summary>
        /// The height of the static sprite after taking into account it's scaling.
        /// </summary>
        public int Height
        {
            get { return (int)_scaledHeight; }
        }

        #endregion



        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for the StaticSprite class.
        /// </summary>
        /// <param name="spriteSheetTexture">Reference to the sprite sheet containing the static sprite.  Type: Texture2D</param>
        /// <param name="spriteSheetTexturePositionX">The horizontal position of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTexturePositionY">The vertical position of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTextureWidth">The width of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteSheetTextureHeight">The height of the static sprite on the sprite sheet.  Type: int</param>
        /// <param name="spriteColorTint">The color of the tint applied to the static sprite.  Type: Color</param>
        /// <param name="alphaTransparencyLevel">The transparency level of the static sprite.  It must be set to a value between 0 and 255. 0 is not completely transparent.  Type: byte</param>        
        /// <param name="rotationAmountInRadians">The amount of rotation in radians applied to the static sprite.  If you prefer to work with degrees, use MathHelper.ToRadians() to convert the degrees into radians.  Type:  float</param>        
        /// <param name="rotationAnchorPoint">The anchor point the static sprite rotates around.  Type: RotationAnchorPoint (enum)</param>
        /// <param name="scaleFactorX">The amount of horizontal scaling applied to the static sprite.  Type: float</param>
        /// <param name="scaleFactorY">The amount of vertical scaling applied to the static sprite.  Type: float</param>
        /// <param name="transformEffect">Determines whether the static sprite is flipped horizontally, flipped vertically, or not flipped.  Type: SpriteEffects</param>
        /// <param name="layer">The layer the static sprite is drawn on.  Accepts a value between 0.0f and 1.0f.  0.0f is the front layer and 1.0f is the back layer.  Type: float</param>        
        public StaticSprite(Texture2D spriteSheetTexture, int spriteSheetTexturePositionX, int spriteSheetTexturePositionY, int spriteSheetTextureWidth, int spriteSheetTextureHeight, Color spriteColorTint, byte alphaTransparencyLevel, float rotationAmountInRadians, RotationAnchorPoint rotationAnchorPoint, float scaleFactor, SpriteEffects transformEffect, float layer)
        {
            // _visible flag starts out as false
            _visible = false;

            // _drawPosition starts out at 0,0
            _drawPosition = new Vector2(0, 0);

            // assigns the parameters to the appropriate members
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

            // checks to make sure layer is between 0.0f and 1.0f
            // then sets the layerDepth
            if (layer < 0.0f)
                layer = 0.0f;

            if (layer > 1.0f)
                layer = 1.0f;

            _layerDepth = layer;
        }

        /// <summary>
        /// The default constructor returns a static sprite object with a null SpriteSheetTexture.  This is used by the SpriteManager class to return a static sprite when a template with that name wasn't found.
        /// </summary>
        public StaticSprite()
        {
            _visible = false;
            _spriteSheetTexture = null;
        }
        
        #endregion
                     
        
        #region METHODS

        /// <summary>
        /// Draws the static sprite to the screen at the DrawPosition if the Visible flag is set to true.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {    
            // only draws the static sprite if it is visible
            if ( _visible == true )
            {
                spriteBatch.Draw(
                    _spriteSheetTexture,
                    _drawPosition,
                    GetTextureArea(),
                    _color,
                    _rotationAmountInRadians,
                    _rotationOriginPoint,
                    _scaleFactor,
                    _transformEffect,
                    _layerDepth);

            }
        }

        /// <summary>
        /// Returns a Rectangle of the static sprite's texture area on the sprite sheet.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetTextureArea()
        {
            return new Rectangle(_spriteSheetTexturePositionX, _spriteSheetTexturePositionY, _spriteSheetTextureWidth, _spriteSheetTextureHeight);
        }

        /// <summary>
        /// Returns a Rectangle of the drawing area for the static sprite.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetDrawArea()
        {
            switch(_rotationAnchorPoint)
            {
                case RotationAnchorPoint.TopLeft:
                    return new Rectangle((int)_drawPosition.X, (int)_drawPosition.Y, Width, Height);                    

                case RotationAnchorPoint.TopCenter:
                    return new Rectangle( (int)(_drawPosition.X + (Width / 2)), (int)_drawPosition.Y, Width, Height );

                case RotationAnchorPoint.TopRight:
                    return new Rectangle( (int)(_drawPosition.X + Width), (int)_drawPosition.Y, Width, Height );

                case RotationAnchorPoint.CenterLeft:
                    return new Rectangle( (int)_drawPosition.X, (int)(_drawPosition.Y + (Width / 2)), Width, Height);

                case RotationAnchorPoint.Center:
                    return new Rectangle((int)(_drawPosition.X + (Width / 2)), (int)(_drawPosition.Y + (Height / 2)), Width, Height);                    

                case RotationAnchorPoint.CenterRight:
                    return new Rectangle( (int)(_drawPosition.X + Width), (int)(_drawPosition.Y + (Height / 2)), Width, Height);

                case RotationAnchorPoint.BottomLeft:
                    return new Rectangle( (int)_drawPosition.X, (int)(_drawPosition.Y + Height), Width, Height);

                case RotationAnchorPoint.BottomCenter:
                    return new Rectangle((int)(_drawPosition.X + (Width / 2)), (int)(_drawPosition.Y + Height), Width, Height);

                case RotationAnchorPoint.BottomRight:
                    return new Rectangle( (int)(_drawPosition.X + Width), (int)(_drawPosition.Y + Height), Width, Height);

                default:
                {
                    System.Diagnostics.Debug.WriteLine(" ");
                    System.Diagnostics.Debug.WriteLine("=====================================");
                    System.Diagnostics.Debug.WriteLine("ERROR in xSprite.StaticSprite.GetDrawArea()");
                    System.Diagnostics.Debug.WriteLine("RotationAnchorPoint was not found in the switch statement.");
                    System.Diagnostics.Debug.WriteLine("Returned a Rectangle (0,0,0,0).");
                    System.Diagnostics.Debug.WriteLine("=====================================");
                    System.Diagnostics.Debug.WriteLine(" ");
                    return new Rectangle(0, 0, 0, 0);
                }

            }
            
        }

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
                    _rotationOriginPoint = new Vector2( _spriteSheetTextureWidth / 2, 0);
                    break;
                }

                case RotationAnchorPoint.TopRight:
                {
                    _rotationOriginPoint = new Vector2( _spriteSheetTextureWidth, 0);
                    break;
                }

                case RotationAnchorPoint.CenterLeft:
                {
                    _rotationOriginPoint = new Vector2(0, _spriteSheetTextureHeight / 2);
                    break;
                }

                case RotationAnchorPoint.Center:
                {
                    _rotationOriginPoint = new Vector2( _spriteSheetTextureWidth / 2, _spriteSheetTextureHeight / 2);
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
                    _rotationOriginPoint = new Vector2( _spriteSheetTextureWidth / 2, _spriteSheetTextureHeight);
                    break;
                }

                case RotationAnchorPoint.BottomRight:
                {
                    _rotationOriginPoint = new Vector2(_spriteSheetTextureWidth, _spriteSheetTextureHeight);
                    break;
                }

            }
        }
       
        #endregion

    } // <-- end StaticSprite class
} // <-- end xSprite namespace
