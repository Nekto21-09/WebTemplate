import { send } from "../utilities";

let usernameInput = document.querySelector("#usernameInput")! as HTMLInputElement;
let signbu = document.querySelector("#signbu")! as HTMLButtonElement;
let passwordInput = document.querySelector("#passwordInput")! as HTMLInputElement;

signbu.onclick = function () {
    send("signup", [usernameInput.value, passwordInput.value]);
}