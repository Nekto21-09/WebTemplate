import { send } from "../utilities";

let WelcomeDiv = document.querySelector("#WelcomeDiv") as HTMLDivElement;

if (localStorage.getItem("UserID") != null) {
    let UserName = await send("GetUserName", localStorage.getItem("UserID")) as string;


    WelcomeDiv.innerText = "Welcome " + UserName + "!";
}