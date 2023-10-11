I made a game where you control a rocket to land on a landing pad. The game has three levels.

Instructions:
-Start from the 'Main Menu' Scene
-Hold space to activate rocket nozzle
-Use WASD to control the direction of the rocket, which is relative to the camera view.
-Use the mouse to rotate the camera
-Goal is to land on the circular landing pad
-The wrenches repairs damage
-The gascan refills fuel

Notes:
-Rocket landing script done using rigidbody.addforce up to make it hover.
-Damage taken from crashing into surfaces is based on the velocity. The higher the velocity, the more damage the player will take
 It takes a value and multiplies it with the velocity magnitude, the resulting value is used as the damage value.
-Health and Fuel UI is scaled based on how many percent of the maximum value that is the current amount. This means you can increase the max health/fuel value without having to make any edits to the UI object.
-Explosion effect is an orange sphere that quickly scales up before disappearing and adding rigidbody explosion force to nearby physics objects.
-Turrets player detection is based on both vector3 distance and linecast line of sight.

Thought Process:
The first thing I focused on was getting the rocket controls done, then I added fuel and health.
After that I added stuff like enemy turrets, healing items, effects, etc.
My thoughts on making levels was to start off simple with a level that lets the player familiarize with the controls, and end on a harder level that challenges the player more. 
I did my best to make sure my code is well structured and understandable. I added a few comments in some areas where I felt some clarification may be needed.
I tried to challenge myself by adding a little bit of mathematical calculations in some areas, such as the health and fuel UI bar scaling being based on percentage.
Although I was the only one working on the game, I tried to make the game as simple as possible for designers to work on by adding a lot of prefabs and minimizing the need to drag and drop components.
Most of the bugs I encountered were related to the UI. I had to make sure the different menus didn't accidentally stack on top of each other, such as the stage cleared UI and the game over UI.

Sources:
-https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d
-https://www.youtube.com/watch?v=reWtxGTyN78&ab_channel=TheGameDevCave
-Sound effects taken from Half-Life 2 and Team Fortress 2.
-Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/starfield-skybox-92717
-Using cinemachine and Probuilder

Unity version: 2022.3.10f1
Erik Gafvelin