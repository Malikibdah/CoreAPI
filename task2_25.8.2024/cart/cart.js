const url = "https://localhost:44363/api/Cartitems";

async function cartitem() {
var response = await fetch(url);
    var result = await response.json();
    
    var container = document.getElementById('cartitem');
    
    result.forEach(element => {
       
    container.innerHTML += `<tr>
         <td>${element.cartId}</td>
         <td>${element.productRsponseDTO.productName}</td>
         <td><img src="${element.productRsponseDTO.productImage}" class="card-img-top" style="width:100px;height:50px ;" alt="..."></td>
         <td>${element.productRsponseDTO.price}</td>
         <td><input type="nummber" class="form-control" id="quentityincart" value="${element.quantity}"></td>
         <td><button onclick="Updateproduct(${element.productId})" class="btn btn-danger">Edit</button></td>
       </tr> `;
    });
}
    cartitem();