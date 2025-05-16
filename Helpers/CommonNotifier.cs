using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Helpers
{
    public class CommonNotifier : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        /// <summary>
        /// �Ӽ� �� ���� �� ���� Client�� ����
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// �Ӽ� �� ���� �Ϸ� ���� Client�� ����
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
