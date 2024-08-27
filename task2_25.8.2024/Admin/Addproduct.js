
document.addEventListener("DOMContentLoaded", async function() {
    let selection = document.getElementById("Categorys");
    let url1 = "https://localhost:44363/api/Categories/Categorys/getAllCategories";
    var response = await fetch(url1);
    var result = await response.json();
    result.forEach(element => {
        var option = document.createElement("option");
        option.value = element.categoryId;
        option.innerHTML = element.categoryName;
        selection.appendChild(option);
    });
});
let url = "https://localhost:44363/api/Products";

let form = document.getElementById("formAddproduct");
async function AddProduct() {
    event.preventDefault();
    let data = new FormData(form);
    let categoryId = document.getElementById("Categorys").value;
    data.append("categoryId", categoryId);
    let response = await fetch(url, {
        method: 'POST',
        body: data
    });
    alert("Added product successfully");
    }

    // function categoryid(value) {
       
    //    localStorage.setItem('categoryid', value); 
    // }





