using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MvvmGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

// https://github.com/dotnet/aspnetcore/issues/38238
// https://github.com/dotnet/roslyn/issues/50451

// https://github.com/CommunityToolkit/MVVM-Samples/blob/master/docs/mvvm/PuttingThingsTogether.md

namespace WinUITest.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        public bool CanSave => (string.IsNullOrEmpty(Name) == false);

        private readonly Customer _customer;
        public string Name
        {
            get => _customer.Name;
            set
            {
                if (_customer.Name != value)
                {
                    _customer.Name = value;
                    //RaisePropertyChanged();
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public string CustomerCode
        {
            get => _customer.CustomerCode;
            set
            {
                if (_customer.CustomerCode != value)
                {
                    _customer.CustomerCode = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int CustomerId
        {
            get => _customer.CustomerId;
            //set
            //{
            //    if (_customer.CustomerCode != value)
            //    {
            //        _customer.CustomerCode = value;
            //        RaisePropertyChanged();
            //    }
            //}
        }

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }

        public void Save()
        {
            App.DataProvider.Customers.Save(_customer);
        }

        public override string ToString()
        {
            return $"{CustomerCode}\t{Name}\t{CanSave}";
        }

    }
}
