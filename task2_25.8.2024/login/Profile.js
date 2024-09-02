
async function userinfo(value) {


    const url = `https://localhost:44363/api/Users/UserInfo/${value}`;
    const response = await fetch(url);
    const data = await response.json();
    document.getElementById("Username").innerHTML = data.username;
    document.getElementById("useremail").innerHTML = data.email;
    document.getElementById("userid").innerHTML = data.userId;
}