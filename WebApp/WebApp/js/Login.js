/**
 * Created by Chase on 9/16/2015.
 */


function  validateCredentials()
{
    console.log("Got Here");
    var username = document.getElementById("username_ID");
    var password = document.getElementById("password_ID");
    if(username.value!="" && password.value!="") {
        console.log("Both elements entered in!")
    }

    return true;
}