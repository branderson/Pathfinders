# VR Project

## Development
When cloning the project, do it with "git clone --recursive https://github.com/branderson/VR-Project". This is necessary because the project contains a git submodule which will otherwise remain as an empty folder when cloned. If your version of the project directory has an empty folder for "Assets/Scripts/Utility", run the command "git submodule update --init --recursive". The Utility submodule is actively under development because I use it across several different projects, so it's a good idea to run git submodule update every now and then to make sure you're using the newest version, since it does not update with a git pull. This is especially true if something stops working related to something in the Assets.Utility namespace.

Above all else, no two people should ever be working on the same scene at the same time. This causes the nastiest of conflicts which are pretty much impossible to resolve without throwing out someone's work. To prevent this, each of us should have our own test scene which will only be edited by that person. Anyone making changes to the actual level scene(s) should let everyone else know first to prevent conflicts.

The master branch should always reflect a working state of the game. Any commits should instead be made to develop. When develop is in a working state that is better than the current state of master, we'll pull it in to master. Since this is a very short-term project where we'll probably only get an even somewhat working build of the game towards the end of development this isn't super important, but its just good practice.

## Design
### Description
2-player game in which one player uses the VR headset and gamepad to navigate a dungeon and the other player uses the keyboard, mouse, and monitor to view and manipulate the dungeon from a top-down view.

### Core Mechanics
#### Stealth
* The VR player is unable to defend themself and must instead avoid all hazards.
* Movement speed is slow, to reduce nausea and lend a feeling of sneaking. Because of this, the levels should be designed such that the player doesn't have to move too far between decision points (branches).
* If the player is seen by an enemy they will be immediately attacked and killed, since allowing them to run away could be disorienting. This could change depending on whether the feeling of excitement from attempting to escape overshadows the nausea that it may induce.

#### Puzzles
* The players must work together to solve puzzles within the dungeon
* Puzzles can include:
    * Finding passwords written on walls
    * Tile puzzles which occur in real time, requiring the monitor player to stay on the lookout and warn the player to escape if enemies get close
    * Decoding puzzles similar to Keep Talking and Nobody Explodes, in which one player sees a key mapping symbols to inputs and the other must describe what they see to them

### Level Design
* Short, branching, maze-like corridors
* Enemies patrol specific areas of the level in pre-determined paths, which the player will need to sneak past
* Some enemies patrol the level in random paths and can go anywhere, preventing the player from being truly safe at any time
* Important locations in the levels, such as the locations of puzzles, are in areas heavily guarded by patroling enemies

### Visuals
#### Game

#### Interface
