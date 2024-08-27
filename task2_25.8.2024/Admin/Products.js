// var x = localStorage.getItem('categoryId');

// if (x == null) {

var url = 'https://localhost:44363/api/Products/Products/getAllProducts';
// } else {

//     let n = Number(x);
//    var url = `https://localhost:44363/api/Products/Products/GetProductByCategoryId/${n}`;

// }
async function GetProduct() {
    
    var response = await fetch(url);
    var result = await response.json();
    console.log(result);
    var container = document.getElementById('tableproduct');
    
    result.forEach(element => {
        
       
    container.innerHTML += `<tr>
         <td>${element.productId}</td>
         <td>${element.productName}</td>
         <td><img src="${element.productImage}" class="card-img-top" style="width:100px;height:50px ;" alt="..."></td>
         <td>${element.price}</td>
         <td><button onclick="Updateproduct(${element.productId})" class="btn btn-danger">Edit</button></td>
       </tr> `;
    });
    
    }
    
    GetProduct();
    async function Updateproduct(productId) {
        localStorage.setItem('productId', productId);
        alert('Product saved in local storage');
        window.location.href ="/Admin/Updateproduct.html";
     
    }