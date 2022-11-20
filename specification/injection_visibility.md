# Dependency injection visiblity
What can the player see and how is it shown to him.
a player injector sees:
- everything that is attached to a player and its dependencies ( states ect... )
- all the globals constructs and its dependencies
- some informations about constructs attached to other players

new tag proposal that defines what the owner and what the other players can see.

should have a filter that tells which player can see taht information ( some stuff can be shown to a player in the same team, but not to the opponent team )

```
visible
{
    filter
    {
        return <bool expression>
    }

    default {
        this.<attribute>
        this.action ect...
    }
}
```

multiple visible tags can be used in the same construct to defines what is visible for different filters.

a filter takes in a player and returns a true or false if the player can see the default tag in the visible tago

hidden actions are not accessible and can't be chosen by a player,
by default the actions are visible only to the owner of the construct 
(global -> everyone, group -> everytone in the grop, local -> attached player)

when a player can't see something it goes to a default hide 
- states are completely hidden with all its attributes and actions  
- attributes are completely hidden
- stacks are inaccessible
    - the interactables inside the stack inherit te visibily of the stack
- interactables on the board are always visible but if hidden they are non interactable

la interagibilitá con le cose é definita dai trigger per le azioni,
la selezionabilitá invece é definita da ció che é possibile vedere 

visibility influences what can be interacted by the player but ultimately it all comes down to what the filters in the inputs of the actions,
visibility main concern is if the opponents or the player sees the information about some object.

## PROPOSAL

all the informations that are a player can see ( he can still interact if the input of actions have filters that allow it ) are:
- states, attributes and actions that are in the state that he is attached to ( global, group and local)
- interactables on a board that is associated with an attached state 

a player can't see any information about the states that the other player posseses, attributes, interactables ect... 

