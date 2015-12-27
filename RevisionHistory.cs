/* ************************************************************************************************ 
 * 
 * REVISION HISTORY
 * 
 * 12/13/2012
 * 
 * -- xSprite.SpriteManager.CreateStaticSpriteTemplate() checks to make sure you didn't pass it
 *    a null value for the spriteSheetTexture.
 *    
 * -- xSprite.SpriteManager.EndCreateAnimatedSpriteTemplate() checks to make sure you didn't pass
 *    a null value for the spriteSheetTexture to any of the AddFrameToAnimation() calls.
 *    
 * 12/20/12
 * 
 * -- Changed xSprite.StaticSprite.Draw() so the DrawPosition starts at the RotationAnchorPoint
 *    instead of always referring to the top left corner of the static sprite.
 *    
 * -- Changed xSprite.StaticSprite.ScaleFactor from Vector2 to float to work with the new 
 *    xSprite.StaticSprite.Draw() method.
 *    
 * -- Changed xSprite.SpriteManager.CreateStaticSpriteTemplate to work with the changes to
 *    xSprite.StaticSprite.ScaleFactor.
 *    
 * -- Changed xSprite.StaticSpriteTemplate constructor to work with the changes to 
 *    xSprite.StaticSprite.ScaleFactor.
 *    
 * -- Changed xSprite.SpriteManager.CreateNewStaticSpriteFromTemplate() to work with the
 *    changes to xSprite.StaticSprite.ScaleFactor.
 *    
 * -- Changed xSprite.Frame.Draw() so the drawPosition passed in from
 *    xSprite.AnimatedSprite.Draw() starts at the RotationAnchorPoint instead of always
 *    referring to the top left corner of the animated sprite.
 *    
 * -- Changed xSprite.Frame.ScaleFactor from Vector2 to float to work with the new 
 *    xSprite.Frame.Draw() method.
 *    
 * -- Changed xSprite.SpriteManager.AddFrameToAnimation() to work with the changes
 *    to xSprite.Frame.ScaleFactor.
 *    
 * -- Changed xSprite.SpriteManager.CreateNewAnimatedSpriteFromTemplate() to work
 *    with the changes to xSprite.Frame.ScaleFactor.
 * 
 * ************************************************************************************************ 
 */