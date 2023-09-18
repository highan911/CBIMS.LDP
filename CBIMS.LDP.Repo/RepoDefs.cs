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

namespace CBIMS.LDP.Repo
{
    public static class RepoDefs
    {
        //classes

        public static readonly RdfURIClassDef Container
            = new RdfURIClassDef(RdfCommonNS.LDP, "Container", null, false, typeof(LDPContainer), null,
                _this =>
                {
                    _this.AddPropDef(new RdfPropDef(RdfCommonNS.LDP, "contains"));
                    _this.AddPropDef(new RdfPropDef(RdfCommonNS.LDP, "membershipResource"));
                    _this.AddPropDef(new RdfPropDef(RdfCommonNS.LDP, "hasMemberRelation"));
                    _this.AddPropDef(new RdfPropDef(RdfCommonNS.LDP, "isMemberOfRelation"));
                    _this.AddPropDef(new RdfPropDef(RdfCommonNS.LDP, "insertedContentRelation"));
                }
            );


        public static readonly RdfURIClassDef BasicContainer = new RdfURIClassDef(RdfCommonNS.LDP, "BasicContainer", Container, false, typeof(LDPContainer), null);

        public static readonly RdfURIClassDef DirectContainer = new RdfURIClassDef(RdfCommonNS.LDP, "DirectContainer", Container, false, typeof(LDPContainer), null);

        public static readonly RdfURIClassDef IndirectContainer = new RdfURIClassDef(RdfCommonNS.LDP, "IndirectContainer", Container, false, typeof(LDPContainer), null);

        //contant nodes

        public const string QNAME_member = "ldp:member";
        public const string QNAME_MemberSubject = "ldp:MemberSubject";

    }
}
