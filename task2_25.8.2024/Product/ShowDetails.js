
var n = Number (localStorage.getItem('productId'));
var url = `https://localhost:44363/api/Products/Products/GetProductById/${n}`;



async function GetProductById() {
    
    var response = await fetch(url);
    var result = await response.json();
    
    console.log(result);
    var container = document.getElementById('ShowDetails');
    
   
       
    container.innerHTML = `<div class="card">
         <h5 class="card-title">${result.productName}</h5>
         <img src="${result.productImage}" style="width:100%;height:100%px ; " class="card-img-top" alt="...">
         <div class="card-body">
           <h5 class="card-title">${result.productId}</h5>
           <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
           <input type="nummber" class="form-control mb-2" id="quentity" required>
           <button onclick="AddToCart(${result.productId})" class="btn btn-primary">Add to Cart</button>
         </div>
       </div> `;
       localStorage.setItem("cartId",1);
    
    }
    GetProductById();

async function AddToCart() {
  debugger;
let url1 = "https://localhost:44363/api/Cartitems";
let cartId = localStorage.getItem("cartId");
let productId = localStorage.getItem("productId");
debugger;
let quentity =  document.getElementById("quentity").value;
console.log(typeof quentity);
debugger;
if ( quentity == "") {
  alert("Please enter quantity");
  return;
}
  var data = 
  {
    cartId: cartId,
    productId: productId,
    quantity: quentity
  }

let response = await fetch(url1, {
  method : 'POST',
  body : JSON.stringify(data),
  headers : {
    'Content-Type' : 'application/json'
  }  
});
quentity.textContent = "Added to Cart";
alert("Product added to cart");



}

  

