// <copyright file="PopupPlacementHelper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// The <see cref="PopupPlacementHelper" /> class.
    /// </summary>
    internal class PopupPlacementHelper
    {
        private readonly DateTime?[,] _grid;
        private readonly Size _popupSize;
        private readonly Point _offset;

        public PopupPlacementHelper(Rect workArea, Size popupSize)
        {
            _grid = new DateTime?[(int)Math.Floor(workArea.Width / popupSize.Width), (int)Math.Floor(workArea.Height / popupSize.Height)];
            _popupSize = popupSize;
            _offset = new Point(workArea.Width % popupSize.Width, workArea.Height % popupSize.Height);
        }

        public Point Hold()
        {
            Func<int, int, Point> getPoint = (x, y) => new Point((x * _popupSize.Width) + _offset.X, (y * _popupSize.Height) + _offset.Y);

            var cells = new List<KeyValuePair<DateTime, Tuple<int, int>>>();

            for (var columnIndex = _grid.GetLength(0) - 1; columnIndex >= 0; columnIndex--)
            {
                for (var rowIndex = _grid.GetLength(1) - 1; rowIndex >= 0; rowIndex--)
                {
                    if (_grid[columnIndex, rowIndex].HasValue)
                    {
                        cells.Add(new KeyValuePair<DateTime, Tuple<int, int>>(_grid[columnIndex, rowIndex].Value, new Tuple<int, int>(columnIndex, rowIndex)));

                        continue;
                    }

                    _grid[columnIndex, rowIndex] = DateTime.UtcNow;

                    return getPoint(columnIndex, rowIndex);
                }
            }

            var cell = cells.OrderBy(c => c.Key).FirstOrDefault().Value ?? new Tuple<int, int>(_grid.GetLength(0) - 1, _grid.GetLength(1) - 1);

            _grid[cell.Item1, cell.Item2] = DateTime.UtcNow;

            return getPoint(cell.Item1, cell.Item2);
        }

        public void Release(Point point)
        {
            _grid[(int)((point.X - _offset.X) / _popupSize.Width), (int)((point.Y - _offset.Y) / _popupSize.Height)] = null;
        }
    }
}
