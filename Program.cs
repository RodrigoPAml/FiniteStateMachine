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
        RunExample($"MachineFiles/aabb.txt", "AABBAABB", "INITIAL");
        RunExample($"MachineFiles/aabb.txt", "AABBAABBA", "INITIAL");

        RunExample($"MachineFiles/abc.txt", "ABCABC", "STATE_A");
        RunExample($"MachineFiles/abc.txt", "ABCABCABAC", "STATE_A");
    }
}