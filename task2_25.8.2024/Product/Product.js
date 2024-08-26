var x = localStorage.getItem('categoryId');

if (x == null) {

var url = 'https://localhost:44363/api/Products/Products/getAllProducts';
} else {

    let n = Number(x);
   var url = `https://localhost:44363/api/Products/Products/GetProductByCategoryId/${n}`;

}


async function GetProduct() {
    
var response = await fetch(url);
var result = await response.json();
console.log(result);
var container = document.getElementById('containerproduct');

result.forEach(element => {
    
   
container.innerHTML += `<div class="card">
     <h5 class="card-title">${element.productName}</h5>
     <img src="${element.productImage}" class="card-img-top" style="width:300px;height:200px ;" alt="...">
     <div class="card-body">
       <h5 class="card-title">${element.productId}</h5>
       <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
       <button onclick="ShowDetail(${element.productId})" class="btn btn-danger">Show More</button>
     </div>
   </div> `;
});

}

GetProduct();
async function ShowDetail(productId) {
    localStorage.setItem('productId', productId);
    alert('Category saved in local storage');
    window.location.href ="/product/ShowDetails.html";
 
}


