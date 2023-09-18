# CBIMS.LDP libraries


## Introduction

CBIMS.LDP libraries are used in supporting the functions of other CBIMS tools about IFC, RDFS/OWL and LDP.

## Dependencies and resources

The project uses the following dependencies from the NuGet:

* `dotNetRDF` (https://github.com/dotnetrdf)
* `Xbim` (https://github.com/xBimTeam/XbimEssentials, https://github.com/xBimTeam/XbimGeometry)
* `MathNet.Numerics` (https://github.com/mathnet/mathnet-numerics)
* `Newtonsoft.Json` (https://github.com/JamesNK/Newtonsoft.Json)
* `RTree` (https://github.com/drorgl/cspatialindexrt)

Other resources contained in the repository:

* `CBIMS.LDP.IFC/Resource/ifcowl_ifc4.ttl` - The buildingSMART ifcOWL definitions for IFC4 schema (https://standards.buildingsmart.org/IFC/DEV/IFC4/ADD2_TC1/OWL).


## Folders in the repository

* `CBIMS.LDP.Def` - Commonly used RDFS/OWL definitions, and classes for interact with the dotNetRDF toolkit.
* `CBIMS.LDP.Repo` - Classes for implementing the Linked Data Platform (LDP) containers.
* `CBIMS.LDP.IFC` - Abstract classes for manipulating the Industry Foundation Classes (IFC) data.
* `CBIMS.LDP.IFC.XbimLoader` - Loading IFC data using the Xbim toolkit.
* `CBIMS.LDP.Geom` - Library for simple geometry calculations.

## License

CBIMS.LDP libraries use `GNU Lesser General Public License (LGPL)`. 
Please refer to:
https://www.gnu.org/licenses/lgpl-3.0.en.html

## About CBIMS

**CBIMS** (Computable Building Information Modeling Standards) is a set of research projects aiming at the development of computable BIM standards for the interoperability of BIM data exchange and the automation of BIM-based workflow. The projects are led by the BIM research group at the School of Software, Tsinghua University, China.
