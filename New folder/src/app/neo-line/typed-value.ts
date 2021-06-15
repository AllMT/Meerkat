type NeoType = "Hash160" | "Boolean" | "Integer" | "String";

type TypedValue = { type: NeoType; value: string | boolean };

export default TypedValue;