using System.ComponentModel;
using System.Runtime.CompilerServices;

// https://github.com/dotnet/aspnetcore/issues/38238
// https://github.com/dotnet/roslyn/issues/50451

// https://github.com/CommunityToolkit/MVVM-Samples/blob/master/docs/mvvm/PuttingThingsTogether.md

namespace WinUITest.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Diagnostics.Debug.WriteLine($"ViewModelBase Property Changed:{propertyName}");
        }
    }

}
