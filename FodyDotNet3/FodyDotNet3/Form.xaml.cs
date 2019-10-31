using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FodyDotNet3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Form : ContentPage
    {
        public Form()
        {
            InitializeComponent();
        }
    }
}