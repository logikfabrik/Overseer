// <copyright file="DamerauLevenshtein.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Text
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="DamerauLevenshtein" /> class.
    /// </summary>
    public static class DamerauLevenshtein
    {
        public static int GetDistance(string from, string to)
        {
            Ensure.That(from).IsNotNull();
            Ensure.That(to).IsNotNull();

            var bounds = new
            {
                Height = from.Length + 1,
                Width = to.Length + 1
            };

            var matrix = new int[bounds.Height, bounds.Width];

            for (var height = 0; height < bounds.Height; height++)
            {
                matrix[height, 0] = height;
            }

            for (var width = 0; width < bounds.Width; width++)
            {
                matrix[0, width] = width;
            }

            for (var height = 1; height < bounds.Height; height++)
            {
                for (var width = 1; width < bounds.Width; width++)
                {
                    var cost = from[height - 1] == to[width - 1] ? 0 : 1;
                    var insertion = matrix[height, width - 1] + 1;
                    var deletion = matrix[height - 1, width] + 1;
                    var substitution = matrix[height - 1, width - 1] + cost;

                    var distance = Math.Min(insertion, Math.Min(deletion, substitution));

                    if (height > 1 && width > 1 && from[height - 1] == to[width - 2] && from[height - 2] == to[width - 1])
                    {
                        distance = Math.Min(distance, matrix[height - 2, width - 2] + cost);
                    }

                    matrix[height, width] = distance;
                }
            }

            return matrix[bounds.Height - 1, bounds.Width - 1];
        }
    }
}
