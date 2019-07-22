# AgribattleArena

Yet another generic browser TBS game.

## AgribattleArena.BackendServer
ASP.NET Core server-side app. Progress:
- [x] Authorization api
- [x] Profiles api
- [x] Characters store api
- [x] Queueing and matchmaking api
- [x] Battle engine implementation
  - [x] Interfaces for interacting with AgribattleArena.Engine
  - [x] Communication with client (SignalR)
- [ ] Catalogs api
- [ ] \(Postponed to the future releases) Battle history api 

## AgribattleArena.BrowserClient
Angular client-side app. Progress:
- [x] Authorization component
- [x] Loading and error handling
- [ ] Frontend battle engine
  - [x] Communication with server (SignalR)
  - [x] Events and tokens
  - [x] Synchronization
  - [ ] Scene
  - [ ] Player controls
- [ ] Engine unit tests (Karma & jasmine)
- [ ] Scene drawing (Pixijs)
- [ ] Battle ui
- [ ] \(Postponed to the future releases) Store component

## AgribattleArena.Configurator
.NET Core application for db catalogs filling by json documents. Progress:
- [x] Store entities
- [x] Server-side engine natives
- [x] Revelation levels
- [ ] Client-side engine natives

## AgribattleArena.DBProvider
.NET Core library with EF contexts and entities. Progress:
- [x] Profiles db
- [x] Natives db
- [x] Store db
- [ ] \(Postponed to the future releases) Battle history db

## AgribattleArena.Engine
.NET Core library with server-side battle engine functionality. Progress:
- [x] Scene
- [x] Generation engine
- [x] Natives engine
- [x] Synchronization
- [x] Duel generator
- [ ] Natives action methods

## AgribattleArena.Tests
NUnit project for unit testing. Progress:
- [x] AgribattleArena.Engine tests
