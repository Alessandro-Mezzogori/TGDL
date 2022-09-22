# Tabletop Game Description Language

## Table of Contents
- [Tabletop Game Description Language](#tabletop-game-description-language)
  - [Table of Contents](#table-of-contents)
  - [Types](#types)
    - [Predefined types](#predefined-types)
    - [Supplied predefined types](#supplied-predefined-types)
    - [Input types](#input-types)
  - [Expressions](#expressions)
    - [Binary Operations](#binary-operations)
      - [Math Operations](#math-operations)
      - [Comparison Operations](#comparison-operations)
      - [Equality Operations](#equality-operations)
      - [Logic Operations](#logic-operations)
      - [Generic operations](#generic-operations)
    - [Unary Operations](#unary-operations)
    - [Order of evaluation](#order-of-evaluation)
  - [Statement](#statement)
  - [Actions](#actions)
    - [Inputs](#inputs)
    - [Triggers](#triggers)
    - [Requires](#requires)
    - [Effects](#effects)
    - [Global and Named](#global-and-named)
    - [Phases](#phases)
  - [State](#state)
    - [Attributes](#attributes)
    - [Actions](#actions-1)
    - [Scopes](#scopes)
    - [Global State](#global-state)
  - [State Modifiers](#state-modifiers)
  - [Stackables](#stackables)
  - [Placeable](#placeable)
  - [Players](#players)
  - [Board](#board)
  - [Defaults](#defaults)
  - [Keywords](#keywords)
    - [Syntax](#syntax)
    - [Selectors](#selectors)
  - [Definitions](#definitions)
    - [Player](#player)
    - [State](#state-1)
      - [Scope Modifiers](#scope-modifiers)
  - [Action](#action)
    - [Action Phase](#action-phase)
  - [State Modifier ( or Playables )](#state-modifier--or-playables-)
  - [State Modifier ( or Playables )](#state-modifier--or-playables--1)
    - [Decks](#decks)
  - [Board](#board-1)
- [Cose da fare](#cose-da-fare)
  - [Interprete / Programma](#interprete--programma)

## Types 

### Predefined types 
TGDL supports a small set of predefined types for simplicity and ease of use for non skilled users.
The standard supported predefined types are:
- decimal: for numbers both integer and floating point
- string: for descriptions or storing text ( no string manipulation is provided and they are not supported in the majority of expression operations )
- bool: boolean values true and false

### Supplied predefined types
Supplied predefines types are user declared types ( or defaults if not declared ) that are automatically supplied to 
the consumer action, they are the following types:
- player 
- global states
- board 

### Input types 
Inputs types are choosen by the player or players when required in an action they are:
( **Experimental: input types can be supplied from another action in a callable** )
- local and group states
- boardcell

## Expressions 
An expression is a combination of one operator and one or more operands.
Every type of expression defines the result type and legal operands.

### Binary Operations
A binary operation is defined as an expression that involves two operands and one operators.
they are left associative by default, if otherwise it will noted as such.

#### Math Operations
Math operations only allow **decimal** operands and as result a **decimal** type:
- addition: `<operand> + <operand>`
- substraction: `<operand> - <operand>`
- multiplication: `<operand> * <operand>`
- division: `<operand> / <operand>`
- power: `<operand> ^ <operand>` ( right associative )
- modulo: `<operand> % <operand>`

#### Comparison Operations
Comparison operations only allow **decimal** operands and as result a **bool** type:
- less than: `<operand> < <operand>`
- greater than: `<operand> > <operand>`
- less or equal than: `<operand> <= <operand>`
- greater or equal than: `<operand> >= <operand>`

#### Equality Operations
equality operations allow both **decimal** or **boolean** operands, the two operands must be of the same type in the same operation,
and as result **bool** type;
- equal: `<operand> == <operand>`
- not equal: `<operand> != <operand>`

#### Logic Operations
logic operations only allow **boolean** values and as result **bool** type
- and: `<operand> and <operand>`
- or: `<operand> or <operand>`
- xor: `<operand> xor <operand>`
- nand: `<operand> nand <operand>`

#### Generic operations
generic operations don't fall under a specific category:
- state attribute access: '<state>.<attribute>'

### Unary Operations
A unary operation is defines as an expression that involves one operand and one operator, the following are supported in TGDL:
- plus: `+<operand>`
- minus: `-<operand>`
- not: `not <operand>`

### Order of evaluation
1. state attribute acces
2. plus and minus
4. pow
5. multiplication, division and modulo
6. addition and subtraction
7. comparison operations
8. equality operations
9. not
10. and
11. or

## Statement
a stamentent is a combination of clauses and expressions that end with a semicolon, the following are supported in TGDL
- return: `return <expression>;`
- expression: `expression;` 
- assignment: `<attribute or variable> = <expression>;`
- declaration: `<type> <attribute or variable>;`
  - an assignemnt can be chained to perform an initialization of the declared variable or state attribute
  
## Actions
actions are a combination of instructions set by the user that are run on a trigger.

example:
```
action <action>
{
  input   { }
  require { }
  trigger { }
  effect  { }
}
```

### Inputs
An action input are the constructs that will be used troughtout the action definition and execution

### Triggers
A trigger is a body of statementes than when evalueted true will launch the corresponding effects

```
trigger 
{
  <statements>
}
```

### Requires
A requirement is a condition or body of condition that must be satisfied so that the corresponding effect can be applied

```
require
{
  <statements>
}
```

### Effects
An effect is a body of statements or statement that interacts with the inputs
```
effect
{
  <statements>
}
```

### Global and Named
Triggers, requires and effects can be associated to a shared subgroup through the name funcionality

```
trigger [for <identifier>] {}
require [for <identifier>] {}
effect [for <identifier>] {}
```

A named require, trigger or effect will interact only with the require, triggers or effects within the same subgroup.
An identifier can be assigned to **multiple** triggers, require and effects if needed.

Global action triggers, requires and effects are not named and can interact with the named counterparts, 
establishing a gerarchical hierarchy between the two.
A global trigger, require or effect is global to where it is defined ( action, phase ):
- global trigger: starts the execution of the global effect
- global require: filters the input / blocks the execution if no input satisfies the conditions 
To prevent code duplication and encourage use of naming when possible, the global action triggers, requires and effects can reference 
the named counterparts trough their identifier and boolean operations (not, and, or, nand, xor).

```
require for A {}
require for A {}
require for B {}
require { A and B} // satisfied when both A and B are satisfied
```

### Phases
Phases are introduces for multi effect actions with different requirements and / or triggers.
A phase declaration can only contain trigger, require and effect ( both named and global for that phase)


a phase is declared like in the following example.
```
phase
{
  trigger { }
  require { }
  effect { }
}
```

phases ( and actions ) have the option of a specifier in the following list that changes the behaviour of the effects:
- choice <n>: not applicable to a callable action, asks a player to choose n effects that have satisfied requires
- sequence: effects are executed in order of definition
- simultaneos ( experimental )

## State
### Attributes
### Actions
### Scopes
### Global State

## State Modifiers

## Stackables

## Placeable

## Players

## Board

## Defaults

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
- \\\\ is a comment

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
  
multiple requirements are in OR with eachother, the requirements inside one require block are in AND with eachother

### Action Phase 
An action phase is a set of effects with their optional require that is characterized by a specific rule

Syntax:
```
phase <name> [<rule>]
{
  
}
```

Rules:
- choice < n >: select n effects between all the effects with satisfied requires 
## State Modifier ( or Playables )
- sequence ( default ): is the default, the effects are executed in the order of definition ( of the effect keyword, the require keyword can be in any order)

## State Modifier ( or Playables )
Abstraction for everything that could be played such  as cards

Definition:
```
state modifier <identifier>:
  [playable by <state [, state, state]> ]
  [
    require [for <effect_id>]:
      <require lambda>
  ]
  [
    effect [for <effect_id>]:
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
  
## Interprete / Programma
The parsing in my prototype program is done following:
1. Syntax parsing: extract pure syntax information without checking beyond correct structure
2. Syntax validation: checks the correctness o the typed syntax ( expression types corresponding, invalid operations, ect...)
3. Syntax Translation: translate the syntax constructs in data structures in c#