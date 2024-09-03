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
    let result = await response.json();
   
    
    if(response.ok) {
        localStorage.setItem("token", result.token);
        alert("Login successful");
        window.location.href = "/home/home.html";
        
    } else {
        alert("Login failed");
    }
    
}