/* ***********************************************************************************************
 * XSPRITE
 * Version: 1.2 
 * Author:  Roy Penrod  
 * Copyright 2012.  All Rights Reserved.
 * Last Modified:  12-20-2012
 * 
 * xSprite is a 2D frame based animation system for sprites
 * in XNA 4.0.
 * 
 * I developed it for my own personal use, but you can use 
 * it if you want.  I'm supposed to put a whole bunch of legal
 * stuff in here, but I'm going to skip all that crap and 
 * just say this:
 * 
 * You can use xSprite in your personal and commercial 
 * projects.  No fees.  No royalties.  No need to give me 
 * any credit.  Just pay it forward by doing something 
 * nice for another person. 
 * 
 * Don't remove this info and try to pass it off as your
 * own work.  That's just not nice.  Bad programmer.
 * Bad.  Bad. :-)
 * 
 * And just to cover my ass ... you use this code at your
 * own risk.  There are no warranties or guarantees of
 * any kind.  It could crash your computer (unlikely), 
 * kick your dog (I'm a cat person), or even launch 
 * nuclear warheads (probably in your game but who knows
 * these days?).  You have been warned! 
 * 
 * Have fun and let me know if you create a kick ass game
 * with it.
 * 
 * -- Roy
 * 
 * ************************************************************************************************
 * ************************************************************************************************
 * 
 *              H O W   T O   U S E   X S P R I T E 
 * 
 * THE STRUCTURE
 * 
 * xSprite lets you use two kinds of sprites:
 * (1) static sprites
 * (2) animated sprites
 * 
 * Static sprites are just images that don't have any
 * animations attached to them.  You use them for stuff like
 * the ground or a rock.
 * 
 * All of the code for static sprites is found in the 
 * StaticSprite class.  They implement the IxSpriteDrawable
 * interface.
 * 
 * Animated sprites are made up of Frame objects attached
 * to Animation objects which are attached to the 
 * Animated Sprite object.
 * 
 * You can find the code for animated sprites in the 
 * AnimatedSprite class, the Animation class, and the Frame
 * class.  They implement the IxSpriteDrawable and
 * IxSpriteUpdatable interfaces.
 * 
 * The real magic happens in the SpriteManager class.  
 * A SpriteManager object helps you manage all of the 
 * sprites in your game.
 * 
 * I suggest you only create one SpriteManager instance
 * per game and let it manage all of the sprites for
 * you.  
 * 
 * The code for the SpriteManger is -- surprise -- 
 * in the SpriteManager class.
 * 
 * Before you can use a sprite, you have to build
 * a template for it.  The template is the blueprint
 * your sprite is based off of.  You can create as 
 * many copies of a sprite as you need from its
 * template.
 * 
 * The code for the templates are found in the
 * StaticSpriteTemplate and AnimatedSpriteTemplate
 * classes.
 * 
 * ------------------------------------------------------------------------------------------------
 * 
 * GETTING STARTED -- SPRITEMANAGER
 *  
 * Before you can create any sprites, you need to 
 * create an instance of the SpriteManager class.
 * 
 * Just use something like:
 * [CODE]
 * SpriteManger spriteManager;
 * spriteManager = new SpriteManager();
 * [/CODE] 
 * 
 * I recommend you only create one instance of the
 * SpriteManager class per game, but hey ... it's your
 * game ... do what you want.
 * 
 * Now that you've got a SpriteManager object, let's
 * create some sprites.
 * 
 * ------------------------------------------------------------------------------------------------
 * 
 * LOADING SPRITESHEETS
 * 
 * xSprite uses spritesheets as Texture2D objects, but
 * doesn't  load them into XNA for you.  You'll have to 
 * do that yourself.  
 * 
 * The most common way is to use the
 * Content.Load<Textures2D>() method in the LoadContent()
 * method of your Game1.cs file.
 * 
 * ------------------------------------------------------------------------------------------------
 * 
 * CREATING A STATIC SPRITE
 * 
 * You'll use static sprites for the game objects that 
 * don't need animations.  You know, stuff like the ground 
 * and rocks.  
 * 
 * Before you can create a static sprite, you need to create
 * a static sprite template.  To do this, call the 
 * CreateStaticSpriteTemplate() method through your 
 * SpriteManager object.  
 * 
 * ----------------------------------------------------------------
 * REMEMBER  
 * Always go through your SpriteManager object for everything 
 * you do with xSprite.  If you don't, you could break 
 * something.  Trust me ... you don't want that.
 * ---------------------------------------------------------------- 
 * NOTE  
 * There is a long ass list of parameters for static sprite 
 * templates.  You can find an explanation  for each parameter 
 * above the method in the SpriteManager class.
 * 
 * You can find explanations for each parameter of any method
 * by looking above the method in the correct class file.
 * ---------------------------------------------------------------- 
 * 
 * Here's what the code looks like:
 * [CODE]
 * spriteManager.CreateStaticSpriteTemplate(parameters);
 * [/CODE]
 *      
 * Now that you've got a static sprite template, you can 
 * create as many static sprites as you want from it.  
 * Just use the CreateNewStaticSpriteFromTemplate() 
 * method through your SpriteManager object.
 * 
 * Here's what the code looks like for a single brick
 * assuming you named the sprite template "brick":
 * [CODE]
 * private StaticSprite brick;
 * brick = CreateNewStaticSpriteFromTemplate("brick");
 * [/CODE]
 * 
 * ------------------------------------------------------------------------------------------------
 * 
 * CREATING AN ANIMATED SPRITE 
 * 
 * You'll use animated sprites for any game objects that 
 * need animations.  Stuff like characters, monsters, your
 * boss (so you can kill him without getting fired ...
 * or going to prison for life and spending "quality time"
 * with your bunk new mate Bubba ... <shudders>).  
 * You know, stuff like that.
 * 
 * Before you create an animated sprite, you need to create 
 * an animated sprite template.  This is more involved
 * than the static sprite template, so I broke it down
 * into several steps to make it easier to use.
 * 
 * STEP 1
 * Call the StartCreateAnimatedSpriteTemplate()
 * method through your SpriteManager object.  
 * You must do this before Step 2 or you'll get
 * exceptions.
 * 
 * STEP 2
 * Call the AddAnimationToAnimatedSpriteTemplate()
 * method through your SpriteManager object for
 * EACH ANIMATION in your animated sprite template.
 * You must do this before Step 3 or you'll 
 * get exceptions.  
 * 
 * STEP 3
 * Call the AddFrameToAnimation() method through 
 * your SpriteManager object for EACH FRAME OF
 * EACH ANIMATION in your animated sprite template.
 * You must do this before Step 4 or you'll get
 * exceptions.  (See a pattern here?)
 * 
 * STEP 4
 * Call the EndCreateAnimatedSpriteTemplate()
 * method through your SpriteManager object.
 * It will check to make sure your animated
 * sprite template is valid and throw a 
 * temper tantrum (exceptions) if it's not.
 * 
 * ---------------------------------------------------------------- 
 * NOTE  
 * A valid animated sprite template has at least one animation
 * consisting of at least one frame.
 * ---------------------------------------------------------------- 
 *  
 * Here's what the code looks like for an animated
 * bat with two animations.  The "fly" animation 
 * has 4 frames while the "die" animation has 3 frames.
 * 
 * [CODE]
 * spriteManager.StartCreateAnimatedSpriteTemplate("bat", other parameters);
 * 
 * spriteManager.AddAnimationToAnimatedSpriteTemplate("fly", other parameters);
 * spriteManager.AddAnimationToAnimatedSpriteTemplate("die", other parameters);
 *      
 * spriteManager.AddFrameToAnimation("fly", 1, other parameters);
 * spriteManager.AddFrameToAnimation("fly", 2, other parameters);
 * spriteManager.AddFrameToAnimation("fly", 3, other parameters);
 * spriteManager.AddFrameToAnimation("fly", 4, other parameters);
 *      
 * spriteManager.AddFrameToAnimation("die", 1, other parameters);
 * spriteManager.AddFrameToAnimation("die", 2, other parameters);
 * spriteManager.AddFrameToAnimation("die", 3, other parameters); 
 *      
 * spriteManager.EndCreateAnimatedSpriteTemplate();
 * [/CODE]
 * 
 * ----------------------------------------------------------------
 * NOTE  
 * If you've got a lot of sprites, you might want to create 
 * some sort of sprite loader class or at least a single method 
 * for each sprite template you want to create.  It'll let
 * you take the template creation code out of your Game1.cs file 
 * and keep that clutter out of your way.
 * ----------------------------------------------------------------
 * 
 * Now that you've got an animated sprite template, you can 
 * create as many animated sprites as you want from it.  
 * Just use the CreateAnimatedSpriteFromTemplate() 
 * method through your SpriteManager object.
 * 
 * Here's what the code looks like for the animated bat 
 * sprite we added above: 
 * [CODE]
 * AnimatedSprite bat;
 * bat = CreateNewAnimatedSpriteFromTemplate("bat");
 * [/CODE]
 * 
 * ------------------------------------------------------------------------------------------------
 * 
 * INTEGRATING THE SPRITEMANAGER WITH YOUR GAME
 * 
 * SpriteManager has an Update() method and a Draw()
 * method, just like your Game1.cs file.
 * 
 * THE UPDATE METHOD
 * Call the Update() method through your 
 * SpriteManager object in your Game1.cs Update()
 * method, like this:
 * [CODE]
 * spriteManager.Update(gameTime);
 * [/CODE]
 * 
 * THE DRAW METHOD
 * xSprite uses layers to draw your sprites and 
 * alpha transparency, so you'll want to use a 
 * few parameters when you call the 
 * SpriteBatch.Begin() method in your Game1.cs 
 * Draw() method.
 * 
 * Here's what the code looks like:
 * [CODE]
 * spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
 * spriteManager.Draw(spriteBatch); 
 * spriteBatch.End();
 * [/CODE]
 * 
 * That's it.  The SpriteManager will take care
 * of the rest of the work for you.
 * 
 * ----------------------------------------------------------------
 * NOTE  
 * When you create a new static sprite or animated sprite from
 * a template, you need to do two things before you let the
 * SpriteManager draw them for you:
 * (1) Set the Visible property to true.  It defaults to false.
 * (2) Set the DrawPosition property.  It defaults to 0,0.  *      
 * ------------------------------------------------------------------------------------------------
 * 
 * MANAGING SPRITE OBJECTS AND SPRITE TEMPLATES
 * WITH THE SPRITEMANAGER
 * 
 * Let's say your player just finished a level
 * and you're done with the enemies for that
 * level, but you need new enemies for the 
 * next level.  You also need to keep the player's
 * character around for the next level.
 * 
 * How would you do that?
 * 
 * SpriteManager has several methods to help you.
 * They are:
 * AddAninmatedSpriteObjectToSpriteManager()
 * AddAnimatedSpriteTemplateToSpriteManager() 
 * AddStaticSpriteObjectToSpriteManager()
 * AddStaticSpriteTemplateToSpriteManager()
 * ClearAllSpriteObjectsFromSpriteManager()
 * ClearAllSpriteTemplatesFromSpriteManager()
 * ClearOnlyAnimatedSpriteTemplatesFromSpriteManager()
 * ClearOnlyStaticSpriteTemplatesFromSpriteManager()
 * CopyAnimatedSpriteTemplateFromSpriteManager()
 * CopyStaticSpriteTemplateFromSpriteManager()
 * RemoveAnimatedSpriteObjectFromSpriteManager()
 * RemoveAnimatedSpriteTemplateFromSpriteManager()
 * RemoveStaticSpriteObjectFromSpriteManager()
 * RemoveStaticSpriteTemplateFromSpriteManager()
 * 
 * Let's take a look at how to use them. 
 *
 * Remember, the player just finished a level
 * and you're done with the enemies for that level,
 * but you need new enemies for the next level.
 * You also need to keep the player's character
 * around for the next level.
 *  
 * In this situation, you're working with both
 * animated sprite objects (the player's character
 * and the enemies from the last level) and animated 
 * sprite templates (the player's character and the 
 * enemies for the next level).
 * 
 * Let's tackle the player's character first.  We want
 * to keep both the animated sprite object and the
 * animated sprite template for the player's character.
 * 
 * We've already got a copy of the animated sprite
 * object from when we used the 
 * CreateNewAnimatedSpriteFromTemplate() method, 
 * so we just need to keep a copy of that.
 * 
 * It probably looked something like this when you first
 * created it:
 * [CODE]
 * AnimatedSprite playerCharacter;
 * playerCharacter = CreateNewAnimatedSpriteFromTemplate("playerCharacter");
 * [/CODE]
 * 
 * To get the animated sprite template, we need to call the
 * CopyAnimatedSpriteTemplateFromSpriteManager() method
 * through our SpriteManager object and store the returned 
 * animated sprite template (or null if it couldn't find it).
 * Here's what the code looks like:
 * [CODE]
 * AnimatedSpriteTemplate playerCharacterTemplate;
 * playerCharacterTemplate = CopyAnimatedSpriteTemplateFromSpriteManager("playerCharacter");
 * 
 * if (playerCharacterTemplate == null)
 *      System.Diagnostics.Debug.WriteLine("Oops.  We couldn't find the template.");
 * [/CODE]
 * 
 * Ok, now that we've got the player's character sprite 
 * object and template, go ahead and clear out all 
 * the sprite objects and templates from the SpriteManager.
 * Here's the code:
 * [CODE]
 * spriteManager.ClearAllSpriteObjectsFromSpriteManager();
 * spriteManager.ClearAllSpriteTemplatesFromSpriteManager();   
 * [/CODE]
 * 
 * That's done.  Let's add the player's character sprite
 * object and template back in to the SpriteManager:
 * [CODE]
 * spriteManager.AddAnimatedSpriteTemplateToSpriteManager(playerCharacterTemplate);
 * spriteManager.AddAninmatedSpriteObjectToSpriteManager(playerCharacter);
 * [/CODE]
 * 
 * We're finished with the player's character so it's
 * time to move on to the enemies.
 * 
 * We got rid of the enemies from the last level when
 * we cleared the sprite objects and templates from the
 * SpriteManager so we don't have to worry about them.
 * 
 * All we have to do now is create the animated sprite
 * template for the new enemies starting with the
 * StartCreateAnimatedSpriteTemplate() method through
 * our SpriteManager object, then call the 
 * CreateNewAnimatedSpriteFromTemplate() method.
 * 
 * You already now how to do that so I won't show the
 * code again.  If you can't remember how to do it, 
 * check the "Creating An Animated Sprite" section
 * above.
 * 
 * ************************************************************************************************ 
 */