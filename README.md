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
- Archived completed version of input system and scaler into *snapshot/input-system-v1*
- Created *develop* branch, which will now contain all work: main represents latest stable build, updated through PRs from *develop* instead of direct commits. Enforced with a protection ruleset.

**8/29/26**
- Created a new *Classes* folder in the Scripts folder to hold definitions for objects representing important game elements.
- Created rudimentary **Province.cs** and **Building.cs** variable schema, boilerplate for other files.
- Created a *Text* folder to hold display text instead of directly writing in logic files - will help with localization later.
- Updated **Building.cs** to define subclasses of a parent Building class - building IDs unneeded as types will be directly initialized. Upgrade cost and time, benefits determined by tier index.
- Added some display text construction for each building, to be expanded later.

**8/31/26**
- Created Victory type building to earn victory points and raise stability when built.
- Created an *Assets/Text* folder - text is stored in .JSON files, not .cs files, to separate logic from data files.
- Made **BuildingText.cs** file in the Scripts/Text folder to load JSON data based on scenario ID. **ScenarioManager.cs** now calls a helper function to get building text based on id, which is referenced by **Building.cs** to get a set of all building text for a scenario.
- ScenarioID has an implementation for loading JSON, while BuildingID selects the specific building text entry in the scenario block.