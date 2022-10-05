# Tabletop Game Description Language

## Table of Contents
- [Tabletop Game Description Language](#tabletop-game-description-language)
  - [Table of Contents](#table-of-contents)
- [Language primitives](#language-primitives)
- [Types](#types)
  - [Predefined types](#predefined-types)
  - [Supplied predefined types](#supplied-predefined-types)
  - [Input types](#input-types)
  - [None Value](#none-value)
  - [Lists](#lists)
  - [Type checking](#type-checking)
  - [Type casting](#type-casting)
- [Expressions](#expressions)
  - [Binary Operations](#binary-operations)
    - [Math Operation](#math-operation)
    - [Comparison Operations](#comparison-operations)
    - [Equality Operations](#equality-operations)
    - [Logic Operations](#logic-operations)
    - [Generic operations](#generic-operations)
  - [Unary Operations](#unary-operations)
  - [Order of evaluation](#order-of-evaluation)
- [Statement](#statement)
- [Actions](#actions)
  - [Inputs](#inputs)
    - [Input Filters](#input-filters)
  - [Triggers](#triggers)
    - [Placeable Movement Events](#placeable-movement-events)
    - [State Events](#state-events)
    - [State Attribute Events](#state-attribute-events)
    - [Callable](#callable)
  - [Requires](#requires)
  - [Effects](#effects)
  - [Global and Named](#global-and-named)
  - [Phases](#phases)
  - [Action Fail](#action-fail)
  - [Action priority](#action-priority)
  - [Action transaction](#action-transaction)
    - [Action Failures](#action-failures)
- [State](#state)
  - [Attributes](#attributes)
  - [Value Substate](#value-substate)
  - [Action Substate](#action-substate)
  - [Scopes](#scopes)
    - [Local](#local)
    - [Groups](#groups)
    - [Global](#global)
  - [Transferability](#transferability)
    - [Local states](#local-states)
  - [Group states](#group-states)
- [Interactables](#interactables)
  - [Placeable](#placeable)
  - [Stackables](#stackables)
- [Stack](#stack)
- [Players](#players)
  - [Starting player](#starting-player)
  - [Winner selection and game end](#winner-selection-and-game-end)
  - [Turns](#turns)
    - [Turn phase](#turn-phase)
- [Board](#board)
  - [Boardcells](#boardcells)
  - [Groups](#groups-1)
    - [Hex cell type](#hex-cell-type)
    - [Square cell type](#square-cell-type)
    - [Adjacency cell type](#adjacency-cell-type)
    - [Border Cells](#border-cells)
  - [Board Changes](#board-changes)
  - [Group Changes](#group-changes)
- [Movement](#movement)
  - [Default terms](#default-terms)
  - [Defining movement](#defining-movement)
  - [Board checks](#board-checks)
    - [Adjacent places](#adjacent-places)
    - [Connected place](#connected-place)
- [Goals](#goals)
- [Setup](#setup)
- [Verbs](#verbs)
- [Keywords](#keywords)

# Language primitives
- identifier: sequence of letters or numbers starting with a letter;
- comments: a comment is everything till the end of line character from the double slash characters `\\` 
  - they are ignored and are only for informing, documenting or in general leaving messages inside the code

# Types 

## Predefined types 
TGDL supports a small set of predefined types for simplicity and ease of use for non skilled users.
The standard supported predefined types are:
- decimal: for numbers both integer and floating point
- string: for descriptions or storing text ( no string manipulation is provided and they are not supported in the majority of expression operations )
- bool: boolean values true and false

## Supplied predefined types
Supplied predefines types are user declared types ( or defaults if not declared ) that are automatically supplied to 
the consumer action, they are the following types:
- player 
- global states
- board 

## Input types 
Inputs types are choosen by the player or players when required in an action they are:
( **Experimental: input types can be supplied from another action in a callable** )
- local and group states
- boardcell

## None Value
`none` is a special value that any type can have and it indicates the absence of the value for that specific attribute ( unassigned )

the none value behaves differently than normal types in comparisons:
- if two attributes with none value are confronted they will always be different `attribute1 = attribute2 => falls if attribute1 and attribute2 are none`
- an attribute with none value is equals to a none literal `attribute == none -> true if attribute is none`
- a none literal is equals to another none literal `none == none => true`

## Lists
List are a set of multiple instances of the same type that are aggregated under a single named variable.

Declaration: `type[] <list_name>`
Access to a single instance: `<list_name>[decimal]` (if a number with a decimal part is inserted, the decimal is ignored), when an out of index access is done it returns none
List Lenght: `<list_name>.lenght`
Inserting: `<list_name>[decimal] = <instance>` to append `<list_name>[<list_name>.lenght] = <instance>`
Initialization: `type[] <list_name> = { <instance>, <instance2>, <instance3> };` ( can be empty to initializes an empty list)

## Type checking
Type checking is done trough the `is` keyword: `<attribute> is <type>` is a type checkign expression and will return true if the attribute
is of type `<type>`, otherwise return false

## Type casting
type casting is used to change an attribute from a type to another type, it is done trough the `as` keyword `<type> id = <val> as <type>`
the as expression has two possible returns:
- the variable with the new type if it is allowed
- none if the variable is not allowed to be of the new type

# Expressions 
An expression is a combination of one operator and one or more operands.
Every type of expression defines the result type and legal operands.

## Binary Operations
A binary operation is defined as an expression that involves two operands and one operators.
they are left associative by default, if otherwise it will noted as such.

### Math Operation
Math operations only allow **decimal** operands and as result a **decimal** type:
- addition: `<operand> + <operand>`
- substraction: `<operand> - <operand>`
- multiplication: `<operand> * <operand>`
- division: `<operand> / <operand>`
- power: `<operand> ^ <operand>` ( right associative )
- modulo: `<operand> % <operand>`

### Comparison Operations
Comparison operations only allow **decimal** operands and as result a **bool** type:
- less than: `<operand> < <operand>`
- greater than: `<operand> > <operand>`
- less or equal than: `<operand> <= <operand>`
- greater or equal than: `<operand> >= <operand>`

### Equality Operations
equality operations allow both **decimal** or **boolean** operands, the two operands must be of the same type in the same operation,
and as result **bool** type;
- equal: `<operand> == <operand>`
- not equal: `<operand> != <operand>`

### Logic Operations
logic operations only allow **boolean** values and as result **bool** type
- and: `<operand> and <operand>`
- or: `<operand> or <operand>`
- xor: `<operand> xor <operand>`
- nand: `<operand> nand <operand>`

### Generic operations
generic operations don't fall under a specific category:
- state attribute access: '<state>.<attribute>'
- is operation: `<construct> is <type>`: returns a boolean value, true if the construct is of type otherwise false

## Unary Operations
A unary operation is defines as an expression that involves one operand and one operator, the following are supported in TGDL:
- plus: `+<operand>`
- minus: `-<operand>`
- not: `not <operand>`

## Order of evaluation
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

# Statement
a stamentent is a combination of clauses and expressions that end with a semicolon, the following are supported in TGDL
- return: `return <expression>;`
- expression: `expression;` 
- assignment: `<attribute or variable> = <expression>;`
- declaration: `<type> <attribute or variable>;`
  - an assignemnt can be chained to perform an initialization of the declared variable or state attribute
- if: `if expression {}`
- while: `while expression {}`
  - break: exits the while without evalueting the expression
  - continue: goes to next iteration (it evalueates the expression, if false exits)
- turn: `pass expression;`

anything that isn't an expression or a body is a clause (ex. return, =, if, while, pass, ...).

# Actions
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

any of the actions can be deactived or activated trough the allowed attributge associated with an action `<state>.<action>.allowed = <boolean>;`

## Inputs
An action input are the constructs that will be used troughtout the action definition and execution.
It works trough method dependency injection following the subsequent rules:
1. if its a supplied type it will be supplied from the translation / interpreter
2. if its a declared type it will be asked to the user to choose (if the require are satisfied else no choice is given and the action fails, check action fail):
3. if its a Predefined types it will ask the user to choose ( if possible restrict the values so that it will always satisfy the require, or give an indication when satisfied)

**Attention**: every action in the action substate of a specific state has as automatic input its value substate ( this could generate a name conflict if an input
has the same identifier of an attribute of its value substate)

### Input Filters
Input filters can be defined using a block after the input statement that returns a boolean value, true if the current examined value passes the filters or false if it does not pass the filter.

an input filter reduces the possible selections from all the instances of a certain type to a specfici subtype satisfying the given filter

```
input <type> <name>
{
  <statements>
}

// alternative input filter declaration
input <type> <name>
{
  filter [<type> <name>]
  {

  }
}
```

alternative filter declaration uses the filter keyword ( only accessible in the input context block ) that defines a filter for the 
specific type, used generally for list filters ( see input filters for list ), and a generic filter that derives the type and name of the
object to filter on from the input parameters.

multiple filters mapped on the same type will be evalueated one after the other, if one fails the input fails.

an example can be of an action that can be applied only on states with higher points
```
state player_state
{
  decimal points = 0;

  action on_higher_points
  {
    input player_state other
    {
      return other.points > points;
    }
  }
}
```

**WARNING INPUT FILTER FOR LIST**
to have better control on a list, its input filters need two separate definitions one to filter for the instances that will go in the list
and one to filter if the list is in an allowed state or if the input will fail.

**ATTENTION input lists cannot be used with predefined types**

```
state example 
{
  decimal points = 0;
}

action list_input
{
  input example[] examples 
  {
    filter example ex 
    {
      return ex < 10 and ex > 0;
    }
    filter
    {
      return examples.lenght < 3 and examples.lenght > 0;
    }
  }
}
```

first the filter for a single example type is run so that the player can choose between one of the choices,
after every selection made by the users the generic filter runs to check if the lists fails the check thus failing the input

**ATTENTION** to get all of a non literal type in a list the **all** keyword can be appened to the input statement, if a all keyword is used
there should no input filters.
the all keyword bypasses the need for a player to select the input



## Triggers
A trigger is a body of statementes than when evalueated true will launch the corresponding effects

```
trigger <trigger event> [<params>]
{
  <statements>
}
```

possible triggers events are:
- `movement <movement>`
- `<state>.action` ( < state > can not be in input )
- `callable`
- `change <state>.value`
- `<state>.<action>.<phase>` 

the inputs accessible from the trigger body depend on the specified trigger event, every event should define which attributes it allows access

events:
- state
  - attribute change {} 
  - action {}
- placeable
  - on placement
  - on removal
  - on movement
- stackable
  - on drawn
  - on discarded
  - on kept
- stack
  - on draw card
  - on shuffle
  - on reshuffle
  - on put card
  - on discard
- turn
  - on turn activation
  - on turn disactivation
  - on turn phase change

### Placeable Movement Events
### State Events
### State Attribute Events

### Callable
callable is a specialized trigger that allows action to be called in other state actions with the following syntax:

```
action <triggerable>
{
  input 
  {
    decimal a;
    <state> s;
  }
  trigger { callable; }
}

action <action>
{
  effect 
  {
    call <triggerable> with 
    {
      a = 1;
    } // will call with one of the inputs already supplied, what is missing will be asked in the defaults modes

    call <triggerable> with
    {
      a = 1;
      s = <state>;
    } // will call with all parameters specified even declared or supplied types
  }
}
```

## Requires
A require block is used to allow or disallow an action, it can contain two other tags (input and condition)
the input tag defines what the condition will use as input ( like the effect block in an action )
the condition block is defined as a body that **must** return a boolean value:
- true = requirement satisfied
- false = requirement not satisfied

condition can be named but there **must be** a unnamed condition that is the only thing that will run.

```
require
{
  input {}
  condition {}
}
```

## Effects
An effect is a body of statements or statement that interacts with the inputs
```
effect
{
  <statements>
}
```

## Global and Named
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

## Phases
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

## Action Fail
An action fail happens when a require is not satisfied, it follows the defined fail policy that can be declared in the default section.

## Action priority 
As default the actions are executed in the same order as they are defined in the file, that equals a priority of value 0.
An action priority can be changed trough the keyword `priority <decimal>` that assigns a arbitrary priority to the action.
a lower number or means a lower priority and viceversa a higher number means an higher priority

example:
```
action low_prio priority -10 {}
action default_prio {}
action default_prio_spec priority 0 {}
action high_prio priority 10 {}
```

## Action transaction
a player action creates a tree of changes trough its effects and the effects of the triggered actions called a transaction.
if a transaction has failures all the transaction is cancelled and its effects are reverted

### Action Failures
an action fails when a it reaches a failure statement:
- `fail "message";` -> fails the entirety of the transaction
- `cancel [<effect>] "message"` -> 
  - if no effect is specified fails only the global effect and stops all the triggers from activating ( and reverts the triggers that activated with the before keyword )
  - if an effect is specified it fails that specific effect
  - if `all` is specified for effects the entire action fails without propagating backwards to the trigger action 

# State
A state is a construct to describe values and actions related to eachothers such as roles.
a state is divided in two substates, the value substate and the action substate.

## Attributes
A state attribute is defined as a variable declaration (and optionally an assignment) inside the state declaration

```
state <state>
{
  <type> <identifier> = <default> or <none>;
  <type> <identifier> = <default> or <none>;
}
```

the **none** keyword rapresents the absence of an assigned value to an attribute and it is the default for every type if assigned a default initializer value.

## Value Substate
the value substate contains the state attributes which the state actions or other state actions can use.
it is implicitly defined as the set of attributes defined in the state declaration ( if no attribute is declared then the value state is empty ).

## Action Substate
The action substate is the set of all defined action inside a state declaration.
It can be accessed and/or modified by any other action or pseudo-action if the state containing the action substate is not decared with the **sealed** keyword.

## Scopes
State scopes change the behaviour and attachment of a state to a player

### Local
```
local state <state>
```
the local scope is the default scope, they are transferrable state with a unique instance for each player that it was assigned it.
local states are the meat of a TGDL program and manage abstractions such as roles, default player actions, ect...

### Groups 
```
group state <state>
```
the group scope defines a state that has one instance assigned to a subgroup of players.
players can be assigned or removed from the group state trough a transfer.

group states serve the purpose of having values or actions shared between a subset of players 

**WARNING:** at the moment only one instance of a defined group state is allowed due to syntax restrictions in defining multiple instances 

### Global
```
global state <state>
```
the global scope defines a state that has only one instance and cannot be assigned to any player, the ownership of the global state is the game itself.
a global state defines values and actions that encompass values outside of the players but that can be influenced from them in some shape o form.

an example of a global state use case is the presence of global parameters that dictate the flow of the game, the parameters have no attached player to them
but the are still a part of the game.

global states are a supplied type, **no other identifier can be the same as a global state name**

## Transferability

### Local states
local states can be transferred from a player to another player trough the **transfer** operation.
The transfer keeps the value state constant during the transfer opereration ( a special trigger can be setup to reset/change the value substate on transfer to another player ).

the player that acquired the state can now access the state substates, on the other hand the other player has lost the rights to access the substates of the transferred state

## Group states
a group states cannot be transferred as a local state, the transfer operations change the attachments of players to the group states.
players can be removed or attached to a group state, acquiring the access to its substates.

actions can be specified with special triggers ( on remove and on attach ) to perform actions on the respective removal or attachment of a player to the group state
( this special trigger, can be specialized with a state name to trigger only when attached or deattached from that specific state )

**WARNING:** the special triggers are available to local states as well

# Interactables 
interactables are objects that abstract the concept of interaction with the player trough phisical objects like tokens, cards, ect...

```
interactable <name> [<type>|<type>,<type>,...]
```

## Placeable
Placeable is a type of interatable, it interacts with boardcells and board groups
Placeable have specialized triggers to enhance their functions (defined in the respective section reletaed to action triggers):

a placeable stores a reference to the boardcell that it occupies ( if not placed the boardcells is none ).
and viceversa a boardcells stores the reference to all the placeable placed on itself trough a placeable list

## Stackables


# Stack

# Players
A player is defined as a decimal, every state has a hidden state attribute called `player` that corresponds to the player to which the state is attached to.

## Starting player
the starting player is always the player numbered 0.

## Winner selection and game end
`winner <player> [,<player>]*`: sets the winner or winners for the game and afterwards ends the program

## Turns
A turn defines who is the active player ( in most games who is taking the actions, there are games where it's permitted to act out of turn like magic the gathering ).

to define the passing of turns there are two main ways:
- using directly the turn statement `pass <decimal returning expression>;`
- defining actions in the global state that call the turn statement 

example for global turn action
```
players 4;

global state turn_controller
{
  action default_turn
  {
    input
    {
      activePlayer ap;
    }
    trigger
    {
      any action 1
    }
    effect
    {
      pass ap + 1;
    }
  }
}
```

### Turn phase
a turn can be defined with the following syntax:
```
turn <turn>
{
  phase <phase>
  {
    trigger {}
    effect {}
  }
  ...
}
```
every turn can have multiple phases, if no phase is defined the turn will only have one phase with **default** as the name.
each phase can have triggers to start it, global inputs ( all of some type, board, global states) and an effect 

a phase if has a callable trigger can be called to activate that phase effect and switch the turn phase to the called `call <turn>.<phase>`.
a turn can be started from its first phase trough the call keyword `call <turn>`, this will deactivate the active turn and activate the selected turn 

# Board
the board is the construct used to rapresent something that can be interacted with by the players trough placing tokens, claiming cells or other actions.
it is globally defined, meaning there is only one instance of the board in the entire game, a board can have attributes.

## Boardcells
a boardcell is a specialized state-like definition trough the **boardcell** keyword, it can have multiple instances of the same declaration like local states and players.

```
boardcell <cellname>
{
  <attributes>
  <actions>
}
```

the concept of boardcell is abstracted by its geometrical nature that is defined by the group type

boardcells have two implicit attributes:
- `board`: reference to the global board
- `group`: reference to the group in which they are attached

to do group manipulation or other that requires a specific group, type checking and casting can be used
```
if(<boardcell>.group is <group>)
{
  <group> <id> = <boardcell>.grop as <group>;
}
``` 

## Groups
a board contains groups of boardcells, every group can contain only one type of cell between the following:
- hex cells
- square cells
- adjacency

like the board, a group is globally defined and can have attributes ( they are declaration and assignement statement inside the group beside the group boardcells definition)

all the groups can contain the blank cell (used only for spacing is an empty space) used in the group declaration with the keyword `b`
the cell type is used to define default terms like **line**, **distance**, **adjancency** and **coordinates**,  

Example
```
board
{
  group <name> square
  {
    <cell>, <cell>, ...
    <cell>, ...
    <cell>, <cell>, ...
    <cell>, <cell>, ...
  }
}
```

### Hex cell type
an hex cell group can have two orientations, **column** (face side up) or **row** (point up), that define the overall look of the board group and changes how the group definition is interpreted

col definition has even rows displaced by one unit from the top.
row definition has even rows displaced by one unit to the right.

in column orientation a line from left to right equals a column from top to bottom.
in row orientation a line from left to right equals a row from left to right.

for coordinates in both column and row orientation the origin hex is the one in the upper left (0,0)
- in rows the first number is the row and the second is the position of the hex in the row from the left 
- incols the first number is the column and the second is the position fo the hex in the column from the top

<span>
<img src="images/board/hex_row_coords.png" />
<img src="images/board/hex_col_coords.png" />
</span>

distance is defined as the following = abs(x_1 - x_0 + y_1 - y_0)



examples with a defined boardcell
```
boardcell h 
{
  <attribute>
  <attribute>
  
  <action>
  <action>
  <action>
}
```

<table>
<tr>
<td>

```
group < name > hex row
{
  h,h,h;
  h,h,h;
  h,h,h;
} 
```
</td> 
<td>
<img src="./images/board/hex_row1.png"/>
</td>
</tr>
<tr>
<td>

```
group < name > hex row
{
  h,h,h,h,h;
  h,h,b,h,b;
  b,b,h,h,h;
} 
```
</td> 
<td>
<img src="./images/board/hex_row2.png"/>
</td>
</tr>
<tr>
<td>

```
group < name > hex row
{
  b,b,h,h,h,h,h,b,b;
  b,h,h,h,h,h,h,b,b;
  b,h,h,h,h,h,h,h,b;
  h,h,h,h,h,h,h,h,b;
  h,h,h,h,h,h,h,h,h;
  h,h,h,h,h,h,h,h,b;
  b,h,h,h,h,h,h,h,b;
  b,h,h,h,h,h,h,b,b;
  b,b,h,h,h,h,h,b,b;
} 
```
</td> 
<td>
<img src="./images/board/hex_row3.png"/>
</td>
</tr>
<tr>
<td>

```
group < name > hex col
{
  h,h,b,h,h;
  h,b,b,h,h;
  h,h,h,b,h;
  h,h,h,h,h;
} 
```
</td> 
<td>
<img src="./images/board/hex_col1.png"/>
</td>
</tr>
</table>

by default all hexagons are adjcency with their neighbors, it is possible do modify the adjancency rules in a group using the adjacency keyword and the correspective coordinates
```
group <name> hex col
{
  h,h,h;
  h,h,h;
  h,h,h;

  adjency
  {
    (0,0) -> (2,2); // for one way adjacency
    (0,0) <-> (2,2); // for two way adjacency
    (0,0) 1 <-> 3 (2,2); // specify distance between adjacencies ( default is 1)
    (0,0) x-> (0,1); // one way removal of adjacency
    (0,0) x-x (0,1); // two way removal of adjacency 
  }
}
```

a line in a hex group is defined as a segment exiting a face or border of the hexagon

<img src="board/../images/board/hex_row_lines.png"/>

### Square cell type
square cell has the same distance, line, coordinates and adjacency rules of an hex type cell with the following exceptions:
- there are only 4 lines numbered 0 ( to top ), 1 ( to right), 2 ( to bottom ), 3 (to left)
- there are no orientations distinctions
- coordinates have origin (0,0) at top left, the first number is the row the second is the column.
- square cells have two type of default adjcency: **around** or **sides**

### Adjacency cell type
adjacency cell types are different from both hex and square types in that they can rapresent any kind of board by using a graph notation:
- nodes are the cells
- edges are the adjacency and define the distance between the nodes that connect

in an adjacency cell type group there is no concept of line or coordinates, just of distance and adjancency.

```
group <name> adjency
{
  <id1> -> 3 <id2> 
}
```

### Border Cells
A border cells is a cell that connects to another group cell.

The definition syntax is the same as a the adjacency list, because the bordercells list is a glorified adjacency list starting off as empty 

```
board
{
  group first {}
  group second {}
  boardcells
  {
    <first.cell> -> <second.cell>
    <first.cell> <-> <second.cell>
  }
}
```


## Board Changes

## Group Changes

# Movement 
Movement is the act of removing a placeable from a boarder cell and placing in another boardercell.
Custom movements can be defined trough default terms and movement operators. with an action-like syntax

any movement must follow the following pattern:
```
<placeable> <movement> <parameters>
```

## Default terms
generic terms valid for any type of cells:
- adjacent <n>: a movement executed trough the adjacency for n times
- distance <n>: a movement executed for n distance
- jump <identifier>: jumps to the subject to the specific cell ( identifier can be a group name, the player will decide where it will end up inside the group )

square and hex type movements:
- line <number> <n>: movement in the direction of line <line> for <n> steps
- coord <x> <y>: jumps to coordinate

modifiers:
- max <n>: modifies n to be an interval from 0 to n
- min <n>: modifies n to be an interval from n and up

## Defining movement
Movements can be defined like in the following example 

```
movement chess_rook 
{
  input <placeable> line1;
  input deciaml lin2
  effect
  {
    line line1 2;
    line line2 1;
  }
}
```

every movement function is checked if it is allowed, if the movement is not allowed it is not displayed.

multiple effects on a movement represent alternatives and need to be chosen by the player.

movements can have triggers, require and phases.


## Board checks

### Adjacent places
this check is used to interact with cell or placeable nearby to a specific cell or other placeable
```
placeable/boardcell adj placeable/boardcell -> returns a boolean value
```

checks if a placeable or boardcell has beside a placeable or boardcell of the given type

to get all adjacent boardcells to a boardcell an hidden attribute is provided called **adjcents**

### Connected place
this is check is used to interact with connected cells ( cells that have a path between them )
```
placeable/boardcell connect placeable/boardcell
```

# Goals
Goals are specific actions that will terminate the game if the global effect is triggered.
The syntax is the same as a state action, it supports naming and phases:
```
goal <id> [priority]
{
  input {}
  require {}
  trigger {}
  effect {}
}
```
goals can have a priority, the higher priority goals will be run before the lower priority goals

# Setup
The setup is the initial stage of the game where everything is put in place before the first player action is played.
```
setup { }
```
the setup can have inputs and effects:
- allowed inputs are the types that do not require an input from a player (global inputs)
  - lists of types with all keyword ( players, states, ect...)
  - any of the global types like board, global states, groups, stacks
- there can be named effects but there **must** be a unnnamed effect ( that will be run for the setup)
- as all statement blocks it can interact trough the dictionary method with any of the instances or class

# Verbs
Verbs are a definition to group and reuse a certain set of statements on a specific set of inputs

```
verb <name>
{
  input {}
  effect {}
  return <type>;
}
```
a should define a return type ( if it returns something ), a verb is called with the parenthesis syntax `<name>(comma separated params)`

# Keywords
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
- // is a comment
