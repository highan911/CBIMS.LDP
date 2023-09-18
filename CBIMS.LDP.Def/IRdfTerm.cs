// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace CBIMS.LDP.Def
{
    public enum LiteralBasic
    {
        NONE,
        STRING,
        INTEGER,
        DOUBLE,
        BOOLEAN,
        OTHER
    }

    public interface IRdfTerm
    {
        INode ToNode(IGraph graph);
    }

    public interface IRdfNode : IRdfTerm
    {
        RdfNSDef NS { get; }
        string Name { get; }
        string QName { get; }
        string FullPath { get; }
        IUriNode Node { get; }
    }

    public abstract class RdfNodePersist : IRdfNode
    {
        public RdfNSDef NS { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string QName => NS?.PrefixNC + ":" + Name;
        public virtual string FullPath => NS?.FullPath + Name;
        public virtual IUriNode Node { get; protected set; }

        public virtual INode ToNode(IGraph graph) 
        {
            if (Node != null)
            {
                return Node.ToNode(graph);
            }
            else 
            {
                var uri = new Uri(NS.FullPath + Name);
                if (graph != null)
                {
                    return graph.CreateUriNode(uri);
                }
                else if(Graph != null)
                {
                    return Graph.CreateUriNode(uri);
                }
                else
                {
                    //throw new InvalidOperationException();
                    return null;
                }
            }
        }
            

        public IGraph Graph => NS.Graph;


        protected RdfNodePersist(RdfNSDef ns, string name, IUriNode node = null)
        {
            NS = ns;
            Name = name;
            Node = node;

            if (NS == null)
            {
                throw new InvalidOperationException("Null NameSpace");
            }

            if (Graph != null)
            {
                if (Node == null || Node.Graph != Graph)
                {
                    Node = ToNode(Graph) as IUriNode;
                }
            }
            else
            {
                //pass
            }

        }

    }



}
