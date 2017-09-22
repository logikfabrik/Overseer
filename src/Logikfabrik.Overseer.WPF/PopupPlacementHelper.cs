// <copyright file="PopupPlacementHelper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using EnsureThat;

    /// <summary>
    /// The <see cref="PopupPlacementHelper" /> class.
    /// </summary>
    internal class PopupPlacementHelper
    {
        private readonly Func<Rect> _getWorkArea;
        private readonly Size _popupSize;

        private DateTime?[,] _grid;
        private Point _offset;

        public PopupPlacementHelper(Func<Rect> getWorkArea, Size popupSize)
        {
            Ensure.That(() => getWorkArea).IsNotNull();

            _getWorkArea = getWorkArea;
            _popupSize = popupSize;

            Setup();
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

            // The client might have changed screen resolution. If all cells are empty, reset the grid (by calling Setup()).
            for (var columnIndex = 0; columnIndex < _grid.GetLength(0); columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < _grid.GetLength(1); rowIndex++)
                {
                    if (_grid[columnIndex, rowIndex].HasValue)
                    {
                        return;
                    }
                }
            }

            Setup();
        }

        private void Setup()
        {
            var area = _getWorkArea();

            _grid = new DateTime?[(int)Math.Floor(area.Width / _popupSize.Width), (int)Math.Floor(area.Height / _popupSize.Height)];
            _offset = new Point(area.Width % _popupSize.Width, area.Height % _popupSize.Height);
        }
    }
}
