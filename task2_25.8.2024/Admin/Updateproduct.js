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
