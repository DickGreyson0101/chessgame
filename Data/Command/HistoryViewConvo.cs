using System.Collections.Generic;

namespace Data.Command
{
    public class HistoryViewConvo : ICompensableConvo
    {
        private Stack<ICompensableCommand> _redoCommands = new Stack<ICompensableCommand>();
        private Stack<ICompensableCommand> _undoCommands = new Stack<ICompensableCommand>();

        public void Execute(ICompensableCommand command)
        {
            command.Execute();
            _undoCommands.Push(command);
            _redoCommands.Push(command);
        }

        public ICompensableCommand Undo()
        {
            if (_undoCommands.Count == 0) return null;

            ICompensableCommand command = _undoCommands.Pop();
            command.Compensate();
            _redoCommands.Push(command);

            return command;
        }

        public ICompensableCommand Redo()
        {
            if (_redoCommands.Count == 0) return null;

            ICompensableCommand command = _redoCommands.Pop();
            command.Execute();
            _redoCommands.Push(command);

            return command;
        }
    }
}