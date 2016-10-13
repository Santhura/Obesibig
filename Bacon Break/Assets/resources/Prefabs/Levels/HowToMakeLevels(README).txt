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


NEW:
because of terrible merges some traps have been broken, they are easy to fix:
1 - go to your level and in the search bar (top left in your hierarchy), type "wall", find all the children of any spikedwall with that name and do the following:
  - change the tag to "Wall" on said children. and then change the boxcollider.y of the children to 2. the boxcollider should be 1,2,1.
2 - go t your level and check if all loose saw traps correspond to the current prefab. the parent should contain a boxcollider with trigger checked and a traptrap.cs script.
  - also check the child named "sawblade", make sure it has the traphitbox.cs and make sure it does NOT contain traptap.cs.
3 - Make suree the scale of your traps corresponds with the new, working prefabs. for example: the sawblade would be 5,6,6.
4 - you're done, make sure to do this on all your levels.


Written & updated by: Manuel Martin
