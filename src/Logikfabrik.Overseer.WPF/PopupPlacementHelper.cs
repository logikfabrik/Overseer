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

            Initialize();
        }

        public Point Hold()
        {
            var cells = new List<KeyValuePair<DateTime, Tuple<int, int>>>();

            // Find the first free cell.
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

                    return TranslateCellIndexToScreenPoint(columnIndex, rowIndex);
                }
            }

            // No free cell was found.
            var cell = cells.OrderBy(c => c.Key).FirstOrDefault().Value ?? new Tuple<int, int>(_grid.GetLength(0) - 1, _grid.GetLength(1) - 1);

            _grid[cell.Item1, cell.Item2] = DateTime.UtcNow;

            return TranslateCellIndexToScreenPoint(cell.Item1, cell.Item2);
        }

        public void Release(Point screenPoint)
        {
            var cellIndex = TranslateScreenPointToCellIndex(screenPoint);

            _grid[cellIndex.Item1, cellIndex.Item2] = null;

            // The client might have changed screen resolution; reinitialize.
            Reinitialize();
        }

        private Point TranslateCellIndexToScreenPoint(int columnIndex, int rowIndex)
        {
            var x = (columnIndex * _popupSize.Width) + _offset.X;
            var y = (rowIndex * _popupSize.Height) + _offset.Y;

            return new Point(x, y);
        }

        private Tuple<int, int> TranslateScreenPointToCellIndex(Point screenPoint)
        {
            var columnCount = _grid.GetLength(0);
            var columnIndex = (int)Math.Floor((screenPoint.X - _offset.X) / _popupSize.Width);

            if (columnIndex < 0 || columnIndex >= columnCount)
            {
                throw new IndexOutOfRangeException();
            }

            var rowCount = _grid.GetLength(1);
            var rowIndex = (int)Math.Floor((screenPoint.Y - _offset.Y) / _popupSize.Height);

            if (rowIndex < 0 || rowIndex >= rowCount)
            {
                throw new IndexOutOfRangeException();
            }

            return new Tuple<int, int>(columnIndex, rowIndex);
        }

        private void Reinitialize()
        {
            Func<bool> gridIsEmpty = () =>
            {
                var columnCount = _grid.GetLength(0);
                var rowCount = _grid.GetLength(1);

                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        if (_grid[columnIndex, rowIndex].HasValue)
                        {
                            return false;
                        }
                    }
                }

                return true;
            };

            if (!gridIsEmpty())
            {
                return;
            }

            // Reinitialize if the grid is empty.
            Initialize();
        }

        private void Initialize()
        {
            var area = _getWorkArea();

            var maxColumns = (int)Math.Floor(area.Width / _popupSize.Width);
            var maxRows = (int)Math.Floor(area.Height / _popupSize.Height);

            // Create a grid. The number of cells in the grid represents the max number of popups that can be displayed on screen at the same time.
            _grid = new DateTime?[maxColumns, maxRows];

            var remainingScreenWidth = area.Width % _popupSize.Width;
            var remainingScreenHeight = area.Height % _popupSize.Height;

            // Popups are to be displayed from the bottom right corner. The max number of popups will most likely not fill the entire screen. Therefor the grid must be offset.
            _offset = new Point(remainingScreenWidth, remainingScreenHeight);
        }
    }
}
