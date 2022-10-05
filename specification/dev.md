13. game phase
// 15. stackable / placeable show information -> info section
// 17. azioni perpetue
// 23. out of turn actions ( require special trigger event )
// 24. definition for change of setup / mechanics correlated to player number -> can be defined trough new copying and changing the relevant information ( una rottura )
// 24. modificare stati players -> trough dictionary / lua rappr
// 25. modificare action of states -> trough dictionary / lua rapp 
30. optional block or tag
32. draw stackable until condition
    1.  change stack interactions to return a specific type 
    2.  draw from <stack> where { block with stackable as input if returns true it draws the card, optional default is always true }
42. provare altri giochi 
43. input di altri giocatori oltre all'attivo -> audio telefono
44. setup iniziale 
45. definition of verbs
    1.  multiple inputs
    2.  one effects
    3.  return type specifief
46. movement overhaul
    1. obstruction
    2. allow movement till condition 
    3. ignore obstructions
47. transaction like effects
48. asking for input inside effect function 
49. change how indexing works for groups ( blank tiles count has a tile but will return none if indexed )
50. refernce to attached players in state  
51. clearer access to players without input 
52. access to dictionary like lua ( dot notation and brackets notation)
53. automatic input keyword ( like all it skips asking for input from the player )
    1.  restriction for non list, input filter must match with only one value
    2.  automatic filter for list 
54. placeable place on boardcells syntax ( and define boardcells with already placeables on it )
55. assign state to syntax and access state from player
56. maybe new keyword to create new instances
    1.  restrict it to local, group and interactables
    2.  maybe initializer list for attributes ( construcotr like syntax specifying the name ) -> new x(attribute_name = value)
57. change boardcell to tile
58. optional return type for verb ( infer from return expressions, if types are not the same throw compiler error )
59. list qol 
    1.  clear
    2.  append
60. verb do not have dependency injection all must be passed to them
61. allow inheritance ? 
    1. complex implementing it
    2. semplifies code and introduces to  an important concept
    3. restricting only to inherit attributes and actions with no overrides 
    4. override only of attributes
    5. up casting and down casting
62. single statement bodies ( no brackets )  
63. this keyword 
    1.  for state
64. define trigger events attributes 
65. triggers modifiers:
    1.  before
66. else if
67. optional input ( input that can be none if not given )
    1.  will be none if it is not given ( like in a verb )
    2.  will be none if it has no match ( automatic match )
    3.  will be noen if it has no possible player selection ( standard input )