async function register() {
    const url = "https://localhost:44363/api/Users/Register";
    let form = document.getElementById("registerform")
    event.preventDefault();
    let data = new FormData(form);
    
    let response = await fetch(url, {
        method: 'POST',
        body: data
    });
    
    if(response.ok) {
        alert("Registration successful");
        window.location.href = "login.html";
    } else {
        alert("Registration failed");
    }
    
}