70. define the mechanism for reversing the game state on failures:
    1.  change tree concept ( changes of the state in the action tree following the triggers )
    2.  copy of the state each input and action's state that is influenced by the changes but they are applied only after the whole action is applied z
    3.  make a way to preseve changes even on failures 
    4.  effect failure destroys the branch of the change tree
    5.  an action failure destroys the whole change tree 
71. definire cosa deve essere fatto vedere al giocatore ( esempio quali azioni ect... )
73. when should event start to be generated ( inside setup or after setup ? )
74. typed stack ( stack that only accepts one type of interactables ) 
76. events:
    1.  board events
        1. attribute change
    2.  group events
        1.  group change
    3.  tile events
        1.  moved from
        2.  moved in
        3.  moved
    4.  stack events
    5.  turn events
        1.  on turn active
        2.  on turn inactive
77. access global informations:
    1.  turn:
        1.  phase
        2.  turn
    2.  players:
        1.  active
        2.  number of playerss
78. define the dependency injection trough input:
    1.  construct tracking
79. body less tags
80. language conventions 
    1.  functions and actions in snake case
    2.  state names with with first upper case ect...
    3.  list functions snake case
81. are goals needed  ? 
    1.  functions of goals
    2.  convoluted ways to trigger them
    3.  change the functionality to a simple check win check that is triggered based on events ?
    4.  transfer the actual end of game and selecting winnenr to the winner statement
82. trigger target specificity ( ex. interactable -> placeables -> specific placeables / interactable ect... )
84. scrivere tag returns spec
85. IOC and DI tracking definition, does it track every new instantiaed construct ? how to free memory then ?
    1.  new is for temporary objects ( destroyes at end of scope )
    2.  another way to create and save to dependency container objects 
86. input prompts to guide 
87. special premade filters 
    1.  ex. choice from a list of possibilities
    2.  could be introduces with filter construct for reuseability -> premade filters are just predefined filter constructs 
88. should all special functions like move, winner ect... be called normally with parenthesis operators ()
    1.  simplifies language parsers
    2.  simplifies intellisense-like helpers
    3.  removes some gated keywords  
89. way to start/stop generation of events at will 
90. exceptions are for grave faults like lua
91. error checking like lua ( none is)
92. creating and placing tiles to a group dinamically
// scacchi
1. setup
2. castleing
   1. rook e king non si devono essere mossi
   2. gli spazi tra 
   3. rook e king devono essere liberi
   4. gli spazi tra rook e king non devono essere in visione dell'avversario
   5. king non deve essere sotto scacco
   6. sia rook che king devono essere liberi di muoversi ( non credo sia un problema  controllare perché é impossibile fare un pin del rook )

// santa monica 

// tris
