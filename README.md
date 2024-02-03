# Finite State Machine in C#


A Finite State Machine (FSM) is a mathematical model used to represent and control systems with a finite number of states.
It is a concept widely used in computer science, engineering, and various other fields to design and analyze systems that exhibit discrete behavior. 
Finite State Machines are particularly useful for modeling systems with distinct, well-defined states and transitions between these states.

Here are the key components of a Finite State Machine:

## States:

States represent the different conditions or situations that a system can be in. These are typically distinct and well-defined stages or modes of operation.
Transitions:

## Transitions 
Describe the movement between states. Events or inputs trigger transitions, causing the system to move from one state to another. Transitions are associated with conditions or actions that determine when they can occur.

## Inputs

Inputs are external stimulus or events that trigger state transitions. They can be signals, user actions, sensor readings, or any other form of external influence.

## Outputs

Outputs are the results or actions produced by the system in response to inputs or state changes. Outputs are associated with specific states or transitions.
Finite State Machines can be classified into two main types:

## Deterministic Finite State Machine (DFSM)

In a deterministic FSM, the transition from one state to another is uniquely determined by the current state and the input. Given a specific state and input, there is only one possible next state.

## Non-deterministic Finite State Machine (NDFSM or just NFA)

In a non-deterministic FSM, there can be multiple possible next states for a given state and input. The system may have choices or uncertainties in its transitions.
Finite State Machines find applications in various areas, including:

* Software Engineering: Finite State Machines are used in the design of software systems, especially for modeling the behavior of sequential algorithms, parsers, and protocol implementations.
* Digital Logic Design: FSMs are employed in the design of digital circuits and systems, where states represent different configurations or modes of operation.
* Control Systems: Finite State Machines are used to model and control the behavior of systems with discrete modes, such as automated manufacturing processes.
* Natural Language Processing: Finite State Machines can be applied to model the syntax and semantics of languages, aiding in the development of language processing tools.

# Using the code

We need a input to represent the FSM behavior, in this case a .txt with the content (search in MachineFiles folder)

```
# Recognizes the string ABC multiples times or empty (ABC)+

STATE_A=A->STATE_B|*->ACCEPT
STATE_B=B->STATE_C
STATE_C=C->STATE_A
ACCEPT=*->ACCEPT 
```

And run in the program

```C#
using StateMachine;

var machine = new Machine(path); // open txt file
machine.SetData("ABCABC"); // data to be readed (input)

var (finalState, finished) = machine.Execute("STATE_A"); // Run with initial state A

Console.WriteLine($"The final state is {finalState}");
Console.WriteLine($"Finished reading: {finished}");
```

In the output we have

```
The final state is ACCEPT
Finished reading: True
```