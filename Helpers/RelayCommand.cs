using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action execute = null;
        private readonly Func<bool> canExecute = null;
        private readonly Action<object> executeObj = null;
        private readonly Func<object, bool> canExecuteObj = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        /// <summary>
        /// 생성자. <see cref="ICommand.Execute(object)"/>, <see cref="ICommand.CanExecute(object)"/> 모두 구현.
        /// </summary>
        /// <param name="execute"><see cref="ICommand.Execute(object)"/> 구현 메서드</param>
        /// <param name="canExecute"><see cref="ICommand.CanExecute(object)"/> 구현 메서드</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null || canExecute == null)
                throw new ArgumentNullException("RelayCommand 생성자 파라미터 오류");
            this.executeObj = execute;
            this.canExecuteObj = canExecute;
        }

        /// <summary>
        /// 생성자. <see cref="ICommand.Execute(object)"/>(파라미터 미사용), <see cref="ICommand.CanExecute(object)"/>(파라미터 사용) 구현.
        /// </summary>
        /// <param name="execute"><see cref="ICommand.Execute(object)"/> 구현 메서드</param>
        /// <param name="canExecute"><see cref="ICommand.CanExecute(object)"/> 구현 메서드</param>
        public RelayCommand(Action execute, Func<object, bool> canExecute)
        {
            if (execute == null || canExecute == null)
                throw new ArgumentNullException("RelayCommand 생성자 파라미터 오류");
            this.execute = execute;
            this.canExecuteObj = canExecute;
        }

        /// <summary>
        /// 생성자. <see cref="ICommand.Execute(object)"/>(파라미터 사용), <see cref="ICommand.CanExecute(object)"/>(파라미터 미사용) 구현.
        /// </summary>
        /// <param name="execute"><see cref="ICommand.Execute(object)"/> 구현 메서드</param>
        /// <param name="canExecute"><see cref="ICommand.CanExecute(object)"/> 구현 메서드</param>
        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null || canExecute == null)
                throw new ArgumentNullException("RelayCommand 생성자 파라미터 오류");
            this.executeObj = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// 생성자. <see cref="ICommand.Execute(object)"/>, <see cref="ICommand.CanExecute(object)"/>를 파라미터 없이 구현.
        /// </summary>
        /// <param name="execute"><see cref="ICommand.Execute(object)"/> 구현 메서드</param>
        /// <param name="canExecute"><see cref="ICommand.CanExecute(object)"/> 구현 메서드</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null || canExecute == null)
                throw new ArgumentNullException("RelayCommand 생성자 파라미터 오류");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (canExecute?.Invoke() ?? false) || (canExecuteObj?.Invoke(parameter) ?? false);
        }

        public void Execute(object parameter)
        {
            if (executeObj != null)
                executeObj(parameter);
            else
                execute?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        [DebuggerStepThrough()]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
