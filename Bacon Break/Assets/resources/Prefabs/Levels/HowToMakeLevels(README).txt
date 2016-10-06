NAMING:

T = tutorial/intro level
E = easy level
M = moderate level
H = hard level

Make sure to use the above named letters to sort the levels as follows:
E.G.: ""difficulty"-"levelnumber"-"levelname"" ->E.G: "T-2-firstlevelwithsawblades"

in ths case, the number is simply to tell the user which level goes before another level.
right now numbering is not that important, as it can be discussed which levels should go first, but it is convenient for navigating through the prefabs/levels folder.


CREATING:
use the "empty level" as a basic prefab, thiss contains 3 lanes with the appropriate textures.
From there, you can go to prefabs and select "completed traps" to get your traps. 
the prefab section also contains the bacons and other usables (decoration, tiles, etc)

ADDING:
to add your level to the game, make a button for it in the level select, and in that button,
add "LevelSelector" as a script, give the onclick function of the button: 
LevelSelector.SelectLevel, to allow the button to call the script function.
In the script itself, add the prefab of your level and tell it which scene to load. as of the
moment this txt. was written it sohuld be "TutorialScene".

 
POSITIONING:
The left lane is X = -687
the middle lane is X = -678
the right lane is X = -669
Please stay on these values for X whne placing 1-lane-sized objects (this excludes moving sawtraps & objects placed outside lanes)

Written&updates by: Manuel Martin