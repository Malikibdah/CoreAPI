let url = "https://localhost:44363/api/Categories/AddCaegory";
let form = document.getElementById("formdata");

async function AddCategory() {
    event.preventDefault();
    let formData = new FormData(form);
    let response = await fetch(url, {
        method: 'POST',
        body: formData
    });
    
    alert("Added category successfully");
    // clear input fields
    document.getElementById("CategoryName").value = "";
    document.getElementById("CategoriesImage").value = "";
}