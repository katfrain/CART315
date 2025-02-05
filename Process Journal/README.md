# Process Journal
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
