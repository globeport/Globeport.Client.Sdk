using System.Windows.Input;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Globeport.Shared.Library.Components
{
    public class Command : Observable, ICommand, IDisposable
    {
        public event EventHandler CanExecuteChanged;
        public event CancelCommandEventHandler Executing;
        public event CommandEventHandler Executed;

        public Action Action { get; set; }

        public Command()
        {
            CanExecute = true;
        }

        public Command(Action action, bool canExecute = true)
        {
            Action = action;
            CanExecute = canExecute;
        }

        public async virtual void DoExecute(object param)
        {
            var args = new CancelCommandEventArgs() { Parameter = param, Cancel = false };
            OnExecuting(args);
            if (args.Cancel) return;
            await InvokeAction(param).ConfigureAwait(false);
            OnExecuted(new CommandEventArgs() { Parameter = param });
        }

        protected async Task InvokeAction(object param)
        {
            if (Action != null)
            {
                await Task.Run(() => Action()).ConfigureAwait(false);
            }
        }

        protected virtual void OnExecuted(CommandEventArgs args)
        {
            IsExecuting = false;
            Executed?.Invoke(this, args);
        }

        protected virtual void OnExecuting(CancelCommandEventArgs args)
        {
            IsExecuting = true;
            Executing?.Invoke(this, args);
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }

        void ICommand.Execute(object parameter)
        {
            DoExecute(parameter);
        }

        public void Dispose()
        {
            Action = null;
        }

        bool isExecuting;
        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }
            set
            {
                if (isExecuting != value)
                {
                    isExecuting = value;
                    OnPropertyChanged(nameof(IsExecuting));
                }
            }
        }

        bool canExecute;
        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    OnCanExecuteChanged();
                    OnPropertyChanged(nameof(CanExecute));
                }
            }
        }
    }

    public delegate void CommandEventHandler(object sender, CommandEventArgs args);
    public delegate void CancelCommandEventHandler(object sender, CancelCommandEventArgs args);

    public class CommandEventArgs : EventArgs
    {
        public object Parameter { get; set; }
    }

    public class CancelCommandEventArgs : CommandEventArgs
    {
        public bool Cancel { get; set; }
    }
}