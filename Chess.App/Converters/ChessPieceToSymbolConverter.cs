using Chess.App.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chess.App.Converters
{
    public class ChessPieceToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as FigureModel;
            if (val != null)
            {
                char res = ' ';
                switch (val.Origin.Type)
                {
                    case Core.FigureType.King:
                        res = '\u2654';
                        break;
                    case Core.FigureType.Queen:
                        res = '\u2655';
                        break;
                    case Core.FigureType.Rook:
                        res = '\u2656';
                        break;
                    case Core.FigureType.Bishop:
                        res = '\u2657';
                        break;
                    case Core.FigureType.Knight:
                        res = '\u2658';
                        break;
                    case Core.FigureType.Pawn:
                        res = '\u2659';
                        break;
                }
                if (val.Color == Core.FigureColor.Black)
                {
                    res += (char)6;
                }
                return res.ToString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
