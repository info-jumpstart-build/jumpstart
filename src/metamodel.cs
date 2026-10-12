using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace jumpstart {

    public static class TypeMapping
    {
        /// <summary>
        /// Extracts the base data type by splitting on punctuation (particularly parentheses)
        /// and returns the left portion as the key for dictionary lookups
        /// </summary>
        /// <param name="dataType">The full data type string (e.g., "numeric(18,4)", "varchar(255)")</param>
        /// <returns>The base data type without parameters (e.g., "numeric", "varchar")</returns>
        public static string GetBaseDataType(string dataType)
        {
            if (string.IsNullOrEmpty(dataType))
                return dataType;

            // Split by common punctuation that indicates parameters
            var punctuationChars = new[] { '(', '[', '<', ' ' };
            int index = dataType.IndexOfAny(punctuationChars);
            
            if (index > 0)
            {
                return dataType.Substring(0, index).ToLower().Trim();
            }
            
            return dataType.ToLower().Trim();
        }

        /// <summary>
        /// Maps a MetaAttribute.DotNetType (e.g. "long", "string", "DateTime") to the
        /// TypeScript type used in generated Node/React templates (web/nodejs). Kept as
        /// a static method here -- rather than a lambda declared inline in a .cshtml
        /// template -- because RazorLight chokes on local `Func&lt;string, string&gt;`
        /// variables assigned via a multi-line lambda inside an `@{ }` block.
        /// </summary>
        public static string ToTsType(string dotNetType)
        {
            switch (dotNetType)
            {
                case "string": return "string";
                case "bool": return "boolean";
                case "long":
                case "int":
                case "short":
                case "decimal":
                case "double":
                case "float":
                    return "number";
                default:
                    return "string";
            }
        }

        /// <summary>
        /// Drives which HTML input widget a generated Node/React edit form renders for
        /// a given MetaAttribute.DotNetType. Distinct from ToTsType above: an "enum"/FK
        /// field is still a plain number on the wire (e.g. org_id), but needs a
        /// &lt;select&gt; instead of a number input -- callers layer that FK check on
        /// top of this before falling back to it for non-FK attributes.
        /// </summary>
        public static string ToFormKind(string dotNetType)
        {
            switch (dotNetType)
            {
                case "bool": return "boolean";
                case "DateTime": return "date";
                case "long":
                case "int":
                case "short":
                case "decimal":
                case "double":
                case "float":
                    return "number";
                default:
                    return "string";
            }
        }

        public static readonly Dictionary<string, string> DataTypeMap = new()
        {
            { "integer", "int" },
            { "bigint", "long" },
            { "smallint", "short" },
            { "serial", "int" },
            { "bigserial", "long" },
            { "boolean", "bool" },
            { "char", "string" },
            { "varchar", "string" },
            { "text", "string" },
            { "date", "DateTime" },
            { "timestamp", "DateTime" },
            { "timestamptz", "DateTime" },
            { "real", "float" },
            { "double precision", "double" },
            { "numeric", "decimal" },
            { "json", "string" },
            { "uuid", "Guid" },
            { "bytea", "byte[]" },
            { "xml", "string" }
        };

        
        

        public static readonly Dictionary<string, string> ConvertMap = new()
        {
            {"integer" , "ToInt32"},	
            {"bigint" , "ToInt64"},	
            {"smallint" , "ToInt16"},	
            {"serial" , "ToInt32"},	
            {"bigserial" , "ToInt64"},	
            {"boolean" , "ToBoolean"},	
            {"char" , "ToString"},	
            {"varchar" , "ToString"},	
            {"text" , "ToString"},	
            {"date" , "ToDateTime"},	
            {"timestamp" , "ToDateTime"},	
            {"timestamptz" , "ToDateTime"},	
            {"time" , "ToDateTime"},	 // Convert to DateTime, then use .TimeOfDay for TimeSpan
            {"timetz" , "ToDateTime" },	//  Convert to DateTime, then use .TimeOfDay for TimeSpan
            {"real" , "ToSingle"},	
            {"double precision" , "ToDouble"},	
            {"numeric" , "ToDecimal"},	
            {"numeric(18,4)" , "ToDecimal"},	
            {"decimal" , "ToDecimal"},	
            {"bytea" , "ToByte[]"},	
            {"uuid" , "Guid.Parse" },	// Guid.Parse is used instead of Convert for UUIDs
            {"json" , "ToString"},	
            {"jsonb" , "ToString"},	
            {"xml" , "ToString"},	
            {"money" , "ToDecimal"},	
            {"inet" , "ToString"},	
            {"cidr" , "ToString"},	
            {"macaddr" , "ToString"}	
        };
        public static readonly Dictionary<string, string> InputMap = new()
        {
            {"integer", "Number"},
            {"bigint", "Number"},
            {"smallint", "Number"},
            {"serial", "Number"},
            {"bigserial", "Number"},
            {"boolean", "Radio"},
            {"char", "Text"},
            {"varchar", "Text"},
            {"text", "TextArea"},
            {"date", "Date"},
            {"timestamp", "Date"},
            {"timestamptz", "Date"},
            {"real", "Number"},
            {"double precision", "Number"},
            {"numeric", "Number"},
            {"numeric(18,4)", "Number"},
            {"json", "TextArea"},
            {"uuid", "Text"},
            {"bytea", "Text"},
            {"xml", "Text"}
        };

        public static readonly Dictionary<string, string> PostgreSQLToMSSQLMap = new()
        {
            // Integer types
            { "smallint", "SMALLINT" },
            { "integer", "INT" },
            { "bigint", "BIGINT" },
            { "serial", "INT IDENTITY(1,1)" },
            { "bigserial", "BIGINT IDENTITY(1,1)" },
            
            // Character types
            { "character", "CHAR" },
            { "character varying", "VARCHAR" },
            { "varchar", "VARCHAR" },
            { "char", "CHAR" },
            { "text", "TEXT" },
            { "nchar", "NCHAR" },
            { "nvarchar", "NVARCHAR" },
            { "ntext", "NTEXT" },
            
            // Numeric types
            { "numeric", "DECIMAL" },
            { "decimal", "DECIMAL" },
            { "real", "REAL" },
            { "double precision", "FLOAT" },
            { "float", "FLOAT" },
            { "money", "MONEY" },
            { "smallmoney", "SMALLMONEY" },
            
            // Date/Time types
            { "date", "DATE" },
            { "time", "TIME" },
            { "timestamp", "DATETIME2" },
            { "timestamp without time zone", "DATETIME2" },
            { "timestamp with time zone", "DATETIME2" },
            { "timestamptz", "DATETIME2" },
            { "time without time zone", "TIME" },
            { "time with time zone", "TIME" },
            { "timetz", "TIME" },
            { "interval", "VARCHAR(50)" }, // SQL Server doesn't have direct interval type
            
            // Boolean type
            { "boolean", "BIT" },
            
            // Binary types
            { "bytea", "VARBINARY(MAX)" },
            { "binary", "BINARY" },
            { "varbinary", "VARBINARY" },
            
            // Other types
            { "uuid", "UNIQUEIDENTIFIER" },
            { "xml", "XML" },
            { "json", "NVARCHAR(MAX)" },
            { "jsonb", "NVARCHAR(MAX)" },
            
            // Network types (PostgreSQL specific - map to string equivalents)
            { "inet", "VARCHAR(45)" },
            { "cidr", "VARCHAR(43)" },
            { "macaddr", "VARCHAR(17)" },
            { "macaddr8", "VARCHAR(23)" },
            
            // Geometric types (PostgreSQL specific - map to string equivalents)
            { "point", "VARCHAR(50)" },
            { "line", "VARCHAR(100)" },
            { "lseg", "VARCHAR(100)" },
            { "box", "VARCHAR(100)" },
            { "path", "VARCHAR(MAX)" },
            { "polygon", "VARCHAR(MAX)" },
            { "circle", "VARCHAR(100)" },
            
            // Array types (PostgreSQL specific - map to string equivalents)
            { "integer[]", "NVARCHAR(MAX)" },
            { "text[]", "NVARCHAR(MAX)" },
            { "varchar[]", "NVARCHAR(MAX)" }
        };
    }

    public class ChildRelationship
    {
        public string Role { get; set; }
        public string Label { get; set; }
        public MetaObject Object { get; set; }
        
        public ChildRelationship(string role, string label, MetaObject obj)
        {
            Role = role;
            Label = label;
            Object = obj;
        }
    }

/// <summary>
/// A many-to-many relationship reached through a junction/mapping table
/// (FK_TYPE=map, see docs/metadata.md). Populated on the "self" side --
/// e.g. Principal.MapRelationships holds one entry per other side
/// reachable through op_role_member (org, in this example). Anchored
/// from a single "self" row, so the UI-facing query is a LEFT JOIN from
/// OtherObject to MapObject (not a literal three-way FULL OUTER JOIN --
/// unnecessary once one side is pinned to a single id).
/// </summary>
public class MapRelationship
{
    public MetaObject MapObject { get; set; }       // the junction table itself (e.g. op_role_member)
    public MetaAttribute SelfAttribute { get; set; } // the map-table FK column pointing back at "self" (this object)
    public MetaAttribute OtherAttribute { get; set; }// the map-table FK column pointing at OtherObject
    public MetaObject OtherObject { get; set; }      // the other side of the many-to-many relationship
    public string Role { get; set; }                 // OtherAttribute.Name minus trailing "_id" -- disambiguates multiple map relationships to the same OtherObject
    public string Label { get; set; }                // display label, defaults to OtherObject.Label

    public MapRelationship(MetaObject mapObject, MetaAttribute selfAttribute, MetaAttribute otherAttribute, MetaObject otherObject, string role, string label)
    {
        MapObject = mapObject;
        SelfAttribute = selfAttribute;
        OtherAttribute = otherAttribute;
        OtherObject = otherObject;
        Role = role;
        Label = label;
    }
}

public class EnumRelationship
{   public string idColumn {get;set;}
    public string nameColumn {get;set;}
    public MetaAttribute EnumAttribute {get;set;}
    public EnumRelationship(string idColumn, string nameColumn, MetaAttribute enumAttribute)
    {
        this.idColumn = idColumn;
        this.nameColumn = nameColumn;
        this.EnumAttribute = enumAttribute;
    }
}

public class ViewRelationship
{
    public MetaAttribute FkAttribute { get; set; }  // The FK attribute from the view
    public MetaObject FkObject { get; set; }  // The referenced object
    public string TableAlias { get; set; }  // The alias for this table in the JOIN
    public string ColumnPrefix { get; set; }  // Prefix for column names (base FK name)
    public List<ViewRelationship> NestedRelationships { get; set; }  // For recursive FK traversal
    public List<MetaAttribute> RwkAttributes { get; set; }  // RWK attributes from this FK object
    
    public ViewRelationship(MetaAttribute fkAttribute, MetaObject fkObject, string tableAlias, string columnPrefix)
    {
        FkAttribute = fkAttribute;
        FkObject = fkObject;
        TableAlias = tableAlias;
        ColumnPrefix = columnPrefix;
        NestedRelationships = new List<ViewRelationship>();
        RwkAttributes = new List<MetaAttribute>();
    }
}   
    public class MetaBaseElement
    {
        public string Name{get;set;}
        protected string _filename = string.Empty;

        public string FileName 
        {
            get 
            {
                if (string.IsNullOrEmpty(_filename)) 
                {
                    return Name;

                }
                else
                {
                    return _filename;
                }
            }
            set 
            {
                _filename = value;
            
            }
        }

        public readonly string  CR = "\n";
    }
    public class MetaAttribute : MetaBaseElement
    {
        private string _pascalName = null; 
        public string SqlDataType { get; set; }
        public string MSSQLDataType { get; set; }
        public string DotNetType {get;set;}

        public string InputType {get;set;}

        public string PascalName {
            get {
                if (_pascalName == null) _pascalName =  Utils.ConvertToPascalCase(Name);
                return _pascalName;
            }
        }
        public string ConvertMethod {get;set;}
        public string Length { get; set; }
        public string Label { get; set; }
        public string RWK { get; set; }
        public string FkType {get;set;}
        public string FkTable {get;set;}
        public string FkObject {get;set;}
        public string FkVar {get;set;}


        public bool IsNullable{get;set;} = false;

        public string TestDataSet{get;set;}

        bool _IsGlobal{get;set;} = false;
        public bool IsGlobal() { return _IsGlobal;}
        public void SetGlobal() { _IsGlobal = true;}

        // Table alias for view relationships (used when this attribute is part of a view)
        public string TableAlias { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nDataType: {SqlDataType}\nLength: {Length}\nLabel: {Label}\nRWK: {RWK}\n";
        }
    }

    public class MetaObject : MetaBaseElement
    {
        private List<MetaAttribute> _userAttributes = null;
        private List<MetaAttribute> _globalAttributes = null;
        public string Namespace {get; private set;}
        public string DomainObj { get; private set; }
        public bool IsView {get { return TableName.ToLower().EndsWith("_view"); }}
        public string DomainObjView { get { if (IsView) return DomainObj; else return DomainObj + "View"; }}

        public string DomainConst {
            get {
                return DomainObj.ToUpper();
            }
        }
        public string DomainVar { get; private set; }
        public string TableName { get; private set; }

        public string Label {get; private set;}
        public string SchemaName {get; set;}
        public string NavMenu {get; set;}
        private string _uri = string.Empty;
        public string Uri { get { return string.IsNullOrEmpty(_uri) ? DomainVar : _uri; } }

        /// <summary>
        /// Names the TS row-type used by a generated Node/React Edit page's DataTable
        /// for one of this object's child relationships (see
        /// web/nodejs/src/pages/Edittemplate.tsx.cshtml). Self-referential children
        /// (childObject == this, e.g. a "parent_id" pointing back at the same table)
        /// reuse this object's own generated interface instead of a duplicate
        /// "...Row" declaration. A method here (rather than a `Func&lt;MetaObject,
        /// string&gt;` local declared in the template) sidesteps the same RazorLight
        /// limitation ToTsType/ToFormKind above work around.
        /// </summary>
        public string ChildRowTypeName(MetaObject childObject)
        {
            return childObject.DomainObj == DomainObj ? DomainObj : childObject.DomainObj + "Row";
        }

        /// <summary>
        /// Names the generated DataTableColumn[] const for one of this object's child
        /// relationships, paired with ChildRowTypeName above.
        /// </summary>
        public string ChildColumnsConstName(MetaObject childObject)
        {
            return childObject.DomainObj == DomainObj ? "OWN_COLUMNS" : (childObject.DomainObj + "_COLUMNS").ToUpperInvariant();
        }

        public MetaModel Model {get;set;}
        
        public List<MetaAttribute> Attributes { get; private set; } = new();
        public List<ChildRelationship> Children { get; private set; } = new();
        public List<EnumRelationship> EnumAttributes { get; private set; } = new();
        public List<ViewRelationship> ViewRelationships { get; private set; } = new();
        public List<MapRelationship> MapRelationships { get; private set; } = new();

        public List<MetaAttribute> UserAttributes {
            get 
            {
                if (_userAttributes == null)
                {
                    _userAttributes = new List<MetaAttribute>();
                    foreach(MetaAttribute a in Attributes)
                    {
                        if (!a.IsGlobal()) _userAttributes.Add(a);
                    }
                }
                return _userAttributes;
            }
        }
        public List<MetaAttribute> GlobalAttributes {
            get 
            {
                if (_globalAttributes == null)
                {
                    _globalAttributes = new List<MetaAttribute>();
                    foreach(MetaAttribute a in Attributes)
                    {
                        if (a.IsGlobal()) _globalAttributes.Add(a);
                    }
                }
                return _globalAttributes;
            }
        }
        public bool IsAudited { get; private set; } = true;

        public MetaObject(string _namespace, string tableName, string schemaName, string label, string navMenu, string uri = "", bool isAudited = true)
        {
            Namespace = _namespace;
            TableName = tableName;
            DomainObj = Utils.ConvertToPascalCase(tableName);
            DomainVar = DomainObj.ToLower();
            Name = tableName;
            Label = label;
            NavMenu = navMenu;
            _uri = uri ?? string.Empty;
            SchemaName = schemaName;
            FileName = DomainObj;
            IsAudited = isAudited;
        }

        public override string ToString()
        {
            string result = $"Domain Obj: {DomainObj}\nDomain Var: {DomainVar}\nTable Name: {TableName}\n";
            foreach (var attribute in Attributes)
            {
                result += attribute.ToString();
            }
            return result;
        }

        /// <summary>
        /// Processes children by finding parent foreign keys and establishing parent-child relationships.
        /// Iterates through all attributes to find foreign keys of type "parent" and adds this object
        /// to the Children list of the parent object with the appropriate role (column name).
        /// </summary>
        /// <param name="allObjects">Collection of all MetaObjects to search for parent references</param>
        public void ProcessChildren(IEnumerable<MetaObject> allObjects)
        {
            foreach (var attribute in Attributes)
            {
                // Check if this attribute is a parent foreign key
                if (!string.IsNullOrEmpty(attribute.FkType) && 
                    attribute.FkType.ToLower() == "parent" && 
                    !string.IsNullOrEmpty(attribute.FkTable))
                {
                    // Find the parent object by table name
                    var parentObject = allObjects.FirstOrDefault(mo => mo.TableName == attribute.FkTable);
                    
                    if (parentObject != null)
                    {
                        // Use the attribute name as the role to distinguish multiple FKs to the same table
                        string role = attribute.Name.Replace("_id", "");
                        string label = attribute.Label ?? attribute.Name; // Use label if available, fallback to name
                        
                        // Check if this role-object combination already exists
                        bool alreadyExists = parentObject.Children.Any(child => 
                            child.Role == role && child.Object == this);
                        
                        if (!alreadyExists)
                        {
                            parentObject.Children.Add(new ChildRelationship(role, label, this));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes many-to-many ("map") relationships reached through this object
        /// acting as a junction/mapping table (e.g. op_role_member between principal
        /// and op_role). Looks for pairs of FK_TYPE=map attributes on this object --
        /// each ordered pair (selfAttribute, otherAttribute) describes one side of the
        /// relationship, registered on selfObject.MapRelationships so a many-to-many
        /// table with more than two map FKs (unusual, but not disallowed) yields one
        /// MapRelationship per ordered pair rather than assuming exactly two.
        /// </summary>
        /// <param name="allObjects">Collection of all MetaObjects to resolve FkTable references against</param>
        public void ProcessMapRelationships(IEnumerable<MetaObject> allObjects)
        {
            var mapAttributes = Attributes.Where(a =>
                !string.IsNullOrEmpty(a.FkType) &&
                a.FkType.ToLower() == "map" &&
                !string.IsNullOrEmpty(a.FkTable)).ToList();

            if (mapAttributes.Count < 2)
            {
                // Not a junction table -- a map relationship needs at least two
                // FK_TYPE=map columns on the same table to pair up.
                return;
            }

            foreach (var selfAttribute in mapAttributes)
            {
                var selfObject = allObjects.FirstOrDefault(mo => mo.TableName == selfAttribute.FkTable);
                if (selfObject == null) { continue; }

                foreach (var otherAttribute in mapAttributes)
                {
                    if (ReferenceEquals(selfAttribute, otherAttribute)) { continue; }

                    var otherObject = allObjects.FirstOrDefault(mo => mo.TableName == otherAttribute.FkTable);
                    if (otherObject == null) { continue; }

                    string role = otherAttribute.Name.EndsWith("_id")
                        ? otherAttribute.Name.Substring(0, otherAttribute.Name.Length - 3)
                        : otherAttribute.Name;

                    bool alreadyExists = selfObject.MapRelationships.Any(m =>
                        m.MapObject == this && m.SelfAttribute == selfAttribute && m.OtherAttribute == otherAttribute);

                    if (!alreadyExists)
                    {
                        selfObject.MapRelationships.Add(new MapRelationship(this, selfAttribute, otherAttribute, otherObject, role, otherObject.Label));
                    }
                }
            }
        }

        public void ProcessEnumObjects(IEnumerable<MetaObject> allObjects)
        {
            foreach (var attribute in Attributes)
            {
               

                // Check if this attribute is an enum foreign key
                if (!string.IsNullOrEmpty(attribute.FkType) && 
                    attribute.FkType.ToLower() == "enum" && 
                    !string.IsNullOrEmpty(attribute.FkTable) )
                {
                    
                    // Find the enum object by table name
                    var enumObject = allObjects.FirstOrDefault(mo => mo.TableName == attribute.FkTable);

                    
                    if (enumObject != null)
                    {
                        // Get alias from attribute name (remove "_id" suffix if present)
                        string aliasBase = attribute.Name;
                        if (aliasBase.EndsWith("_id"))
                        {
                            aliasBase = aliasBase.Substring(0, aliasBase.Length - 3);
                        }
                        string alias = aliasBase;
                        
                        // Find the first RWK attribute from the enum object to construct the name column
                        var rwkAttribute = enumObject.Attributes.FirstOrDefault(attr => attr.RWK == "1");
                        
                        if (rwkAttribute != null)
                        {
                            // Construct name column following the same pattern as fkAttrName: alias + "_" + rwkAttribute.Name
                            string nameColumn = alias + "_" + rwkAttribute.Name;
                            if (!nameColumn.ToLower().EndsWith("_id") )
                            {
                                EnumAttributes.Add(new EnumRelationship(attribute.Name, nameColumn, attribute));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes view objects by recursively traversing foreign keys and synthesizing MetaAttributes
        /// for RWK columns. Creates both a flattened Attributes list and a hierarchical ViewRelationships
        /// structure to drive LEFT OUTER JOIN logic.
        /// </summary>
        /// <param name="allObjects">Collection of all MetaObjects to search for FK references</param>
        public void ProcessView(IEnumerable<MetaObject> allObjects)
        {
            // Only process if this is a view
            if (!IsView)
            {
                return;
            }

            Console.WriteLine($"ProcessView: Processing view '{TableName}'");
            
            // Get all foreign key attributes (excluding id)
            var foreignKeys = Attributes.Where(attr => !string.IsNullOrEmpty(attr.FkTable) && attr.Name != "id").ToList();
            
            // Console.WriteLine($"ProcessView: Found {foreignKeys.Count} foreign key(s)");
            // foreach(var fk in foreignKeys)
            // {
            //     Console.WriteLine($"  FK: {fk.Name} -> {fk.FkTable}");
            // }
            
            if (foreignKeys.Count == 0)
            {
                // Console.WriteLine($"ProcessView: No foreign keys found for view '{TableName}', skipping");
                return; // Views must have at least one FK
            }

            var usedAliases = new HashSet<string>();

            // Helper method to recursively process RWK attributes from an FK object
            // originalViewTableName: Used to detect self-referential FKs and prevent infinite recursion
            void ProcessRwkAttributes(MetaObject fkObject, string columnPrefix, string tableAlias, ViewRelationship parentRelationship, HashSet<string> aliases, string originalViewTableName)
            {
                // Console.WriteLine($"  ProcessRwkAttributes: Processing FK object '{fkObject.TableName}' with prefix '{columnPrefix}', alias '{tableAlias}'");
                foreach (var attr in fkObject.Attributes)
                {
                    if (attr.RWK == "1")
                    {
                        // Check if this RWK attribute is itself a foreign key
                        if (!string.IsNullOrEmpty(attr.FkTable))
                        {
                            // Console.WriteLine($"    RWK attribute '{attr.Name}' is a nested FK -> {attr.FkTable}");
                            
                            // Check for self-referential FK (FK points back to the FK object we're currently processing)
                            // This prevents infinite recursion when a table has an FK to itself (e.g., parent_id)
                            bool isSelfReferential = attr.FkTable == fkObject.TableName;
                            
                            if (isSelfReferential)
                            {
                                // Console.WriteLine($"      DETECTED self-referential FK: '{attr.Name}' -> '{attr.FkTable}' (same as current FK object '{fkObject.TableName}')");
                                // Console.WriteLine($"      Skipping recursion to prevent infinite loop - only processing one level");
                                
                                // For self-referential FKs, we don't recurse but still create a synthesized attribute
                                // representing the FK itself (e.g., parent_id as a column)
                                // Note: The RWK attribute IS the FK, so we still want to include it
                                var columnName = columnPrefix + "_" + attr.Name;
                                var synthesizedAttr = new MetaAttribute
                                {
                                    Name = columnName,
                                    SqlDataType = attr.SqlDataType,
                                    MSSQLDataType = attr.MSSQLDataType,
                                    DotNetType = attr.DotNetType,
                                    ConvertMethod = attr.ConvertMethod,
                                    InputType = attr.InputType,
                                    Length = attr.Length,
                                    Label = columnPrefix + " " + (attr.Label ?? attr.Name),
                                    RWK = attr.RWK,
                                    FkType = attr.FkType,
                                    FkTable = attr.FkTable,
                                    FkObject = attr.FkObject,
                                    FkVar = attr.FkVar,
                                    IsNullable = attr.IsNullable,
                                    TestDataSet = attr.TestDataSet,
                                    TableAlias = tableAlias  // Store the alias for JOIN generation
                                };
                                
                                // Add to flattened attributes list
                                Attributes.Add(synthesizedAttr);
                                
                                // Add to parent relationship's RWK attributes
                                parentRelationship.RwkAttributes.Add(synthesizedAttr);
                                
                                // Console.WriteLine($"      Created synthesized attribute for self-ref FK: '{columnName}' (alias='{tableAlias}', original='{attr.Name}')");
                            }
                            else
                            {
                                // Find the nested FK object
                                var nestedFkObject = allObjects.FirstOrDefault(obj => obj.TableName == attr.FkTable);
                                if (nestedFkObject != null)
                                {
                                    // Create alias for nested FK table
                                    var nestedAliasBase = attr.Name;
                                    if (nestedAliasBase.EndsWith("_id"))
                                    {
                                        nestedAliasBase = nestedAliasBase.Substring(0, nestedAliasBase.Length - 3);
                                    }
                                    var nestedAlias = nestedAliasBase;
                                    var counter = 1;
                                    while (aliases.Contains(nestedAlias))
                                    {
                                        nestedAlias = nestedAliasBase + "_" + counter;
                                        counter++;
                                    }
                                    aliases.Add(nestedAlias);

                                    // Create nested relationship
                                    var nestedPrefix = columnPrefix + "_" + nestedAliasBase;
                                    var nestedRelationship = new ViewRelationship(attr, nestedFkObject, nestedAlias, nestedPrefix);
                                    parentRelationship.NestedRelationships.Add(nestedRelationship);
                                    
                                    // Console.WriteLine($"      Created nested ViewRelationship: alias='{nestedAlias}', prefix='{nestedPrefix}', table='{nestedFkObject.TableName}'");

                                    // Recursively process nested FK (pass originalViewTableName to detect cycles)
                                    ProcessRwkAttributes(nestedFkObject, nestedPrefix, nestedAlias, nestedRelationship, aliases, originalViewTableName);
                                }
                                else
                                {
                                    // Console.WriteLine($"      WARNING: Nested FK object '{attr.FkTable}' not found");
                                }
                            }
                        }
                        else
                        {
                            // This RWK is not a FK, create a synthesized MetaAttribute
                            var columnName = columnPrefix + "_" + attr.Name;
                            var synthesizedAttr = new MetaAttribute
                            {
                                Name = columnName,
                                SqlDataType = attr.SqlDataType,
                                MSSQLDataType = attr.MSSQLDataType,
                                DotNetType = attr.DotNetType,
                                ConvertMethod = attr.ConvertMethod,
                                InputType = attr.InputType,
                                Length = attr.Length,
                                Label = columnPrefix + " " + (attr.Label ?? attr.Name),
                                RWK = attr.RWK,
                                FkType = attr.FkType,
                                FkTable = attr.FkTable,
                                FkObject = attr.FkObject,
                                FkVar = attr.FkVar,
                                IsNullable = attr.IsNullable,
                                TestDataSet = attr.TestDataSet,
                                TableAlias = tableAlias  // Store the alias for JOIN generation
                            };
                            
                            // Add to flattened attributes list
                            Attributes.Add(synthesizedAttr);
                            
                            // Add to parent relationship's RWK attributes
                            parentRelationship.RwkAttributes.Add(synthesizedAttr);
                            
                            // Console.WriteLine($"    Created synthesized attribute: '{columnName}' (alias='{tableAlias}', original='{attr.Name}')");
                        }
                    }
                }
            }

            // Process first FK table as anchor
            var firstFk = foreignKeys[0];
            var firstFkObject = allObjects.FirstOrDefault(obj => obj.TableName == firstFk.FkTable);
            int counter = 0;
            
            // Console.WriteLine($"ProcessView: Processing first FK '{firstFk.Name}' -> '{firstFk.FkTable}'");
            
            if (firstFkObject != null)
            {
                // Create alias for first FK table
                var firstAliasBase = firstFk.Name;
                if (firstAliasBase.EndsWith("_id"))
                {
                    firstAliasBase = firstAliasBase.Substring(0, firstAliasBase.Length - 3);
                }
                var firstAlias = firstAliasBase;
                while (usedAliases.Contains(firstAlias))
                {
                    firstAlias = firstAliasBase + "_" + counter;
                    counter++;
                }
                usedAliases.Add(firstAlias);

                // Create ViewRelationship for first FK
                var firstRelationship = new ViewRelationship(firstFk, firstFkObject, firstAlias, firstAliasBase);
                ViewRelationships.Add(firstRelationship);
                
                // Console.WriteLine($"  Created first ViewRelationship: alias='{firstAlias}', prefix='{firstAliasBase}', table='{firstFkObject.TableName}'");

                // Process RWK attributes from first FK (pass TableName to detect self-referential FKs)
                ProcessRwkAttributes(firstFkObject, firstAliasBase, firstAlias, firstRelationship, usedAliases, TableName);
            }
            else
            {
                // Console.WriteLine($"  WARNING: First FK object '{firstFk.FkTable}' not found");
            }

            
            // Process remaining FK tables
            for (int i = 1; i < foreignKeys.Count; i++)
            {
                var fk = foreignKeys[i];
                var fkObject = allObjects.FirstOrDefault(obj => obj.TableName == fk.FkTable);
                
                // Console.WriteLine($"ProcessView: Processing additional FK {i+1}/{foreignKeys.Count}: '{fk.Name}' -> '{fk.FkTable}'");
                
                if (fkObject != null)
                {
                    // Create alias for this FK table
                    var aliasBase = fk.Name;
                    if (aliasBase.EndsWith("_id"))
                    {
                        aliasBase = aliasBase.Substring(0, aliasBase.Length - 3);
                    }
                    var alias = aliasBase;
                    counter = 1;
                    while (usedAliases.Contains(alias))
                    {
                        alias = aliasBase + "_" + counter;
                        counter++;
                    }
                    usedAliases.Add(alias);

                    // Create ViewRelationship for this FK
                    var relationship = new ViewRelationship(fk, fkObject, alias, aliasBase);
                    ViewRelationships.Add(relationship);
                    
                    // Console.WriteLine($"  Created ViewRelationship: alias='{alias}', prefix='{aliasBase}', table='{fkObject.TableName}'");

                    // Process RWK attributes from this FK (pass TableName to detect self-referential FKs)
                    ProcessRwkAttributes(fkObject, aliasBase, alias, relationship, usedAliases, TableName);
                }
                else
                {
                    // Console.WriteLine($"  WARNING: FK object '{fk.FkTable}' not found");
                }
            }
            
            // Summary logging
            // Console.WriteLine($"ProcessView: Completed processing view '{TableName}'");
            // Console.WriteLine($"  Total ViewRelationships: {ViewRelationships.Count}");
            // Console.WriteLine($"  Total synthesized attributes: {Attributes.Count(attr => !string.IsNullOrEmpty(attr.TableAlias))}");
            
            // Log ViewRelationships structure
            // foreach(var relationship in ViewRelationships)
            // {
            //     Console.WriteLine($"  ViewRelationship: FK='{relationship.FkAttribute.Name}', alias='{relationship.TableAlias}', prefix='{relationship.ColumnPrefix}', RWK attrs={relationship.RwkAttributes.Count}, nested={relationship.NestedRelationships.Count}");
            // }
        }

       
    }

    public class MetaSchema : MetaBaseElement
    {
        
        public List<MetaObject> Objects { get;  set; } = new();
        public Dictionary<string, MetaObject> ObjectMap { get;  set; } = new();

        public string Namespace {get; set;}
        public MetaSchema(string name, string _namespace)
        {
            Name = name;
            Namespace = _namespace;
        }

        public override string ToString()
        {
            string result = $"Schema: {Name}\n";
            foreach (var obj in Objects)
            {
                result += obj.ToString();
            }
            return result;
        }
    }

    public class MetaBuild : MetaBaseElement
    {
        public Dictionary<string,List<string>> outputFolderMap = [];
        public List<string> sourceFiles = new();


        public void AddToOutputFolderMap(string outputFolder, string outputFile)
        {
            if (!outputFolderMap.ContainsKey(outputFolder))
            {
                outputFolderMap[outputFolder] = new List<string>();
            }

            List<string> sourceFiles = outputFolderMap[outputFolder];

            string sourceFile = Path.GetFileName(outputFile);

            if (!sourceFiles.Any(f => f == sourceFile))
            {
                sourceFiles.Add(sourceFile);
            }
        }

        public void SetOutputFolder( string outputDir )
        {
            if (outputFolderMap.ContainsKey(outputDir))
            {
                sourceFiles = outputFolderMap[outputDir];
                
            }
        }
    }

    public class MetaModel : MetaBaseElement
    {
        public string Namespace { get; set; }
    

        public Dictionary<string, MetaSchema> Schemas { get; private set; } = new();
        public List<MetaObject> Objects { get; private set; } = new();
        public Dictionary<string, List<MetaObject>> NavMenus { get; private set; } = new();

        public List<MetaAttribute> GlobalAttributes {get; private set;} = new();

        public bool ContainsGlobalAttribute(string name)
        {
            return GlobalAttributes.Any(attr => attr.Name == name);
        }
        public MetaBuild build = new MetaBuild();

        // ── Auth0 settings (populated from ~/.jumpstart.json) ────────────────
        // Templates reference these as @Model.Auth0Domain, @Model.Auth0ClientId, etc.

        /// <summary>Auth0 tenant domain, e.g. "your-tenant.auth0.com"</summary>
        public string Auth0Domain { get; set; } = string.Empty;

        /// <summary>Auth0 SPA client ID from the Blazor application registration</summary>
        public string Auth0ClientId { get; set; } = string.Empty;

        /// <summary>Auth0 API audience, e.g. "https://your-api-identifier"</summary>
        public string Auth0Audience { get; set; } = string.Empty;

        // ── NoAuth toggle (populated from the --noauth CLI flag) ──────────────
        // When true, templates suppress authentication code generation: no
        // Auth0 login flow in the web frontends, no JWT enforcement in the
        // servers. Templates reference this as @Model.NoAuth.
        public bool NoAuth { get; set; } = false;

        // ── Selected stack (populated from ~/.jumpstart.json "templatedefs") ──
        // The full list of template registry names requested for this run, e.g.
        // ["database-pgsql", "server-rust", "web-nodejs", "test-rust", "tools-rust"].
        // Only meaningful when driven via ~/.jumpstart.json, since that's the only
        // mode where all registries for a project are known in a single invocation.
        // Templates reference this as @Model.TemplateDefNames to conditionally emit
        // stack-specific content (see templates/root/makefile.cshtml).
        public List<string> TemplateDefNames { get; set; } = new();

        public MetaModel(string _namespace)
        {
            Name = _namespace;
            Namespace = _namespace;
            build.Name = Name;
        }

        public override string ToString()
        {
            string result = $"Namespace: {Namespace}\n";
            foreach (var schema in Schemas.Values)
            {
                result += schema.ToString();
            }
            return result;  
        }

        public void Process()
        {
           foreach(var obj in Objects)
           {
                obj.ProcessChildren(Objects);
                obj.ProcessMapRelationships(Objects);
                obj.ProcessEnumObjects(Objects);
                obj.ProcessView(Objects);
           }
           
        }   

        public void SortMetaObjectsByReference()
        {
            this.Objects = SortMetaObjectsByReference( this.Objects );

            foreach(var schema in this.Schemas.Values)
            {
                schema.Objects = SortMetaObjectsByReference( schema.Objects );
            }
        }
        
        protected  List<MetaObject> SortMetaObjectsByReference_1(List<MetaObject> metaObjects)
        {
            // Build a dependency graph
            var graph = new Dictionary<MetaObject, List<MetaObject>>();
            var inDegree = new Dictionary<MetaObject, int>();
            
            // Initialize graph and in-degree map
            foreach (var metaObject in metaObjects)
            {
                graph[metaObject] = new List<MetaObject>();
                inDegree[metaObject] = 0;
            }

            // Populate the graph based on foreign key dependencies
            foreach (var metaObject in metaObjects)
            {
                foreach (var attribute in metaObject.Attributes)
                {
                    if (!string.IsNullOrEmpty(attribute.FkObject))
                    {
                        var fkObject = metaObjects.FirstOrDefault(mo => mo.Name == attribute.FkObject);
                        if (fkObject != null)
                        {
                            graph[fkObject].Add(metaObject);
                            inDegree[metaObject]++;
                        }
                    }
                }
            }

            // Perform topological sort using Kahn's algorithm
            var sorted = new List<MetaObject>();
            var queue = new Queue<MetaObject>();

            // Add all nodes with in-degree 0 to the queue
            foreach (var kvp in inDegree)
            {
                if (kvp.Value == 0)
                {
                    queue.Enqueue(kvp.Key);
                }
            }

            

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                sorted.Add(current);

                // Reduce the in-degree of neighbors
                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // Check for circular dependencies
            
            if (sorted.Count != metaObjects.Count)
            {
                throw new InvalidOperationException("Circular dependency detected among MetaObjects.");
            }
            

            return sorted;
        }

        protected List<MetaObject> SortMetaObjectsByReference(List<MetaObject> metaObjects)
        {
            // 1. Build the adjacency list: for each MetaObject, which MetaObjects depend on it?
            //    Because if B has an FK to A, then A should come before B.
            var adjacency = new Dictionary<MetaObject, List<MetaObject>>();
            foreach (var metaObject in metaObjects)
            {
                adjacency[metaObject] = new List<MetaObject>();
            }

            foreach (var metaObject in metaObjects)
            {
                foreach (var attribute in metaObject.Attributes)
                {
                    if (!string.IsNullOrEmpty(attribute.FkObject))
                    {
                        var fkObject = metaObjects.FirstOrDefault(mo => mo.Name == attribute.FkTable);
                        if (fkObject != null)
                        {
                            // fkObject -> metaObject
                            adjacency[fkObject].Add(metaObject);
                        }
                    }
                }
            }

            // 2. Find Strongly Connected Components (SCCs) in this directed graph
            var sccs = TarjanScc(metaObjects, adjacency);

            // 3. Build a graph of SCC "super-nodes" and topologically sort the super-nodes
            //    Each SCC is treated as a single node in the "condensed" graph.
            var sortedSccs = TopologicalSortSccs(sccs, adjacency);

            // 4. Flatten the SCCs in topological order into a single list of MetaObject
            var result = new List<MetaObject>();
            foreach (var scc in sortedSccs)
            {
                // You can order objects within an SCC however you like.
                // Usually the input or alphabetical order is fine.
                result.AddRange(scc);
            }

            return result;
        }

        /// <summary>
        /// Uses Tarjan's algorithm to find strongly connected components in the given graph.
        /// Returns a List of SCCs, where each SCC is a list of MetaObjects.
        /// </summary>
        private List<List<MetaObject>> TarjanScc(
            List<MetaObject> nodes, 
            Dictionary<MetaObject, List<MetaObject>> adjacency)
        {
            var sccResult = new List<List<MetaObject>>();

            var indexMap   = new Dictionary<MetaObject, int>();   // Map: Node -> Tarjan index
            var lowLinkMap = new Dictionary<MetaObject, int>();   // Map: Node -> low-link value
            var onStack    = new HashSet<MetaObject>();
            var stack      = new Stack<MetaObject>();

            int currentIndex = 0;

            // StrongConnect function (recursive DFS)
            void StrongConnect(MetaObject v)
            {
                // Set the depth index for v to the smallest unused index
                indexMap[v] = currentIndex;
                lowLinkMap[v] = currentIndex;
                currentIndex++;
                stack.Push(v);
                onStack.Add(v);

                // Consider successors of v
                foreach (var w in adjacency[v])
                {
                    if (!indexMap.ContainsKey(w))
                    {
                        // Successor w has not yet been visited; recurse on it
                        StrongConnect(w);
                        lowLinkMap[v] = Math.Min(lowLinkMap[v], lowLinkMap[w]);
                    }
                    else if (onStack.Contains(w))
                    {
                        // Successor w is in the stack and hence in the current SCC
                        lowLinkMap[v] = Math.Min(lowLinkMap[v], indexMap[w]);
                    }
                }

                // If v is a root node, pop the stack and generate an SCC
                if (lowLinkMap[v] == indexMap[v])
                {
                    var scc = new List<MetaObject>();
                    MetaObject nodeInScc;
                    do
                    {
                        nodeInScc = stack.Pop();
                        onStack.Remove(nodeInScc);
                        scc.Add(nodeInScc);
                    }
                    while (!nodeInScc.Equals(v));

                    sccResult.Add(scc);
                }
            }

            // Run StrongConnect for all nodes that haven't been visited
            foreach (var node in nodes)
            {
                if (!indexMap.ContainsKey(node))
                {
                    StrongConnect(node);
                }
            }

            return sccResult;
        }

        /// <summary>
        /// Given the list of SCCs, build a "condensed" graph of SCC super-nodes
        /// and perform a topological sort on it. Returns the list of SCCs in topological order.
        /// </summary>
        private List<List<MetaObject>> TopologicalSortSccs(
            List<List<MetaObject>> sccs,
            Dictionary<MetaObject, List<MetaObject>> adjacency)
        {
            // First, build an SCC lookup: which SCC does each node belong to?
            var sccLookup = new Dictionary<MetaObject, int>();
            for (int i = 0; i < sccs.Count; i++)
            {
                foreach (var mo in sccs[i])
                {
                    sccLookup[mo] = i;
                }
            }

            // Build adjacency among the SCCs (condensed graph)
            var sccGraph = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < sccs.Count; i++)
            {
                sccGraph[i] = new HashSet<int>();
            }

            for (int i = 0; i < sccs.Count; i++)
            {
                foreach (var metaObject in sccs[i])
                {
                    // look at the adjacency from metaObject to other nodes
                    foreach (var neighbor in adjacency[metaObject])
                    {
                        int neighborScc = sccLookup[neighbor];
                        if (neighborScc != i)
                        {
                            sccGraph[i].Add(neighborScc);
                        }
                    }
                }
            }

            // Now we topologically sort the condensed graph of SCC indices
            // Kahn's Algorithm or DFS-based approach can be used here.
            // We'll use a Kahn's algorithm style for SCC indices:
            var inDegree = new Dictionary<int, int>();
            for (int i = 0; i < sccs.Count; i++)
            {
                inDegree[i] = 0;
            }

            foreach (var kvp in sccGraph)
            {
                var fromScc = kvp.Key;
                foreach (var toScc in kvp.Value)
                {
                    inDegree[toScc]++;
                }
            }

            var queue = new Queue<int>();
            for (int i = 0; i < sccs.Count; i++)
            {
                if (inDegree[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }

            var sortedSccIndices = new List<int>();
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                sortedSccIndices.Add(current);

                foreach (var neighbor in sccGraph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // If not all SCCs are sorted, there's a problem, but strictly speaking
            // strongly connected components can still be sorted as a condensed graph.
            // If there's somehow a cycle among the SCC "super-nodes" (which shouldn't happen),
            // you'd detect it here. Typically, it won't happen since each SCC is collapsed.
            if (sortedSccIndices.Count != sccs.Count)
            {
                // This would be unusual with correct Tarjan's usage,
                // but you could handle or throw if you want.
                throw new InvalidOperationException("Unable to topologically sort the SCCs.");
            }

            // Return the SCCs in topological order
            return sortedSccIndices.Select(sccIndex => sccs[sccIndex]).ToList();
        }

        

    }


}
