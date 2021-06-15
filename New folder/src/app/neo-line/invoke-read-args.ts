import TypedValue from "./typed-value";

type InvokeReadArgs = {
    scriptHash: string;
    operation: string;
    args: TypedValue[];
};

export default InvokeReadArgs;