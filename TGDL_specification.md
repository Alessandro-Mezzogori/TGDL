# GeneralTabletop Description Language

## Table of Contents
- [GeneralTabletop Description Language](#generaltabletop-description-language)
  - [Table of Contents](#table-of-contents)
  - [Keywords](#keywords)
    - [Syntax](#syntax)
    - [Selectors](#selectors)
  - [Definitions](#definitions)
    - [Player](#player)
    - [State](#state)
      - [Scope Modifiers](#scope-modifiers)
  - [Action](#action)
  - [State Modifier ( or Playables )](#state-modifier--or-playables-)
    - [Decks](#decks)
  - [Board](#board)
- [Cose da fare](#cose-da-fare)

## Keywords
- player: references a specific player 
- players: references to all players, used to define the number of players 
- is: checks if player has state
- and: logic and
- or: logic or
- not: logic not
- \+
- \-
- \*
- /
- mod: modulo operator
- if: used for conditions
- state: defines a state 
- global: global scope modifier
- local: local scope modifier
- group: group modifier for a local state
- has: assigns state to player ( comma separated list)
- action: defines an action 
- require: defines the requirements for an action
- trigger: defines the triggers for an action
- board: defines the board of the game
- boardCell: refers to a cell of the board 
- goal: end game conditino
- random -> random funciton that returns a number from 0 to n non compres
- setup: allows different setups related to the number of players
- modifier

### Syntax
- `players <number of players> or <min>-<max>`
- `player <number>` (specific player) or `player` ( all players )
- `[global] or [local [group]] or [owned] state` defines a state with a scope modifier, default modifier is local
- `turn <state>`
- `setup <num players> or <min>-<max>`: used to specify specific rules correlated to the number of players 
  can be used inside a state definition like the following
  ```
  local/global state <state>:
    counter: 0

    action shared:
      counter = counter + 1

    setup 2-3:
      action shared:
        counter = counter + 2
  ```

### Selectors
- **external**: specifies that the state is external to the local or owned state, referes to a state attached to another player

## Definitions

### Player
**player** is defined as an acting person or a group

a player can be in a group, it inherits all the state that are attached to the group.

### State
**state** is defined as 
- action state: set of all rules that apply to the state and its childrens
- can be global or local
- every local state is a children of another local state or of the global state

state mutable but not directly assignable


#### Scope Modifiers
- **global**: specifies state not associated with a player/players, should contain parameters needed to move the game forward ( player turn ect... )
- **local**: attached to a player, can be transferred to another player
- **owned**: attached to a player, cannot be transffered to a player ( subset of local ) ( maybe not needed )
- **group**: should be used with local or owned, only one is created and shared with all attached players 

local and owned state can be associated with a role and must be assigned to a player, when assigning a role to a player 

the assignnment creates a new copy of the state with its default value state ( new reference ).

to transfer a state from player A to player B it must be used the keyword 
`transfer state from a to b.`
transfering a state changes the ownership of that state without modifying the value state.

## Action
An action can be defined with 3 constructs:
- Requirements: a requirement can be one of the following:
  - an action
  - a specific state ( condition, ex. **needs fridge** to store food)
- Triggers:
  - an action
  - a state change
- State changes: a state change is anything that changes a specific state
  - Cost: a obbligatory state change that must be satisfied for the other state changes to take place

## State Modifier ( or Playables )
Abstraction for everything that could be played such  as cards

Definition:
```
state modifier <identifier>:
  [playable by <state [, state, state]> ]
  [
    require:
      <require lambda>
  ]
  [
    effect:
      <action lambda>
  ]
```

A state modifier can access all the states given trough the lambdas and to the state given
by the **playable by** statement.

A state modifer can add, remove, lock or unlock methods from a given state like any other action lambda

When drawn they are attached to a player.

They can be played only if the attached player has at least one of the state in the playble by statement

### Decks
Decks are a constructor for holding and drawing ( using a specific algorithm or randomly ) a state modifier,
a state modifier can have multiple copies of itself in a single deck

Definition
```
deck <identifier>:
  [
    <list of state modifiers> [x <number_in_deck>]
  ]
```

Example:
```
deck main:
  potatoes [x 1]
```


Example:
```
state player_state:
  ...

state modifier potatoes:
  playable by player_state
  require:
    _ => player.plants >= 2
  effect:
  _ =>
    player_state.plants = player_state.plants - 2
    player_state.money_production = player_state.money_production + 2
```

Defines a state modifier called potatoes.

a state modifier needs 

## Board 
There can be multiple or no boards either globals or locals ( interaction gated to one player )

Boards can be either:
- static, defined in code or in another file 
- dynamic, defined trough the gameplay

a board can start of as static and be modified by the actions taken by the players  effectively becoming dynamic.

# Cose da fare
- tenere lambda come nome
- require block:
  - piú require lambdas
  - lambda é una funzione cui nome é data in automatico in referenza al block in cui é stata definita:
      ```
        require:
          this.test is not none                   // unique name <state>.<action>.<require>.<number>
          x => x.access less than 3               // i require sono messi in una lista di espressioni sono valutate tutte
          y, x => y.access greater than x.access  // puó accedere ai parametri dell'azione a cui é associata
      ```
  - la lambda é definita tramite operatore =>
  - un qualsiasi altro codice é definito con operatore di default :
  - se ha solo uno statement esso é anche il return