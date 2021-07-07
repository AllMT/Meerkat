import InvokeReadArgs from "./invoke-read-args";
import InvokeWriteArgs from "./invoke-write-args";
import NeoAccount from "./neo-account";
import Signers from "./signers";
import TypedValue from "./typed-value";

export interface NeoLineN3Interface {
    getAccount(): Promise<NeoAccount>;

    //   getBalance(params: GetBalanceArgs[]): Promise<{
    //     [address: string]: { contract: string; symbol: string; amount: string }[];
    //   }>;

    invoke(params: InvokeReadArgs & InvokeWriteArgs & Signers): Promise<any>;

    invokeRead(
        params: InvokeReadArgs & Signers
    ): Promise<{ script: string; stack: TypedValue[]; state: string }>;

    ScriptHashToAddress(params: {scriptHash: string});
    AddressToScriptHash(params: {address: string});

    //   // Note that the order of items in the result array is not consistent with
    //   // the order of the items in the input array.
    //   invokeReadMulti(params: {
    //     invokeReadArgs: InvokeReadArgs[];
    //     signers: { account: string; scopes: number }[];
    //   }): Promise<{ script: string; stack: TypedValue[]; state: string }[]>;
}
