using StateMachine;

public class Program
{
    private static void RunExample(string path, string data, string initialState)
    {
        Console.WriteLine("*-------------------------------------------------------*");
        Console.WriteLine($"Opening finite state machine at {path}");
        Console.WriteLine();

        var machine = new Machine(path);
        
        Console.WriteLine($"Running finite state machine with initial state at {initialState} and content {data}");
        Console.WriteLine();

        machine.SetData(data);

        var result = machine.Execute(initialState);

        Console.WriteLine($"The final state is {result.Item1}");
        Console.WriteLine($"Finished reading: {result.Item2}");
    }

    private static void Main(string[] args)
    {
        string path = "C:/Users/Rodrigo/Desktop/FiniteStateMachine/MachineFiles/";

        RunExample($"{path}aabb.txt", "AABBAABB", "INITIAL");
        RunExample($"{path}aabb.txt", "AABBAABBA", "INITIAL");

        RunExample($"{path}abc.txt", "ABCABC", "STATE_A");
        RunExample($"{path}abc.txt", "ABCABCABAC", "STATE_A");
    }
}