# UnityCardGame
Multiplayer Card Game for EECE 4371 Final Project

### Gameplay Instructions
* Create a build by pressing the “Build and Run” button. This will prompt for a name for the executable, enter a name such as “networkTest”
* A stand-alone player will launch, and show a resolution choice dialogue.
* Choose the “windowed” checkbox and a lower resolution such as 640x480
* The stand-alone player will start and show the NetworkManager HUD.
* Choose “Host” from the menu to start as a host. A player should be created
* Switch back to the editor and close the Build Settings dialog.
* Enter play mode with the play button
* From the NetworkManagerHUD user interace, choose “LAN Client” to connect to the host as a client
* There should be two objects, one for the local player on the host and one for the remote player for this client
* Press the "hit me" button to draw a card from the deck


### Done
* Designed a CardModel object to represent a card on the board
* Implemented the "Draw" button to draw a card randomly from the deck
* Created a deck structure that tracks which cards have been drawn, allowing each card to be drawn only once per game
* Implemented a GameController to handle all game logic
* Added a NetworkManager to allow multiple clients to connect to each other

### To-Do
* Synchronize displays over the network (so players can see each other's card)
* Implement turn-based actions (player has control, takes action, gives up control, then waits on opponent to take action)
* Implement game logic for comparing cards to determine the winner
* Implement graphic/GUI components to notify winners/losers