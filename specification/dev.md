71. definire cosa deve essere fatto vedere al giocatore ( esempio quali azioni ect... )
74. . events:
    1.  board events
        3. attribute change
        5. action call
    2.  group events
        1. group change 
        2. tile add
        3. tile remove
    3.  tile events
        1.  moved from
        2.  moved in
        3.  moved
        4.  on placeable placed
        5.  attribute change
        6.  actino call
    4.  stack events
        1.  action call
        2.  attribute change
        3.  on draw
    5.  turn events
        1.  on turn active
        2.  on turn inactive
87. exceptions are for grave faults like lua
88. error checking like lua ( none is)
91. create new board groups 
92. global game state that contain every information about the current game
93. change pass to a function call
94. update images for groups 
 // scacchi
1. setup
2. castleing
   1. rook e king non si devono essere mossi
   2. gli spazi tra 
   3. rook e king devono essere liberi
   4. gli spazi tra rook e king non devono essere in visione dell'avversario
   5. king non deve essere sotto scacco
   6. sia rook che king devono essere liberi di muoversi ( non credo sia un problema  controllare perché é impossibile fare un pin del rook )