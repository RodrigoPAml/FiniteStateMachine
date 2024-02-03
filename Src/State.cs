namespace StateMachine
{
    /// <summary>
    /// Representa um estado da maquina
    /// </summary>
    public class State
    {
        /// <summary>
        /// Nome do estado da maquina
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Transições para outros estados desta maquina
        /// </summary>
        public List<Transition> Transitions { get; set; } = new List<Transition>();
    }
}
