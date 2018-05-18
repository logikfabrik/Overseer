// <copyright file="NotificationGrid{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Windows;
    using EnsureThat;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="NotificationGrid{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The notification type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    internal class NotificationGrid<T> : IDisposable
        where T : class, INotification
    {
        private readonly IDisplaySetting _displaySetting;
        private readonly Size _popupSize;
        private Tuple<T, DateTime>[,] _grid;
        private Point _gridOffset;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationGrid{T}" /> class.
        /// </summary>
        /// <param name="displaySetting">The display setting.</param>
        /// <param name="popupSize">The popup size.</param>
        public NotificationGrid(IDisplaySetting displaySetting, Size popupSize)
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

        public Point? HoldCell(T notification)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(notification).IsNotNull();

            var cellIndex = GetNextFreeCellIndex();

            if (cellIndex == null)
            {
                return null;
            }

            _grid[cellIndex.Item1, cellIndex.Item2] = new Tuple<T, DateTime>(notification, DateTime.UtcNow);

            var screenPoint = GetScreenPoint(cellIndex);

            return screenPoint;
        }

        public void ReleaseCell(T notification)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(notification).IsNotNull();

            var cellIndex = GetCellIndex(notification);

            if (cellIndex == null)
            {
                return;
            }

            _grid[cellIndex.Item1, cellIndex.Item2] = null;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing && _grid != null)
            {
                var columnCount = _grid.GetLength(0);
                var rowCount = _grid.GetLength(1);

                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        _grid[columnIndex, rowIndex] = null;
                    }
                }

                _grid = null;
            }

            _isDisposed = true;
        }

        private static Tuple<T, DateTime>[,] GetGrid(Rect workArea, Size popupSize)
        {
            var maxColumns = (int)Math.Floor(workArea.Width / popupSize.Width);
            var maxRows = (int)Math.Floor(workArea.Height / popupSize.Height);

            return new Tuple<T, DateTime>[maxColumns, maxRows];
        }

        private static Point GetGridOffset(Rect workArea, Size popupSize)
        {
            var remainingScreenWidth = workArea.Width % popupSize.Width;
            var remainingScreenHeight = workArea.Height % popupSize.Height;

            return new Point(remainingScreenWidth, remainingScreenHeight);
        }

        private Point? GetScreenPoint(Tuple<int, int> cellIndex)
        {
            var columnCount = _grid.GetLength(0);
            var rowCount = _grid.GetLength(1);

            if (cellIndex.Item1 < 0 || cellIndex.Item1 >= columnCount || cellIndex.Item2 < 0 || cellIndex.Item2 >= rowCount)
            {
                return null;
            }

            var x = (cellIndex.Item1 * _popupSize.Width) + _gridOffset.X;
            var y = (cellIndex.Item2 * _popupSize.Height) + _gridOffset.Y;

            return new Point(x, y);
        }

        private Tuple<int, int> GetCellIndex(T notification)
        {
            var columnCount = _grid.GetLength(0);
            var rowCount = _grid.GetLength(1);

            for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    if (_grid[columnIndex, rowIndex]?.Item1 != notification)
                    {
                        continue;
                    }

                    return new Tuple<int, int>(columnIndex, rowIndex);
                }
            }

            return null;
        }

        private Tuple<int, int> GetNextFreeCellIndex()
        {
            var columnCount = _grid.GetLength(0);
            var rowCount = _grid.GetLength(1);

            for (var columnIndex = columnCount - 1; columnIndex >= 0; columnIndex--)
            {
                for (var rowIndex = rowCount - 1; rowIndex >= 0; rowIndex--)
                {
                    if (_grid[columnIndex, rowIndex] != null)
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
            var columnCount = _grid.GetLength(0);
            var rowCount = _grid.GetLength(1);

            for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    if (_grid[columnIndex, rowIndex] == null)
                    {
                        continue;
                    }

                    _grid[columnIndex, rowIndex].Item1?.Close();
                    _grid[columnIndex, rowIndex] = null;
                }
            }

            Initialize();
        }

        private void Initialize()
        {
            var workArea = _displaySetting.WorkArea;

            // Create a grid. The number of cells in the grid represents the max number of notifications that can be displayed on screen at the same time.
            _grid = GetGrid(workArea, _popupSize);

            // Notifications are to be displayed from the bottom right corner. The max number of notifications will most likely not fill the entire screen. Therefor the grid must be offset.
            _gridOffset = GetGridOffset(workArea, _popupSize);
        }
    }
}