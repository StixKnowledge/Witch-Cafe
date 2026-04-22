<img width="590" height="302" alt="Screenshot 2025-12-04 211646" src="https://github.com/user-attachments/assets/a3f09004-854e-4658-a0ea-24558e4a0766" />

🔮 The Witch's Craft: Order & Healing
A Witch-Themed Management Game

📖 The Lore
In a cozy corner of the woods, a witch runs a unique cafe. Customers don't just come for the taste—they come to be healed. Every beverage and food item is a piece of the witch’s craft, designed to restore those who seek her help.

🕹️ Gameplay & Features
The Cooker's Role: Play as the witch and fulfill customer orders by selecting the correct ingredients from the shop.

Order Alignment: Success depends on matching your selections exactly to the customer's request using the Ordering Tabulation System.

Patience Mechanics: Every customer has a unique patience timer. If the countdown hits zero, they leave!

Currency System: Collect payments from satisfied customers to grow the cafe's resources.

🛠️ Technical Note & Post-Mortem
This project was a "rescue mission" for a classmate's group who lacked a programmer. I developed this while simultaneously working on my own separate game project.

🌙 The "No Sleep" Factor
This project was built during a period of zero sleep. Because of the extreme crunch and the rush to help a friend, the code was not optimized, and some architectural shortcuts were taken.

⚠️ The "MissionManager.cs" Bug
There is a known error in MissionManager.cs. I placed reference variables (like UI elements or scene objects) inside an Instance/Singleton class.

Why this causes errors:
In Unity, static instances persist between scenes. However, the objects those variables point to are destroyed when a scene changes. This results in the code trying to talk to "ghost" objects that no longer exist, leading to null errors. If you are using this code for your own project, be sure to re-assign those references or move them out of the static instance.

🛠️ How to Run
Engine: Unity (2022.3.43f1 recommended)

Setup: 
1. Clone the repository.
2. Open in Unity Hub.
3. Navigate to Assets/Scenes and open the main Cafe scene.

