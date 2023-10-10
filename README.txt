I made a game where you control a rocket to land on a landing pad. The game has three levels.

Instructions:
-Start from the 'Main Menu' Scene
-Hold space to activate rocket nozzle
-Use WASD to control the direction of the rocket
-Use the mouse to rotate the camera
-Goal is to land on the circular landing pad
-The wrenches repairs damage
-The gascan refills fuel

Notes:
-Rocket landing script done using rigidbody.addforce to make it hover.
-Damage taken from crashing into surfaces is based on the velocity. The higher the velocity, the more damage the player will take.
-Health/Fuel UI is scaled based on how many percent of the maximum value that is the current amount.
-Explosion effect is an orange sphere that quickly scales up before disappearing and adding rigidbody explosion force to nearby physics objects.
-Turrets player detection is based on both vector3 distance and linecast line of sight.

Sources:
-https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d
-https://www.youtube.com/watch?v=reWtxGTyN78&ab_channel=TheGameDevCave
-Sound effects taken from Half-Life 2 and Team Fortress 2.
-Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/starfield-skybox-92717

Unity version: 2022.3.10f1
Erik Gafvelin