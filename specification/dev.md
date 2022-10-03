// 21. trigger modifiers
// 10. trigger events -> to define identifiers and what can you use in the filter 
//11. definizione dei placeable
//    1.  number of placeables on boardcells
// 12. turn phases 
13. game phases
// 14. action requires
// 15. stacable / placeable show information -> info section
// 17. azioni perpetue
board checks:
    18. is boardcell connected syntax 
    19. adjacency checks 
// 21. list support  ( maybe liek lua dict for array, list, hashmaps, ect...)
// 23. out of turn actions ( require special trigger event )
// 24. definition for change of setup / mechanics correlated to player number -> can be defined trough new copying and changing the relevant information ( una rottura )
25. stack 
//    1.  movement like syntax for stack motions
//    2.  stacks that can be seen from one or more players 
//    3.  show stackables to other to subset of players
//    4.  random motions ( see 4 keep 2 random ) 
//    5.  keep in stack in user defined order 
//    6.  start from end of stack
      7.  max stack size
      8.  max stack size policy
// 24. modificare stati players -> trough dictionary / lua rappr
// 25. modificare action of states -> trough dictionary / lua rapp 
30. optional block or tag
32. draw stackable until condition
    1.  change stack interactions to return a specific type 
    2.  draw from <stack> where { block with stackable as input if returns true it draws the card, optional default is always true }
42. provare altri giochi 