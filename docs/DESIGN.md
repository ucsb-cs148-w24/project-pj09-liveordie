# System Architecture
- Our project follows the high level idea of ECS (Entity-Component-System) framework partially provided by the interal Unity editor system, which is mainly embodied the way we assmeble gameobjects and system designs. At the same time, we also tried to follow the MVC framework to separate the UI and data containers, and incorporated OOP for class abstractions. 

# Team Decisions
- In our second meeting, we tried to flesh out some of the core game mechanics, decided the genre of the game.  
https://github.com/ucsb-cs148-w24/project-pj09-liveordie/blob/main/team/sprint01/lab02.md  

- In our first meeting after the mvp, we decided to add more concrete gameplay features and refactor our code to make the system more flexible and expandable.  
https://github.com/ucsb-cs148-w24/project-pj09-liveordie/blob/main/team/sprint02/lect09.md  

# User Flow
The user flow for our product is fairly simple and linear. 
- Players will first see a starting menu with Start, Settings and Quit buttons. 
- After game starts, players can cooperate through the game by controlling the character using pre-defined keyboard buttons (WASD and arrow keys). They can interactor with the gameobjects inside the scene including haunted enemies, powerful level-up items, and fancy weapons.
- During the game, players are free to pause the game at any time. They can choose to adjust settings, back to the game or quit the game.
- After one of the characters falls in the battle, the game ends and provides players with a game stats plane.
- Players can then choose to restart the challenge or quit the game. 
  
