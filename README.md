# Branch Notes

This branch is a snapshot of the first working UI movement system finished on 8/28, and will not be updated - it serves as a reference point for future stable development. It is intentionally kept static. The code in this branch reflects a stable, functional version of the system before major updates and refactors were introduced on main.

The following features were completed for this branch

- Map Navigation using **Map Navigation.cs**, but may later adjust values and weights and implement player settings to change them.
- Resolution Scaling in **ResolutionScaler.cs**, which for now only scales the map and background - need to implement UI components in scaling as well.
- Scenario Management with **ScenarioAsset.cs** and **ScenarioManager.cs** which only contain texture data. Other fields are empty and must be edited to become functional later.

# A Land Divided

A strategy game built upon the Unity Game Engine. You are a statesman aiming to unite the realm. One land, one culture, one people divided.

## Devlog

**Prior to this repo:**

- Research into a potential set of 20 multipolar intracultural conflicts to represent
- Extensive mechanic, loop, and game systems planning
- Creation of one complete scenario map with a texture, city and text overlays, border
- Game engine research and rough plan for managing and interacting with game data

**8/26/26**
- Initialized Github Repo and pushed default 2D BRP Unity setup code.
- Added GPL-3.0 license to protect against direct code replication in other commercial projects.

**8/27/26** 
- Developed plan for map rendering - images scale and transform around the camera, more directly mirroring real rendering practices - "camera" remains static. 
- Developed **MapNavigation.cs** script in order to handle mouse and key input for panning and zooming. 
- Added lerp capability for smooth movement. A particular issue was getting the map to transform when zooming in to center the mouse point. 

**8/28/26**
- Updated .gitignore to remove image and texture assets to make this a code-only repo.
- Added README details and began devlog.
- Updated support for different screens with a separate **ResolutionScaler.cs** file. Independently scales map and background components in the Canvas by dividing the window size by the standard sizes of the component.
- Began scenario management code to make asset switching modular, making only one scene necessary to manage multiple scenarios. Stores only rudimentary texture data for now.
- Added background animation screen for the Archaic Greece scenario, the first in development.