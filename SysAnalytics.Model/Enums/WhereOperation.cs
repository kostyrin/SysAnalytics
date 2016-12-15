namespace SysAnalytics.Model.Enums
{
    /// <summary>
    /// The supported operations in where-extension
    /// </summary>
    public enum WhereOperation
    {
        //{"name": "eq", "description": "equal", "operator":"="},
        [StringValue("eq")]Equal,
        //{"name": "ne", "description": "not equal", "operator":"<>"},
        [StringValue("ne")]NotEqual,
        //{"name": "cn", "description": "contains", "operator":"LIKE"},
        [StringValue("cn")]Contains,
		//{"name": "lt", "description": "less", "operator":"<"},
        [StringValue("lt")]Less,
		//{"name": "le", "description": "less or equal","operator":"<="},
        [StringValue("le")]LessOrEqual,
		//{"name": "gt", "description": "greater", "operator":">"},
        [StringValue("gt")]Greater,
		//{"name": "ge", "description": "greater or equal", "operator":">="},
        [StringValue("ge")]GreaterOrEqual,
		//{"name": "bw", "description": "begins with", "operator":"LIKE"},
        [StringValue("bw")]BeginsWith,
		//{"name": "bn", "description": "does not begin with", "operator":"NOT LIKE"},
        [StringValue("bn")]BeginsWithNot,
		//{"name": "in", "description": "in", "operator":"IN"},
        [StringValue("in")]In,
		//{"name": "ni", "description": "not in", "operator":"NOT IN"},
        [StringValue("ni")]NotIn,
		//{"name": "ew", "description": "ends with", "operator":"LIKE"},
        [StringValue("ew")]EndWith,
		//{"name": "en", "description": "does not end with", "operator":"NOT LIKE"},
        [StringValue("en")]EndWithNot,
		//{"name": "nc", "description": "does not contain", "operator":"NOT LIKE"},
        [StringValue("nc")]ContainNot,
		//{"name": "nu", "description": "is null", "operator":"IS NULL"},
        [StringValue("nu")]IsNull,
		//{"name": "nn", "description": "is not null", "operator":"IS NOT NULL"}
        [StringValue("nn")]IsNotNull,
    }
}