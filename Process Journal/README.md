# Process Journal

## Table of Contents

### Tiny Game Assignment
- [Tiny Game - 01.23.2025](#tiny-game---01232025)
### Exploration Prototypes
- [Exploration Prototype 1 - 01.27.2025](#exploration-prototype-1---01272025)
- [Exploration Prototype 2 - 02.04.2025](#exploration-prototype-2---02042025)
- [Exploration Prototype 3 - 02.13.2025](#exploration-prototype-3---02132025)
- [Exploration Prototype 4 - 02.20.2025](#exploration-prototype-4---02202025)
### Iterative Prototypes
- [Iterative Prototype 1 - 03.03.2025](#iterative-prototype-1---03032025)
- [Iterative Prototype 2 - 03.13.2025](#iterative-prototype-2---03132025)
- [Iterative Prototype 3 - 03.20.2025](#iterative-prototype-3---03202025)
- [Iterative Prototype 4 - 03.27.2025](#iterative-prototype-4---03272025)
- [Iterative Prototype 5 - 04.01.2025](#iterative-prototype-5---04012025)

## Tiny Game - 01.23.2025
### "A Trip to the Store"
One thing I knew when starting the tiny game assignment was that I couldn’t allow myself to overthink it. I knew with too many options, I would get overwhelmed. I avoided looking at examples provided and chose the first game engine I remembered being mentioned - Bitsy. Again avoiding looking at other’s creations, I started by watching a simple 5-minute tutorial and got right into it.

My initial idea was to create a memory game, where you go shopping with a list of items, and you must choose the correct ones. Choosing the wrong item could lead to… strange circumstances. I also knew I wanted to have multiple endings. My first challenge was figuring out how to incorporate a visual shopping list. With Bitsy’s limited UI and each tile being so small, I had a bit of trouble figuring this out. My solution was having the wizard (the character that gives you the initial quest to go shopping) “conjure” images of the items you need in the starting room. This allowed me to have a visual representation of the images and forced users to try to memorize each one.

Another challenge I had, less related to the game mechanics, was Bitsy’s limited color palette and tile size. While I am familiar with pixel art - and quite enjoy it - I have never attempted to make a game at this small of a resolution, with only three colors. It pushed me out of my comfort zone creatively, and I’m really happy with how it turned out.

Once I overcame these challenges, Bitsy was quite easy to use. I used their room system to set up the different levels of the store. The player must choose the right item out of many similar items to move to the next room, and if they choose wrong, they get to meet the cult that haunts the grocery store in 1 of 3 rooms, each getting creepier as the player progresses. If the player enters cult room 2, they find a note discussing their next sacrifice. With this note, if the player warns the next sacrifice, the player replaces them. If the player however chooses to ignore the sacrifice, they leave the store safe, with an eerie feeling that something is wrong.

This assignment really pushed me to stop over-thinking, over-planning, and over-analyzing the process of game-making and to simply make something. It was super cool playing all the games by my classmates, and I was especially interested in other game made with Bitsy. It was super neat seeing how they interpreted the software and how different our games were.

[A Trip to the Store](../Projects/a_trip_to_the_store.html)


## Exploration Prototype 1 - 01.27.2025
### Rhythm Game Prototype
For my first experimental prototype, I worked on the base mechanics for a rhythm game. I’d been toying with the idea for a while, and when we learned about the mechanics for the falling-asleep game last class, like the collector, dropper, and circle, I realized many of those concepts aligned with what I had in mind.

The basic idea for my game is that the player switches between different lanes using keys like ASDF. When the notes fall, the player needs to be in the correct lane and press ENTER at just the right time for it to count. I was able to get this working, but definitely not without some challenges.

The first challenge was so simple it drove me crazy for about half an hour. I needed my player box to act as a trigger so it could detect when a note collided with it. I used the function I found online, OnTriggerEnter, but no matter what I tried, my player box just wouldn’t detect the notes. After a lot of Googling and even more debug text, I finally realized my mistake: I needed to use OnTriggerEnter2D because my game is 2D. 🤦

Once that was sorted, I needed to figure out how the notes would fall. At first, I tested using gravity, but since this is a rhythm game, the movement needed to be precise to sync with the audio. I ended up calculating the velocity based on the fall duration I wanted and the distance the notes needed to travel. It worked perfectly, until I changed the fall duration to something other than 2 seconds. No matter what value I used, the notes still fell in exactly 2 seconds. That’s when I learned the hard way that you need to properly initialize your variables in the Start function.

After solving that, everything else went relatively smoothly. I learned how Unity’s event system works (super useful!), and I created a spawner that releases notes at specific times. There are still a few features I’d like to add, but they aren’t really necessary for this prototype. For example, I’d love to include proper feedback when a note is hit or missed (right now, it’s just console logs). I also think it’d be great to have the ability to chart exact note patterns, rather than having them fall into random lanes, kind of like Guitar Hero.

Overall, I’m really happy with how this turned out. I managed to build what I was hoping to build, learned a ton, and set myself up with a solid foundation to maybe keep working on this project in the future.

[In this Unity project - under Scene "1 - Experimental Protype"](../Projects/Cart%20310%20-%20Experimental%20Prototypes)

![The rhythm game prototype](../Images/rhythm_game_prototype.png)

## Exploration Prototype 2 - 02.04.2025
### Hot Potato!

With this prototype, I had a few goals in mind:
- Play around with the physics concepts we learned in the last game
- Add custom sprites and sounds
- Create a simple user interface

The game I made is pretty straightforward. Hot Potato! is all about keeping as many potatoes in the air as you can, while more and more keep spawning in. Instead of using a physics material, I ended up writing custom physics in the potato script. This was because I didn’t want the potato's bounce to keep increasing uncontrollably. I’m not sure if this is the best solution, but it worked well and gave me the control I needed. I also wanted to make the platform a bit more interesting, so I added the ability to rotate it, along with the usual side-to-side movement, giving you more control over the direction the potatoes go.

As for the assets, I wanted some variation, so I created a few different-colored potatoes. I also made them react to bouncing off different objects, adding a little animation. It’s subtle, but I think it gives the potatoes a cute touch! I also downloaded a couple of royalty-free sound effects from the internet to implement. There’s one for when a potato spawns, when one falls, and every time it bounces on the platform. Additionally, I learned how to manipulate text to display the score and high score, which was a fun little challenge.

I’m starting to get pretty comfortable with the basics of the platform now. I definitely ran into fewer issues and relied on the internet a lot less this time around, which feels like progress!

[In this Unity project - under Scene "2 - Experimental Protype"](../Projects/Cart%20315%20-%20Experimental%20Prototypes)

![hot potato!](../Images/hot-potato-gif.gif)

## Exploration Prototype 3 - 02.13.2025
### Sprinkler Wars

Ok. I’m not as proud of this one as I have been of my prototypes over the past few weeks. I mean, it works, but there’s no polish, the code is full of spaghetti, and I’m ready to forget about this one and try something new next week.

The premise is simple: it’s a two-player game where resentful neighbors engage in a hose-spraying war over a fence. I wanted the water pellets to be physics-based so that players had to aim in an arc to hit their opponent. I also wanted the controls to be very simple since both players would be using the same keyboard. Each player can move back and forth, adjust their hose upwards (it lowers on its own), and shoot. I got these core mechanics working, though I’m not completely satisfied with the implementation.

That said, through all this spaghetti, I did learn a lot about shooting projectiles, thanks to [this](https://youtu.be/zYN1LTMdFYg?si=dDOiGjsx3OSctZVt) youtube video. I think I’ll give these projectiles another shot in my next prototype. My biggest struggles with this project came from not fully understanding how different scripts communicate with each other, which gave me lots of headaches, time I could have spent making this game… actually fun.

And I do think there’s potential here! I just might need to dig it up from the roots and start again.

[In this Unity project - under Scene "3 - Experimental Protype"](../Projects/Cart%20315%20-%20Experimental%20Prototypes)

![Sprinkler Wars](../Images/sprinklerwars.png)

## Exploration Prototype 4 - 02.20.2025
### Treasure Keeper

Building off the mess of sprinkler wars, I still wanted to make a game with a shooting mechanic, but one that actually felt complete (in its limited scope) and well, fun. I had the idea of making a top down game kinda like Journey of the Prairie King, the mini-game inside Stardew Valley (I apologize it there are more classic examples of this kind of game) where enemies spawn in on all sides but instead of waves you just have to survive as long as possible. Also in this game surviving just means keeping the enemies grubby hands off your treasure for as long as possible.

Basically I knew I needed/wanted the following mechanics:

Needs:
- Player movement and gun shooting mechanics
  - Thank you again to [this](https://youtu.be/zYN1LTMdFYg?si=dDOiGjsx3OSctZVt) youtube video!
- Simple enemy AI
- Treasure that takes damage while enemies are touching it
- A health bar for the treasure
  - Thanks to [this](https://youtu.be/lYZayXViTN8?si=yzutEf_o5F2evggJ) short video

Wants:
- Particle effects ~~~
  - Thanks again mr sasquatch for [this](https://youtu.be/0HKSvT2gcuk?si=Hi4hPBCNi61zqG-f) video
- Score system
- Finally implement a dreaded menu screen
- Power ups (didn't get to this one… maybe one day)

I will say, even though this one feels much more polished than the last… I’m still not super proud of some of my coding choices that I made for time purposes. I figure that my knowledge of why these decisions are bad + the fact that I would do it differently if I was going further than a prototype has to count for something… right?

Some of the changes I would make to the code implementation if I was going forward would be:
- **Health bar implementation**: it sucked last time and it's not great this time, Im handling the health in both the treasure script and sperately in the health bar script, and the health in the health bar is based on its width just happening to be perfectly 100. If I wanted to change the size of the health bar I would be screwed unless I changed the code.
- **Implementing different behaviours in odd scripts**: for example I implemented the entire scoring mechanic within the enemy script simply because I was lazy and I knew it would be easy to increment the score everytime the an enemy died. I had to do a few workarounds to even make this work but it probably would've been more worth it to just do it right

I am probably forgetting some too… but I’m really just writing it as a reminder to myself to practice best practices and stop being lazy lol.
Anyways, I’m happy with this game overall and I genuinely think its kinda fun :)

[In this Unity project - under Scene "4 - Experimental Protype", or "TK Menu Screen"](../Projects/Cart%20315%20-%20Experimental%20Prototypes)

![TKGameStart.gif](../Images/TKGameStart.gif)
![TKGameEnd.gif](../Images/TKGameEnd.gif)

## Iterative Prototype 1 - 03.03.2025
### Untitled Roguelike - Dungeon Generation

I cant lie, the pressure to come up with an idea that I would be working on for the rest of the semester got to me, but I ended up settling on a Roguelike! I am really interested in procedurally generated worlds so I thought I’d dip my toes in it with a little dungeon generation.

My overall idea for the game is that the player is a museum owner during the apocalypse and must go out into the dangerous world to uncover ~ lost relics of a forgotten past ~. I was thinking the game play loop would be pretty similar to Moonlighter, where you go dungeon crawling at night and shopkeep (museum keep?) during the day. However for my scope for this class, I am thinking I will be mostly sticking to just the dungeon.

For my first iteration I wanted to jump right in to the map generation, and even though I went pretty simple with a grid based system, it was still much more complicated than I hoped. I started with a Room class, and created room type child classes (Entry, Easy, Medium, Hard, Exit) that are a bit redundant at the moment but I think having the separate classes will come in handy later. I also have a Door class that teleports the player between rooms (... see below for me having trouble with this mechanic, and my fixed version)

![BadTeleporter.gif](../Images/BadTeleporter.gif)
![GoodTeleporter.gif](../Images/GoodTeleporter.gif)

I created a generator using a breadth-first generation algorithm using a queue so that the rooms would generate in all directions. It starts with an entry room and begins creating new adjacent rooms given a random number of doors. When generating the amount of doors each room had I also had to take into account how many rooms were left so I didnt accidentally create doors that led to nowhere.

I also didn't want to have the entry room go directly into a hard room, so I made sure that each room could only go up 1 difficulty from the previous room so it gets harder the further you go form the entrance.

Ideas for the future ~
- Infinite Dungeon: After reaching the purple exit room, you advance to a new level with difficulty increased
- Combat system
- Different types of enemies
- Inventory system and treasure
- Big relic room at the end of each floor where you have to protect your excavator while enemies swarm (can reuse a lot from treasure keeper here heheh)
- Fine-tune the dungeon generation: maybe limit hard rooms or change probabilities

Enjoy some of my generated maps :)

Legend ~
- Blue: Entry
- Green: Easy Difficulty
- Yellow: Medium Difficulty
- Red: Hard Difficulty
- Purple: Exit

![DungeonGeneration1.png](../Images/DungeonGeneration1.png)
![DungeonGeneration2.png](../Images/DungeonGeneration2.png)
![DungeonGeneration3.png](../Images/DungeonGeneration3.png)

## Iterative Prototype 2 - 03.13.2025
### Untitled Roguelike - Dungeon Generation

It's midterm week and the amount of energy it took to resist the urge to work on this game instead of studying was insane. With all the studying I had to do, I didn’t get as much done in this iteration as I’d hoped, but I did create a checklist and expanded on my previous map generation to make it fully playable and infinitely generated.

The following outlines my minimum viable product, basically what I hope the accomplish by the end of the semester so I have something playable and maybe fun. I originally wanted to set up the foundation for the basic gameplay loop this week, but alas, I only managed to complete the infinite levels portion. Right now, I don’t have much beyond a player and some rooms, but the movement and dungeon flow are finally starting to feel exactly like what I pictured.

![checklist.png](../Images/checklist.png)

I have three more iterations and 5 more sections to go, so Im not exactly sure what the game is gonna look like by the last class, but honestly I’m not too worried. I’m having so much fun with this that I’ll probably keep working on it through the summer anyway.

![infinite_dungeon.gif](../Images/infinite_dungeon.gif)

## Iterative Prototype 3 - 03.20.2025
### Untitled Roguelike - Dungeon Generation

I thought after midterms I’d be able to work on this much more, but I’ve just gotten (and will most likely continue to get) exponentially busier. With that said, this week my goal was to get the foundation of the basic gameplay loop running, and I almost fully completed that! The only thing left is to fix a bug for a feature that I wasn’t even planning to implement but did anyway because I knew I would need to, plus adding death in the dungeon that removes all or most of the player's coins.

To recap, these were my goals for the week:

![Tasklist.png](../Images/Tasklist.png)

And I accomplished everything checked off, plus a little extra:
- Switching scenes between being in the dungeon vs out of the dungeon
  - This required lots of refactoring of my code and looooots of bug fixing, the bug that I still haven't fixed comes from this implementation
  - Also started using more singletons than I should, I’ve heard it’s not best practice to use singletons (at least often) but I’ve decided I wnat to learn that the hard way >:)
- And a little loading screen to cover up the weird transition between scenes, currently the loading bar just shows at full (maybe it's just loading too quick?) but it definitely looks better with it vs without it

The following gif demonstrates:
- The coins, coin counter, and shop all interacting together
- The damage dealer, healer, and health bar all interacting together

![StarterRoom.gif](../Images/StarterRoom.gif)

I also added a 3rd door to the end room that allows the player to leave the dungeon instead of continuing deeper, by doing this they keep everything they’ve earned within the dungeon. The following gif shows that off as well as my not-so-fancy loading screen

![ExitingDungeon.gif](../Images/ExitingDungeon.gif)

Next week I'm planning on creating some sort of basic enemy as well as starting on some level design, fun stuff

## Iterative Prototype 4 - 03.27.2025
### Untitled Roguelike - Level Design and Enemies

As promised, this week I focused on level design and enemy creation. I didn’t get as much enemy creation done as hoped, but I have one functional one! Which is something!

I was able to use a lot of behavior I implemented in Treasure Keeper, such as the aim-and-shoot mechanics, which helped speed things up a bit. For the enemy type, I created a turret which simply aims at the player and shoots at a constant rate of fire. I used my player aim and shoot behaviour and just adjusted it so that it would aim at the player’s position instead of the mouse position, and shoot at an interval instead of on click.

The other enemy I want to implement before the end of the semester is just the following enemy that will damage the player or excavator on contact, which I’ve also basically created already in Treasure Keeper.

One thing I want to implement (and might have to implement with the following enemies) is a line-of-sight mechanic for the enemies. I’ve done this before in Godot and it wasn’t too difficult so I’m hoping it’ll be similar in Unity.

Here’s how the turret looks:

![Turret.gif](../Images/Turret.gif)

And here’s some level design:

![LevelDesign.png](../Images/LevelDesign.png)

The legend makes everything pretty self-explanatory I hope, but basically all of these rooms will be prefabs and when generating a room of the category, it will pick one at random.

I cant believe there’s only one more iterative prototype after this, I can’t promise that it will be anything crazy impressive by next week, but I still have hope it will be playable! I still need a name though…

## Iterative Prototype 5 - 04.01.2025
### Untitled Roguelike - Game Loop!

Ok! I made some progress! And made something playable? And it’s not an April Fools joke!

This week I completed everything to give my game a relatively basic gameplay loop. You enter the dungeon, grab as many coins as you can, try not to die before reaching an exit room, and if you make it out with enough coins, get some upgrades!

First I continued working on my turrets. Last week I didn’t have a line-of-sight mechanism in place yet, so the turrets knew where u were… at all times… in all rooms. Kinda scary. Its relatively simple, just uses a raycast and checks if the first collision detected is the player. With this is place I also wanted to add a slight transition animation from active to inactive so it wasnt snapping back and forth. Then I got a little carried away with particle effects and bullet trails, but I think it gives the game (which is using 0 external sprites) a bit more character and just feels better to play.

![raycastLOS.gif](../Images/raycastLOS.gif)

After this, I started on the levels. Using my designs from last week I started building each prefab and adjusted the room generation so it was selected from a 2D array instead of a 1D array, this way it chooses a random difficulty and a random level from said difficulty.

Next, I implemented the upgrade system. This wasn't too bad to add in as I already had the base for the shop and it was mostly just adjusting some player variables, however I did need to make quite a few tweaks here and there. I also added some debug player stats for visibility, and increased the size of the health bar as the player increases their armor, to add some visual feedback.

![upgradesbabyyy.gif](../Images/upgradesbabyyy.gif)

Finally, to complete the actual gameplay loop, I added scaling to both the enemies' difficulty and the coin values as you enter each new level in the dungeon. This is to incentivize the player to go deeper once they have enough upgrades. Also, I’m not sure if this was this week or last week, but I also added a game over screen which appears when the player dies in the dungeon and prompts the player to respawn in the starter room while deducting a large percentage of their coin.

So I’ve completed my ultimate goal of something playable! Yay! Now to make it fun for the final submission. Here are some goals to get there:
- Make the chaser enemies. These should spawn from a designated spawn point and chases down a target, doing damage to the target if touching it.
- Make the boss room w/ excavator. Chaser enemies would target the excavator instead of the player and come in hoards, similar to treasure keeper. Also implement a door lock so players cant leave while boss level is in progress.
- Add some charm and uniqueness! My ultimate vision for this game had it, but having to boil it down to it’s most basic mechanics to have something I could present by the end of the semester has left it as a generic rogue-like with no identity. Im not sure how I can do this before the final submission, but it’s a goal regardless.
- Think of a title. Please.

Anyway, enjoy a few gifs of gameplay c:

![actuallyplayingthegame.gif](../Images/actuallyplayingthegame.gif)
![death.gif](../Images/death.gif)

## Final Game - 04.10.2025
### Relict

Final week! While I don’t have a fully finished game that I’d want to showcase on my Itch, I’m super happy with the progress I’ve made and I’m super excited to get feedback from my classmates (and playtest their games as well!)

This week I accomplished the goals I set for myself last week, which included making the chasing enemy (which I aptly named Chaser), make the boss level with an excavator, implement door locks, and while I didn’t get the chance to personalize the game too much, I think the sprite for the chasers are quite cute :3 And, I came up with a title: <b><i>Relict</i></b>

I chose relict because it means “a thing which has survived from an earlier period or in a primitive form.” which I feel is fitting with the narrative I eventually want to implement, but is vague enough to make sense even without the narrative. 

I wont go into all the technical details of the implementation of these features but here’s a gif of testing the chasers before adding their damage, so they look like they are just aggressively trying to hug the player.

![hugs.gif](../Images/hugs.gif)

And here’s one of the excavator levels!

![excavator.gif](../Images/excavator.gif)

This week also involved a lot of bug fixing, and also for the first time, playtesting! I got my roommate to sit down and try it out, and while she doesn’t play games much she picked it up pretty quickly and told me that the drive to get upgrades was really addicting (mission accomplished). It must of been because when I asked my boyfriend to try it, he almost got the character fully maxed out, which is further than I’ve ever gotten. During his playtest, we found some bugs and decided on a few small changes that I think improved the game a lot. We also workshopped some future features for the game if I decide to continue with it such as:

- Health pack drops/purchasable consumables
- Checkpoints for the dungeon similar to Stardew Valleys Mines (Ex. if you’ve reached level 5, you can start from there instead of starting from 1)
- A map consumable (dropped or purchasable) which reveals the map for a level

Here’s some footage of his playthrough :)

![playtest-1.gif](../Images/playtest-1.gif)
![playtest-2.gif](../Images/playtest-2.gif)

I’m super excited to have something playable and functional at this point, and super hyped that someone actually enjoys playing it!

I am excited to get feedback in class and will hopefully be able to make some adjustments for the final submission, as well as a couple extra changes such as adding some UI & cleaning stuff up.

And if you want to play, it’s being hosted on github pages, check it out!

https://katfrain.github.io/CART315/


[Back to top](#process-journal)