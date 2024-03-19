# System Architecture
- Our project follows the high level idea of ECS (Entity-Component-System) framework partially provided by the interal Unity editor system, which is mainly embodied the way we assmeble gameobjects and system designs. At the same time, we also tried to follow the MVC framework to separate the UI and data containers, and incorporated OOP for class abstractions.

## Main Scene Structure/Hierarchy
<img align="center" width="100%" alt="Screenshot 2024-03-18 at 09 16 55" src="https://github.com/ucsb-cs148-w24/project-pj09-liveordie/assets/11906082/2f9b7c3b-5d03-472a-97ab-372bb334c2e8">

--------

## Scripting LifeCycle

<table align="center">
  <tr align="center">
    <th>Unity LifeCycle</th>
    <th>ECS Design</th>
  </tr>
  <tr align="center">
    <td> <img width="80%" src="https://github.com/ucsb-cs148-w24/project-pj09-liveordie/assets/11906082/179781ee-ecd3-49c7-834e-a8b9ee39c21c"> </td>
    <td> <img width="100%" src="https://github.com/ucsb-cs148-w24/project-pj09-liveordie/assets/11906082/96a7e12a-cc50-4fee-8456-4dc68c96bd6c"> </td>
  </tr>
  <tr align="center">
    <td> <a href="https://vionixstudio.com/2020/11/06/unity-awake-vs-start-what-is-the-difference/">Image Source</a> </td>
    <td> <a href="https://docs.unity3d.com/Packages/com.unity.entities@0.1/manual/ecs_core.html">Image Source</a> </td>
  </tr>
</table>

--------

## Jason's Toolkit

<img align="center" width="100%" alt="Screenshot 2024-03-18 at 09 36 40" src="https://github.com/ucsb-cs148-w24/project-pj09-liveordie/assets/11906082/0733ccac-3aa6-4b44-b8d6-1e083abb4e0a">

--------

# Team Decisions
- In our second meeting, we tried to flesh out some of the core game mechanics, decided the genre of the game.  
https://github.com/ucsb-cs148-w24/project-pj09-liveordie/blob/main/team/sprint01/lab02.md  

- In our first meeting after the mvp, we decided to add more concrete gameplay features and refactor our code to make the system more flexible and expandable.  
https://github.com/ucsb-cs148-w24/project-pj09-liveordie/blob/main/team/sprint02/lect09.md  

## Script Refactoring PRs

#### https://github.com/ucsb-cs148-w24/project-pj09-liveordie/pull/135
#### https://github.com/ucsb-cs148-w24/project-pj09-liveordie/pull/111
#### https://github.com/ucsb-cs148-w24/project-pj09-liveordie/pull/91

--------

# User Flow
The user flow for our product is fairly simple and linear. 
- Players will first see a starting menu with Start, Settings and Quit buttons. 
- After game starts, players can cooperate through the game by controlling the character using pre-defined keyboard buttons (WASD and arrow keys). They can interactor with the gameobjects inside the scene including haunted enemies, powerful level-up items, and fancy weapons.
- During the game, players are free to pause the game at any time. They can choose to adjust settings, back to the game or quit the game.
- After one of the characters falls in the battle, the game ends and provides players with a game stats plane.
- Players can then choose to restart the challenge or quit the game. 

--------

# UI Design
- Consistency: Ensure the game maintains a unified aesthetic by incorporating pixelated visuals, pixel art, and themes consistently across all elements. This approach contributes to a cohesive and engaging lightweight web game experience that captivates players with its nostalgia and simplicity.
- Informative Feedback: Deliver immediate and intuitive feedback to players. Display damage indicators, such as floating numbers, to visually represent harm to the player character, coupled with a dynamically decreasing health bar. Complement enemy attacks with distinctive sound effects to enhance gameplay immersion. Utilize pixel-themed sound cues for actions like item usage or experience gain, reinforcing the game's pixel art style and providing a seamless interactive experience.
- Readability: Enhance text readability, especially during fast-paced introductory scenes, by transitioning from pixelated fonts to more legible, traditional serif fonts. This modification ensures that rapidly appearing text is easily readable, allowing players to quickly grasp story elements and instructions without strain.
- Aesthetic Choices: Incorporate a unique blend of fusion jazz tracks from Asia to evoke the ambiance of 90's arcade machines, creating a nostalgic yet refreshing audio backdrop. Select fonts that symbolize both blood and ink brushes, marrying the elegance of ancient Chinese calligraphy with a horror theme to create a distinct visual identity. This careful fusion aims to combine traditional aesthetics with modern horror elements, achieving a balance between eeriness and a certain whimsical charm.

--------

# Future Development Directions
- Adjusting attacking power of current weapons/More weapons
- Enhanced Cooperative Mechanisms: Right now, the rope mechanic primarily acts as a constraint between the two players, limiting their range of motion relative to each other. Future updates could introduce beneficial interactions using this mechanic. For instance, allowing one player to pull the other out of dangerous situations or pitfalls. This addition would not only add depth to the gameplay but also emphasize teamwork and strategic collaboration between players.
- Exploration of Extended Reality (XR): Investigating the possibilities within extended reality could open up new avenues for immersive gameplay experiences. By leveraging XR technologies—encompassing virtual reality (VR), augmented reality (AR), and mixed reality (MR)—the game could offer players innovative ways to interact with the game world and each other, which fits our original goal of experimenting interactivity in a 2-player game. This exploration could lead to unique gameplay mechanics, enhanced immersion, and a richer narrative experience, potentially setting a new standard for interactive gaming.
