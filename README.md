# Multiplayer Card Game Developed in Unity
The purpose of our project is to implement a simple multiplayer game using the Networking API of Unity (High-level overview here: https://docs.unity3d.com/Manual/UNetOverview.html).
The game is a turn-based card game implementation of Robot Rumble, a card game that was created and developed by a friend of mine on campus (Kevin Ellenburg).
 
## Robot Rumble Overview:
Players start with their robots facing each other, 6 steps away. The goal of the game is to have your robot attack and destroy your opponent’s robot. The players have a set number of cards in their hand that allow them to move towards or away from their opponent a number of steps and execute melee or ranged attacks. Players take turns simultaneously by playing one card from their hand. Movement is taken simultaneously, and combat is also simultaneously. If both robots attack at the same time, they deflect each other and neither is destroyed. A winner is determined when one robot attacks another and is not countered.
This is a simplified version of the rules for the game, the full rules can be seen in the video below:
YouTube video explaining the rules: https://www.youtube.com/watch?v=cUTdHfHucL8
 
## Unity Overview:
We will use Unity to display simple visuals of the cards being played and the location of the robots on the game “board.” The project will be written in C#, and will heavily use Unity APIs. In particular, we will use Unity’s Networking API to handle communication and synchronization of the game state.
 
## Unity Networking Overview:
We will use Unity to control the state of the game (location of robots, cards played, etc.) via the Network Manager. We will use client-hosted P2P networking for simplicity. Messages containing game information will be sent client-to-client using the NetworkTransport API of Unity (UDP-based).


### Complete Network-Flow Breakdown
* Players join the game via the UNet NetworkHUD.
* One player acts as the HOST and will perform server duties (such as player spawning and state synchronization) in addition to being a client, while the other player joins as a simple client (P2P Networking).
* When players take actions (clicking a card to play) the card information is sent over the network using the UNet framework (UDP based by default) to the HOST, which then synchronizes the game board between the players so that both players can see which card was played.
* The HOST waits for BOTH players to make a card selection before executing the game logic and advancing the round.
* After each move, the game logic for WIN/LOSE conditions is performed locally, and each player will check for win/lose conditions and load the appropriate scene.


### Summary of Work Completed
* We designed the CardModel of the cards, and the structure of the deck from scratch using Unity.
* The only part we did not do ourselves is create the sprite assets (these already existed for the Robot Rumble game, and were provided courtesy of Kevin Ellenburg.
* We implemented all game logic in the GameController, and also all of the networking via the Unity UNet framework.

### Summary of Work Not Completed
* While the game is networked, it currently only runs on LAN. We still have to implement a game lobby and matchmaking for players to play without being on the same network. This should be fairly trivial using the UNet framework, but we did not have time to complete it before the deadline.
* The game is still a prototype/proof-of-concept design, many features of the game logic (such as multiple game rounds and multiple characters) are not yet implemented. In addition, there are still some debugging features in the game (such as players being able to see the other player's move before making their own). This would be removed in a full release.



## How to Play
* You should be able to download the RobotRumble executable (you might need the DATA folder as well, unsure), and execute the game in two separate instances.
* Make one instance the LAN HOST, and the other a LAN client before making robot selections.
* Only "The Original" robot has all its game logic implemented, so select "The Original" on both players.
* Make a card selection on each instance, and try to defeat the other robot!




## Progress 11/30
### Done
* Designed a CardModel object to represent a card on the board
* Implemented the "Draw" button to draw a card randomly from the deck
* Created a deck structure that tracks which cards have been drawn, allowing each card to be drawn only once per game
* Implemented a GameController to handle all game logic
* Added a NetworkManager to allow multiple clients to connect to each other

### To-Do
* Synchronize displays over the network (so players can see each other's card)
* Implement remote/online connections (rather than just LAN)
* Implement turn-based actions (player has control, takes action, gives up control, then waits on opponent to take action)
* Implement game logic for comparing cards to determine the winner
* Implement graphic/GUI components to notify winners/losers
