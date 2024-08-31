
async function cartitem() {
    const url = "https://localhost:44363/api/Cartitems";
    let response = await fetch(url);
    let result = await response.json();
    
    let container = document.getElementById('cartitem');
    
    result.forEach(element => {
       
    container.innerHTML += `<tr>
         <td>${element.cartId}</td>
         <td>${element.productRsponseDTO.productName}</td>
         <td><img src="${element.productRsponseDTO.productImage}" class="card-img-top" style="width:100px;height:50px ;" alt="..."></td>
         <td>${element.productRsponseDTO.price}</td>
         <td><input type="number" class="form-control" id="quentityincart${element.cartItemId}" value="${element.quantity}"></td>
         <td><button onclick="Updateproduct(${element.cartItemId})" class="btn btn-primary">Edit</button></td>
         <td><button onclick="Deleteproduct(${element.cartItemId})" class="btn btn-danger">Delete</button></td>
       </tr> `;
    });
}
cartitem();

async function Updateproduct(m) {
   const url = `https://localhost:44363/api/Cartitems/CartItem/UpdateItem/${m}`;
   quentity = {quantity: document.getElementById(`quentityincart${m}`).value};
    let response = await fetch(url,
        {
            method : 'PUT',
            body : JSON.stringify(quentity),
            headers : {
                'Content-Type' : 'application/json'
              } 
        }
    );
    alert("Product Edit from cart");
   
}

async function Deleteproduct(m) {
    const url = `https://localhost:44363/api/Cartitems/CartItem/DeletCartItemById/${m}`;
    let response = await fetch(url,
        {
            method : 'DELETE',            
            headers : {
                'Content-Type' : 'application/json'
              } 
        }
    );
    alert("Product removed from cart");
    location.reload();
}