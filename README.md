# CS576_FinalProject
This is the actual repository for the final project

## NOTE
## MAKE SURE TO ONLY STORE THE ASSETS, PROJECT SETTINGS, PACKAGES, AND THE FINAL_PROJECT SOLLUTION FILE

### LIBRARIES TO BE INSTALLED
## WINDOW -> PACKAGE MANAGER -> UNITY REGISTRY
Packages you need:
- 3D World Building
- 3D Character and Animation
- High Definition RP
- At this point I hit generate lighting a couple times: WINDOW -> RENDERING -> LIGHTING -> GENERATE LIGHTING
- Visual Scripting
### Assets Used
- 4k Ground Textures p1
- Stone Floor Textures
- amusedART Mummy Mon
- ManNeko Assets Adventurer Blake
- Scrolls
- Desert Kit
- Hand Torch
- CR_Tent and Training Table

## Contributions by Team Members
Manu - Most of my work focused on the maze. Using Keshav's initial outline for the narrative of our maze component, I worked on the code for generating the maze within the pyramid, involving the objects that made up the walls, hallfways, and entrances to the rooms. I also primarily focused on the pathfinding of the mummy, which fulfills our AI component through the pathfinding algorithm implemented for its behavior. I established 3 distinct stages for the mummy as it pursued the player in order to introduce more diverse gameplay and further challenges as the player navigated the maze. From there, I worked with the rest of the team to then stich together distinct components of our game, helping create the end screens for when the player wins/loses, tying the difficulty of the mummy along with the players progress throughout the rooms and collection of scrolls, and adding to the interactibility of the rooms. 

Vin - I was responsible for setting up the outdoor portion of the map, containing the obstacle course and the transition  to the maze portion. Additionally, I set up the lighting system for the indoor portion of the maze, as well as helping Noah  set up, fix, and debug the minimap. To allow for the movement for the Blake character, I created character controllers, Animator controllers, as well as a third person camera that rotates to allow for movement. Overall, I also set up general lighting and rendering pipelines, leading the switch from URP to HDRP, as well as initiative for gathering prefabs, models, textures, skyboxes,  etc.

Aarav - 

Keshav - My work was generally focused on creating the six rooms of the pyramid: this includes the entering, exiting, and interaction logic, populating room interiors dynamically, and the implementation of the 3D and 2D puzzles in these rooms and any tracking involved with these. I also worked on the audio of the game, including the music. 

Noah - I was responsible for the initial setup of the game's minimap.  With significant help from Vin, we were able to deploy the map with both an outdoor texture and indoor texture of the static game outline that is responsive to both the player's and mummy's location.  My second most important contribution was the puzzle for the Ibis chamber.  Working in a 2D canvas, I created interactive elements to solve a mathematial puzzle under the framework of hieroglpyhs.  The piping for the minigame to work was built off of Keshav's template code so much credit to him is due as well.  
