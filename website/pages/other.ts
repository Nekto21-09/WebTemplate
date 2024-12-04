import { send } from "../utilities";
let b = document.getElementById("b")!;
let bu = document.getElementById("bu")!;
let but = document.getElementById("but")!;
let num;

b.onclick = async function () {
    num = await send("Button", null) as number;
    console.log(num);
    but.innerText = num.toString();

}
bu.onclick = async function () {
    num = await send("Mess", null) as number;
    console.log(num);
    but.innerText = num.toString();

}

