// 21. trigger modifiers
// 10. trigger events -> to define identifiers and what can you use in the filter 
//11. definizione dei placeable
//    1.  number of placeables on boardcells
12. turn phases and game phases
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
// 22. movimenti che vanno fuori dai gruppi -> no policy, it will be needed to define the adjacency between them manually
// 24. modificare stati players -> trough dictionary / lua rappr
// 25. modificare action of states -> trough dictionary / lua rapp 
// 26. filter inputs 
27. input all instances of type
29. stackable terms max and min 
30. optional block or tag
31. break, continue ect... for while loops
32. draw until condition
33. winner, loser, draw goal syntax
34. trigger interaction ( setting / using ) with attributesa
35. starting player selection => default starting player 0
36. group attributes / separate attributes and group definition
37. get from boarcell to group or board ( group naming problem and type problem if used with generic cell.group tag, maybe do cast or return none if wrong )
38. if two goals with different priority trigger
    1.  define a stop propagation syntax
    2.  do with global attributes and priority sequence execution ( check tris.txt)
39. rivedere named and unnamed triggers ( permit the existance of only named triggers without a global trigger ( for only partial execution ))