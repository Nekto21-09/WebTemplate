import { send } from "../utilities";

let passwordInput = document.querySelector("#passwordInput")! as HTMLInputElement;
let usernameInput = document.querySelector("#usernameInput")! as HTMLInputElement;
let loginbu = document.querySelector("#loginbu")! as HTMLButtonElement;

loginbu.onclick = async function () {
   let [UserFound, UserID] = await send("login", [usernameInput.value, passwordInput.value]) as [boolean, string];
   console.log("User Found " + UserFound);

   if (UserFound) {
      localStorage.setItem("UserID", UserID);
   }
}