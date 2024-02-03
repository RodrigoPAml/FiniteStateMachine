namespace StateMachine
{
    /// <summary>
    /// Transição de estado da maquina
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// Estado destino
        /// </summary>
        public string Destiny { get; set; }

        /// <summary>
        /// Condição para passar para o novo estado
        /// </summary>
        public char Condition { get; set; }
    }
}
