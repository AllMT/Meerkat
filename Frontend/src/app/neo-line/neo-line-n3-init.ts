import { NeoLineN3Interface } from "./neo-line-n3-interface";

function NeoLineN3Init(): Promise<NeoLineN3Interface> {
    return new Promise((resolve) => resolve(new (<any>window).NEOLineN3.Init()));
}

export default NeoLineN3Init;