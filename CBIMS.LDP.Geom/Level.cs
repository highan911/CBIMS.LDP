// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.Geom
{
    public class Level
    {
        public string Name;

        public int Elevation;

        public double? Number;

        public string UpperLevel;

        public int? UpperElevation;

        public Level(string name, int elevation, double? number, string upperLevel, int? upperElevation)
        {
            Name = name;
            Elevation = elevation;
            Number = number;
            UpperLevel = upperLevel;
            UpperElevation = upperElevation;
        }
    }
}
