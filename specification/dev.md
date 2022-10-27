// 15. stackable / placeable show information -> info section
// 17. azioni perpetue
// 23. out of turn actions ( require special trigger event )
52. access to dictionary like lua ( dot notation and brackets notation)
54. placeable place on boardcells syntax ( and define boardcells with already placeables on it )
//62. single statement bodies ( no brackets )  
64. define trigger events attributes 
68. DRY for filters ( way to defined filters outside of their contexts )
69. better define stackables
    1. draw stackable until condition
        1.  change stack interactions to return a specific type 
        2.  draw from <stack> where { block with stackable as input if returns true it draws the card, optional default is always true }
70. define the mechanism for reversing the game state on failures:
    1.  change tree concept ( changes of the state in the action tree following the triggers )
    2.  copy of the state each input and action's state that is influenced by the changes but they are applied only after the whole action is applied z
    3.  make a way to preseve changes even on failures 
    4.  effect failure destroys the branch of the change tree
    5.  an action failure destroys the whole change tree 
71. trigger event for player choice -> explicit trigger for actions that a player can choose to do
72. define name for what can contain an attribute
73. define name for what can contain actions
74. definire cosa deve essere fatto vedere al giocatore ( esempio quali azioni ect... )

// scacchi
1. pareggio -> se un giocatore non puó fare una mossa o chiede pareggio ( se cosi testati == cosi che rimangono non ci osno mosse e pareggia)
2. scacco matto
3. arresa -> richiesta da giocatore
4. castleing
   1. rook e king non si devono essere mossi
   2. gli spazi tra 
   3. rook e king devono essere liberi
   4. gli spazi tra rook e king non devono essere in visione dell'avversario
   5. king non deve essere sotto scacco
   6. sia rook che king devono essere liberi di muoversi ( non credo sia un problema  controllare perché é impossibile fare un pin del rook )