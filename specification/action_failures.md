# Action Failures
an action failure happens when something unexcepted happened or when the failure methods are called ( on the whole tree or on a branch )

the effect tree is created and store in the action construct and is updated when possible changes happen, this allows a fast execution of the related effects

the effect tree should save the construct that changes so to restore their
internal state in case of a failure

before executing the effect makes a copy of the states that changes

**WARNING:** if a single parent effect is failed, the shared changed attributes with the childs are preserved.

it is not the case if the whole branch of the effect is reversed, in that case starting from the leaf nodes of the involved effect branch all the internal states 
are restored to their state before the effect execution by doing an inverse breadth first search starting from the leaf nodes. 
this ensures consistency in the game state model on a branch wide effect reversal.

for a full on action reversal it is the same as a whole branch reversal but with branch starting point being the root node of the effect tree involved 

proposta per le funzioni di fail:
- fail, fail effect ( manca un fail effetto senza propagazione )


