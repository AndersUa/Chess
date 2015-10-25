using Chess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFoundation;

namespace Chess.App.Models
{
    public class FigureModel : BindableBase
    {
        IFigure figure;

        public int X => this.figure.Point.X;
        public int Y => this.figure.Point.Y;
        public FigureColor Color => this.figure.Color;
        public string Type => this.figure.Type.ToString();

        public IFigure Origin => this.figure;

        private bool isActive;
        public bool IsActive
        {
            get { return this.isActive; }
            set { this.SetProperty(ref this.isActive, value); }
        }

        public FigureModel(IFigure figure)
        {
            this.figure = figure;
        }

        public void Update()
        {
            base.OnPropertyChanged("X");
            base.OnPropertyChanged("Y");
        }
    }
}
