using System;
using PropertyChanged;

namespace FodyDotNet3
{
    [AddINotifyPropertyChangedInterface]
    public class Class1
    {
        public string Prop { get; set; }
    }
}
