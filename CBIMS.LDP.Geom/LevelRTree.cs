// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBIMS.LDP.Geom
{
    public class LevelRTree
    {
        public const string DEFAULT_LEVEL_NAME = "__DEFAULT__";
        public static readonly Level DEFAULT_LEVEL = new Level("__DEFAULT__", -1000000, null, null, 1000000);

        private Dictionary<string, Level> Levels = new Dictionary<string, Level>();

        private Dictionary<string, RTree<BBox>> LevelRTreeMap = new Dictionary<string, RTree<BBox>>();

        private Dictionary<string, HashSet<string>> LevelElementsMap = new Dictionary<string, HashSet<string>>();
        private Dictionary<string, string> LevelElementsMapInv = new Dictionary<string, string>();

        private Dictionary<string, BBox> IdBBoxMap = new Dictionary<string, BBox>();
        private Dictionary<string, PointInt> LocationMap = new Dictionary<string, PointInt>();
        private Dictionary<string, PointIntList> LocationCurveMap = new Dictionary<string, PointIntList>();

        public LevelRTree()
        {
            _addOneLevel(DEFAULT_LEVEL);
        }

        public void InitLevels(List<Level> inputLevels)
        {
            inputLevels.Sort((x, y) => x.Elevation.CompareTo(y.Elevation));
            Level last = null;
            foreach (Level current in inputLevels)
            {
                _addOneLevel(current);
                if(last != null)
                {
                    if (last.UpperLevel == null)
                    {
                        last.UpperLevel = current.Name;
                        if (last.UpperElevation == null)
                        {
                            last.UpperElevation = current.Elevation;
                        }
                    }
                    else
                    {
                        var last_upper = inputLevels.FirstOrDefault(t=>t.Name == last.UpperLevel);
                        if(last_upper == null)
                        {
                            last_upper = current;
                        }
                        if (last.UpperElevation == null)
                        {
                            last.UpperElevation = last_upper.Elevation;
                        }
                    }
                }
                last = current;
            }

        }
        private void _addOneLevel(Level level)
        {
            Levels[level.Name] = level;
            LevelRTreeMap[level.Name] = new RTree<BBox>();
            LevelElementsMap[level.Name] = new HashSet<string>();
        }
        public bool AddEntity(BBox bbox, string elemid, string levelName, PointInt location, PointIntList locationCurve, bool autoFindLevel)
        {
            if (IdBBoxMap.ContainsKey(elemid))
            {
                return false;
            }
            if (location == null)
            {
                location = bbox.Center;
            }
            if (levelName == null)
            {
                levelName = ((!autoFindLevel) ? "__DEFAULT__" : _findLevelName(location));
            }
            if (!Levels.ContainsKey(levelName))
            {
                throw new InvalidOperationException("invalid level name");
            }
            IdBBoxMap.Add(elemid, bbox);
            LevelRTreeMap[levelName].Add(bbox.GetRTreeRectangle(), bbox);
            LevelElementsMap[levelName].Add(elemid);
            LevelElementsMapInv[elemid] = levelName;
            LocationMap.Add(elemid, location);
            if (locationCurve != null)
            {
                LocationCurveMap.Add(elemid, locationCurve);
            }
            return true;
        }

        public string GetContainedInLevelById(string id)
        {
            if (LevelElementsMapInv.ContainsKey(id))
            {
                return LevelElementsMapInv[id];
            }
            return null;
        }

        private string _findLevelName(PointInt location)
        {
            foreach (string key in Levels.Keys)
            {
                if (!(key == "__DEFAULT__"))
                {
                    Level level = Levels[key];
                    if (level.Elevation <= location.Z && (!level.UpperElevation.HasValue || level.UpperElevation.Value >= location.Z))
                    {
                        return key;
                    }
                }
            }
            return "__DEFAULT__";
        }

    }
}
