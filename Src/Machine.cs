namespace StateMachine
{
    /// <summary>
    /// The state machine class
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// Estados da Maquina
        /// </summary>
        List<State> _states = new List<State>();

        /// <summary>
        /// Dados da maquina
        /// </summary>
        string _data = string.Empty;

        /// <summary>
        /// Posição de leitura
        /// </summary>
        private int _curr = 0;

        /// <summary>
        /// Ultimo estado da maquina
        /// </summary>
        private string _lastState = string.Empty;

        /// <summary>
        /// Le arquivo da maquina de estados
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="Exception"></exception>
        public Machine(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                // Linha vazia
                if (string.IsNullOrEmpty(line)) 
                    continue;

                // Comentario
                if (line.Trim().StartsWith("#")) 
                    continue;

                int equalPos = line.IndexOf('=');

                string stateName = line.Substring(0, equalPos);

                if (string.IsNullOrEmpty(stateName))
                    throw new Exception("State name is empty");

                string[] transitions = line
                    .Substring(equalPos + 1)
                    .Split('|');

                if (transitions.Length == 0)
                    throw new Exception($"No transitions on state {stateName}");

                State newState = new State()
                {
                    Name = stateName,
                };

                foreach (var transitionStr in transitions)
                {
                    if (!transitionStr.Contains("->"))
                        throw new Exception("Bad transition format");

                    var data = transitionStr.Split("->");

                    if (data.Length != 2)
                        throw new Exception("Bad transition format");

                    var transitionData = data[0];
                    var transitionState = data[1];

                    if (string.IsNullOrWhiteSpace(transitionState) || string.IsNullOrWhiteSpace(transitionState))
                        throw new Exception("Bad transition format");

                    if (transitionState.Contains(" "))
                        throw new Exception("Bad transition format");

                    if (transitionData.Length > 1)
                        throw new Exception("Bad transition format");

                    Transition transition = new Transition();

                    transition.Destiny = transitionState;
                    transition.Condition = transitionData[0];

                    newState.Transitions.Add(transition);
                }

                _states.Add(newState);
            }

            var states = _states.Select(x => x.Name).ToList();

            if (states.GroupBy(x => x).Count() != states.Count())
                throw new Exception("States repetated");

            if (_states.Any(x => x.Transitions.Any(y => !states.Contains(y.Destiny))))
                throw new Exception("Transation with state that don't exists");
        }

        /// <summary>
        /// Seta dados da maquina de estado
        /// </summary>
        /// <param name="data"></param>
        public void SetData(string data)
        {
            _data = data ?? string.Empty;
        }

        /// <summary>
        /// Executa maquina de estados, retornando o ultimo estado e se leu todos os dados
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public (string, bool) Execute(string initialState)
        {
            _curr = 0;
            _lastState = string.Empty;

            State? state = _states.Find(x => x.Name == initialState);

            if (state == null)
                throw new Exception("Initial state not found");

            // Se data é vazia já ve se primeiro estado aceita vazio
            if (_data.Length == 0)
            {
                var transaction = state.Transitions
                    .Where(x => x.Condition == '*')
                    .FirstOrDefault();

                if (transaction != null)
                    _lastState = transaction.Destiny;
            }
            else 
             Iterate(_data[_curr], state);
            
            return (_lastState, _curr == _data.Count());
        }

        /// <summary>
        /// Calcula resultado da maquina de estados
        /// </summary>
        /// <param name="current"></param>
        /// <param name="state"></param>
        /// <exception cref="Exception"></exception>
        private void Iterate(char current, State? state)
        {
            if (state == null)
                throw new Exception("State is null");

            // Salva ultimo estado que entrou
            _lastState = state.Name;

            // Se o proximo dado da fita é o ultimo
            bool endOfData = _curr + 1 >= _data.Count();

            foreach (var transition in state.Transitions)
            {
                // Verifica se aceita vazio e esta no fim da fita, se sim é o ultimo estado e retornamos
                if (transition.Condition == '*' && _curr >= _data.Count())
                {
                    _lastState = transition.Destiny;
                    break;
                }
                // Verifica se condição bate para entrar em novo estado
                else if (transition.Condition == current)
                {
                    // Salva no novo ultimo estado
                    _lastState = transition.Destiny;
                    _curr++;

                    // Se tem mais o que ler, verifica proximo estado
                    if (!endOfData)
                        Iterate(_data[_curr], _states.Find(x => x.Name == transition.Destiny));
                    else
                    {
                        // Se não tem mais o que ler, verifica se o ultimo estado aceita vazio, sendo este o ultimo estado
                        var lastState = _states.Find(x => x.Name == transition.Destiny);

                        var transaction = lastState?.Transitions
                            .Where(x => x.Condition == '*')
                            .FirstOrDefault();

                        if (transaction != null)
                            _lastState = transaction.Destiny;
                    }

                    break;
                }
            }
        }
    }
}
