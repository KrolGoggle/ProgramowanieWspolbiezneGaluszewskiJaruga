using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    internal class ModelPoolBall :IModelPoolBall, INotifyPropertyChanged
    {

       
    public event PropertyChangedEventHandler? PropertyChanged;

        private float pos_X;
        private float pos_Y;
        private int radius;

        public override float Pos_X { get => pos_X; set { pos_X = value; NotifyPropertyChanged(); } }
        public override float Pos_Y { get => pos_Y; set { pos_Y = value; NotifyPropertyChanged(); } }
        public override int Radius { get => radius; }

        public ModelPoolBall(float X, float Y, int r)
        {
            this.pos_X = X;
            this.pos_Y = Y;
            this.radius = r;

        }
       


        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        
    }
}
