using CommunityToolkit.Mvvm.ComponentModel;

namespace WinUITest.ViewModels
{
    public class CustomerNavigationMenuItemViewModel : ObservableObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
    }
}
