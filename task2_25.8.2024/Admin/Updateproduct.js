let selection = document.getElementById("Categorys");
async function getcategory() {
    

let url1 = "https://localhost:44363/api/Categories/Categorys/getAllCategories";
var response = await fetch(url1);
var result = await response.json();
result.forEach(element => {
    var option = document.createElement("option");
    option.value = element.categoryId;
    option.innerHTML = element.categoryName;
    selection.appendChild(option);
});
}
getcategory();
let r = localStorage.getItem("productId");
let url = `https://localhost:44363/api/Products/UpdateProductById/${r}`;

var form = document.getElementById("formupdatproduct");

async function UpdateProduct() {
    event.preventDefault();
    let data = new FormData(form);
    
    let response = await fetch(url, {
        method: 'PUT',
        body: data
    });
    
    alert("Product updated successfully");
    
}
