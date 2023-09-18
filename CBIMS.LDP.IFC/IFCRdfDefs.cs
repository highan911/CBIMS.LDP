// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using CBIMS.LDP.Def;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.IFC
{
    public static class IFCRdfDefs
    {
        public static readonly RdfNSDef IFCOWL_IFC4 = new RdfNSDef(null, "https://standards.buildingsmart.org/IFC/DEV/IFC4/ADD2_TC1/OWL#", "ifcowl", false);

        public static readonly RdfNSDef IFCRDF_IFC4 = new RdfNSDef(null, "http://www.cbims.org.cn/ns/ifcrdf/ifc4#", "ifc4", true);
    }
}
