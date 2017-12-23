// <copyright file="PopupPlacementHelper.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Windows;
    using EnsureThat;

    /// <summary>
    /// The <see cref="PopupPlacementHelper" /> class.
    /// </summary>
    internal class PopupPlacementHelper
    {
        private readonly IDisplaySetting _displaySetting;
        private readonly Size _popupSize;

        private DateTime?[,] _grid;
        private Point _offset;

        public PopupPlacementHelper(IDisplaySetting displaySetting, Size popupSize)
        {
            Ensure.That(displaySetting).IsNotNull();

            _displaySetting = displaySetting;
            _popupSize = popupSize;

            _displaySetting.DisplaySettingsChanged += (sender, args) =>
            {
                Reinitialize();
            };

            Initialize();
        }

        public Point? Hold()
        {
            var cellIndex = GetNextFreeCellIndex();

            if (cellIndex == null)
            {
                return null;
            }

            _grid[cellIndex.Item1, cellIndex.Item2] = DateTime.UtcNow;

            var screenPoint = TranslateCellIndexToScreenPoint(_popupSize, _offset, cellIndex.Item1, cellIndex.Item2);

            return screenPoint;
        }

        public void Release(Point screenPoint)
        {
            var cellIndex = TranslateScreenPointToCellIndex(_grid, _popupSize, _offset, screenPoint);

            _grid[cellIndex.Item1, cellIndex.Item2] = null;
        }

        private static DateTime?[,] GetGrid(Rect workArea, Size popupSize)
        {
            var maxColumns = (int)Math.Floor(workArea.Width / popupSize.Width);
            var maxRows = (int)Math.Floor(workArea.Height / popupSize.Height);

            return new DateTime?[maxColumns, maxRows];
        }

        private static Point GetOffset(Rect workArea, Size popupSize)
        {
            var remainingScreenWidth = workArea.Width % popupSize.Width;
            var remainingScreenHeight = workArea.Height % popupSize.Height;

            return new Point(remainingScreenWidth, remainingScreenHeight);
        }

        private static Point TranslateCellIndexToScreenPoint(Size popupSize, Point offset, int columnIndex, int rowIndex)
        {
            var x = (columnIndex * popupSize.Width) + offset.X;
            var y = (rowIndex * popupSize.Height) + offset.Y;

            return new Point(x, y);
        }

        private static Tuple<int, int> TranslateScreenPointToCellIndex(DateTime?[,] grid, Size popupSize, Point offset, Point screenPoint)
        {
            var columnCount = grid.GetLength(0);
            var columnIndex = (int)Math.Floor((screenPoint.X - offset.X) / popupSize.Width);

            if (columnIndex < 0 || columnIndex >= columnCount)
            {
                throw new IndexOutOfRangeException();
            }

            var rowCount = grid.GetLength(1);
            var rowIndex = (int)Math.Floor((screenPoint.Y - offset.Y) / popupSize.Height);

            if (rowIndex < 0 || rowIndex >= rowCount)
            {
                throw new IndexOutOfRangeException();
            }

            return new Tuple<int, int>(columnIndex, rowIndex);
        }

        private Tuple<int, int> GetNextFreeCellIndex()
        {
            var columnCount = _grid.GetLength(0);
            var rowCount = _grid.GetLength(1);

            for (var columnIndex = columnCount - 1; columnIndex >= 0; columnIndex--)
            {
                for (var rowIndex = rowCount - 1; rowIndex >= 0; rowIndex--)
                {
                    if (_grid[columnIndex, rowIndex].HasValue)
                    {
                        continue;
                    }

                    return new Tuple<int, int>(columnIndex, rowIndex);
                }
            }

            return null;
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
                // TODO: Resize if possible.
                return;
            }

            // Reinitialize if the grid is empty.
            Initialize();
        }

        private void Initialize()
        {
            var workArea = _displaySetting.WorkArea;

            // Create a grid. The number of cells in the grid represents the max number of popups that can be displayed on screen at the same time.
            _grid = GetGrid(workArea, _popupSize);

            // Popups are to be displayed from the bottom right corner. The max number of popups will most likely not fill the entire screen. Therefor the grid must be offset.
            _offset = GetOffset(workArea, _popupSize);
        }
    }
}