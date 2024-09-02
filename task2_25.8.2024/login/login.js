async function login() {
    debugger;
    const url = "https://localhost:44363/api/Users/login";
    let form = document.getElementById("loginform");
    event.preventDefault();
    let data = new FormData(form);
    
    let response = await fetch(url, {
        method: 'POST',
        body: data
    });
    
    if(response.ok) {
        alert("Login successful");
        window.location.href = "/home/home.html";
        
    } else {
        alert("Login failed");
    }
    
}