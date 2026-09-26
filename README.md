# Unity Quest System

A modular quest system developed in Unity and C# for my game **Lords of the World**.

This repository contains a simplified portfolio showcase of the quest system used in the game. Game-specific content and unrelated dependencies have been removed to keep the code focused on the core quest architecture.

## Features

- Multiple quest types including Main, Side, Daily, Weekly and Tutorial quests
- Multiple objective types including:
  - Kill
  - Collect
  - Talk
  - Travel
  - Reach Level
  - Train Troops
  - Upgrade Buildings
  - Produce Resources
- Multi-objective quests
- Quest requirements and progression tracking
- Item, Gold, Ruby and Experience rewards
- Random side quest selection
- Duplicate quest prevention
- Quest rank system
- Dynamic quest UI generation
- Building, troop, resource and player-level progression quests
- Periodic quest list refreshing

## Architecture

The system separates quest data, quest progression and UI responsibilities across multiple scripts.

### Quest.cs
Contains the core quest data model, quest targets, rewards, quest types, objective types, ranks and NPC definitions.

### DataQuest.cs
Creates and stores quest data. It also demonstrates programmatic generation of progression quests such as building upgrades, troop training and resource production.

### QuestManager.cs
Manages random side quest selection, prevents duplicate quests, controls available quest slots and refreshes the quest list.

### QuestProgressManager.cs
Handles quest progression and completion when gameplay events occur, including combat, collection, dialogue, building upgrades, troop training, resource production and player-level progression.

### StructureQuestPanel.cs
Handles the UI representation of progression quests and separates available quests from completed quests.

## Progression Approach

Quest progress is updated when relevant gameplay events occur rather than continuously checking every quest every frame.

For example, defeating an enemy can trigger the Kill/Collect quest progression logic, while upgrading a building triggers only the building-related quest progression logic.

This approach keeps unrelated quest checks out of the main gameplay update loop.

## Example Quest Flow

1. Quest data is created and stored in `DataQuest`.
2. Available quests are selected and displayed by the quest managers.
3. The player accepts a quest.
4. Gameplay events notify `QuestProgressManager`.
5. Matching quest targets are updated.
6. The quest is marked as completed when its requirements are satisfied.
7. Rewards are granted according to the quest configuration.

## Technologies

- Unity
- C#
- TextMeshPro

## About

This system was developed as part of **Lords of the World**, a solo-developed City Builder / Action RPG project.

This repository contains only a selected and simplified portion of the original game's source code for portfolio purposes.